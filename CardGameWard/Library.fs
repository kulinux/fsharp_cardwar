namespace CardGameWard



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


