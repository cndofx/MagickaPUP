using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Nav
{
    public enum MovementProperties
    {
        Default = 0,
        Water = 1,
        Jump = 2,
        Fly = 4,
        Dynamic = 128,
        All = 255
    }


    // This triangle class refers to a nav mesh triangle, which means that it contains weights on each edge.
    // Basically, this nav mesh is a way to represent a graph with nodes (vertices) and weights on the connections between nodes (edges).
    // The name of this class is absurdly generic, but it is what the Magicka code uses,
    // so I'll keep it like that for greater consistency and ease for anyone who wants to go back and forth
    // between the game's decompiled code and this code.
    public class Triangle : XnaObject
    {
        #region Variables

        public ushort VertexA { get; set; }
        public ushort VertexB { get; set; }
        public ushort VertexC { get; set; }

        // README : Neighbours in Magicka's navmesh system are terribly named, but have the following meaning:
        // NeighbourN refers to the index of the neighbouring triangle to the current triangle, connected on the edge between the vertices N->N+1
        // In short:
        // - NeighbourA = Triangle idx for the neighbouring triangle to this triangle on the edge between the vertices of this triangle A and B
        // - NeighbourB = Triangle idx for the neighbouring triangle to this triangle on the edge between the vertices of this triangle B and C
        // - NeighbourC = Triangle idx for the neighbouring triangle to this triangle on the edge between the vertices of this triangle C and A
        // That is why I said that they are terribly named... it would make more sense for them to be named NeighbourAB, NeighbourBC, NeighbourCA
        public ushort NeighbourA { get; set; }
        public ushort NeighbourB { get; set; }
        public ushort NeighbourC { get; set; }

        // README : Magicka's naming for the cost variables follows the ordering of the vertices as they are stored within the triangle data.
        // A->B
        // B->C
        // C->A
        // Basically doing a loop all around all the vertices of the triangle.
        public float CostAB { get; set; }
        public float CostBC { get; set; }
        public float CostCA { get; set; }

        public MovementProperties Properties { get; set; }

        #endregion

        #region Constructor

        public Triangle()
        {
            this.VertexA = 0;
            this.VertexB = 0;
            this.VertexC = 0;
            this.NeighbourA = 0;
            this.NeighbourB = 0;
            this.NeighbourC = 0;
            this.CostAB = 0.0f;
            this.CostBC = 0.0f;
            this.CostCA = 0.0f;
            this.Properties = MovementProperties.Default;
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Triangle (NavMesh, NavTriangle)...");

            this.VertexA = reader.ReadUInt16();
            this.VertexB = reader.ReadUInt16();
            this.VertexC = reader.ReadUInt16();

            this.NeighbourA = reader.ReadUInt16();
            this.NeighbourB = reader.ReadUInt16();
            this.NeighbourC = reader.ReadUInt16();

            this.CostAB = reader.ReadSingle();
            this.CostBC = reader.ReadSingle();
            this.CostCA = reader.ReadSingle();

            this.Properties = (MovementProperties)reader.ReadByte();
        }

        public static Triangle Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Triangle();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Triangle (NavMesh, NavTriangle)...");

            writer.Write(this.VertexA);
            writer.Write(this.VertexB);
            writer.Write(this.VertexC);
            writer.Write(this.NeighbourA);
            writer.Write(this.NeighbourB);
            writer.Write(this.NeighbourC);
            writer.Write(this.CostAB);
            writer.Write(this.CostBC);
            writer.Write(this.CostCA);
            writer.Write((byte)this.Properties);
        }

        #endregion
    }
}
