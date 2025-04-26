using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Item;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.XnaClasses.ContentType.Readers;
using MagickaPUP.XnaClasses.ContentType.Readers.Derived;
using MagickaPUP.XnaClasses.ContentType.Writers;
using MagickaPUP.XnaClasses.ContentType.Writers.Derived;
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
            public Type ContentType;
            public ContentTypeReader ContentTypeReader;
            public TypeReaderBase TypeReader;
            public TypeWriterBase TypeWriter;

            public TypeData(Type type, ContentTypeReader contentTypeReader, TypeReaderBase typeReader, TypeWriterBase typeWriter)
            {
                this.ContentType = type;
                this.ContentTypeReader = contentTypeReader;
                this.TypeReader = typeReader;
                this.TypeWriter = typeWriter;
            }
        }

        public Dictionary<ContentTypeReader, TypeReaderBase> contentTypeReaders = new();
        public Dictionary<ContentTypeReader, TypeWriterBase> contentTypeWriters = new();
        public Dictionary<Type, ContentTypeReader> contentTypeMap = new();

        public ContentTypeReaderManager()
        {
            TypeData[] defaultTypeData =
            {
                new TypeData
                (
                    typeof(CharacterTemplate),
                    new ContentTypeReader("Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral", 0),
                    new CharacterTemplateReader(),
                    new CharacterTemplateWriter()
                ),
                new TypeData
                (
                    typeof(EffectAdditive),
                    new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                    new EffectAdditiveReader(),
                    new EffectAdditiveWriter()
                ),
                new TypeData
                (
                    typeof(EffectDeferred),
                    new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                    new EffectDeferredReader(),
                    new EffectDeferredWriter()
                ),
                new TypeData
                (
                    typeof(EffectDeferredLiquid),
                    new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0),
                    new EffectDeferredLiquidReader(),
                    new EffectDeferredLiquidWriter()
                ),
                new TypeData
                (
                    typeof(EffectLava),
                    new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
                    new EffectLavaReader(),
                    new EffectLavaWriter()
                ),
                new TypeData
                (
                    typeof(BiTreeModel),
                    new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0),
                    new BiTreeModelReader(),
                    new BiTreeModelWriter()
                ),
                new TypeData
                (
                    typeof(LevelModel),
                    new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0),
                    new LevelModelReader(),
                    new LevelModelWriter()
                ),
                new TypeData
                (
                    typeof(PhysicsEntityTemplate),
                    new ContentTypeReader("Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka", 0),
                    new PhysicsEntityTemplateReader(),
                    new PhysicsEntityTemplateWriter()
                ),
                new TypeData
                (
                    typeof(IndexBuffer),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.IndexBufferReader", 0),
                    new IndexBufferReader(),
                    new IndexBufferWriter()
                ),
                new TypeData
                (
                    typeof(Model),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0),
                    new ModelReader(),
                    new ModelWriter()
                ),
                new TypeData
                (
                    typeof(Texture2D),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.Texture2DReader", 0),
                    new Texture2DReader(),
                    new Texture2DWriter()
                ),
                new TypeData
                (
                    typeof(VertexBuffer),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexBufferReader", 0),
                    new VertexBufferReader(),
                    new VertexBufferWriter()
                ),
                new TypeData
                (
                    typeof(VertexDeclaration),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.VertexDeclarationReader", 0),
                    new VertexDeclarationReader(),
                    new VertexDeclarationWriter()
                ),
                new TypeData
                (
                    typeof(string),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0),
                    new StringReader(),
                    new StringWriter()
                ),
                new TypeData
                (
                    typeof(List<Vec3>),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]", 0),
                    new ListReader<Vec3, Vector3Reader>(),
                    new ListWriter<Vec3, Vector3Writer>()
                ),
                new TypeData
                (
                    typeof(Vec3),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.Vector3Reader", 0),
                    new Vector3Reader(),
                    new Vector3Writer()
                ),
                new TypeData
                (
                    typeof(Item),
                    new ContentTypeReader("Magicka.ContentReaders.ItemReader, Magicka, Version=1.0.0.0, Culture=neutral", 0),
                    new ItemReader(),
                    new ItemWriter()
                ),
                new TypeData(
                    typeof(EffectCode),
                    new ContentTypeReader("Microsoft.Xna.Framework.Content.EffectReader", 0),
                    new EffectReader(),
                    new EffectWriter()
                ),
                /*new TypeData // Temporarily disabled since I need to figure out how XNA handles getting the correct type reader for this object type.
                (
                    typeof(Dictionary<string, object>),
                    new ContentTypeReader("TODO : FIND THE STRING LOL"),
                    new DictionaryReader<string, object, SOMETHING, SOMETHING>(),
                    new DictionaryWriter<string, object, SOMETHING, SOMETHING>()
                ),*/
            };

            AddTypeData(defaultTypeData);
        }

        public TypeReaderBase GetTypeReader(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return this.contentTypeReaders[contentType];
            throw new Exception($"The TypeReader for ContentTypeReader \"{contentType.Name}\" is not implemented yet!");
        }

        public TypeWriterBase GetTypeWriter(ContentTypeReader contentType)
        {
            if(this.contentTypeWriters.ContainsKey(contentType))
                return this.contentTypeWriters[contentType];
            throw new Exception($"The TypeWriter for ContentTypeReader \"{contentType.Name}\" is not implemented yet!");
        }

        public ContentTypeReader GetContentTypeReader(Type type)
        {
            if(this.contentTypeMap.ContainsKey(type))
                return this.contentTypeMap[type];
            throw new Exception($"Could not find a Content Type mapping for Type \"{type.Name}\"");
        }

        public void AddTypeData(TypeData typeData)
        {
            this.contentTypeReaders.Add(typeData.ContentTypeReader, typeData.TypeReader);
            this.contentTypeWriters.Add(typeData.ContentTypeReader, typeData.TypeWriter);
            this.contentTypeMap.Add(typeData.ContentType, typeData.ContentTypeReader);
        }

        public void AddTypeData(TypeData[] typeDataArray)
        {
            foreach (var typeDataEntry in typeDataArray)
                AddTypeData(typeDataEntry);
        }

        public void AddTypeData(List<TypeData> typeDataArray)
        {
            foreach (var typeDataEntry in typeDataArray)
                AddTypeData(typeDataEntry);
        }
    }
}
