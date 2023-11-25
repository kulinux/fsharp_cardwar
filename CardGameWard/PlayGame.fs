namespace CardGameWard

module PlayGame =

    exception OuterError of string

    type TurnResult =
        | Player1Win
        | Player2Win
        | Equal

    type GameResult =
        | Player1WinMatch
        | Player2WinMatch

    type TurnOrGameResult =
        | TurnResult of TurnResult
        | GameResult of GameResult

    let turnResultCard (card1: Card) (card2: Card) : TurnResult =
        if card1.number > card2.number then Player1Win
        elif card1.number < card2.number then Player2Win
        else Equal

    let getCards (n: int) (card: Card list) : Card list * Card list =
        if card.Length >= n then
            (card |> List.take n, card |> List.skip n)
        else
            raise (OuterError($"Player win, not enough cards {n}"))


    let war (game: Game) : Game =
        let warPack (player: Player) =
            player
            |> getCards 4
            ||> fun a b ->
                (a |> getCards 1 |> fst, a |> getCards 1 |> snd, b |> getCards 1 |> fst, b |> getCards 1 |> snd)

        let play1war, play1down, cardToPlay1, rest1 = warPack game.player1
        let play2war, play2down, cardToPlay2, rest2 = warPack game.player2

        match turnResultCard cardToPlay1.Head cardToPlay2.Head with
        | Player1Win ->
            { player1 = play2war @ play2down @ cardToPlay2 @ play1war @ play1down @ cardToPlay1 @ rest1
              player2 = rest2 }
        | Player2Win ->
            { player1 = rest1
              player2 = play1war @ play1down @ cardToPlay1 @ play2war @ play2down @ cardToPlay2 @ rest2 }
        | _ -> raise (OuterError("Not implemented"))


    let playOneTurn (game: Game) : Game =
        let (player1Head, player1Tail) = game.player1 |> getCards 1
        let (player2Head, player2Tail) = game.player2 |> getCards 1

        match turnResultCard player1Head.Head player2Head.Head with
        | Player1Win ->
            { player1 = player1Head @ player1Tail @ player2Head
              player2 = player2Tail }
        | Player2Win ->
            { player1 = player1Tail
              player2 = player2Head @ player2Tail @ player1Head }
        | Equal -> war game
