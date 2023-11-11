namespace CardGameWard.Tests;

using CardGameWard;

[TestClass]
public class CardWardShould
{
    [TestMethod]
    public void DealtEquallyInEachPlayer()
    {
        Game game = InitGame.initGame(20);
        Assert.AreEqual(game.player1.numberOfCards, game.player2.numberOfCards);
    }

    [TestMethod]
    public void DealtEquallyInEachPlayerWithHalfOfTheCards()
    {
        Game game = InitGame.initGame(20);
        Assert.AreEqual(game.player1.numberOfCards, 10);
    }

    [TestMethod]
    public void DealtWithoutRepeatingCard()
    {
        Game game = InitGame.initGame(20);
        var allCards = game.player1.cards.Concat(game.player2.cards);

        var anyDuplicate = allCards.GroupBy(x => x).Any(g => g.Count() > 1);

        Assert.IsFalse(anyDuplicate);
    }

    [TestMethod]
    public void DealtRandomnly()
    {
        Game game1 = InitGame.initGame(20);
        Game game2 = InitGame.initGame(20);

        Assert.AreNotEqual(game1.player1.cards.First(), game2.player1.cards.First());
    }

    [TestMethod]
    public void PlayOneTurnNoEqual()
    {
        var game = new Game(
            new Player(new Card[] { new Card(CardNumber.Two, Suit.Club) }),
            new Player(new Card[] { new Card(CardNumber.Three, Suit.Heart) })
        );

        var res = PlayGame.playOneTurn(game);

        var expected = new Game(
            new Player(new Card[] { }),
            new Player(new Card[] { new Card(CardNumber.Three, Suit.Heart), new Card(CardNumber.Two, Suit.Club) })
        );

        Assert.AreEqual(expected, res);

    }

}