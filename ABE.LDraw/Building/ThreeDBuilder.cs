using ABE.LDraw.File;
using System.Linq;

namespace ABE.LDraw.Building
{
    public sealed class ThreeDBuilder
    {
        private TwoDBuilder[] _builders;

        public ThreeDBuilder(int x, int y, int z, int maxBrickLength, bool plates)
        {
            _builders = new TwoDBuilder[y];
            TwoDBuilder lastBuilder = null;
            for (int i = y-1; i >=0; i--)
            {
                _builders[i] = new TwoDBuilder(x, z, i, lastBuilder) { MaxLength = maxBrickLength, Plates = plates };
                lastBuilder = _builders[i];
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

        public void BuildTo(LdrFileBuilder file)
        {
            file.Comment("3D brick builder output");
            foreach (var twoD in _builders.Reverse())
            {
                file.Comment("Y = " + twoD.Height);
                twoD.BuildTo(file);
            }
        }
    }
}
