using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw.Building.LineClash
{
    public sealed class LineClashDetector
    {
        private readonly HashSet<LineRecord> _res = new HashSet<LineRecord>();

        public void Add(LineRecord lineRecord)
        {
            _res.Add(lineRecord);
        }

        public bool Contains(LineRecord lineRecord)
        {
            return _res.Contains(lineRecord);
        }
    }
}
