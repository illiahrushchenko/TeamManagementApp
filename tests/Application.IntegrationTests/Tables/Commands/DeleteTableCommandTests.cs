using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.DeleteTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Tables.Commands;

public class DeleteTableCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        await Testing.RunAsDefaultUserAsync();

        var command = new DeleteTableCommand(452);

        await FluentActions.Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTable()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));

        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));

        await Testing.SendAsync(new DeleteTableCommand(tableId));

        var table = await Testing.FindAsync<Table>(tableId);

        table.Should().BeNull();
    }
}