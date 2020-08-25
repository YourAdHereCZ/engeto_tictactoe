using System;
using System.Text;

namespace tictactoe
{
    public sealed class Game
    {
        /// <summary>
        /// The default symbol for an empty space on the board.
        /// </summary>
        private const char emptySymbol = ' ';
        public static char EmptySymbol { get => emptySymbol; }

        public GameState State { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }
        public Player CurrentPlayer { get { return State.IsFirstPlayersTurn ? Player1 : Player2; } }
        public Player OtherPlayer { get { return State.IsFirstPlayersTurn ? Player2 : Player1; } }

        /// <summary>
        /// Creates a new instance of Game with a clean board.
        /// </summary>
        /// <param name="player1">Player one</param>
        /// <param name="player2">Player two</param>
        /// <param name="isFirstPlayersTurn">Whose turn it is</param>
        public Game(Player player1, Player player2, bool isFirstPlayersTurn)
        {
            State = new GameState(isFirstPlayersTurn);
            Player1 = player1;
            Player2 = player2;
        }

        /// <summary>
        /// Creates a new instance of Game from a custom board.
        /// </summary>
        /// <param name="player1">Player one</param>
        /// <param name="player2">Player two</param>
        /// <param name="isFirstPlayersTurn">Whose turn it is</param>
        /// <param name="board">The custom board to begin on</param>
        public Game(Player player1, Player player2, bool isFirstPlayersTurn, bool?[,] board)
        {
            State = new GameState(isFirstPlayersTurn, board);
            Player1 = player1;
            Player2 = player2;
        }

        /// <summary>
        /// Plays out the next move and switches players.
        /// </summary>
        public void PlayNextTurn()
        {
            (int, int) nextMove = CurrentPlayer.GetNextMove(State);
            State.PlayMove(nextMove);
        }

        /// <summary>
        /// Prints the board to the console.
        /// </summary>
        public void PrintBoard()
        {
            string[] rows = GetBoardString().Split('\n');

            Console.WriteLine(" c123 \nr╔═══╗");
            for (int i = 0; i < 3; i++)
            {
                Console.Write((i + 1).ToString() + "║" + rows[i] + "║\n");
            }
            Console.WriteLine(" ╚═══╝");
        }

        /// <summary>
        /// Returns a human-readable string representation of the board.
        /// </summary>
        public string GetBoardString()
        {
            StringBuilder boardBuilder = new StringBuilder(12);
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!State.Board[i, j].HasValue)
                    {
                        boardBuilder.Append(EmptySymbol);
                    }
                    else
                    {
                        boardBuilder.Append((bool)State.Board[i, j] ? Player1.Symbol : Player2.Symbol);
                    }
                }
                boardBuilder.Append("\n");
            }
            return boardBuilder.ToString().Trim('\n');
        }
    }
}
