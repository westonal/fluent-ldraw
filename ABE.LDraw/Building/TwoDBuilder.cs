using System.Collections.Generic;
using System.Linq;
using ABE.LDraw.File;

namespace ABE.LDraw.Building
{
    public sealed class TwoDBuilder
    {
        private BrickColor?[,] _array;
        private int _height;

        public TwoDBuilder(int x, int z, int height)
        {
            _array = new BrickColor?[x, z];
            _height = height;
            MaxLength = 2;
        }

        public BrickColor? this[int studX, int studZ]
        {
            get
            {
                return _array[studX, studZ];
            }
            set
            {
                _array[studX, studZ] = value;
            }
        }

        public void BuildTo(LdrFileBuilder file)
        {
            //clone array
            var clone = (BrickColor?[,])_array.Clone();

            LDU tHeight = Plates ? Conversions.PlateHeightToLDU(_height) : Conversions.BrickHeightToLDU(_height);

            if (_height % 2 == 0)
            {
                FillMax(file, clone, tHeight, MaxLength, WideBricksToUse);
                FillMax(file, clone, tHeight, MaxLength, RegularBricksToUse);
                FillMaxRotate(file, clone, tHeight, MaxLength, WideBricksToUse);
                FillMaxRotate(file, clone, tHeight, MaxLength, RegularBricksToUse);
            }
            else
            {
                FillMaxRotate(file, clone, tHeight, MaxLength, WideBricksToUse);
                FillMaxRotate(file, clone, tHeight, MaxLength, RegularBricksToUse);
                FillMax(file, clone, tHeight, MaxLength, WideBricksToUse);
                FillMax(file, clone, tHeight, MaxLength, RegularBricksToUse);
            }

            Fill1x1(file, clone, tHeight, RegularBricksToUse);
        }

        private Dictionary<int, Bricks.BrickFormat> RegularBricksToUse { get { return Plates ? AllPlates : AllBricks; } }
        private Dictionary<int, Bricks.BrickFormat> WideBricksToUse { get { return Plates ? WidePlates : WideBricks; } }
        //private Dictionary<int, Bricks.BrickFormat>[] BricksToUse { get { return Plates ? new[] { AllPlates } : new[] { AllBricks }; } }

        private class Placer : IBrickPlacer
        {
            private readonly IBrickBuilder _brick;
            private readonly BrickColor?[,] _tempArray;
            private readonly LDU _height;
            private readonly int _fixedParam;
            private readonly bool _fixedIsX;
            private readonly Dictionary<int, Bricks.BrickFormat> _bricks;

            public Placer(IBrickBuilder brick, BrickColor?[,] array, LDU height, Dictionary<int, Bricks.BrickFormat> bricks, int fixedParam, bool fixedIsX)
            {
                _brick = brick;
                _tempArray = array;
                _height = height;
                _bricks = bricks;
                _fixedParam = fixedParam;
                _fixedIsX = fixedIsX;
            }

            public void Place(int pos, int length, BrickColor color)
            {
                //select brick to use based on length
                var brick = _bricks[length];
                _brick.File(brick).Color(color);

                if (_fixedIsX)
                {
                    _brick.PositionByStud(_fixedParam, _height, pos).Paste();
                    //null out values
                    for (int d = _fixedParam; d < (_fixedParam + Conversions.LDUToBrickWidth(brick.Depth)); d++)
                    {
                        for (int z = pos; z < (pos + length); z++)
                        {
                            _tempArray[d, z] = null;
                        }
                    }
                }
                else
                {
                    _brick.PositionByStud(pos, _height, _fixedParam).Paste();
                    //null out values
                    for (int d = _fixedParam; d < (_fixedParam + Conversions.LDUToBrickWidth(brick.Depth)); d++)
                    {
                        for (int x = pos; x < (pos + length); x++)
                        {
                            _tempArray[x, d] = null;
                        }
                    }
                }
            }
        }

