using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsAlphaBeta
    {
        private const int iterations = 2000;        

        #region AlphaBeta tests
        [Test]
        // just like minimax, minimax with alphabeta should never lose to a random AI
        public void PlayerAIAlphaBeta9_vsPlayerAIRandom_NeverLoses()
        {
            // Arrange
            Player alphabeta = new PlayerAIAlphaBeta('X', "AlphaBeta", 9);
            Player random = new PlayerAIRandom('O', "Random");

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(alphabeta, random), Is.Not.EqualTo(-1));
            }

            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(random, alphabeta), Is.Not.EqualTo(1));
            }
        }

        [Test]
        // minimax with AlphaBeta with sufficient depth will always draw against itself
        public void PlayerAIAlphaBeta9_vsPlayerAIAlphaBeta9_AlwaysATie()
        {
            // Arrange
            Player alphabeta1 = new PlayerAIAlphaBeta('X', "AlphaBeta1", 9);
            Player alphabeta2 = new PlayerAIAlphaBeta('O', "AlphaBeta2", 9);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(alphabeta1, alphabeta2), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(alphabeta2, alphabeta1), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
        }

        [Test]
        // AlphaBeta should be no different from Minimax regarding the quality of decision making,
        //  only faster, so they should always draw against each other
        public void PlayerAIAlphaBeta9_vsPlayerAIMinimax9_AlwaysATie()
        {
            // Arrange
            Player minimax = new PlayerAIMinimax('X', "Minimax", 9);
            Player alphabeta = new PlayerAIAlphaBeta('O', "AlphaBeta", 9);

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(minimax, alphabeta), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(TestsMain.SimulateGame(alphabeta, minimax), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
        }

        [Test]
        // in a situation where a win can be forced, the AI must always force it
        public void PlayerAIAlphaBeta9_WinnableState_AlwaysForcesWin()
        {
            // Arrange
            Player alphabeta1 = new PlayerAIAlphaBeta('X', "AlphaBeta 1", 9);
            Player alphabeta2 = new PlayerAIAlphaBeta('O', "AlphaBeta 2", 9);
            
            // Act & Assert
            bool?[,] board;
            for (int i = 0; i < iterations; i++)
            {
                board = new bool?[,] { { true, null, null }, { null, null, null }, { null, false, null } };
                Assert.That(TestsMain.SimulateGameFromState(alphabeta1, alphabeta2, true, board), Is.Not.EqualTo(-1));
            }
        }
        #endregion
    }
}