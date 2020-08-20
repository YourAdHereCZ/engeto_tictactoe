using System;
using System.Collections.Generic;
using System.Linq;

namespace tictactoe
{
    public class Utils
    {
        internal static char[] FlattenBoard(char[,] gameBoard)
        {
            char[] result = new char[9];
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    result[i * gameBoard.GetLength(0) + j] = gameBoard[i, j];
                }
            }
            return result;
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

        internal static bool IsWon(char[,] gameBoard, char player)
        {
            char[] flat = FlattenBoard(gameBoard);

            if ((flat[0] == player && flat[1] == player && flat[2] == player) 
                || (flat[3] == player && flat[4] == player && flat[5] == player)
                || (flat[6] == player && flat[7] == player && flat[8] == player)
                || (flat[0] == player && flat[3] == player && flat[6] == player)
                || (flat[1] == player && flat[4] == player && flat[7] == player)
                || (flat[2] == player && flat[5] == player && flat[8] == player)
                || (flat[0] == player && flat[4] == player && flat[8] == player)
                || (flat[2] == player && flat[4] == player && flat[6] == player))
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
