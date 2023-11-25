namespace CardGameWard

module PlayGame =

    exception OuterError of string

    type TurnResult =
        | Player1Win
        | Player2Win
        | Player1WinMatch
        | Player2WinMatch
        | Equal

    let turnResultCard (card1: Card, card2: Card) : TurnResult =
        if card1.number > card2.number then Player1Win
        elif card1.number < card2.number then Player2Win
        else Equal

    let turnResult (game: Game) : TurnResult =
        match (game.player1.cards, game.player2.cards) with
        | (player1Head :: _), (player2Head :: _) -> turnResultCard (player1Head, player2Head)
        | [], _ -> Player2WinMatch
        | _, [] -> Player1WinMatch

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
        let (player1Head :: player1Tail) = game.player1.cards
        let (player2Head :: player2Tail) = game.player2.cards

        match turnResult game with
        | Player1Win ->
            { player1 = { cards = [ player1Head ] @ player1Tail @ [ player2Head ] }
              player2 = { cards = player2Tail } }
        | Player2Win ->
            { player1 = { cards = player1Tail }
              player2 = { cards = [ player2Head ] @ player2Tail @ [ player1Head ] } }
        | Equal -> war game
        | Player1WinMatch | Player2WinMatch -> raise(OuterError("Player win"))
