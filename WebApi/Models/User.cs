namespace WebApi.Models
{
    public interface IUser
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Pass { get; set; }
    }

    public class User : IUser
    {
        public int? Id { get ; set ; }
        public string UserName { get ; set ; }
        public string Pass { get ; set ; }
    }
}
