using Application.Boards.Commands.CreateBoard;
using Application.Boards.Queries.GetBoards;
using FluentAssertions;

namespace Application.IntegrationTests.Boards.Queries;

public class GetAvailableBoardsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnOwnBoards()
    {
        await Testing.RunAsUserAsync("ddd@gmail.com", "1234");

        await Testing.SendAsync(new CreateBoardCommand("OwnBoard"));
        await Testing.SendAsync(new CreateBoardCommand("OwnBoard2"));

        var boards = await Testing.SendAsync(new GetAvailableBoardsQuery());

        boards.Should().NotBeNull();
        boards.OwnBoards.Should().NotBeNullOrEmpty();
        boards.OwnBoards.Should().HaveCount(2);
    }

    [Test]
    public async Task ShouldReturnOtherBoards()
    {
        //Init member
        var memberUserId = await Testing.RunAsUserAsync("member@gmail.com", "1234");
        
        //Run as board owner
        await Testing.RunAsUserAsync("owner@gmail.com", "1234");
        await Testing.SendAsync(new CreateBoardCommand("Board"));
        //TODO: Add member by id

        //Run as board member
        Testing.UserId = memberUserId;
        await Testing.SendAsync(new GetAvailableBoardsQuery());
    }
}