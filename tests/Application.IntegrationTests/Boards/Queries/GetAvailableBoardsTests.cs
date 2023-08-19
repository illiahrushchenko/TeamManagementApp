using Application.Boards.Commands.AddMember;
using Application.Boards.Commands.CreateBoard;
using Application.Boards.Queries.GetBoards;
using FluentAssertions;

namespace Application.IntegrationTests.Boards.Queries;

public class GetAvailableBoardsTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnOwnBoards()
    {
        await Testing.RunAsDefaultUserAsync();

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
        var memberEmail = "member@gmail.com";
        var memberUserId = await Testing.RunAsUserAsync(memberEmail, "1234");
        
        //Run as board owner
        await Testing.RunAsUserAsync("owner@gmail.com", "1234");
        var boardId = await Testing.SendAsync(new CreateBoardCommand("Board"));
        await Testing.SendAsync(new AddMemberCommand(boardId, memberEmail));

        //Run as board member
        Testing.UserId = memberUserId;
        var boards = await Testing.SendAsync(new GetAvailableBoardsQuery());
        
        boards.Should().NotBeNull();
        boards.OtherBoards.Should().NotBeNullOrEmpty();
        boards.OtherBoards.Should().HaveCount(1);
    }
}