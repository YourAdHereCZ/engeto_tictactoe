using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsMinimax
    {
        private const int iterations = 1000;

        // quietly simulate a whole game between two AIs,
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
            if (quietGame.OtherPlayer == player1)
            {
                return 1;
            }
            return -1;
        }

        #region Minimax tests
        [Test]
        // If we let two perfect minimaxing AIs play against each other, they should always tie.
        public void PlayerAIMinimax9_vsPlayerAIMinimax9_AlwaysATie()
        {
            // Arrange
            Player minimax1 = new PlayerAIMinimax('X', "Minimax1", 9);
            Player minimax2 = new PlayerAIMinimax('O', "Minimax2", 9);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(minimax1, minimax2), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(minimax2, minimax1), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
        }

        [Test]
        // If we let a perfect minimaxing AI play against an AI that picks randomly, the minimaxer should never lose.
        public void PlayerAIMinimax9_vsPlayerAIRandom_NeverLoses()
        {
            // Arrange
            Player minimax = new PlayerAIMinimax('X', "Minimax", 9);
            Player random = new PlayerAIRandom('O', "Random");

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

        // more test ideas:
        // higher difficulty minimax should always win more than lose against lower difficulty?
        // create situations that are 100% winnable, test whether minimax always wins them?
        // 
        #endregion
    }
}