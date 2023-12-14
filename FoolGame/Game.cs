using System;
using System.Collections.Generic;
using System.Linq;
using FoolGame.entities;
using FoolGame.Properties;
using FoolGame.Utils;

namespace FoolGame
{
    public class Game
    {
        private List<Player> players = new List<Player>();
        private Stack<Card> currentStack = new Stack<Card>();
        private bool gameIsOver;
        private int currentMove;
        private Card trumpCard;

        public List<Player> Players => players;
        public Stack<Card> CurrentStack => currentStack;
        public bool GameIsOver => GameIsFinished();
        public int CurrentMove => currentMove;
        public Card TrumpCard => trumpCard;

        public void StartGame(int playerCounts)
        {
            if (playerCounts > 6)
                throw new Exception();

            DeckOfCard.ShuffleDeck();
            SetPlayers(playerCounts);
            trumpCard = DeckOfCard.GetCardsFromDeck(1)[0];
            currentMove = SetFirstMove();
        }


        public void Move(Suit suit, Value value)
        {
            if (currentStack.Count == 0)
            {
                Move(currentMove, suit, value);
                return;
            }

            foreach (var card in currentStack)
            {
                if (card.value == value)
                {
                    Move(currentMove, suit, value);
                    return;
                }
            }

            throw new Exception();
        }

        public void MoveBack(Suit suit, Value value)
        {
            if ((currentStack.Peek().suit == suit && currentStack.Peek().value < value)
                || (currentStack.Peek().suit != trumpCard.suit && trumpCard.suit == suit))
            {
                Move(GetNextPlayersMove(), suit, value);
                return;
            }

            throw new Exception();
        }

        private void Move(int currentMove, Suit suit, Value value)
        {
            players[currentMove].Move(suit, value);
            currentStack.Push(new Card(suit, value));
        }

        public int GetNextPlayersMove()
        {
            if (currentMove + 1 > players.Count - 1)
                return 0;
            return currentMove + 1;
        }

        public void SetNextMove(string constants = null, int step = 1)
        {
            FillMissingCards(constants);
            for (int i = 0; i < step; i++)
            {
                currentMove = GetNextPlayersMove();
            }
        }

        private void FillMissingCards(string constants = null)
        {
            if (constants == Constants.TAKE_CARDS)
            {
                int currentPlayer = GetNextPlayersMove();
                foreach (var card in currentStack)
                {
                    players[currentPlayer].cards.Add(card);
                }
            }

            foreach (var player in players)
            {
                if (player.cards.Count < 6)
                {
                    foreach (var card in DeckOfCard.GetCardsFromDeck(6 - player.cards.Count))
                    {
                        player.cards.Add(card);
                    }
                }
            }
        }

        private int SetFirstMove()
        {
            Value currentMinValue = Value.ACE;
            int i = -1;
            int firstMove = -1;
            foreach (var player in players)
            {
                i++;
                foreach (var card in player.cards)
                {
                    if (card.suit == trumpCard.suit && card.value < currentMinValue)
                    {
                        currentMinValue = card.value;
                        firstMove = i;
                    }
                }
            }

            return firstMove;
        }


        private void SetPlayers(int playerCounts)
        {
            int countOfDeck = 0;
            for (int i = 0; i < playerCounts; i++)
            {
                players.Add(new Player(DeckOfCard.GetCardsFromDeck(6)));
            }
        }

        private bool GameIsFinished()
        {
            if (DeckOfCard.GetDeckOfCard().Count == 0)
            {
                foreach (var player in players)
                {
                    if (player.cards.Count == 0)
                        return true;
                }
            }
            return false;
        }
    }
}