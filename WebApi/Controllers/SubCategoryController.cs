#pragma warning disable CS1591
using Microsoft.AspNetCore.Mvc;
using WebApi.Contexts;
using WebApi.Models;

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
        if (string.IsNullOrEmpty(subCategory.Name))
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