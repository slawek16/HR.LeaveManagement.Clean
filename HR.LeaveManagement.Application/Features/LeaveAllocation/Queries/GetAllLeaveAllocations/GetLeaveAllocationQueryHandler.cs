using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations
{
    public class GetLeaveAllocationQueryHandler : IRequestHandler<GetLeaveAllocationQuery, List<LeaveAllocationDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveAllocationRepository _repository;

        public GetLeaveAllocationQueryHandler(IMapper mapper, ILeaveAllocationRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationQuery request, CancellationToken cancellationToken)
        {
            //Get all records from repository
            var leaveAllocations = await _repository.GetLeaveAllocationWithDetails();

            //return all results
            var leaveAllocationsDto = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);
            return leaveAllocationsDto;
        }
    }
}
