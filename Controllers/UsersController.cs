using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {// هيكل بيانات في الذاكرة (hashmap)
        private static readonly Dictionary<string, User> _usersDb = new();

        // إنشاء مستخدم جديد (Create)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = Guid.NewGuid().ToString();
            user.Id = userId;
            _usersDb[userId] = user;
            return CreatedAtAction(nameof(GetUser), new { userId = userId }, user);
        }

        // قراءة مستخدم (Read)
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUser(string userId)
        {
            if (!_usersDb.ContainsKey(userId))
            {
                return NotFound("User not found");
            }
            return Ok(_usersDb[userId]);
        }

        // قراءة كل المستخدمين (Read all)
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            return Ok(_usersDb.Values);
        }

        // تحديث مستخدم (Update)
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(string userId, [FromBody] User updatedUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_usersDb.ContainsKey(userId))
            {
                return NotFound("User not found");
            }

            updatedUser.Id = userId;
            _usersDb[userId] = updatedUser;
            return Ok(updatedUser);
        }

        // حذف مستخدم (Delete)
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(string userId)
        {
            if (!_usersDb.ContainsKey(userId))
            {
                return NotFound("User not found");
            }

            var deletedUser = _usersDb[userId];
            _usersDb.Remove(userId);
            return Ok(new { Message = "User deleted", Id = userId, User = deletedUser });
        }
    }
}
