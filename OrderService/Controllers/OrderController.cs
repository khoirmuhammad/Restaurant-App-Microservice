using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Models.Customs;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _service;
        public OrderController(IUnitOfWork service)
        {
            _service = service;
        }

        [HttpPost("PostOrder")]
        public async Task<IActionResult> PostOrder([FromBody] OrderModel model)
        {
            ResponseModel<Guid> response = new ResponseModel<Guid>();

            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                UserId = model.UserId,
                Date = DateTime.Now,
                BillTotal = model.BillTotal,
                DiscountByPoint = model.DiscountByPoint,
                FinalTotal = model.FinalTotal,
                Status = 1
            };

            List<OrderDetail> orderDetail = new List<OrderDetail>();

            foreach(var item in model.Details)
            {
                orderDetail.Add(new OrderDetail()
                {
                    OrderId = order.Id,
                    FoodItem = item.FoodItem,
                    Qty = item.Qty,
                    TotalPrice = item.TotalPrice,
                    ItemDiscount = item.ItemDiscount,
                    FinalPrice = item.FinalPrice,
                });
            }

            await _service.OrderRepository.Save(order);
            await _service.OrderDetailRepository.SaveBulk(orderDetail);

            bool result = await _service.SaveAsync();

            if (result)
            {
                response.Data = order.Id;

                return StatusCode(201, response);
            }
            else
            {
                throw new Exception("Something error happened when saving data");
            }

        }

        [HttpPut]
        public async Task<IActionResult> PutOrderStatus(Guid orderId, int status)
        {
            ResponseModel<bool> response = new ResponseModel<bool>();

            var order = await _service.OrderRepository.GetOrderById(orderId);

            if (order == null)
            {
                response.Data = false;
                response.ErrorMessage = $"Order {orderId} Not Found";

                return NotFound(response);
            }

            order.Status = status;
            await _service.OrderRepository.UpdateStatus(order);

            bool result = await _service.SaveAsync();

            if (result)
            {
                return NoContent();
            }
            else
            {
                throw new Exception("Something error happened when updating data");
            }
        }

    }
}
