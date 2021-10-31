using System;
using System.Linq;
using k180307_DDR_A1.Library.Backend;
using k180307_DDR_A1.Library.Controller;

namespace k180307_DDR_A1.Library.Frontend
{   
    public class Session : ISession
    {
        /*
         * Class for maintaining the flow of the entire TicTacToe game 
         *
         * Responsibilities:
         *  - Switch contexts using class TicTacToe for the entire flow
         */
        
        // Instance of the TicTacToe logic 
        private TicTacToe TicTacToe { get; }
        
        // Determine the list of symbols valid for TicTacToe to accept from Session
        private string[] Symbols { get; }
        
        // A switch between PlayerOne, PlayerTwo
        private bool IsPlayerOneTurn { get; set; }
        
        // If playing again, do we want to get the info again?
        private bool GetPlayerInfo { get; set; }
        
        // For readability sake, introduce a enum for each player
        private enum PlayerIdentities
        {
            PlayerOne = 0,
            PlayerTwo = 1,
        };
        
        public Session()
        {
            /*
             * Instantiate attributes
             *
             * Responsibilities:
             *  - Instantiation
             */
            
            // Instantiate TicTacToe. Allows dynamic boards - default
            // set to 3.
            this.TicTacToe = new TicTacToe(dimensions: 3);
            
            // Let PlayerOne start first
            this.IsPlayerOneTurn = true;
            
            // Single Point of Addition/Removal for symbols
            this.Symbols = new[] {"X", "O", "+", "*", "-"};
            
            // Getting player info
            this.GetPlayerInfo = true;
        }

        public void Start()
        {
            /*
             * Start the game!
             *
             * Responsibilities:
             *  - Encapsulating the entire flow of the program
             */

            var isPlayAgain = true;

            // Start the game with the basic flow
            Console.WriteLine("Do you want to play TicTacToe?");
            Console.WriteLine("Enter Y to continue, any key to exit.");

            // If anything other than the letter, then exit the session
            if (Console.ReadLine() != "Y") return;

            // Clear the console screen
            Console.Clear();
            
            while (isPlayAgain)
            {
                // Get the desired player information
                if (this.GetPlayerInfo)
                {
                    TicTacToe.ClearPlayerList();
                    this.PlayerSetup();
                }

                // Setup te TicTacToe view
                this.TicTacToeSetup();
            
                // End prompt
                isPlayAgain = this.PlayAgainSetup();
            }
            
            this.Stop();
        }

        private void PlayerSetup()
        {
            /*
             * Gets the desired player information
             *
             * Responsibilities:
             *  - Validating inputs for player creation in TicTacToe
             */
            
            // Introduce variable to check for duplicate symbol edge case
            var duplicateSymbolCheck = "👻";
            
            // For each player in the game
            for (var iteration = 1; iteration <= 2; ++iteration)
            {
                var isCorrectSymbol = false; // Check for the right symbol selection
                
                // Initiate user input
                Console.WriteLine($"Enter Player {iteration.ToString()} Name");
                
                // Read name into 'name'
                var name = Console.ReadLine();

                // A loop until the correct symbol is selected
                while (!isCorrectSymbol)
                {
                    // Generate print dialog to provide information to user
                    this.PrintDialog($"Key in Player {iteration.ToString()} Symbol, any one from [", "]");
                    
                    // A variable to hold the selected symbol
                    // Read the symbol into 'symbol'
                    var symbol = Console.ReadLine();

                    // If the new symbol is the same as previous symbol ...
                    if (symbol == duplicateSymbolCheck)
                    {
                        // ... Inform the user - shift to next iteration
                        Console.WriteLine("Same as Player 1 Symbol, key in again");
                    }
                    else
                    {
                        // If the duplicateSymbolCheck is empty, set it to 'symbol'
                        // This piece of logic only works in the first iteration
                        // To make the scope of duplicateSymbolCheck last longer than an iteration
                        // it has been declared before the loop
                        if (duplicateSymbolCheck == "👻")
                            duplicateSymbolCheck = symbol;
                        
                        // If the selected symbol is correct
                        if (this.Symbols.Contains(symbol))
                        {
                            // Let the while loop know
                            isCorrectSymbol = true;

                            // And add the player to the tic tac toe game
                            TicTacToe.AddPlayer(new Player(name: name, symbol: symbol));
                        }
                        // If the symbol isn't correct ...
                        else
                        {
                            // Let the user know
                            this.PrintDialog("Symbol must be anyone from ", "");
                        }
                    }
                }
            }
            
            this.GetPlayerInfo = false;
        }

