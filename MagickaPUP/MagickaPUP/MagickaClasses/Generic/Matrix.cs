using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Generic
{
    // README : Matrix is composed by 16 floats (16 * 4 = 64 bytes)
    public class Matrix : XnaObject
    {
        #region Variables

        public float M11 { get; set; }
        public float M12 { get; set; }
        public float M13 { get; set; }
        public float M14 { get; set; }
        public float M21 { get; set; }
        public float M22 { get; set; }
        public float M23 { get; set; }
        public float M24 { get; set; }
        public float M31 { get; set; }
        public float M32 { get; set; }
        public float M33 { get; set; }
        public float M34 { get; set; }
        public float M41 { get; set; }
        public float M42 { get; set; }
        public float M43 { get; set; }
        public float M44 { get; set; }

        #endregion

        #region Constructor

        public Matrix
        (
            float inM11 = 0.0f,
            float inM12 = 0.0f,
            float inM13 = 0.0f,
            float inM14 = 0.0f,
            float inM21 = 0.0f,
            float inM22 = 0.0f,
            float inM23 = 0.0f,
            float inM24 = 0.0f,
            float inM31 = 0.0f,
            float inM32 = 0.0f,
            float inM33 = 0.0f,
            float inM34 = 0.0f,
            float inM41 = 0.0f,
            float inM42 = 0.0f,
            float inM43 = 0.0f,
            float inM44 = 0.0f
        )
        {
            this.M11 = inM11;
            this.M12 = inM12;
            this.M13 = inM13;
            this.M14 = inM14;
            this.M21 = inM21;
            this.M22 = inM22;
            this.M23 = inM23;
            this.M24 = inM24;
            this.M31 = inM31;
            this.M32 = inM32;
            this.M33 = inM33;
            this.M34 = inM34;
            this.M41 = inM41;
            this.M42 = inM42;
            this.M43 = inM43;
            this.M44 = inM44;
        }

        public Matrix()
        {
            this.M11 = 0.0f;
            this.M12 = 0.0f;
            this.M13 = 0.0f;
            this.M14 = 0.0f;
            this.M21 = 0.0f;
            this.M22 = 0.0f;
            this.M23 = 0.0f;
            this.M24 = 0.0f;
            this.M31 = 0.0f;
            this.M32 = 0.0f;
            this.M33 = 0.0f;
            this.M34 = 0.0f;
            this.M41 = 0.0f;
            this.M42 = 0.0f;
            this.M43 = 0.0f;
            this.M44 = 0.0f;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Matrix...");

            this.M11 = reader.ReadSingle();
            this.M12 = reader.ReadSingle();
            this.M13 = reader.ReadSingle();
            this.M14 = reader.ReadSingle();
            this.M21 = reader.ReadSingle();
            this.M22 = reader.ReadSingle();
            this.M23 = reader.ReadSingle();
            this.M24 = reader.ReadSingle();
            this.M31 = reader.ReadSingle();
            this.M32 = reader.ReadSingle();
            this.M33 = reader.ReadSingle();
            this.M34 = reader.ReadSingle();
            this.M41 = reader.ReadSingle();
            this.M42 = reader.ReadSingle();
            this.M43 = reader.ReadSingle();
            this.M44 = reader.ReadSingle();
        }

        public static Matrix Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Matrix();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Matrix...");

            writer.Write(this.M11);
            writer.Write(this.M12);
            writer.Write(this.M13);
            writer.Write(this.M14);
            writer.Write(this.M21);
            writer.Write(this.M22);
            writer.Write(this.M23);
            writer.Write(this.M24);
            writer.Write(this.M31);
            writer.Write(this.M32);
            writer.Write(this.M33);
            writer.Write(this.M34);
            writer.Write(this.M41);
            writer.Write(this.M42);
            writer.Write(this.M43);
            writer.Write(this.M44);
        }

        #endregion
    }
}
