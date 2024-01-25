using AutoMapper;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
    }
}