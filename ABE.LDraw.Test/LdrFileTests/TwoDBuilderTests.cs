using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABE.LDraw.File;
using ABE.LDraw.Building;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public sealed class TwoDBuilderTests : BaseLdrFileTests
    {
        [TestMethod]
        public void T()
        {
            var builder = new TwoDBuilder(10, 10, 0);
            builder[0, 0] = BrickColor.LightGray;
            builder[0, 1] = BrickColor.DarkPink;
            builder[1, 2] = BrickColor.DarkTurquoise;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual("1 7 10 0 10 1 0 0 0 1 0 0 0 1 3005.dat", lines[0]);
            Assert.AreEqual("1 5 10 0 30 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);
            Assert.AreEqual("1 3 30 0 50 1 0 0 0 1 0 0 0 1 3005.dat", lines[2]);

            //Save("2DBuilderTest1");
        }

        [TestMethod]
        public void T2()
        {
            var builder = new TwoDBuilder(10, 10, 0);
            builder[0, 0] = BrickColor.LightGray;
            builder[0, 1] = BrickColor.LightGray;
            builder[1, 0] = BrickColor.LightGray;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual("1 7 20 0 10 1 0 0 0 1 0 0 0 1 3004.dat", lines[0]);
            Assert.AreEqual("1 7 10 0 30 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);

            //Save("2DBuilderTest2");
        }

        [TestMethod]
        public void T3()
        {
            var builder = new TwoDBuilder(10, 10, 1);
            builder[0, 0] = BrickColor.LightGray;
            builder[0, 1] = BrickColor.LightGray;
            builder[1, 0] = BrickColor.LightGray;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual("1 7 10 24 20 0 0 1 0 1 0 -1 0 0 3004.dat", lines[0]);
            Assert.AreEqual("1 7 30 24 10 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);

            //Save("2DBuilderTest3");
        }

        [TestMethod]
        public void T2_2x2()
        {
            var builder = new TwoDBuilder(2, 2, 0);
            builder[0, 0] = BrickColor.LightGray;
            builder[0, 1] = BrickColor.LightGray;
            builder[1, 0] = BrickColor.LightGray;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(2, lines.Length);
            Assert.AreEqual("1 7 20 0 10 1 0 0 0 1 0 0 0 1 3004.dat", lines[0]);
            Assert.AreEqual("1 7 10 0 30 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);
        }

        [TestMethod]
        public void T4()
        {
            var builder = new TwoDBuilder(10, 10, 0) { MaxLength = 3 };
            builder[1, 1] = BrickColor.LightGray;
            builder[1, 0] = BrickColor.LightGray;
            builder[2, 0] = BrickColor.LightGray;
            builder[3, 0] = BrickColor.LightGray;
            builder[4, 0] = BrickColor.LightGray;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual("1 7 50 0 10 1 0 0 0 1 0 0 0 1 3622.dat", lines[0]);
            Assert.AreEqual("1 7 30 0 30 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);
            Assert.AreEqual("1 7 90 0 10 1 0 0 0 1 0 0 0 1 3005.dat", lines[2]);

            //Save("2DBuilderTest4");
        }

        [TestMethod]
        public void T5()
        {
            var builder = new TwoDBuilder(10, 10, 1) { MaxLength = 3 };
            builder[1, 1] = BrickColor.LightGray;
            builder[0, 1] = BrickColor.LightGray;
            builder[0, 2] = BrickColor.LightGray;
            builder[0, 3] = BrickColor.LightGray;
            builder[0, 4] = BrickColor.LightGray;

            builder.BuildTo(_file);

            var lines = _file.Build();
            Assert.AreEqual(3, lines.Length);
            Assert.AreEqual("1 7 10 24 50 0 0 1 0 1 0 -1 0 0 3622.dat", lines[0]);
            Assert.AreEqual("1 7 10 24 90 1 0 0 0 1 0 0 0 1 3005.dat", lines[1]);
            Assert.AreEqual("1 7 30 24 30 1 0 0 0 1 0 0 0 1 3005.dat", lines[2]);

            //Save("2DBuilderTest5");
        }
    }

    [TestClass]
    public sealed class ThreeDBuilderTests : BaseLdrFileTests
    {
        [TestMethod]
        public void T()
        {
            var builder = new ThreeDBuilder(10, 10, 10, 2);
            builder[0, 0, 0] = BrickColor.LightGray;
            builder[0, 1, 1] = BrickColor.DarkPink;
            builder[1, 2, 2] = BrickColor.DarkTurquoise;

            builder.BuildTo(_file);

            Save("3DBuilderTest");
        }

        [TestMethod]
        public void Sphere()
        {
            int size = 30;
            int halfPos = (size - 1) / 2;
            int max = Conversions.BrickHeightToLDU(halfPos - 2).AsInteger;
            max = max * max;

            var builder = new ThreeDBuilder(size, size, size, 4);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        var x1 = Conversions.BrickWidthToLDU(x - halfPos).AsInteger;
                        var y1 = Conversions.BrickHeightToLDU(y - halfPos).AsInteger;
                        var z1 = Conversions.BrickWidthToLDU(z - halfPos).AsInteger;

                        var d2 = x1 * x1 + y1 * y1 + z1 * z1;

                        if ((d2 < max) && (d2 > max * 5 / 6)) //this thins it out here
                        {
                            builder[x, y, z] = BrickColor.Red;
                        }
                    }
                }
            }

            builder.BuildTo(_file);

            Save("3DSphere");
        }

        [TestMethod]
        public void SuperSphere()
        {
            int size = 60;
            int halfPos = (size - 1) / 2;
            int max = Conversions.BrickHeightToLDU(halfPos - 2).AsInteger;
            max = max * max;

            var builder = new ThreeDBuilder(size, size, size, 4);

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    for (int z = 0; z < size; z++)
                    {
                        var x1 = Conversions.BrickWidthToLDU(x - halfPos).AsInteger;
                        var y1 = Conversions.BrickHeightToLDU(y - halfPos).AsInteger;
                        var z1 = Conversions.BrickWidthToLDU(z - halfPos).AsInteger;

                        var d2 = x1 * x1 + y1 * y1 + z1 * z1;

                        if ((d2 < max) && (d2 > max * 5 / 6)) //this thins it out here
                        {
                            builder[x, y, z] = BrickColor.Yellow;
                        }
                    }
                }
            }

            builder.BuildTo(_file);

            Save("Super3DSphere");
        }
    }


}
