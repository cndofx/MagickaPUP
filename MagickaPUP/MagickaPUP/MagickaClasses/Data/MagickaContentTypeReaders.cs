using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Data
{
    public static class MagickaContentTypeReaders
    {
        // PolygonHead Readers
        public static readonly ContentTypeReader BiTreeModelReader = new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0);
        public static readonly ContentTypeReader EffectDeferredReader = new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0);
        public static readonly ContentTypeReader EffectAdditiveReader = new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0);
        public static readonly ContentTypeReader EffectLiquidReader = new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0);
        public static readonly ContentTypeReader EffectLavaReader = new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0);


        // Magicka Readers
        public static readonly ContentTypeReader LevelModelReader = new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0);
        public static readonly ContentTypeReader CharacterTemplateReader = new ContentTypeReader("Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral", 0);
        public static readonly ContentTypeReader PhysicsEntityTemplateReader = new ContentTypeReader("Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka", 0);

        /*
        public enum ContentTypeReaderIndex
        {
            BiTreeModelReader = 0,
            EffectDeferredReader,
            EffectAdditiveReader,
            EffectLiquidReader,
            EffectLavaReader,
            LevelModelReader,
            CharacterTemplateReader,
            PhysicsEntityTemplateReader,
        }

        public static readonly ContentTypeReader[] ContentTypeReaders = {
            // PolygonHead Readers
            new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0),
            new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead", 0),
            new ContentTypeReader("PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral", 0),

            // Magicka Readers
            new ContentTypeReader("Magicka.ContentReaders.LevelModelReader, Magicka", 0),
            new ContentTypeReader("Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral", 0),
            new ContentTypeReader("Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka", 0),
        };
        */
    }
}
