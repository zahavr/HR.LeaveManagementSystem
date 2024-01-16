using AutoMapper;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
    }
}