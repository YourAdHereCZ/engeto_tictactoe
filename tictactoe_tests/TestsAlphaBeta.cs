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
        private int SimulateGame(Player player1, Player player2)
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

        #region AlphaBeta tests
        [Test]
        // AlphaBeta should be no different from Minimax in decision making, only faster, so they should always draw against each other
        public void PlayerAIMinimax9_vsPlayerAIRandom_NeverLoses()
        {
            // Arrange
            Player minimax = new PlayerAIMinimax('X', "Minimax", 9);
            Player random = new PlayerAIAlphaBeta('O', "AlphaBeta", 9);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(minimax, random), Is.Not.EqualTo(-1));
            }

            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(random, minimax), Is.Not.EqualTo(1));
            }
        }
        #endregion

    }
}