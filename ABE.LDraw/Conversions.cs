using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw
{
    public sealed class Conversions
    {
        public static LDU BrickWidthToLDU(int width)
        {
            return new LDU(width * 20);
        }

        public static LDU BrickHeightToLDU(int height)
        {
            return new LDU(height * 24);
        }

        public static LDU PlateHeightToLDU(int height)
        {
            return new LDU(height * 8);
        }

        public static float LDUToMm(LDU lDU)
        {
            return lDU.AsInteger * 0.4f;
        }

        internal static int LDUToBrickWidth(LDU lDU)
        {
            return lDU.AsInteger / 20;
        }
    }
}
