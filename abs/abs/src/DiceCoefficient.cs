using System;
using System.Collections.Generic;
using System.Text;

namespace abs.src
{
    public static class DiceCoefficient
    {
        private static HashSet<string> _setA;
        private static HashSet<string> _setB;

        static DiceCoefficient()
        {
            _setA = new HashSet<string>();
            _setB = new HashSet<string>();
        }

        public static double Calc(string strA, string strB)
        {
            _setA.Clear();
            _setB.Clear();

            for (var i = 0; i < strA.Length - 1; ++i)
                _setA.Add(strA.Substring(i, 2));

            for (var i = 0; i < strB.Length - 1; ++i)
                _setB.Add(strB.Substring(i, 2));

            var setACount = _setA.Count;
            var setBCount = _setB.Count;
            _setA.IntersectWith(_setB);

            return (2.0 * _setA.Count) / (setACount + setBCount);
        }
    }
}