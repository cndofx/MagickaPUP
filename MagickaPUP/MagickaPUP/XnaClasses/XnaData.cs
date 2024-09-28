using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public static class XnaInfo
    {
        public enum ContentTypeReaderIndex
        {
            None = 0,
            LevelModelReader,
            BiTreeModelReader,
            VertexDeclarationReader,
            VertexBufferReader,
            IndexBufferReader,
            EffectDeferredReader,
            ModelReader,
            StringReader,
            ListVec3Reader,
            Vec3Reader,
            EffectLiquidReader
        }

        // TODO : Change this system to maybe use named properties to get the strings rather than hard coding them everywhere?
        public static ContentTypeReader[] ContentTypeReaders = {
            new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0),
            new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.IndexBufferReader", 0),
            new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.Vector3Reader", 0),
            new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0),
            new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0)
        };

        public static ContentTypeReader GetContentTypeReader(ContentTypeReaderIndex idx)
        {
            return ContentTypeReaders[(int)idx];
        }

        public static int GetContentTypeReaderIndex(string name)
        {
            for (int i = 0; i < ContentTypeReaders.Length; ++i)
            {
                if (name == ContentTypeReaders[i].Name)
                {
                    return i;
                }
            }
            return -1;
        }
    }

    public static class XnaVersion
    {
        public enum XnaVersionByte
        {
            Version_3_1 = 4,
            Version_4 = 5
        }

        public static string XnaVersionString(XnaVersionByte version)
        {
            switch (version)
            {
                default:
                    return "XNA Version Unkown";
                case XnaVersionByte.Version_3_1:
                    return "XNA Version 3.1";
                case XnaVersionByte.Version_4:
                    return "XNA Version 4.0";
            }
        }
    }
}
