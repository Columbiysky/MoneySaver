#pragma warning disable CS1591
using Microsoft.AspNetCore.Mvc;
using Models;
using WebApi.Contexts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private UserContext db;

        public UserController(UserContext context)
        {
            db = context;
        }

        /// <summary>
        /// Get all registered users
        /// </summary>
        /// <returns>List of registered Users</returns>
        [HttpGet]
        [Route("Users")]
        public List<User> GetUsers() =>
            db.Users.ToList();

        /// <summary>
        /// Get one user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Checks is user registered or not</returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpGet]
        public ActionResult<User> Login([FromQuery] User user)
        {
            if (CheckCreditinals(user))
            {
                var res = db.Users.FirstOrDefault(element => element.Login == user.Login
                                                          && element.Pass == user.Pass);
                if (res == null)
                    throw new ArgumentException("No user found with this login");
                return Ok(res);
            }
            throw new ArgumentException("No user found with these creditinals");
        }

        /// <summary>
        /// Registers new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Info is registration completed succesful or not</returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPost]
        public async Task<ActionResult<User>> RegisterUser([FromBody] User user)
        {
            if (CheckCreditinals(user))
            {
                db.Users.Add(new Models.User
                {
                    Login = user.Login,
                    Pass = user.Pass
                });
                await db.SaveChangesAsync();
                return Ok(GetResult(user));
            }
            throw new ArgumentException("Something went wrog, try again");
        }

        /// <summary>
        /// Updates info about user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Info was update succesful or not</returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        public async Task<ActionResult<User>> UpdateUser([FromBody] User user)
        {
            if (CheckCreditinals(user))
            {
                var oldUser = db.Users.FirstOrDefault(user_ => user_.Id == user.Id 
                    || user_.Login == user.Login);
                if (oldUser == null)
                    throw new ArgumentException("User wasn't found");
                oldUser.Login = user.Login;
                oldUser.Pass = user.Pass;
                db.Update(oldUser);
                await db.SaveChangesAsync();
                return Ok(GetResult(oldUser));
            }
            throw new ArgumentException("No user found with these creditinals");
        }

        private bool CheckCreditinals(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
                throw new ArgumentNullException("Login field is empty");

            if (string.IsNullOrEmpty(user.Pass))
                throw new ArgumentNullException("Pass field is empty");
            return true;
        }

        private User GetResult(User user)
        {
            if (CheckCreditinals(user)) 
            {
                var res = db.Users.FirstOrDefault(element => element.Login == user.Login && element.Pass == user.Pass);
                if (res == null)
                    throw new ArgumentException("No user found with these creditinals");
                else return res;
            }
            throw new ArgumentException("Something went wrog");
        }
    }
}
