using System;

namespace tictactoe
{
    public class PlayerAIRandom : Player
    {
        static readonly Random rnd = new Random();

        public PlayerAIRandom(char symbol, string name) : base(symbol, name, false)
        {
        }

        public override (int, int) GetNextMove(GameState state)
        {
            var moves = state.GetAllLegalMoves();
            int rand = rnd.Next(moves.Count);
            (int, int) randomLegalMove = moves[rand];
            return randomLegalMove;
        }
    }
}
