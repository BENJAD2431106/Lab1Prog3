using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Nourriture : Produit
    {
        public bool Collation {  get; set; }
        public Nourriture(string nom, double prix, int tps, bool collation, List<Ingredient> Ingredients) : base(nom,prix,tps, Ingredients)
        {
            Collation = collation;
        }
        public override async Task<Produit> Preparer(string no)
        {
            await base.Preparer(no);
            Console.WriteLine("Mise en assiette...");
            await Task.Delay(1000);
            await Cuisson();
            await Dressage();
            return this;
        }
        public async Task Cuisson()
        {
            Console.ResetColor();
            if (TempsPreparationSuppEnSec>=3)
            {
                Console.WriteLine("En cuisson...");
                Console.WriteLine("Attente...");
                await Task.Delay(2000);
            }
        }
        public override async Task Dressage()
        {
            Console.ResetColor();
            if (TempsPreparationSuppEnSec==1) 
            {
                Console.WriteLine("Dressage en cours.");
                await Task.Delay(1000);
                Console.WriteLine("Dressage terminé");
            }
            else if(TempsPreparationSuppEnSec>=2)
            {
                int tempsAttente=(TempsPreparationSuppEnSec*1000)-2000;
                Console.WriteLine("Dressage en cours.");
                await Task.Delay(tempsAttente);
                Console.WriteLine("Dressage terminé");
            }
        }
        public override async Task FairePayer(string no)
        {
            await base.FairePayer(no);
        }
    }
}
