using System;

namespace tictactoe
{
    public class PlayerHuman : PlayerBase
    {
        public PlayerHuman(char symbol) : base(symbol)
        {
        }

        public PlayerHuman(char symbol, string name) : base(symbol)
        {
            if (name != "")
            {
                this.name = name;
            }            
        }        

        public override bool IsHuman()
        {
            return true;
        }
    }
}
