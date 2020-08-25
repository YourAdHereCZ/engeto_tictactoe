using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{    
    public class GameState
    {
        /// <summary>
        /// Represents the board as a 3x3 2D array of nullable booleans.
        /// </summary>
        /// <remarks>
        /// null  = empty
        /// true  = player 1, 
        /// false = player 2
        /// </remarks>
        public bool?[,] Board { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder boardBuilder = new StringBuilder(12);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!Board[i, j].HasValue)
                    {
                        boardBuilder.Append(' ');
                    }
                    else
                    {
                        boardBuilder.Append((bool)Board[i, j] ? 'X' : 'O');
                    }
                }
                boardBuilder.Append("\n");
            }
            return boardBuilder.ToString().Trim('\n');
        }

        /// <summary>
        /// Returns true if the board is won by player one, false otherwise.
        /// </summary>
        public bool IsWonByPlayerOne { get { return GetIsWon(Board, true); } }

        /// <summary>
        /// Returns true if the board is won by player two, false otherwise.
        /// </summary>
        public bool IsWonByPlayerTwo { get { return GetIsWon(Board, false); } }

        /// <summary>
        /// Represents the player whose turn it is.
        /// </summary>
        /// <remarks>
        /// true = player 1, 
        /// false = player 2
        /// it could be argued that this might as well be called CurrentPlayer,
        /// but that would get confused with Game.CurrentPlayer, also this description is
        /// more indicative of the fact that it is a boolean value
        /// </remarks>
        public bool IsFirstPlayersTurn { get; private set; }

        /// <summary>
        /// Returns true if the board is drawn, or won by either player, false otherwise.
        /// </summary>
        public bool IsFinal { get { return GetIsFull() || IsWon; } }

        /// <summary>
        /// Returns true wif the board is drawn, false otherwise.
        /// </summary>
        public bool IsDraw { get { return GetIsFull() && !IsWon; } }

        /// <summary>
        /// Returns true when the board is won by either player, false otherwise.
        /// </summary>
        public bool IsWon { get { return IsWonByPlayerOne || IsWonByPlayerTwo; } }

        /// <summary>
        /// Creates a clean GameState (typically for a new game).
        /// </summary>
        /// <param name="isFirstPlayersTurn">Whose turn it is.</param>
        public GameState(bool isFirstPlayersTurn)
        {
            Board = new bool?[3, 3];
            IsFirstPlayersTurn = isFirstPlayersTurn;
        }

        /// <summary>
        /// Creates a GameState from a custom board.
        /// </summary>
        /// <param name="isFirstPlayersTurn">Whose turn it is.</param>
        /// <param name="board">The board to be used by the GameState.</param>
        public GameState(bool isFirstPlayersTurn, bool?[,] board)
        {
            Board = board;
            IsFirstPlayersTurn = isFirstPlayersTurn;
        }

        /// <summary>
        /// Checks whether a move is legal on this board.
        /// </summary>
        /// <param name="move"></param>
        /// <returns>True if the move is legal, false otherwise.</returns>
        public bool IsLegalMove((int, int) move)
        {
            int row = move.Item1;
            int col = move.Item2;

            if (row < 0 || row >= 3)
            {
                return false;
            }
            if (col < 0 || col >= 3)
            {
                return false;
            }
            if (Board[row, col].HasValue)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a list of all legal moves on this board.
        /// </summary>
        /// <returns>A list of all the legal moves.</returns>
        public List<(int, int)> GetAllLegalMoves()
        {
            List<(int, int)> legalMoves = new List<(int, int)>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!Board[i, j].HasValue)
                    {
                        legalMoves.Add((i, j));
                    }
                }
            }
            return legalMoves;
        }

        internal void PlayMove((int, int) move)
        {
            if (!IsLegalMove(move))
            {
                throw new ArgumentException();
            }
            Board[move.Item1, move.Item2] = IsFirstPlayersTurn;
            SwitchPlayers();
        }

        internal void UndoMove((int, int) move)
        {
            if (IsLegalMove(move))
            {
                throw new ArgumentException();
            }
            Board[move.Item1, move.Item2] = null;
            SwitchPlayers();
        }

        private void SwitchPlayers()
        {
            IsFirstPlayersTurn = !IsFirstPlayersTurn;
        }

        private bool GetIsFull()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (!Board[i, j].HasValue)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static uint BoardToBitmask(bool?[,] board, bool player)
        {
            uint mask = 0;
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == player)
                    {
                        mask += 1u << index;
                    }
                    index++;
                }
            }
            return mask;
        }

        private static bool GetIsWon(bool?[,] board, bool player)
        {
            uint mask = BoardToBitmask(board, player);
            if (((mask & 0b000000111) == 0b000000111)  // row1
             || ((mask & 0b000111000) == 0b000111000)  // row2
             || ((mask & 0b111000000) == 0b111000000)  // row3
             || ((mask & 0b001001001) == 0b001001001)  // col1
             || ((mask & 0b010010010) == 0b010010010)  // col2
             || ((mask & 0b100100100) == 0b100100100)  // col3
             || ((mask & 0b100010001) == 0b100010001)  // dia1
             || ((mask & 0b001010100) == 0b001010100)) // dia2
            {
                return true;
            }
            return false;
        }
    }
}
