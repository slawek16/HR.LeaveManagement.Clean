using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
        }
        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            DeleteLeaveAllocationCommandValidator validations = new DeleteLeaveAllocationCommandValidator(_leaveAllocationRepository);
            var validationResults = await validations.ValidateAsync(request, cancellationToken);
            if (validationResults.Errors.Any())
                throw new BadRequestException(typeof(DeleteLeaveAllocationCommand).Name, validationResults);

            var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Domain.LeaveAllocation), request.Id);
            await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

            return Unit.Value;
        }
    }
}
