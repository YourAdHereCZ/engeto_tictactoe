using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace tictactoe
{
    public class Utils
    {
        internal static string FlattenBoard(char[,] gameBoard)
        {
            char[] result = new char[9];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    result[i * 3 + j] = gameBoard[i, j];
                }
            }
            return new string(result);
        }

        internal static char[,] InflateBoard(string flattenedBoard)
        {
            throw new NotImplementedException();
        }

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
