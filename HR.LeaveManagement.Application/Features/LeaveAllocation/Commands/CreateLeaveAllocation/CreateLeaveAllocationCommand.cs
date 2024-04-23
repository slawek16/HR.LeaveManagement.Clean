using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public record CreateLeaveAllocationCommand(int leaveTypeId, int employeeId, int numberOfDays, int period) : IRequest<int>;
}
