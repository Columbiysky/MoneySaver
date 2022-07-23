var resList = ApiConnector.UserConnector.GetAllUsers("https://localhost:5001/User/Users");
Console.WriteLine("GetAllUsers finished");

var resUser = ApiConnector.UserConnector.GetUser("https://localhost:5001/User?Login=a&Pass=a");
Console.WriteLine("GetUser finished");

var newUser = ApiConnector.UserConnector.CreateUser("https://localhost:5001/User", new WebApi.Models.User { Login = "qw", Pass = "qw" });
Console.WriteLine("CreateUser finished");

newUser = ApiConnector.UserConnector.UpdateUser("https://localhost:5001/User", new WebApi.Models.User { Login = "qw", Pass = "qwa" });
Console.WriteLine("UpdateUser finished");