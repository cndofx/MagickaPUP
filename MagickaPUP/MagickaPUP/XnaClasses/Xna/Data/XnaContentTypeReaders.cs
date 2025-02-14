using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xna.Data
{
    public static class XnaContentTypeReaders
    {
        public static readonly ContentTypeReader[] ContentTypeReaders = {
            new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.IndexBufferReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.Vector3Reader", 0),
            new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0),
        };
    }
}
