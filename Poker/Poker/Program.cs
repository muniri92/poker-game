﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Card : IComparable 
    {
        public char suit; // C or H or D or S
        public int rank;  // 2=2 .. K=13, A=14

        public Card()
        {

        }

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

    class Program
    {
        static void Main(string[] args)
        {

            Card[] hand = GetHand(args);
            Array.Sort(hand);

            if (IsStraightFlush(hand))
                Console.WriteLine("STRAIGHT FLUSH");

            else if (IsFourOfAKind(hand))
                Console.WriteLine("FOUR OF A KIND");

            else if (IsFullHouse(hand))
                Console.WriteLine("FULL HOUSE");

            else if (IsFlush(hand))
                Console.WriteLine("FLUSH");

            else if (IsStraight(hand))
                Console.WriteLine("STRAIGHT");

            else if (IsThreeOfAKind(hand))
                Console.WriteLine("THREE OF A KIND");

            else if (IsTwoPair(hand))
                Console.WriteLine("TWO PAIRS");

            else if (IsPair(hand))
                Console.WriteLine("PAIRS");

            else 
                Console.WriteLine("HIGH CARD");

            Console.Read();

        }

        static Card[] GetHand(string[] args)
        {
            Card[] hand = new Card[5];
            int index = 0;
            foreach (string a in args)
            {
                if (index >= 5)
                    break;
                Card c = new Card(a);
                hand[index++] = c;
            }

            /*while (index < 5)
            {
                hand[index++] = Deal();
            }
            */
            return hand;

            

        }
       /* 
        CREATE DECK

        static Card[] deck = null;
        static int dealIndex = 0;

        static Card Deal()
        {
            if (deck == null || dealIndex >= 52)
            {
                deck = new Card[52];
                int index = 0;
                // TODO: one of each card

                // shuffle (randomize)
            }

            return deck[deckIndex++];

        }
        */


        /*
         HELPER FUNCTION FOR EVERYTHING

         FULL HOUSE = 5
         Four of a Kind = 4
         Three of a Kind = 3
         Two Pairs = 8
         Pair = 2

        */
        static int IsOfAKind(Card[] hand)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < hand.Length; i++)
            {
                if (dict.ContainsKey(hand[i].rank))
                    dict[hand[i].rank]++;
                else
                    dict[hand[i].rank] = 1;
            }
            int topVal = 0;
            int twice = 0;
            bool fullTwo = false;
            bool fullThree = false;
            foreach (var item in dict)
            {
                if (item.Value == 2)
                {
                    fullTwo = true;
                    twice = twice + 4;
                    topVal = 2;
                }
                else if (item.Value == 3)
                {
                    fullThree = true;
                    topVal = 3;
                }
                else if (item.Value == 4)
                    topVal = 4;
                else if (item.Value == 1)
                    topVal = 1;
            }
            if (fullTwo && fullThree)
                return 5;
            else if (topVal == 4 || topVal == 3)
                return topVal;
            else if (twice == 8)
                return twice;
            return topVal;
        }

        // IS A FLUSH
        static bool IsFullHouse(Card[] hand)
        {
            if (IsOfAKind(hand) == 5)
                return true;
            return false;
        }

        // FOUR OF A KIND
        static bool IsFourOfAKind(Card[] hand)
        {
            if (IsOfAKind(hand) == 4)
                return true;
            return false;
        }

        // THREE OF A KIND
        static bool IsThreeOfAKind(Card[] hand)
        {
            if (IsOfAKind(hand) == 3)
                return true;
            return false;
        }

        // TWO OF A KIND
        static bool IsTwoPair(Card[] hand)
        {
            if (IsOfAKind(hand) == 8)
                return true;
            return false;
        }

        // TWO OF A KIND
        static bool IsPair(Card[] hand)
        {
            if (IsOfAKind(hand) == 2)
                return true;
            return false;
        }

        // IS A STRAIGHT FLUSH
        static bool IsStraightFlush(Card[] hand)
        {
            return IsFlush(hand) && IsStraight(hand);
        }

        // IS A FLUSH
        static bool IsFlush(Card[] hand)
        {
            for (int i = 1; i < hand.Length; i++)
            {
                if (hand[i].suit != hand[0].suit)
                    return false;
            }
            return true;
        }

        // IS A STRAIGHT
        static bool IsStraight(Card[] hand)
        {
            // Assuming cards in hand are sorted
            for (int i = 1; i < hand.Length; i++)
            {
                if (hand[i].rank != hand[i - 1].rank + 1)
                    return false;
            }
            return true;
        }

        //// HIGH CARD
        //static ... HighCard(Card[] hand)
        //{
        //    return ;
        //}
    }
}
