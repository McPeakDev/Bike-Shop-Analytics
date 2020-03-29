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
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private IRepository<Category> _categoryRepo;
        private IRepository<Auth> _authRepo;

        public CategoryController(IRepository<Category> categoryRepo, IRepository<Auth> authRepo)
        {
            _categoryRepo = categoryRepo;
            _authRepo = authRepo;
        }

        [HttpGet("[action]/{categoryID}")]
        public async Task<ActionResult<Category>> Read([FromHeader(Name = "Token")]string token, int categoryID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                return await _categoryRepo.Read(so => so.CategoryID == categoryID);
            }
            return StatusCode(403);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromHeader(Name = "Token")]string token, Category category)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepo.Create(category);
                    return Ok("Success! Category Created!");
                }
                return Problem("Error! Could not create the category..");
            }
            return StatusCode(403);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromHeader(Name = "Token")]string token, Category category)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                if (ModelState.IsValid)
                {
                    await _categoryRepo.Update(category);
                    return Ok("Success! Category Updated!");
                }
                return Problem("Error! Could not update the sales order..");
            }
            return StatusCode(403);
        }

        [HttpDelete("[action]/{categoryID}")]
        public async Task<IActionResult> Delete([FromHeader(Name = "Token")]string token, int categoryID)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var category = await _categoryRepo.Read(a => a.CategoryID == categoryID);

                if (!(category is null))
                {
                    await _categoryRepo.Delete(category);
                    return Ok("Success! Category Deleted!");

                }
                return Problem("Error! Could not delete the category..");
            }
            return StatusCode(403);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Category>>> ReadAll([FromHeader(Name = "Token")]string token)
        {
            if(await _authRepo.Read(a => a.Token == token) != null)
            {
                var categoryList = await _categoryRepo.ReadAll();
                return categoryList.ToList();
            }
            return StatusCode(403);
        }
    }
}