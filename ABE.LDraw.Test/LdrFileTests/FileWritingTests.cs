using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABE.LDraw.File;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public sealed class FileWritingTests : BaseLdrFileTests
    {
        [TestMethod]
        public void Build_a_wall()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            for (int i = 0; i < 10; i++)
            {
                builder.Position(Conversions.BrickWidthToLDU(i), LDU.Zero, LDU.Zero).Paste();
            }
            Save("walla.ldr");
        }

        [TestMethod]
        public void Build_a_wall_2()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow).File(Bricks.B_2x1);
            for (int i = 0; i < 10; i++)
            {
                builder.Position(Conversions.BrickWidthToLDU(i * 2), LDU.Zero, LDU.Zero).Paste();
            }
            Save("wallb.ldr");
        }

        [TestMethod]
        public void Build_a_wall_3()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            for (int i = 0; i < 10; i++)
            {
                builder.PositionByStud(i, 0, 0).Paste();
            }
            builder.File(Bricks.B_2x1).Color(BrickColor.Green);
            for (int i = 0; i < 10; i++)
            {
                builder.PositionByStud(i * 2, 0, 1).Paste();
            }
            Save("wallc.ldr");
        }

        [TestMethod]
        public void Build_a_wall_4()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            builder.File(Bricks.B_2x1).Color(BrickColor.Green);
            for (int y = 0; y < 10; y++)
            {
                for (int i = 0; i < 10; i++)
                {
                    builder.PositionByStud(i * 2 + y % 2, y, 1).Paste();
                }
            }
            Save("walld.ldr");
        }

        [TestMethod]
        public void Build_a_wall_4_rotate()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            builder.File(Bricks.B_2x1).Color(BrickColor.Green).RotateY(90);
            for (int y = 0; y < 10; y++)
            {
                for (int i = 0; i < 10; i++)
                {
                    builder.PositionByStud(1, y, i * 2 + y % 2).Paste();
                }
            }
            Save("wall_4_rot.ldr");
        }

        [TestMethod]
        public void Build_a_wall_5()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            for (int i = 0; i < 10; i++)
            {
                builder.PositionByStud(i, 0, 0).Paste();
            }
            builder.File(Bricks.B_2x1).Color(BrickColor.Green);
            for (int i = 0; i < 10; i++)
            {
                builder.PositionByStud(i * 2, 0, 1).Paste();
            }
            builder.RotateY(90);
            for (int i = 0; i < 10; i++)
            {
                builder.PositionByStud(i, 0, 2).Paste();
            }
            Save("wall_5.ldr");
        }

        [TestMethod]
        public void Build_a_wall_6()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            builder.File(Bricks.B_3x1).Color(BrickColor.Green).RotateY(90);
            for (int y = 0; y < 10; y++)
            {
                for (int i = 0; i < 10; i++)
                {
                    builder.PositionByStud(1, y, i * 3 + y % 2).Paste();
                }
            }
            Save("wall_6.ldr");
        }

        [TestMethod]
        public void Build_a_wall_7()
        {
            var builder = _file.NewBrickBuilder().Color(BrickColor.Yellow);
            builder.File(Bricks.B_4x1).Color(BrickColor.Green).RotateY(90);
            for (int y = 0; y < 10; y++)
            {
                for (int i = 0; i < 10; i++)
                {
                    builder.PositionByStud(1, y, i * 4 + 2 * (y % 2)).Paste();
                }
            }
            Save("wall_7.ldr");
        }
    }
}
