using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Boisson : Produit
    {
        //ajout
        public int totalCafeine { get; set; } = 0;
        public bool BoissonChaude { get; set; }
        public Boisson(string nom, double prix, int tps, bool BoissonChaude, List<Ingredient> Ingredients) : base(nom, prix, tps, Ingredients)
        {
            this.BoissonChaude = BoissonChaude;
        }
        public async override Task<Produit> Preparer(string no)
        {
            await base.Preparer(no);
            await ChaufferEau();
            Console.WriteLine("Mise en place, versement de la boisson dans une tasse");
            await Task.Delay(1000);
            await Dressage();
            await FairePayer(no);
            return this;

        }
        public async Task ChaufferEau()
        {
            Console.ResetColor();
            if (BoissonChaude)
            {
                totalCafeine++;
                Random random = new Random();
                int tempsAttente = random.Next(5000, 10001);
                double tpsenSec = tempsAttente / 1000;
                Console.WriteLine("Ça chauffe ! Temps d'attente ---> " + Math.Round(tpsenSec, 2));
                Console.WriteLine("Attente...");
                await Task.Delay(1000);
                Infuser();
            }
        }
        public void Infuser()
        {
            var requete = from ing in Ingredients
                          where ing.ContientCafeine is true
                          select ing;
            foreach (Ingredient req in requete)
            {
                    Console.WriteLine("Infusion du breuvage contenant de la cafeine.");
                    Thread.Sleep(5000);
                    Console.WriteLine("Pret ! ");
                    totalCafeine++;
            }
        }
        public override async Task Dressage()
        {
            Console.ResetColor();
            if (TempsPreparationSuppEnSec > 0)
            {
                Console.WriteLine("Dressage en cours.");
                await Task.Delay(TempsPreparationSuppEnSec*1000);
                Console.WriteLine("Dressage terminé.");
            }
        }
        public override async Task FairePayer(string no)
        {
            await base.FairePayer(no);
        }
    }
}
