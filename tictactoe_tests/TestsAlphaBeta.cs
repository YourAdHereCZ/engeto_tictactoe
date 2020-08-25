using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsAlphaBeta
    {
        private const int iterations = 20;

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
            if (quietGame.OtherPlayer == player1)
            {
                return 1;
            }
            return -1;
        }

        #region AlphaBeta tests
        [Test]
        // just like minimax, minimax with alphabeta should never lose to a random AI
        public void PlayerAIAlphaBeta9_vsPlayerAIRandom_NeverLoses()
        {
            // Arrange
            Player alphabeta = new PlayerAIAlphaBeta('X', "AlphaBeta", 10);
            Player random = new PlayerAIRandom('O', "Random");

            // Act & Assert
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(alphabeta, random), Is.Not.EqualTo(-1));
            }

            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(random, alphabeta), Is.Not.EqualTo(1));
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
                Assert.That(SimulateGame(alphabeta1, alphabeta2), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(alphabeta2, alphabeta1), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
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
                Assert.That(SimulateGame(minimax, alphabeta), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
            for (int i = 0; i < iterations; i++)
            {
                Assert.That(SimulateGame(alphabeta, minimax), Is.Not.EqualTo(-1).And.Not.EqualTo(1));
            }
        }
        #endregion

    }
}