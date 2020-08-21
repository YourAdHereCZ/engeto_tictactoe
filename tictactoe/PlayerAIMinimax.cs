using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class PlayerAIMinimax : Player
    {
        private static Random random = new Random();

        public PlayerAIMinimax(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        private void PlayMove((int, int) move, char player, char[,] board)
        {
            if (!Utils.IsLegalMove(move, board))
            {
                throw new ArgumentException();
            }
            board[move.Item1, move.Item2] = player;
        }

        public void UndoMove((int, int) move, char[,] board)
        {
            board[move.Item1, move.Item2] = Game.EmptySymbol;
        }

        private int Minimax(char[,] board, char player, char opponent, bool isMaximizing, out (int, int) bestMove)
        {
            bestMove = (-1, -1);
            if (Utils.IsWon(board, player))
            {
                return 1;
            }
            if (Utils.IsWon(board, opponent))
            {
                return -1;
            }
            if (Utils.IsFull(board))
            {
                return 0;
            }

            List<(int, int)> allMoves = Utils.GetAllLegalMoves(board);
            List<int> scores = new List<int>();
            int score;

            foreach ((int, int) move in allMoves)
            {
                PlayMove(move, (isMaximizing ? player : opponent), board);
                score = Minimax(board, player, opponent, !isMaximizing, out (int, int) newBestMove);
                scores.Add(score);
                UndoMove(move, board);
            }

            List<int> indices = new List<int>();

            int max = scores.Max();
            int min = scores.Min();
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i] == (isMaximizing ? max : min))
                {
                    indices.Add(i);
                }
            }
            var randomBestIndex = indices[random.Next(indices.Count)];
            bestMove = allMoves[randomBestIndex];
            return scores[randomBestIndex];
        }

        public override (int, int) GetNextMove(char[,] board, char player, char opponent)
        {
            int _ = Minimax(board, player, opponent, true, out (int, int) bestMove);
            return bestMove;
        }
    }
}
