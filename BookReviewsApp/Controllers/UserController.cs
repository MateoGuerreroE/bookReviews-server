using BookReviewsApp.Data.Helpers;
using BookReviewsApp.Data.Repositories;
using BookReviewsApp.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookReviewsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(await _userRepository.GetAllUsers());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            return Ok(await _userRepository.GetUserDetails(id));
        }

        [HttpPost("register")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null) { return BadRequest(); }
            user.key = "";
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            var created = await _userRepository.InsertUser(user);
            return Created("created", created);
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] User user)
        {
            if (user == null) { return BadRequest(); }
            if (!ModelState.IsValid) { return BadRequest(ModelState); }
            await _userRepository.UpdateUser(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            await _userRepository.DeleteUser(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginUser([FromBody] UserCredentials userCredentials)
        {
            if (userCredentials == null) { return BadRequest(); }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _userRepository.LoginUser(userCredentials.email, userCredentials.password));
        }

        [HttpPut("updatePassword")]
        public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            if (!ModelState.IsValid) return BadRequest(ModelState);
            return Ok(await _userRepository.ChangePassword(model.UserId, model.Password));
        }

    }
}
