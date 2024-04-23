using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repository;

        public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveAllocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<int> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            // validate LeaveAllocation object
            CreateLeaveAllocationCommandValidator validator = new CreateLeaveAllocationCommandValidator();
            validator.Validate(request);

            //map createLeaveAllocationCommand into LeaveAllocation
            Domain.LeaveAllocation leaveAllocation = _mapper.Map<Domain.LeaveAllocation>(request);

            // add validated object to database
            await _repository.CreateAsync(leaveAllocation);
            return leaveAllocation.Id;
        }
    }
}
