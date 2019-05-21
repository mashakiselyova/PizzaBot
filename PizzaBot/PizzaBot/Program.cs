using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Runtime.Serialization;

namespace PizzaBot
{
    class Program
    {
        static void Main(string[] args)
        {
           //InitializeDefault();
            Customer customer = new Customer();
            Bot.TakeOrder(customer);

            Console.ReadLine();
        }

        static void InitializeDefault()
        {
            Menu menu = new Menu();
            menu.MenuList.Add(new Margherita());
            menu.MenuList.Add(new Vegeratian());
            menu.MenuList.Add(new Spicy());
            menu.MenuList.Add(new AppleJuice());
            menu.MenuList.Add(new Vodka());
            menu.SaveToFile();
        }
    }

    static class Bot
    {
        public static void TakeOrder(Customer customer)
        {
            Console.Write("Hello! Please, enter your name: ");
            customer.Name = Console.ReadLine();
            Menu menu = Menu.LoadFromFile();
            menu.ShowMenu();
            bool wantAnotherItem = false;
            customer.Order = new List<IFood>();
            int itemNumber = 0;
            do
            {
                Console.Write("Choose an item number from the menu: ");
                try
                {
                    itemNumber = Convert.ToInt32(Console.ReadLine()) - 1;
                    customer.Order.Add(menu.MenuList[itemNumber]);
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
            for (int i = 0; i < customer.Order.Count(); i++)
            {
                Console.Write((i + 1) + ". ");
                Console.WriteLine(customer.Order[i].Name);
            }
            Console.WriteLine();
        }
    }

    [DataContract]
    [KnownType(typeof(Margherita))]
    [KnownType(typeof(Vegeratian))]
    [KnownType(typeof(Spicy))]
    [KnownType(typeof(AppleJuice))]
    [KnownType(typeof(Vodka))]
    class Menu
    {
        [DataMember]
        public List<IFood> MenuList=new List<IFood>();
        public void ShowMenu()
        {
            Console.WriteLine("\n\tMenu:");
            for (int i = 0; i < MenuList.Count; i++)
            {
                Console.Write((i + 1) + ". ");
                MenuList[i].Describe();
            }
            Console.WriteLine();
        }

        public static Menu LoadFromFile()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Menu));
            FileInfo menuFile = new FileInfo(@"Menu.json");
            FileStream fs = menuFile.Open(FileMode.Open, FileAccess.Read);
            return (Menu)ser.ReadObject(fs);
        }

        public void SaveToFile()
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(Menu));
            FileInfo menuFile = new FileInfo(@"Menu.json");
            FileStream fs = menuFile.Open(FileMode.OpenOrCreate, FileAccess.Write);
            ser.WriteObject(fs, this);
            fs.Close();
        }

        
    }

    class Customer
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public List<IFood> Order { get; set; }
        public Payment PaymentMethod { get; set; }
    }

    enum Payment
    {
        Cash,
        Card
    }
}
