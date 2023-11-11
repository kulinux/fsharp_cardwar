namespace CardGameWard

module PlayGame =
    let playOneTurn (game: Game) : Game =
        match (game.player1.cards, game.player2.cards ) with
        | (player1Head :: player1Tail), (player2Head :: player2Tail) when player1Head.number > player2Head.number ->
            { player1 = { cards = [ player1Head ] @ player1Tail @ [ player2Head ] }
              player2 = { cards = player2Tail } }
        | (player1Head :: player1Tail), (player2Head :: player2Tail) ->
            { player1 = { cards = player1Tail }
              player2 = { cards = [ player2Head ] @ player2Tail @ [ player1Head ] } }
        | (_), (_) -> game
