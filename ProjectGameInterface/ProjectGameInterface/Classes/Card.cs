/*###########################################################################################################################################################################
Name: Abigail Herron
ID: S00200536
Date: 12/03/21
GitHub Link:

Description: Defines a Card-like object
Properties: Rank, Suit, Image, Point, Position
Constructors: Default
Methods: ToString (override), CompareRank, CompareImg

NOTES: This was one of the few files that wasn't corrupted
       Had originally intended to use the IComparable to compare attributes, but considering all of the comparison
           methods are performed on string variables, it was difficult to correctly overload the CompareTo() method
###########################################################################################################################################################################*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameInterface
{
    public class Card
    {
        /*PROPERTIES ------------------------------------------------------------------------------------------------------*/
        public string Rank { get; set; }
        public string Suit { get; set; }
        public string Image { get; set; }
        public int Point { get; set; }
        public bool Position { get; set; }  /* | Note: Position is relavant for the TarotDeck as the card looks          |
                                               |       different upside-down than upright. The PlayingDeck cards         |
                                               |       look the same either way, so it won't be referenced for that deck | */

        /*METHODS ---------------------------------------------------------------------------------------------------------*/
        /*Method: ToString()
                  1) Overrides original ToString()
                  2) Returns the current object's Rank, Suit and Point values 
                  3) This is merely to test that the card class is working correctly
                     and will most likely be removed at a later date */
        public override string ToString()
        {
            return $"Card dealt is the {this.Rank} of {this.Suit}, value {this.Point}";
        }// end ToString()


        /*Method: CompareRank()
                  1) Takes in a Card object
                  2) Checks whether the current card's Rank is the same as
                     that card's Rank
                  3) Returns true if Rank is the same, and false if it is not */
        public bool CompareRank(Card that)
        {
            if (this.Rank == that.Rank)
                return true;
            else
                return false;
        }// end CompareRank()



        /*Method: CompareImg()
                  1) Takes in a Card object
                  2) Checks whether the current card's Image is the same as
                     that card's Image
                  3) Returns true if Image is the same, and false if it is not */
        public bool CompareImg(Card that)
        {
            if (this.Image == that.Image)
                return true;
            else
                return false;
        }// end CompareImg()
    }// end Card Class
}// end ProjectGameInterface namespace
