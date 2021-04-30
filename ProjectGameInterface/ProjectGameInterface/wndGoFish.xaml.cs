/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 25/04/21
GitHub Link:

Description:  Is the GoFish Window logic for the GoFish Game
Properties: db, session, imgs
Constructors: Default, Player
EventBased Methods: 
Logic Methods: 

NOTES: Did not get around to making this work
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectGameInterface
{
    /// <summary>
    /// Interaction logic for wndGoFish.xaml
    /// </summary>
    public partial class wndGoFish : Window
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        Model1Container db = new Model1Container();
        GoFish session = new GoFish();
        List<Image> imgs = new List<Image>();


        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        public wndGoFish()
        {
            InitializeComponent();
        }// end Default constructor


        /*Constructor: Player
                       1) Initializes Component
                       2) Adds Player to session Player list */
        public wndGoFish(Player p)
        {
            InitializeComponent();
            this.session.Players.Add(p);
        }// end Player constructor



        /*EVENT BASED METHODS ---------------------------------------------------------------------------------------------*/



        /*LOGIC BASED METHODS ---------------------------------------------------------------------------------------------*/

    }// end wndGoFish class
}// end ProjectGameInterface namespace
