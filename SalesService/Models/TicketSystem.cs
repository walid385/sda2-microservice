using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace SalesService.Models
{
    public class TicketSystem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }
        public DateTime Date { get; set; }
        public string CompanyName { get; set; }
        public string Time { get; set; }
        public int Quantity { get; set; }
        public float Subtotal { get; set; }
        public float Total { get; set; }
        public float Cost { get; set; }
        public float Discount { get; set; }
        public float Tax { get; set; }
        public float TaxRate { get; set; }
        public float Cash { get; set; }
        public float Credit { get; set; }
        public bool CartPurchase { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
    }
}