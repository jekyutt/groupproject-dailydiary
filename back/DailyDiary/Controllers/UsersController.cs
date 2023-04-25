using DailyDiary.Model;
using DailyDiary.Model.Requests;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DailyDiary.Model.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DailyDiary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly DataContext _context;

        public UsersController(UserManager<User> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUserResponse>>> GetUsers(string username)
        {
            var query = _userManager.Users
                .Include(x => x.UsersPosts)
                .Include(x => x.UsersCommentaries).AsQueryable();

            if (username != null)
            {
                query = query.Where(x => x.UserName.ToUpper().Contains(username.ToUpper())); 
            }

            List<GetUserResponse> responseList = new List<GetUserResponse>();

            foreach (var user in query)
            {
                responseList.Add(new GetUserResponse
                {
                    Id = user.Id,
                    Type = user.Type,
                    UserName = user.UserName,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber,
                    Bio = user.Bio,
                    Adult = user.Adult,
                    AvatarLink = user.AvatarLink
                });
            }

            return responseList;
        }

        // GET: api/Users/test
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUserResponse>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return new GetUserResponse
            {
                Id = user.Id,
                Type = user.Type,
                UserName = user.UserName,
                Email = user.Email,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                BirthDate = user.BirthDate,
                PhoneNumber = user.PhoneNumber,
                Bio = user.Bio,
                Adult = user.Adult,
                AvatarLink = user.AvatarLink
            };
        }

        // GET: api/Users/5/posts
        [HttpGet("{id}/posts")]
        public async Task<ActionResult<IEnumerable<Post>>> GetUserPosts(string id)
        {
            return await _context.Posts.Where(x => x.OwnerId == id).ToListAsync();
        }

        // GET: api/Users/5/commentaries
        [HttpGet("{id}/commentaries")]
        public async Task<ActionResult<IEnumerable<Commentary>>> GetUserCommentaries(string id)
        {
            return await _context.Commentaries.Where(x => x.OwnerId == id).ToListAsync();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, PutUserRequest request)
        {
            var user = _userManager.Users.AsNoTrackingWithIdentityResolution().First(x => x.Id == id);

            user.Type = request.Type;
            user.UserName = request.Username;
            user.Email = request.Email;
            user.Firstname = request.Firstname;
            user.Lastname = request.Lastname;
            user.PhoneNumber = request.PhoneNumber;
            user.Bio = request.Bio;
            user.AvatarLink =
                CheckUrl(request.AvatarLink) ? request.AvatarLink : "https://i.imgur.com/0pNBFeA.png";

            //_userManager.Users.Update(user);
            

            try
            {
                await _userManager.UpdateAsync(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserRegistrationRequestDto user)
        {
            if (ModelState.IsValid)
            {
                if (!CheckEmailOnReal(user.Email))
                {
                    return new JsonResult(
                            new RegistrationResponse()
                            {
                                Result = false,
                                Errors = new List<string>(){
                                    "Email is not valid"
                                }
                            })
                    { StatusCode = 400 };
                }

                // check i the user with the same email exist
                var existingEmail = await _userManager.FindByEmailAsync(user.Email);

                if (existingEmail != null)
                {
                    return BadRequest(new RegistrationResponse()
                    {
                        Result = false,
                        Errors = new List<string>(){
                                            "Email already exist"
                                        }
                    });
                }

                var newUser = new User
                {
                    Type = Model.User.UserType.Public,
                    UserName = user.UserName,
                    Email = user.Email,
                    Firstname = user.Firstname,
                    Lastname = user.Lastname,
                    BirthDate = user.BirthDate,
                    PhoneNumber = user.PhoneNumber,
                    Bio = user.Bio,
                    Adult = IsAdult(user.BirthDate),
                    AvatarLink = CheckUrl(user.AvatarLink) ? user.AvatarLink : "https://i.imgur.com/0pNBFeA.png"
                };

                var isCreated = await _userManager.CreateAsync(newUser, user.Password);
                if (isCreated.Succeeded)
                {
                    User createdUser = await _userManager.FindByEmailAsync(newUser.Email);
                    return CreatedAtAction("GetUser", new { id = createdUser.Id }, createdUser);
                }

                return new JsonResult(
                        new RegistrationResponse()
                        {
                            Result = false,
                            Errors = isCreated.Errors.Select(x => x.Description).ToList()
                        })
                { StatusCode = 400 };
            }

            return BadRequest(new RegistrationResponse()
            {
                Result = false,
                Errors = new List<string>(){
                                            "Invalid payload"
                                        }
            });

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Users/5
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] JsonPatchDocument<User> patchEntity)
        {
            var entity = await _userManager.FindByIdAsync(id);
            if (entity == null) return BadRequest("User not found");

            if (patchEntity != null)
            {
                patchEntity.ApplyTo(entity, ModelState);

                _context.Entry(entity).State = EntityState.Modified;

                await _context.SaveChangesAsync();
            }

            return Ok(entity);
        }

        private bool UserExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }

        private bool IsAdult(DateTime birthDate)
        {
            return birthDate.AddYears(18) <= DateTime.Now;
        }

        private bool CheckValidData(string email, string username)
        {
            if (!CheckEmailOnExist(email) || !CheckUsernameOnExist(username)) return false;
            return true;
        }

        private bool CheckUsernameOnExist(string input)
        {
            var check = _userManager.Users.Where(x => x.UserName == input).ToList();

            if (check.Count > 0) return false;
            return true;
        }

        private bool CheckEmailOnExist(string input)
        {
            var checkDup = _userManager.Users.Where(x => x.Email == input).ToList();
            if (checkDup.Count > 0) return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(input);
                return addr.Address == input;
            }
            catch (Exception e)
            {
                return false;
            }

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

        private bool CheckEmailOnReal(string input)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(input);
                return addr.Address == input;
            }
            catch (Exception e)
            {
                return false;
            }

        }

    }
}
