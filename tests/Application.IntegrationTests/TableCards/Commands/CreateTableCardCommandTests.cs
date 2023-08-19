using Application.Boards.Commands.AddMember;
using Application.Boards.Commands.CreateBoard;
using Application.Common.Exceptions;
using Application.TableCards.Commands.CreateTableCard;
using Application.Tables.Commands.CreateTable;
using Application.Users.Commands.CreateUser;
using Domain.Entities;
using FluentAssertions;

namespace Application.IntegrationTests.TableCards.Commands;

public class CreateTableCardCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateTableCard()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));
        var tableCardId = await Testing.SendAsync(
            new CreateTableCardCommand("New Card", "Card description", tableId));

        var tableCard = await Testing.FindAsync<TableCard>(tableCardId);

        tableCard.Should().NotBeNull();
        tableCard!.Title.Should().Be("New Card");
        tableCard.Description.Should().Be("Card description");
    }
    
    [Test]
    public async Task ShouldThrowForbiddenWhenUserIsNotBoardOwnerOrMember()
    {
        await Testing.RunAsUserAsync("boardOwner@gmail.com", "1234");

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));

        await Testing.RunAsUserAsync("otherUser@gmail.com", "1234");
        var command = new CreateTableCardCommand("New Card","Card description", tableId);

        await FluentActions.Invoking(() => 
            Testing.SendAsync(command)).Should().ThrowAsync<ForbiddenAccessException>();
    }
    
    [Test]
    public async Task ShouldCreateTableCardWhenUserIsMember()
    {
        await Testing.RunAsUserAsync("boardOwner@gmail.com", "1234");
        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));
        var tableId = await Testing.SendAsync(new CreateTableCommand("New Table", boardId));
        var memberUserId = await Testing.SendAsync(new CreateUserCommand("boardMember@gmail.com", "1234"));
        await Testing.SendAsync(new AddMemberCommand(boardId, "boardMember@gmail.com"));

        Testing.UserId = memberUserId;
        var tableCardId = await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card description", tableId));

        var tableCard = await Testing.FindAsync<TableCard>(tableCardId);

        tableCard.Should().NotBeNull();
        tableCard!.Title.Should().Be("New Card");
        tableCard.Description.Should().Be("Card description");
    }

    [Test]
    public async Task ShouldRequireValidTableId()
    {
        await Testing.RunAsDefaultUserAsync();

        var command = new CreateTableCardCommand("New Card", "Card description", 99);

        await FluentActions.Invoking(() => Testing.SendAsync(command))
            .Should().ThrowAsync<NotFoundException>();
    }
}