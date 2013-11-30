using Microsoft.VisualStudio.TestTools.UnitTesting;
using ABE.LDraw.File;
using System.IO;

namespace ABE.LDraw.Test.LdrFileTests
{
    [TestClass]
    public abstract class BaseLdrFileTests
    {
        protected LdrFileBuilder _file;

        [TestInitialize]
        public void Setup()
        {
            _file = new LdrFileBuilder();
        }

        protected void Save(string filename)
        {
            var fullFileName = Path.ChangeExtension(Path.Combine(@"C:\Users\Alan\Documents\LDraw\My Models\UnitTestsOutput\", filename), ".ldr");
            System.IO.File.WriteAllLines(fullFileName, _file.Build());
        }
    }
}
