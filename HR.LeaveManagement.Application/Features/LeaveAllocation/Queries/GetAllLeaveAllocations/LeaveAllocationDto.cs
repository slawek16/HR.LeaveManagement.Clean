namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class LeaveAllocationDto
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        public int LeaveTypeId { get; set; }
        public int Period { get; set; }
        public string EmployeeId { get; set; } = string.Empty;
    }
}
