using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.Tables.Commands.CreateTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Tables;

public class CreateTableCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateTable()
    {
        await Testing.RunAsUserAsync("qqq@gmail.com", "1234");

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
        await Testing.RunAsUserAsync("qqq@gmail.com", "1234");

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));

        await Testing.RunAsUserAsync("aaa@gmail.com", "1234");
        var command = new CreateTableCommand("New Table", boardId);

        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();


    }
}