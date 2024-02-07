using HR.LeaveManagementSystem.Application.Contracts.Identity;
using HR.LeaveManagementSystem.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class BaseTest
{
    protected readonly HrDatabaseContext _hrDatabaseContext;

    public BaseTest()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase("HrLeaveManagementSystemDatabase").Options;
        var userServiceMock = new Mock<IUserService>();
        
        _hrDatabaseContext = new HrDatabaseContext(dbOptions, userServiceMock.Object);
    }
}