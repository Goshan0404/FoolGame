using FoolGame.entities;
using FoolGame.Properties;

namespace FoolGame
{
    public  class Card
    {
        public readonly Suit suit;
        public readonly Value value;

        public Card(Suit suit, Value value)
        {
            this.suit = suit;
            this.value = value;
        }

        public override string ToString()
        {
            return value + " of " + suit;
        }
    }
}