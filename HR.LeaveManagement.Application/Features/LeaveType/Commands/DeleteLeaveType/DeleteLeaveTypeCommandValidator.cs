using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandValidator : AbstractValidator<DeleteLeaveTypeCommand>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
        {
            RuleFor(p => p)
                .MustAsync(LeaveTypeIdExists)
                .WithMessage("Id does not exist in database");
            
            _leaveTypeRepository = leaveTypeRepository;
        }

        private Task<bool> LeaveTypeIdExists(DeleteLeaveTypeCommand command, CancellationToken token)
        {
            return _leaveTypeRepository.IsLeaveTypeExist(command.Id);
        }
    }
}
