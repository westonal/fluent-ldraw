using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABE.LDraw.Test
{
    [TestClass]
    public sealed class ConversionsTest
    {
        [TestMethod]
        public void Brick_width_to_LDU()
        {
            Assert.AreEqual(new LDU(10 * 20), Conversions.BrickWidthToLDU(10));
            Assert.AreEqual(new LDU(20), Conversions.BrickWidthToLDU(1));
        }

        [TestMethod]
        public void Brick_height_to_LDU()
        {
            Assert.AreEqual(new LDU(10 * 24), Conversions.BrickHeightToLDU(10));
            Assert.AreEqual(new LDU(24), Conversions.BrickHeightToLDU(1));
        }

        [TestMethod]
        public void Plate_height_to_LDU()
        {
            Assert.AreEqual(new LDU(10 * 8), Conversions.PlateHeightToLDU(10));
            Assert.AreEqual(new LDU(8), Conversions.PlateHeightToLDU(1));
        }

        [TestMethod]
        public void Five_to_six_rule()
        {
            //http://www.brickwiki.info/wiki/File:56RULE.PNG
            Assert.AreEqual(Conversions.BrickHeightToLDU(5), Conversions.BrickWidthToLDU(6));
        }

        [TestMethod]
        public void Five_to_two_rule()
        {
            //http://www.brickwiki.info/wiki/File:56RULE.PNG
            Assert.AreEqual(Conversions.PlateHeightToLDU(5), Conversions.BrickWidthToLDU(2));
        }

        [TestMethod]
        public void LDU_to_mm()
        {
            Assert.AreEqual(0.4f * 8, Conversions.LDUToMm(new LDU(8)));
            Assert.AreEqual(0.4f * 24, Conversions.LDUToMm(new LDU(24)));
        }

        [TestMethod]
        public void LDU_constructor()
        {
            Assert.AreEqual(200, new LDU(10 * 20).AsInteger);
        }
    }
}
