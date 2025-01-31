using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryWriter : BinaryWriter/*, IMWriter*/
    {
        private int scopeDepth;
        private string newlineString;
        private string indentString;

        public MBinaryWriter(Stream stream) : base(stream)
        {
            this.scopeDepth = 0;
            this.newlineString = "\r\n";
            this.indentString = "\t";
        }

        public new void Write7BitEncodedInt(int x)
        {
            base.Write7BitEncodedInt(x);
        }

        public void WriteRaw(string s)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(s);
            Write(bytes);
        }

        public void WriteText(string s)
        {
            WriteIndent();
            WriteRaw($"{s}{this.newlineString}");
        }

        public void WriteScopeStart()
        {
            WriteText("{");
            ++scopeDepth;
        }

        public void WriteScopeEnd(string s = "")
        {
            --scopeDepth;
            WriteText($"}}{s}");
        }

        private void WriteIndent()
        {
            for (int i = 0; i < this.scopeDepth; ++i)
                WriteRaw(indentString);
        }
    }

    public class MTextWriter : StreamWriter
    {
        public MTextWriter(Stream stream) : base(stream) { }

    }
}
