using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Contexts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private DataConext db;
        public DataController(DataConext db)
        {
            this.db = db;
        }

        [HttpGet("{userId}")]
        public List<DataModel> GetData(int userId) =>
            db.Data.Where(info => info.UserId == userId).ToList();

        [HttpPost("{userId}/{dateTime}/{categoryId}/" +
            "{currencyId}/{summ}/{comment}")]
        public string InsertData(int userId, string dateTime, int categoryId,
                                int currencyId, decimal summ, string comment)
        {
            db.Data.Add(new DataModel
            {
                UserId = userId,
                CategoryId = categoryId,
                CurrencyId = currencyId,
                DateTimeOfPurchace = DateTime.Parse(dateTime),
                Summ = summ,
                Comment = comment
            });
            db.SaveChanges();
            return "Succesfully added";
        }

        [HttpPost("{userId}/{dateTime}/{categoryId}/{subCategoryId}/" +
            "{currencyId}/{summ}/{comment}")]
        public string InsertData(int userId, string dateTime, int categoryId, int subCategoryId,
                                int currencyId, decimal summ, string comment)
        {
            db.Data.Add(new DataModel
            {
                UserId = userId,
                CategoryId = categoryId,
                SubCategoryId = subCategoryId,
                CurrencyId = currencyId,
                DateTimeOfPurchace = DateTime.Parse(dateTime),
                Summ = summ,
                Comment = comment
            }) ;
            db.SaveChanges();
            return "Succesfully added";
        }

        [HttpPut("{id}/{dateTime}/{categoryId}/" +
            "{currencyId}/{summ}/{comment}")]
        public string UpdateData(int id, DateTime dateTime, int categoryId,
                                int currencyId, decimal summ, string comment)
        {
            var info = db.Data.FirstOrDefault(i => i.Id == id);

            if (info == null)
                throw new ArgumentException("Item wasn't found");
            
            info.CategoryId = categoryId;
            info.DateTimeOfPurchace = dateTime;
            info.Summ = summ;
            info.Comment = comment;
            info.CurrencyId = currencyId;

            db.Update(info);
            db.SaveChanges();
            return "Update succesful";
        }

        [HttpPut("{id}/{dateTime}/{categoryId}/{subCategoryId}/" +
            "{currencyId}/{summ}/{comment}")]
        public string UpdateData(int id, DateTime dateTime, int categoryId, int subCategoryId,
                                int currencyId, decimal summ, string comment)
        {
            var info = db.Data.FirstOrDefault(i => i.Id == id);

            if (info == null)
                throw new ArgumentException("Item wasn't found");

            info.CategoryId = categoryId;
            info.SubCategoryId = subCategoryId;
            info.DateTimeOfPurchace = dateTime;
            info.Summ = summ;
            info.Comment = comment;
            info.CurrencyId = currencyId;

            db.Update(info);
            db.SaveChanges();
            return "Update succesful";
        }

        [HttpDelete("{id}")]
        public string DeleteData(int id)
        {
            var info = db.Data.FirstOrDefault(i => i.Id == id);

            if(info == null)
                throw new ArgumentException("Item wasn't found");

            db.Data.Remove(info);
            db.SaveChanges();
            return "Success delete";
        }
    }
}
