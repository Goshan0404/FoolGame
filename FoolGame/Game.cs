using System;
using System.Collections.Generic;

namespace FoolGame
{
    public class Game
    {
        public int currentMove;
        private List<Player> players = new List<Player>();
        private Card trumpCard;

        public void StartGame(int playerCounts)
        {
            if (playerCounts > 6)
                throw new Exception();

            DeckOfCard.ShuffleDeck();
            SetPlayers(playerCounts);
            trumpCard = DeckOfCard.GetCardFromDeck();
            currentMove = SetFirstMove();
        }


        public void Move(Card card)
        {
            players[currentMove].Move(card);
            SetNextMove();
        }

        private void SetNextMove()
        {
            if (currentMove + 1 > players.Count)
                currentMove = 0;
            else
                currentMove++;
        }

        private int SetFirstMove()
        {
            int currentMin = Int32.MaxValue;
            int i = 0;
            foreach (var player in players)
            {
                i++;
                foreach (var keyValuePair in player.cards)
                {
                    if (keyValuePair.Value.suit == trumpCard.suit && keyValuePair.Value.value < currentMin)
                        currentMin = i;
                }
            }

            return currentMin;
        }

        private void SetPlayers(int playerCounts)
        {
            int countOfDeck = 0;
            for (int i = 0; i < playerCounts; i++)
            {
                players.Add(new Player(DeckOfCard.GetCardsFromDeck(6)));
            }
        }
    }
}