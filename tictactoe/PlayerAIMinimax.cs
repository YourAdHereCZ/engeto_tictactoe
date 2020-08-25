using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class PlayerAIMinimax : Player
    {
        protected static readonly Random random = new Random();

        public int Difficulty { get; protected set; }

        public PlayerAIMinimax(char symbol, string name, int difficulty) : base(symbol, name, false)
        {
            Difficulty = difficulty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="state">The state to evaluate.</param>
        /// <param name="player">The maximizing player.</param>
        /// <param name="depth">The maximum depth to search to.</param>
        /// <param name="isMaximizing">Whether it is the maximizing player's turn.</param>
        /// <param name="bestMove">The best move to be returned from the initial call.</param>
        /// <param name="isInitialCall">Whether the call is root or recursive.</param>
        /// <returns>The game value of the state - 1 if won, 0 if drawn, -1 if lost.</returns>
        /// <remarks>
        /// Depth is the maximum depth the minimax searches to and can essentially be thought of as the difficulty of the AI
        /// 3 - 6 may be nice values for different difficulties,
        /// 9+ should always be able to search the whole tree, even when the board is empty.
        /// The main issue with the minimax "AI" is that it assumes that the opponent is playing perfectly as well,
        /// so for instance it won't prefer paths where it could still theoretically win (if the opponent makes a mistake)
        /// over ones where it definitely can't - because it "expects" that the opponent won't allow the win to happen.
        /// </remarks>
        protected int Minimax(GameState state, bool player, int depth, bool isMaximizing, out (int, int) bestMove, bool isInitialCall = false)
        {
            bestMove = (-1, -1);

            // evaluate the board and return a value if game over
            if (state.IsWon)
            {
                return state.IsWonByPlayerOne == player ? 1 : -1;
            }
            else if (state.IsDraw || depth == 0)
            {
                return 0;
            }

            // for each legal move: play the move, run recursively on the resulting state, revert move
            List<(int, int)> allMoves = state.GetAllLegalMoves();
            List<int> scores = new List<int>();
            foreach ((int, int) move in allMoves)
            {
                state.PlayMove(move);
                scores.Add(Minimax(state, player, depth - 1, !isMaximizing, out _));
                state.UndoMove(move);
            }

            // now make a list of indices of scores that are best/worst
            // and use it to pick a score (and a move) at random
            // but do this only if the method is being called directly from GetNextMove
            // (i.e. we're not in a recursive call) to hopefully save some overhead;
            // this could also be solved by wrapping the initial calls in a foreach(legalmoves) outside of the method (i.e., in GetNextMove)
            // and selecting the best move there - bestMove and isInitialCall wouldn't be needed,
            // however this will lead to some code duplication, possibly worse reusability for alphabeta;
            // TODO: needs to be benchmarked to evaluate the potential gain from refactoring
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
            // TODO: if first move of game and difficulty above certain threshold, start sensibly (corner or middle),
            // just to offer a hypothetical human opponent more chances to make a bad move
            int _ = Minimax(state, state.IsFirstPlayersTurn, Difficulty, true, out (int, int) bestMove, true);
            return bestMove;
        }
    }
}
