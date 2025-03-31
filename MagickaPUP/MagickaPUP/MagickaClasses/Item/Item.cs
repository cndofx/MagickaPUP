using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Item.SpecialAbilities;
using MagickaPUP.MagickaClasses.Character.Events;
using MagickaPUP.MagickaClasses.Character.Aura;

namespace MagickaPUP.MagickaClasses.Item
{
    // NOTE : With the new object implementation, even classes that can be primary objects (like this one) don't really need to inherit from XnaObject.
    // As a matter of fact, XnaObject doesn't really even need to exist anymore... so yeah...
    // TODO : In the future, get rid of this XnaObject inheritance for code cleanup...
    public class Item : XnaObject
    {
        #region Structs

        // Read futher ahead to see the TODO comment regarding these structs...
        // The reason I have not used them yet is that I don't really know if it would make sense to put Homing inside of Ranged and Facing inside of Melee...
        // Who knows, we can divide that on the GUI side or whatever and just let the JSON side be more "raw" / "simpler"...
        /*
        public struct MeleeData
        {
            public float MeleeRange { get; set; }
            public bool MeleeMultiHit { get; set; }
            public ConditionCollection MeleeConditions { get; set; }
        }

        public struct RangedData
        {

        }

        public struct GunData
        { }
        */

        #endregion

        #region Variables

        // Item strings
        public string ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }

        // Sounds
        public Sound[] Sounds { get; set; }

        // Item properties
        public bool Pickable { get; set; }
        public bool Bound { get; set; }
        public int BlockValue { get; set; }
        public WeaponClass WeaponClass { get; set; } // NOTE : Maybe rename this to ItemType or whatever?
        public float CooldownTime { get; set; }
        public bool HideModel { get; set; }
        public bool HideEffect { get; set; }
        public bool PauseSounds { get; set; }

        // Resistances
        public Resistance[] Resistances { get; set; }

        // Passive Ability
        public PassiveAbility PassiveAbility { get; set; }

        // Effects
        public string[] Effects { get; set; }

        // Point Lights
        public PointLightHolder[] Lights { get; set; } // This being an array is a hacky workaround but it makes sense to a certain degree... in Magicka, XNB files for Items contain a list of point lights, but the game only supports 1 single point light so it throws an exception if more than 1 light was found. This could be understood as actually containing an i32 bool that says whether a light exists or not, but yeah... we could instead have a bool HasLight property and call it a day. But doing it this way, we ensure that multi-light objects are supported in the future if an engine rewrite is ever made in the future, which could allow supporting multiple lights. This feels like the easiest way to go, altough not the cleanest... This change also makes it possible to read XNB files for items that have no lights and actually getting an empty object (array in this case) rather than a default constructed light, which would make re-generating the XNB file actually wrong and add a light with 0 initialized values (those 0s coming from mpup's Item PointLightHolder's default constructor, not the game itself, ofc...)

        // Special Ability
        public SpecialAbilityStorage SpecialAbilityData { get; set; }

        // Weapon Properties
        // TODO : Maybe in the future pack this whole data into 3 structs: MeleeData, RangedData and GunData... maybe?
        // I mean, all properties HAVE to be present even if the weapon is of any given type that does not use all of them, so yeah...
        // it would just be to make organization a bit easier to deal with.
        public float MeleeRange { get; set; }
        public bool MeleeMultiHit { get; set; }
        public float RangedRange { get; set; }
        public bool Facing { get; set; }
        public float Homing { get; set; }
        public float RangedElevation { get; set; } // This appears to be an angle in degrees (which then gets translated into radians internally, but the XNB data itself is in degrees)
        public float RangedDanger { get; set; }
        public float GunRange { get; set; }
        public int GunClip { get; set; }
        public int GunRate { get; set; } // This value is stored as an i32 within the XNB files, but is then converted into an f32 in memory within Magicka's code... why? who knows!
        public float GunAccuracy { get; set; }

        // Assets Strings and Data
        // NOTE : All of these values can actually be null or empty strings. Magicka's code will simply default to using numeric ID 0 for all of them when the
        // input path strings are empty or null. Otherwise, it will use the strings' hashes as the ID.
        public string GunSoundID { get; set; }
        public string GunMuzzleEffectID { get; set; }
        public string GunShellsEffectID { get; set; }
        public float TracerVelocity { get; set; }
        public string NonTracerSprite { get; set; }
        public string TracerSprite { get; set; }

        // Melee Condition Collection
        public ConditionCollection MeleeConditions { get; set; }

        // Gun Condition Collection
        public ConditionCollection GunConditions { get; set; }

        // Ranged Condition Collection
        public ConditionCollection RangedConditions { get; set; }

        // Model Properties
        public string ProjectileModel { get; set; } /* ER */
        public string Model { get; set; } /* ER */
        public float Scale { get; set; }

        // Auras
        public AuraStorage[] Auras { get; set; }

        #endregion

        #region Constructor

        public Item()
        { }

        #endregion

        #region PublicMethods - Read and Write

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item...");

            // Item strings
            this.ItemID = reader.ReadString();
            this.ItemName = reader.ReadString();
            this.ItemDescription = reader.ReadString();

            logger?.Log(1, $" - ItemID          : {this.ItemID}");
            logger?.Log(1, $" - ItemName        : {this.ItemName}");
            logger?.Log(1, $" - ItemDescription : {this.ItemDescription}");

            // Sounds
            int numSounds = reader.ReadInt32();
            logger?.Log(1, $" - Num Sounds : {numSounds}");
            this.Sounds = new Sound[numSounds];
            for (int i = 0; i < numSounds; ++i)
                this.Sounds[i] = new Sound(reader, logger);

