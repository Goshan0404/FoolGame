using System;
using System.Collections.Generic;
using System.Linq;
using FoolGame.entities;
using FoolGame.Properties;

namespace FoolGame
{
    public class Player
    {
        public List<Card> cards;
        bool isInGame;

        public Player(List<Card> cards)
        {
            this.cards = cards;
        }

        public bool Move(Suit suit, Value value)
        {
            foreach (var currentCard in cards)
            {
                if (currentCard.suit == suit && currentCard.value == value)
                {
                    cards.Remove(currentCard);
                    return true;
                }
            }
            return false;
        }
    }
    
    
}