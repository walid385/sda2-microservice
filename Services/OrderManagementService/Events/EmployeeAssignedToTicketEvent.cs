namespace Events
{
    public class EmployeeAssignedToTicketEvent
    {
        public int TicketId { get; set; }
        public int EmployeeId { get; set; }
    }

}