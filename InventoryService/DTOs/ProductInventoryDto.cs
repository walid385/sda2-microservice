namespace InventoryService.DTOs
{
    public class ProductInventoryDto
    {
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public string ProductSubType { get; set; }
        public float UnitPrice { get; set; }
        public float Cost { get; set; }
        public int InStock { get; set; }
        public int VendorId { get; set; }
    }
}
