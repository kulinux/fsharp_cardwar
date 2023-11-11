namespace CardGameWard

module PlayGame =
    let playOneTurn (game: Game) : Game =
        match (game.player1.cards |> Array.toList, game.player2.cards |> Array.toList) with
        | (player1Head :: player1Tail), (player2Head :: player2Tail) when player1Head.number > player2Head.number ->
            { player1 = { cards = [ player1Head ] @ player1Tail @ [ player2Head ] |> List.toArray }
              player2 = { cards = player2Tail |> List.toArray } }
        | (player1Head :: player1Tail), (player2Head :: player2Tail) ->
            { player1 = { cards = player1Tail |> List.toArray }
              player2 = { cards = [ player2Head ] @ player2Tail @ [ player1Head ] |> List.toArray } }
        | (_), (_) -> game
