using System;
using System.Collections.Generic;

namespace tictactoe
{
    public class Game
    {
        private protected const char _EMPTY_SYMBOL = ' ';

        private static Random rnd = new Random();

        public char[,] gameBoard = new char[3, 3] {
            { _EMPTY_SYMBOL, _EMPTY_SYMBOL, _EMPTY_SYMBOL },
            { _EMPTY_SYMBOL, _EMPTY_SYMBOL, _EMPTY_SYMBOL },
            { _EMPTY_SYMBOL, _EMPTY_SYMBOL, _EMPTY_SYMBOL }
        };

        private bool isFirstPlayersTurn { get; set; }
        public bool isWon { get; set; } = false;
        public bool isTie = false;

        private PlayerBase player1;
        private PlayerBase player2;
        
        public Game(PlayerBase player1, PlayerBase player2, bool isFirstPlayersTurn)
        {   
            this.player1 = player1;
            this.player2 = player2;
            this.isFirstPlayersTurn = isFirstPlayersTurn;
        }

        public Game(PlayerBase player1, PlayerBase player2, bool isFirstPlayersTurn, char[,] gameBoard) 
            : this(player1, player2, isFirstPlayersTurn)
        {
            this.gameBoard = gameBoard;
        }

        private bool IsLegalMove((int, int) coordinates)
        {
            int row = coordinates.Item1;
            int col = coordinates.Item2;

            if (row < 1 || row > gameBoard.GetLength(0))
            {
                return false;
            }
            if (col < 1 || col > gameBoard.GetLength(1))
            {
                return false;
            }
            if (gameBoard[row-1, col-1] != _EMPTY_SYMBOL)
            {
                return false;
            }

            return true;

        }

        private List<(int, int)> GetAllLegalMoves()
        {
            List<(int, int)> legalMoves = new List<(int, int)>();
            (int, int) move;
            for (int i = 1; i <= gameBoard.GetLength(0); i++)
            {
                for (int j = 1; j <= gameBoard.GetLength(1); j++)
                {
                    move = (i, j);
                    if (IsLegalMove(move))
                    {
                        legalMoves.Add(move);
                    }
                }
            }

            return legalMoves;
        }

        public void UpdateIsWonOrTie()
        {
            PlayerBase potentialWinner = GetCurrentPlayer();
            char symbol = potentialWinner.symbol;

            //rows
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (gameBoard[i,0] == symbol && gameBoard[i, 1] == symbol && gameBoard[i, 2] == symbol)
                {
                    isWon = true;
                    return;
                }
            }

            //cols
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                if (gameBoard[0, i] == symbol && gameBoard[1, i] == symbol && gameBoard[2, i] == symbol)
                {
                    isWon = true;
                    return;
                }
            }

            //diagonals
            if (gameBoard[1, 1] == symbol 
                && ((gameBoard[0, 0] == symbol && gameBoard[2, 2] == symbol) 
                    || (gameBoard[0, 2] == symbol && gameBoard[2, 0] == symbol)))
            {
                isWon = true;
                return;
            }

            //tie
            isTie = true;
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    if (gameBoard[i, j] == _EMPTY_SYMBOL)
                    {
                        isTie = false;
                        return;
                    }
                }
            }
        }

        public void SwitchCurrentPlayer()
        {
            isFirstPlayersTurn = !isFirstPlayersTurn;
        }

        // returns the player whose turn it currently is
        public PlayerBase GetCurrentPlayer()
        {
            return isFirstPlayersTurn ? player1 : player2;
        }

        // does the opposite than the above,
        // i.e. returns the player whose turn it currently ISN'T
        public PlayerBase GetOtherPlayer()
        {
            return isFirstPlayersTurn ? player2 : player1;
        }

        public void UpdateBoard((int, int) coordinates)
        {
            if (!IsLegalMove(coordinates))
            {
                return;
            }

            char symbol = GetCurrentPlayer().symbol;
            gameBoard[coordinates.Item1 - 1, coordinates.Item2 - 1] = symbol;            
        }

        public (int, int) PickRandomLegalMove()
        {
            var moves = GetAllLegalMoves();
            int rand = rnd.Next(moves.Count);
            (int, int) randomLegalMove = moves[rand];
            return randomLegalMove;
        }

        public (int, int) GetNextMoveFromComputer()
        {
            return PickRandomLegalMove();
        }

        public (int, int) GetNextMoveFromHuman()
        {
            (int, int) nextMove;
            bool isLegal;
            do
            {
                nextMove = this.ReadMoveFromConsole();
                isLegal = IsLegalMove(nextMove);

                if (!isLegal)
                {
                    Console.WriteLine("Illegal move.");
                }
            }
            while (!IsLegalMove(nextMove));

            return nextMove;
        }

        public (int, int) ReadMoveFromConsole()
        {
            bool tryParse;
            int row;
            do
            {
                Console.Write("Row: ");
                tryParse = int.TryParse(Console.ReadLine(), out row);
                if (!tryParse)
                {
                    Console.WriteLine("You need to specify a positive integer. ");
                }
            }
            while (!tryParse);

            int col;
            do
            {
                Console.Write("Column: ");
                tryParse = int.TryParse(Console.ReadLine(), out col);
                if (!tryParse)
                {
                    Console.Write("You need to specify a positive integer. ");
                }
            }
            while (!tryParse);

            return (row, col);
        }

        public void PlayNextTurn()
        {
            if (!GetCurrentPlayer().IsHuman())
            {
                Console.WriteLine("Press any key to let " + GetCurrentPlayer().name + " play.");
                Console.ReadKey();
            }
            Console.WriteLine(GetCurrentPlayer().name + "'s turn");
            (int, int) nextMove = GetCurrentPlayer().IsHuman() ? GetNextMoveFromHuman() : GetNextMoveFromComputer();
            UpdateBoard(nextMove);
        }

        public void PrintBoard()
        {
            Console.WriteLine('╔' + new string('═',gameBoard.GetLength(0)) + '╗');
            for (int i = 0; i < gameBoard.GetLength(0); i++)
            {
                Console.Write('║');
                for (int j = 0; j < gameBoard.GetLength(1); j++)
                {
                    Console.Write(gameBoard[i, j].ToString());
                }
                Console.WriteLine('║');
            }
            Console.WriteLine('╚' + new string('═', gameBoard.GetLength(0)) + '╝');
        }
    }
}
