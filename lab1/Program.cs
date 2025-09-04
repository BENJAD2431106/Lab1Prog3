using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;
namespace lab1
{

    internal class Program
    {
        static async Task Main(string[] args)
        {


            double total = 0;
            Random rand = new Random();
            int nbClients = rand.Next(5, 100);
            int choix;
            var tasks = new List<Task>();
            //Création de la liste d'ingrédients
            List<Ingredient> Ingredients = new List<Ingredient>();
            //Création des ingrédients de base
            Ingredient cafe = new Ingredient("café", true, true, true);
            Ingredients.Add(cafe);
            Ingredient farine = new Ingredient("farine", false, true, false);
            Ingredients.Add(farine);
            Ingredient oeuf = new Ingredient("oeuf", false, false, true);
            Ingredients.Add(oeuf);
            Ingredient eau = new Ingredient("eau", false, true, true);
            Ingredients.Add(eau);
            Ingredient fruit = new Ingredient("fruit", false, true, true);
            Ingredients.Add(fruit);
            Ingredient beurre = new Ingredient("beurre", false, false, true);
            Ingredients.Add(beurre);
            Ingredient patate = new Ingredient("patate", false, true, true);
            Ingredients.Add(patate);
            Ingredient the = new Ingredient("thé", false, true, true);
            Ingredients.Add(the);
            //
            Ingredient laitAmande = new Ingredient("Lait d'amande", false, true, true);
            Ingredients.Add(laitAmande);
            Ingredient cacao = new Ingredient("Cacao pur", true, true, true);
            Ingredients.Add(cacao);
            //Création de tous les produits
            List<Produit> produits = new List<Produit>();
            produits.Add(new Nourriture("Croissant", 2.49, 0, false, new List<Ingredient> { farine, beurre }));
            produits.Add(new Nourriture("Muffin aux fruits", 1.99, 0, true, new List<Ingredient> { farine, fruit }));
            produits.Add(new Nourriture("Sandwich déjeuner", 4.79, 3, false, new List<Ingredient> { farine, beurre, oeuf }));
            produits.Add(new Nourriture("Patates déjeuner", 1.99, 2, true, new List<Ingredient> { patate }));
            produits.Add(new Boisson("Chocolat chaud", 2.49, 0, true, new List<Ingredient> { eau }));
            produits.Add(new Boisson("Latté", 4.29, 2, true, new List<Ingredient> { eau, cafe }));
            produits.Add(new Boisson("Espresso", 1.79, 0, true, new List<Ingredient> { eau, cafe }));
            produits.Add(new Boisson("Thé glacé maison", 2.49, 0, false, new List<Ingredient> { eau, the, fruit }));
            produits.Add(new Boisson("Jus de fruits", 1.49, 0, false, new List<Ingredient> { fruit }));
            produits.Add(new Boisson("Lait chocolaté", 2.80, 0, false, new List<Ingredient> { laitAmande, cacao }));
            produits.Add(new Boisson("Thé chaud", 2.70, 3, true, new List<Ingredient> { eau, the }));
            /// Mise en variable des nourritures:
            var nourritures = from prod in produits
                              where prod is Nourriture
                              select prod;
            /// Mise en variable des boissons:
            var boissons = from prod in produits
                           where prod is Boisson
                           select prod;

            var boissonsChaudes = from b in produits.OfType<Boisson>()
                                  where b.BoissonChaude is true
                                  select b;

            var boissonsCafeinees = from Boisson b in produits.OfType<Boisson>()
                                    where b.Ingredients.Any(ing => ing.ContientCafeine)
                                    select b;
            var nourritureSansGluten = from Nourriture n in produits.OfType<Nourriture>()
                                       where n.Ingredients.All(ing => ing.SansGluten)
                                       select n;

            var nourritureVegan = from Nourriture n in produits.OfType<Nourriture>()
                                  where n.Ingredients.All(ing => ing.vegan)
                                  select n;

            var nourrituresPasCollations = from Nourriture n in produits.OfType<Nourriture>()
                                           where n.Collation is false
                                           select n;

            //var listNourr = nourrituresPasCollations.ToList(); 
            //var listBoiss = boissons.ToList();
            var ListeCombinees = new List<String>();
            var ListeCombinee = from Nourriture n in produits.OfType<Nourriture>()
                                from Boisson b in boissons
                                where n.Collation is false
                                select n.Nom + " et "+ b.Nom;
            //foreach (var item in listNourr)
            //{
            //    foreach (var item2 in listBoiss)
            //    {
            //        ListeCombinees.Add(item.ToString()+" et "+item2.ToString());
            //    }
            //}

            //foreach(var item in ListeCombinees)
            //{
            //    Console.WriteLine(item);
            //}

            Console.WriteLine("Bienvenue au Café Chez Gino !");
            Console.ForegroundColor=ConsoleColor.Yellow;
            Console.WriteLine("Voici les nourritures : ");
            Console.ResetColor();
            nourritures.ToList().ForEach(i=>Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici les nourritures Sans Gluten: ");
            Console.ResetColor();
            nourritureSansGluten.ToList().ForEach(i => Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici les nourritures Végan: ");
            Console.ResetColor();
            nourritureVegan.ToList().ForEach(i => Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici les boissons: ");
            Console.ResetColor();
            boissons.ToList().ForEach(i => Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Voici les boissons cafeinées: ");
            Console.ResetColor();
            boissonsCafeinees.ToList().ForEach(i => Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Voici les boissons chaudes: ");
            Console.ResetColor();
            boissonsChaudes.ToList().ForEach(i => Console.WriteLine(i.ToString()));
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Voici tous les combos possibles: ");
            Console.ResetColor();
            foreach (var n in ListeCombinee)
            {
                Console.WriteLine(n.ToString());
            }

            //Pour chaque client, procéder à une commande
            for (int i = 0; i < nbClients; i++)
            {
                //Choisir aléatoirement un produit à commander
                Random random = new Random();
                choix = random.Next(produits.Count);
                //Afficher quel produit la personne veut commander
                Console.WriteLine("Client " + (i + 1) + " commande ---> " + produits[choix]);
                //Préparer le produit, le numérode de la commande doit être sous le format Com001
                //Astuce, utiliser ToString("D3")
                string numCom = "Com" + (i + 1).ToString("D3");
                tasks.Add(produits[choix].Preparer(numCom));
                //Augmenter le total par la valeur du produit
                total += produits[choix].Prix;
                Console.WriteLine("");
            }
            await Task.WhenAll(tasks);
            Console.WriteLine("Merci d'avoir travaillé au Café Chez Gino !");
            Console.WriteLine("Aujourd'hui, le café a fait " + Math.Round(total, 2) + "$!");
        }
    }
}

