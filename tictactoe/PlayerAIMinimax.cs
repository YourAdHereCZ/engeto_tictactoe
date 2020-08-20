using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class PlayerAIMinimax : Player
    {
        public PlayerAIMinimax(char symbol, string name, bool isHuman) : base(symbol, name, isHuman)
        {
        }

        private int Minimax(char[,] board, char player, char opponent, bool isMaximizing)
        {
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

            foreach ((int, int) move in allMoves)
            {
                PlayMove(move, (isMaximizing ? player : opponent), ref board);
                scores.Add(Minimax(board, player, opponent, !isMaximizing));
                UndoMove(move, ref board);
            }

            return isMaximizing ? scores.Max() : scores.Min();

        }

        private void PlayMove((int, int) move, char player, ref char[,] board)
        {
            if (!Utils.IsLegalMove(move, board))
            {
                throw new ArgumentException();
            }
            board[move.Item1, move.Item2] = player;
        }

        public void UndoMove((int, int) move, ref char[,] board)
        {
            board[move.Item1, move.Item2] = Game.EmptySymbol;
        }

        public override (int, int) GetNextMove(char[,] board, char player, char opponent)
        {
            int bestScore = int.MinValue;
            (int, int) bestMove = (-1, -1);

            foreach ((int, int) move in Utils.GetAllLegalMoves(board))
            {
                PlayMove(move, player, ref board);
                int score = Minimax(board, player, opponent, false);
                UndoMove(move, ref board);
                if (score > bestScore )
                {
                    bestScore = score;
                    bestMove = move;
                }
            }

            return bestMove;
        }
    }
}
