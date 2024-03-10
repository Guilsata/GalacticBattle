using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBattle
{
    internal static class Utils
    {
        private static Random _random = new Random();

        /// <summary>
        /// Retourne un int entre start et end tous les deux inclusifs
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int RandomInt(int start, int end) { return _random.Next(start, end + 1); }

        /// <summary>
        /// Retourne un int entre start (inclusif) et end (non inclusif)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int RandomIndex(int start, int end) { return _random.Next(start, end); }

        public static bool IsNullOrEmpty<T>(this List<T>? liste) 
        {
            return liste == null ? true : liste.Count == 0;
        }
    }
}
