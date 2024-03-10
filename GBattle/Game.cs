using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GBattle.Factory;
using GBattle.GameObjects;

namespace GBattle
{
    internal class Game
    {

        private Army RebelleArmy;

        private Army EmpireArmy;

        private List<Soldier> Both { get { return RebelleArmy.Battalion.Concat(EmpireArmy.Battalion).ToList(); } }

        private Affiliation? prediction;

        private long nbrTurn;

        public Game(int sizeArmyRebelle, int sizeArmyEmpire) 
        {
            ArmyFactory.InitArmy(Affiliation.Rebelle, out RebelleArmy, sizeArmyRebelle);
            ArmyFactory.InitArmy(Affiliation.Empire, out EmpireArmy, sizeArmyEmpire);
            prediction = Prediction();
        }

        /// <summary>
        /// Calcule de prédiction :
        /// le but ici est de calculé des scores qui représente une valeur moyenne du nombre de tour nécessaire à une armée
        /// de supprimer tous les soldats d'une autre. Plus ce score est bas mieux c'est.
        /// le calcule est réalisé comme suit :
        /// le total des HitPoints de l'armée (A) sont divisés par la moyenne des dégâts par tour de l'armée (B)
        /// modulé par le pourcentage de soldat appartenant à l'armée (B) du total des soldats des deux armées.
        /// A noté que plus les forces sont équivalentes plus la prédiction à de chances d'être fausse.
        /// </summary>
        /// <returns>si le retour est null c'est qu'il y a égalité</returns>
        private Affiliation? Prediction() 
        {
            double scoreEmpire = (RebelleArmy.SumHp() * Both.Count) / (EmpireArmy.DamageAverageByTurn() * EmpireArmy.Battalion.Count);
            double scoreRebelle = (EmpireArmy.SumHp() * Both.Count) / (RebelleArmy.DamageAverageByTurn() * RebelleArmy.Battalion.Count);
            if (scoreEmpire < scoreRebelle) { nbrTurn = (long)scoreEmpire; return Affiliation.Empire; }
            else if (scoreRebelle < scoreEmpire) { nbrTurn = (long)scoreRebelle; return Affiliation.Rebelle; } 
            else { nbrTurn = 0; return null; }
        }

        private Soldier ChooseAttacker() 
        {
            return Both[Utils.RandomIndex(0, Both.Count)];
        }

        private Soldier ChooseTarget(Soldier attacker) 
        {
            if (attacker.Affiliation == Affiliation.Rebelle)
            {
                return EmpireArmy.GetTarget();
            }
            else 
            {
                return RebelleArmy.GetTarget();
            }
        }

        private string Attack(Soldier attacker) 
        {
            Soldier target = ChooseTarget(attacker);

            int damage = attacker.CalculateDamage();
            Console.WriteLine(attacker.Name + " attaque " + target.Name + " avec une puissance de feu de " + damage);
            Console.WriteLine(attacker.WarCry());

            if (target.Affiliation == Affiliation.Rebelle)
            {
                
                return RebelleArmy.GetAttacked(target, damage);
            }
            else 
            {
                return EmpireArmy.GetAttacked(target, damage);
            }
        }

        private void EndingCom() 
        {
            Console.WriteLine("Rappel de la prédiction : " + (prediction != null ? prediction.ToString() : " force égale.")+"\nNombre de tours prédis : " + nbrTurn);
            if (prediction == Affiliation.Empire)
            {

                if (RebelleArmy.Undefeated())
                {
                    Console.WriteLine("Les rebelles se sont joués du destin et ont remporté la victoire ! Gloire à la princesse !");
                    Console.WriteLine("Prédiction fausse !");
                }
                else
                {
                    Console.WriteLine("Tous c'est déroulé comme prévu ; les traitres sont morts, l'empire perdurera !");
                    Console.WriteLine("Prédiction vraie !");
                }

            }
            else if (prediction == Affiliation.Rebelle)
            {
                if (RebelleArmy.Undefeated())
                {
                    Console.WriteLine("Les rebelles ont remporté la victoire comme prévu ! Gloire à la princesse !");
                    Console.WriteLine("Prédiction vraie !");
                }
                else
                {
                    Console.WriteLine("La stratégique brilliante de l'empire à arracher la victoire ! Les traitres sont morts, l'empire perdurera !");
                    Console.WriteLine("Prédiction fausse !");
                }
            }
            else
            {
                if (RebelleArmy.Undefeated()) { Console.WriteLine("Les rebelles ont remporté la victoire ! Gloire à la princesse !"); }
                else { Console.WriteLine("Les traitres sont morts, l'empire perdurera !"); }
            }
        }

        private void StartingCom() 
        {
            Console.Write("La bataille va commencer, les augures");
            if (prediction == Affiliation.Empire)
            {
                Console.WriteLine(" sont en faveur de l'empire !");
            }
            else if (prediction == Affiliation.Rebelle)
            {
                Console.WriteLine(" sont en faveur des rebelles !");
            }
            else
            {
                Console.WriteLine(" ne favorisent personnes la bataille sera serrée !");
            }
            Console.WriteLine("Pour une bataille de " + nbrTurn + " tours.");
            Console.WriteLine();

            Console.WriteLine(EmpireArmy.HeroAnnouncement());
            Console.WriteLine(RebelleArmy.HeroAnnouncement());
            Console.WriteLine();
        }

        public void run() 
        {
            StartingCom();

            int tour = 1;

            while (RebelleArmy.Undefeated() & EmpireArmy.Undefeated()) 
            {
                Console.WriteLine("Tour " + tour++ + " ::");
                Soldier attacker = ChooseAttacker();
                Console.WriteLine(Attack(attacker));
                Console.WriteLine();
            }

            EndingCom();
        }
    }
}
