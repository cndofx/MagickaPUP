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
using System.Text.Json.Serialization;
using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.MagickaClasses.PhysicsEntities;
using MagickaPUP.XnaClasses.Xna.Data;
using MagickaPUP.Core.Content.Pipeline.Export;
using MagickaPUP.Core.Content.Pipeline.Export.Derived;
using MagickaPUP.Core.Content.Data;
using MagickaPUP.XnaClasses.Xnb;
using MagickaPUP.MagickaClasses.Item;

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
    [JsonDerivedType(typeof(EffectAdditive), typeDiscriminator: "effect_additive")]
    [JsonDerivedType(typeof(EffectDeferred), typeDiscriminator: "effect_deferred")]
    [JsonDerivedType(typeof(EffectDeferredLiquid), typeDiscriminator: "effect_deferred_liquid")]
    [JsonDerivedType(typeof(EffectLava), typeDiscriminator: "effect_lava")]
    [JsonDerivedType(typeof(MagickaClasses.Generic.BoundingBox), typeDiscriminator: "bounding_box")] // TODO : This BB class should not be a primary object ever, along with many other classes... we should clean this shit up to speed up serialization tbh.
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
    [JsonDerivedType(typeof(IndexBuffer), typeDiscriminator: "xna_index_buffer")]
    [JsonDerivedType(typeof(Model), typeDiscriminator: "xna_model")]
    [JsonDerivedType(typeof(VertexBuffer), typeDiscriminator: "xna_vertex_buffer")]
    [JsonDerivedType(typeof(VertexDeclaration), typeDiscriminator: "xna_vertex_declaration")]
    [JsonDerivedType(typeof(Texture2D), typeDiscriminator: "xna_texture_2d")]
    [JsonDerivedType(typeof(Texture2DData), typeDiscriminator: "xna_texture_2d_data")]
    [JsonDerivedType(typeof(CharacterTemplate), typeDiscriminator: "Character")]
    [JsonDerivedType(typeof(PhysicsEntityTemplate), typeDiscriminator: "PhysicsEntity")]
    [JsonDerivedType(typeof(Item), typeDiscriminator: "Item")]
    [JsonDerivedType(typeof(EffectCode), typeDiscriminator: "EffectCode")]
    [JsonDerivedType(typeof(EffectMaterial), typeDiscriminator: "EffectMaterial")]
    public class XnaObject
    {
        #region Instance Methods

        // NOTE : These are methods that correspond to an individual instance of an object that inherits from XnaObject

        public virtual void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new Exception("Base Object type XnaObject cannot be read! It contains no data to be read!");
        }

        public virtual void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new Exception("Base Object type XnaObject cannot be written! It contains no data to be written!");
        }

        #endregion

        #region Experimental

        // NOTE : Pretty good functions but temporarily disabled because I am not sure how I could rework support for non XnaObject object types
        // with something like this. For example, what happens with reading raw objects, reading strings, reading List<XnaObject>, etc...?
        // We could maybe make a wrapper class for those, but I am not sure how to feel about that...
        /*
        public void ReadObject(MBinaryReader reader, DebugLogger logger = null)
        {
            string readerName = this.GetReaderName();
            for (int i = 0; i < reader.ContentTypeReaders.Count; ++i)
            {
                if (reader.ContentTypeReaders[i].Name == readerName)
                {
                    reader.Read7BitEncodedInt(); // Read the idx value and discard it because we'll reorganize things by adding our own readers when we write the binary data.
                    ReadInstance(reader, logger);
                    return;
                }
            }
            throw new Exception("Could not find content type reader!");
        }

        public void WriteObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            string readerName = this.GetReaderName();
            for (int i = 0; i < writer.ContentTypeReaders.Count; ++i)
            {
                if (writer.ContentTypeReaders[i].Name == readerName)
                {
                    writer.Write7BitEncodedInt(i + 1); // Write the idx value for this content type reader (in XNB files, the indices start at 1 so we add 1).
                    WriteInstance(writer, logger);
                    return;
                }
            }
            throw new Exception("Could not find content type reader!");
        }
        */


        #endregion

        #region Static Methods
        /*
        public static T ReadObject<T>(MBinaryReader reader, DebugLogger logger = null) where T : XnaObject, new()
        {
            var obj = new T();

            ContentTypeReader contentTypeReader = obj.GetObjectContentTypeReader();
            int index;
            int realIndex;

            logger?.Log(1, $"Requesting ContentTypeReader \"{contentTypeReader.Name}\" to read type \"{obj.GetType().Name}\"");
            index = reader.ContentTypeReaderStorage.GetReaderIndex(contentTypeReader.Name);
            if (index < 0)
                index = reader.ContentTypeReaderStorage.AddReader(contentTypeReader); // We are permissive and allow input XNB files that are malformed and lack content type readers. That way, we can correct them ourselves later on by recompiling.
            realIndex = index + 1;

            logger?.Log(1, $"Reading XNA Object with required ContentTypeReader \"{contentTypeReader.Name}\", index {realIndex}...");
            int expectedIndex = reader.Read7BitEncodedInt(); // NOTE : If the requested index does not match, we don't care, as mentioned before, we're permissive so that we can recompile and fix broken XNB files.
            obj.ReadInstance(reader, logger);
            return obj;
        }

        public static string ReadObject(MBinaryReader reader, DebugLogger logger = null)
        {
            int aux = reader.Read7BitEncodedInt();
            string ans = reader.ReadString();
            return ans;
        }

        public static T ReadObjectz<T>(MBinaryReader reader, DebugLogger logger = null)
        {
            // Read a 7 bit encoded int to obtain the index (starting at 1) of the required content type reader.
            int indexContentTypeReaderXnb = reader.Read7BitEncodedInt();

            // Subtract 1 because the indices start at 1 on the XNB file but they start at 0 in C#.
            int indexContentTypeReaderReal = indexContentTypeReaderXnb - 1;
            
            object obj = null;

            // Handle reading object with reader index 0 (NULL object)
            // NOTE : This could be understood as thinking of XNB content readers actually starting at index 0 and always containing an implicit NULL object reader.
            if (indexContentTypeReaderXnb == 0)
            {
                logger?.Log(1, "Object is NULL.");
                return (T)obj;
            }

            if (indexContentTypeReaderReal < 0 || indexContentTypeReaderReal >= reader.ContentTypeReaderStorage.Count)
            {
                throw new Exception($"Requested Content Type Reader does not exist! (Index = {indexContentTypeReaderXnb})");
            }

            // Get the content type reader
            ContentTypeReader contentTypeReader = reader.ContentTypeReaderStorage.ContentTypeReaders[indexContentTypeReaderReal]; // TODO : Change this to get the readers from a CTX var in the future.
            logger?.Log(1, $"Required Content Type Reader : {{ name = \"{contentTypeReader.Name}\", index = {indexContentTypeReaderXnb}}}");

            // Read the data and return the constructed object
            switch (contentTypeReader.Name)
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
                    throw new Exception($"Content Reader Type \"{contentTypeReader.Name}\" is not supported yet!");
            }

            // This kind of gives me cancer, but I guess you could always do an "obj.GetType()" or "obj is" or whatever to
            // figure out what to do with this returned object according to its real type in respect to what is returned...
            return (T)obj;
        }

        // Generic function to read XnaObjects
        public static void WriteObject(XnaObject obj, MBinaryWriter writer, DebugLogger logger = null)
        {
            // NOTE : If the input object is null, then call the internal write empty object method.
            // This was added mainly for the Model class when writing objects that are null for the bone tags.
            if (obj == null)
            {
                WriteEmptyObject(writer, logger);
                return;
            }    

            ContentTypeReader contentTypeReader = obj.GetObjectContentTypeReader();
            int index;
            int realIndex;

            logger?.Log(1, $"Requesting ContentTypeReader \"{contentTypeReader.Name}\" to read type \"{obj.GetType().Name}\"");
            index = writer.ContentTypeReaderStorage.GetReaderIndex(contentTypeReader.Name);
            if (index < 0)
                index = writer.ContentTypeReaderStorage.AddReader(contentTypeReader);
            realIndex = index + 1;

            logger?.Log(1, $"Writing XNA Object with required ContentTypeReader \"{contentTypeReader.Name}\", index {realIndex}...");
            writer.Write7BitEncodedInt(realIndex);
            obj.WriteInstance(writer, logger);
        }

        // Special case for strings
        public static void WriteObject(string str, MBinaryWriter writer, DebugLogger logger = null)
        {
            ContentTypeReader contentTypeReader = new ContentTypeReader("Microsoft.Xna.Framework.Content.StringReader", 0);
            int index;
            int realIndex;

            logger?.Log(1, $"Requesting ContentTypeReader \"{contentTypeReader.Name}\" to read type \"{str.GetType().Name}\"");
            index = writer.ContentTypeReaderStorage.GetReaderIndex(contentTypeReader.Name);
            if(index < 0)
                index = writer.ContentTypeReaderStorage.AddReader(contentTypeReader);
            realIndex = index + 1;

            logger?.Log(1, $"Writing String with required ContentTypeReader \"{contentTypeReader.Name}\", index {realIndex}...");
            writer.Write7BitEncodedInt(realIndex);
            writer.Write(str);
        }

        // Special case for empty objects
        public static void WriteEmptyObject(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing NULL object to XNB file...");
            writer.Write7BitEncodedInt(0);
        }

        public static void WriteObject<T>(List<T> objList, MBinaryWriter writer, DebugLogger logger = null) where T : XnaObject
        {
            #region Comment

            // Internal implementation to read any list type. Requires knowing the reader's string beforehand, so the public interface will have a function for every possible case,
            // such as List<Vec3>.
            // This could probably be "simplied" further if appart from GetReaderName(), we added a GetListReaderName() method or whatever... that way, we could get rid of the specialized static methods.
            // TODO : It no longer requires public specific implementations, so we can just make this the catch all public impl, but I would need to clean up some shit like the other public List<Vec3> implementation and remove it, etc...
            // TODO : Maybe we can use the GetReaderName() method to skip the name param, since all XnaObjects have that method, which would technically allow making any object be readable as a primary object or as a list of objects... or make a GetListReaderName() method which returns the name for the list version, which tbh I'm not sure it would be needed considering how so far only stuff like Vec3 seems to be required, so it's kinda like a special case, yk? Or maybe we could replace this with some custom struct that contains a single object reader and a list reader, so we could have something like GetContentTypeReader().SingleObject and GetContentTypeReader().List or whatever. Or make it a property, etc... it would be a good idea to move away from this monolithic class into a more interface based system tbh cause we could then make it into a property instead.

            #endregion

            var aux = Activator.CreateInstance<T>();
            ContentTypeReader contentTypeReader = aux.GetListContentTypeReader();
            int index;
            int realIndex;

            logger?.Log(1, $"Requesting ContentTypeReader \"{contentTypeReader.Name}\" to read type \"{objList.GetType().Name}\"");
            index = writer.ContentTypeReaderStorage.GetReaderIndex(contentTypeReader.Name);
            if (index < 0)
                index = writer.ContentTypeReaderStorage.AddReader(contentTypeReader);
            realIndex = index + 1;

            logger?.Log(1, $"Writing List of XNA Objects with required ContentTypeReader \"{contentTypeReader.Name}\", index {realIndex}...");
            writer.Write7BitEncodedInt(realIndex);
            writer.Write((int)objList.Count);
            if (objList.Count <= 0)
                return;
            for (int i = 0; i < objList.Count; ++i)
                objList[i].WriteInstance(writer, logger);
        }
        */
        #endregion

        #region Config Methods

        // NOTE : These are methods that correspond to config specific to each type of object. Stuff like the reader names and all that...

        // As ugly as this is, this is actually very similar to what the real XNA framework does when an user implements their own Content Type Readers and Writers...
        // README : Should try to find a way to get rid of the large switch and just try to make use of a similar system to this one...? maybe?
        /*
        public virtual string GetReaderName()
        {
            return "__none__";
        }
        */

        public virtual bool ShouldAppendNullObject()
        {
            return false;
        }

        public virtual ContentTypeReader GetObjectContentTypeReader()
        {
            throw new Exception($"Base Object type XnaObject has no content type reader for Object of type \"{this.GetType()}\"!");
        }

        public virtual ContentTypeReader GetListContentTypeReader()
        {
            throw new Exception($"Base Object type XnaObject has no content type reader for List<Object> of type \"{this.GetType()}\"!");
        }

        // The file type used by Magicka (the game) when an object is in its XNA packed form.
        // XNB by default, but could be other types like xwb and such.
        // NOTE : This could become useless in the future, as it would make more sense to simply have a different class for XWB.
        // What I mean with this is that since XWB is not an XNB, rather than XnbFile and XnbFileData we should probably have an XwbFile class for this case.
        // But we'll see in the future when the time comes what is it that I do...
        public virtual FileType GetFileTypePacked()
        {
            return FileType.Xnb;
        }

        // The file type used by MagickaPUP (the tool) when an object is in its unpacked form.
        // JSON by default, but could be other types like png, wav, or any other type that users can easily edit by hand.
        public virtual FileType GetFileTypeUnpacked()
        {
            return FileType.Json;
        }

        public virtual ExporterBase<XnbFile> GetUnpackExporter()
        {
            return new JsonExporter();
        }

        #endregion
    }
}
