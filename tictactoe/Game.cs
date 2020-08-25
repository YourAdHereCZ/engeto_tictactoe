using System;
using System.Text;

namespace tictactoe
{
    public sealed class Game
    {
        /// <summary>
        /// Stores the default symbol for an empty space on the board.
        /// </summary>
        private const char emptySymbol = ' ';
        public static char EmptySymbol { get => emptySymbol; }

        public GameState State { get; private set; }
        public Player Player1 { get; private set; }
        public Player Player2 { get; private set; }

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
        /// Returns the player whose turn it currently is.
        /// </summary>
        /// <returns>Player 1 if it is Player 1's turn, PLayer 2 otherwise</returns>
        public Player GetCurrentPlayer()
        {
            return State.IsFirstPlayersTurn ? Player1 : Player2;
        }

        /// <summary>
        /// Returns the player whose turn it currently isn't.
        /// </summary>
        /// <returns>Player 2 if it is Player 1's turn, Player 1 otherwise</returns>
        public Player GetOtherPlayer()
        {
            return State.IsFirstPlayersTurn ? Player2 : Player1;
        }

        /// <summary>
        /// Plays out the next move and switch players.
        /// </summary>
        public void PlayNextTurn()
        {
            Player currentPlayer = GetCurrentPlayer();
            (int, int) nextMove = currentPlayer.GetNextMove(State);
            UpdateBoard(nextMove);
            State.IsFirstPlayersTurn = !State.IsFirstPlayersTurn;
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
            StringBuilder boardBuilder = new StringBuilder(11);
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

        private void UpdateBoard((int, int) move)
        {
            if (!State.IsLegalMove(move))
            {
                throw new ArgumentException();
            }
            State.Board[move.Item1, move.Item2] = State.IsFirstPlayersTurn;
        }
    }
}
