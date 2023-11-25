namespace CardGameWard

module InitGame =
    open System

    let private shuffle (org: _ list) =
        let rng = new Random()
        let arr = org |> List.toArray
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
        let chunks = allCards |> List.chunkBySize cardsForEachPlayer
        (chunks.Head, chunks.Tail.Head)

    let private cards (number: int) : Card list =
        let suits = (Enum.GetValues(typeof<Suit>) :?> (Suit[])) |> Array.toList

        let cardNumbers =
            (Enum.GetValues(typeof<CardNumber>) :?> (CardNumber[])) |> Array.toList

        List.allPairs suits cardNumbers
        |> List.take number
        |> shuffle
        |> List.map (fun (suit, number) -> { number = number; suit = suit })



    let initGame (totalCards: int) : Game =
        let allCards = cards totalCards
        let (firstList, secondList) = dealCards allCards

        { player1 = firstList
          player2 = secondList }
