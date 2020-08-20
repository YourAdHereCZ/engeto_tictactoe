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

        public bool IsFirstPlayersTurn { get; private set; } = true;

        
        public bool IsFinal { get { return Utils.IsFull(GameBoard) || IsWon; } }
        public bool IsWon { get { return Utils.IsWon(GameBoard, Player1.Symbol) || Utils.IsWon(GameBoard, Player2.Symbol); } }
        public bool IsTie { get { return IsFinal && !IsWon; } }
        
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

        public Game(Player player1, Player player2, bool isFirstPlayersTurn)
        {   
            Player1 = player1;
            Player2 = player2;
            IsFirstPlayersTurn = isFirstPlayersTurn;
        }

        public Player GetCurrentPlayer()
        {
            return IsFirstPlayersTurn ? Player1 : Player2;
        }

        public Player GetOtherPlayer()
        {
            return IsFirstPlayersTurn ? Player2 : Player1;
        }

        private void PlayMove((int, int) move)
        {
            if (!Utils.IsLegalMove(move, GameBoard))
            {
                return;
            }

            char symbol = GetCurrentPlayer().Symbol;
            GameBoard[move.Item1, move.Item2] = symbol;            
        }

        public void PlayNextTurn()
        {
            Player currentPlayer = GetCurrentPlayer();
            Player otherPlayer = GetOtherPlayer();            

            (int, int) nextMove = currentPlayer.GetNextMove(GameBoard, currentPlayer.Symbol, otherPlayer.Symbol);
            PlayMove(nextMove);

            if (!IsFinal)
            {
                IsFirstPlayersTurn = !IsFirstPlayersTurn;
            }
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
