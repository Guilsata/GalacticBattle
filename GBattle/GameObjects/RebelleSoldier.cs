using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal class RebelleSoldier : Soldier
    {
        public RebelleSoldier(int hitPoints, int damage, string name) : base(hitPoints, damage, name){}

        public override string WarCry()
        {
            return "Pour la princesses Organa !";
        }

        public override Affiliation Affiliation { get { return GameObjects.Affiliation.Rebelle; } }
    }
}
