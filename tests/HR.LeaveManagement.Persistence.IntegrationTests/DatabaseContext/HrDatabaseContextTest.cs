using HR.LeaveManagement.Persistence.IntegrationTests.AutoFixture;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagement.Persistence.IntegrationTests.DatabaseContext;

public class HrDatabaseContextTest : BaseTest
{
    private readonly Fixture _fixture = new AutoFixtureBuilder()
        .OmitRecursionGeneration()
        .EnableAutoMoq()
        .Create();
    
    [Fact]
    public async Task Save_SetDateCreatedValue()
    {
        var leaveType = _fixture.Build<LeaveType>()
            .Without(p => p.DateCreated)
            .Without(p => p.DateModified)
            .Create();

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateCreated.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Save_SetDateModifiedValue()
    {
        var leaveType = _fixture.Build<LeaveType>()
            .Without(p => p.DateCreated)
            .Without(p => p.DateModified)
            .Create();

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();
        
        leaveType.Name = _fixture.Create<string>();
        _hrDatabaseContext.LeaveTypes.Update(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        leaveType.DateModified.ShouldNotBeNull();
    }
}