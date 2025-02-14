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
    }
}
