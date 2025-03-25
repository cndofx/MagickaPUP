
using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.XnaClasses.Xna;

// TODO : Implement
// TODO : Change all of the object Tags with XnaObject tags so that they can read and written using the system that has been implemented in this codebase...
// README : This whole reading process is kind of trash, mainly because of the whole dependency on ReadBoneReference(), so we cannot encapsulate the reading to each individual class...
// TODO : Modify the system to remove all of the unnecessary XnaObject inheritance that is going on here... plenty of these objects simply cannot be primary objects within an XNB file and it just would not make sense for them to be such. In any case, even if they could be, their reading process for inline objects and primary objects could also be different, so it would make sense to implement both separately through interfaces... in the case of many of these structs, we would probably not even need any of them to be useable as primary objects...
namespace MagickaPUP.XnaClasses
{
    // TODO : Fix problem : the fucking buffers and declarations are duplicated, shouldn't they be indices or some shit pointing to the parent's data??? or just pointers, like, its ok as classes, they are pointers after all, the problem is when this data is serialized as JSON, it lierally just dupes all of the contents that is already found in the parents...
    public class ModelMeshPart : XnaObject
    {
        #region Variables

        public int streamOffset { get; set; }
        public int baseVertex { get; set; }
        public int numVertices { get; set; }
        public int startIndex { get; set; }
        public int primitiveCount { get; set; }

        /*
        public VertexBuffer vertexBuffer { get; set; }
        public IndexBuffer indexBuffer { get; set; }
        public VertexDeclaration vertexDeclaration { get; set; }
        */

        public int vertexDeclarationIndex { get; set; }

        public object tag { get; set; }

        public int sharedResourceIndex { get; set; }

        #endregion

        #region Constructor

        public ModelMeshPart()
        {
            this.streamOffset = 0;
            this.baseVertex = 0;
            this.numVertices = 0;
            this.startIndex = 0;
            this.primitiveCount = 0;
            this.vertexDeclarationIndex = 0;
            this.tag = null;
            this.sharedResourceIndex = 0;
        }

        /*
        public ModelMeshPart(int streamOffset, int baseVertex, int numVertices, int startIndex, int primitiveCount, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, VertexDeclaration vertexDeclaration, object obj, int sharedResourceIndex)
        {
            this.streamOffset = streamOffset;
            this.baseVertex = baseVertex;
            this.numVertices = numVertices;
            this.startIndex = startIndex;
            this.primitiveCount = primitiveCount;
            this.vertexBuffer = vertexBuffer;
            this.indexBuffer = indexBuffer;
            this.vertexDeclaration = vertexDeclaration;
            this.tag = obj;
            this.sharedResourceIndex = sharedResourceIndex;
        }
        */

        public ModelMeshPart(int streamOffset, int baseVertex, int numVertices, int startIndex, int primitiveCount, int vertexDeclarationIdx, object obj, int sharedResourceIndex)
        {
            this.streamOffset = streamOffset;
            this.baseVertex = baseVertex;
            this.numVertices = numVertices;
            this.startIndex = startIndex;
            this.primitiveCount = primitiveCount;
            this.vertexDeclarationIndex = vertexDeclarationIdx;
            this.tag = obj;
            this.sharedResourceIndex = sharedResourceIndex;
        }

        #endregion

        #region PublicMethods
        #endregion
    }

    public class ModelMesh : XnaObject
    {
        #region Variables

        public string name { get; set; }
        public int parentBone { get; set; }
        public BoundingSphere boundingSphere { get; set; }
        public VertexBuffer vertexBuffer { get; set; }
        public IndexBuffer indexBuffer { get; set; }
        public ModelMeshPart[] meshParts { get; set; }

        #endregion

        #region Constructor

        public ModelMesh()
        {
            this.name = "__none__";
            this.parentBone = -1;
            this.boundingSphere = new BoundingSphere();
            this.vertexBuffer = new VertexBuffer();
            this.indexBuffer = new IndexBuffer();
            this.meshParts = new ModelMeshPart[0];
        }

