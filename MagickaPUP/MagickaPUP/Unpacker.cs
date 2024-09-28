using MagickaPUP.IO;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace MagickaPUP
{
    class Unpacker
    {
        #region Variables

        private string readfilename;
        private string writefilename;

        private FileStream readFile;
        private FileStream writeFile;

        private MBinaryReader reader;
        private StreamWriter writer;

        private DebugLogger logger;

        #endregion

        #region wtf

        private readonly string[] errorMessages = {
            /* 0 */ "Operation successfully completed",
            /* 1 */ "Wrong file type, must be XNB",
            /* 2 */ "Wrong platform, must be Windows",
            /* 3 */ "XNB file is compressed, must be decompressed"
        };

        #endregion

        #region Properties

        public string FileNameIn { get { return readfilename; } set { readfilename = value; } }
        public string FileNameOut { get { return writefilename; } set { writefilename = value; } }

        public int DebugLevel { get { return logger.DebugLevel; } set { logger.DebugLevel = value; } }

        #endregion

        #region Constructors

        public Unpacker(string infilename, string outfilename, int debugLevel = 1)
        {
            this.readfilename = infilename;
            this.writefilename = outfilename;
            this.logger = new DebugLogger("Unpacker", debugLevel);
        }

        #endregion

        #region PublicMethods

        public int Unpack()
        {
            using (this.readFile = new FileStream(this.readfilename, FileMode.Open, FileAccess.Read))
            using (this.reader = new MBinaryReader(this.readFile))
            using (this.writeFile = new FileStream(this.writefilename, FileMode.Create, FileAccess.Write))
            using (this.writer = new StreamWriter(this.writeFile))
            {
                logger.Log(1, "Validaing XNB file...");
                // Validate as XNB file
                char x = reader.ReadChar();
                char n = reader.ReadChar();
                char b = reader.ReadChar();

                string headerString = $"{x}{n}{b}";

                if (!(x == 'X' && n == 'N' && b == 'B'))
                {
                    logger.Log(1, $"Header \"{headerString}\" is not valid.");
                    return 1;
                }

                logger.Log(1, $"Header \"{x}{n}{b}\" is valid.");

                // Check that it is for windows (no other platforms are supported so it really can't be anything else)
                char w = reader.ReadChar();
                if (w != 'w')
                {
                    logger.Log(1, $"Platform \"{w}\" is not valid.");
                    return 2;
                }
                logger.Log(1, $"Platform \"{w}\" is valid (Windows)");

                // Get Version number, even tho it does not matter that much in this case.
                int xnbVersion = reader.ReadByte();
                logger.Log(1, $"XNA Version : {{ byte = {xnbVersion}, version = {XnaVersion.XnaVersionString(((XnaVersion.XnaVersionByte)xnbVersion))} }}");

                // Get compression. Should always be uncompressed to be able to read it. Expect this vlaue to be 0x00. If it's 0x80 or anything else, bail out.
                int compressed = reader.ReadByte();
                bool isCompressed = (compressed & 0x80) == 0x80;

                logger.Log(1, $"Compressed : {isCompressed} (byte = {compressed})");

                if (isCompressed)
                {
                    logger.Log(1, "Cannot read compressed files.");
                    return 3;
                }

                // Get sizes. They can be whatever, it doesn't really matter since Magicka doesn't use these values actually...
                // btw, these values should be ushort but I'm keeping them as uint as originally, the XNB file reference I was following was for XNA 4.0 and those use u32 for the size variables. In short, I'm just keeping it like this to remember and for furutre feature support etc etc... could really just be changed to ushort without any problems tbh. Actually, the writer does use ushorts so yeah lol...
                uint packedSize = reader.ReadUInt16();
                uint unpackedSize = reader.ReadUInt16();
                logger.Log(1,
                    $"File Size :\n" +
                    $" - Packed   : {packedSize}\n" +
                    $" - Unpacked : {unpackedSize}"
                );

                // Get the amount of TypeReaders and iterate through all of them.
                int typeReaderCount = reader.Read7BitEncodedInt();
                logger.Log(1, $"Content Type Reader Count : {typeReaderCount}");
                for (int i = 0; i < typeReaderCount; ++i)
                {
                    ContentTypeReader currentReader = ContentTypeReader.Read(reader, logger);
                    reader.ContentTypeReaders.Add(currentReader);
                }

                // Get shared resource amount.
                int sharedResourceCount = reader.Read7BitEncodedInt();
                logger.Log(1, $"Shared Resource Count : {sharedResourceCount}");

                // Create variable for XNB file container.
                XnbFileObject xnbFileObject = new XnbFileObject();

                // Read primary objects.
                logger.Log(1, "Reading Primary Object...");
                Wait();
                XnaObject primaryObject = XnaObject.ReadObject<XnaObject>(reader, logger);
                xnbFileObject.SetPrimaryObject(primaryObject);
                logger?.Log(1, "Finished Reading Primary Object!");
                Wait();

                // Read shared resources and store them.
                logger.Log(1, "Reading Shared Resources...");
                for (int i = 0; i < sharedResourceCount; ++i)
                {
                    logger.Log(1, $"Reading Shared Resource {(i + 1)} / {sharedResourceCount}...");
                    Wait();
                    var sharedResource = XnaObject.ReadObject<XnaObject>(reader, logger);
                    xnbFileObject.AddSharedResource(sharedResource);
                    Wait();
                }
                logger.Log(1, "Finished reading XNB file!");

                // Write the JSON file.
                logger.Log(1, "Writing JSON file...");
                writer.Write(JsonSerializer.Serialize(xnbFileObject, new JsonSerializerOptions() {WriteIndented = true}));
                writer.Flush();
                logger.Log(1, "Finished writing JSON file!");
            }
            return 0;
        }

        public string GetErrorString(int errorCode)
        {
            if (errorCode < errorMessages.Length)
                return errorMessages[errorCode];
            return "Unknown Error";
        }

        #endregion

        #region PrivateMethods

        private void ReadLevelModelReader()
        {
            //int num;
            //bool b;
            //
            //Print("Reading Level Model...");
            //// Read the Binary Tree for the primary asset, which is the map mesh.
            //// Is this a binary tree to replicate a very shitty version of Doom's binary space partition? could it be?
            //ReadBiTreeModel();
            //
            //// Load animated parts in the level
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; i++)
            //{
            //    ReadAnimatedLevelPart();
            //}
            //
            //// Load lights
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    string lightName = reader.ReadString();
            //    Vec3 pos = ReadVec3();
            //    Vec3 vector = ReadVec3(); // this is the direction for directional lights and spotlights
            //    int lightType = reader.ReadInt32(); // enum
            //    int lightVariation = reader.ReadInt32(); // enum
            //    float reach = reader.ReadSingle(); // this is the radius for point lights and the distance for spotlights.
            //    bool useAttenuation = reader.ReadBoolean();
            //    float cutoffAngle = reader.ReadSingle();
            //    float sharpness = reader.ReadSingle();
            //
            //    Vec3 diffuseColor = ReadVec3();
            //    Vec3 ambientColor = ReadVec3();
            //    float specularAmount = reader.ReadSingle();
            //    float variationSpeed = reader.ReadSingle();
            //    float variationAmount = reader.ReadSingle();
            //    int shadowMapSize = reader.ReadInt32();
            //    bool castsShadows = reader.ReadBoolean();
            //}
            //
            //// Visual effects (not sure what it could refer to exactly with this, probably particles)
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    // No fucking clue what any of this is... yet...
            //    string iString = reader.ReadString();
            //    Vec3 vector2 = ReadVec3();
            //    Vec3 vector3 = ReadVec3();
            //    float effectStorageRange = reader.ReadSingle();
            //    string iString2 = reader.ReadString();
            //}
            //
            //// Number of physics entity storage
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    /*Matrix startTransform = */
            //    ReadMatrix();
            //    reader.ReadString(); // a path that in game gets appended to "Data/PhysicsEntities/"
            //}
            //
            //// Liquids
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    ReadLiquid();
            //}
            //
            //// Force Fields
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    ReadForceField();
            //}
            //
            //// Collision Mesh
            //for (int i = 0; i < 10; ++i)
            //{
            //    b = reader.ReadBoolean();
            //    if (b)
            //    {
            //        ReadListVec3();
            //        num = reader.ReadInt32();
            //        for (int k = 0; k < num; ++k)
            //        {
            //            int triangleVertexIndex0 = reader.ReadInt32();
            //            int triangleVertexIndex1 = reader.ReadInt32();
            //            int triangleVertexIndex2 = reader.ReadInt32();
            //        }
            //    }
            //}
            //
            //// Form collision mesh triangles
            //b = reader.ReadBoolean();
            //if (b)
            //{
            //    ReadListVec3();
            //    num = reader.ReadInt32();
            //    for (int k = 0; k < num; ++k)
            //    {
            //        int triangleVertexIndex0 = reader.ReadInt32();
            //        int triangleVertexIndex1 = reader.ReadInt32();
            //        int triangleVertexIndex2 = reader.ReadInt32();
            //    }
            //}
            //
            //// Trigger areas
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    string triggerName = reader.ReadString();
            //    ReadTriggerArea();
            //}
            //
            //// Locators
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    string locatorName = reader.ReadString();
            //    ReadLocator();
            //}
            //
            //// Create Nav Mesh
            //ReadNavMesh();
            //
        } /**/

        private void ReadBiTreeModel()
        {
            //int aux = reader.Read7BitEncodedInt();
            //
            //Print("Reading Binary Tree Model...");
            //
            //int num = reader.ReadInt32(); // number of root nodes in the tree.
            //Print($"Root node count : {num}");
            //for (int i = 0; i < num; ++i)
            //    ReadBiTreeRootNode(i);
            //
        } /**/

        private void ReadBiTreeRootNode(int idx)
        {
            //Print($"Reading Binary Tree Root Node [{idx}]");
            ////string ans;
            //
            //bool isVisible = reader.ReadBoolean();
            //bool castShadows = reader.ReadBoolean();
            //float sway = reader.ReadSingle();
            //float entityInfluence = reader.ReadSingle();
            //float groundLevel = reader.ReadSingle();
            //int numVertices = reader.ReadInt32();
            //int vertexStride = reader.ReadInt32();
            //
            ///*
            //ans = "- BiTreeRootNode:\n";
            //ans += $"  - isVisible : {isVisible}\n";
            //ans += $"  - castShadows : {castShadows}\n";
            //ans += $"  - sway : {sway}\n";
            //ans += $"  - entityInfluence : {entityInfluence}\n";
            //ans += $"  - groundLevel : {groundLevel}\n";
            //ans += $"  - numVertices : {numVertices}\n";
            //ans += $"  - vertexStride : {vertexStride}\n";
            //
            //Print($"{ans}");
            //*/
            //
            //string s = $"num vertices : {numVertices}\n";
            //writer.WriteRaw(s);
            //
            //ReadVertexDeclaration();
            //ReadVertexBuffer();
            //ReadIndexBuffer();
            //ReadEffect();
            //
            //int primitiveCount = reader.ReadInt32();
            //int startIndex = reader.ReadInt32();
            //
            //Vec3 boundingBoxMin = ReadVec3();
            //Vec3 boundingBoxMax = ReadVec3();
            //
            //s = $"num primitives : {primitiveCount}\nstartIndex : {startIndex}\n";
            //writer.WriteRaw(s);
            //writer.Flush();
            //
            //bool hasChildA = reader.ReadBoolean();
            //if (hasChildA)
            //    ReadBiTreeNode();
            //
            //bool hasChildB = reader.ReadBoolean();
            //if (hasChildB)
            //    ReadBiTreeNode();
        } /**/

        private void ReadVertexDeclaration()
        {
            //Print("Reading Vertex Declaration...");
            //int aux = reader.Read7BitEncodedInt(); // to identiy this thing's type, but can't do shit since it is hardcoded anyway so lol? Much of the polymorphic code from Magicka makes little sense...
            //
            //int num = reader.ReadInt32();
            //Print($"   num = {num}");
            //for (int i = 0; i < num; ++i)
            //{
            //    short stream = reader.ReadInt16();
            //    short offset = reader.ReadInt16();
            //    byte elementFormat = reader.ReadByte();
            //    byte elementMethod = reader.ReadByte();
            //    byte elementUsage = reader.ReadByte();
            //    byte usageIndex = reader.ReadByte();
            //
            //
            //    string s = $"VertexDeclaration:\nstream = {stream}, offset = {offset}, elementFormat = {elementFormat}, elementMethod = {elementMethod}, elementUsage = {elementUsage}, usageIndex = {usageIndex}\n";
            //    writer.WriteRaw(s);
            //    writer.Flush();
            //
            //    /*
            //    string ans = "- Vertex:\n";
            //    ans += $"  - stream : {stream}\n";
            //    ans += $"  - offset : {offset}\n";
            //    ans += $"  - elementFormat : {elementFormat}\n";
            //    ans += $"  - elementMethod : {elementMethod}\n";
            //    ans += $"  - elementUsage : {elementUsage}\n";
            //    ans += $"  - usageIndex : {usageIndex}\n";
            //    */
            //    //Print($"{ans}");
            //}
        } /**/

        private void ReadVertexBuffer()
        {
            //Print("Reading Vertex Buffer...");
            //int aux = reader.Read7BitEncodedInt();
            //
            //int num = reader.ReadInt32();
            //Print($"   num = {num}");
            //byte[] data = reader.ReadBytes(num);
            //
            //
            //writer.WriteRaw($"VertexBuffer with length {num}:\n{{\n");
            //for (int i = 0; i < data.Length; ++i)
            //    writer.WriteRaw(data[i].ToString() + ", ");
            //writer.WriteRaw("END\n}\n");
            //writer.Flush();
            //
            ///*
            //for (int i = 0; i < num; ++i)
            //{
            //    Console.Write($"{data[i]} ");
            //}
            //Console.WriteLine();
            //*/
        } /**/

        private void ReadIndexBuffer()
        {
            //Print("Reading Index Buffer...");
            //int aux = reader.Read7BitEncodedInt();
            //
            //bool flag = reader.ReadBoolean(); // if true -> IndexElementSize is 16 bits. If false -> IndexElementSize is 32 bits.
            //string bits16str = "IndexElementSize is 16 bits";
            //string bits32str = "IndexElementSize is 32 bits";
            //string chosenstr = flag ? bits16str : bits32str;
            //Print($"   flag = {flag} : {chosenstr}");
            //int num = reader.ReadInt32();
            //Print($"   num = {num}");
            //byte[] data = reader.ReadBytes(num);
            //
            ///*
            //string s = $"IndexBuffer with length {num} and chosen {chosenstr}:\n{{\n";
            //for (int i = 0; i < num; ++i)
            //    s += data[i].ToString() + ", ";
            //s += "END\n}\n";
            //writer.WriteRaw(s);
            //writer.Flush();
            //*/
        } /**/

        private void ReadEffect()
        {
            //Print("Reading Effect...");
            //// Determine the type of effect to be used and read it. In 90% of situations, a deferred effect.
            //int aux = reader.Read7BitEncodedInt() - 1;
            //ReadObject(aux);
            //Print("Finished calling effect...");
            //
            ///* old discarded code!!!!
            //int aux = reader.Read7BitEncodedInt();
            //
            //int count = reader.ReadInt32();
            //Print($"   num = {count}");
            //byte[] effectCode = reader.ReadBytes(count);
            //*/
        } /**/

        private Vec3 ReadVec3()
        {
            //Print("Reading Vec3...");
            //float x = reader.ReadSingle();
            //float y = reader.ReadSingle();
            //float z = reader.ReadSingle();
            //Vec3 vec = new Vec3(x, y, z);
            //return vec;
            return new Vec3();
        } /**/

        public void ReadMatrix()
        {
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
            reader.ReadSingle();
        } /**/

        private void ReadBiTreeNode()
        {
            //Print("Reading BiTreeNode...");
            //int primitiveCount = reader.ReadInt32();
            //int startIndex = reader.ReadInt32();
            //Vec3 boundingBoxMin = ReadVec3();
            //Vec3 boundingBoxMax = ReadVec3();
            //
            //bool hasChildA = reader.ReadBoolean();
            //if (hasChildA)
            //    ReadBiTreeNode();
            //
            //bool hasChildB = reader.ReadBoolean();
            //if (hasChildB)
            //    ReadBiTreeNode();
        } /**/

        private void ReadEffectDeferred()
        {
            //float alpha = reader.ReadSingle();
            //float sharpness = reader.ReadSingle();
            //bool vertexColorEnabled = reader.ReadBoolean();
            //bool useMaterialTextureForReflectiveness = reader.ReadBoolean();
            //string reflectionMap = reader.ReadString(); /* READ EXTERNAL REFERENCE TextureCube */ Print(reflectionMap);
            //bool diffuseTexture0AlphaDisabled = reader.ReadBoolean();
            //bool alphaMask0Enabled = reader.ReadBoolean();
            //Vec3 diffuseColor0 = ReadVec3();
            //float specAmount0 = reader.ReadSingle();
            //float specPower0 = reader.ReadSingle();
            //float emissiveAmount0 = reader.ReadSingle();
            //float normalPower0 = reader.ReadSingle();
            //float reflectiveness0 = reader.ReadSingle();
            //string diffuseTexture0 = reader.ReadString(); /* ER */ Print($"------- er : {diffuseTexture0}");
            //string materialTexture0 = reader.ReadString(); /* ER */ Print($"------- er : {materialTexture0}");
            //string normalTexture0 = reader.ReadString(); /* ER */ Print($"------- er : {normalTexture0}");
            //bool flag = reader.ReadBoolean();
            //if (flag)
            //{
            //    bool DiffuseTexture1AlphaDisabled = reader.ReadBoolean();
            //    bool AlphaMask1Enabled = reader.ReadBoolean();
            //    Vec3 DiffuseColor1 = ReadVec3();
            //    float SpecAmount1 = reader.ReadSingle();
            //    float SpecPower1 = reader.ReadSingle();
            //    float EmissiveAmount1 = reader.ReadSingle();
            //    float NormalPower1 = reader.ReadSingle();
            //    float Reflectiveness1 = reader.ReadSingle();
            //    string DiffuseTexture1 = reader.ReadString(); /* ER */ Print($"------- er : {DiffuseTexture1}");
            //    string MaterialTexture1 = reader.ReadString(); /* ER */ Print($"------- er : {MaterialTexture1}");
            //    string NormalTexture1 = reader.ReadString(); /* ER */ Print($"------- er : {NormalTexture1}");
            //}
        } /**/

        private void ReadListVec3()
        {
            int num = reader.ReadInt32();
            while (num-- > 0)
            {
                int aux = reader.Read7BitEncodedInt();
                ReadVec3();
            }
        } /**/

        // TODO : Implement
        // Even without the events of 40 years ago, I still believe that man would be a creature that fears the dark...
        private void ReadAnimatedLevelPart()
        {
            //Lol("ReadAnimatedLevelPart()");
            //
            //int num;
            //bool b;
            //
            //string animatedPartName = reader.ReadString();
            //bool affectShields = reader.ReadBoolean();
            //ReadModel(); // This Model is an XNA model.
            //
            //// Mesh settings (we'll have to figure out what each setting is by reading the possible strings and what each does...)
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    // this data goes into a Dictionary<string, KeyValuePair<bool,bool>>
            //    string key = reader.ReadString();
            //    bool key2 = reader.ReadBoolean();
            //    bool value = reader.ReadBoolean();
            //}
            //
            //// Liquids
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    // Liquid.Read();
            //}
            //
            //// Locators associated with the animated level part.
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    string name = reader.ReadString();
            //    // Locator value2 = new Locator(name, reader);
            //}
            //
            //float animationDuration = reader.ReadSingle();
            ////AnimationChannel.Read(reader);
            //
            //// Effects
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    string iString = reader.ReadString();
            //    Vec3 vector = ReadVec3();
            //    Vec3 vector2 = ReadVec3();
            //    float range = reader.ReadSingle();
            //    string iString2 = reader.ReadString();
            //}
            //
            //// Lights
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    /*names[i] = */
            //    string name = reader.ReadString();
            //    /*positions[i] = */
            //    ReadMatrix();
            //}
            //
            //// Collision Material and Collision Mesh
            //b = reader.ReadBoolean();
            //if (b)
            //{
            //    byte collisionMaterial = reader.ReadByte(); // enum CollisionMaterials
            //    /* List<Vector3> vertices = */
            //    ReadListVec3();
            //    num = reader.ReadInt32();
            //    for (int i = 0; i < num; ++i)
            //    {
            //        int triangleVertexIndex0 = reader.ReadInt32();
            //        int triangleVertexIndex1 = reader.ReadInt32();
            //        int triangleVertexIndex2 = reader.ReadInt32();
            //    }
            //}
            //
            //// AnimatedNavMesh
            //b = reader.ReadBoolean();
            //if (b)
            //{
            //    // animatedNavMesh = new AnimatedNavMesh(reader);
            //}
            //
            //// Add children animated level parts
            //num = reader.ReadInt32();
            //for (int i = 0; i < num; ++i)
            //{
            //    /*AnimatedLevelPart animatedLevelPart = new AnimatedLevelPart(reader, iLevel);*/
            //    // in the base game gode, it wouldd now do something like children.Add(animatedLevelPart);
            //}
        } /* still not fully implemented */

        // TODO : Implement
        // This model refers to animated models in XNA format. The reason Magicka has its own model format is because even regular models have to go through this and waste space having an empty set of bones and animation data.
        void ReadModel()
        {
            int aux = reader.Read7BitEncodedInt();
            // model.ReadBones()
            // model.ReadVertexDeclaration()
            // model.ReadMeshes()
            // model.ReadBoneReference()
            // model.Tag = input.ReadObject<object>()
        } /**/

        #endregion

        #region Debug

        private void Wait()
        {
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }

        #endregion

    }
}
