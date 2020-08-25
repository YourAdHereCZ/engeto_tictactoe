using System;

namespace tictactoe
{
    public class PlayerAINaive : Player
    {
        public PlayerAINaive(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        public override (int, int) GetNextMove(GameState state)
        {
            throw new NotImplementedException();
        }
    }
}
