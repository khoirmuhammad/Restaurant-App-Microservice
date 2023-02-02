using FoodService.Models.Customs;
using FoodService.Models;
using FoodService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.Net.Mail;

namespace FoodService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodRepository _foodRepository;
        public FoodController(IFoodRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll() 
        {
            var data = await _foodRepository.GetAll();

            if (data == null || data.Count() < 1) 
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("GetByCategory")]
        public async Task<IActionResult> GetByCategory(string categoryCode)
        {
            var data = await _foodRepository.GetByCategory(categoryCode);

            if (data == null || data.Count() < 1)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpGet("GetById/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var data = await _foodRepository.Get(id);

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> Save([FromForm] FoodModel model)
        {
            Food food = new Food
            {
                Id = string.IsNullOrEmpty(model.Id) ? ObjectId.GenerateNewId().ToString() : model.Id,
                CategoryCode = model.CategoryCode,
                Name = model.Name,
                Description = model.Description,
                Size = model.Size,
                Discount = model.Discount,
                Price = model.Price,
                Image = GetImage(model.Image),
                Deleted = model.Deleted,
            };

            await _foodRepository.Save(food);

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
            await _foodRepository.Delete(id);
            return NoContent();
        }

        [HttpDelete("SoftDelete")]
        public async Task<IActionResult> SoftDelete(string id)
        {
            await _foodRepository.SoftDelete(id);
            return NoContent();
        }

        private byte[] GetImage(IFormFile image)
        {
            if (image.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    image.CopyTo(ms);
                    var fileBytes = ms.ToArray();

                    return fileBytes;
                }
            }
            else
            {
                return default!;
            }
        }
    }
}
