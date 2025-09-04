using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class Ingredient
    {
        public string Nom {  get; set; }
        public bool ContientCafeine { get; set; }
        public bool vegan {  get; set; }
        public bool SansGluten {  get; set; }

        public Ingredient(string nom, bool contientCafeine, bool vegan, bool sansGluten)
        {
            Nom = nom;
            ContientCafeine = contientCafeine;
            this.vegan = vegan;
            SansGluten = sansGluten;
        }
    }
}
