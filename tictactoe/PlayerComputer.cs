using System;
using System.Collections.Generic;

namespace tictactoe
{
    public class PlayerComputer : PlayerBase
    {
        static Random rnd = new Random();

        public PlayerComputer(char symbol) : base(symbol)
        {
        }

        public PlayerComputer(char symbol, string name) : base(symbol)
        {
            if (name != "")
            {
                this.name = name;
            }
        }

        public override bool IsHuman()
        {
            return false;
        }
    }
}
