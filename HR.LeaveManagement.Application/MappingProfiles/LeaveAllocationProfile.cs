using AutoMapper;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;
using HR.LeaveManagement.Domain;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveAllocationProfile : Profile
    {
        public LeaveAllocationProfile()
        {
            CreateMap<CreateLeaveAllocationCommand, LeaveAllocation>();
            CreateMap<LeaveAllocationDto, LeaveAllocation>().ReverseMap();
        }
    }
}
