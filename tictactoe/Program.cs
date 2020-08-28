using System;

namespace tictactoe
{
    class Program
    {
        private static Player UserPicksAI(char symbol, string name)
        {
            Console.WriteLine("What type of AI should " + name + " be?"
                + "\n1) (E)asy - picks random moves [default]"
                + "\n2) (M)oderate"
                + "\n3) (H)ard"
                + "\n4) e(X)treme");

            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "2" || response == "m" || response == "moderate")
            {
                return new PlayerAIAlphaBeta(symbol, name, 4);
            }
            else if(response == "3" || response == "h" || response == "hard")
            {
                return new PlayerAIAlphaBeta(symbol, name, 5);
            }
            else if (response == "4" || response == "x" || response == "extreme")
            {
                return new PlayerAIAlphaBeta(symbol, name, 9);
            }
            return new PlayerAIRandom(symbol, name);
        }

        private static int UserPicksGameMode()
        {
            Console.WriteLine("Choose an option by typing a number or a letter:" +
                "\n1) (C)lassic - play against a computer opponent [default]" +
                "\n2) (H)otseat - play against a friend" +
                "\n3) (O)bserve - watch two computer players" +
                "\n4) (Q)uit");
            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "2" || response == "h" || response == "hotseat")
            {
                return 2;
            }
            else if (response == "3" || response == "o" || response == "observe")
            {
                return 3;
            }
            else if (response == "4" || response == "q" || response == "quit")
            {
                return 4;
            }
            return 1;
        }
        private static (string, string) UserPicksPlayerNames()
        {

            string playerOneName = "Player 1";
            string playerTwoName = "Player 2";

            Console.Write("Would you like to select players' names? y/N ");
            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "y" || response == "yes")
            {
                Console.Write("Type the name for " + playerOneName + ": ");
                playerOneName = Console.ReadLine();
                Console.Write("Type the name for " + playerTwoName + ": ");
                playerTwoName = Console.ReadLine();
            }
            return (playerOneName, playerTwoName);
        }
        private static (char, char) UserPicksPlayerSymbols((string first, string second) playersNames)
        {
            char playerOneSymbol = 'X';
            char playerTwoSymbol = 'O';

            Console.Write("Would you like to select players' symbols? y/N ");
            string response = Console.ReadLine().ToLower();
            if (response == "y" || response == "yes")
            {
                response = "";
                while (response.Length == 0)
                {
                    Console.Write("Select the symbol for " + playersNames.first + ": ");
                    response = Console.ReadLine();
                }
                playerOneSymbol = response[0];
                if (response.Length > 1)
                {
                    Console.WriteLine("More than one character specified. The first character (\'" + playerOneSymbol + "\') will be used.");
                }

                playerTwoSymbol = playerOneSymbol;
                while (response.Length == 0 || playerTwoSymbol == playerOneSymbol) 
                {
                    Console.Write("Select the symbol for " + playersNames.second + ": ");
                    response = Console.ReadLine();
                    
                    if (response.Length != 0)
                    {
                        playerTwoSymbol = response[0];
                    }
                    if (playerTwoSymbol == playerOneSymbol)
                    {
                        Console.WriteLine("The players' symbols must be unique.");
                    }
                }
                
                if (response.Length > 1)
                {
                    Console.WriteLine("More than one character specified. The first character (\'" + playerTwoSymbol + "\') will be used.");
                }
            }
            return (playerOneSymbol, playerTwoSymbol);
        }

        private static bool UserPicksWhoStarts(string playerOneName)
        {
            Console.Write("Would you like to switch the players' order? (By default, " + playerOneName + " will start in the first round.) y/N ");
            string response = Console.ReadLine().ToLower();
            if (response == "y" || response == "yes")
            {
                return false;
            }
            return true;
        }

        static void Main(string[] args)
        {
            bool continueGame = true;           

            Console.WriteLine("Welcome to TicTacToe!");
            
            while (continueGame)
            {
                int gameMode;
                (string first, string second) playersNames;
                (char first, char second) playersSymbols;
                bool firstPlayerStarts;                

                gameMode = UserPicksGameMode();
                if (gameMode == 4) break;
                playersNames = UserPicksPlayerNames();
                playersSymbols = UserPicksPlayerSymbols(playersNames);
                firstPlayerStarts = UserPicksWhoStarts(playersNames.first);
                // TODO: who starts next round?
                // keep / flip / winner / loser / random

                (Player first, Player second) players;

                switch (gameMode)
                {
                    case 1:
                        players.first = new PlayerHuman(playersSymbols.first, playersNames.first);
                        players.second = UserPicksAI(playersSymbols.second, playersNames.second);
                        break;
                    case 2:
                        players.first = new PlayerHuman(playersSymbols.first, playersNames.first);
                        players.second = new PlayerHuman(playersSymbols.second, playersNames.second);
                        break;
                    case 3:
                        players.first = UserPicksAI(playersSymbols.first, playersNames.first);
                        players.second = UserPicksAI(playersSymbols.second, playersNames.second);
                        break;
                    default:
                        throw new ArgumentException();
                }

                Game game;
                bool playAgain = true;

                while (playAgain)
                {
                    game = new Game(players.first, players.second, firstPlayerStarts);
                    game.PrintBoard();

                    while (!game.State.IsFinal)
                    {
                        Console.WriteLine(game.CurrentPlayer.Name + "'s turn: ");
                        if (!game.CurrentPlayer.IsHuman)
                        {
                            System.Threading.Thread.Sleep(1500);
                        }
                        
                        game.PlayNextTurn();
                        game.PrintBoard();
                    }

                    Console.WriteLine(game.State.IsWon ? game.OtherPlayer.Name + " wins!" : "It's a tie!");
                    Console.WriteLine("Play again? (Y)es / (n)o / (c)hange mode");

                    string response = Console.ReadLine().ToLower();
                    if (response == "n" || response == "no")
                    {
                        playAgain = false;
                        continueGame = false;
                    }
                    else if (response == "c" || response == "change")
                    {
                        playAgain = false;
                    }                    
                }
            }            
            Console.WriteLine("Thank you for playing! Come again!");
        }        
    }
}
