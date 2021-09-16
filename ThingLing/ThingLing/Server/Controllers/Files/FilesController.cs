using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ThingLing.Server.Data;
using ThingLing.Shared.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThingLing.Server.Controllers.Files
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class FilesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FilesController(ApplicationDbContext context)
        {
            _context = context;
            Directory.CreateDirectory(BaseApi.Storage);
        }

        // GET: api/<FilesController>
        [HttpGet]
        async public Task<ActionResult> Get()
        {
            try
            {
                return Ok(await _context.Files.ToListAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<FilesController>/fileName
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult Get(string id)
        {
            try
            {
                var path = $"{BaseApi.Storage}/{id}";
                var file = new FileStream(path, FileMode.Open);
                return Ok(file);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
        // GET api/<FilesController>/fileName
        [HttpGet("profile/{id}")]
        [AllowAnonymous]
        public ActionResult GetProfile(string id)
        {
            try
            {
                var path = $"{BaseApi.Storage}/{id}";
                var file = new FileStream(path, FileMode.Open);
                return Ok(file);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        // POST api/<FilesController>
        [HttpPost]
        [DisableRequestSizeLimit]
        [AllowAnonymous]
        async public Task<ActionResult> Post()
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    foreach (var formFile in Request.Form.Files)
                    {
                        var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + "___" + formFile.FileName;
                        var path = Path.Combine(BaseApi.Storage, fileName);
                        using var stream = new FileStream(path, FileMode.Create);
                        await formFile.CopyToAsync(stream);

                        var file = new ThingLing.Shared.Models.Files.File
                        {
                            Id = Guid.NewGuid().ToString(),
                            Name = fileName
                        };

                        _context.Files.Add(file);
                    }
                }
                await _context.SaveChangesAsync();

                return Ok("Upload complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        // POST api/<FilesController>
        [HttpPost("profile")]
        async public Task<ActionResult> PostProfile()
        {
            try
            {
                if (Request.Form.Files.Any())
                {
                    var formFile = Request.Form.Files[0];
                    var fileName = $"bil.png";
                    var path = Path.Combine(BaseApi.Storage, fileName);
                    using var stream = new FileStream(path, FileMode.Create);
                    await formFile.CopyToAsync(stream);
                }

                return Ok("Upload complete");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<FilesController>/fileId
        [HttpDelete("{id}")]
        async public Task<ActionResult> Delete(string id)
        {
            try
            {
                var file = await _context.Files.FindAsync(id);
                if (file != null)
                {
                    _context.Files.Remove(file);
                    await _context.SaveChangesAsync();

                    var path = $"{BaseApi.Storage}/{file.Name}";
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }

                    return Ok("Success");
                }
                return NotFound("File not found");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
