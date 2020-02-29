using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikeShopAnalyticsAPI.Models.Entities;
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
        public Category Read(int categoryID)
        {
            return _categoryRepo.Read(so => so.CategoryID == categoryID);
        }

        [HttpPost("[action]")]
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Create(category);
                return Ok("Success! Category Created!");
            }
            return Problem("Error! Could not create the category..");
        }

        [HttpPut("[action]")]
        public IActionResult Update(Category category)
        {
            if (ModelState.IsValid)
            {
                _categoryRepo.Update(category);
                return Ok("Success! Category Updated!");
            }
            return Problem("Error! Could not update the sales order..");
        }

        [HttpDelete("[action]/{categoryID}")]
        public IActionResult Delete(int categoryID)
        {
            var category = Read(categoryID);
            if (!(category is null))
            {
                _categoryRepo.Delete(category);
                return Ok("Success! Category Deleted!");

            }
            return Problem("Error! Could not delete the category..");
        }
    }
}