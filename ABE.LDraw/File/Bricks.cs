namespace ABE.LDraw.File
{
    public static class Bricks
    {
        public class BrickFormat
        {
            internal BrickFormat(string fileName, LDU width, LDU depth)
            {
                FileName = fileName;
                Width = width;
                Depth = depth;
            }

            public string FileName { get; private set; }
            public LDU Width { get; private set; }
            public LDU Depth { get; private set; }
        }

        public static readonly BrickFormat B_1x1 = new BrickFormat("3005.dat", Conversions.BrickWidthToLDU(1), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat B_2x1 = new BrickFormat("3004.dat", Conversions.BrickWidthToLDU(2), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat B_3x1 = new BrickFormat("3622.dat", Conversions.BrickWidthToLDU(3), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat B_4x1 = new BrickFormat("3010.dat", Conversions.BrickWidthToLDU(4), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat B_6x1 = new BrickFormat("3009.dat", Conversions.BrickWidthToLDU(6), Conversions.BrickWidthToLDU(1));

        /// <summary>
        /// Wide bricks
        /// </summary>
        public static readonly BrickFormat B_2x2 = new BrickFormat("3003.dat", Conversions.BrickWidthToLDU(2), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat B_3x2 = new BrickFormat("3002.dat", Conversions.BrickWidthToLDU(3), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat B_4x2 = new BrickFormat("3001.dat", Conversions.BrickWidthToLDU(4), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat B_6x2 = new BrickFormat("2456.dat", Conversions.BrickWidthToLDU(6), Conversions.BrickWidthToLDU(2));

        /// <summary>
        /// Plates
        /// </summary>
        public static readonly BrickFormat P_1x1 = new BrickFormat("3024.dat", Conversions.BrickWidthToLDU(1), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat P_2x1 = new BrickFormat("3023.dat", Conversions.BrickWidthToLDU(2), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat P_3x1 = new BrickFormat("3623.dat", Conversions.BrickWidthToLDU(3), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat P_4x1 = new BrickFormat("3710.dat", Conversions.BrickWidthToLDU(4), Conversions.BrickWidthToLDU(1));
        public static readonly BrickFormat P_6x1 = new BrickFormat("3666.dat", Conversions.BrickWidthToLDU(6), Conversions.BrickWidthToLDU(1));

        /// <summary>
        /// Wide Plates
        /// </summary>
        public static readonly BrickFormat P_2x2 = new BrickFormat("3022.dat", Conversions.BrickWidthToLDU(2), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat P_3x2 = new BrickFormat("3021.dat", Conversions.BrickWidthToLDU(3), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat P_4x2 = new BrickFormat("3020.dat", Conversions.BrickWidthToLDU(4), Conversions.BrickWidthToLDU(2));
        public static readonly BrickFormat P_6x2 = new BrickFormat("3795.dat", Conversions.BrickWidthToLDU(6), Conversions.BrickWidthToLDU(2));
    }
}
