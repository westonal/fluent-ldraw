using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw
{
    /// <summary>
    /// LDraw coordinate unit
    /// </summary>
    public sealed class LDU
    {
        public static LDU Zero = new LDU(0);

        private int _lduUnits;

        public LDU(int lduUnits)
        {
            _lduUnits = lduUnits;
        }

        public override int GetHashCode()
        {
            return _lduUnits;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LDU);
        }

        public bool Equals(LDU other)
        {
            if (other == null) return false;
            return other._lduUnits == _lduUnits;
        }

        public static LDU operator +(LDU one, LDU two)
        {
            return new LDU(one._lduUnits + two._lduUnits);
        }

        public static LDU operator +(LDU one, int two)
        {
            return new LDU(one._lduUnits + two);
        }

        public override string ToString()
        {
            return string.Format("{0} LDU", _lduUnits);
        }

        public int AsInteger { get { return _lduUnits; } }
    }
}
