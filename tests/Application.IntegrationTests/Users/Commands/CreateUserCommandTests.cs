using Application.Users.Commands.CreateUser;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Users.Commands.CreateUser;

public class CreateUserCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateUser()
    {
        var userId = await Testing.SendAsync(new CreateUserCommand("mail@gmail.com",
            "1111"));

        var user = await Testing.FindAsync<User>(userId);

        user.Should().NotBeNull();
    }
}