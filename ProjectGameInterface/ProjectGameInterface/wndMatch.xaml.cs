/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 24/04/21
GitHub Link:

Description:  Is the Match Window logic for the Match Game
Properties: db, session, imgs, map, path
Constructors: Default, Player
EventBased Methods: Window_Loaded, btnMenuRtn_Click, btnQuit_Click
Logic Methods: 

NOTES: Trying to get the images to be selectable and dynamically alter their sources was far more challenging than
       I thought it would be -> Did not manage to finish this
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for wndMatch.xaml
    /// </summary>
    public partial class wndMatch : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container();
        Match session = new Match(); // should have all variables of game necessary contained within
        List<Image> imgs = new List<Image>(); // should gather all images loaded into window
        BitmapImage map;
        Uri path;
        string ImageSourcePath { get; set; }

        // Shortcut properties
        public string deckImg;
        public string cardImg;

        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        public wndMatch()
        {
            InitializeComponent();
        }// end Default constructor


        /*Constructor: Player
                       1) Initializes Component
                       2) Populates deckImg shortcut variable
                       3) Adds Player to session Player list */
        public wndMatch(Player p)
        {
            InitializeComponent();
            this.deckImg = session.GameDeck.DeckImg;
            this.session.Players.Add(p);
        }// end of Player constructor

        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: Window_Loaded()
                  1) Updates the TextBlock tblkLivesLeft with livesLeft variable */
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            tblkLivesLeft.Text = session.LivesLeft.ToString();
            deckImg = session.GameDeck.DeckImg; // Creating shortcut to path snippet

            Image temp = new Image(); // Creating new temp Image
            for (int i = 0; i < session.GameDeck.Pack.Count(); i++)
            {
                imgs.Add(temp); // Filling second list with images (will be a 1 to 1 with the game's GameDeck.Pack List)
            }

            lbxMatchCards.ItemsSource = imgs;
        }// end Window_Loaded()



        /*Method: lbxMatchCards_SelectionChanged()
                   1) Increments the Match.CardCount variable
                   2) Updates lbxMatchCards.ItemsSource
                   3) Current item's image is updated whether CardCount is <= 2 
                   4) Calls IsMatch() method */
        private void lbxMatchCards_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }// end lbxMatchCards_SelectionChanged()



        /*Method: btnMenuRtn_Click()
                  1) Creates a new MainWindow object
                  2) Opens new wnd object
                  3) Closes this window without warning (too many MessageBoxes can be tedious) */
        private void btnMenuRtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            this.Close();
        }// end btnMenuRtn_Click()



        /*Method: btnQuit_Click()
                  1) Asks the user if they'd like to quit the program (via MessageBox)
                  2) If Yes is selected, the window will close and the program will end
                  3) If No is selected, nothing happens */
        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show($"Do you want to quit?", "Leave Game",
                                    MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                this.Close();
        }// end btnQuit_Click()

        
    }// end wndMatch class
}// end ProjectGameInterface namespace

/* NOTE: Took this from here -> https://stackoverflow.com/questions/350027/setting-wpf-image-source-in-code */
//BitmapImage src = new BitmapImage(new Uri(@"/ProjectGameInterface;component/" + deckImgPath, UriKind.Relative));
//temp.Source = src;
