using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw.File
{
    public abstract class LdrFileElementBuilder
    {
        internal abstract IEnumerable<string> Build();

        public LdrFileElementBuilder Freeze()
        {
            return new Constant(Build());
        }
    }

    public abstract class LdrFileElementBuilderRepeat : LdrFileElementBuilder, ILdrFileBuilder
    {
        private readonly ILdrFileBuilder _ldrFileBuilder;

        public LdrFileElementBuilderRepeat(ILdrFileBuilder ldrFileBuilder)
        {
            _ldrFileBuilder = ldrFileBuilder;
        }

        protected ILdrFileBuilder LdrFileBuilder { get { return _ldrFileBuilder; } }

        public IBrickBuilder Brick()
        {
            return _ldrFileBuilder.Brick();
        }

        public ILdrFileBuilder Comment(string comment)
        {
            return _ldrFileBuilder.Comment(comment);
        }

        public ILdrFileBuilder Add(LdrFileElementBuilder ldrFileElement)
        {
            return _ldrFileBuilder.Add(ldrFileElement);
        }
    }
}
