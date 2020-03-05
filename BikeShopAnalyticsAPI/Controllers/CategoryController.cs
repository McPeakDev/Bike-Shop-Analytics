using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
using BikeShopAnalyticsAPI.Models.Entities.Loggin_In;
using BikeShopAnalyticsAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BikeShopAnalyticsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private IRepository<Category> _categoryRepo;

        public CategoryController(IRepository<Category> categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }

        [HttpGet("[action]/{categoryID}")]
        public async Task<Category> Read(int categoryID)
        {
            return await _categoryRepo.Read(so => so.CategoryID == categoryID);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepo.Create(category);
                return Ok("Success! Category Created!");
            }
            return Problem("Error! Could not create the category..");
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(Category category)
        {
            if (ModelState.IsValid)
            {
                await _categoryRepo.Update(category);
                return Ok("Success! Category Updated!");
            }
            return Problem("Error! Could not update the sales order..");
        }

        [HttpDelete("[action]/{categoryID}")]
        public async Task<IActionResult> Delete(int categoryID)
        {
            var category = new Category();//Read(categoryID);
            if (!(category is null))
            {
                await _categoryRepo.Delete(category);
                return Ok("Success! Category Deleted!");

            }
            return Problem("Error! Could not delete the category..");
        }

        [HttpGet("[action]")]
        public async Task<List<Category>> ReadAll()
        {
            var categoryList = await _categoryRepo.ReadAll().ToList();
            return categoryList;
        }
    }
}