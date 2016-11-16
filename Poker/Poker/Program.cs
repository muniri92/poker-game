using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poker
{
    class Program
    {
        static void Main(string[] args)
        {

            // Grab the hand a user has given
            Card[] hand = GetHand(args);
            Card[] aiHand = GetHand(args);

            runHand(hand);
            runHand(aiHand);

            // Sort the hand by 'rank'
            Array.Sort(hand);

            // Possible choices from the highest possible hand to the lowest
            Console.Read();

        }

        private static void runHand(Card[] hand)
        {
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
        }

        // Get the current hand to play the game

        static Card[] GetHand(string[] args)
        {
            Card[] hand = new Card[5];
            int index = 0;

            if (args != null)
            {
                // Initialize new hand using the given input
                foreach (string a in args)
                {
                    if (index >= 5)
                        break;
                    Card c = new Card(a);
                    hand[index++] = c;
                    Console.Write(a);
                }
            }


            // Initialize new hand using the Deal method 

            while (index < 5)
            {
                hand[index++] = Deal();
            }

            return hand;
        }

        /*/
        ///  CREATE DECK
        /*/
        // Initialize an empty deck
        static Card[] deck = null;

        static char[] suitTypes = { 'c', 'h', 's', 'd' };

        // Keep track of the card that have been dealed
        static int dealIndex = 0;

        // Method that deals a random card 
        static Card Deal()
        {
            // Check if the deck exists or if it is empty
            if (deck == null || dealIndex >= 52)
            {
                // Initalize an empty array to create deck
                Random r = new Random();
                deck = new Card[52];
                int index = 0;

                // A double loop that will create all 52 cards 
                foreach (char value in suitTypes)
                {
                    for (int i = 2; i < 15; i++)
                    {
                        deck[index] = new Card() { suit = value, rank = i };
                        index = index + 1;
                    }
                }

                // Shuffle the deck (Thank you Lyrana for this chunk of code)
                for (int t = 0; t < deck.Length; t++)
                {
                    Card tmp = deck[t];
                    int next = r.Next(t, deck.Length);
                    deck[t] = deck[next];
                    deck[next] = tmp;
                }

            }
            if (dealIndex >= 52)
            {
                dealIndex = 0;
                Deal();
            }
            return deck[dealIndex++];
        }

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
                else if (item.Value == 1 && topVal == 0)
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
    }
}