            // Item Properties
            this.Pickable = reader.ReadBoolean();
            this.Bound = reader.ReadBoolean();
            this.BlockValue = reader.ReadInt32();
            this.WeaponClass = (WeaponClass)reader.ReadByte();
            this.CooldownTime = reader.ReadSingle();
            this.HideModel = reader.ReadBoolean();
            this.HideEffect = reader.ReadBoolean();
            this.PauseSounds = reader.ReadBoolean();

            // Resistances
            int numResistances = reader.ReadInt32();
            logger?.Log(1, $" - Num Resistances : {numResistances}");
            this.Resistances = new Resistance[numResistances];
            for (int i = 0; i < numResistances; ++i)
                this.Resistances[i] = new Resistance(reader, logger);

            // Passive Ability
            this.PassiveAbility = new PassiveAbility(reader, logger);

            // Effects
            int numEffects = reader.ReadInt32();
            logger?.Log(1, $" - Num Effects : {numEffects}");
            this.Effects = new string[numEffects];
            for (int i = 0; i < numEffects; ++i)
            {
                logger?.Log(1, $" - Effect : {this.Effects[i]}");
                this.Effects[i] = reader.ReadString();
            }

            // Point Lights
            int numLightHolders = reader.ReadInt32();
            logger?.Log(1, $" - Num Lights : {numLightHolders}");
            
            if (numLightHolders > 1)
                throw new MagickaReadException("Magicka Items may only have one light!");

            this.Lights = new PointLightHolder[numLightHolders];
            for (int i = 0; i < numLightHolders; ++i)
                this.Lights[i] = new PointLightHolder(reader, logger);

            // Special Ability
            this.SpecialAbilityData = new SpecialAbilityStorage(reader, logger);

            logger.Log(1, $" - Has Special Ability : {this.SpecialAbilityData.HasSpecialAbility}");

            // Weapon Properties (1)
            this.MeleeRange = reader.ReadSingle();
            this.MeleeMultiHit = reader.ReadBoolean();

            // Melee Condition Collection
            this.MeleeConditions = new ConditionCollection(reader, logger);
            
            // Weapon Properties (2)
            this.RangedRange = reader.ReadSingle();
            this.Facing = reader.ReadBoolean();
            this.Homing = reader.ReadSingle();
            this.RangedElevation = reader.ReadSingle();
            this.RangedDanger = reader.ReadSingle();
            this.GunRange = reader.ReadSingle();
            this.GunClip = reader.ReadInt32();
            this.GunRate = reader.ReadInt32();
            this.GunAccuracy = reader.ReadSingle();

            // Assets Strings and Data
            this.GunSoundID = reader.ReadString();
            this.GunMuzzleEffectID = reader.ReadString();
            this.GunShellsEffectID = reader.ReadString();
            this.TracerVelocity = reader.ReadSingle();
            this.NonTracerSprite = reader.ReadString();
            this.TracerSprite = reader.ReadString();

            logger?.Log(1, $"GunSoundID        : {this.GunSoundID}");
            logger?.Log(1, $"GunMuzzleEffectID : {this.GunMuzzleEffectID}");
            logger?.Log(1, $"GunShellsEffectID : {this.GunShellsEffectID}");
            logger?.Log(1, $"TracerVelocity    : {this.TracerVelocity}");
            logger?.Log(1, $"NonTracerSprite   : {this.NonTracerSprite}");
            logger?.Log(1, $"TracerSprite      : {this.TracerSprite}");

            // Gun Conditions
            this.GunConditions = new ConditionCollection(reader, logger);

            // Model Properties (1)
            this.ProjectileModel = reader.ReadString();

            // Ranged Conditions
            this.RangedConditions = new ConditionCollection(reader, logger);

            // Model Properties (2)
            this.Scale = reader.ReadSingle();
            this.Model = reader.ReadString();

            logger?.Log(1, $" - Projectile Model : {this.ProjectileModel}");
            logger?.Log(1, $" - Model            : {this.Model}");
            logger?.Log(1, $" - Model Scale      : {this.Scale}");

            // Auras
            // TODO : Ensure that this is correctly implemented
            int numAuras = reader.ReadInt32();
            this.Auras = new AuraStorage[numAuras];
            for(int i = 0; i < numAuras; ++i)
                this.Auras[i] = new AuraStorage(reader, logger);
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item...");

            writer.Write(this.ItemID);
            writer.Write(this.ItemName);
            writer.Write(this.ItemDescription);

            writer.Write((int)this.Sounds.Length);
            foreach (var sound in this.Sounds)
                sound.Write(writer, logger);

            writer.Write(this.Pickable);
            writer.Write(this.Bound);
            writer.Write(this.BlockValue);
            writer.Write((byte)this.WeaponClass);
            writer.Write(this.CooldownTime);
            writer.Write(this.HideModel);
            writer.Write(this.HideEffect);
            writer.Write(this.PauseSounds);

            writer.Write((int)this.Resistances.Length);
            foreach(var resistance in this.Resistances)
                resistance.Write(writer, logger);

            this.PassiveAbility.Write(writer, logger);

            writer.Write((int)this.Effects.Length);
            foreach (var effect in this.Effects)
                writer.Write(effect);

            // TODO : Finish implementation

            throw new NotImplementedException("Write Item is not implemented yet!");
        }

        #endregion

        #region PublicMethods - Static

        public static Item Read(MBinaryReader reader, DebugLogger logger = null)
        {
            Item ans = new Item();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public static void Write(Item item, MBinaryWriter writer, DebugLogger logger = null)
        {
            if(item != null)
                item.WriteInstance(writer, logger);
        }

        #endregion
    }
}
