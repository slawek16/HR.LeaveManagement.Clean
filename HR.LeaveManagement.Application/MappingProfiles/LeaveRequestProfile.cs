using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagement.Application.Features.LeaveRequest.Queries.GetLeaveRequestList;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveRequestProfile : Profile
    {
        public LeaveRequestProfile()
        {
            CreateMap<LeaveRequest, LeaveRequestListDto>().ReverseMap();
            CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
            CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        }
    }
}
