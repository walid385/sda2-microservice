using MassTransit;
using SalesService.Events;

namespace SalesService.Services
{
    public class RewardService
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public RewardService(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task UpdateCustomerRewards(int customerId, int points)
        {
            var rewardEvent = new UpdateCustomerRewardsEvent
            {
                CustomerId = customerId,
                Points = points
            };

            await _publishEndpoint.Publish(rewardEvent);
        }
    }
}
