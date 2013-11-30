using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw.File
{
    public sealed class Comment : LdrFileElementBuilder
    {
        private string _commentText;

        public Comment(string commentText)
        {
            _commentText = commentText;
        }

        public override string ToString()
        {
            return _commentText;
        }

        internal override IEnumerable<string> Build()
        {
            return new []{string.Format("0 {0}", _commentText)};
        }
    }
}
