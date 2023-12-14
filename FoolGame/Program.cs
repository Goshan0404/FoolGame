using System;
using FoolGame.entities;
using FoolGame.Properties;
using FoolGame.Utils;

namespace FoolGame
{
    internal class Program
    {
        static Suit suit = Suit.CLUBS;
        static Value value = Value.ACE;
        static Game game = new Game();
        static string[] input;

        public static void Main(string[] args)
        {
            game.StartGame(2);
            Console.WriteLine();
            

            while (game.GameIsOver != true)
            {
                PrintMove(game.CurrentMove);
                Console.WriteLine("Ход атакующего:");
                input = Console.ReadLine().ToUpper().Split(' ');
                
                if (!Moved(() => game.Move(suit, value), MoveIsFinish))
                {
                    Console.WriteLine("Ход закончен");
                    continue;
                }

                PrintMove(game.GetNextPlayersMove());
                Console.WriteLine("Ход защищающегося:");
                input = Console.ReadLine().ToUpper().Split(' ');

                if (!Moved(() => game.MoveBack(suit, value), CardsIsTaked))
                {
                    Console.WriteLine("Карты взяты");
                    continue;
                }
            }
        }

        private static bool CardsIsTaked()
        {
            bool takeCards = input[0] == Constants.TAKE_CARDS;
            if (takeCards)
            {
                game.SetNextMove(Constants.TAKE_CARDS, 2);
                game.CurrentStack.Clear();
                return true;
            }

            if (!CheckCorrectInput())
            {
                Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                input = Console.ReadLine().ToUpper().Split(' ');
                CardsIsTaked();
            }

            return false;
        }

        private static bool MoveIsFinish()
        {
            bool finisMove = input[0] == Constants.PAS || game.CurrentStack.Count == 6;
            if (finisMove)
            {
                game.SetNextMove();
                game.CurrentStack.Clear();
                return true;
            }

            if (!CheckCorrectInput())
            {
                Console.WriteLine("НЕКОРРЕКТНЫЙ ВВОД");
                input = Console.ReadLine().ToUpper().Split(' ');
                MoveIsFinish();
            }

            return false;
        }

        private static bool CheckCorrectInput()
        {
            try
            {
                suit = GetEnumFromString<Suit>(input[1]);
                value = GetEnumFromString<Value>(input[0]);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private static bool Moved(Action action, Func<bool> moveIsFinished)
        {
            if (moveIsFinished())
            {
                return false;
            }

            try
            {
                action();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Такой картой невозможно сходить");
                input = Console.ReadLine().ToUpper().Split(' ');
                return Moved(action, moveIsFinished);
            }
        }

        private static void PrintMove(int player)
        {
            PrintTrumpCard(game);
            if (game.CurrentStack != null && game.CurrentStack.Count != 0)
            {
                PrintCurrentStackCards();
            }

            PrintPlayersCards(player);
        }

        private static void PrintCurrentStackCards()
        {
            Console.WriteLine("Последняя карта на столе: " + game.CurrentStack.Peek());
            Console.WriteLine("Текущие карты на столе: ");
            foreach (var card in game.CurrentStack)
            {
                Console.Write(card+ ", ");
            }
            Console.WriteLine();
        }

        private static void PrintTrumpCard(Game game)
        {
            Console.WriteLine();
            Console.WriteLine("КОЗЫРНАЯ КАРТА: " + game.TrumpCard);
            Console.WriteLine();
        }

        private static void PrintPlayersCards(int player)
        {
            Console.WriteLine("Текущие карты игрока:");
            foreach (var card in game.Players[player].cards)
            {
                Console.Write(card + ", ");
            }

            Console.WriteLine();
        }

        private static T GetEnumFromString<T>(string input)
        {
            return (T)Enum.Parse(typeof(T), input);
        }
    }
}