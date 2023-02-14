using MessageBrokerLibrary;
using Newtonsoft.Json;
using OrderService.Events.Models;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Events
{
    public class FoodEventHandler : IHostedService
    {
        private readonly ISubscriber _subscriber;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IUnitOfWork _unitOfWork;
        public FoodEventHandler(ISubscriber subscriber, IServiceScopeFactory scopeFactory)
        {
            _subscriber = subscriber;
            _scopeFactory = scopeFactory;
            _unitOfWork = scopeFactory.CreateScope().ServiceProvider.GetRequiredService<IUnitOfWork>();
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            _subscriber.Subscribe(DoInsertFoodData);
            return Task.CompletedTask;
        }

        private bool DoInsertFoodData(string message, IDictionary<string, object> headers)
        {
            if (!string.IsNullOrEmpty(message))
            {
                var food = JsonConvert.DeserializeObject<Food>(message);

                if (food == null)
                {
                    return false;
                }

                FoodViewModel data = new FoodViewModel
                {
                    Code = food.Id,
                    Name = food.Name,
                    Category = food.CategoryCode,
                    Size = food.Size,
                    Price = food.Price,
                    Discount = food.Discount,
                    MinQtyDiscount = food.MinQtyDiscount,
                    Image = food.Image,
                    Deleted = food.Deleted,
                };

                _unitOfWork.FoodViewModelRepository.Save(data);
                bool result = _unitOfWork.SaveChanges();

                return result;
            }
            else
            {
                return false;
            }            
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

    }
}
