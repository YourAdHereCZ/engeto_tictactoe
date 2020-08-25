using System;
using tictactoe;

namespace tictactoe_tests
{
    // this dummy always returns a random corner move
    // it doesn't check for validity as it is only meant to be used for the first move of a test game
    public class PlayerDummyCorner : Player
    {
        private static readonly Random rand = new Random();

        public PlayerDummyCorner() : base('C', "Corner-playing dummy", false)
        {
        }

        public override (int, int) GetNextMove(GameState _)
        {   
            (int, int)[] cornerMoves = {(0,0),(0,2),(2,0),(2,2)};
            int randomCorner = rand.Next(4);
            return cornerMoves[randomCorner];
        }
    }
}
