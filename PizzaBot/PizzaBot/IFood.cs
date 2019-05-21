using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace PizzaBot
{
    interface IFood
    {
        string Name { get; set; }
        Energy Nutrition { get; set; }
        void Describe();
    }

    [DataContract]
    abstract class Pizza : IFood
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        protected Energy _nutrition;
        [DataMember]
        public Energy Nutrition { get => _nutrition; set => _nutrition=value; }
        [DataMember]
        public List<string> Ingredients { get; set; }

        public virtual void Describe()
        {
            Console.WriteLine("{0}", Name);
            Console.Write("Ingredients: ");
            foreach (var item in Ingredients)
            {
                Console.Write("{0} ", item);
            }
            Console.WriteLine();
            Console.WriteLine($"Nutrition: calories - {Nutrition.calories}, protein - {Nutrition.protein}, carbs - {Nutrition.carbs}, fat - {Nutrition.fat}");
        }
    }

    [DataContract]

    abstract class Drinks : IFood
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        protected Energy _nutrition;
        [DataMember]
        public Energy Nutrition { get => _nutrition; set => _nutrition = value; }
        [DataMember]
        public bool Alcoholic { get; set; }

        public void Describe()
        {
            Console.WriteLine("{0}", Name);
            if (Alcoholic)
                Console.WriteLine("Alcoholic");
            else
                Console.WriteLine("Non-alcoholic");
            Console.WriteLine($"Nutrition: calories - {Nutrition.calories}, protein - {Nutrition.protein}, carbs - {Nutrition.carbs}, fat - {Nutrition.fat}");

        }
    }

    [DataContract]
    class Margherita : Pizza
    {
        public Margherita()
        {
            Name = "Margherita";
            Ingredients = new List<string>();
            Ingredients.AddRange(new string[]{ "Mozarella", "Tomato"});
            _nutrition.calories = 600;
            _nutrition.protein = 40;
            _nutrition.carbs = 100;
            _nutrition.fat = 50;
        }
    }

    [DataContract]
    class Spicy : Pizza
    {
        public Spicy()
        {
            Name = "Spicy";
            Ingredients = new List<string>();
            Ingredients.AddRange(new string[] { "Mozarella", "Chili pepper", "Carolina reaper pepper", "Beef" });
            _nutrition.calories = 700;
            _nutrition.protein = 40;
            _nutrition.carbs = 120;
            _nutrition.fat = 50;
        }

        public override void Describe()
        {
            base.Describe();
            Console.WriteLine("Careful, very spicy!!!");
        }
    }

    [DataContract]
    class Vegeratian : Pizza
    {
        public Vegeratian()
        {
            Name = "Vegetarian";
            Ingredients = new List<string>();
            Ingredients.AddRange(new string[] { "Mozarella", "Champignons", "Tomato", "Cucumber" });
            _nutrition.calories = 400;
            _nutrition.protein = 20;
            _nutrition.carbs = 80;
            _nutrition.fat = 30;
        }
    }

    [DataContract]
    class Vodka : Drinks
    {
        public Vodka()
        {
            Name = "Vodka";
            _nutrition.calories = 100;
            _nutrition.protein = 0;
            _nutrition.carbs = 30;
            _nutrition.fat = 0;
            Alcoholic = true;
        }
    }

    [DataContract]
    class AppleJuice : Drinks
    {
        public AppleJuice()
        {
            Name = "Apple Juice";
            _nutrition.calories = 100;
            _nutrition.protein = 0;
            _nutrition.carbs = 30;
            _nutrition.fat = 0;
            Alcoholic = false;
        }
    }

    public struct Energy
    {
        public int calories;
        public int protein;
        public int carbs;
        public int fat;
    }
}
