using ABE.LDraw.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public sealed class BrickLdrFileTests : BaseLdrFileTests
    {
        [TestMethod]
        public void Can_add_brick()
        {
            _file.Brick();
            var fileText = _file.Build();
            Assert.AreEqual("1 0 0 -16 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
        }

        [TestMethod]
        public void Can_position_brick()
        {
            _file.Brick().Position(1, 2, 3);
            var fileText = _file.Build();
            Assert.AreEqual("1 0 1 2 3 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
        }

        [TestMethod]
        public void Can_color_brick()
        {
            _file.Brick().Color(BrickColor.Red);
            var fileText = _file.Build();
            Assert.AreEqual("1 4 0 -16 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
        }

        [TestMethod]
        public void Can_add_brick_detached()
        {
            IBrickBuilder detachedBuilder = _file.NewBrickBuilder();
            var fileText = _file.Build();
            _file.Add(detachedBuilder.Freeze());
            detachedBuilder.Color(BrickColor.BrightGreen);
            fileText = _file.Build();
            Assert.AreEqual("1 0 0 -16 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
        }

        [TestMethod]
        public void NewBrickBuilder_adds_nothing()
        {
            IBrickBuilder detachedBuilder = _file.NewBrickBuilder();
            var fileText = _file.Build();
            Assert.AreEqual(0, fileText.Length);
        }

        [TestMethod]
        public void Can_add_brick_detached_by_paste_call()
        {
            IBrickBuilder detachedBuilder = _file.NewBrickBuilder();
            var fileText = _file.Build();
            detachedBuilder.Paste();
            detachedBuilder.Color(BrickColor.BrightGreen);
            fileText = _file.Build();
            Assert.AreEqual("1 0 0 -16 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
        }

        [TestMethod]
        public void Can_add_10_bricks_detached_by_paste_call()
        {
            IBrickBuilder detachedBuilder = _file.NewBrickBuilder();
            var fileText = _file.Build();
            for (int i = 0; i < 10; i++)
            {
                detachedBuilder.Position(i, 0, 0).Paste();
            }
            fileText = _file.Build();
            Assert.AreEqual(10, fileText.Length);
            Assert.AreEqual("1 0 0 0 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[0]);
            Assert.AreEqual("1 0 1 0 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[1]);
            Assert.AreEqual("1 0 9 0 0 1 0 0 0 1 0 0 0 1 3005.dat", fileText[9]);
        }
    }
}
