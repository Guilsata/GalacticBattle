using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle.GameObjects
{
    internal abstract class Soldier
    {
        public int HitPoints { get; private set; }

        public int Damage { get; }

        public string Name { get; }
        
        public abstract Affiliation Affiliation { get; }

        public Soldier(int hitPoints, int damage, string name)
        {
            HitPoints = hitPoints;
            Damage = damage;
            Name = name;
        }

        public int CalculateDamage()
        {
            return Damage * Utils.RandomInt(0,100) / 100;
        }

        /// <summary>
        /// Applique les dommages, si le soldat est toujours en vie (HP > 0)
        /// retourne true
        /// sinon retourne false
        /// </summary>
        /// <param name="damage"></param>
        /// <returns></returns>
        public bool GetShoot(int damage)
        {
            HitPoints -= damage;
            return HitPoints >= 0;
        }

        public abstract string WarCry();
    }
}
