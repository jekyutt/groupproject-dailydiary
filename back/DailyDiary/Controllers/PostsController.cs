using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DailyDiary.Model;
using DailyDiary.Model.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DailyDiary.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly DataContext _context;

        public PostsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            return await _context.Posts
                .Include(x => x.Owner)
                .Include(x => x.Commentaries).ThenInclude(x => x.Owner)
                .ToListAsync();
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // GET: api/Posts/5/owner
        [HttpGet("{id}/owner")]
        public ActionResult<User> GetPostOwner(int id)
        {
            var post = _context.Posts.Find(id);
            return _context.Users.First(x => x.Id == post.OwnerId);
        }

        // GET: api/Posts/5/commentaries
        [HttpGet("{id}/commentaries")]
        public async Task<ActionResult<IEnumerable<Commentary>>> GetPostCommentaries(int id)
        {
            return await _context.Commentaries.Where(x => x.PostId == id).ToListAsync();
        }

        // GET: api/Posts/5/likes
        [HttpGet("{id}/likes")]
        public async Task<ActionResult<IEnumerable<User>>> GetPostLikes(int id)
        {
            return _context.Posts.Include(x => x.PostLikes).ThenInclude(x => x.User).First(x => x.Id == id)
                .PostLikes.Select(x => x.User).ToList();
        }


        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, PutPostRequest request)
        {
            Post post = new Post
            {
                Id = id,
                Type = request.Type,
                Title = request.Title,
                Text = request.Text,
                ReleaseDate = request.ReleaseDate,
                NSFW = request.NSFW,
                ImageLink = CheckUrl(request.ImageLink) ? request.ImageLink : "",
                OwnerId = request.OwnerId,
                Owner = _context.Users.Find(request.OwnerId)
            };

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Post successfully updated");
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost(AddPostRequest request)
        {
            Post post = new Post
            {
                Id = _context.Posts.OrderBy(x => x.Id).Last().Id+1,
                Type = request.Type,
                Title = request.Title,
                Text = request.Text,
                ReleaseDate = request.ReleaseDate,
                NSFW = request.NSFW,
                ImageLink = CheckUrl(request.ImageLink) ? request.ImageLink : "",
                OwnerId = request.OwnerId,
                Owner = _context.Users.Find(request.OwnerId)
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]    
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<Post> patchEntity)
        {
            var entity = await _context.Posts.FindAsync(id);
            if (entity == null) return BadRequest("Post not found");

            if (patchEntity != null)
            {
                patchEntity.ApplyTo(entity, ModelState);

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return Ok(entity);
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }

        private bool CheckUrl(string input)
        {
            try
            {
                WebRequest.Create(input).GetResponse();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
