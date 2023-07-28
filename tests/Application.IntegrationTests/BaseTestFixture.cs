using NUnit.Framework;

namespace Application.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await Testing.ResetAsync();
    }
}