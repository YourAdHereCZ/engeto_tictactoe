using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsMinimax
    {
        private const int iterations = 200;


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
                Assert.That(TestsMain.SimulateGame(minimax1, minimax2), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(minimax2, minimax1), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
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
                Assert.That(TestsMain.SimulateGame(minimax, random), Is.Not.EqualTo(-1));
            }

            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(random, minimax), Is.Not.EqualTo(1));
            }
        }


        [Test]
        // in a situation where a win can be forced, the AI must always force it
        public void PlayerAIMinimax9_WinnableState_AlwaysForcesWin()
        {
            // Arrange
            Player minimax1 = new PlayerAIMinimax('X', "Minimax 1", 9);
            Player minimax2 = new PlayerAIMinimax('O', "Minimax 2", 9);

            // Act & Assert
            bool?[,] board;
            for (int i = 0; i < iterations; i++)
            {
                board = new bool?[,] { { true, null, null }, { null, null, null }, { null, false, null } };
                Assert.That(TestsMain.SimulateGameFromState(minimax1, minimax2, true, board), Is.Not.EqualTo(-1));
            }
        }

        // more test ideas:
        // higher difficulty minimax should always win more than lose against lower difficulty?
        // create situations that are 100% winnable, test whether minimax always wins them?
        // 
        #endregion
    }
}