using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.XnaClasses.ContentType.Readers;
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
        public Dictionary<Type, TypeWriterBase> contentTypeWriters = new();

        public ContentTypeReaderManager()
        {
            TypeData[] defaultTypeData =
            {
                new TypeData()
            };

            foreach(var typeData in defaultTypeData)
                AddTypeData(typeData);
        }

        public TypeReaderBase GetTypeReader(ContentTypeReader contentType)
        {
            if (this.contentTypeReaders.ContainsKey(contentType))
                return this.contentTypeReaders[contentType];
            throw new Exception($"The ContentTypeReader \"{contentType.Name}\" is not implemented yet!");
        }

        public TypeWriterBase GetTypeWriter(Type type)
        {
            if(this.contentTypeWriters.ContainsKey(type))
                return this.contentTypeWriters[type];
            throw new Exception($"The ContentTypeReader for type \"{type.Name}\" could not be found!");
        }

        public void AddTypeData(TypeData typeData)
        {
            this.contentTypeReaders.Add(typeData.ContentTypeReader, typeData.TypeReader);
            this.contentTypeWriters.Add(typeData.ContentType, typeData.TypeWriter);
        }

        public void AddTypeData(TypeData[] typeDataArray)
        {
            foreach (var typeDataEntry in typeDataArray)
                AddTypeData(typeDataEntry);
        }
    }
}
