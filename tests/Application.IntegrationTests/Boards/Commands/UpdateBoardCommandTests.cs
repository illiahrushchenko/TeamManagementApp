using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.UpdateBoard;
using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Boards.Commands;

public class UpdateBoardCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBoardId()
    {
        var command = new UpdateBoardCommand(99, "New Title");
        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldUpdateBoard()
    {
        await Testing.RunAsUserAsync("ddd@gmail.com", "1234");

        var boardId = await Testing.SendAsync(new CreateBoardCommand("old title"));
        await Testing.SendAsync(new UpdateBoardCommand(boardId, "new title"));

        var board = await Testing.FindAsync<Board>(boardId);

        board.Should().NotBeNull();
        board!.Title.Should().Be("new title");
    }
}