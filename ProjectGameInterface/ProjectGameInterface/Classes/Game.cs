/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 17/03/21
GitHub Link:

Document Description: Defines a parent, partial class Game and all sub-classes
                      The properties Id and GameType are defined in the auto-generated Game class
                      contained under the Model1.edmx path

         Parent-Class: Game
         Properties: GameDeck, Players, DateToday
         Constructors: Default
         Methods: Deal, ResetGame

         Sub-Calss: Match
         Additional Properties: LivesLeft, CardCount, First
         Constructors: Default
         Additional Methods: ResetGame (override)

         Sub-Calss: GoFish
         Constructors: Default
         Additional Methods: Deal (override), ResetGame (override)


         Sub-Calss: Fortune
         Additional Methods: Spread
         Constructors: Default
         Additional Methods: Deal (override), GetPrediction, ToString(override)

NOTE: Was able to find half of this file in an earlier version I saved accidentally.
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProjectGameInterface
{
    public partial class Game
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public Deck GameDeck { get; set; }
        public List<Player> Players { get; set; }
        public DateTime DateToday { get; set; }

        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method:  Deal()
                   1) To be overritten by sub-classes */
        public virtual void Deal() { }
        
        
        /*Method: ResetGame()
                  1) To be overritten by sub-classes */
        public virtual void ResetGame() { } // end ResetGame
    }// end partial Game class


    /*==================================================MATCH CLASS========================================================*/
    public class Match : Game
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public int LivesLeft { get; set; } // When this reaches 0, its game over
        public int CardCount { get; set; } // This is for keeping count of highlighted cards (should only reach to 2)
        public Card First { get; set; } // This is for comparing cards, will be using the .CompareImg of the Card class

        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, shuffled MatchDeck object
                       2) Sets LivesLeft to 3 
                       3) Sets CardCount to 0
                       4) Initialises First as a null Card object */
        public Match()
        {
            this.Id = 1;
            this.GameType = "Match";
            this.GameDeck = new MatchDeck();
            GameDeck.Shuffle();
            this.LivesLeft = 3;
            this.CardCount = 0;
            this.First = null;
            this.DateToday = DateTime.Today;

            // NOTE: DON'T FORGET TO DO THIS
            // can pass the selected Player to this list when creating the game window
            this.Players = new List<Player>();
        }// end Default constructor


        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: ResetGame() (override)
                  1) Sets LivesLeft back to 3
                  2) Sets CardCount back to 0
                  3) Re-Shuffles GameDeck 
                  4) Sets Card object First to null */
        public override void ResetGame()
        {
            this.LivesLeft = 3;
            this.CardCount = 0; // just in case the user resets after highlighting a card
            this.GameDeck.Shuffle();
            this.First = null; // flushing out possible previous First card data
        }// end ResetGame
    }// end Match class



    /*=================================================GO-FISH CLASS=======================================================*/
    public class GoFish : Game
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        

        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, shuffled PlayingDeck object
                       2) Adds Player and Bot to List<Player> */
        public GoFish()
        {
            this.Id = 2;
            this.GameType = "GoFish";
            this.GameDeck = new PlayingDeck();
            GameDeck.Shuffle();
            this.DateToday = DateTime.Today;

            // can pass the selected Player to this list when creating the game window
            this.Players = new List<Player>();
            Player bot = new Player("Botty"); // adding a Bot to play against for now
            Players.Add(bot);
        }// end Default constructor



        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: Deal() (override)
                  1) Places 7 Card objects from the GameDeck into the Hand property of
                     each Player in the List Players
                  2) Removes Cards from GameDeck as they are dealt into Player Hands */
        public override void Deal()
        {
            foreach (Player p in Players)
            {
                while (p.Hand.Count < 7) // each hand starts with 7 cards
                {
                    p.Hand.Add(GameDeck.Pack.ElementAt(0));
                    GameDeck.Pack.RemoveAt(0); // card can't be in the deck and in a hand or pair
                }
            }
        }// end Deal()



        /*Method: ResetGame() (override)
                  1) Takes all the cards from each player's hand and pair pile
                           and puts them back into the deck 
                  2) Shuffles the restored deck
                  3) Deals out a fresh game */
        public override void ResetGame()
        {
            // taking back all cards
            foreach (Player p in Players)
            {
                // returning all cards in each player's hand to the pack
                foreach (Card c in p.Hand)
                {
                    this.GameDeck.Pack.Add(c);
                    p.Hand.Remove(c);
                }
                // returning all cards in each player's pair pile to the pack
                foreach (Card c in p.Pairs)
                {
                    this.GameDeck.Pack.Add(c);
                    p.Pairs.Remove(c);
                }
            }// end nested foreach block

            this.GameDeck.Shuffle(); // shuffling the restored pack
            Deal(); // dealing out a fresh game
        }// end ResetGame()



        /*Method: FillHand()
                  1) Takes in a Player object
                  2) Checks whether there are 5 cards left in the deck or not
                  3) Fills the player's hand with cards
                  4) Removes each card from the deck */
        public void FillHand(Player p)
        {
            // if there are still 5 cards left in the pack
            if (this.GameDeck.Pack.Count() >= 5)
            {
                while (p.Hand.Count() < 5)
                { //add 5 cards to this player's hand
                    p.Hand.Add(GameDeck.Pack.ElementAt(0));
                    GameDeck.Pack.RemoveAt(0);
                }
            }
            else // if there are less than 5 cards left in the pack
            {
                foreach(Card c in this.GameDeck.Pack)
                { // add whatever is left to player's hand
                    p.Hand.Add(c);
                    this.GameDeck.Pack.Remove(c);
                }
            }
        }// end FillHand()
    }// end GoFish class




    /*=================================================FORTUNE CLASS=======================================================*/
    public class Fortune : Game
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public List<Card> Spread { get; set; }

        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, shuffled PlayingDeck object */
        public Fortune()
        {
            this.Id = 3;
            this.GameType = "Fortune";
            this.GameDeck = new TarotDeck();
            GameDeck.Shuffle();
            this.Players = new List<Player>();
            this.Spread = new List<Card>();
            this.DateToday = DateTime.Today;
        }// end Default constructor



        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: Deal() (overried) 
                  1) Grabs the top 3 cards from the Game's deck and places them
                           into the List Spread */
        public override void Deal()
        {
            for(int i = 0; i < 3; i++)
            {
                this.Spread.Add(this.GameDeck.Pack.ElementAt(i));
            }
        }// end Deal()



        /*Method: GetPrediction()
                  1) Takes in a list of Card object 
                  2) Uses a string array to itterate through prediction file paths
                  2) Creates new FileStream and StreamReader objects to treat prediction files as CSV files
                  3) Takes each new temp string array and tests the [0] value for a match with each Card objects
                          Rank and Suit combination
                  4) When a match is found, tests whether the Card object's position is true or false and
                          adds the appropriate temp[] value to the string prediction[] array 
                  5) Returns the string prediction[] array */
        /*NOTE: Got part of the code from - https://codereview.stackexchange.com/questions/61973/csv-reader-using-streamreader-and-linq */
        public string[] GetPrecition(List<Card> spread)
        {
            // declaring string array to be returned
            string[] prediction = new string[3];

            // declaring file paths
            string[] filePaths = new string[] { "TextFiles/RecentPast.txt", "TextFiles/Present.txt",
                                                "TextFiles/NearFuture.txt" };
            FileStream fs; // creating new FileStream object

            for (int i = 0; i < filePaths.Length; i++) // i value is important
            {
                fs = new FileStream(filePaths[i], FileMode.Open, FileAccess.Read);
                using (StreamReader sr = new StreamReader(fs, System.Text.Encoding.ASCII, true, 1024))
                {
                    string thisLine;
                    while ((thisLine = sr.ReadLine()) != null) // itterating through each line of the file
                    {
                        string[] temp = thisLine.Split(new char[] { ',' }, StringSplitOptions.None); // turning each file line to a string array
                        if (temp[0] == (spread[i].Rank + spread[i].Suit))
                        {
                            if (spread[i].Position) // the .Position Card property returns a bool value
                                prediction[i] = temp[1]; // value if true
                            else
                                prediction[i] = temp[2]; // value if false
                        }// end nested if block
                    }// end while block
                }// end using block (sr)
                fs.Close(); // closing the FileStream now that it's no longer needed
            }// end for block
            return prediction; // returning the string array
        }// end GetPrediction()



        /*Method: ToString() (overloaded)
                  1) Overloads the original ToString() method
                  2) Takes in a Card object
                  3) Returns a string based on the card's Suit value */
        public string ToString(Card c)
        {
            if (c.Suit == "major")
                return $"The {c.Rank}";
            else
                return $"The {c.Rank} of {c.Suit}";
        }// end ToString()
    }// end Fortune class
}// ened ProjectGameInterface namespace
