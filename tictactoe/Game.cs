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
        public bool isFinal { get; private set; } = false;

        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Game(Player player1, Player player2, bool isFirstPlayersTurn)
        {   
            Player1 = player1;
            Player2 = player2;
            this.isFirstPlayersTurn = isFirstPlayersTurn;
        }

        public void UpdateIsWonAndIsFinal()
        {
            isWon = Utils.IsWon(GameBoard, GetCurrentPlayer().Symbol) || Utils.IsWon(GameBoard, GetOtherPlayer().Symbol);
            isFinal = Utils.IsFull(GameBoard) || isWon;
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

        public void PlayMove((int, int) move)
        {
            if (!Utils.IsLegalMove(move, GameBoard))
            {
                return;
            }

            char symbol = GetCurrentPlayer().Symbol;
            GameBoard[move.Item1, move.Item2] = symbol;            
        }

        public void UndoMove((int, int) move)
        {
            GameBoard[move.Item1, move.Item2] = EmptySymbol;
        }


        public void PlayNextTurn()
        {
            Player currentPlayer = GetCurrentPlayer();
            Player otherPlayer = GetOtherPlayer();
            if (!currentPlayer.IsHuman)
            {
                Console.WriteLine("Press any key to let " + currentPlayer.Name + " play.");
                Console.ReadKey();
            }
            Console.WriteLine(currentPlayer.Name + "'s turn");

            (int, int) nextMove = currentPlayer.GetNextMove(GameBoard, currentPlayer.Symbol, otherPlayer.Symbol);
            PlayMove(nextMove);
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
