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

            // Sort the hand by 'rank'
            Array.Sort(hand);
            Array.Sort(aiHand);

            Dictionary<string, int> playerOneDict = helperDict(hand);
            Dictionary<string, int> playerTwoDict = helperDict(aiHand);

            runHand(playerOneDict, hand);
            runHand(playerTwoDict, hand);
            
            // Possible choices from the highest possible hand to the lowest
            Console.Read();

        }

        private static void runHand(Dictionary<string, int> dict, Card[] hand)
        {
            int val;
            if (IsStraightFlush(hand))
                Console.WriteLine("STRAIGHT FLUSH");

            else if (dict.ContainsKey("fourKind"))
                Console.WriteLine("FOUR OF A KIND");

            else if (dict.ContainsKey("twoKind") && dict.ContainsKey("threeKind"))
                Console.WriteLine("FULL HOUSE");

            else if (IsFlush(hand))
                Console.WriteLine("FLUSH");

            else if (IsStraight(hand))
                Console.WriteLine("STRAIGHT");

            else if (dict.ContainsKey("threeKind"))
                Console.WriteLine("THREE OF A KIND");

            else if (dict.ContainsKey("twoPair") && dict["twoPair"] == 2)
                Console.WriteLine("TWO PAIRS");

            else if (dict.ContainsKey("twoKind"))
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


        static Dictionary<string, int> helperDict(Card[] hand)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < hand.Length; i++)
            {
                if (dict.ContainsKey(hand[i].rank))
                    dict[hand[i].rank]++;
                else
                    dict[hand[i].rank] = 1;
            }


            /*
            * 
            * Dictionary < string, int>
            *   { highCard: Top rank of all cards in hand,
            *     fourKind: Four of a kind = [4]
            *     threeKind: Three of a kind = [3]
            *     twoKind: Two of a kind = [2]
            *     twoPair: Two pairs = [2]
            * 
            */

            Dictionary<string, int> player = new Dictionary<string, int>();
            int idx = 0;
            int highPair = 0;
            foreach (var value in dict)
            {
                if (value.Value == 4)
                {
                    player.Add("fourKind", 4);
                    player.Add("highCard", value.Key);
                    break;
                }

                if (value.Value == 3)
                {
                    player.Add("threeKind", 3);
                    player.Add("highCard", value.Key);
                }

                if (value.Value == 2)
                {
                    if (player.ContainsKey("twoKind"))
                    {
                        player["twoPairs"]++;
                        if (highPair < value.Key)
                            highPair = value.Key;
                    }
                    else
                    {
                        player.Add("twoPairs", 1);
                        player.Add("twoKind", 2);
                        highPair = value.Key;
                    }
                }

                if (value.Value == 1)
                {
                    if (idx >3)
                        player.Add("highCard", hand[0].rank);
                    idx++;
                }

            }
            return player;
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
