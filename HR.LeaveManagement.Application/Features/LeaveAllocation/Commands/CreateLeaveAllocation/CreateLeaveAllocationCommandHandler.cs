using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repository, ILeaveTypeRepository leaveTypeRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // validate LeaveAllocation object
            CreateLeaveAllocationCommandValidator validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
                throw new BadRequestException("CreateLeaveAllocationCommand error", validationResult);

            //map createLeaveAllocationCommand into LeaveAllocation
            Domain.LeaveAllocation leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

            // add validated object to database
            await _repository.CreateAsync(leaveAllocation);
            return leaveAllocation.Id;
        }
    }
}
