using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeService.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required, EmailAddress, MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int PinNumber { get; set; }

        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public long PhoneNumber { get; set; }

        [Required]
        public long SSN { get; set; }

        [Required, MaxLength(100)]
        public string StreetAddress { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        [Required, MaxLength(2)]
        public string State { get; set; }

        [Required]
        public int ZipCode { get; set; }

        public DateTime StartDate { get; set; }

        [MaxLength(100)]
        public string CompanyName { get; set; }

        public int? NumberOfStores { get; set; }

        [Required]
        public int UserType { get; set; }

        public int? CustomerId { get; set; }
    }
}
