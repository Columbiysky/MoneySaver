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

        /// <summary>
        /// Returns data about your expenses
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public List<DataModel> GetData(int userId) =>
            db.Data.Where(info => info.UserId == userId).ToList();

        /// <summary>
        /// Insert your expenses
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<DataModel>> InsertData([FromBody] DataModel dataModel)
        {
            db.Data.Add(dataModel);
            await db.SaveChangesAsync();
            return Ok(dataModel);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="dataModel"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        public async Task<ActionResult<DataModel>> UpdateData([FromBody] DataModel dataModel)
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

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
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
