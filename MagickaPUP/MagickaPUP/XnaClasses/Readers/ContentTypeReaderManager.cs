using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.PhysicsEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Readers
{
    public class ContentTypeReaderManager
    {
        // TODO : Replace all of these placeholder / dummy new() expressions with actual reader classes so that we can properly read the contents of each of these objects.
        public Dictionary<ContentTypeReader, object> contentTypeReaders = new() {
            { new ContentTypeReader(), new XnaObject() }, // TODO : Get rid of this shit maybe?
            { new ContentTypeReader("Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral", 0), new CharacterTemplate() },
            { new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0), new EffectAdditive() },
            { new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0), new EffectDeferred() },
            { new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0), new EffectDeferredLiquid() },
            { new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0), new EffectLava() },
            { new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0), new BiTreeModel() },
            { new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0), new LevelModel() },
            { new ContentTypeReader("Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka", 0), new PhysicsEntityTemplate() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.IndexBufferReader", 0), new IndexBuffer() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0), new Model() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0), new Texture2D() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0), new VertexBuffer() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0), new VertexDeclaration() },
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0), default(string) }, // TODO : Implement a reader class...
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]", 0), new List<Vec3>() }, // TODO : Implement reader class for generic list reading and for specific List<T> reading, such as the list of vec3...
            { new ContentTypeReader("Microsoft.Xna.Framework.Content.Vector3Reader", 0), new Vec3() },
        };

        public object Get(string name, int version)
        {
            return Get(new ContentTypeReader(name, version));
        }

        public object Get(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return this.contentTypeReaders[contentType];
            throw new Exception($"The ContentTypeReader \"{contentType.Name}\" is not implemented yet!"); // If the requested content type reader does not exist within the dictionary, then we just say it is not supported yet and bail out.
            // return null;
        }

        public void Add(string name, int version, object obj) // TODO : Same as below for the "obj" param...
        {
            Add(new ContentTypeReader(name, version), obj);
        }

        public void Add(ContentTypeReader contentTypeReader, object content) // TODO : Rename "content" for "reader" or "readerInstance" something like that
        {
            if (!this.contentTypeReaders.ContainsKey(contentTypeReader))
                this.contentTypeReaders.Add(contentTypeReader, content);
        }
    }
}
