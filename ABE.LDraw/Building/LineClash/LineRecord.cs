namespace ABE.LDraw.Building.LineClash
{
    public class LineRecord
    {
        private int _x;
        private int _z;
        private Dir _dir;
        private enum Dir { X, Z }

        private LineRecord(int x, int z, Dir dir)
        {
            _x = x;
            _z = z;
            _dir = dir;
        }

        public static LineRecord XFacing(int x, int z)
        {
            return new LineRecord(x, z, Dir.X);
        }

        public static LineRecord YFacing(int x, int z)
        {
            return new LineRecord(x, z, Dir.Z);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as LineRecord);
        }

        public bool Equals(LineRecord other)
        {
            if (other == null) return false;
            return other._x == _x && other._z == _z && other._dir == _dir;
        }

        public override int GetHashCode()
        {
            var hash = _x;
            hash = hash * 31 + _z;
            hash = hash * 31 + (_dir == Dir.X ? 1 : 2);
            return hash;
        }
    }
}
