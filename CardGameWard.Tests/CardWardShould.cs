namespace CardGameWard.Tests;

using CardGameWard;
using Microsoft.FSharp.Collections;



[TestClass]
public class CardWardShould
{

    [TestMethod]
    public void DealtEquallyInEachPlayer()
    {
        Game game = InitGame.initGame(20);
        Assert.AreEqual(game.player1.Length, game.player2.Length);
    }

    [TestMethod]
    public void DealtEquallyInEachPlayerWithHalfOfTheCards()
    {
        Game game = InitGame.initGame(20);
        Assert.AreEqual(game.player1.Length, 10);
    }

    [TestMethod]
    public void DealtWithoutRepeatingCard()
    {
        Game game = InitGame.initGame(20);
        var allCards = game.player1.Concat(game.player2);

        var anyDuplicate = allCards.GroupBy(x => x).Any(g => g.Count() > 1);

        Assert.IsFalse(anyDuplicate);
    }

    [TestMethod]
    public void DealtRandomnly()
    {
        Game game1 = InitGame.initGame(20);
        Game game2 = InitGame.initGame(20);

        Assert.AreNotEqual(game1.player1.First(), game2.player1.First());
    }

    [TestMethod]
    public void PlayOneTurnNoEqual()
    {
        var game = new Game(
            ListModule.OfArray(new Card[] { new Card(CardNumber.Two, Suit.Club) }),
            ListModule.OfArray(new Card[] { new Card(CardNumber.Three, Suit.Heart) })
        );

        var res = PlayGame.playOneTurn(game);

        var expected = new Game(
            ListModule.OfArray(new Card[] { }),
            ListModule.OfArray(new Card[] { new Card(CardNumber.Three, Suit.Heart), new Card(CardNumber.Two, Suit.Club) })
        );

        Assert.AreEqual(expected, res);
    }

    [TestMethod]
    public void AcesAreHigh()
    {
        var game = new Game(
            ListModule.OfArray(new Card[] { new Card(CardNumber.King, Suit.Club) }),
            ListModule.OfArray(new Card[] { new Card(CardNumber.Ace, Suit.Heart) })
        );

        var res = PlayGame.playOneTurn(game);

        var expected = new Game(
            ListModule.OfArray(new Card[] { }),
            ListModule.OfArray(new Card[] { new Card(CardNumber.Ace, Suit.Heart), new Card(CardNumber.King, Suit.Club) })
        );

        Assert.AreEqual(expected, res);
    }

    public FSharpList<Card> CardOf(params CardNumber[] numbers)
    {
        var res = numbers.Select(number => new Card(number, Suit.Spade)).ToList<Card>();
        return ListModule.OfSeq(res);
    }

    [TestMethod]
    public void War()
    {
        var game = new Game(
            CardOf(CardNumber.Two, CardNumber.King, CardNumber.King, CardNumber.King, CardNumber.Three),
            CardOf(CardNumber.Two, CardNumber.King, CardNumber.King, CardNumber.King, CardNumber.Four)
        );

        var res = PlayGame.playOneTurn(game);

        var expected = new Game(
            CardOf(),
                CardOf(
                    CardNumber.Two, CardNumber.King, CardNumber.King, CardNumber.King, CardNumber.Three,
                    CardNumber.Two, CardNumber.King, CardNumber.King, CardNumber.King, CardNumber.Four
                )
        );

        Assert.AreEqual(expected, res);
    }


}