using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Effects;
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
        public Dictionary<ContentTypeReader, object> contentTypeReaders = new() {
            { new ContentTypeReader(), new XnaObject() },
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
        };

        public object Get(string name, int version)
        {
            return Get(new ContentTypeReader(name, version));
        }

        public object Get(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return this.contentTypeReaders[contentType];
            return null;
        }

        public void Add(ContentTypeReader contentTypeReader, object content)
        {
            if (!this.contentTypeReaders.ContainsKey(contentTypeReader))
                this.contentTypeReaders.Add(contentTypeReader, content);
        }
    }
}