        public ModelMesh(string name, int parentBone, BoundingSphere boundingSphere, VertexBuffer vertexBuffer, IndexBuffer indexBuffer, ModelMeshPart[] meshParts)
        {
            this.name = name;
            this.parentBone = parentBone;
            this.boundingSphere = boundingSphere;
            this.vertexBuffer = vertexBuffer;
            this.indexBuffer = indexBuffer;
            this.meshParts = meshParts;
        }

        #endregion

        #region PublicMethods
        #endregion
    }

    public class ModelBone : XnaObject
    {
        #region Variables

        public int index { get; set; }
        public string name { get; set; }
        
        public Matrix transform { get; set; }
        
        public int parent { get; set; }
        public int[] children { get; set; }

        #endregion

        #region Constructor

        public ModelBone()
        {
            this.index = 0;
            this.name = "__none__";
            this.transform = new Matrix();
            this.parent = 0;
            this.children = new int[0];
        }

        #endregion

        #region PublicMethods

        public void SetParentAndChildren(ModelBone newParent, ModelBone[] newChildren)
        {
            this.parent = newParent == null ? -1 : newParent.index;
            this.children = new int[newChildren.Length];
            for (int i = 0; i < this.children.Length; ++i)
                this.children[i] = newChildren[i].index;
        }

        #endregion
    }

    // TODO : Implement Writing
    public class Model : XnaObject
    {
        #region Variables

        public object tag { get; set; }
        int rootBone { get; set; }

        public int numBones { get; set; }
        public List<ModelBone> bones { get; set; }

        public int numVertexDeclarations { get; set; }
        public List<VertexDeclaration> vertexDeclarations { get; set; }


        public int numModelMeshes { get; set; }
        public List<ModelMesh> modelMeshes { get; set; }

        #endregion

        #region Constructor

        public Model()
        {
            this.tag = null;
            this.rootBone = -1;

            this.numBones = 0;
            this.bones = new List<ModelBone>();

            this.numVertexDeclarations = 0;
            this.vertexDeclarations = new List<VertexDeclaration>();

            this.numModelMeshes = 0;
            this.modelMeshes = new List<ModelMesh>();
        }

        #endregion

        #region PublicMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger.Log(1, "Reading Model...");

            this.ReadBones(reader, logger);
            this.ReadBoneReferences(reader, logger);
            this.ReadVertexDeclarations(reader, logger);
            this.ReadMeshes(reader, logger);
            this.ReadRootBone(reader, logger);
            this.ReadTag(reader, logger);
        }

        public static Model Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new Model();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Model...");

            // throw new NotImplementedException("Write Model is not implemented yet!");

