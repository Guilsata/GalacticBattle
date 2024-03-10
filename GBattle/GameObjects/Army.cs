using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal abstract class Army
    {
        public List<Soldier> Battalion { get; }

        public Soldier? Hero { get; }

        public Army(List<Soldier> battalion)
        {
            if (battalion == null || battalion.Count == 0) { throw new Exception("Un batallion doit avoir des soldats !"); }
            Battalion = battalion;
            Hero = Battalion.MaxBy(s => CalculateHeroScore(s.HitPoints, s.Damage));
            if (Hero == null) { throw new Exception("Aucun héro trouvé."); }
        }

        public string GetAttacked(Soldier soldier, int damage) 
        {
            if (soldier.GetShoot(damage))
            {
                return soldier.Name + " toujours vivant avec " + soldier.HitPoints + " points de vie.";
            }
            else 
            {
                RemoveTheDead(soldier);
                return soldier == Hero ? HeroDeath(soldier.Name) : soldier.Name + " hors de combat";
            }
        }

        public bool Undefeated() { return Battalion.Count != 0; }

        public Soldier GetTarget() { return Battalion[Utils.RandomIndex(0, Battalion.Count)]; }

        public double DamageAverageByTurn() 
        {
            return Battalion.Sum(e => (double)e.Damage)/(Battalion.Count*2);
        }

        public long SumHp() 
        {
            return Battalion.Sum(e => (long)e.HitPoints);
        }

        private void RemoveTheDead(Soldier deadSoldier) 
        {
            Battalion.Remove(deadSoldier);
        }

        protected int CalculateHeroScore(int hp, int damage) { return hp + damage * 10; }

        public abstract string HeroDeath(string heroname);

        public abstract string HeroAnnouncement();
    }
}
