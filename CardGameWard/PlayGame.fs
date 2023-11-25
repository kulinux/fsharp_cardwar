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

    let turnResultCard (card1: Card, card2: Card) : TurnResult =
        if card1.number > card2.number then Player1Win
        elif card1.number < card2.number then Player2Win
        else Equal

    let turnResult (game: Game) : TurnOrGameResult =
        match (game.player1.cards, game.player2.cards) with
        | (player1Head :: _), (player2Head :: _) -> TurnResult(turnResultCard (player1Head, player2Head))
        | [], _ -> GameResult(Player2WinMatch)
        | _, [] -> GameResult(Player1WinMatch)

    let war(game: Game): Game =
        let play1war = game.player1.cards |> List.head
        let play2war = game.player2.cards |> List.head
        let play1down = game.player1.cards |> List.take 4 |> List.skip 1
        let play2down = game.player2.cards |> List.take 4 |> List.skip 1
        let cardToPlay1 = game.player1.cards |> List.skip 4 |> List.head
        let cardToPlay2 = game.player2.cards |> List.skip 4 |> List.head
        let rest1 = game.player1.cards |> List.skip 5
        let rest2 = game.player2.cards |> List.skip 5
        match turnResultCard (cardToPlay1, cardToPlay2) with
            | Player1Win ->
                { player1 = {cards = [play2war] @ play2down @ [ cardToPlay2 ] @ [play1war] @ play1down @ [ cardToPlay1 ] @ rest1 }
                  player2 = { cards = rest2 }}
            | Player2Win ->
                { player1 = {cards = rest1}
                  player2 = { cards = [play1war] @ play1down @ [ cardToPlay1 ] @ [play2war] @ play2down @ [ cardToPlay2 ] @ rest2 } }
            | _ -> raise(OuterError("Not implemented")) 


    let playOneTurn (game: Game) : Game =
        let player1Head = game.player1.cards.Head
        let player1Tail = game.player1.cards.Tail

        let player2Head = game.player2.cards.Head
        let player2Tail = game.player2.cards.Tail

        match turnResult game with
        | TurnResult Player1Win ->
            { player1 = { cards = [ player1Head ] @ player1Tail @ [ player2Head ] }
              player2 = { cards = player2Tail } }
        | TurnResult Player2Win ->
            { player1 = { cards = player1Tail }
              player2 = { cards = [ player2Head ] @ player2Tail @ [ player1Head ] } }
        | TurnResult Equal -> war game
        | GameResult _ -> raise(OuterError("Player win"))
