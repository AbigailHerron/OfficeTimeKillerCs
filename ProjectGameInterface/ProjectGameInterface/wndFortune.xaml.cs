/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 23/04/21
GitHub Link: https://github.com/AbigailHerron/Project/blob/main/ProjectGameInterface/ProjectGameInterface/wndFortune.xaml.cs

Description:  Is the Fortune Window logic for the Fortune Game
Properties: db, session, imgs, prediction, count, score
Constructors: Default, Player
EventBased Methods: btnGetRead_Click, btnMainRtn_Click
Logic Methods: UpdateText, UpdateDB

NOTES: I didn't manage to get the images loaded into the listbox, but succeeded in grabing the data from 
       the text files held in the /bin/Debug/TextFiles folder of this project
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for wndFortune.xaml
    /// </summary>
    public partial class wndFortune : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container();
        Fortune session = new Fortune();
        List<Image> imgs = new List<Image>();
        string[] prediction = new string[3];
        int count = 0; // need a counter to make sure the user can only have one reading per day
        int score = 0;



        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        public wndFortune()
        {
            InitializeComponent();
        }// end Default constructor


        /*Constructor: Player
                       1) Initializes Component
                       2) Adds Player to session Player list */
        public wndFortune(Player p)
        {
            InitializeComponent();
            this.session.Players.Add(p);
        }// end Player constructor




        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: btnGetRead_Click()
                  1) */
        private void btnGetRead_Click(object sender, RoutedEventArgs e)
        {
            session.Deal();  // Dealing out cards
            this.UpdateText(); // Updating window text blocks

            // Showing the user they're Luck score
            if (this.count == 0)
            {
                foreach (Card c in session.Spread)
                {
                    this.score += c.Point;
                }
                tblkLuckScore.Text = score.ToString();
                count++;
            }// end if block

            // Updating Database
            this.UpdateDB();
        }// end btnGetRead_Click()




        /*Method: btnMainRtn_Click()
                  1) Creates a new MainWindow object wnd
                  2) Opens wnd
                  3) Closes Fortune window */
        private void btnMainRtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            this.Close();
        }// end btnMainRtn_Click




        /*LOGIC BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: UpdateText()
                  1) itterates through the session's Spread list
                  2) Updates text block elements based on each Spread card's properties */
        public void UpdateText()
        {
            // Getting tarot card titles
            tblkPastCard.Text = session.ToString(session.Spread.ElementAt(0));
            tblkPresentCard.Text = session.ToString(session.Spread.ElementAt(1));
            tblkFutureCard.Text = session.ToString(session.Spread.ElementAt(2));

            // Getting prediction from text files
            prediction = session.GetPrecition(session.Spread);

            tblkPastPrediction.Text = "Recently, you have " + prediction[0].ToLower().Trim();
            tblkPresentPrediction.Text = "Today, you are " + prediction[1].ToLower().Trim();
            tblkFuturePrediction.Text = "Soon, you will " + prediction[2].ToLower().Trim();
        }// end UpdateText()



        /*Method: UpdateDB()
                  1) Queries the database for the exact row to update
                  2) Changes the LastScore and LastGame fields
                  3) Attempts to save changes to the database */
        public void UpdateDB()
        {
            Player temp = session.Players.ElementAt(0);

            // querying the database for the exact row to update
            var queryRow = from s in db.Stats
                           where (s.GameId == 3) && (s.PlayerId == temp.Id)
                           select s;

            foreach (Stats s in queryRow)  // updating the LastScore and LastGame fields
            {
                s.LastScore = this.score;
                s.LastGame = this.session.DateToday;
            }

            try
            {
                db.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }// end UpdateDB()

    }// end wndFortune class
}// end ProjectGameInterface namespace
