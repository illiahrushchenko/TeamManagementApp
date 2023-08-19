using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.Tables.Commands.CreateTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Tables.Commands;

public class CreateTableCommmandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateTable()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));

        var table = await Testing.FindAsync<Table>(tableId);

        table.Should().NotBeNull();
        table!.Title.Should().Be("New Table");
        table.BoardId.Should().Be(boardId);
    }

    [Test]
    public async Task ShouldThrowForbiddenWhenUserIsNotBoardOwner()
    {
        await Testing.RunAsUserAsync("boardOwner@gmail.com", "1234");

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));

        await Testing.RunAsUserAsync("otherUser@gmail.com", "1234");
        var command = new CreateTableCommand("New Table", boardId);

        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }

    [Test]
    public async Task ShouldRequireValidBoardId()
    {
        await Testing.RunAsDefaultUserAsync();

        var command = new CreateTableCommand("New Table", 99);

        await FluentActions.Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
}