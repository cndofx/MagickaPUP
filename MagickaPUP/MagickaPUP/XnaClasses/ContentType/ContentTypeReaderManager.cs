using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.XnaClasses.ContentType.Writers;
using MagickaPUP.XnaClasses.Specific;
using MagickaPUP.XnaClasses.Specific.Derived;
using System;
using System.Collections.Generic;

namespace MagickaPUP.XnaClasses.Readers
{
    public class ContentTypeReaderManager
    {
        public struct TypeData
        {
            public ContentTypeReader ContentTypeReader;
            public TypeReader<object> TypeReader;
            public TypeWriter<object> TypeWriter;
        }

        public TypeData THING = new TypeData() {
            ContentTypeReader = new ContentTypeReader(),
            TypeReader = new CharacterTemplateReader(),
            TypeWriter = null
        };

        // TODO : Replace all of these placeholder / dummy new() expressions with actual reader classes so that we can properly read the contents of each of these objects.
        public Dictionary<ContentTypeReader, object> contentTypeReaders = new() {
            {
                new ContentTypeReader("Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral", 0),
                new CharacterTemplateReader()
            },
            {
                new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                new EffectAdditiveReader()
            },
            {
                new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                new EffectDeferredReader()
            },
            {
                new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0),
                new EffectDeferredLiquidReader()
            },
            {
                new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                new EffectLavaReader()
            },
            {
                new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0),
                new BiTreeModelReader()
            },
            {
                new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0),
                new LevelModelReader()
            },
            {
                new ContentTypeReader("Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka", 0),
                new PhysicsEntityTemplateReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.IndexBufferReader", 0),
                new IndexBufferReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0),
                new ModelReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0),
                new Texture2DReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0),
                new VertexBufferReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0),
                new VertexDeclarationReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0),
                new StringReader()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]", 0),
                new ListReader<Vec3>()
            },
            {
                new ContentTypeReader("Microsoft.Xna.Framework.Content.Vector3Reader", 0),
                new Vector3Reader()
            },
        };
        // TODO : Maybe with these changes, we can get rid of the whole XnaObject class and the WriteInstance() and ReadInstance() methods?

        public Dictionary<Type, ContentTypeReader> contentTypeWriters = new()
        {
            { typeof(string), new ContentTypeReader() }
        };

        public TypeReader<object> Get(string name, int version)
        {
            return Get(new ContentTypeReader(name, version));
        }

        public TypeReader<object> Get(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return (TypeReader<object>)this.contentTypeReaders[contentType];
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
