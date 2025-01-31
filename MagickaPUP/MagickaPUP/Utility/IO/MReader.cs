using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;

namespace MagickaPUP.Utility.IO
{
    public class MBinaryReader : BinaryReader/*, IMReader*/
    {
        #region Variables

        private List<ContentTypeReader> contentTypeReaders;

        #endregion

        #region Properties

        /*
        public int ContentTypeReadersCount { get { return contentTypeReaders.Count; } }

        public ContentTypeReader[] ContentTypeReaders { get { return contentTypeReaders.ToArray(); } }
        */

        public List<ContentTypeReader> ContentTypeReaders { get { return this.contentTypeReaders; } }

        #endregion

        #region Constructor

        public MBinaryReader(Stream stream) : base(stream)
        {
            this.contentTypeReaders = new List<ContentTypeReader>();
        }

        #endregion

        #region Public Methods

        public new int Read7BitEncodedInt()
        {
            return base.Read7BitEncodedInt();
        }

        /*
        public T ReadObject<T>()
        {
            int code = this.Read7BitEncodedInt();

            string s = contentTypeReaders[code - 1].Name; // Subtract 1 because the indices start at 1 on the XNB file but they start at 0 in C#.

            object obj = default(T);

            // Yes, I know I should make a hard coded list with these so that I can switch on index, and assign my indices to the readers list
            // rather than making them store strings, but I don't have the time to refact that right now, so we'll deal with the overhead until
            // I find the time to fix that. Besides, with this current (bad) implementation, at least I don't have to touch anything to be able
            // to read out the name of readers that my code does not support yet, so eh... all in all, C# makes things a fucking headache.
            // For now, please, forgive my sins.
            switch (s)
            {
                case "Magicka.ContentReaders.LevelModelReader, Magicka":
                    obj = this.ReadVec3();
                    break;
                case "PolygonHead.Pipeline.BiTreeModelReader, PolygonHead":
                    break;
                case "Microsoft.Xna.Framework.Content.VertexDeclarationReader":
                    break;
                case "Microsoft.Xna.Framework.Content.VertexBufferReader":
                    break;
                case "Microsoft.Xna.Framework.Content.IndexBufferReader":
                    break;
                case "PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral":
                    break;
                case "Microsoft.Xna.Framework.Content.ModelReader":
                    break;
                case "Microsoft.Xna.Framework.Content.StringReader":
                    break;
                case "Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]":
                    break;
                case "Microsoft.Xna.Framework.Content.Vector3Reader":
                    break;
                case "PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead":
                    break;
            }

            return (T)obj;
        }
        */

        /*
        public ContentTypeReader ReadContentTypeReader()
        {
            ContentTypeReader ans = ContentTypeReader.Read(this);
            this.contentTypeReaders.Add(ans);
            return ans;
        }

        public void ReadContentTypeReaders()
        {
            int typeReaderCount = this.Read7BitEncodedInt();
            for (int i = 0; i < typeReaderCount; ++i)
            {
                string name = this.ReadString();
                int version = this.ReadInt32();
                this.contentTypeReaders.Add(new ContentTypeReader(name, version));
            }
        }

        public Vec3 ReadVec3()
        {
            return Vec3.Read(this);
        }
        */

        #endregion

        #region PrivateMethods

        #endregion
    }

    public class MStreamReader : StreamReader
    {
        MStreamReader(Stream stream) : base(stream)
        { }
    }

}
