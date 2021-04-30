/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 23/04/21
GitHub Link: https://github.com/AbigailHerron/Project/blob/main/ProjectGameInterface/ProjectGameInterface/wndAddPlayer.xaml.cs

Description:  Is the AddPlayer window logic for the Game Interface
Properties: db, selected
EventBased Methods: btnConfirmAddPlayer_Click, btnCancleAddPlayer_Click
###########################################################################################################################################################################*/
using System;
using System.Windows;

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for wndAddPlayer.xaml
    /// </summary>
    public partial class wndAddPlayer : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container(); // linking window to Database
        Player selected;
        


        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Initializes Component */
        public wndAddPlayer()
        {
            InitializeComponent();
        }// end Default constructor




        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: btnConfirmAddPlayer_Click()
                  1) Checks whether the texbox has text or not
                  2) Creates a new Player object with a Name entered in tbxNewPlayerName
                  3) Attempts to add this object to the database
                  4) Opens a message box stating if a) there is no text in the textbox
                           and b) if the Player was successfully added to the Database */
        private void btnConfirmAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            if((tbxNewPlayerName.Text == null) || (tbxNewPlayerName.Text == ""))
            {
                MessageBox.Show("You can't have a blank name!", "No Name Detected", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }// end if/else block
            else
            {
                // NOTE: The constructor that takes a string is found in the partial Player class
                //       inside the 'Classes' folder
                Player p = new Player(tbxNewPlayerName.Text);  // new Player object created

                // Attempting to update DataSet Players of the Database
                try
                {
                    db.Players.Add(p);
                    db.SaveChanges(); // saving changes to Database
                    // Notifying User that the Player was added
                    MessageBox.Show($"New Player {tbxNewPlayerName.Text} Added!", "Success");
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.ToString(), "Database Error");
                }// end try/catch block
            }
            this.Close(); // Closing wndAddPlayer window
        }// end btnConfirmAddPlayer_Click()




        /*Method: btnCancleAddPlayer_Click()
                  1) Creates a Player object
                  2) Queries Database for player with the same PlayerID as the
                             Player passed into the window
                  2) Assigns this p to new Player object and attempts to remove
                             Player from Database 
                  3) Opens a message box stating if a) there was an unknown Database
                           error and b) if the Player was successfully removed */
        private void btnCancleAddPlayer_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }// end btnCancleAddPlayer_Click()
    }// end wndAddPlayer class
}// end ProjectGameInterface namespace
