using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;

namespace MagickaPUP.MagickaClasses.Map
{
    public class BiTreeModel : XnaObject
    {
        #region Variables

        public int NumRoots { get; set; } // number of root nodes in the binary tree
        public List<BiTreeRootNode> RootNodes { get; set; } // list of root nodes in the binary tree.

        #endregion

        #region Constructor

        public BiTreeModel()
        {
            this.NumRoots = 0;
            this.RootNodes = new List<BiTreeRootNode>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading BiTreeModel...");

            this.NumRoots = reader.ReadInt32(); // number of root nodes in the tree.

            logger?.Log(1, $" - Num Roots : {this.NumRoots}");

            for (int i = 0; i < this.NumRoots; ++i)
            {
                BiTreeRootNode root = BiTreeRootNode.Read(reader, logger);
                this.RootNodes.Add(root);
            }
        }

        public static BiTreeModel Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new BiTreeModel();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing BiTreeModel...");

            writer.Write(this.NumRoots);

            for (int i = 0; i < this.NumRoots; ++i)
            {
                this.RootNodes[i].WriteInstance(writer, logger);
            }
        }

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("PolygonHead.Pipeline.BiTreeModelReader, PolygonHead", 0);
        }

        #endregion
    }
}
