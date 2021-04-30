/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 15/03/21
GitHub Link: https://github.com/AbigailHerron/Project/blob/main/ProjectGameInterface/ProjectGameInterface/Classes/Player.cs

Document Description: Defines a partial class Player
                      The properties Id and PlayerName are defined in the auto-generated Player class
                      contained under the Model1.edmx path

Properties: Hand, Pairs
Constructors: PlayerName

NOTE: Was able to retrieve this from corrupted project as is
###########################################################################################################################################################################*/
using System.Collections.Generic;

namespace ProjectGameInterface
{
    public partial class Player
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public List<Card> Hand { get; set; }
        public List<Card> Pairs { get; set; }

        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: PlayerName
                       1) Initialises a Player object with
                          the PlayerName name */
        public Player(string name)
        {
            this.PlayerName = name;
            this.Hand = new List<Card>();
            this.Pairs = new List<Card>();
        }// end PlayerName constructor

        /*METHODS ---------------------------------------------------------------------------------------------------------*/

    }// end Player class
}// end ProjectGameInterface Namespace
