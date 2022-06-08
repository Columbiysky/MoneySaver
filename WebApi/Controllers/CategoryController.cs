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

        [HttpGet("{userId}")]
        public List<Category> GetCategories(int userId) =>
            db.Categories.Where(category => category.UserId == userId).ToList();

        [HttpPost("{userId}/{name}")]
        public async Task<ActionResult<string>> AddCategory(int userId, string? name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Category name is empty");

            db.Categories.Add(new Category { UserId = userId, Name = name });
            await db.SaveChangesAsync();
            return "Category has been added";
        }

        [HttpPut("{categoryId}/{name}")]
        public async Task<ActionResult<string>> UpdateCategory(int categoryId, string? name)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Category name is empty");

            var category = db.Categories.FirstOrDefault(category => category.Id == categoryId);
            if (category == null)
                throw new ArgumentException("Category wasn't found");

            category.Name = name;
            db.Update(category);
            await db.SaveChangesAsync();
            return "Update succesful";
        }

        [HttpDelete("{categoryId}")]
        public async Task<ActionResult<string>> DeleteCategory(int categoryId)
        {
            var category = db.Categories.FirstOrDefault(c => c.Id == categoryId);
            if (category == null)
                throw new ArgumentException("Category wasn't found");
            db.Categories.Remove(category);
            await db.SaveChangesAsync();
            return "Success delete";
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

        [HttpGet("{userId}")]
        public List<SubCategory> GetSubCategories(int userId)
        {
            var categoriesIds = dbCategory.Categories.Where(category => category.UserId == userId)
                                                .Select(category => category.Id).ToList();

            return db.SubCategories.Where(subCategory => categoriesIds.Contains( subCategory.LinkedCategoryId)).ToList();
        }

        [HttpPost("{userId}/{name}/{linkedCategoryId})")]
        public async Task<ActionResult<string>> AddSubCategory(int userId, string? name, int? linkedCategoryId)
        {
            if (linkedCategoryId == null)
            {
                return "Linked category field is empty";
            }
            else
            {
                if (String.IsNullOrEmpty(name))
                    throw new ArgumentNullException("Subcategory name is empty");

                db.SubCategories.Add(new SubCategory
                {
                    Name = name,
                    LinkedCategoryId = linkedCategoryId
                });
                await db.SaveChangesAsync();
                return "Subcategory has been added";
            }
        }

        [HttpPut("{subcategoryId}/{name}")]
        public async Task<ActionResult<string>> UpdateSubCategory(int subcategoryId, string name)
        {
            if(string.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name is empty");

            var subcategory = db.SubCategories.FirstOrDefault(subcat => subcat.Id == subcategoryId);
            if (subcategory == null)
                throw new ArgumentException("Subcategory wasn't found");

            subcategory.Name = name;
            db.Update(subcategory);
            await db.SaveChangesAsync();
            return "Subcategory updated succesfully";
        }

        [HttpDelete("{subcategoryId}")]
        public async Task<ActionResult<string>> DeleteSubCategory(int subcategoryId)
        {
            var subCategory = db.SubCategories.FirstOrDefault(subcat => subcat.Id == subcategoryId);
            if (subCategory == null)
                throw new ArgumentNullException("Subcategory wasn't found");

            db.SubCategories.Remove(subCategory);
            await db.SaveChangesAsync();
            return "Delete successful";
        }
    }
}
