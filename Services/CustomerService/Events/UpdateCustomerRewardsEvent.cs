namespace CustomerService.Events
{
    public class UpdateCustomerRewardsEvent
    {
        public int CustomerId { get; set; }
        public int Points { get; set; }
    }
}