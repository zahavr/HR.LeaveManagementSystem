using AutoMapper;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;
using HR.LeaveManagementSystem.Application.Features.LeaveAllocation.Models;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.MappingProfiles;

public class LeaveAllocationProfile : Profile
{
    public LeaveAllocationProfile()
    {
        CreateMap<LeaveAllocation, LeaveAllocationDto>().ReverseMap();
        CreateMap<LeaveAllocation, LeaveAllocationDetailsDto>();
        CreateMap<CreateLeaveAllocationCommand, LeaveRequest>();
        CreateMap<UpdateLeaveAllocationCommand, LeaveRequest>();
    }
}