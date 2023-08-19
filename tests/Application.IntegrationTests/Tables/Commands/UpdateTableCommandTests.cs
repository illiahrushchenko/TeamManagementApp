using Application.Boards.Commands.CreateBoard;
using Application.Boards.Commands.UpdateBoard;
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

        var firstBoardId = await Testing.SendAsync(new CreateBoardCommand("Board"));
        var secondBoardId =await Testing.SendAsync(new CreateBoardCommand("Empty Board"));
        
        var tableId = await Testing.SendAsync(new CreateTableCommand("Old Title", firstBoardId));
        
        await Testing.SendAsync(new UpdateTableCommand(tableId, "New Title", secondBoardId));

        var table = await Testing.FindAsync<Table>(tableId);

        table.Should().NotBeNull();
        table!.BoardId.Should().Be(secondBoardId);
        table.Title.Should().Be("New Title");
    }
}