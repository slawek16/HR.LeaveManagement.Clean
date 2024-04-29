using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation
{
    public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _leaveAllocationRepository = leaveAllocationRepository;

            RuleFor(p => p.NumberOfDays)
                .GreaterThan(0).WithMessage("{PropertyName} must be greater than {ComparisionValue}.");

            //RuleFor(p => p.Period)
            //    .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("{PropertyName} must be after {ComparisionValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} must exist in database.");

            RuleFor(p => p.Id)
                .MustAsync(LeaveAllocationMustExist).WithMessage("{PropertyName} must exist in database.");
        }

        private async Task<bool> LeaveAllocationMustExist(int leaveAllocationId, CancellationToken token)
        {
            Domain.LeaveAllocation leaveAllocation = await _leaveAllocationRepository.GetByIdAsync(leaveAllocationId).ConfigureAwait(false);
            return leaveAllocation != null;
        }

        private async Task<bool> LeaveTypeMustExist(int leaveTypeId, CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeExist(leaveTypeId);
        }
    }
}
