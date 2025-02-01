using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.PhysicsEntities
{
    // NOTE : Similar to the other "duplicated" classes, this has been created to adjust the JSON and binary serialization process to the PhysicsEntityTemplate class
    // but unlike the others, this class actually has its own specific implementation for LevelModel and for PhysicsEntityTemplate within Magicka's code
    // (LevelModel.VisualEffectStorage and PhysicsEntity.VisualEffectStorage respectively, and yes, that is NOT a typo, the storage class is a subclass of
    // PhysicsEntity and NOT of PhysicsEntityTemplate...)
    public class VisualEffectStorage
    {
        public string EffectName { get; set; }
        public Matrix TransformMatrix { get; set; }

        public VisualEffectStorage()
        {
            this.EffectName = string.Empty;
            this.TransformMatrix = new Matrix();
        }

        public VisualEffectStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading VisualEffectStorage...");

            this.EffectName = reader.ReadString();
            this.TransformMatrix = Matrix.Read(reader, logger);
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing VisualEffectStorage...");

            writer.Write(this.EffectName);
            this.TransformMatrix.WriteInstance(writer, logger);
        }
    }
}
