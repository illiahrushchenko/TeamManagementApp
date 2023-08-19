using Application.Boards.Commands.CreateBoard;
using Application.Boards.Queries.GetBoardDetails;
using Application.TableCards.Commands.CreateTableCard;
using Application.Tables.Commands.CreateTable;
using FluentAssertions;

namespace Application.IntegrationTests.Boards.Queries;

public class GetBoardDetailsQueryTests : BaseTestFixture
{
    [Test]
    public async Task ShouldGetBoardDetails()
    {
        await Testing.RunAsDefaultUserAsync();

        var boardId = await Testing.SendAsync(new CreateBoardCommand("New Board"));

        var aTableId = await Testing.SendAsync(new CreateTableCommand("A Table", boardId));
        await Testing.SendAsync(new CreateTableCommand("B Table", boardId));
        await Testing.SendAsync(new CreateTableCardCommand("New Card", "Card Description", aTableId));

        var boardDetails = await Testing.SendAsync(new GetBoardDetailsQuery(boardId));


        boardDetails.Should().NotBeNull();
        boardDetails.Should().Be("New Board");
        boardDetails.Tables.Should().NotBeNullOrEmpty();
        boardDetails.Tables.Should().HaveCount(2);
        boardDetails.Tables.Should()
            .Satisfy(x => x.TableCards.Count() == 1);
    }
}