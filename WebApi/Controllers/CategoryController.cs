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
        /// <param name="categoty"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult<Category>> AddCategory(Category categoty)
        {
            if (categoty == null)
                throw new ArgumentNullException("Category is empty");

            db.Categories.Add(categoty);
            await db.SaveChangesAsync();
            return Ok(categoty);
        }

        /// <summary>
        /// Updates category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        public async Task<ActionResult<Category>> UpdateCategory(Category category)
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

    [ApiController]
    [Route("[controller]")]
    public class SubCategoryController : ControllerBase
    {
        private SubCategoryContext db;
        private CategoryContext dbCategory;

        public SubCategoryController(SubCategoryContext db, CategoryContext db_)
        {
            this.db = db;
            this.dbCategory = db_;
        }

        /// <summary>
        /// Returns subcategories
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public List<SubCategory> GetSubCategories(int userId)
        {
            var categoriesIds = dbCategory.Categories
                .Where(category => category.UserId == userId)
                .Select(category => category.Id).ToList();

            return db.SubCategories.Where(subCategory => 
                categoriesIds.Contains(subCategory.LinkedCategoryId)).ToList();
        }

        /// <summary>
        /// Adds subcategory
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpPost]
        public async Task<ActionResult<SubCategory>> AddSubCategory(SubCategory subCategory)
        {
            if (subCategory.LinkedCategoryId == null)
                throw new ArgumentNullException("Linked oldCategory field is empty");

            else
            {
                if (String.IsNullOrEmpty(subCategory.Name))
                    throw new ArgumentNullException("Subcategory name is empty");

                db.SubCategories.Add(new SubCategory
                {
                    Name = subCategory.Name,
                    LinkedCategoryId = subCategory.LinkedCategoryId
                });
                await db.SaveChangesAsync();
                return Ok(subCategory);
            }
        }

        /// <summary>
        /// Updates subcategory
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        [HttpPut]
        public async Task<ActionResult<SubCategory>> UpdateSubCategory(SubCategory subCategory)
        {
            if(string.IsNullOrEmpty(subCategory.Name))
                throw new ArgumentNullException("Name is empty");

            var oldSubCategory = db.SubCategories.FirstOrDefault(subcat => subcat.Id == subCategory.Id);
            if (oldSubCategory == null)
                throw new ArgumentException("Subcategory wasn't found");

            oldSubCategory.Name = subCategory.Name;
            db.Update(oldSubCategory);
            await db.SaveChangesAsync();
            return Ok(oldSubCategory);
        }

        /// <summary>
        /// Deletes subcategory
        /// </summary>
        /// <param name="subcategoryId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        [HttpDelete("{subcategoryId}")]
        public async Task<ActionResult<SubCategory>> DeleteSubCategory(int subcategoryId)
        {
            var subCategory = db.SubCategories.FirstOrDefault(subcat => subcat.Id == subcategoryId);
            if (subCategory == null)
                throw new ArgumentNullException("Subcategory wasn't found");

            db.SubCategories.Remove(subCategory);
            await db.SaveChangesAsync();
            return Ok(subCategory);
        }
    }
}