        private void TicTacToeSetup()
        {
            TicTacToe.ClearBoard();
            
            // Board initialization should be successful now
            Console.WriteLine("Board Initialized");
            
            // Key input to continue
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            
            Console.Clear();
            
            // Keep the tic tac toe view until the game is won
            while (!TicTacToe.IsGameWon() && TicTacToe.IsSpaceLeft())
            {
                // Print board by fetching the board via BoardService from TicTacToe
                BoardPrinter.PrintBoard(TicTacToe.FetchBoard());

                // Which player's turn is it? If true, it's PlayerOne, else it's PlayerTwo
                var playerIdentity = IsPlayerOneTurn ? PlayerIdentities.PlayerOne : PlayerIdentities.PlayerTwo;

                // Get the player's information via TicTacToe
                var player = this.TicTacToe.FetchPlayer((int) playerIdentity);

                // What are the board dimensions?
                var boardDimensions = this.TicTacToe.FetchBoard().Cells.Length.ToString();

                // Prompt to retrieve user answer
                Console.WriteLine($"Player {player.Name}, please enter position (1 - {boardDimensions})");

                // Is the input actually an int?
                if (int.TryParse(Console.ReadLine(), out var position))
                {
                    // If so, update the board!
                    if (this.TicTacToe.UpdateBoard(position: position, symbol: player.Symbol))
                    {
                        // Did the placement achieve a winning move?
                        if (TicTacToe.IsGameWon())
                        {
                            // Show ending prompt
                            Console.WriteLine($"Game Over!\n{player.Name} is the winner!\nPress any key to continue...");

                            // Read Key
                            Console.ReadKey();
                            
                            Console.Clear();
                        }
                        else
                        {
                            // It's the other person's turn
                            IsPlayerOneTurn = !IsPlayerOneTurn;
                        }
                    }
                }
                else
                {
                    // Print instructions on how to proceed
                    // If it is, let the user know
                    Console.WriteLine($"Unable to parse '{position.ToString()}'\n" +
                                      "Press any key to continue...");

                    // Read empty input
                    Console.ReadKey();
                }

                // Clear Screen
                if (TicTacToe.IsSpaceLeft())
                    Console.Clear();
            }

            if (!TicTacToe.IsDrawAchieved()) return;
            
            Console.WriteLine("Game Over!\n" +
                              "The game is tied\n" +
                              "Press any key to continue...");

            Console.ReadKey();
            
            Console.Clear();
        }

        private bool PlayAgainSetup()
        {
            Console.WriteLine("Do you want to play again?");
            Console.WriteLine("Enter Y to continue, any key to exist.");

            if (Console.ReadLine() != "Y") return false;

            Console.Clear();
            
            Console.WriteLine("Do you want to change the players?");
            Console.WriteLine("Press Y to change, any key to continue...");

            if (Console.ReadLine() == "Y")
                GetPlayerInfo = true;

            return true;
        }

        public void Stop()
        {
            /*
             * Clear data before termination
             */

            TicTacToe.ClearPlayerList();
            TicTacToe.ClearBoard();
        }

        private void PrintDialog(string message, string endWith)
        {
            Console.Write(message);
            
            foreach (var symbol in this.Symbols)
                Console.Write($"{symbol} ");
            
            Console.WriteLine(endWith);
        }
    }
}