using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
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

        [HttpGet(Name = "GetUsersList")]
        public List<User> GetUsers() =>
            db.Users.ToList();

        [HttpGet("{login}/{pass}")]
        public string Login(string? login, string? pass)
        {
            if(CheckCreditinals(login, pass))
            {
                var res = db.Users.Where(predicate: element => element.UserName == login).FirstOrDefault();
                if (res == null)
                    throw new ArgumentException("No user found with this login.");
                return "Done";
            }
            return "Something went wrog, try again";
        }

        [HttpPost("{login}/{pass}")]
        public string RegisterUser(string? login, string? pass)
        {
            if (CheckCreditinals(login, pass))
            {
                db.Users.Add(new WebApi.Models.User { UserName = login, Pass=pass});
                db.SaveChanges();
                return "Success registered";
            }
            return "Something went wrog, try again";
        }

        private bool CheckCreditinals(string? login, string? pass)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentNullException("Login field is empty");

            if (string.IsNullOrEmpty(pass))
                throw new ArgumentNullException("Pass field is empty");
            return true;
        }
    }
}
