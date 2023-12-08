using System;
using System.Collections.Generic;
using System.Linq;

namespace FoolGame
{
    public class Player
    {
        private int cardsCounts;
        public Dictionary<Card, Card> cards;
        bool isInGame;

        public Player(Dictionary<Card, Card> cards)
        {
            this.cards = cards;
        }

        public void Move(Card card)
        {
            if (cards.ContainsKey(card))
            {
                cards.Remove(card);
                cardsCounts--;
            }
            else
            {
                throw new ArgumentException();
            }
        }
    }
    
    
}