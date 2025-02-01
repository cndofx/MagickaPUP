using MagickaPUP.Utility.IO;
using MagickaPUP.MagickaClasses.Effects;
using MagickaPUP.MagickaClasses.Generic;
using MagickaPUP.MagickaClasses.Map;
using MagickaPUP.MagickaClasses.Areas;
using MagickaPUP.MagickaClasses.Lightning;
using MagickaPUP.MagickaClasses.Liquids;
using MagickaPUP.MagickaClasses.Nav;
using MagickaPUP.MagickaClasses.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel;
using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.PhysicsEntities;

namespace MagickaPUP.XnaClasses
{
    // README : Maybe change it so that all objects derive from this or some similar object as a base class?
    // then make it abstract, so that the read function could be virtual. That way, we could return this as a base object
    // type rather than just an "object" in the Read<> call on the unpacker program. Then, use that to just call .Read() and .Write() on
    // said object without any issues. I guess... Ofc, then, the read functions would no longer work by being static and they'd
    // have to be implemented on the object itself, so we'd say:
    //
    // Thing thing;
    // Thing.Read(reader, logger);
    //
    // rather than saying:
    //
    // Thing t = Thing.Read(reader, logger);
    //
    // altough we can still have a static method on the reader class that actually calls this under the hood:
    //
    // public static Thing ReadThing(reader, logger)
    // {
    //      Thing t = new Thing();
    //      t.Read(reader, logger);
    //      return t;
    // }
    //
    // or something like that... idk... whatever... we'll see when the time comes to actually write stuff back.
    [JsonDerivedType(typeof(XnaObject), typeDiscriminator: "base")]
    [JsonDerivedType(typeof(Locator), typeDiscriminator: "locator")]
    [JsonDerivedType(typeof(Trigger), typeDiscriminator: "trigger")]
    [JsonDerivedType(typeof(Effect), typeDiscriminator: "effect")]
    [JsonDerivedType(typeof(EffectDeferred), typeDiscriminator: "effect_deferred")]
    [JsonDerivedType(typeof(BoundingBox), typeDiscriminator: "bounding_box")]
    [JsonDerivedType(typeof(Matrix), typeDiscriminator: "matrix")]
    [JsonDerivedType(typeof(Quaternion), typeDiscriminator: "quaternion")]
    [JsonDerivedType(typeof(Vec3), typeDiscriminator: "vector3")]
    [JsonDerivedType(typeof(Light), typeDiscriminator: "light")]
    [JsonDerivedType(typeof(Liquid), typeDiscriminator: "liquid")]
    [JsonDerivedType(typeof(AnimatedLevelPart), typeDiscriminator: "animated_level_part")]
    [JsonDerivedType(typeof(BiTreeModel), typeDiscriminator: "bi_tree_model")]
    [JsonDerivedType(typeof(BiTreeNode), typeDiscriminator: "bi_tree_node")]
    [JsonDerivedType(typeof(BiTreeRootNode), typeDiscriminator: "bi_tree_root_node")]
    [JsonDerivedType(typeof(LevelModel), typeDiscriminator: "level_model")]
    [JsonDerivedType(typeof(NavMesh), typeDiscriminator: "nav_mesh")]
    [JsonDerivedType(typeof(Triangle), typeDiscriminator: "nav_triangle")]
    [JsonDerivedType(typeof(ForceField), typeDiscriminator: "force_field")]
    [JsonDerivedType(typeof(ContentTypeReader), typeDiscriminator: "xna_content_type_reader")]
    [JsonDerivedType(typeof(IndexBuffer), typeDiscriminator: "xna_index_buffer")]
    [JsonDerivedType(typeof(Model), typeDiscriminator: "xna_model")]
    [JsonDerivedType(typeof(VertexBuffer), typeDiscriminator: "xna_vertex_buffer")]
    [JsonDerivedType(typeof(VertexDeclaration), typeDiscriminator: "xna_vertex_declaration")]
    [JsonDerivedType(typeof(Texture2D), typeDiscriminator: "xna_texture_2d")]
    [JsonDerivedType(typeof(Texture2DData), typeDiscriminator: "xna_texture_2d_data")]
    [JsonDerivedType(typeof(CharacterTemplate), typeDiscriminator: "Character")]
    [JsonDerivedType(typeof(PhysicsEntityTemplate), typeDiscriminator: "PhysicsEntity")]
    public class XnaObject
    {
        public virtual void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new Exception("Base Object type XnaObject cannot be read! It contains no data to be read!");
        }

