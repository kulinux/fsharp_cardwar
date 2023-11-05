namespace CardGameWard

open System
open System.Linq

module CardWard =

    type Suit =
        | Club = 1
        | Spade = 2
        | Heart = 3
        | Diamon = 4

    type CardNumber =
        | Two = 2
        | Three = 3
        | Four = 4
        | Five = 5
        | Six = 6
        | Seven = 7
        | Eight = 8
        | Nine = 9
        | Ten = 10
        | Jack = 11
        | Queen = 12
        | King = 13
        | Ace = 14

    type Card = { number: CardNumber; suit: Suit }

    type Player =
        { cards: Card array }

        member this.numberOfCards = this.cards.Length

    type Game = { player1: Player; player2: Player }

    let rng = new Random()
    let shuffle (org:_[]) = 
        let arr = Array.copy org
        let max = (arr.Length - 1)
        let randomSwap (arr:_[]) i =
            let pos = rng.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr
   
        [|0..max|] |> Array.fold randomSwap arr


    let private cards (number: int) : Card array =
        let suits = (Enum.GetValues(typeof<Suit>) :?> (Suit[]))
        let cardNumbers = (Enum.GetValues(typeof<CardNumber>) :?> (CardNumber[]))

        seq {
            for suit in suits do
                for cardNumber in cardNumbers do
                    yield { number = cardNumber; suit = suit }
        }
        |> Seq.take number
        |> Seq.toArray
        |> shuffle

    let private dealCards (allCards: Card array) : Card array * Card array =
        let cardsForEachPlayer = allCards.Length / 2
        let firstList = allCards |> Array.take cardsForEachPlayer
        let secondList = allCards |> Array.skip cardsForEachPlayer
        (firstList, secondList)


    let initGame (totalCards: int) : Game =
        let allCards = cards totalCards
        let (firstList, secondList) = dealCards allCards

        let res: Game =
            { player1 = { cards = firstList }
              player2 = { cards = secondList } }

        res
