using FoolGame.Properties;

namespace FoolGame
{
    public  class Card
    {
        public readonly Suit suit;
        public readonly int value;

        public Card(Suit suit, int value)
        {
            this.suit = suit;
            this.value = value;
        }

        public override string ToString()
        {
            return suit + " " + value;
        }
    }
}