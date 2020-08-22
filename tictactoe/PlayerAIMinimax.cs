using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class PlayerAIMinimax : Player
    {
        private static readonly Random random = new Random();

        public int Difficulty { get; private set; }

        public PlayerAIMinimax(char symbol, string name, int difficulty) : base(symbol, name, false)
        {
            Difficulty = difficulty;
        }

        private void SimulateMove((int, int) move, char player, char[,] board)
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

        // depth is the maximum depth to search to and can essentially be thought of as the difficulty of the AI
        // 0 should essentially play randomly,
        // 3 - 5 may be nice values for different difficulties,
        // 5 can still be beaten if it makes a mistake early
        // 6+ seems to always tie,
        // 9+ should always be able to search the whole tree.
        private int Minimax(char[,] board, char player, char opponent, int depth, bool isMaximizing, out (int, int) bestMove, bool isInitialCall = false)
        {
            bestMove = (-1, -1);

            // evaluate the board and return a value if game is over
            if (Utils.IsWon(board, player))
            {
                return 1;
            }
            if (Utils.IsWon(board, opponent))
            {
                return -1;
            }
            if (Utils.IsFull(board) || depth == 0)
            {
                return 0;
            }

            // for each legal move: play the move, run recursively on the resulting board, undo the move
            List<(int, int)> allMoves = Utils.GetAllLegalMoves(board);
            List<int> scores = new List<int>();
            foreach ((int, int) move in allMoves)
            {
                SimulateMove(move, (isMaximizing ? player : opponent), board);
                scores.Add(Minimax(board, player, opponent, depth - 1, !isMaximizing, out _));
                UndoMove(move, board);
            }

            // now make a list of indices of scores that are best/worst
            // and use it to pick a score (and a move) at random
            // but do this only if we're being called directly from GetNextMove
            // (i.e. we're not in a nested call) to hopefully save some overhead
            int maxOrMin = isMaximizing ? scores.Max() : scores.Min();
            if (isInitialCall)
            {
                List<int> indices = new List<int>();
                
                for (int i = 0; i < scores.Count; i++)
                {
                    if (scores[i] == maxOrMin)
                    {
                        indices.Add(i);
                    }
                }
                var randomBestIndex = indices[random.Next(indices.Count)];
                bestMove = allMoves[randomBestIndex];
            }
            return maxOrMin;
        }

        public override (int, int) GetNextMove(char[,] board, char player, char opponent)
        {
            int _ = Minimax(board, player, opponent, Difficulty, true, out (int, int) bestMove, true);
            return bestMove;
        }
    }
}
