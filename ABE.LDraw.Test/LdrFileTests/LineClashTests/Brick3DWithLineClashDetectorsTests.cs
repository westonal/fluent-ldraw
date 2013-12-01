using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABE.LDraw.Building;
using ABE.LDraw.File;

namespace ABE.LDraw.Test.LdrFileTests.LineClashTests
{
    [TestClass]
    public sealed class Brick3DWithLineClashDetectorsTests : BaseLdrFileTests
    {
        private ThreeDBuilder _builder;

        [TestInitialize]
        public void Setup()
        {
            base.Setup();
            _builder = new ThreeDBuilder(10, 3, 10, 4);
        }

        [TestMethod]
        public void Standard_builder_output()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _builder[i, y * 2, 0] = BrickColor.Red;
                }
            }

            _builder.BuildTo(_file);

            var lines = _file.BuildWithoutComments();

            Assert.AreEqual(4, lines.Length);
        }

        [TestMethod]
        public void Builder_output_lines_above_one_another()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _builder[i, y, 0] = BrickColor.Red;
                }
            }

            _builder.BuildTo(_file);

            Save("Builder_output_lines_above_one_another");

            var lines = _file.BuildWithoutComments();
            Assert.AreEqual(5, lines.Length);
        }

        [TestMethod]
        public void Builder_output_lines_above_one_another_in_z()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int i = 0; i < 8; i++)
                {
                    _builder[2, y, i] = BrickColor.Red;
                }
            }

            _builder.BuildTo(_file);

            Save("Builder_output_lines_above_one_another_in_z");

            var lines = _file.BuildWithoutComments();
            Assert.AreEqual(5, lines.Length);
        }

        [TestMethod]
        public void Different_length_on_x()
        {
            for (int y = 0; y < 2; y++)
            {
                for (int i = 0; i < (y == 0 ? 5 : 4); i++)
                {
                    _builder[i, y, 2] = BrickColor.Red;
                }
            }

            _builder.BuildTo(_file);

            Save("Different_length_on_x");

            var lines = _file.BuildWithoutComments();
            Assert.AreEqual(3, lines.Length);
        }
    }
}
