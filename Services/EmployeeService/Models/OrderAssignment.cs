// EmployeeService.Models.OrderAssignment.cs
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Models
{
    public class OrderAssignment
    {
        [Key]
        public int AssignmentId { get; set; } // Primary Key

        [Required]
        public int OrderId { get; set; } // ID of the order

        [Required]
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; } // ID of the assigned employee

        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow; // Date of assignment

        // Navigation property
        public virtual Employee Employee { get; set; } // Links to Employee table
    }
}
