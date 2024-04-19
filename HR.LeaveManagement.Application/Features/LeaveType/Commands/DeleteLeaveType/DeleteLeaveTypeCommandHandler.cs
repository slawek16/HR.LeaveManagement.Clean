using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository) => _leaveTypeRepository = leaveTypeRepository;
        public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteLeaveTypeCommandValidator(_leaveTypeRepository);
            var validatorResult = await validator.ValidateAsync(request);
            if (validatorResult.Errors.Any())
                throw new BadRequestException("Invalid Leavetype", validatorResult);

            var leaveTypeToDelete = await _leaveTypeRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Domain.LeaveType), request.Id);
            await _leaveTypeRepository.DeleteAsync(leaveTypeToDelete);
            return Unit.Value;
        }
    }
}
