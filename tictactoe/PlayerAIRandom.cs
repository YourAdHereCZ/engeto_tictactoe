using System;

namespace tictactoe
{
    public class PlayerAIRandom : Player
    {
        static readonly Random rnd = new Random();

        public PlayerAIRandom(char symbol, string name) : base(symbol, name, false)
        {
        }

        public override (int, int) GetNextMove(char[,] gameBoard, char _, char __)
        {
            return PickRandomLegalMove(gameBoard);
        }

        private (int, int) PickRandomLegalMove(char[,] gameBoard)
        {
            var moves = Utils.GetAllLegalMoves(gameBoard);
            int rand = rnd.Next(moves.Count);
            (int, int) randomLegalMove = moves[rand];
            return randomLegalMove;
        }
    }
}
