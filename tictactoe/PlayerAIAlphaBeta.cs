using System;

namespace tictactoe
{
    public class PlayerAIAlphaBeta : Player
    {
        public PlayerAIAlphaBeta(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        public override (int, int) GetNextMove(GameState state)
        {
            throw new NotImplementedException();
        }
    }
}
