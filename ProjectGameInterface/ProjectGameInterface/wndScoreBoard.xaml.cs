/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 23/04/21
GitHub Link: https://github.com/AbigailHerron/Project/blob/main/ProjectGameInterface/ProjectGameInterface/wndScoreBoard.xaml.cs

Description:  Is the ScoreBoard Window logic 
Properties: db
Constructors: Default
EventBased Methods: tabLeaderBoard_GotFocus, tabMatch_GotFocus, tabGoFish_GotFocus, tabFortune_GotFocus, btnReturn_Click

NOTES: This was the easy part -> This window's purpose is to display score data pulled from the database
###########################################################################################################################################################################*/
using System.Linq;
using System.Windows;

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for wndScoreBoard.xaml
    /// </summary>
    ///

    public partial class wndScoreBoard : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container(); // linking windo to Database


        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        public wndScoreBoard()
        {
            InitializeComponent();
        }



        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/
        /*Method: tabLeaderBoard_GotFocus()
                  1) Executes when Window is loaded as well
                  2) Sets the tblkCurrentLeader.Text to something appropriate 
                  3) Queries the Stats table for 3 different sets of values:
                           Match Results, GoFish Results and Fortune Results
                  4) Places the top results into their respective DataGrids */
        private void tabLeaderBoard_GotFocus(object sender, RoutedEventArgs e)
        {
            // Query 1 (Match Results)
            var matchQ = from s in db.Stats
                         join p in db.Players
                         on s.PlayerId equals p.Id
                         join g in db.Games
                         on s.GameId equals g.Id
                         where (s.GameId == 1) && (s.Wins + s.Draws + s.Losses != 0)
                         orderby (s.Wins - s.Losses) descending, s.Losses ascending// Best score first, then least losses
                         select new
                         {
                             Name = p.PlayerName,
                             Game = g.GameType,
                             Overall_Score = s.Wins - s.Losses,
                             Games_Played = s.Wins + s.Draws + s.Losses
                         };

            // Query 2 (GoFish Results)
            var goFishQ = from s in db.Stats
                          join p in db.Players
                          on s.PlayerId equals p.Id
                          join g in db.Games
                          on s.GameId equals g.Id
                          where (s.GameId == 2) && (s.Wins + s.Draws + s.Losses != 0)
                          orderby (s.Wins - s.Losses) descending, s.Losses ascending
                          select new
                          {
                              Name = p.PlayerName,
                              Game = g.GameType,
                              Overall_Score = s.Wins - s.Losses,
                              Games_Played = s.Wins + s.Draws + s.Losses
                          };

            // Query 3 (Fortune Results)
            var fortuneQ = from s in db.Stats
                           join p in db.Players
                           on s.PlayerId equals p.Id
                           join g in db.Games
                           on s.GameId equals g.Id
                           where (s.GameId == 3)
                           orderby s.LastScore descending // Ordering by best luck
                           select new
                           {
                               Name = p.PlayerName,
                               Game = g.GameType,
                               Date_Of_Last_Reading = s.LastGame.ToString().Substring(0, 11),
                               Luck_Result = s.LastScore
                           };
            // Updating the Current Leader box at the bottome
            tblkCurrentLeader.Text = "All Of The Above!";

            // pushing results to respected DataGrids
            /*NOTE: Got the .Take(1) idea from here: https://stackoverflow.com/questions/16451339/get-first-item-in-list-from-linq-query/16451365 */
            dgMatchLeader.ItemsSource = matchQ.ToList().Take(1);
            dgGoFishLeader.ItemsSource = goFishQ.ToList().Take(1);
            dgFortuneLeader.ItemsSource = fortuneQ.ToList().Take(1);
        }// end tabLeaderBoard_GotFocus()



        /*Method: tabMatch_GotFocus()
                  1) Queries database for list of all Match Scores
                  2) Orders results based on best overall percentage
                  3) Passes results to DataGrid dgMatch
                  4) Changes tblkCurrentLeader text to the name of the current best player */
        private void tabMatch_GotFocus(object sender, RoutedEventArgs e)
        {
            var query = from s in db.Stats
                        join p in db.Players
                        on s.PlayerId equals p.Id
                        join g in db.Games
                        on s.GameId equals g.Id

                        // Can't be on the board if you haven't played any games! (also can't divide by 0)
                        where (s.GameId == 1) && (s.Wins + s.Draws + s.Losses != 0)
                        orderby (s.Wins - s.Losses) descending, s.Losses ascending
                        select new
                        {
                            Name = p.PlayerName,
                            Overall_Score = s.Wins - s.Losses,
                            Games_Won = s.Wins,
                            Draws = s.Draws,
                            Games_Lost = s.Losses,
                            Games_Played = s.Wins + s.Draws + s.Losses,
                            Last_Game_Date = s.LastGame.ToString().Substring(0, 11),
                            Last_Game_Score = s.LastScore
                        };

            var results = query.ToList();
            dgMatch.ItemsSource = results;
            tblkCurrentLeader.Text = results.ElementAt(0).Name;
        }// end tabMatch_GotFocus()


        /*Method: tabGoFish_GotFocus()
                  1) Queries database for list of all GoFish Scores
                  2) Orders results based on best overall percentage
                  3) Passes results to DataGrid dgGoFish
                  4) Changes tblkCurrentLeader text to the name of the current best player */
        private void tabGoFish_GotFocus(object sender, RoutedEventArgs e)
        {
            var query = from s in db.Stats
                        join p in db.Players
                        on s.PlayerId equals p.Id
                        join g in db.Games
                        on s.GameId equals g.Id

                        // Can't be on the board if you haven't played any games! (also can't divide by 0)
                        where (s.GameId == 2) && (s.Wins + s.Draws + s.Losses != 0)
                        orderby (s.Wins - s.Losses) descending, s.Losses ascending 
                        select new
                        {
                            Name = p.PlayerName,
                            Overall_Score = s.Wins - s.Losses,
                            Games_Won = s.Wins,
                            Draws = s.Draws,
                            Games_Lost = s.Losses,
                            Games_Played = s.Wins + s.Draws + s.Losses,
                            Last_Game_Date = s.LastGame.ToString().Substring(0, 11),
                            Last_Game_Score = s.LastScore
                        };

            var results = query.ToList();
            dgGoFish.ItemsSource = results;
            tblkCurrentLeader.Text = results.ElementAt(0).Name;
        }// end tabGoFish_GotFocus()


        /*Method: tabFortune_GotFocus()
                  1) Queries database for list of all current Fortune results
                  2) Orders results based on best current luck
                  3) Passes results to DataGrid dgFortune
                  4) Changes tblkCurrentLeader text to the name of the current luckiest player */
        private void tabFortune_GotFocus(object sender, RoutedEventArgs e)
        {
            var query = from s in db.Stats
                        join p in db.Players
                        on s.PlayerId equals p.Id
                        join g in db.Games
                        on s.GameId equals g.Id

                        where (s.GameId == 3)
                        orderby s.LastScore descending // Ordering by best luck
                        select new
                        {
                            Name = p.PlayerName,
                            Date_Of_Last_Reading = s.LastGame.ToString().Substring(0, 11),
                            Luck_Result = s.LastScore
                        };

            var results = query.ToList();
            dgFortune.ItemsSource = results;
            tblkCurrentLeader.Text = results.ElementAt(0).Name;
        }// end tabFortune_GotFocus() 


        /*Method: btnReturn_Click()
                  1) Creates new MainWindow wndMain
                  2) Closes opens new window
                  1) Closes wndSocreBoard window */
        private void btnReturn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Show();
            this.Close();
        }// end btnReturn_Click()
    }// end wndScoreBoard class
}// end ProjectGameInterface namespace
