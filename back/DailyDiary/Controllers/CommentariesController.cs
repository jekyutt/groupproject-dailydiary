using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyDiary.Model;
using DailyDiary.Model.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;

namespace DailyDiary.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentariesController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentariesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Commentaries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Commentary>>> GetCommentaries()
        {
            return await _context.Commentaries
                .Include(x=>x.Post)
                .Include(x=>x.Owner)
                .ToListAsync();
        }

        // GET: api/Commentaries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Commentary>> GetCommentary(int id)
        {
            var commentary = await _context.Commentaries.FindAsync(id);

            if (commentary == null)
            {
                return NotFound();
            }

            return commentary;
        }

        // PUT: api/Commentaries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentary(int id, Commentary commentary)
        {
            if (id != commentary.Id)
            {
                return BadRequest();
            }

            _context.Entry(commentary).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentaryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Commentaries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Commentary>> PostCommentary(AddCommentaryRequest request)
        {
            Commentary newCommentary = new Commentary
            {
                Id = _context.Commentaries.OrderBy(x => x.Id).Last().Id + 1,
                Text = request.Text,
                OwnerId = request.OwnerId,
                PostId = request.PostId
            };
            _context.Commentaries.Add(newCommentary);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentary", new { id = newCommentary.Id }, newCommentary);
        }

        // DELETE: api/Commentaries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentary(int id)
        {
            var commentary = await _context.Commentaries.FindAsync(id);
            if (commentary == null)
            {
                return NotFound();
            }

            _context.Commentaries.Remove(commentary);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Commentary> patchEntity)
        {
            var entity = await _context.Commentaries.FindAsync(id);
            if (entity == null) return BadRequest("Commentary not found");

            if (patchEntity != null)
            {
                patchEntity.ApplyTo(entity, ModelState);

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return Ok(entity);
        }

        private bool CommentaryExists(int id)
        {
            return _context.Commentaries.Any(e => e.Id == id);
        }
    }
}
