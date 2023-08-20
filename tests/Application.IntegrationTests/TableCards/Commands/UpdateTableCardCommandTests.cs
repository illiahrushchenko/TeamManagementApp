using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.TableCards.Commands.CreateTableCard;
using Application.TableCards.Commands.UpdateTableCard;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.UpdateTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.TableCards.Commands;

public class UpdateTableCardCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldUpdateTableCard()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("Board"));
        
        var firstTableId = await Testing.SendAsync(new CreateTableCommand("First Table", boardId));
        var secondTableId = await Testing.SendAsync(new CreateTableCommand("Second Table", boardId));

        var cardId = await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card description", firstTableId));

        await Testing.SendAsync(new UpdateTableCardCommand(cardId, "New Title", "New description", secondTableId));

        var card = await Testing.FindAsync<TableCard>(cardId);

        card.Should().NotBeNull();
        card!.Title.Should().Be("New Title");
        card.Description.Should().Be("New description");
        card.TableId.Should().Be(secondTableId);
    }
    
    [Test]
    public async Task ShouldRequireValidTableId()
    {
        await Testing.RunAsDefaultUserAsync();
        
        var command = new UpdateTableCardCommand(99, "New Title", "New description", 99);
        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }
}