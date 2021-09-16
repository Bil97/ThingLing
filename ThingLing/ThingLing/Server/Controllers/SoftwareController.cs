using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThingLing.ImageResizer;
using ThingLing.Server.Data;
using ThingLing.Shared.Models;
using ThingLing.Shared.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThingLing.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SoftwareController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SoftwareController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Software
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get() => Ok(await _context.Software.ToListAsync());

        // POST api/<SoftwaresController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] string name, [FromForm] string url, [FromForm] string details)
        {
            try
            {
                var value = new Software
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Url = url,
                    Details = details,
                };

                if (Request.Form.Files.Any())
                {
                    var icon = Request.Form.Files.FirstOrDefault(x => x.Name == "Icon");
                    if (icon != null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(icon.FileName);
                        var iconPath = Path.Combine(BaseApi.Storage, fileName);
                        using var stream = icon.OpenReadStream();
                        // Resize the image
                        Resizer.ResizeImageFromStream(stream, iconPath, 50, 50);
                        value.IconPath = fileName;
                    }
                }

                _context.Software.Add(value);
                await _context.SaveChangesAsync();
                return Ok("Save successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SoftwaresController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromForm] string name, [FromForm] string url, [FromForm] string details)
        {
            try
            {
                var value = await _context.Software.FindAsync(id);
                if (value == null)
                    return NotFound();

                value.Name = name;
                value.Url = url;
                value.Details = details;

                if (Request.Form.Files.Any())
                {
                    var path = $"{BaseApi.Storage}/{value.IconPath}";
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    var icon = Request.Form.Files.FirstOrDefault(x => x.Name == "Icon");
                    if (icon != null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(icon.FileName);
                        var iconPath = Path.Combine(BaseApi.Storage, fileName);
                        using var stream = icon.OpenReadStream();
                        // Resize the image
                        Resizer.ResizeImageFromStream(stream, iconPath, 50, 50);
                        value.IconPath = fileName;
                    }
                }

                _context.Entry(value).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<SoftwaresController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var item = await _context.Software.FindAsync(id);
                if (item != null)
                {
                    _context.Software.Remove(item);
                    await _context.SaveChangesAsync();

                    var path = $"{BaseApi.Storage}/{item.IconPath}";
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

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
