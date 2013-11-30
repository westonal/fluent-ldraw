using ABE.LDraw.File;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public sealed class BasicLdrFileTests : BaseLdrFileTests
    {
        [TestMethod]
        public void Can_add_comment()
        {
            _file.Add(new Comment("Hello"));
            var fileText = _file.Build();
            Assert.AreEqual("0 Hello", fileText[0]);
        }

        [TestMethod]
        public void Can_add_comment_direct()
        {
            _file.Comment("Test");
            var fileText = _file.Build();
            Assert.AreEqual("0 Test", fileText[0]);
        }
    }
}
