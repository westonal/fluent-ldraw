using System.Collections.Generic;
using System.Linq;

namespace ABE.LDraw.File
{
    internal sealed class Constant : LdrFileElementBuilder
    {
        private readonly string[] _strings;

        public Constant(IEnumerable<string> strings)
        {
            _strings = strings.ToArray();
        }

        internal override IEnumerable<string> Build()
        {
            return _strings;
        }
    }
}
