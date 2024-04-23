using FluentValidation;
using System.Data;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
    {
        public CreateLeaveAllocationCommandValidator()
        {
            RuleFor(p => p.leaveTypeId)
                .NotNull()
                .NotEmpty();

            RuleFor(p => p.employeeId)
                .NotNull()
                .NotEmpty();
        }
    }
}
