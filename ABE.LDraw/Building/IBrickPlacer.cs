using ABE.LDraw.File;

namespace ABE.LDraw.Building
{
    public interface IBrickPlacer
    {
        void Place(int pos, int length, BrickColor color);
    }
}
