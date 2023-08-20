using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.TableCards.Commands.CreateTableCard;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Queries.GetTable;
using FluentAssertions;

namespace Application.IntegrationTests.Tables.Queries;

public class GetTableQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldGetTable()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var createTableCommand = new CreateTableCommand("New Table", boardId);
        var tableId = await Testing.SendAsync(createTableCommand);

        await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card description", tableId));
        await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card description", tableId));

        var tableDetailsDto = await Testing.SendAsync(new GetTableQuery(tableId));

        tableDetailsDto.Should().NotBeNull();
        tableDetailsDto.TableCards.Should().HaveCount(2);
        tableDetailsDto.Title.Should().Be(createTableCommand.Title);
    }
    
    [Test]
    public async Task ShouldRequireValidId()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var command = new GetTableQuery(99);
        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
}