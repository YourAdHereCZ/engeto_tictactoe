using System;

namespace tictactoe
{
    public class PlayerHuman : Player
    {
        public PlayerHuman(char symbol, string name) : base(symbol, name, true)
        {
        }

        public override (int, int) GetNextMove(char[,] gameBoard, char _, char __)
        {
            (int, int) nextMove;
            bool isLegal;
            do
            {
                nextMove = ReadHumanMoveFromInput();
                isLegal = Utils.IsLegalMove(nextMove, gameBoard);

                if (!isLegal)
                {
                    Console.WriteLine("Illegal move.");
                }
            }
            while (!Utils.IsLegalMove(nextMove, gameBoard));

            return nextMove;
        }

        private (int, int) ReadHumanMoveFromInput()
        {
            bool tryParse;
            int row;
            do
            {
                Console.Write("Row: ");
                tryParse = int.TryParse(Console.ReadLine(), out row);
                if (!tryParse)
                {
                    Console.WriteLine("You need to specify an integer. ");
                }
            }
            while (!tryParse);

            int col;
            do
            {
                Console.Write("Column: ");
                tryParse = int.TryParse(Console.ReadLine(), out col);
                if (!tryParse)
                {
                    Console.Write("You need to specify an integer. ");
                }
            }
            while (!tryParse);

            return (row - 1 , col - 1);
            // moves are expected from the player as 1-indexed but stored as 0-indexed
            // so this is the only place we need to do any "off by one" magic
        }
    }
}
