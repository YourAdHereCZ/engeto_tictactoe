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

        private bool?[,] GetBoardAfterMove((int, int) move, bool player, bool?[,] board)
        {
            bool?[,] result = new bool?[3, 3];

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == null)
                    {
                        result[i, j] = null;
                    }
                    else if (board[i, j] == true)
                    {
                        result[i, j] = true;
                    }
                    else
                    {
                        result[i, j] = false;
                    }
                }
            }
            result[move.Item1, move.Item2] = player;
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">The state to evaluate.</param>
        /// <param name="player">Stores the maximizing player.</param>
        /// <param name="depth">The maximum depth to search to.</param>
        /// <param name="isMaximizing">Stores whether it is the maximizing player's turn.</param>
        /// <param name="bestMove">The best move to be returned from the initial call.</param>
        /// <param name="isInitialCall">Stores whether the call is root or recursive.</param>
        /// <returns>The game value of the state - 1 if won, 0 if drawn, -1 if lost.</returns>
        /// <remarks>
        /// Depth is the maximum depth the minimax searches to and can essentially be thought of as the difficulty of the AI
        /// 3 - 6 may be nice values for different difficulties,
        /// 9+ should always be able to search the whole tree, even when the board is empty.
        /// The main issue with the minimax "AI" is that it assumes that the opponent is playing perfectly as well,
        /// so for instance it won't prefer paths where it could still theoretically win (if the opponent makes a mistake)
        /// over ones where it definitely can't - because it "expects" that the opponent won't allow the win to happen.
        /// </remarks>

        private int Minimax(GameState state, bool player, int depth, bool isMaximizing, out (int, int) bestMove, bool isInitialCall = false)
        {
            bestMove = (-1, -1);

            // evaluate the board and return a value if game over
            if (state.IsWon)
            {   
                if (state.IsWonByPlayerOne)
                {
                    return (player ? 1 : -1);
                }
                else
                {
                    return (player ? -1 : 1);
                }
            }
            else if (state.IsDraw || depth == 0)
            {
                return 0;
            }

            // for each legal move: create a new state from the current state and this move, 
            //                      run recursively on the resulting state,
            //                      add to list of scores
            List<(int, int)> allMoves = state.GetAllLegalMoves();
            List<int> scores = new List<int>();
            foreach ((int, int) move in allMoves)
            {
                var newBoard = GetBoardAfterMove(move, state.IsFirstPlayersTurn, state.Board);
                scores.Add(Minimax((new GameState(!state.IsFirstPlayersTurn, newBoard)), player, depth - 1, !isMaximizing, out _));
            }

            // now make a list of indices of scores that are best/worst
            // and use it to pick a score (and a move) at random
            // but do this only if the method is being called directly from GetNextMove
            // (i.e. we're not in a recursive call) to hopefully save some overhead;
            // this could also be solved by wrapping the initial calls in a foreach(legalmoves) outside of the method
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

        public override (int, int) GetNextMove(GameState state)
        {
            // TODO: if first move of game and difficulty above certain threshold, start sensibly (corner or middle)

            int _ = Minimax(state, state.IsFirstPlayersTurn, Difficulty, true, out (int, int) bestMove, true);
            return bestMove;
        }
    }
}
