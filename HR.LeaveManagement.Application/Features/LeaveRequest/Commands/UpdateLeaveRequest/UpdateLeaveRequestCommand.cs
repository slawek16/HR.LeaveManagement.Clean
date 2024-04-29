using HR.LeaveManagement.Application.Features.LeaveRequest.Shared;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest
{
    public class UpdateLeaveRequestCommand : BaseLeaveRequest
    {
        public int Id { get; set; }
        public string RequestCommand { get; set; } = string.Empty;
        public bool Cancelled { get; set; }
    }
}
