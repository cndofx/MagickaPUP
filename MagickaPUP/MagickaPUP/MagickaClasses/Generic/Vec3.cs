using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Generic
{
    public class Vec3 : XnaObject
    {
        #region Variables

        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }

        #endregion

        #region Constructor

        public Vec3(float x = 0.0f, float y = 0.0f, float z = 0.0f)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vec3()
        {
            this.x = 0.0f;
            this.y = 0.0f;
            this.z = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Vec3...");

            this.x = reader.ReadSingle();
            this.y = reader.ReadSingle();
            this.z = reader.ReadSingle();

            logger?.Log(2, $" - Vec3 = < x = {this.x}, y = {this.y}, z = {this.z} >");
        }

        public static Vec3 Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Vec3();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public static List<Vec3> ReadList(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading List<Vec3>...");

            int aux;
            int num;
            List<Vec3> ans = new List<Vec3>();

            num = reader.ReadInt32();

            logger?.Log(1, $" - Num Entries : {num}");

            while (num-- > 0)
            {
                // README : This is a BIG mistake!!! (the line where we read the 7 bit encoded integer that is now commented out)
                // I'm keeping it commented so that you remember in the future when you come back to the code.
                // The ReadObject<>() method from the XNA framework has multiple forms, the
                // one that we usually see where it takes a content reader (binary reader from our POV), and the one where
                // it takes a *ContentTypeReader*. In that form where it takes a ContentTypeReader, it does NOT need to
                // read a 7 bit encoded integer to determine the reader to be used, because it is already taking said reader as a parameter.
                // That's the type of ReadObject<>() overload used in the list reader in XNA framework.
                
                //aux = reader.Read7BitEncodedInt();
                
                Vec3 v = Vec3.Read(reader, null);
                ans.Add(v);
                logger?.Log(2, $"  - Vec3 : < x = {v.x}, y = {v.y}, z = {v.z} >");
            }

            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Vec3...");

            writer.Write(this.x);
            writer.Write(this.y);
            writer.Write(this.z);

            logger?.Log(2, $" - < x = {this.x}, y = {this.y}, z = {this.z} >");
        }

        #endregion
    }
}
