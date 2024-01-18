using HR.LeaveManagement.Application.UnitTests.AutoFixture;
using HR.LeaveManagementSystem.Application.Contracts.Persistence;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Models;
using HR.LeaveManagementSystem.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagementSystem.Domain;

namespace HR.LeaveManagement.Application.UnitTests.Features.LeaveTypes.Queries;

public class GetLeaveTypesQueryHandlerTests : BaseTest
{
    private readonly Fixture _fixture = new AutoFixtureBuilder()
        .OmitRecursionGeneration()
        .EnableAutoMoq()
        .Create();

    [Fact]
    public async Task Handle_LeaveTypesExists_LeaveTypeCollectionReturned()
    {
        List<LeaveType> leaveTypes = _fixture.CreateMany<LeaveType>().ToList();
        List<LeaveTypeDto> expected = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        
        var leaveTypeRepositoryMock = _fixture.Freeze<Mock<ILeaveTypeRepository>>();
        leaveTypeRepositoryMock.Setup(r => r.GetAllAsync())
            .ReturnsAsync(leaveTypes);

        var mapperMock = _fixture.Freeze<Mock<IMapper>>();
        mapperMock.Setup(m => m.Map<List<LeaveTypeDto>>(leaveTypes))
            .Returns(expected);

        var sut = _fixture.Create<GetLeaveTypesQueryHandler>();
        var request = _fixture.Create<GetLeaveTypesQuery>();
        List<LeaveTypeDto> actual = await sut.Handle(request, CancellationToken.None);

        actual.ShouldBeEquivalentTo(expected);
    }
}