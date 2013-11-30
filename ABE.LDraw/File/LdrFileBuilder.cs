using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ABE.LDraw.File
{
    public sealed class LdrFileBuilder : ABE.LDraw.File.ILdrFileBuilder
    {
        private readonly List<LdrFileElementBuilder> _elements = new List<LdrFileElementBuilder>();

        public string[] Build()
        {
            return _elements.SelectMany(x => x.Build()).ToArray();
        }

        public ILdrFileBuilder Add(LdrFileElementBuilder element)
        {
            _elements.Add(element);
            return this;
        }

        public ILdrFileBuilder Comment(string comment)
        {
            return Add(new Comment(comment));
        }

        public IBrickBuilder Brick()
        {
            var builder = new BrickBuilder(this);
            Add((LdrFileElementBuilder)builder);
            return builder;
        }

        public IBrickBuilder NewBrickBuilder()
        {
            return new BrickBuilder(this);
        }
    }
}
