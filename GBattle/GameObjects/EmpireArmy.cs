using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal class EmpireArmy : Army
    {
        public override string HeroDeath(string heroName) { return "Le matricule exceptionnel "+heroName+" est mort pour l'empire !"; }

        public override string HeroAnnouncement() { return "Voici " + Hero.Name + " héro de l'empire, son score de bravoure est : " + CalculateHeroScore(Hero.HitPoints,Hero.Damage) + "."; }

        public EmpireArmy(List<Soldier> battalion) : base(battalion) { }

    }
}
