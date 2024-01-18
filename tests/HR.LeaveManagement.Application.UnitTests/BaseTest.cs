using AutoMapper;
using HR.LeaveManagementSystem.Application.MappingProfiles;

namespace HR.LeaveManagement.Application.UnitTests;

public class BaseTest
{
    protected IMapper _mapper;

    public BaseTest()
    {
        var mappingConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new LeaveTypeProfile());
            mc.AddProfile(new LeaveAllocationProfile());
            mc.AddProfile(new LeaveRequestProfile());
        });
        _mapper = mappingConfig.CreateMapper();;   
    }
}