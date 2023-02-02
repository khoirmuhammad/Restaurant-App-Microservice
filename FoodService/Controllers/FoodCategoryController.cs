using FoodService.Models;
using FoodService.Models.Customs;
using FoodService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodCategoryController : ControllerBase
    {
        private readonly IFoodCategoryRepository _foodCategoryRepository;
        public FoodCategoryController(IFoodCategoryRepository foodCategoryRepository)
        {
            _foodCategoryRepository = foodCategoryRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _foodCategoryRepository.GetAll();

            if (data == null || data.Count() < 1)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("Get")]
        public async Task<IActionResult> Get(string id)
        {
            var data = await _foodCategoryRepository.Get(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromBody] FoodCategoryModel model)
        {
            FoodCategory foodCategory = new FoodCategory
            {
                Id = string.IsNullOrEmpty(model.Id) ? ObjectId.GenerateNewId().ToString() : model.Id,
                Code = model.Code,
                Name = model.Name,
                Deleted = model.Deleted,
            };

            await _foodCategoryRepository.Save(foodCategory);

            if (string.IsNullOrEmpty(model.Id))
            {
                return StatusCode(201);
            }
            else
            {
                return NoContent();
            }
            

        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            await _foodCategoryRepository.Delete(id);
            return NoContent();
        }

        [HttpDelete("SoftDelete")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            await _foodCategoryRepository.SoftDelete(id);
            return NoContent();
        }
    }
}
