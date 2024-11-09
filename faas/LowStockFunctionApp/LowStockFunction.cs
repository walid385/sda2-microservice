using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace LowStockFunctionApp
{
    public static class LowStockFunction
    {
        [FunctionName("LowStockFunction")]
        public static void Run(
            [RabbitMQTrigger("low_stock_alert_queue", ConnectionStringSetting = "RabbitMQConnection")] string message,
            ILogger logger)
        {
            logger.LogInformation($"Received message: {message}");
            // Add your logic here
        }
    }
}
