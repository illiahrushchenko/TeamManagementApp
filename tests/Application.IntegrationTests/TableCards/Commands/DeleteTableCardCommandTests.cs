using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.TableCards.Commands.CreateTableCard;
using Application.TableCards.Commands.DeleteTableCard;
using Application.Tables.Commands.CreateTable;
using Application.Tables.Commands.DeleteTable;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.TableCards.Commands;

public class DeleteTableCardCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidId()
    {
        await Testing.RunAsDefaultUserAsync();

        var command = new DeleteTableCardCommand(99);

        await FluentActions.Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
    
    [Test]
    public async Task ShouldDeleteTableCard()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));
        var tableCardId = await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card description", tableId));

        await Testing.SendAsync(new DeleteTableCardCommand(tableCardId));

        var tableCard = await Testing.FindAsync<TableCard>(tableCardId);

        tableCard.Should().BeNull();
    }
}