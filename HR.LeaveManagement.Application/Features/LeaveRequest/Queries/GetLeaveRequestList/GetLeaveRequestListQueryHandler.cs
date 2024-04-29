using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList
{
    public class GetLeaveRequestListQueryHandler : IRequestHandler<GetLeaveRequestListQuery, List<LeaveRequestListDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveRequestRepository _leaveRequestRepository;

        public GetLeaveRequestListQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestRepository)
        {
            _mapper = mapper;
            _leaveRequestRepository = leaveRequestRepository;
        }
        public async Task<List<LeaveRequestListDto>> Handle(GetLeaveRequestListQuery request, CancellationToken cancellationToken)
        {
            List<Domain.LeaveRequest> leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetails();
            List<LeaveRequestListDto> leaveRequestListDtos = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
            return leaveRequestListDtos;
        }
    }
}
