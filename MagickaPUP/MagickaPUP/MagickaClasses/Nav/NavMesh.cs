using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Nav
{
    public class NavMesh : XnaObject
    {
        #region Variables

        public int NumVertices { get; set; }
        public Vec3[] Vertices { get; set; }

        public int NumTriangles { get; set; }
        public Triangle[] Triangles { get; set; }

        #endregion

        #region Constructor

        public NavMesh()
        {
            this.NumVertices = 0;
            this.NumTriangles = 0;
            this.Vertices = new Vec3[0];
            this.Triangles = new Triangle[0];
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading NavMesh...");

            this.NumVertices = (int)(reader.ReadUInt16());
            this.Vertices = new Vec3[this.NumVertices];
            for (int i = 0; i < this.NumVertices; ++i)
            {
                this.Vertices[i] = Vec3.Read(reader, null);
            }

            this.NumTriangles = (int)(reader.ReadUInt16());
            this.Triangles = new Triangle[this.NumTriangles];
            for (int i = 0; i < this.NumTriangles; ++i)
            {
                this.Triangles[i] = Triangle.Read(reader, null);
            }

            logger?.Log(2, $" - Num Vertices  : {this.NumVertices}");
            logger?.Log(2, $" - Num Triangles : {this.NumTriangles}");
        }

        public static NavMesh Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new NavMesh();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing NavMesh...");

            writer.Write((ushort)this.NumVertices);
            for (int i = 0; i < this.NumVertices; ++i)
            {
                this.Vertices[i].WriteInstance(writer, logger);
            }

            writer.Write((ushort)this.NumTriangles);
            for (int i = 0; i < this.NumTriangles; ++i)
            {
                this.Triangles[i].WriteInstance(writer, logger);
            }
            
        }

        #endregion
    }
}
