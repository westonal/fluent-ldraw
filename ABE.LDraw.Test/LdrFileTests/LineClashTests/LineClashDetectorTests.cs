using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABE.LDraw.Building.LineClash;

namespace ABE.LDraw.Test.LdrFileTests.LineClashTests
{
    [TestClass]
    public class LineClashDetectorTests
    {
        private LineClashDetector _detector;

        [TestInitialize]
        public void Setup()
        {
            _detector = new LineClashDetector();
        }

        [TestMethod]
        public void Does_detect_clash()
        {
            _detector.Add(LineRecord.Hoz(3, 4));
            Assert.IsTrue(_detector.Contains(LineRecord.Hoz(3, 4)));
        }

        [TestMethod]
        public void Default_does_not_detect_clash()
        {
            Assert.IsFalse(_detector.Contains(LineRecord.Hoz(3, 4)));
        }

        [TestMethod]
        public void Does_detect_clash_miss_by_x()
        {
            _detector.Add(LineRecord.Hoz(3, 4));
            Assert.IsFalse(_detector.Contains(LineRecord.Hoz(4, 4)));
        }

        [TestMethod]
        public void Does_detect_clash_miss_by_z()
        {
            _detector.Add(LineRecord.Hoz(3, 5));
            Assert.IsFalse(_detector.Contains(LineRecord.Hoz(3, 4)));
        }

        [TestMethod]
        public void Does_detect_clash_miss_by_dir()
        {
            _detector.Add(LineRecord.Hoz(3, 5));
            Assert.IsFalse(_detector.Contains(LineRecord.Vert(3, 5)));
        }

        [TestMethod]
        public void Does_detect_clash_multiple_records()
        {
            _detector.Add(LineRecord.Hoz(3, 4));
            _detector.Add(LineRecord.Hoz(3, 5));
            Assert.IsTrue(_detector.Contains(LineRecord.Hoz(3, 4)));
        }

        [TestMethod]
        public void Does_detect_clash_multiple_records_second()
        {
            _detector.Add(LineRecord.Hoz(3, 4));
            _detector.Add(LineRecord.Hoz(3, 5));
            Assert.IsTrue(_detector.Contains(LineRecord.Hoz(3, 5)));
        }
    }
}
