using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw.File
{
    public sealed class BrickBuilder : LdrFileElementBuilderRepeat, IBrickBuilder
    {
        private int _x;
        private int _y = -16;
        private int _z;
        private BrickColor _color;
        private Bricks.BrickFormat _brickFormat = Bricks.B_1x1;
        private int _angleY;

        public BrickBuilder(LdrFileBuilder ldrFileBuilder)
            : base(ldrFileBuilder)
        {
        }

        public IBrickBuilder Position(int x, int y, int z)
        {
            _x = x;
            _y = y;
            _z = z;
            return this;
        }

        public IBrickBuilder Position(LDU ldu_x, LDU ldu_y, LDU ldx_z)
        {
            return Position(ldu_x.AsInteger, ldu_y.AsInteger, ldx_z.AsInteger);
        }

        public IBrickBuilder Color(BrickColor color)
        {
            _color = color;
            return this;
        }

        internal override IEnumerable<string> Build()
        {
            string matrix;
            switch ((_angleY + 360) % 360)
            {
                default:
                case 0: matrix = "1 0 0 0 1 0 0 0 1"; break;
                case 90: matrix = "0 0 1 0 1 0 -1 0 0"; break;
                case 180: matrix = "-1 0 0 0 1 0 0 0 -1"; break;
                case 270: matrix = "0 0 -1 0 1 0 1 0 0"; break;
            }
            return new[] { string.Format("1 {0} {1} {2} {3} {4} {5}", (int)_color, _x, _y, _z, matrix, _brickFormat.FileName) };
        }

        public IBrickBuilder Paste()
        {
            LdrFileBuilder.Add(Freeze());
            return this;
        }

        public IBrickBuilder File(Bricks.BrickFormat brickFormat)
        {
            _brickFormat = brickFormat;
            return this;
        }

        public IBrickBuilder PositionByStud(int studX, int studY, int studZ)
        {
            return PositionByStud(studX, Conversions.PlateHeightToLDU(studY), studZ);
        }

        public IBrickBuilder PositionByStud(int studX, LDU lduHeight, int studZ)
        {
            var a = _brickFormat.Width.AsInteger / 2;
            var b = _brickFormat.Depth.AsInteger / 2;
            if (Is90Degree)
            {//swap
                var c = a;
                a = b;
                b = c;
            }
            return Position(
                Conversions.BrickWidthToLDU(studX) + a,
                lduHeight,
                Conversions.BrickWidthToLDU(studZ) + b);
        }

        public IBrickBuilder RotateY(int angle)
        {
            _angleY = angle;
            return this;
        }

        public bool Is90Degree
        {
            get
            {
                var round = (_angleY + 360) % 360;
                return round == 90 || round == 270;
            }
        }
    }
}
