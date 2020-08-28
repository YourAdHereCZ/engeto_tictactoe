using tictactoe;

namespace tictactoe_tests
{
    public class TestsMain
    {
        // quietly play a whole game between two AIs,
        // return 0 if the game was a draw, 1 if player 1 won and -1 if player 2 won
        internal static int SimulateGameFromState(Player player1, Player player2, bool isFirstPlayersTurn, bool?[,] board)
        {
            Game quietGame = new Game(player1, player2, isFirstPlayersTurn, board);

            while (!quietGame.State.IsFinal)
            {
                quietGame.PlayNextTurn();
            }

            if (quietGame.State.IsDraw)
            {
                return 0;
            }
            if (quietGame.OtherPlayer == player1)
            {
                return 1;
            }
            return -1;
        }

        // quietly play a whole game between two AIs,
        // return 0 if the game was a draw, 1 if player 1 won and -1 if player 2 won
        internal static int SimulateGame(Player player1, Player player2)
        {
            Game quietGame = new Game(player1, player2, true);

            while (!quietGame.State.IsFinal)
            {
                quietGame.PlayNextTurn();
            }

            if (quietGame.State.IsDraw)
            {
                return 0;
            }
            if (quietGame.OtherPlayer == player1)
            {
                return 1;
            }
            return -1;
        }
    }
}
