using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formal_Language.Classes
{
    class Functie
    {
        public int indexOrigine, indexListaFinala;
        public string modificator;

        public Functie(int indexOrigine, string modificator, int indexListaFinala = 0 )
        {
            this.indexOrigine = indexOrigine;
            this.indexListaFinala = indexListaFinala;
            this.modificator = modificator;
        }
    }
}
