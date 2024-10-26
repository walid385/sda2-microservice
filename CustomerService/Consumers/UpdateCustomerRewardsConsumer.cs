using MassTransit;
using CustomerService.Events;
using CustomerService.Repositories;


namespace CustomerService.Consumers
{
    public class UpdateCustomerRewardsConsumer : IConsumer<UpdateCustomerRewardsEvent>
    {
        private readonly ICustomerRepository _repository;

        public UpdateCustomerRewardsConsumer(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<UpdateCustomerRewardsEvent> context)
        {
            var message = context.Message;
            await _repository.UpdateCustomerRewardsAsync(message.CustomerId, message.Points);
        }
    }
}