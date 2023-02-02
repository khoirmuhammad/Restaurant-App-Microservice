using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Models;
using UserService.Models.Customs;
using UserService.Repositories;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserCustomerController : ControllerBase
    {
        private readonly IUnitOfWork _service;
        public UserCustomerController(IUnitOfWork service)
        {
            _service = service;
        }

        [HttpPost("PostUserCustomer")]
        public async Task<IActionResult> PostUserCustomer([FromBody] UserCustomerModel model)
        {
            ResponseModel<UserCustomerModel> response = new ResponseModel<UserCustomerModel>();

            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = model.Username,
                Password = model.Password
            };

            Customer customer = new Customer
            {
                UserId = user.Id,
                Name = model.Name,
                Address = model.Address,
                Phone = model.Phone,
                Point = 0,
            };

            await _service.UserRepository.Save(user);
            await _service.CustomerRepository.Save(customer);

            bool result = await _service.SaveAsync();

            if (result)
            {
                model.Id = user.Id;
                response.Data = model;

                return StatusCode(201, response);
            }
            else
            {
                throw new Exception("Something error happened when saving data");
            }

        }

        [HttpGet("GetCustomerByUserId")]
        public async Task<IActionResult> GetCustomerByUserId(Guid userId)
        {
            ResponseModel<CustomerModel> response = new ResponseModel<CustomerModel>();

            var customer = await _service.CustomerRepository.GetByUserId(userId);

            if (customer == null)
            {
                response.ErrorMessage = $"Not Found. User {userId} doesn't have customer relation data";

                return NotFound(response);
            }
            else
            {
                response.Data = new CustomerModel
                {
                    Id = customer.Id,
                    Name = customer.Name,
                    UserId = customer.UserId,
                    Address = customer.Address,
                    Phone = customer.Phone,
                    Point = customer.Point,
                };

                return Ok(response);
            }
        }

        [HttpGet("GetAllUserCustomer")]
        public async Task<IActionResult> GetAllUserCustomer()
        {
            ResponseModel<List<UserCustomerModel>> response = new ResponseModel<List<UserCustomerModel>>();

            var data = await _service.CustomerRepository.GetAll();

            if (data == null)
            {
                response.ErrorMessage = "List empty";

                return NotFound(response);
            }

            List<UserCustomerModel> userCustomers = new List<UserCustomerModel>();

            foreach(var item in data)
            {
                userCustomers.Add(new UserCustomerModel
                {
                    Id = item.User.Id,
                    Username = item.User.Username,
                    Name = item.Name,
                    Address = item.Address,
                    Point = item.Point,
                    Phone = item.Phone
                });
            }

            response.Data = userCustomers;

            return Ok(response);
        }

        [HttpPut("PutUserCustomer")]
        public async Task<IActionResult> PutUserCustomer([FromBody] UserCustomerModel model)
        {
            ResponseModel<UserCustomerModel> response = new ResponseModel<UserCustomerModel>();

            User? user = await _service.UserRepository.GetById(model.Id);

            Customer? customer = await _service.CustomerRepository.GetByUserId(model.Id);

            if (user == null || customer == null) 
            {
                response.ErrorMessage = $"Data {model.Id} Not Found";
                return BadRequest(response);
            }

            user.Password = model.Password;

            customer.Name = model.Name;
            customer.Address = model.Address;
            customer.Phone = model.Phone;
            customer.Point = model.Point;

            await _service.UserRepository.Save(user);
            await _service.CustomerRepository.Save(customer);

            bool result = await _service.SaveAsync();

            if (result)
            {
                model.Id = user.Id;
                response.Data = model;

                return NoContent();
            }
            else
            {
                throw new Exception("Something error happened when updating data");
            }
        }

        [HttpDelete("DeleteUserCustomerByUserId/{userid}")]
        public async Task<IActionResult> DeleteUserCustomerByUserId(Guid userid)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            await _service.UserRepository.SoftDelete(userid);
            await _service.CustomerRepository.SoftDelete(userid);

            bool result = await _service.SaveAsync();

            if (result)
            {
                return NoContent();
            }
            else
            {
                throw new Exception("Something error happened when deleting data");
            }
        }
    }
}
