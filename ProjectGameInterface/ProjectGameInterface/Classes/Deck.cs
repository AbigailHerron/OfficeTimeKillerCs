/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 13/03/21
GitHub Link:

Document Description: Defines a parent class Deck and all sub-classes

         Parent-Class: Deck
         Properties: Pack, DeckImg
         Constructors: Default
         Methods: Shuffle

         Sub-Calss: MatchDeck
         Additional Properties: suits
         Constructors: Default

         Sub-Calss: PlayingDeck
         Additional Properties: suits, ranks
         Constructors: Default


         Sub-Calss: TarotDeck
         Additional Properties: majorArcana, minorSuits, minorRanks
         Constructors: Default
         Additional Methods: Shuffle (override)

NOTE: Thankfully, this file was also safe from corruption.
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectGameInterface
{
    public class Deck
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public List<Card> Pack { get; set; }
        public string DeckImg { get; set; }



        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: Shuffle
                  1) To be executed after CreateDeck has run
                  2) Re-orders the card objects in the list Pack */
        public virtual void Shuffle()
        {
            Random order = new Random();
            Card temp = new Card();
            int cardIndex;

            for (int i = 0; i < this.Pack.Count; i++)
            {
                // makes cardIndex a random number within the limited range of cards in the list Pack
                cardIndex = order.Next(this.Pack.Count);

                temp = this.Pack.ElementAt(i); // keeping the details of card object at i in the list
                this.Pack[i] = this.Pack.ElementAt(cardIndex); // makes the card at i now equal a random card in the Pack list
                // puts original card at i (temp) where the index of the random card is
                this.Pack[cardIndex] = temp; // dealing with duplicates
            }// end for block
        }// end Shuffle()
    }// end Deck abstract class




    /*=================================================MATCH-DECK CLASS====================================================*/
    public class MatchDeck : Deck
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public string[] suits = new string[] {"star", "triangle", "cirlce", "pentagon", "spoon", "glass", "music",
                                              "rabbit", "hat", "cheese", "leaf", "tv"};



        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, unshuffled MatchDeck object
                       2) Loops through suits array twice to create 24 cards, and places them
                          in the property Pack */
        public MatchDeck()
        {
            this.Pack = new List<Card>(); // initialising List to avoid problems
            this.DeckImg = "Images/mdb.png";

            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    Card c = new Card();
                    c.Suit = suits[i];
                    // NOTE: the Image property will be the same for 2 cards in the MatchDeck
                    c.Image = $"Images/MatchCards/{suits[i]}.png";
                    Pack.Add(c); // adding new card to deck
                }
            }// end nested for block
        }// end Default constructor
    }// end MatchDeck class




    /*================================================PLAYING-DECK CLASS===================================================*/
    public class PlayingDeck : Deck
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public string[] suits = new string[] { "spades", "diamonds", "clubs", "hearts" };
        public string[] ranks = new string[] { "Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Jack", "Queen", "King" };



        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, unshuffled PlayingDeck object
                       2) Loops through suits and ranks arrays to create 52 cards, and places them
                          in the property Pack */
        public PlayingDeck()
        {
            this.Pack = new List<Card>(); // initialising List to avoid problems
            this.DeckImg = @"ProjectGameInterface/Images/pdb.png";
            for (int i = 0; i < suits.Length; i++)
            {
                for (int j = 0; j < ranks.Length; j++)
                {
                    Card c = new Card();
                    c.Suit = suits[i];
                    c.Rank = ranks[j];
                    c.Image = $"Images/PlayingCards/{ranks[j]}{suits[i].Substring(0,1)}.png";

                    // assigning the default point value for each card
                    if (j <= 8)
                        c.Point = j + 1; // the point value for ace is set to 1 at this time
                    else
                        c.Point = 10;  // cards 10 to King will be worth 10 points

                    Pack.Add(c); // adding new card to deck
                }
            }// end nested for block
        }// end Default constructor
    }// end PlayingDeck sub-class




    /*=================================================TAROT-DECK CLASS====================================================*/
    public class TarotDeck : Deck
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public string[] majorArcana = new string[] {"Fool", "Magician", "High Priestess", "Empress", "Emperor",
                                                    "Hierophant", "Lovers","Chariot", "Justice", "Hermit",
                                                    "Wheel of Fortune", "Strength", "Hanged Man", "Death", "Temperance",
                                                    "Devil", "Tower", "Star", "Moon", "Sun", "Judgement", "World"};

        public string[] minorSuits = new string[] { "Wands", "Cups", "Swords", "Pentacles" };
        public string[] minorRanks = new string[] {"Ace", "2", "3", "4", "5", "6", "7", "8", "9", "10", "Page",
                                                   "Knight", "Queen", "King"};



        /*CONSTRUCTORS ----------------------------------------------------------------------------------------------------*/
        /*Constructor: Default
                       1) Creates a default, unshuffled TarotDeck object
                       2) Loops through majorArcana, minorSuits and minorRanks arrays to create 78 cards, and places them
                          in the property Pack */
        public TarotDeck()
        {
            this.Pack = new List<Card>(); // initialising Pack first to avoid problems
            this.DeckImg = "Images/tdb.png";

            // looping through all major cards and adding them to the Pack list
            for (int i = 0; i < majorArcana.Length; i++)
            {
                Card c = new Card();
                c.Suit = "major";
                c.Rank = majorArcana[i];
                c.Image = "Images/TarotCards/placeholder.png";

                // Note: from Ace to King in minor is 1 - 14, should start with Fool at 15
                c.Point = i + 15;
                c.Position = true; // this just means the card was pulled upright
                Pack.Add(c); // adding new card to deck
            }

            // looping through all minor cards and adding them to the Pack list
            for (int i = 0; i < minorSuits.Length; i++)
            {
                for (int j = 0; j < minorRanks.Length; j++)
                {
                    Card c = new Card();
                    c.Suit = minorSuits[i];
                    c.Rank = minorRanks[j];
                    c.Image = "Images/TarotCards/placeholder.png";

                    // assigning the default point value for each card
                    c.Point = j + 1; // start with Ace and make it worth 1
                    c.Position = true;
                    Pack.Add(c); // adding new card to deck
                }
            }// end nested for block
        }// end Default constructor



        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: Shuffle(override)
                  1) Takes the Shuffle() method from Deck class
                  2) Randomly changes the Position of each card ('flipping' random cards) 
                  3) Changes the Point value of any 'flipped' cards to a negative */
        public override void Shuffle()
        {
            base.Shuffle(); // re-ordering the deck as before
            Random flip = new Random();

            // each card in the Pack list has a random chance of being 'flipped'
            foreach (Card c in Pack)
            {
                if (flip.Next(1) != 0) // flip can either be 0 or 1
                {
                    // using if/else here in case the deck is shuffled more than once
                    if (c.Position == true)
                        c.Position = false;
                    else
                        c.Position = true; // make the card upside-down, essentially

                    c.Point = c.Point * (-1); // reverse the point value of the card when flipped
                }
            }// end loop
        }// end Shuffle()
    }// end TarotDeck sub-class
}// end ProjectGameInterface namespace