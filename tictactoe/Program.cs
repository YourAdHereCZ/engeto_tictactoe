using System;
using System.ComponentModel.Design;
using System.Threading;

namespace tictactoe
{
    class Program
    {
        private static Player PickAndCreateAI(char symbol, string name)
        {
            Console.WriteLine("What type of AI should " + name + " be?");
            Console.WriteLine("1) (E)asy - picks random moves (default)");
            Console.WriteLine("2) (h)ard - never loses");
            string response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "2" || response == "h" || response == "hard")
            {
                return new PlayerAIMinimax(symbol, name, false);
            }
            else return new PlayerAIRandom(symbol, name, false);
        }

        static void Main(string[] args)
        {
            // greet
            Console.WriteLine("Welcome to TicTacToe!");
        Menu:
            // prompt: game mode
            string response;
            int humanPlayerCount = 1;
            Console.WriteLine("Choose an option by typing the highlighted letter:" +
                "\n1) Play against a (c)omputer opponent (default)" +
                "\n2) Play against another (p)layer" +
                "\n3) (w)atch two computer players" +
                "\n4) (q)uit");
            response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "2" || response == "p" || response == "player")
            {
                humanPlayerCount = 2;
            }
            else if (response == "3" || response == "w" || response == "watch")
            {
                humanPlayerCount = 0;
            }
            else if (response == "4" || response == "q" || response == "quit")
            {
                Console.WriteLine("Come again!");
                return;
            }

            // prompt: choose names?
            string playerOneName = "Player 1";
            string playerTwoName = "Player 2";
            Console.Write("Would you like to select players' names? y/N ");
            response = Console.ReadLine().Trim(' ').ToLower();
            if (response == "y" || response == "yes")
            {
                
                Console.Write("Type the name for player 1: ");
                playerOneName = Console.ReadLine();
                Console.Write("Type the name for player 2: ");
                playerTwoName = Console.ReadLine();
            }

            // prompt: choose symbols?
            char playerOneSymbol = 'X';
            char playerTwoSymbol = 'O';
            Console.Write("Would you like to select players' symbols? y/N ");
            response = Console.ReadLine().ToLower();
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

            // difficulty ideas: 1: easiest (maybe reverse minimax, lets you win quickly?)
            //                   2: easy (picks random legal move) (easiest to implement)
            //                   3: moderate (picks from ruleset, often forgets rules)
            //                   4: hard (picks from ruleset, sometimes forgets rules)
            //                   5: impossible (always plays a perfect game using minimax)

            // create players
            Player playerOne;
            Player playerTwo;

            // TODO: we need to let the player pick the AI(s) now that we have more than one
            switch (humanPlayerCount)
            {
                case 0:
                    playerOne = PickAndCreateAI(playerOneSymbol, playerOneName); //;new PlayerAIRandom(playerOneSymbol, playerOneName, false);
                    playerTwo = PickAndCreateAI(playerTwoSymbol, playerTwoName);
                    break;
                case 1:
                    playerOne = new PlayerHuman(playerOneSymbol, playerOneName, true);
                    playerTwo = PickAndCreateAI(playerTwoSymbol, playerTwoName);
                    break;
                case 2:
                    playerOne = new PlayerHuman(playerOneSymbol, playerOneName, true);
                    playerTwo = new PlayerHuman(playerTwoSymbol, playerTwoName, true);
                    break;
                default:
                    throw new ArgumentException();
            }             

            // prompt: who starts?
            // default: player1
            Console.Write("Would you like to switch the players' order? (By default, " + playerOneName + " will start in the first round.) y/N ");
            response = Console.ReadLine().ToLower();
            bool firstPlayerStarts = true;
            if (response == "y" || response == "yes")
            {
                firstPlayerStarts = false;
            }

            // TODO: prompt: who starts next round?
            // default: no change
            // choices: no change / flip / winner starts / loser starts / random
            
            Game game;
            bool playAgain = true;

            // game loop
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

                response = Console.ReadLine().ToLower();
                if (response == "n" || response == "no")
                {
                    playAgain = false;
                }
                else if(response == "c" || response == "change")
                {
                    goto Menu;
                    // TODO: this is really ugly but it's just a quick hack to play again
                }
            }
            Console.WriteLine("Thank you for playing! Come again!");
        }
    }
}
