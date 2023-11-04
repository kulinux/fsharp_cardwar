namespace CardGameWard.Tests;

using Microsoft.FSharp.Linq.RuntimeHelpers;
using static CardWard;

[TestClass]
public class CardWardShould
{
    [TestMethod]
    public void DealtEquallyInEachPlayer()
    {
        Game game = initGame(20);
        Assert.AreEqual(game.player1.numberOfCards, game.player2.numberOfCards);
    }

    [TestMethod]
    public void DealtEquallyInEachPlayerWithHalfOfTheCards()
    {
        Game game = initGame(20);
        Assert.AreEqual(game.player1.numberOfCards, 10);
    }

    [TestMethod]
    public void DealtEquallyInEachPlayerWithoutDealtTheSameCard()
    {
        Game game = initGame(20);
        var allCards = game.player1.cards.Concat(game.player2.cards);

        var anyDuplicate = allCards.GroupBy(x => x).Any(g => g.Count() > 1);
        
        Assert.IsFalse(anyDuplicate);
    }

}