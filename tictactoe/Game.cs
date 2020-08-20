using System;

namespace tictactoe
{
    public sealed class Game
    {
        private const char emptySymbol = ' ';
        public static char EmptySymbol { get => emptySymbol; }

        public char[,] GameBoard { get; private set; } = new char[3, 3] {
            { EmptySymbol, EmptySymbol, EmptySymbol },
            { EmptySymbol, EmptySymbol, EmptySymbol },
            { EmptySymbol, EmptySymbol, EmptySymbol }
        };

        public bool isFirstPlayersTurn { get; private set; } = true;
        public bool isWon { get; private set; } = false;
        public bool isTie { get; private set; } = false;

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        

        public Game(Player player1, Player player2, bool isFirstPlayersTurn)
        {   
            Player1 = player1;
            Player2 = player2;
            this.isFirstPlayersTurn = isFirstPlayersTurn;
        }

        public void UpdateIsWonOrTie()
        {
            Player potentialWinner = GetCurrentPlayer();
            char symbol = potentialWinner.Symbol;

            //rows
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                if (GameBoard[i,0] == symbol && GameBoard[i, 1] == symbol && GameBoard[i, 2] == symbol)
                {
                    isWon = true;
                    return;
                }
            }

            //cols
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                if (GameBoard[0, i] == symbol && GameBoard[1, i] == symbol && GameBoard[2, i] == symbol)
                {
                    isWon = true;
                    return;
                }
            }

            //diagonals
            if (GameBoard[1, 1] == symbol
                && ((GameBoard[0, 0] == symbol && GameBoard[2, 2] == symbol) 
                    || (GameBoard[0, 2] == symbol && GameBoard[2, 0] == symbol)))
            {
                isWon = true;
                return;
            }

            //tie
            isTie = true;
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    if (GameBoard[i, j] == EmptySymbol)
                    {
                        isTie = false;
                        return;
                    }
                }
            }
        }

        internal void SwitchPlayers()
        {
            isFirstPlayersTurn = !isFirstPlayersTurn;
        }

        public Player GetCurrentPlayer()
        {
            return isFirstPlayersTurn ? Player1 : Player2;
        }

        public Player GetOtherPlayer()
        {
            return isFirstPlayersTurn ? Player2 : Player1;
        }

        public void UpdateBoard((int, int) coordinates)
        {
            if (!Utils.IsLegalMove(coordinates, GameBoard))
            {
                return;
            }

            char symbol = GetCurrentPlayer().Symbol;
            GameBoard[coordinates.Item1, coordinates.Item2] = symbol;            
        }

        public (int, int) GetNextMoveFromHuman()
        {
            (int, int) nextMove;
            bool isLegal;
            do
            {
                nextMove = this.ReadHumanMoveFromConsole();
                isLegal = Utils.IsLegalMove(nextMove, GameBoard);

                if (!isLegal)
                {
                    Console.WriteLine("Illegal move.");
                }
            }
            while (!Utils.IsLegalMove(nextMove,GameBoard));

            return nextMove;
        }

        public (int, int) ReadHumanMoveFromConsole()
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
            Player currentPlayer = GetCurrentPlayer();
            if (!currentPlayer.IsHuman)
            {
                Console.WriteLine("Press any key to let " + currentPlayer.Name + " play.");
                Console.ReadKey();
            }
            Console.WriteLine(currentPlayer.Name + "'s turn");

            (int, int) nextMove = currentPlayer.GetNextMove(GameBoard);
            UpdateBoard(nextMove);
        }

        public void PrintBoard()
        {
            Console.WriteLine('╔' + new string('═',GameBoard.GetLength(0)) + '╗');
            for (int i = 0; i < GameBoard.GetLength(0); i++)
            {
                Console.Write('║');
                for (int j = 0; j < GameBoard.GetLength(1); j++)
                {
                    Console.Write(GameBoard[i, j].ToString());
                }
                Console.WriteLine('║');
            }
            Console.WriteLine('╚' + new string('═', GameBoard.GetLength(0)) + '╝');
        }
    }
}
