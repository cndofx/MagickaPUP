using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public class Pose : XnaObject
    {
        #region Variables

        public Vec3 translation { get; set; }
        public Quaternion orientation { get; set; }
        public Vec3 scale { get; set; }

        #endregion

        #region Constructor

        public Pose()
        {
            this.translation = new Vec3();
            this.orientation = new Quaternion();
            this.scale = new Vec3();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Pose...");

            this.translation = Vec3.Read(reader, null);
            this.orientation = Quaternion.Read(reader, null);
            this.scale = Vec3.Read(reader, null);
        }

        public static Pose Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Pose ans = new Pose();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Pose...");

            this.translation.WriteInstance(writer, null);
            this.orientation.WriteInstance(writer, null);
            this.scale.WriteInstance(writer, null);
        }

        #endregion
    }

    public class AnimationKeyframe : XnaObject
    {
        #region Variables

        public float time { get; set; }
        public Pose pose { get; set; }

        #endregion

        #region Constructor

        public AnimationKeyframe()
        {
            this.time = 0.0f;
            this.pose = new Pose();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading AnimationKeyframe");

            this.time = reader.ReadSingle();
            this.pose.ReadInstance(reader, logger);
        }

        public static AnimationKeyframe Read(MBinaryReader reader, DebugLogger logger = null)
        {
            AnimationKeyframe ans = new AnimationKeyframe();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing AnimationKeyframe");

            writer.Write(this.time);
            this.pose.WriteInstance(writer, logger);
        }

        #endregion
    }

    public class Animation : XnaObject
    {
        #region Variables

        public int numFrames { get; set; }
        public List<AnimationKeyframe> frames { get; set; }

        #endregion

        #region Constructor

        public Animation()
        {
            this.numFrames = 0;
            this.frames = new List<AnimationKeyframe>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Animation...");

            this.numFrames = reader.ReadInt32();

            logger?.Log(1, $" - Number of Frames : {this.numFrames}");

            for (int i = 0; i < numFrames; ++i)
            {
                AnimationKeyframe frame = AnimationKeyframe.Read(reader, logger);
                this.frames.Add(frame);
            }
        }

        public static Animation Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Animation ans = new Animation();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Animation...");

            writer.Write(this.numFrames);
            for (int i = 0; i < this.numFrames; ++i)
            {
                this.frames[i].WriteInstance(writer, logger);
            }
        }

        #endregion
    }
}
