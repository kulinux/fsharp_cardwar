namespace CardGameWard

module InitGame =
    open System

    let rng = new Random()

    let private shuffle (org: _[]) =
        let arr = Array.copy org
        let max = (arr.Length - 1)

        let randomSwap (arr: _[]) i =
            let pos = rng.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr

        [| 0..max |] |> Array.fold randomSwap arr

    let private dealCards (allCards: Card array) : Card array * Card array =
        let cardsForEachPlayer = allCards.Length / 2
        let firstList = allCards |> Array.take cardsForEachPlayer
        let secondList = allCards |> Array.skip cardsForEachPlayer
        (firstList, secondList)

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


    let initGame (totalCards: int) : Game =
        let allCards = cards totalCards
        let (firstList, secondList) = dealCards allCards

        let res: Game =
            { player1 = { cards = firstList }
              player2 = { cards = secondList } }

        res
