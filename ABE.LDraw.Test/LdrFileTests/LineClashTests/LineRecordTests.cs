using ABE.LDraw.Building.LineClash;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ABE.LDraw.Test.LdrFileTests.LineClashTests
{
    [TestClass]
    public sealed class LineRecordTests
    {
        [TestMethod]
        public void Equals()
        {
            var rec1 = LineRecord.XFacing(1, 2);
            var rec2 = LineRecord.XFacing(1, 2);
            Assert.AreEqual(rec1, rec2);
            Assert.AreEqual(rec1.GetHashCode(), rec2.GetHashCode());
        }

        [TestMethod]
        public void NotEquals_by_x()
        {
            var rec1 = LineRecord.XFacing(1, 2);
            var rec2 = LineRecord.XFacing(2, 2);
            Assert.AreNotEqual(rec1, rec2);
            Assert.AreNotEqual(rec1.GetHashCode(), rec2.GetHashCode());
        }

        [TestMethod]
        public void NotEquals_by_z()
        {
            var rec1 = LineRecord.XFacing(1, 2);
            var rec2 = LineRecord.XFacing(1, 3);
            Assert.AreNotEqual(rec1, rec2);
            Assert.AreNotEqual(rec1.GetHashCode(), rec2.GetHashCode());
        }

        [TestMethod]
        public void NotEquals_by_dir()
        {
            var rec1 = LineRecord.XFacing(1, 2);
            var rec2 = LineRecord.YFacing(1, 2);
            Assert.AreNotEqual(rec1, rec2);
            Assert.AreNotEqual(rec1.GetHashCode(), rec2.GetHashCode());
        }
    }
}
