using System;

namespace tictactoe
{
    public class PlayerHuman : Player
    {
        public PlayerHuman(char symbol, string name) : base(symbol, name, true)
        {
        }

        public override (int, int) GetNextMove(GameState state)
        {
            (int, int) nextMove;
            bool isLegal;
            do
            {
                nextMove = ReadHumanMoveFromInput();
                isLegal = state.IsLegalMove(nextMove);

                if (!isLegal)
                {
                    Console.WriteLine("Illegal move.");
                }
            }
            while (!isLegal);

            return nextMove;
        }

        private (int, int) ReadHumanMoveFromInput()
        {
            static int ReadMove()
            {
                int result;
                bool tryParse;
                do
                {
                    tryParse = int.TryParse(Console.ReadLine(), out result);
                    if (!tryParse)
                    {
                        Console.WriteLine("You need to specify an integer. ");
                    }
                }
                while (!tryParse);
                return result;
            }

            Console.Write("Row: ");
            int row = ReadMove();
            Console.Write("Column: ");
            int col = ReadMove();
            return (row - 1 , col - 1);
            // moves are expected from the player as 1-indexed but stored as 0-indexed;
            // this should be the only place where we need to do any "off-by-one" magic
        }
    }
}
