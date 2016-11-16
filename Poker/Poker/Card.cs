using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Card : IComparable
    {

        // Types of Suits (used for iteration to create a new deck)

        //public enum suitTypes
        //{
        //    c,
        //    h,
        //    d,
        //    s
        //}

        // Declare the kind of 'suit'
        public char suit; // C or H or D or S

        // Declare the 'rank'
        public int rank;  // 2=2 .. K=13, A=14

        // Incase I have to create a Deck
        public Card()
        {

        }

        // Method that gives value {'rank': ?, 'suit': ?} to a given card 
        public Card(string str)
        {
            str = str.ToUpper();
            foreach (char c in str)
            {
                switch (c)
                {
                    case 'C':
                    case 'D':
                    case 'H':
                    case 'S':
                        suit = c;
                        break;
                    case 'T':
                        rank = 10;
                        break;
                    case 'J':
                        rank = 11;
                        break;
                    case 'Q':
                        rank = 12;
                        break;
                    case 'K':
                        rank = 13;
                        break;
                    case 'A':
                        rank = 14;
                        break;
                    case '2':
                    case '3':
                    case '4':
                    case '5':
                    case '6':
                    case '7':
                    case '8':
                    case '9':
                        rank = c - '0';
                        break;
                }
            }

            // Tell user they forgot to add either a suit or a rank when giving input
            if (rank == 0)
                Console.WriteLine("Hey, you forgot the rank from " + str);
            if (suit == 0)
                Console.WriteLine("Hey, you forgot the suit from " + str);

        }

        public int CompareTo(object obj)
        {
            Card c = obj as Card;
            return rank - c.rank;
        }
    }
}