            this.WriteBones(writer, logger);
            this.WriteBoneReferences(writer, logger);
            this.WriteVertexDeclarations(writer, logger);
            this.WriteMeshes(writer, logger);
            this.WriteRootBone(writer, logger);
            this.WriteTag(writer, logger);
        }

        public override ContentTypeReader GetListContentTypeReader()
        {
            return new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0);
        }

        #endregion

        #region PrivateMethodsReadBones

        private void ReadBones(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Bones...");

            this.numBones = reader.ReadInt32();
            for (int i = 0; i < this.numBones; ++i)
            {
                ModelBone bone = this.ReadBone(i, reader, logger);
                this.bones.Add(bone);
            }
        }

        private void ReadBoneReferences(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Bone References...");

            foreach (var bone in this.bones)
            {
                ModelBone parentBone = this.ReadBoneReference(reader, logger);
                int numChildrenBones = reader.ReadInt32();
                ModelBone[] childBones = new ModelBone[numChildrenBones];
                for (int i = 0; i < numChildrenBones; ++i)
                {
                    childBones[i] = this.ReadBoneReference(reader, logger);
                }
                bone.SetParentAndChildren(parentBone, childBones);
            }
        }

        private ModelBone ReadBone(int idx, MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Bone...");

            ModelBone bone = new ModelBone();

            bone.name = XnaUtility.ReadObject<string>(reader, logger);
            bone.transform = Matrix.Read(reader, null);
            bone.index = idx;

            return bone;
        }

        private ModelBone ReadBoneReference(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Bone Reference...");

            int boneCount = this.bones.Count + 1;
            int index;

            if (boneCount <= 255)
            {
                index = (int)reader.ReadByte();
            }
            else
            {
                index = reader.ReadInt32();
            }

            logger?.Log(2, $" - Bone Reference : {{ index = {index - 1}, numBones = {this.bones.Count}}}");

            if (index != 0) // Within the XNB file, the index starts at 1.
                return this.bones[index - 1]; // Within C# code, it starts at 0.

            return null;
        }

        #endregion

        #region PrivateMethodsReadOther

        private void ReadVertexDeclarations(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Vertex Declarations...");

            this.numVertexDeclarations = reader.ReadInt32();
            for (int i = 0; i < this.numVertexDeclarations; ++i)
            {
                VertexDeclaration vertexDeclaration = XnaUtility.ReadObject<VertexDeclaration>(reader, logger);
                this.vertexDeclarations.Add(vertexDeclaration);
            }
        }

        private void ReadMeshes(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Meshes...");

            this.numModelMeshes = reader.ReadInt32();
            for (int i = 0; i < this.numModelMeshes; ++i)
            {
                var current = ReadMesh(reader, logger);
                this.modelMeshes.Add(current);
            }
        }

        private ModelMesh ReadMesh(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Mesh...");

            string name = XnaUtility.ReadObject<string>(reader, logger);
            ModelBone parentBone = this.ReadBoneReference(reader, logger);
            int parentBoneIndex = parentBone == null ? -1 : parentBone.index;
            BoundingSphere boundingSphere = BoundingSphere.Read(reader, logger);
            VertexBuffer vertexBuffer = XnaUtility.ReadObject<VertexBuffer>(reader, logger);
            IndexBuffer indexBuffer = XnaUtility.ReadObject<IndexBuffer>(reader, logger);
            object obj = XnaUtility.ReadObject<object>(reader, logger);
            ModelMeshPart[] meshParts = this.ReadMeshParts(reader, logger);

            ModelMesh current = new ModelMesh(name, parentBoneIndex, boundingSphere, vertexBuffer, indexBuffer, meshParts);

            return current;
        }

        private ModelMeshPart[] ReadMeshParts(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Mesh Parts...");

            int numMeshParts = reader.ReadInt32();

            ModelMeshPart[] ans = new ModelMeshPart[numMeshParts];

            for (int i = 0; i < numMeshParts; ++i)
            {
                ModelMeshPart part = ReadMeshPart(reader, logger);
                ans[i] = part;
            }

            return ans;
        }

        private ModelMeshPart ReadMeshPart(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Mesh Part...");

            int streamOffset = reader.ReadInt32();
            int baseVertex = reader.ReadInt32();
            int numVertices = reader.ReadInt32();
            int startIndex = reader.ReadInt32();
            int primitiveCount = reader.ReadInt32();

            int vertexDeclarationIndex = reader.ReadInt32();

            object obj = XnaUtility.ReadObject<object>(reader, logger);

            // The shared resource index is a 7bit encoded integer that contains the index of the shared resource which corresponds to
            // the Material (known as "Effect" in the XNA framework) that is used by this mesh part.
            int sharedResourceIndex = reader.Read7BitEncodedInt();

            ModelMeshPart ans = new ModelMeshPart(streamOffset, baseVertex, numVertices, startIndex, primitiveCount, vertexDeclarationIndex, obj, sharedResourceIndex);

            return ans;
        }

        private void ReadRootBone(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Root Bone...");
            ModelBone boneRef = this.ReadBoneReference(reader, logger);
            this.rootBone = boneRef == null ? -1 : boneRef.index;
        }

        private void ReadTag(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Tag...");
            this.tag = XnaUtility.ReadObject<object>(reader, logger);
        }

        #endregion

        #region PrivateMethodsWriteBones

        private void WriteBones(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Bones...");

            writer.Write(this.numBones);
            foreach (var bone in this.bones)
            {
                this.WriteBone(writer, bone, logger);
            }
        }

        private void WriteBoneReferences(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Bone References...");
            foreach (var bone in this.bones)
            {
                this.WriteBoneReference(writer, bone.parent, logger);
                writer.Write((int)bone.children.Length);
                foreach (var child in bone.children)
                {
                    this.WriteBoneReference(writer, child, logger);
                }
            }
        }

        private void WriteBone(MBinaryWriter writer, ModelBone bone, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Bone...");

            XnaUtility.WriteObject(bone.name, writer, logger);
            bone.transform.WriteInstance(writer, logger);
        }

        private void WriteBoneReference(MBinaryWriter writer, int idx, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Bone Reference...");

            int boneCount = this.bones.Count + 1;
            int valueToWrite = idx + 1;

            if (boneCount <= 255)
            {
                writer.Write((byte)valueToWrite);
            }
            else
            {
                writer.Write((int)valueToWrite);
            }

            // NOTE : Remember that within the XNB file, if the value is 0, then it is the equivalent to -1 in this code, aka, an invalid bone ref / null ptr / no bone.
        }

        private void WriteBoneReference(MBinaryWriter writer, ModelBone bone, DebugLogger logger = null)
        {
            WriteBoneReference(writer, (bone == null? -1 : bone.index), logger);
        }

        #endregion

        #region PrivateMethodsWriteOther

        private void WriteVertexDeclarations(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Vertex Declarations...");

            writer.Write(this.numVertexDeclarations);
            foreach (var decl in this.vertexDeclarations)
            {
                XnaUtility.WriteObject(decl, writer, logger);
            }
        }

        private void WriteMeshes(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Meshes...");

            writer.Write(this.numModelMeshes);
            foreach (var model in this.modelMeshes)
            {
                this.WriteMesh(writer, model, logger);
            }
        }

        private void WriteMesh(MBinaryWriter writer, ModelMesh mesh, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Mesh...");

            XnaUtility.WriteObject(mesh.name, writer, logger);
            this.WriteBoneReference(writer, mesh.parentBone, logger);
            mesh.boundingSphere.WriteInstance(writer, logger);
            XnaUtility.WriteObject(mesh.vertexBuffer, writer, logger);
            XnaUtility.WriteObject(mesh.indexBuffer, writer, logger);
            writer.Write7BitEncodedInt((int)0); // TODO : This is the code for writing the tag object for the mesh. As of now, it always writes null. Replace this when you change object tags with XnaObject tags.
            this.WriteMeshParts(writer, mesh.meshParts, logger);
        }

        private void WriteMeshParts(MBinaryWriter writer, ModelMeshPart[] parts, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Mesh Parts...");

            writer.Write((int)parts.Length);
            foreach (var part in parts)
            {
                WriteMeshPart(writer, part, logger);
            }
        }

        // TODO : Fix impl.
        private void WriteMeshPart(MBinaryWriter writer, ModelMeshPart part, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Mesh Part...");

            writer.Write(part.streamOffset);
            writer.Write(part.baseVertex);
            writer.Write(part.numVertices);
            writer.Write(part.startIndex);
            writer.Write(part.primitiveCount);
            
            writer.Write(part.vertexDeclarationIndex);

            // TODO : Change this into an actual proper impl. Again, for now only write a 7 bit int with value 0 to indicate that the tag is null.
            writer.Write7BitEncodedInt((int)0);

            // Write the index to the shared resource that contains the material (effect) used by this mesh part.
            writer.Write7BitEncodedInt((int)part.sharedResourceIndex);
        }

        private void WriteRootBone(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Root Bone...");
            this.WriteBoneReference(writer, this.rootBone, logger);
        }

        // TODO : Fix implementation. For now, it always writes a null object
        private void WriteTag(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Tag...");
            writer.Write7BitEncodedInt((int)0);
            // XnaObject.WriteObject(this.tag, writer, logger); // TODO : Recover this after you have changed the whole object stuff and checked that it reads just fine.
        }

        #endregion

        public override ContentTypeReader GetObjectContentTypeReader()
        {
            return new ContentTypeReader("Microsoft.Xna.Framework.Content.ModelReader", 0);
        }
    }
}
