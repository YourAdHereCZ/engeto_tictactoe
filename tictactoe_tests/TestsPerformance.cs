using NUnit.Framework;
using tictactoe;

namespace tictactoe_tests
{
    [TestFixture]
    class TestsPerformance
    {
        [Test]
        public void IsWon_MillionCalls_PerformanceTest()
        {
            // Arrange
            Player random1 = new PlayerAIRandom('X', "Random 1", false);
            Player random2 = new PlayerAIRandom('O', "Random 2", false);
            Game game = new Game(random1, random2, true);
            game.PlayNextTurn();
            game.PlayNextTurn();
            game.PlayNextTurn();
            var board = game.GameBoard;
            bool _;

            // Act
            for (int i = 0; i < 1000000; i++)
            {
                _ = Utils.IsWon(board, 'X');
            }

            // Assert
            Assert.That(true);
            // we're just interested in how long it takes to run this
        }
    }
}