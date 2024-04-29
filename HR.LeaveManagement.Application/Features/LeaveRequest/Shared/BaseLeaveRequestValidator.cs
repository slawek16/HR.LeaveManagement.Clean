﻿using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Shared
{
    public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
            RuleFor(p => p.StartDate)
                .LessThan(p => p.EndDate).WithMessage("{PropertyName} must be before {ComparisionValue}");

            RuleFor(p => p.EndDate)
                .GreaterThan(p => p.StartDate).WithMessage("{PropertyName} must be after {ComparisionValue}");

            RuleFor(p => p.LeaveTypeId)
                .GreaterThan(0)
                .MustAsync(LeaveTypeMustExist).WithMessage("{PropertyName} does not exist.");
        }

        private async Task<bool> LeaveTypeMustExist(int id, CancellationToken token)
        {
            return await _leaveTypeRepository.IsLeaveTypeExist(id); 
        }
    }
}
