﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer customer = new Customer();
            Bot.TakeOrder(customer);

            Console.ReadLine();
        }
    }

    static class Bot
    {
        public static void TakeOrder(Customer customer)
        {
            Console.Write("Hello! Please, enter your name: ");
            customer.Name = Console.ReadLine();
            Menu.ShowMenu();
            bool wantAnotherItem = false;
            customer.Order = new List<string>();
            int itemNumber = 0;
            do
            {
                Console.Write("Choose an item number from the menu: ");
                try
                {
                    itemNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                    customer.Order.Add(Menu.PizzaList[itemNumber]);
                    Console.WriteLine("Do you want anything else?(type yes or no) ");
                    if (Console.ReadLine().ToLower().Equals("yes"))
                        wantAnotherItem = true;
                    else
                        wantAnotherItem = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid item number, try again.");
                    wantAnotherItem = true;
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Invalid item number, try again.");
                    wantAnotherItem = true;
                }
            } while (wantAnotherItem);
            ShowOrder(customer);
            Console.Write("Enter your email: ");
            customer.Email = Console.ReadLine();
            Console.Write("Enter your address: ");
            customer.Address = Console.ReadLine();
            Console.Write("Choose payment method:\n1. Cash\n2. Card\nChoose a number: ");
            string paymentMethod;
            while (true)
            {
                paymentMethod = Console.ReadLine();
                if (paymentMethod.Equals("1") || paymentMethod.Equals("2"))
                    break;
                Console.Write("Wrong number, choose again: ");
            }
            switch (paymentMethod)
            {
                case "1":
                    customer.PaymentMethod = Payment.Cash;
                    break;
                case "2":
                    customer.PaymentMethod = Payment.Card;
                    break;
            }
        }

        private static void ShowOrder(Customer customer)
        {
            Console.WriteLine("\nYour order:");
            for(int i=0; i<customer.Order.Count(); i++)
            {
                Console.Write((i + 1) + ". ");
                Console.WriteLine(customer.Order[i]);
            }
            Console.WriteLine();
        }

    }

    static class Menu
    {
        public static readonly string[] PizzaList = { "Margherita", "Pepperoni", "Carbonara", "Vegetarian", "Hawaiian" };

        public static void ShowMenu()
        {
            Console.WriteLine("\n\tMenu:");
            for (int i = 0; i < PizzaList.Length; i++)
            {
                Console.Write((i + 1) + ". ");
                Console.WriteLine(PizzaList[i]);
            }
            Console.WriteLine();
        }
    }

    class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<string> Order { get; set; }
        public Payment PaymentMethod { get; set; }
    }

    enum Payment
    {
        Cash,
        Card
    }
}
