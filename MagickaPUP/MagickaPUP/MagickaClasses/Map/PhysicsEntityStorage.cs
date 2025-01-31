using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Map
{
    // TODO : Figure out if I need to implement the template class or not... it has a Read() method in Magicka's side of the code, but it does not appear to be used
    // on the map reading side of the code. It looks like the map side of the code simply contains a path to a physics entity template, which is most likely read during
    // the XNB patch up at the end when dealing with shared resources and stuff, so, for now, we're storing a string and that's it.

    /*
    public class PhysicsEntityTemplate : XnaObject
    { }
    */

    public class PhysicsEntityStorage : XnaObject
    {
        #region Variables

        public Matrix transform { get; set; }
        public string template { get; set; } // Instead of storing a template class, we appear to be reading a path for some asset's name, a path that in the game's code gets appended to "Data/PhysicsEntities/" to find the actual template asset.

        // public PhysicsEntityTemplate template { get; set; }

        #endregion

        #region Constructor

        public PhysicsEntityStorage()
        {
            this.transform = new Matrix();
            this.template = default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading PhysicsEntityStorage...");

            this.transform = Matrix.Read(reader, null);
            this.template = reader.ReadString();
        }

        public static PhysicsEntityStorage Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new PhysicsEntityStorage();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing PhysicsEntityStorage...");

            this.transform.WriteInstance(writer, null);
            writer.Write(this.template);
        }

        #endregion
    }
}
