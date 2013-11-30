namespace ABE.LDraw.Building.LineClash
{
    public class LineRecord
    {
        private int _x;
        private int _z;
        private Dir _dir;
        private enum Dir { Hoz, Vert }

        private LineRecord(int x, int z, Dir dir)
        {
            _x = x;
            _z = z;
            _dir = dir;
        }

        public static LineRecord Hoz(int x, int z)
        {
            return new LineRecord(x, z, Dir.Hoz);
        }

        public static LineRecord Vert(int x, int z)
        {
            return new LineRecord(x, z, Dir.Vert);
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
            hash = hash * 31 + (_dir == Dir.Hoz ? 1 : 2);
            return hash;
        }

    }
}
