using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using ThingLing.Server.Data;
using ThingLing.Shared.Models.UserAccount;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThingLing.Server.Controllers.UserAccount
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]

    public class ContactFormsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IEmailSender _emailSender;

        public ContactFormsController(ApplicationDbContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }
        // GET: api/<ContactFormsController>
        [HttpGet]
        async public Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _context.ContactForms.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ContactFormsController>/5
        [HttpGet("{id}")]
        async public Task<ActionResult> Get(string id)
        {
            try
            {
                var contact = await _context.ContactForms.FirstOrDefaultAsync(x => x.Id == id);
                if (contact != null)
                {
                    return Ok(contact);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ContactFormsController>
        [HttpPost]
        [AllowAnonymous]
        async public Task<ActionResult> Post([FromBody] ContactForm contact)
        {
            try
            {
                contact.Id = Guid.NewGuid().ToString();
                _context.ContactForms.Add(contact);
                await _context.SaveChangesAsync();
                return Ok("Message sent");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ContactFormsController>
        [HttpPost("reply")]
        async public Task<ActionResult> PostReply([FromBody] ContactForm contact)
        {
            try
            {
                var reply = $"{contact.Reply} <br/><br/>Message previously sent to ThingLing website:<br/><br/>{contact.Message}";
                await _emailSender.SendEmailAsync(contact.Email, contact.Subject, reply);
                return Ok("Message sent");
            }
            catch (SmtpFailedRecipientException)
            {
                return BadRequest("Invalid email address");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ContactFormsController>/5
        [HttpDelete("{id}")]
        async public Task<ActionResult> Delete(string id)
        {
            try
            {
                var contact = await _context.ContactForms.FindAsync(id);
                if (contact != null)
                {
                    _context.ContactForms.Remove(contact);
                    await _context.SaveChangesAsync();
                    return Ok("Delete successful");
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
