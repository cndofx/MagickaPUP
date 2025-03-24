using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;

namespace MagickaPUP.Utility.IO
{
    public class Context
    {
        public DebugLogger Logger { get; protected set; }
        public List<ContentTypeReader> ContentTypeReaders { get; protected set; }

        public Context()
        {
            this.Logger = null;
            this.ContentTypeReaders = new List<ContentTypeReader>();
        }
    }

    public class ReadContext : Context
    {
        public MBinaryReader Reader { get; private set; }

        public ReadContext(MBinaryReader reader, DebugLogger logger)
        {
            this.Reader = reader;
            this.Logger = logger;
        }

    }

    public class WriteContext : Context
    {
        public MBinaryWriter Writer { get; private set; }

        public WriteContext(MBinaryWriter writer, DebugLogger logger)
        {
            this.Writer = writer;
            this.Logger = logger;
        }
    }
}
