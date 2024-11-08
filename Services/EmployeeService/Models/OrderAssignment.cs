using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeService.Models
{
    public class OrderAssignment
    {
        [Key]
        public int AssignmentId { get; set; } 

        [Required]
        public int OrderId { get; set; } 

        [Required]
        public int EmployeeId { get; set; } 

        [Required]
        public DateTime AssignedDate { get; set; } = DateTime.UtcNow; 

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; } 
    }
}