        private static Dictionary<int, Bricks.BrickFormat> AllBricks = new Dictionary<int, Bricks.BrickFormat>() {
        { 1, Bricks.B_1x1 },
        { 2, Bricks.B_2x1 },
        { 3, Bricks.B_3x1 },
        { 4, Bricks.B_4x1 } };

        private static Dictionary<int, Bricks.BrickFormat> WideBricks = new Dictionary<int, Bricks.BrickFormat>() {
        { 2, Bricks.B_2x2 },
        { 3, Bricks.B_3x2 },
        { 4, Bricks.B_4x2 } };

        private static Dictionary<int, Bricks.BrickFormat> AllPlates = new Dictionary<int, Bricks.BrickFormat>() {
        { 1, Bricks.P_1x1 },
        { 2, Bricks.P_2x1 },
        { 3, Bricks.P_3x1 },
        { 4, Bricks.P_4x1 } };

        private static Dictionary<int, Bricks.BrickFormat> WidePlates = new Dictionary<int, Bricks.BrickFormat>() {
        { 2, Bricks.P_2x2 },
        { 3, Bricks.P_3x2 },
        { 4, Bricks.P_4x2 } };

        private static void FillMax(LdrFileBuilder file, BrickColor?[,] array, LDU height, int maxLength, Dictionary<int, Bricks.BrickFormat> bricks)
        {
            var brick = file.NewBrickBuilder();

            var step = Conversions.LDUToBrickWidth(bricks.First().Value.Depth);

            for (int z = 0; z <= array.GetUpperBound(1); z += step)
            {
                var placer = new Placer(brick, array, height, bricks, z, false);

                //state machine
                var builder = new BrickBuilderStateMachine(placer) { MaxLength = maxLength, MinLength = 2 };

                for (int x = 0; x <= array.GetUpperBound(0); x++)
                {
                    if (step == 2)
                    {
                        if (array.GetUpperBound(1) > z && array[x, z] == array[x, z + 1])
                        {
                            builder.PushStud(array[x, z]);
                        }
                        else
                        {
                            builder.PushStud(null);
                        }
                    }
                    else
                    {
                        builder.PushStud(array[x, z]);
                    }
                }

                builder.EndPlacement();
            }
        }

        private static void FillMaxRotate(LdrFileBuilder file, BrickColor?[,] array, LDU height, int maxLength, Dictionary<int, Bricks.BrickFormat> bricks)
        {
            var brick = file.NewBrickBuilder().RotateY(90);

            var step = Conversions.LDUToBrickWidth(bricks.First().Value.Depth);

            for (int x = 0; x <= array.GetUpperBound(0); x += step)
            {
                var placer = new Placer(brick, array, height, bricks, x, true);

                //state machine
                var builder = new BrickBuilderStateMachine(placer) { MaxLength = maxLength, MinLength = 2 };

                for (int z = 0; z <= array.GetUpperBound(1); z++)
                {
                    if (step == 2)
                    {
                        if (array.GetUpperBound(0) > x && array[x, z] == array[x + 1, z])
                        {
                            builder.PushStud(array[x, z]);
                        }
                        else
                        {
                            builder.PushStud(null);
                        }
                    }
                    else
                    {
                        builder.PushStud(array[x, z]);
                    }
                }

                builder.EndPlacement();
            }
        }

        private static void Fill1x1(LdrFileBuilder file, BrickColor?[,] array, LDU height, Dictionary<int, Bricks.BrickFormat> bricks)
        {
            var brick = file.NewBrickBuilder().File(bricks[1]);
            for (int x = 0; x <= array.GetUpperBound(0); x++)
            {
                for (int z = 0; z <= array.GetUpperBound(1); z++)
                {
                    BrickColor? bc = array[x, z];
                    if (!bc.HasValue) continue;
                    brick.Color(bc.Value).PositionByStud(x, height, z).Paste();
                }
            }
        }

        public int Height { get { return _height; } }

        public int MaxLength { get; set; }

        public bool Plates { get; set; }
    }
}
