using System;
using System.Collections;
using System.Collections.Generic;
using FoolGame.Properties;

namespace FoolGame
{
    public static class DeckOfCard
    {
        private static Stack<Card> deckOfCard = null;

        private static List<Card> listOfCards = new List<Card>
        {
            new Card(Suit.clubs, 6),
            new Card(Suit.clubs, 7),
            new Card(Suit.clubs, 8),
            new Card(Suit.clubs, 9),
            new Card(Suit.clubs, 10),
            new Card(Suit.clubs, 11),
            new Card(Suit.clubs, 12),
            new Card(Suit.clubs, 13),
            new Card(Suit.clubs, 14),

            new Card(Suit.diamonds, 6),
            new Card(Suit.diamonds, 7),
            new Card(Suit.diamonds, 8),
            new Card(Suit.diamonds, 9),
            new Card(Suit.diamonds, 10),
            new Card(Suit.diamonds, 11),
            new Card(Suit.diamonds, 12),
            new Card(Suit.diamonds, 13),
            new Card(Suit.diamonds, 14),

            new Card(Suit.hearts, 6),
            new Card(Suit.hearts, 7),
            new Card(Suit.hearts, 8),
            new Card(Suit.hearts, 9),
            new Card(Suit.hearts, 10),
            new Card(Suit.hearts, 11),
            new Card(Suit.hearts, 12),
            new Card(Suit.hearts, 13),
            new Card(Suit.hearts, 14),

            new Card(Suit.spades, 6),
            new Card(Suit.spades, 7),
            new Card(Suit.spades, 8),
            new Card(Suit.spades, 9),
            new Card(Suit.spades, 10),
            new Card(Suit.spades, 11),
            new Card(Suit.spades, 12),
            new Card(Suit.spades, 13),
            new Card(Suit.spades, 14),
        };

        public static Stack<Card> GetDeckOfCard()
        {
            if (deckOfCard == null)
            {
                deckOfCard = new Stack<Card>(listOfCards);
            }

            return deckOfCard;
        }


        public static void ShuffleDeck()
        {
            int currentCard;
            Card tempCard;
            Random random = new Random();
            
            for (int i = 0; i < 36; i++)
            {
                currentCard = random.Next(36);
                tempCard = listOfCards[i];
                listOfCards[i] = listOfCards[currentCard];
                listOfCards[currentCard] = tempCard;
            }

            deckOfCard = new Stack<Card>(listOfCards);
        }

        public static Card GetCardFromDeck()
        {
            if (deckOfCard.Count == 0)
            {
                throw new Exception();
            }

            Card card = deckOfCard.Peek();
            deckOfCard.Pop();
            return card;
        }
        
        // Этот метод не до конца работает. Я вытаскиваю из стека карту, добавляю ее в словарь, а потом удаляю из стека (из словаря тоже) 
        // Надо бы копировать карту из стека в словарь.
        public static Dictionary<Card, Card> GetCardsFromDeck(int count)
        {
            Dictionary<Card, Card> cards = new Dictionary<Card, Card>();
            for (int i = 0; i < count; i++)
            {
                if (deckOfCard.Count == 0)
                {
                    return cards;
                }
                cards.Add(deckOfCard.Peek(), deckOfCard.Peek());
                deckOfCard.Pop();
            }

            return cards;
        }
    }
}