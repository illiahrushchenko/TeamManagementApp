using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.TableCards.Commands.CreateTableCard;
using Application.TableCards.Queries.GetTableCard;
using Application.Tables.Commands.CreateTable;
using FluentAssertions;

namespace Application.IntegrationTests.TableCards.Queries;

public class GetTableCardQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnTableCardDto()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));

        var createCardCommand = new CreateTableCardCommand("New Card", "Card description", tableId);
        var tableCardId = await Testing.SendAsync(createCardCommand);

        var tableCardDto = await Testing.SendAsync(new GetTableCardQuery(tableCardId));

        tableCardDto.Should().NotBeNull();
        tableCardDto.Title.Should().Be(createCardCommand.Title);
        tableCardDto.Description.Should().Be(createCardCommand.Description);
    }

    [Test]
    public async Task ShouldRequireValidId()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var command = new GetTableCardQuery(99);
        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
}