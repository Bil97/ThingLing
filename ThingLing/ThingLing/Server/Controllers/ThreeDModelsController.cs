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
    public class ThreeDModelsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThreeDModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ThreeDModels
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get() => Ok(await _context.ThreeDModels.ToListAsync());

        // POST api/<ThreeDModelsController>
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] string name, [FromForm] string url)
        {
            try
            {
                var value = new ThreeDModel
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = name,
                    Url = url,
                };

                if (Request.Form.Files.Any())
                {
                    var displayImage = Request.Form.Files.FirstOrDefault(x => x.Name == "DisplayImage");
                    if (displayImage != null)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(displayImage.FileName);
                        var displayImagePath = Path.Combine(BaseApi.Storage, fileName);
                        using var stream = displayImage.OpenReadStream();
                        // Resize the image
                        Resizer.ResizeImageFromStream(stream, displayImagePath, 400, 225);
                        value.DisplayImagePath = fileName;
                    }
                }

                _context.ThreeDModels.Add(value);
                await _context.SaveChangesAsync();
                return Ok("Save successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<ThreeDModelsController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromForm] string name, [FromForm] string url)
        {
            try
            {
                var value = await _context.ThreeDModels.FindAsync(id);
                if (value == null)
                    return NotFound();

                value.Name = name;
                value.Url = url;

                if (Request.Form.Files.Any())
                {
                    var displayImage = Request.Form.Files.FirstOrDefault(x => x.Name == "DisplayImage");
                    if (displayImage != null)
                    {
                        var path = $"{BaseApi.Storage}/{value.DisplayImagePath}";
                        if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + Path.GetExtension(displayImage.FileName);
                        var displayImagePath = Path.Combine(BaseApi.Storage, $"{fileName}{Path.GetExtension(displayImage.FileName)}");
                        using var stream = displayImage.OpenReadStream();
                        // Resize the image
                        Resizer.ResizeImageFromStream(stream, displayImagePath, 400, 225);
                        value.DisplayImagePath = fileName;
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

        // DELETE api/<ThreeDModelsController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var item = await _context.ThreeDModels.FindAsync(id);
                if (item != null)
                {
                    _context.ThreeDModels.Remove(item);
                    await _context.SaveChangesAsync();

                    var path = $"{BaseApi.Storage}/{item.Name}";
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
