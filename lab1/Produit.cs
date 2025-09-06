using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public abstract class Produit
    {
        public string Nom { get; set; }
        public double Prix { get; set; }
        public int TempsPreparationSuppEnSec { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        Random random = new Random();
        public Produit(string nom, double prix, int tps, List<Ingredient> ingredients)
        {
            Nom=nom;
            Prix=prix;
            TempsPreparationSuppEnSec=tps;
            Ingredients = ingredients;
        }

        public virtual async Task<Produit> Preparer(string no)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("EN COURS DE PRÉPARATION DE LA COMMANDE " + no + " PRODUIT COMMANDÉE : " + Nom);
            await Task.Delay(1000);
            Console.ResetColor();
            return this;
        }
        public abstract Task Dressage();      
        public virtual async Task FairePayer(string no)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Commande " + no + " Prête, Prix a payer : " + Prix);
            await Task.Delay(1000);
            Console.ResetColor();
        }

        public override string ToString()
        {
            return Nom;
        }


    }
}
