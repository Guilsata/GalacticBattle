using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal class RebelleArmy : Army
    {
        public override string HeroDeath(string heroName) { return "Le héros "+heroName+" est mort en nous ouvrant la voie vers la liberté !"; }

        public override string HeroAnnouncement() { return "Voici " + Hero.Name + " héro des rebelles, son score de bravoure est : " + CalculateHeroScore(Hero.HitPoints, Hero.Damage) + "."; }

        public RebelleArmy(List<Soldier> battalion) : base(battalion) { }
    }
}
