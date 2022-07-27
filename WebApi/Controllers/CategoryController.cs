#pragma warning disable CS1591
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Contexts;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private CategoryContext db;

        public CategoryController(CategoryContext db)
        {
            this.db = db;
        }

        /// <summary>
        /// Returns list of user's categories
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("userId")]
        public List<Category> GetCategories(int userId) =>
            db.Categories.Where(category => category.UserId == userId).ToList();

        /// <summary>
        /// Adds category for user
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory([FromBody] Category category)
        {
            if (category == null)
                throw new ArgumentNullException("Category is empty");

            db.Categories.Add(category);
            await db.SaveChangesAsync();
            return Ok(category);
        }

        /// <summary>
        /// Updates category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory([FromBody] Category category)
        {
            if (String.IsNullOrEmpty(category.Name))
                throw new ArgumentNullException("Category name is empty");

            var oldCategory = db.Categories.FirstOrDefault(category_ => category_.Id == category.Id);
            if (oldCategory == null)
                throw new ArgumentException("Category wasn't found");

            oldCategory.Name = category.Name;
            db.Update(oldCategory);
            await db.SaveChangesAsync();
            return Ok(oldCategory);
        }

        /// <summary>
        /// Deletes category
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        [HttpDelete("{categoryId}")]
        public async Task<ActionResult> DeleteCategory(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                throw new ArgumentException("Category wasn't found");
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return Ok(category);
        }
    }
}
