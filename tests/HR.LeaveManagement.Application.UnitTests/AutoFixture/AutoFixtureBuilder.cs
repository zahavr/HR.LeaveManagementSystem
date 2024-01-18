using AutoFixture.AutoMoq;

namespace HR.LeaveManagement.Application.UnitTests.AutoFixture;

public class AutoFixtureBuilder
{
    private Fixture _fixture = new();

    public Fixture Create() => _fixture;

    public AutoFixtureBuilder OmitRecursionGeneration()
    {
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        return this;
    }

    public AutoFixtureBuilder EnableAutoMoq()
    {
        _fixture.Customize(new AutoMoqCustomization());
        return this;
    }

}