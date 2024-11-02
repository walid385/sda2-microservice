namespace VendorService.DTOs
{
 public class UpdateVendorDto
    {
        public int VendorId { get; set; }
        public string CompanyName { get; set; }
        public string Department { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public long PhoneNumber { get; set; }
        public long FaxNumber { get; set; }
        public string Email { get; set; }
    }
}