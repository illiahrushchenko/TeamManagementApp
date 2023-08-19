using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.DeleteBoard;
using Application.Common.Exceptions;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Boards.Commands;

public class DeleteBoardCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        await Testing.RunAsDefaultUserAsync();

        var command = new DeleteBoardCommand(452);

        await FluentActions.Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldDeleteBoard()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));

        await Testing.SendAsync(new DeleteBoardCommand(boardId));

        var table = await Testing.FindAsync<Board>(boardId);

        table.Should().BeNull();
    }
}