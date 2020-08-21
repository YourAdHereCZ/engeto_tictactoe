using System;

namespace tictactoe
{
    class Program
    {
        private static Player UserPicksAI(char symbol, string name)
        {
            Console.WriteLine("What type of AI should " + name + " be?");
            Console.WriteLine("1) (E)asy - picks random moves [default]");
            Console.WriteLine("2) (H)ard - never loses");
            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "2" || response == "h" || response == "hard")
            {
                return new PlayerAIMinimax(symbol, name, false);
            }
            return new PlayerAIRandom(symbol, name, false);
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
                //Console.WriteLine("Come again!");
                return 4; 
            }
            return 1;
        }
        private static void UserPicksPlayerNames(ref string playerOneName, ref string playerTwoName)
        {
            Console.Write("Would you like to select players' names? y/N ");
            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "y" || response == "yes")
            {
                Console.Write("Type the name for player 1: ");
                playerOneName = Console.ReadLine();
                Console.Write("Type the name for player 2: ");
                playerTwoName = Console.ReadLine();
            }
        }
        private static void UserPicksPlayerSymbols(string playerOneName, string playerTwoName, ref char playerOneSymbol, ref char playerTwoSymbol)
        {
            Console.Write("Would you like to select players' symbols? y/N ");
            string response = Console.ReadLine().ToLower();
            if (response == "y" || response == "yes")
            {
                // TODO: refactor
                response = "";
                while (response.Length == 0)
                {
                    Console.Write("Select the symbol for " + playerOneName + ": ");
                    response = Console.ReadLine();
                }
                playerOneSymbol = response[0];
                if (response.Length > 1)
                {
                    Console.WriteLine("More than one character specified. The first character (\'" + playerOneSymbol + "\') will be used.");
                }

                response = "";
                while (response.Length == 0)
                {
                    Console.Write("Select the symbol for " + playerTwoName + ": ");
                    response = Console.ReadLine();
                }
                playerTwoSymbol = response[0];
                if (response.Length > 1)
                {
                    Console.WriteLine("More than one character specified. The first character (\'" + playerTwoSymbol + "\') will be used.");
                }
            }
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
                string playerOneName = "Player 1";
                string playerTwoName = "Player 2";

                char playerOneSymbol = 'X';
                char playerTwoSymbol = 'O';

                bool firstPlayerStarts;

                Player playerOne;
                Player playerTwo;

                int gameMode = UserPicksGameMode();
                if (gameMode == 4) break;
                UserPicksPlayerNames(ref playerOneName, ref playerTwoName);
                UserPicksPlayerSymbols(playerOneName, playerTwoName, ref playerOneSymbol, ref playerTwoSymbol);
                firstPlayerStarts = UserPicksWhoStarts(playerOneName);
                // TODO: who starts next round?

                switch (gameMode)
                {
                    case 1:
                        playerOne = new PlayerHuman(playerOneSymbol, playerOneName, true);
                        playerTwo = UserPicksAI(playerTwoSymbol, playerTwoName);
                        break;
                    case 2:
                        playerOne = new PlayerHuman(playerOneSymbol, playerOneName, true);
                        playerTwo = new PlayerHuman(playerTwoSymbol, playerTwoName, true);
                        break;
                    case 3:
                        playerOne = UserPicksAI(playerOneSymbol, playerOneName);
                        playerTwo = UserPicksAI(playerTwoSymbol, playerTwoName);
                        break;
                    default:
                        throw new ArgumentException();
                }

                Game game;
                bool playAgain = true;

                while (playAgain)
                {
                    game = new Game(playerOne, playerTwo, firstPlayerStarts);
                    game.PrintBoard();

                    while (!game.IsFinal)
                    {
                        if (!game.GetCurrentPlayer().IsHuman)
                        {
                            Console.WriteLine("Press any key to let " + game.GetCurrentPlayer().Name + " play.");
                            Console.ReadKey();
                        }
                        Console.WriteLine(game.GetCurrentPlayer().Name + "'s turn");
                        game.PlayNextTurn();
                        game.PrintBoard();
                    }

                    Console.WriteLine(game.IsWon ? game.GetCurrentPlayer().Name + " wins!" : "It's a tie!");
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
