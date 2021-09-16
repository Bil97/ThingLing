using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using ThingLing.Server.Data;
using ThingLing.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ThingLing.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class VideosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public VideosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Videos
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> Get() => Ok(await _context.Videos.ToListAsync());

        // POST api/<VideosController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Video value)
        {
            try
            {
                value.Id = Guid.NewGuid().ToString();
                _context.Videos.Add(value);
                await _context.SaveChangesAsync();
                return Ok("Save successfull");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<VideosController>/5
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] Video value)
        {
            try
            {
                _context.Entry(value).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return Ok("Update successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<VideosController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                var item = await _context.Videos.FindAsync(id);
                if (item != null)
                {
                    _context.Videos.Remove(item);
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
