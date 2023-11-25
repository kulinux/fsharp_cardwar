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
            raise (OuterError($"Player win {n}"))


    let getCards2(a: int)(b: int) (card1: Card list) (card2: Card list): Card list * Card list * Card list * Card list =
        let card1Cards = card1 |> getCards a
        let card2Cards = card2 |> getCards b
        (fst card1Cards, snd card1Cards, fst card2Cards, snd card2Cards)

    let warPack (player: Player): Card list * Card list * Card list * Card list =
         player.cards |> getCards 4 ||> getCards2 1 1 
        

    let war (game: Game) : Game =
        let play1war, play1down, cardToPlay1, rest1 = warPack game.player1
        let play2war, play2down, cardToPlay2, rest2 = warPack game.player2

        match turnResultCard cardToPlay1.Head cardToPlay2.Head with
        | Player1Win ->
            { player1 =
                { cards =
                    play2war
                    @ play2down
                    @ cardToPlay2
                    @ play1war
                    @ play1down
                    @ cardToPlay1
                    @ rest1 }
              player2 = { cards = rest2 } }
        | Player2Win ->
            { player1 = { cards = rest1 }
              player2 =
                { cards =
                    play1war
                    @ play1down
                    @ cardToPlay1
                    @ play2war
                    @ play2down
                    @ cardToPlay2
                    @ rest2 } }
        | _ -> raise (OuterError("Not implemented"))


    let playOneTurn (game: Game) : Game =
        let (player1Head, player1Tail) = game.player1.cards |> getCards 1
        let (player2Head, player2Tail) = game.player2.cards |> getCards 1

        match turnResultCard player1Head.Head player2Head.Head with
        | Player1Win ->
            { player1 = { cards = player1Head @ player1Tail @ player2Head }
              player2 = { cards = player2Tail } }
        | Player2Win ->
            { player1 = { cards = player1Tail }
              player2 = { cards = player2Head @ player2Tail @ player1Head } }
        | Equal -> war game
