using AutoMapper;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;
using HR.LeaveManagementSystem.Application.Features.LeaveRequest.Models;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagementSystem.Application.MappingProfiles;

public class LeaveRequestProfile : Profile
{
    public LeaveRequestProfile()
    {
        CreateMap<LeaveRequest, LeaveRequestListDto>().ReverseMap();
        CreateMap<LeaveRequest, LeaveRequestDetailsDto>();
        CreateMap<CreateLeaveRequestCommand, LeaveRequest>();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequest>();
    }
}