using ABE.LDraw.File;

namespace ABE.LDraw.Building
{
    public sealed class ThreeDBuilder
    {
        private TwoDBuilder[] _builders;

        public ThreeDBuilder(int x, int y, int z, int maxBrickLength, bool plates)
        {
            _builders = new TwoDBuilder[y];
            for (int i = 0; i < y; i++)
            {
                _builders[i] = new TwoDBuilder(x, z, i) { MaxLength = maxBrickLength, Plates = plates };
            }
        }

        public ThreeDBuilder(int x, int y, int z, int maxBrickLength)
            : this(x, y, z, maxBrickLength, false)
        {

        }

        public BrickColor? this[int studX, int studY, int studZ]
        {
            get
            {
                return _builders[studY][studX, studZ];
            }
            set
            {
                _builders[studY][studX, studZ] = value;
            }
        }

        public void BuildTo(LdrFileBuilder _file)
        {
            _file.Comment("3D brick builder output");
            foreach (var twoD in _builders)
            {
                _file.Comment("Y = " + twoD.Height);
                twoD.BuildTo(_file);
            }
        }
    }
}
