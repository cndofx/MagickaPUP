using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    #region Comment - EffectMaterial

    // NOTE : This class corresponds to the XNA EffectMaterial class.
    // Basically, this is just a dict for a bunch of properties with type tags and default values.
    // Allows constructing a custom set of values for a material instance for a given shader type through a dictionary.
    // When XNA loads this type of asset, it generates an Effect of the shader XNB type specified as External Reference here,
    // and then assigns each of the properties within the dict to the created effect if they exist within the effect's code
    // and if they are of a type supported by XNA / D3D's shaders.

    // TODO : Check that this actually works and does what I think it does...

    #endregion
    public class EffectMaterial : XnaObject
    {
        string EffectName { get; set; } // ER. The name of the shader XNB file that we want to instantiate.
        Dictionary<string, object> Parameters { get; set; } // Dict<str, obj>. The list of the parameters of the shader.

        public EffectMaterial()
        {
            this.EffectName = string.Empty;
            this.Parameters = new Dictionary<string, object>();
        }

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading EffectMaterial...");
            throw new NotImplementedException("Read EffectMaterial is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing EffectMaterial...");
            throw new NotImplementedException("Write EffectMaterial is not implemented yet!");
        }
    }
}
