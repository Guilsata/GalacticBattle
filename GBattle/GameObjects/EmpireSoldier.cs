using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal class EmpireSoldier : Soldier
    {
        public EmpireSoldier(int hitPoints, int damage, string name) : base(hitPoints, damage, name) { }

        public override string WarCry()
        {
            return "Traitor !";
        }

        public override Affiliation Affiliation { get { return GameObjects.Affiliation.Empire; } }
    }
}
