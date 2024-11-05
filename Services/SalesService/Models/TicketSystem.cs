using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SalesService.Models
{
    public class TicketSystem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        
        public int ProductId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
        
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public string CompanyName { get; set; } = "DefaultCompany";
        public string Time { get; set; } = DateTime.UtcNow.ToString("HH:mm:ss");
        
        public decimal? Subtotal { get; set; }

        // Make optional properties nullable if they aren’t always needed
        public float? Cost { get; set; }
        public float? Discount { get; set; }
        public float? Tax { get; set; }
        public float? TaxRate { get; set; }
        public float? Cash { get; set; }
        public float? Credit { get; set; }
        public bool? CartPurchase { get; set; }
        public int? EmployeeId { get; set; }
    }
}
