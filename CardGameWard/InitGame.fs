namespace CardGameWard

module InitGame =
    open System

    let rng = new Random()

    let private shuffle (org: _ list) =
        let arr = org |> List.toArray |> Array.copy 
        let max = (arr.Length - 1)

        let randomSwap (arr: _[]) i =
            let pos = rng.Next(max)
            let tmp = arr.[pos]
            arr.[pos] <- arr.[i]
            arr.[i] <- tmp
            arr

        [| 0..max |] |> Array.fold randomSwap arr |> Array.toList

    let private dealCards (allCards: Card list) : Card list * Card list =
        let cardsForEachPlayer = allCards.Length / 2
        let firstList = allCards |> List.take cardsForEachPlayer
        let secondList = allCards |> List.skip cardsForEachPlayer
        (firstList, secondList)

    let private cards (number: int) : Card list =
        let suits = (Enum.GetValues(typeof<Suit>) :?> (Suit[])) |> Array.toList
        let cardNumbers = (Enum.GetValues(typeof<CardNumber>) :?> (CardNumber[])) |> Array.toList

        [
            for suit in suits do
                for cardNumber in cardNumbers do
                    yield { number = cardNumber; suit = suit }
        ]
        |> List.take number
        |> shuffle


    let initGame (totalCards: int) : Game =
        let allCards = cards totalCards
        let (firstList, secondList) = dealCards allCards

        let res: Game =
            { player1 = { cards = firstList }
              player2 = { cards = secondList } }

        res
