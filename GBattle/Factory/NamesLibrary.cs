using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.Factory
{
    internal class NamesLibrary
    {
        public List<string>? Names { get; set; }
        public List<string>? FirstNames { get; set; }

        public string GetCustomName() 
        {
            if (Names.IsNullOrEmpty() | FirstNames.IsNullOrEmpty()) { throw new Exception("Aucun nom disponible."); }
            return Names[Utils.RandomIndex(0,Names.Count)] +" "+FirstNames[Utils.RandomIndex(0,FirstNames.Count)];
        }
    }
}
