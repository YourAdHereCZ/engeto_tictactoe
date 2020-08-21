using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class Utils
    {
        internal static bool IsLegalMove((int, int) move, char[,] gameBoard)
        {
            int row = move.Item1;
            int col = move.Item2;

            if (row < 0 || row >= gameBoard.GetLength(0))
            {
                return false;
            }
            if (col < 0 || col >= gameBoard.GetLength(1))
            {
                return false;
            }
            if (gameBoard[row, col] != Game.EmptySymbol)
            {
                return false;
            }
            return true;
        }

        internal static bool IsFull(char[,] gameBoard)
        {
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == Game.EmptySymbol)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static uint BoardToBitmask(char[,] gameBoard, char player)
        {
            uint mask = 0;
            int index = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (gameBoard[i, j] == player)
                    {
                        mask += 1u << index;
                    }
                    index++;
                }
            }
            return mask;
        }

        public static bool IsWon(char[,] gameBoard, char player)
        {
            uint mask = BoardToBitmask(gameBoard, player);
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

        internal static List<(int, int)> GetAllLegalMoves(char[,] gameBoard)
        {
            List<(int, int)> legalMoves = new List<(int, int)>();
            (int, int) move;
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    move = (i, j);
                    if (IsLegalMove(move, gameBoard))
                    {
                        legalMoves.Add(move);
                    }
                }
            }
            return legalMoves;
        }
    }
}
