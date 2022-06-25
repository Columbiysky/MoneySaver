#pragma warning disable CS1591
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

        [HttpPost]
        public async Task<ActionResult<DataModel>> InsertData([FromQuery] DataModel dataModel)
        {
            db.Data.Add(new DataModel
            {
                UserId = dataModel.UserId,
                CategoryId = dataModel.CategoryId,
                SubCategoryId = dataModel.SubCategoryId,
                CurrencyId = dataModel.CurrencyId,
                DateTimeOfPurchace = dataModel.DateTimeOfPurchace,
                Summ = dataModel.Summ,
                Comment = dataModel.Comment
            }) ;
            await db.SaveChangesAsync();
            return Ok(dataModel);
        }

        [HttpPut]
        public async Task<ActionResult<DataModel>> UpdateData([FromQuery] DataModel dataModel)
        {
            var info = db.Data.FirstOrDefault(i => i.Id == dataModel.Id);

            if (info == null)
                throw new ArgumentException("Item wasn't found");

            info.CategoryId = dataModel.CategoryId;
            info.SubCategoryId = dataModel.SubCategoryId;
            info.DateTimeOfPurchace = dataModel.DateTimeOfPurchace;
            info.Summ = dataModel.Summ;
            info.Comment = dataModel.Comment;
            info.CurrencyId = dataModel.CurrencyId;

            db.Update(info);
            await db.SaveChangesAsync();
            return Ok(info);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DataModel>> DeleteData(int id)
        {
            var info = db.Data.FirstOrDefault(i => i.Id == id);

            if(info == null)
                throw new ArgumentException("Item wasn't found");

            db.Data.Remove(info);
            await db.SaveChangesAsync();
            return Ok(info);
        }
    }
}
