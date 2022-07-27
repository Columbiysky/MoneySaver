//var resList = ApiConnector.UserConnector.GetAllUsers("https://localhost:5001/User/Users");
//Console.WriteLine("GetAllUsers finished");

//var resUser = ApiConnector.UserConnector.GetUser("https://localhost:5001/User?Login=a&Pass=a");
//Console.WriteLine("GetUser finished");

//var newUser = ApiConnector.UserConnector.CreateUser("https://localhost:5001/User", new WebApi.Models.User { Login = "qw", Pass = "qw" });
//Console.WriteLine("CreateUser finished");

//newUser = ApiConnector.UserConnector.UpdateUser("https://localhost:5001/User", new WebApi.Models.User { Login = "qw", Pass = "qwa" });
//Console.WriteLine("UpdateUser finished");

//var resCategories = ApiConnector.CategoryConnector.GetCategories("https://localhost:5001/Category/userId?userId=5");
//Console.WriteLine("GetCategories finished");

//var newCategory = ApiConnector.CategoryConnector.CreateCategory("https://localhost:5001/Category", new WebApi.Models.Category { Name = "created category", UserId = 22});
//Console.WriteLine("CreateCategory finished");

//var updatedCategory = ApiConnector.CategoryConnector.UpdateCategory("https://localhost:5001/Category", new WebApi.Models.Category {Id = newCategory.Id, Name = "updated category", UserId = 22 });
//Console.WriteLine("UpdateCategory finished");

//var deletedCategory = ApiConnector.CategoryConnector.DeleteCategory($"https://localhost:5001/Category/{updatedCategory.Id}");
//Console.WriteLine("DeleteCategory finished");

//var resSubC = ApiConnector.SubCategoryConnector.GetSubCategories("https://localhost:5001/SubCategory/5");
//Console.WriteLine("GetSubCategories finished");

//var newSubC = ApiConnector.SubCategoryConnector.CreateSubCategory("https://localhost:5001/SubCategory/", new WebApi.Models.SubCategory { LinkedCategoryId = 4, Name = "subC conn test"});
//Console.WriteLine("GetSubCategories finished");

//newSubC = ApiConnector.SubCategoryConnector.UpdateSubCategory("https://localhost:5001/SubCategory/", new WebApi.Models.SubCategory { Id= newSubC.Id, LinkedCategoryId = 2, Name = "UPD subC conn test" });
//Console.WriteLine("GetSubCategories finished");

//var resp = ApiConnector.SubCategoryConnector.DeleteSubCategory($"https://localhost:5001/SubCategory/{newSubC.Id}");
//Console.WriteLine("GetSubCategories finished");


//var resData = ApiConnector.DataConnector.GetData("https://localhost:5001/Data/1");
//Console.WriteLine("GetData finished");

//var newData = ApiConnector.DataConnector.CreateData("https://localhost:5001/Data/",
//    new WebApi.Models.DataModel
//    {
//        UserId = 1,
//        DateTimeOfPurchace = DateTime.Now,
//        CategoryId = 1,
//        CurrencyId = 1,
//        Summ = 100,
//        SubCategoryId = 1,
//        Comment = "create test"
//    });
//Console.WriteLine("CreateData finished");

//newData = ApiConnector.DataConnector.UpdateData("https://localhost:5001/Data/",
//    new WebApi.Models.DataModel
//    {
//        Id = newData.Id,
//        UserId = 2,
//        DateTimeOfPurchace = DateTime.Now,
//        CategoryId = 2,
//        CurrencyId = 2,
//        Summ = 200,
//        SubCategoryId = 2,
//        Comment = "UPD create test"
//    });
//Console.WriteLine("UpdateData finished");

//var resp = ApiConnector.DataConnector.DeleteData($"https://localhost:5001/Data/{newData.Id}");