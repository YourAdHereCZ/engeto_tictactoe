using System;

namespace tictactoe
{
    public class PlayerAIRandom : Player
    {
        static readonly Random rnd = new Random();

        public PlayerAIRandom(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        public override (int, int) GetNextMove(char[,] gameBoard, char _, char __)
        {
            return PickRandomLegalMove(gameBoard);
        }

        public (int, int) PickRandomLegalMove(char[,] gameBoard)
        {
            var moves = Utils.GetAllLegalMoves(gameBoard);
            int rand = rnd.Next(moves.Count);
            (int, int) randomLegalMove = moves[rand];
            return randomLegalMove;
        }
    }
}
