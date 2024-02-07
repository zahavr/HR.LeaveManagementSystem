using AutoMapper;
using HR.LeaveManagementSystem.UI.Models;
using HR.LeaveManagementSystem.UI.Models.LeaveAllocations;
using HR.LeaveManagementSystem.UI.Models.LeaveRequests;
using HR.LeaveManagementSystem.UI.Models.LeaveTypes;
using HR.LeaveManagementSystem.UI.Services.Base;

namespace HR.LeaveManagementSystem.UI.MappingProfiles;

public class MappingConfig : Profile
{
    public MappingConfig()
    {
        CreateMap<LeaveTypeDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<LeaveTypeDetailsDto, LeaveTypeViewModel>().ReverseMap();
        CreateMap<CreateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<UpdateLeaveTypeCommand, LeaveTypeViewModel>().ReverseMap();
        CreateMap<RegisterViewModel, RegistrationRequest>().ReverseMap();
        CreateMap<CreateLeaveRequestCommand, LeaveRequestViewModel>().ReverseMap();
        
        CreateMap<LeaveRequestListDto, LeaveRequestViewModel>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.DateRequested.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ReverseMap();
        CreateMap<LeaveRequestDetailsDto, LeaveRequestViewModel>()
            .ForMember(q => q.DateRequested, opt => opt.MapFrom(x => x.RequestedDate.DateTime))
            .ForMember(q => q.StartDate, opt => opt.MapFrom(x => x.StartDate.DateTime))
            .ForMember(q => q.EndDate, opt => opt.MapFrom(x => x.EndDate.DateTime))
            .ForMember(q => q.DateActioned, opt => opt.MapFrom(x => x.DateActioned))
            .ReverseMap();
        
        CreateMap<CreateLeaveRequestCommand, LeaveRequestViewModel>().ReverseMap();
        CreateMap<UpdateLeaveRequestCommand, LeaveRequestViewModel>().ReverseMap();

        CreateMap<LeaveAllocationDto, LeaveAllocationViewModel>().ReverseMap();
        CreateMap<LeaveAllocationDetailsDto, LeaveAllocationViewModel>().ReverseMap();
        CreateMap<CreateLeaveAllocationCommand, LeaveAllocationViewModel>().ReverseMap();
        CreateMap<UpdateLeaveAllocationCommand, LeaveAllocationViewModel>().ReverseMap();

        CreateMap<EmployeeViewModel, Employee>().ReverseMap();
    }
}