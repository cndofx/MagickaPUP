using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses;
using System;
using MagickaPUP.Utility.Exceptions;
using MagickaPUP.MagickaClasses.Item.SpecialAbilities;

namespace MagickaPUP.MagickaClasses.Item
{
    // NOTE : With the new object implementation, even classes that can be primary objects (like this one) don't really need to inherit from XnaObject.
    // As a matter of fact, XnaObject doesn't really even need to exist anymore... so yeah...
    // TODO : In the future, get rid of this XnaObject inheritance for code cleanup...
    public class Item : XnaObject
    {
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

        // Lights
        public PointLightHolder PointLightHolder { get; set; }

        // Special Ability
        public SpecialAbilityStorage SpecialAbilityData { get; set; }


        #endregion

        #region Constructor

        public Item()
        { }

        #endregion

        #region PublicMethods - Read and Write

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading Item...");

            this.ItemID = reader.ReadString();
            this.ItemName = reader.ReadString();
            this.ItemDescription = reader.ReadString();

            int numSounds = reader.ReadInt32();
            this.Sounds = new Sound[numSounds];
            for (int i = 0; i < numSounds; ++i)
                this.Sounds[i] = new Sound(reader, logger);

            this.Pickable = reader.ReadBoolean();
            this.Bound = reader.ReadBoolean();
            this.BlockValue = reader.ReadInt32();
            this.WeaponClass = (WeaponClass)reader.ReadByte();
            this.CooldownTime = reader.ReadSingle();
            this.HideModel = reader.ReadBoolean();
            this.HideEffect = reader.ReadBoolean();
            this.PauseSounds = reader.ReadBoolean();

            int numResistances = reader.ReadInt32();
            this.Resistances = new Resistance[numResistances];
            for(int i = 0; i < numResistances; ++i)
                this.Resistances[i] = new Resistance(reader, logger);

            this.PassiveAbility = new PassiveAbility(reader, logger);

            int numEffects = reader.ReadInt32();
            this.Effects = new string[numEffects];
            for(int i = 0; i < numEffects; ++i)
                this.Effects[i] = reader.ReadString();

            int numLightHolders = reader.ReadInt32();
            if (numLightHolders > 1)
                throw new MagickaReadException("Magicka Items may only have one light!");
            this.PointLightHolder = new PointLightHolder(reader, logger);

            this.SpecialAbilityData = new SpecialAbilityStorage(reader, logger);

            throw new NotImplementedException("Read Item is not implemented yet!");
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing Item...");

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
