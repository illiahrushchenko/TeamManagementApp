using NUnit.Framework;

namespace Application.IntegrationTests;

[TestFixture]
public class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        await Testing.ResetAsync();
    }
}