using ABE.LDraw.File;

namespace ABE.LDraw.Building
{
    public interface IBrickPlacer
    {
        bool Place(int pos, int length, BrickColor color);
    }
}
