using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using GBattle.GameObjects;

namespace GBattle.Factory
{
    internal static class ArmyFactory
    {
        private static long _matricule = 0;
        private static char _generation = 'M';
        private const string _jsonName = "NamesLibrary.json";
        private static NamesLibrary? _namesLibrary;

        public static void InitArmy(Affiliation affiliation, out Army army, int numberSoldier)
        {
            switch (affiliation)
            {
                case Affiliation.Rebelle:
                    army = new RebelleArmy(InitRebelleBattalion(numberSoldier));
                    break;
                case Affiliation.Empire:
                    army = new EmpireArmy(InitEmpireBattalion(numberSoldier));
                    break;
                default:
                    throw new Exception("valeur affiliation incohérente : " + affiliation);
            }
        }

        private static List<Soldier> InitRebelleBattalion(int numberSoldier)
        {
            return Enumerable.Range(1, numberSoldier).Select(e => InitRebelleSoldier()).ToList();
        }

        private static List<Soldier> InitEmpireBattalion(int numberSoldier)
        {
            return Enumerable.Range(1, numberSoldier).Select(e => InitEmpireSoldier()).ToList();
        }

        private static Soldier InitRebelleSoldier()
        {
            return new RebelleSoldier(Utils.RandomInt(1000, 2000), Utils.RandomInt(100, 500), InitRebelleName());
        }

        private static Soldier InitEmpireSoldier()
        {
            return new EmpireSoldier(Utils.RandomInt(1000, 2000), Utils.RandomInt(100, 500), InitEmpireName());
        }

        private static string InitRebelleName()
        {
            if (_namesLibrary == null) { _namesLibrary = JsonReader.GetNamesLibraryFrom(_jsonName); }
            return _namesLibrary.GetCustomName(); ;
        }

        private static string InitEmpireName()
        {
            if (_matricule > 999999)
            {
                _generation = (char)(_generation + 1);
                _matricule = 0;
            }
            return _generation.ToString() + _matricule++.ToString("D6");
        }
    }
}
