using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.UpdateBoard;
using Application.Common.Exceptions;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.UpdateTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.Tables.Commands;

public class UpdateTableCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateBoard()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("Table", boardId));
        
        await Testing.SendAsync(new UpdateTableCommand(tableId, "New Title"));

        var table = await Testing.FindAsync<Table>(tableId);

        table.Should().NotBeNull();
        table!.Title.Should().Be("New Title");
    }
    
    [Test]
    public async Task ShouldRequireValidTableId()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var command = new UpdateTableCommand(99, "New Title");
        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
}