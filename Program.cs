using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;


namespace finalProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            string file = "player_log.csv";

            //Read file and store it in List to be adjusted.
            List<Player> PlayerList = new List<Player>();

            if (File.Exists(file)){

                var playerFile = File.ReadAllLines(file);
                foreach (string line in playerFile){
                    string[] playerLines = line.Split(',');
                    PlayerList.Add(new Player(playerLines[0], Convert.ToInt32(playerLines[1]), Convert.ToInt32(playerLines[2]), Convert.ToInt32(playerLines[3])));
                }

                Console.WriteLine("\nWelcome to Rock, Paper, Scissors!");
                p.introMenu(PlayerList);

                //Print List
                /*foreach (var player in PlayerList){
                    player.toString();
                }*/
            }
            else {
                Console.WriteLine("Could not find indicated file, please enter correct file path.");
            }
        }
        
        //Intro Menu. Begins Start New Game(1), Load Game(2), or Quit(3).
        void introMenu(List<Player> PlayerList){
            Program p = new Program();

            var input = "Finn";
            int play = 0;
            while (true){
                Console.WriteLine("\n\t1. Start New Game\n\t2. Load Game\n\t3. Quit");
                Console.WriteLine("\nEnter Choice: ");
                input = Console.ReadLine();

                if (!int.TryParse(input, out play) || (play != 1 && play != 2 && play != 3)) {
                    Console.WriteLine("\nInvalid input. Enter integer 1, 2, or 3.");
                    continue;
                }
                else {
                    break;
                }
            }

            if (play == 1){
                p.newGame(PlayerList);
            }
            else if (play == 2){
                p.loadGame(PlayerList);
            }
            else if (play == 3){
                Console.WriteLine("You Quit.");
                return;
            }

        }
            
        //Start New Game. Adds new player to List and begins gameplay
        //Prompt for name. Checks if name exists in List, reprompts if so.
        //Gameplay
        void newGame(List<Player> PlayerList){
            Program p = new Program();

            string newName = "Finn";
            while (true){
                Console.WriteLine("\nWhat is your name?");
                newName = Console.ReadLine();
                //int check = -1;

                //LINQ and Any method to check if the inputted name is contained within the list.
                if (PlayerList.Any(check => check.pName == newName)){
                    Console.WriteLine("\nPlayer already exists, enter new name.");
                    continue;
                }

                break;
            }
            Console.WriteLine($"\nHello {newName}. Let's play!");

            p.gameplay(PlayerList, newName);
            //Console.WriteLine($"\nOutcome = {outcome}");

            
        }

        //Loads previous player and begins gamepley.
        void loadGame(List<Player> PlayerList){
            Program p = new Program();

            string newName = "Finn";
            while (true){
                Console.WriteLine("\nWhat is your name?");
                newName = Console.ReadLine();

                //LINQ and Any method to check if the inputted name is contained within the list.
                if (!PlayerList.Any(check => check.pName == newName)){
                    Console.WriteLine($"\n{newName}, your game could not be found.");
                    continue;
                }

                break;
            }
            Console.WriteLine($"\nWelcome back {newName}. Let's play!");

            p.gameplay(PlayerList, newName);
            //Console.WriteLine($"\nOutcome = {outcome}");

        }

        void gameplay(List<Player> PlayerList, string name){
            Program p = new Program();
            int outcome = 0;
            while(true){
                Random rnd = new Random();
                
                int play = 0;
                //1 = player wins, 2 = player lost, 3 = player tie.
                outcome = 0;
                string status = "Finn";
                string pStatus = "Jake";
                string bStatus = "BMO";
                int round = 0;

                int bot = rnd.Next(1, 4);

                //Checks if name is in List and determines round number.
                if (!PlayerList.Any(check => check.pName == name)){
                    round = 1;
                }
                else if (PlayerList.Any(check => check.pName == name)){
                    foreach (var player in PlayerList){
                        if (player.pName == name){
                            round = player.getRound();
                            break;
                        }
                    }
                }

                //Prompts menu for gameplay.
                while (true){
                    Console.WriteLine($"\nRound {round}");
                    Console.WriteLine("\n\t1. Rock\n\t2. Paper\n\t3. Scissors");
                    Console.WriteLine("\nWhat will it be?");
                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out play) || (play != 1 && play != 2 && play != 3)) {
                        Console.WriteLine("\nInvalid input. Enter integer 1, 2, or 3.");
                        continue;
                    }
                    else {
                        break;
                    }
                }

                //Tie
                if (bot == play){
                    outcome = 3;
                    status = "Tie";
                }
                //Player Win
                else if ((bot == 1 && play == 2) || (bot == 2 && play == 3) || (bot == 3 && play == 1)){
                    outcome = 1;
                    status = "Win";
                }
                //Player Loss
                else if ((bot == 1 && play == 3) || (bot == 2 && play == 1) || (bot == 3 && play == 2)){
                    outcome = 2;
                    status = "Lose";
                }

                //Sets string based off decision.
                if (bot == 1){
                    bStatus = "Rock";
                }
                if (play == 1){
                    pStatus = "Rock";
                }
                if (bot == 2){
                    bStatus = "Paper";
                }
                if (play == 2){
                    pStatus = "Paper";
                }
                if (bot == 3){
                    bStatus = "Scissors";
                }
                if (play == 3){
                    pStatus = "Scissors";
                }

                Console.WriteLine($"\nYou chose {pStatus}. The computer chose {bStatus}. You {status}!");

                //Checks if player isn't in list and adds them to it.
                if (!PlayerList.Any(check => check.pName == name)){
                    //Add new player win.
                    if (outcome == 1){
                        PlayerList.Add(new Player(name, 1, 0, 0));
                    }
                    //Add new player loss.
                    else if (outcome == 2){
                        PlayerList.Add(new Player(name, 0, 1, 0));
                    }
                    //Add new player tie.
                    else if (outcome == 3){
                        PlayerList.Add(new Player(name, 0, 0, 1));
                    }
                }

                //Checks if player is in list and modifies their values accordingly.
                else if (PlayerList.Any(check => check.pName == name)){
                    foreach (var player in PlayerList){
                        if (player.pName == name){
                            if (outcome == 1){
                                player.wins+=1;
                                break;
                            }
                            if (outcome == 2){
                                player.losses+=1;
                                break;
                            }
                            if (outcome == 3){
                                player.ties+=1;
                                break;
                            }
                        }
                    }
                }

                p.postMenu(PlayerList, name);
                break;
            }
            //return outcome;
        }

        //After-round menu. Runs after every round.
        //Play Again = Restarts gameplay.
        //Player Stats = One player's stats and restarts after-round menu.
        //Global Leaderboard: Global stats and restarts after-round menu.
        void postMenu(List<Player> PlayerList, string name) {
            Program p = new Program();
            string fileAlt = "player_log.csv";

            int choice = 0;
            while (true){
                while (true){
                    //Prompts post game menu.
                    Console.WriteLine("\nWhat would you like to do?");
                    Console.WriteLine("\n\t1. Play Again\n\t2. View Player Statistics\n\t3. View Leaderboard\n\t4. Quit");
                    Console.WriteLine("\nEnter Choice:");
                    var input = Console.ReadLine();

                    if (!int.TryParse(input, out choice) || (choice != 1 && choice != 2 && choice != 3 && choice != 4)) {
                        Console.WriteLine("Invalid input. Enter integer 1, 2, or 3.");
                        continue;
                    }
                    else {
                        break;
                    }
                }

                //Play Again.
                if (choice == 1){
                    p.gameplay(PlayerList, name);
                    //Console.WriteLine($"Out = {outy}");
                }
                //View Player Statistics.
                else if (choice == 2){
                    Console.WriteLine($"\n{name}, here are your game play statistics...");
                    foreach (var player in PlayerList){
                        if (player.pName == name){
                            decimal ratio = 0;
                            Console.WriteLine($"Wins: {player.wins}\nLosses: {player.losses}\nTies: {player.ties}");
                            if (player.losses != 0){
                                ratio = Convert.ToDecimal(player.wins) / player.losses;
                            }
                            else if (player.losses == 0){
                                ratio = Convert.ToDecimal(player.wins) / 1;
                            }
                            
                            Console.WriteLine($"Win/Loss Ratio: {ratio:0.00}");
                            break;
                        }
                    }
                    continue;
                }
                //View global statistics.
                else if (choice == 3){
                    //Calculates global top 10 winning players.
                    var sortedPlayers = PlayerList.OrderByDescending(x => x.wins).ToList();
                    Console.WriteLine("\nGlobal Game Statistics\n----------------------\n----------------------");
                    Console.WriteLine("Top 10 Winning Players\n----------------------");
                    int count = 0;
                    foreach (var player in sortedPlayers){
                        Console.WriteLine($"{player.pName}: {player.wins}");
                        if (count == 9){ break;}
                        count++;
                    }

                    //Calculates global top 5 players with most games.
                    Console.WriteLine("\n----------------------\nMost Games Played\n----------------------");
                    int counter = 0;
                    int globalTotal = 0;
                    List<Player> mostGames = new List<Player>();
                    foreach (var player in sortedPlayers){
                        int total = player.getRound()-1;
                        globalTotal+=player.getRound()-1;
                        mostGames.Add(new Player(player.pName, total, 0, 0));
                    }
                    var mostPlayers = mostGames.OrderByDescending(x => x.wins).ToList();
                    foreach (var player in mostPlayers){
                        Console.WriteLine($"{player.pName}: {player.wins} games played");
                        if (counter == 4){ break; }
                        counter++;
                    }

                    //Calculates Global Win/Loss Ratio.
                    int tWins = 0;
                    int tLosses = 0;
                    decimal wlRatio = 0;
                    foreach (var player in PlayerList){
                        tWins+=player.wins;
                        tLosses+=player.losses;
                    }
                    if (tLosses != 0){
                        wlRatio = Convert.ToDecimal(tWins) / tLosses;
                    }
                    else if (tLosses == 0){
                        wlRatio = Convert.ToDecimal(tWins) / 1;
                    }
                    Console.WriteLine($"\n----------------------\nOverall Win/Loss Ratio: {wlRatio:0.00}\n----------------------");

                    //Calculates Total Games Played
                    Console.WriteLine($"\n----------------------\nTotal Games Played: {globalTotal}\n----------------------");

                    continue;
                }
                //Quit game and overwrite file with List.
                else if (choice == 4){
                    List<string> PlayerString = new List<string>();
                    foreach (var player in PlayerList){
                        PlayerString.Add(new string(String.Format($"{player.pName}, {player.wins}, {player.losses}, {player.ties}")));
                    }
                    File.WriteAllLinesAsync(fileAlt, PlayerString);
                    if (File.Exists(fileAlt)){
                        Console.WriteLine($"{name}, your game has been saved.");
                        break;
                    }
                    else {
                        Console.WriteLine($"Sorry {name}, the game could not be saved.");
                    }
                    break;
                }
                break;
            }
        }
    }
}
