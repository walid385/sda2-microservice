namespace SalesService.DTOs
{
    public class RegistersTableDto
    {
        public int RegisterId { get; set; }
        public float OpenTotal { get; set; }
        public float CloseTotal { get; set; }
        public DateTime OpenTime { get; set; }
        public DateTime CloseTime { get; set; }
        public string Note { get; set; }
    }
}