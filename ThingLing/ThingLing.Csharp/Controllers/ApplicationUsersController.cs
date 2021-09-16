using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ThingLing.Data;
using ThingLing.Shared.Models.UserAccount;
using ThingLing.Shared.Services;

namespace ThingLing.Controllers.UserAccount
{
    public class ApplicationUsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IEmailSender _emailSender;

        public ApplicationUsersController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration,
            IEmailSender emailSender)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _emailSender = emailSender;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> GetApplicationUser()
        {
            try
            {
                var applicationUsers = await _userManager.Users.ToListAsync();
                List<UserDetails> users = new List<UserDetails>();
                foreach (var applicationUser in applicationUsers)
                {
                    var userDetails = new UserDetails
                    {
                        Id = applicationUser.Id,
                        Email = applicationUser.Email,
                        DateCreated = applicationUser.DateCreated
                    };
                    users.Add(userDetails);
                }

                return View(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetApplicationUser(string id)
        {
            try
            {
                var applicationUser = await _userManager.Users.FirstOrDefaultAsync(i => i.UserName == id);

                if (applicationUser == null)
                {
                    return NotFound("User not found");
                }
                var userDetails = new UserDetails
                {
                    Id = applicationUser.Id,
                    Email = applicationUser.Email,
                    DateCreated = applicationUser.DateCreated
                };

                return View(userDetails);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-account/{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateApplicationUser(string id, UserDetails userDetails)
        {
            ApplicationUser applicationUser = await _userManager.GetUserAsync(User);

            if (id != applicationUser?.Id)
            {
                return BadRequest("Invalid request");
            }

            applicationUser.Email = userDetails.Email;
            try
            {
                await _userManager.UpdateAsync(applicationUser);
                return View("Account update successful");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound("User does not exist");
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-account")]
        public async Task<ActionResult> CreateApplicationUser([FromBody] Register register)
        {
            try
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = register.Email,
                    Email = register.Email,
                    DateCreated = DateTimeOffset.Now
                };

                var result = await _userManager.CreateAsync(applicationUser, register.Password);

                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault().Description);
                }
                else
                {
                    var user = await _userManager.FindByEmailAsync(register.Email);
                    // Add the first user to the admins role
                    if (_userManager.Users.Count() <= 1)
                    {
                        await _userManager.AddToRoleAsync(user, "Admin");
                        user.EmailConfirmed = true;
                        await _userManager.UpdateAsync(user);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, "User");
                    }

                    var callbackUrl = $"{BaseApi.Url}/confirm-email/{user.Id}/{user.SecurityStamp}";

                    var confirmEmail = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                    try
                    {
                        await _emailSender.SendEmailAsync(applicationUser.Email, "Confirm your email", confirmEmail);
                    }
                    catch (SmtpFailedRecipientException)
                    {
                        await _userManager.DeleteAsync(user);
                        await _context.SaveChangesAsync();
                        return NotFound("Invalid email address");
                    }
                    return Ok(new JwtSecurityTokenHandler().WriteToken(await Token(register.Email)));
                }
            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("confirm-email")]
        public async Task<ActionResult> ConfirmEmail([FromBody] ConfirmEmail confirmEmail)
        {
            if (confirmEmail.Id == null || confirmEmail.Code == null)
            {
                return NotFound($"Unable to load user with ID '{confirmEmail.Id}'.");
            }
            try
            {
                var user = await _userManager.FindByIdAsync(confirmEmail.Id);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{confirmEmail.Id}'.");
                }

                if (user.SecurityStamp == confirmEmail.Code)
                {
                    user.EmailConfirmed = true;
                    await _context.SaveChangesAsync();
                    return Ok("Thank you for confirming your email. Login in to access your acoount");
                }
                else
                {
                    return BadRequest("Error confirming your email.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login login)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false);

                if (!result.Succeeded)
                {
                    return BadRequest("Incorrect email or password");
                }

                return Ok(new JwtSecurityTokenHandler().WriteToken(await Token(login.Email)));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        async private Task<JwtSecurityToken> Token(string email)
        {
            var user = await _signInManager.UserManager.FindByEmailAsync(email);
            var roles = await _signInManager.UserManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.Now.AddDays(Convert.ToInt32(_configuration["JwtExpiryInDays"]));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtAudience"],
                claims,
                expires: expiry,
                signingCredentials: creds
            );
            return token;
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(forgotPassword.Email);
                if (user == null)
                {
                    return NotFound("Email does not exist");
                }

                var callbackUrl = $"{BaseApi.Url}/user-account/reset-password/{user.Id}/{user.SecurityStamp}";
                var confirmEmail = $"Reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
                await _emailSender.SendEmailAsync(forgotPassword.Email, "Reset your password", confirmEmail);

                return Ok("Check your email for a link to reset your password");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult> ResetPassword([FromBody] ResetPassword resetPassword)
        {
            if (resetPassword.Id == null || resetPassword.Code == null)
            {
                return NotFound($"Unable to load user with ID '{resetPassword.Id}'.");
            }
            try
            {
                var user = await _userManager.FindByIdAsync(resetPassword.Id);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{resetPassword.Id}'.");
                }

                if (user.SecurityStamp == resetPassword.Code)
                {
                    try
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _context.SaveChangesAsync();

                        var result = await _userManager.AddPasswordAsync(user, resetPassword.NewPassword);
                        if (result.Succeeded)
                        {
                            return Ok("Password reset successful. Login in to access your acoount");
                        }
                        else
                        {
                            return BadRequest(result.Errors.FirstOrDefault().Description);
                        }
                    }
                    catch (Exception ex)
                    {
                        return BadRequest(ex.Message);
                    }
                }
                else
                {
                    return BadRequest("Error reseting your password.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePassword changePassword)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (user == null)
            {
                return NotFound("An error occured when updating your password");
            }

            try
            {
                var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors.FirstOrDefault().Description);
                }
                else
                {
                    return Ok("Password change successfull.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete-account/{id}")]
        [Authorize]
        public async Task<ActionResult<ApplicationUser>> DeleteApplicationUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                ApplicationUser applicationUser = await _userManager.GetUserAsync(User);

                if (id == applicationUser.Id || User.IsInRole("Admin"))

                {
                    await _userManager.DeleteAsync(applicationUser);
                    return Ok("Account delete successful");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApplicationUserExists(id))
                {
                    return NotFound("User does not exist");
                }
                else
                {
                    throw;
                }
            }
            return BadRequest("Invalid request");

        }

        private bool ApplicationUserExists(string id)
        {
            try
            {
                return _userManager.Users.Any(e => e.Id == id);
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
