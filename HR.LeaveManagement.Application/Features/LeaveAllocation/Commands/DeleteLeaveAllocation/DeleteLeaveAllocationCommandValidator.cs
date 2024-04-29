using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandValidator : AbstractValidator<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandValidator(ILeaveAllocationRepository leaveAllocationRepository)
        {
            RuleFor(q => q.Id)
                .MustAsync(LeaveAllocationExists)
                .WithMessage("Leave Allocation does not exist in database.");

            _leaveAllocationRepository = leaveAllocationRepository;
        }

        private async Task<bool> LeaveAllocationExists(int id, CancellationToken token)
        {
            var allocation = await _leaveAllocationRepository.GetLeaveAllocationWithDetails(id);
            return allocation != null;
        }
    }
}
