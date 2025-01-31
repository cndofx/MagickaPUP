using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.Utility.IO;

namespace MagickaPUP.MagickaClasses.Collision
{
    // A proxy class that only exists to make it easier to handle the 10 collision layer loops.
    // Each collision layer corresponds to a different collision material type.
    // All collision meshes with a certain collision material are within the collision layer with the same index as the value within the CollisionMaterial enum.
    // This system is kinda weird, but it makes sense, and it is what Magicka does under the hood.
    // Basically, the static side of the level (LevelModel) has to be allowed to have collisions of multiple different types, so it has a layer for each type.
    // Meanwhile, animated level parts only have 1 byte that determines what their collision type is, because rather than having layers of collisions, they only
    // have 1 single byte that determines what the collision type of that entire animated level part is.
    public class LevelModelCollisionData : XnaObject
    {
        #region Variables

        public bool hasCollision { get; set; }
        public List<Vec3> vertices { get; set; }
        public int numTriangles { get; set; }
        public List<CollisionTriangle> triangles { get; set; }

        #endregion

        #region Constructor

        public LevelModelCollisionData()
        {
            this.hasCollision = false;
            this.vertices = new List<Vec3>();
            this.numTriangles = 0;
            this.triangles = new List<CollisionTriangle>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading LevelModelCollisionData...");

            this.hasCollision = reader.ReadBoolean();

            logger?.Log(1, $" - Layer Has Collision : {this.hasCollision}");

            if (!this.hasCollision)
                return;
            
            this.vertices = XnaObject.ReadObject<List<Vec3>>(reader, logger);

            logger?.Log(1, $" - Num Vertices  : {this.vertices.Count}");

            this.numTriangles = reader.ReadInt32();
            logger?.Log(1, $" - Num Triangles : {this.numTriangles}");
            for (int i = 0; i < this.numTriangles; ++i)
            {
                CollisionTriangle tri = CollisionTriangle.Read(reader, logger);
                this.triangles.Add(tri);
            }
        }

        public static LevelModelCollisionData Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new LevelModelCollisionData();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing LevelModelCollisionData...");

            logger?.Log(1, $" - Layer Has Collision : {this.hasCollision}");

            writer.Write(this.hasCollision);

            if (!this.hasCollision)
                return;

            XnaObject.WriteObject(this.vertices, writer, logger);

            writer.Write(this.numTriangles);
            for (int i = 0; i < this.numTriangles; ++i)
                this.triangles[i].WriteInstance(writer, logger);
        }

        #endregion
    }
}
