using System;

namespace ABE.LDraw.File
{
    public interface ILdrFileBuilder
    {
        IBrickBuilder Brick();
        ILdrFileBuilder Comment(string comment);
        ILdrFileBuilder Add(LdrFileElementBuilder ldrFileElement);
    }

    public interface IBrickBuilder : ILdrFileBuilder
    {
        IBrickBuilder Position(LDU ldu_x, LDU ldu_y, LDU ldx_z);
        IBrickBuilder Position(int x, int y, int z);
        IBrickBuilder Color(BrickColor color);

        LdrFileElementBuilder Freeze();

        IBrickBuilder Paste();

        IBrickBuilder File(Bricks.BrickFormat format);

        IBrickBuilder PositionByStud(int studX, int studY, int studZ);

        IBrickBuilder PositionByStud(int studX, LDU lduHeight, int studZ);

        IBrickBuilder RotateY(int angle);
    }
}