        public virtual void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new Exception("Base Object type XnaObject cannot be written! It contains no data to be written!");
        }

        public static T ReadObject<T>(MBinaryReader reader, DebugLogger logger = null)
        {
            // Read a 7 bit encoded int to obtain the index (starting at 1) of the required content type reader.
            // Subtract 1 because the indices start at 1 on the XNB file but they start at 0 in C#.
            int code = reader.Read7BitEncodedInt() - 1;
            string s = "__none__";
            object obj = null;

            if (code + 1 == 0)
            {
                logger?.Log(1, "Object is NULL.");
                return (T)obj;
            }

            if (code < 0 || code >= reader.ContentTypeReaders.Count)
            {
                throw new Exception($"Requested Content Type Reader does not exist! (Index = {code + 1})");
            }

            s = reader.ContentTypeReaders[code].Name;
            logger?.Log(1, $"Required Content Type Reader : {{ name = \"{s}\", index = {code + 1}}}");

            // Yes, I know I should make a hard coded list with these so that I can switch on index, and assign my indices to the readers list
            // rather than making them store strings, but I don't have the time to refact that right now, so we'll deal with the overhead until
            // I find the time to fix that. Besides, with this current (bad) implementation, at least I don't have to touch anything to be able
            // to read out the name of readers that my code does not support yet, so eh... all in all, C# makes things a fucking headache.
            // For now, please, forgive my sins.
            // This could be a job better suited to a Dictionary tho, but it does not matter much because as of now, it does not need to support that many type readers...
            switch (s)
            {
                case "Magicka.ContentReaders.LevelModelReader, Magicka":
                    obj = LevelModel.Read(reader, logger);
                    break;
                case "PolygonHead.Pipeline.BiTreeModelReader, PolygonHead":
                    obj = BiTreeModel.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.VertexDeclarationReader":
                    obj = VertexDeclaration.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.VertexBufferReader":
                    obj = VertexBuffer.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.IndexBufferReader":
                    obj = IndexBuffer.Read(reader, logger);
                    break;
                case "PolygonHead.Pipeline.RenderDeferredEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral":
                    obj = EffectDeferred.Read(reader, logger);
                    break;
                case "PolygonHead.Pipeline.AdditiveEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral":
                    obj = EffectAdditive.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.ModelReader":
                    obj = Model.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.StringReader":
                    obj = reader.ReadString(); // TODO : Check if this is correct.
                    break;
                case "Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]":
                    obj = Vec3.ReadList(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.Vector3Reader":
                    obj = Vec3.Read(reader, logger);
                    break;
                case "PolygonHead.Pipeline.RenderDeferredLiquidEffectReader, PolygonHead":
                    obj = EffectDeferredLiquid.Read(reader, logger);
                    break;
                case "PolygonHead.Pipeline.LavaEffectReader, PolygonHead, Version=1.0.0.0, Culture=neutral":
                    obj = EffectLava.Read(reader, logger);
                    break;
                case "Microsoft.Xna.Framework.Content.Texture2DReader":
                    obj = Texture2D.Read(reader, logger);
                    break;
                case "Magicka.ContentReaders.CharacterTemplateReader, Magicka, Version=1.0.0.0, Culture=neutral":
                    obj = CharacterTemplate.Read(reader, logger);
                    break;
                case "Magicka.ContentReaders.PhysicsEntityTemplateReader, Magicka":
                    obj = PhysicsEntityTemplate.Read(reader, logger);
                    break;
                default:
                    obj = null;
                    throw new Exception($"Content Reader Type \"{s}\" is not supported yet!");
                    break;
            }

            // This kind of gives me cancer, but I guess you could always do an "obj.GetType()" or "obj is" or whatever to
            // figure out what to do with this returned object according to its real type in respect to what is returned...
            return (T)obj;
        }

        public static void WriteObject(XnaObject obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            // README : This is a new addition, added for the Model class when writing objects that are null... this may fuck other shit up. Or not. Be on the lookout.
            if (obj == null)
            {
                WriteEmptyObject(writer, logger);
                return;
            }    

            string name = obj.GetReaderName();
            int index = XnaInfo.GetContentTypeReaderIndex(name);

            logger?.Log(1, $"Requesting ContentTypeReader \"{name}\" to read type \"{obj.GetType().Name}\"");

            if (index < 0)
            {
                throw new Exception($"No Content Type Writer exists for specified object! (\"{name}\")");
            }

            logger?.Log(1, $"Writing XNA Object with required ContentTypeReader \"{name}\", index {index + 1}...");

            writer.Write7BitEncodedInt(index + 1);
            obj.WriteInstance(writer, logger);
        }

        public static void WriteEmptyObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing NULL object to XNB file...");
            writer.Write7BitEncodedInt(0);
        }

        // Special case for List<Vec3>, if you can figure out a better way of doing this, give me a call lmao.
        public static void WriteObject(List<Vec3> obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            string name = "Microsoft.Xna.Framework.Content.ListReader`1[[Microsoft.Xna.Framework.Vector3, Microsoft.Xna.Framework, Version=3.1.0.0, Culture=neutral, PublicKeyToken=6d5c3888ef60e27d]]";
            WriteObjectList(obj, name, writer, logger);
        }

        // Special case for string
        public static void WriteObject(string str, MBinaryWriter writer, DebugLogger logger = null)
        {
            string name = "Microsoft.Xna.Framework.Content.StringReader";
            int idx = XnaInfo.GetContentTypeReaderIndex(name);
            logger?.Log(1, $"Requesting ContentTypeReader \"{name}\" to read type \"{str.GetType().Name}\"");
            writer.Write7BitEncodedInt(idx + 1);
            writer.Write(str);
        }

        // TODO : Cleanup and remove
        // Special case for object
        /*
        public static void WriteObject(object obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            // etc...
        }
        */

        // As ugly as this is, this is actually very similar to what the real XNA framework does when an user implements their own Content Type Readers and Writers...
        // README : Should try to find a way to get rid of the large switch and just try to make use of a similar system to this one...? maybe?
        public virtual string GetReaderName()
        {
            return "__none__";
        }

        // Internal implementation to read any list type. Requires knowing the reader's string beforehand, so the public interface will have a function for every possible case,
        // such as List<Vec3>.
        // This could probably be "simplied" further if appart from GetReaderName(), we added a GetListReaderName() method or whatever... that way, we could get rid of the specialized static methods.
        // TODO : It no longer requires public specific implementations, so we can just make this the catch all public impl, but I would need to clean up some shit like the other public List<Vec3> implementation and remove it, etc...
        // TODO : Maybe we can use the GetReaderName() method to skip the name param, since all XnaObjects have that method, which would technically allow making any object be readable as a primary object or as a list of objects... or make a GetListReaderName() method which returns the name for the list version, which tbh I'm not sure it would be needed considering how so far only stuff like Vec3 seems to be required, so it's kinda like a special case, yk? Or maybe we could replace this with some custom struct that contains a single object reader and a list reader, so we could have something like GetContentTypeReader().SingleObject and GetContentTypeReader().List or whatever. Or make it a property, etc... it would be a good idea to move away from this monolithic class into a more interface based system tbh cause we could then make it into a property instead.
        private static void WriteObjectList<T>(List<T> obj, string name, MBinaryWriter writer, DebugLogger logger = null) where T : XnaObject
        {
            int index = XnaInfo.GetContentTypeReaderIndex(name);

            logger?.Log(1, $"Requesting ContentTypeReader \"{name}\" to read type \"{obj.GetType().Name}\"");

            if (index < 0)
            {
                throw new Exception($"No Content Type Writer exists for specified object! (\"{name}\")");
            }

            logger?.Log(1, $"Writing List of XNA Objects with required ContentTypeReader \"{name}\", index {index + 1}...");

            writer.Write7BitEncodedInt(index + 1);

            writer.Write((int)obj.Count);

            if (obj.Count <= 0)
                return;

            // README : Read the notes in Vec3's ReadList to know why the commented out part is wrong...
            // TODO : Cleanup
            // int itrCode = XnaInfo.GetContentTypeReaderIndex(obj[0].GetReaderName());
            for (int i = 0; i < obj.Count; ++i)
            {
                // writer.Write7BitEncodedInt(itrCode);
                obj[i].WriteInstance(writer, logger);
            }
        }

        // This shit is pretty fucking dumb, should probably move it into a separate PrimaryXnaObject class that inherits from XnaObject or whatever...
        // Because this should contain a list of ALL of the required content readers to read through this specific object, including its child objects.
        // Yes, we could always embed some instruction within the write instance call where we tell it to add to some sort of list its content reader
        // and then write them all in one go, but that would require reworking too much shit, so this is easier to do...
        public virtual ContentTypeReader[] GetRequiredContentReaders()
        {
            ContentTypeReader[] ans = {
                // Empty array by default, cause no readers are required by default...
            };
            return ans;
        }
    }
}
