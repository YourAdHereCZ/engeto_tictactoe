using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsAlphaBeta
    {
        private const int iterations = 30;

        // quietly play a whole game between two AIs,
        // return 0 if the game was a draw, 1 if player 1 won and -1 if player 2 won
        private int QuietGame(Player player1, Player player2)
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
            if (quietGame.GetCurrentPlayer() == player1)
            {
                return 1;
            }
            return -1;
        }

    }
}