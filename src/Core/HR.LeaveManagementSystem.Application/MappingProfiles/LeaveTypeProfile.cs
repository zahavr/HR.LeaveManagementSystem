using AutoMapper;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.MappingProfiles;

public class LeaveTypeProfile : Profile
{
    public LeaveTypeProfile()
    {
        CreateMap<LeaveTypeDto, LeaveType>().ReverseMap();
        CreateMap<LeaveType, LeaveTypeDetailsDto>();
        CreateMap<CreateLeaveTypeCommand, LeaveType>();
        CreateMap<UpdateLeaveTypeCommand, LeaveType>();
    }
}