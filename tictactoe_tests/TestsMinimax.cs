using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsMinimax
    {
        private const int iterations = 30;

        // quietly play a whole game between two AIs,
        // return 0 if the game was a draw, 1 if player 1 won and -1 if player 2 won
        private int QuietGame(Player player1, Player player2)
        {
            Game quietGame = new Game(player1, player2, true);

            while (!quietGame.IsFinal)
            {
                quietGame.PlayNextTurn();
            }

            if (quietGame.IsTie)
            {
                return 0;
            }
            if (quietGame.GetCurrentPlayer() == player1)
            {
                return 1;
            }
            return -1;
        }

        #region Minimax tests
        [Test]
        // If we let a minimaxing AI play against an AI that picks randomly, the minimaxer should never lose.
        public void PlayerAIMinimax_vsPlayerAIRandom_NeverLoses()
        {
            // Arrange
            Player minimax = new PlayerAIMinimax('X', "Minimax", false);
            Player random = new PlayerAIRandom('O', "Random", false);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(QuietGame(minimax, random), Is.Not.EqualTo(-1));
            }

            for (int i = 0; i < iterations; i++)
            {
                Assert.That(QuietGame(random, minimax), Is.Not.EqualTo(1));
            }
        }

        [Test]
        // If we let two minimaxing AIs play against each other, they should always tie.
        public void PlayerAIMinimax_vsPlayerAIMinimax_AlwaysATie()
        {
            // Arrange
            Player minimax1 = new PlayerAIMinimax('X', "Minimax1", false);
            Player minimax2 = new PlayerAIMinimax('O', "Minimax2", false);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(QuietGame(minimax1, minimax2), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(QuietGame(minimax2, minimax1), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
        }
        #endregion
    }
}