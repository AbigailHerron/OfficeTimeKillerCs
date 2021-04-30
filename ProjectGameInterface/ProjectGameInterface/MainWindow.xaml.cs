/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 23/04/21
GitHub Link:

Description:  Is the Menu window logic for the Game Interface
Properties: db, players, games, selectedP, selectedG, date
EventBased Methods: wdnMain_Loaded, btnAddPlayer_Click, btnDelPlayer_Click, btnScoreboard_Click, btnRandom_Click, btnPlayGame_Click
Logic Methods: UpdatePlayerList, LaunchGame
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

// REFERENCES: https://www.youtube.com/watch?v=LZ8oJjn_2wE <-- MessageBox Tutorial
/*Method:  
          1) */

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container(); // linking windo to Database
        List<Player> players = new List<Player>();
        List<Game> games = new List<Game>();
        Player selectedP;
        Game selectedG;
        string date;




        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        public MainWindow()
        {
            InitializeComponent();
            this.date = DateTime.Today.ToString().Substring(0, 11);
        }// end Default Constructor




        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: wdnMain_Loaded
                  1) Creates a Database Object (db)
                  2) Calls UpdatePlayerList() method
                  2) Queries db for list of games
                  3) Populates both listboxes with respected query results */
        private void wdnMain_Loaded(object sender, RoutedEventArgs e)
        {
            

            // Querying Database
            UpdatePlayerList();

            var query = from g in db.Games
                         select g; // list of Games

            // Adding games to games List
            games = query.ToList();
            lbxGames.ItemsSource = games;
        }// end wdnMain_Loaded()



        /*Method: btnAddPlayer_Click()
                  1) Creates a new window object (wndAddPlayer)
                  2) Pauses until new window closes
                  3) Calls UpdatePlayerList() method */
        private void btnAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            wndAddPlayer wnd = new wndAddPlayer(); // creating Add Player window

            // opens new window and pauses method until it closes
            wnd.ShowDialog(); // displaying window over original (no need to hide it)
            
            UpdatePlayerList();
        }// end btnAddPlayer_Click()



        /*Method: btnDelPlayer_Click()
                  1) Checks whether a Player has been selected or not
                  2) Asks user if they're sure they want to delete the selected player (via MessageBox)
                  2) Attempts to remove selected player from DataSet Players
                  3) Displays MessageBox whether operation is successful or not
                  4) Calls UpdatePlayerList() method */
        private void btnDelPlayer_Click(object sender, RoutedEventArgs e)
        {
            if (lbxPlayers.SelectedItem == null)
                MessageBox.Show("Please select a player to delete", "No Player Selected");
            else
            { 
                selectedP = (Player)lbxPlayers.SelectedItem;

                if (MessageBox.Show($"Are you sure you want to delete {selectedP.PlayerName}?", "Delete This Player",
                                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    // Attempting to update DataSet Players of the Database
                    try // NOTE: No need to do a LINQ query as selectedP must be in the Database in order to be selected
                    {
                        db.Players.Remove(selectedP);
                        db.SaveChanges();
                        MessageBox.Show($"The Player {selectedP.PlayerName} Has Been Removed!", "Success");
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.ToString(), "Database Error");
                    }// end try/catch block
                    UpdatePlayerList();
                }
            }// end if statement
        }// end btnDelPlayer_Click()



        /*Method: btnScoreboard_Click()
                  1) Creates a new wndScoreBoard window object
                  2) Opens new window
                  3) Closes MainWindow */
        private void btnScoreboard_Click(object sender, RoutedEventArgs e)
        {
            wndScoreBoard wnd = new wndScoreBoard();
            wnd.Show();
            this.Close();
        }// end btnScoreboard_Click()



        /*Method: btnRandom_Click()
                  1) Checks whether a Player has been selected or not
                  2) Chooses a random game from the List games
                  3) Asks user if they want to play this game (via MessageBox)
                  4) Calls LaunchGame() method */
        private void btnRandom_Click(object sender, RoutedEventArgs e)
        {
            if (lbxPlayers.SelectedItem == null)
                MessageBox.Show("Please select a player first", "No Player Selected");
            else
            {
                Random rnd = new Random();
                selectedP = players.ElementAt(lbxPlayers.SelectedIndex);
                selectedG = games.ElementAt(rnd.Next(0, games.Count()));

                if (MessageBox.Show($"Do you want to play {selectedG.GameType}?", "Random Game",
                                    MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    LaunchGame(selectedP, selectedG);
            }
        }// end btnRandom_Click()



        /*Method: btnPlayGame_Click()
                  1) Checks whether a player has been selected and issues a warning via MessagBox if not
                  2) Checks whether a game ahs been selected and issues a warning via MessageBox if not
                  3) Passes the selectedP and selectedG into the LaunchGame() method */
        private void btnPlayGame_Click(object sender, RoutedEventArgs e)
        {
            if (lbxPlayers.SelectedItem == null)
                MessageBox.Show("Please select a player first", "No Player Selected");
            else if(lbxGames.SelectedItem == null)
                MessageBox.Show("Please select a game to play", "No Game Selected");
            else
            {
                selectedP = players.ElementAt(lbxPlayers.SelectedIndex);
                selectedG = games.ElementAt(lbxGames.SelectedIndex); // working around inability to use lbxGames.SelectedItem by itself

                // Used to have a MessageBox here but it's annoying to have so many, so I deleted it
                LaunchGame(selectedP, selectedG);
            }
        }// end btnPlayGame_Click()





        /*LOGIC BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: UpdatePlayerList()
                  1) Queries database for all rows in Player table
                  2) Adds list to players List variable*/
        public void UpdatePlayerList()
        {
            var query = from p in db.Players
                         select p; // list of Players

            players = query.ToList();
            lbxPlayers.ItemsSource = players;
        }// end UpdatePlayerList



        /*Method: LaunchGame()
                  1) Takes in a Player and a Game object 
                  2) Launches the appropriate window for the sG (selectedG / selected Game)
                           GameId, and passes in the sP (i.e. selectedP / selected Player)
                  3) For Fortune; tests wether the player has already played Fortune today
                           and if so, will issue a warning via MessageBox
                  4) Closes the MainWindow main */
        public void LaunchGame(Player sP, Game sG)
        {
            if(sG.Id == 1) // LAUCNCH MATCH
            {
                wndMatch wnd = new wndMatch(sP); // passing chosen player into the next window
                wnd.Show();
                this.Close();
            }
            else if(sG.Id == 2)// LAUNCH GOFISH
            {
                wndGoFish wnd = new wndGoFish(sP);
                wnd.Show();
                this.Close();
            }
            else // LAUNCH FORTUNE
            {
                bool hasRecord = false;                 // | NOTE: Using hasRecord to test whether the sP's last sG's  date is the   |
                var record = from s in db.Stats         // |             same as today's date                                        |
                             where (s.PlayerId == sP.Id) && (s.GameId == 3)
                             select s;

                foreach(Stats s in record.ToList())
                {
                    if (s.LastGame.ToString().Substring(0, 11) == date)
                        hasRecord = true;
                }

                if (!hasRecord) // If a Fortune game hasn't been played today, launch game
                {
                    wndFortune wnd = new wndFortune(sP);
                    wnd.Show();
                    this.Close();
                }
                else // Otherwise, tell the user 'no'
                    MessageBox.Show("It is unwise to bother the Univers too often . . .", "Don't Tempt Fate");
            }// end nested if/else block
        }// end LaunchGame()
    }// end MainWindow class
}// end ProjectGameInterface Namespace
