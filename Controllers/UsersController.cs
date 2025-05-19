using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Entities;

namespace RestaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static List<Role> roles = new List<Role>
        {
            new Role(0,"Customer","Allows to make an order"),
            new Role(1,"Employee","Regular Employee"),
            new Role(2,"Supervisor","Allows to make an order"),
            new Role(3,"Manager","Allows to make an order"),
            new Role(4,"Administrator","System administrator")
        };

        private static List<User> users = new List<User>
        {
            new User(1,"John","Paul","JohnPaul","psswd","johnpaul37@mail.com",roles.FirstOrDefault(e=>e.Id==1)),
            new User(1,"Eric","Cartman","Erixx","pswd1","eric@mail.com",roles.FirstOrDefault(e=>e.Id==2)),
            new User(1,"","","","","",roles.FirstOrDefault(e=>e.Id==3)),
            new User(1,"","","","","",roles.FirstOrDefault(e=>e.Id==4))
        };

        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(users);
        }

        /// <summary>
        /// Get a user by ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = users.FirstOrDefault(e => e.Id == id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<User> Create(User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            users.Add(user);
            return CreatedAtAction(nameof(GetById), user.Id, user);
        }

        /// <summary>
        /// Update an existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public ActionResult<User> Update(int id, User user)
        {
            if (user == null || user.Id != id)
            {
                return BadRequest();
            }
            var existingUser = users.FirstOrDefault(e => e.Id == id);
            if (existingUser == null)
            {
                return NotFound();
            }

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Username = user.Username;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.Email = user.Email;
            existingUser.Role = user.Role;
            return NoContent();
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var user = users.FirstOrDefault(e => e.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            users.Remove(user);
            return NoContent();
        }
    }
}
