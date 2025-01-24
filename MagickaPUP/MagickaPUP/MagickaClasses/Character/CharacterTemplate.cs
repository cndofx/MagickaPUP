using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Audio;
using MagickaPUP.MagickaClasses.Lightning;

namespace MagickaPUP.MagickaClasses.Character
{
    // TODO : Change the way we handle values that go out of range for the sounds, lights and gibs... maybe just make this throw an exception so that we can bail out while writing a malformed file? like, if we input an invalid JSON file we should notify the error somehow, so yeah...

    public class CharacterTemplate : XnaObject
    {
        #region Variables

        // ID Strings
        public string characterID { get; set; }
        public string characterDisplayID { get; set; }

        // Enum data
        public Factions faction { get; set; }
        public BloodType bloodType { get; set; }

        // First set of Flags
        public bool isEthereal { get; set; }
        public bool looksEthereal { get; set; }
        public bool isFearless { get; set; }
        public bool isUncharmable { get; set; }
        public bool isNonSlippery { get; set; }
        public bool hasFairy { get; set; }
        public bool canSeeInvisible { get; set; }

        // Sounds
        public int numAttachedSounds { get; set; }
        public KeyValuePair<string, Banks>[] attachedSounds { get; set; }

        // Gibs
        public int numGibs { get; set; }
        public GibReference[] gibs { get; set; }

        // Lights
        public int numLights { get; set; }
        public LightHolder[] lights { get; set; }

        // Character Health data
        public float maxHitPoints { get; set; }
        public int numHealthBars { get; set; }

        // Second set of flags
        public bool isUndying { get; set; }
        public float undieTime { get; set; }
        public float undieHitPoints { get; set; }
        public int hitTolerance { get; set; }
        // TODO : Continue...

        #endregion

        #region Constructor

        public CharacterTemplate()
        {
            // ID Strings Initialization
            this.characterID = default;
            this.characterDisplayID = default;
            
            // Enum data initialization
            this.faction = default;
            this.bloodType = default;

            // Flags initialization
            this.isEthereal = false;
            this.looksEthereal = false;
            this.isFearless = false;
            this.isUncharmable = false;
            this.isNonSlippery = false;
            this.hasFairy = false;
            this.canSeeInvisible = false;

            // Sounds initialization
            this.numAttachedSounds = 0;
            this.attachedSounds = new KeyValuePair<string, Banks>[4]; // we hard code 4 because this is all that Magicka ever uses.

            // Gibs initialization
            this.numGibs = 0;
            this.gibs = new GibReference[0];

            // Light initialization
            this.numLights = 0;
            this.lights = new LightHolder[0]; // a character in Magicka can only hold up to 4 light holders.

            // Health initialization
            this.maxHitPoints = 100.0f; // Maybe this value is not a good one for Magicka, but this is the default I'm going with for now lol...
            this.numHealthBars = 1; // We should have at least 1 health bar, but even if we set it to 0 it does not matter, the reader code clamps it.
        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods

        public override void ReadInstance(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading CharacterTemplate...");

            throw new NotImplementedException("Read Character Template not implemented yet!");

            // Internally, Magicka computes a hash for both of these ID strings after reading them, but we don't need it here.
            this.characterID = reader.ReadString().ToLowerInvariant();
            this.characterDisplayID = reader.ReadString().ToLowerInvariant();

            // Read enum data
            this.faction = (Factions)reader.ReadInt32();
            this.bloodType = (BloodType)reader.ReadInt32();
            
            // Read flags
            this.isEthereal = reader.ReadBoolean();
            this.looksEthereal = reader.ReadBoolean();
            this.isFearless = reader.ReadBoolean();
            this.isUncharmable = reader.ReadBoolean();
            this.isNonSlippery = reader.ReadBoolean();
            this.hasFairy = reader.ReadBoolean();
            this.canSeeInvisible = reader.ReadBoolean();

            // Read character sounds
            this.numAttachedSounds = reader.ReadInt32(); // NOTE : To prevent having to store this value as a variable within this class and just working with the array input data from JSON, we could just initialize the attached sounds array to this input length here, and set the length to min(4, reader.readi32()), maybe do this in the future when we clean up all of the manually hard coded counts in the other JSON files for the level data and stuff?
            for (int i = 0; i < this.numAttachedSounds; ++i)
            {
                #region Comment
                // Can't read more than 4 sounds, since that is the max amount of sounds reserved by magicka for each CharacterTemplate, so we break out.
                // Note that imo this should potentially be an exception on mpup, but we'll do the thing that magicka does and just let it slide... for now.
                // Also, note that magicka's code just keeps looping over the non valid values out of bounds rather than breaking, so malformed files will not read
                // the bytes, which will break things and just crash on load, and also wastes runtime in case of a miswritten byte making this loop iterate for more than it should, when we can just break out and call it a day...
                // In the future, when the engine rewrite comes around, we could actually modify this to support more than 4 sounds per character template...
                #endregion
                if (i >= 4)
                    break;

                string str = reader.ReadString().ToLowerInvariant(); // id String
                Banks banks = (Banks)reader.ReadInt32(); // sound banks
                // Within magicka's code, we compute the hash of the string and then we store wihtin the attached sounds array a pair of type KeyValuePair<int, Banks>(hash,banks)
                // here we can just store a pair of string and banks and not give a fuck since we don't need to compute the hash for anything.
                this.attachedSounds[i] = new KeyValuePair<string, Banks>(str, banks);
            }

            // Read character gibs (GORE!!!!!!! YEAH, BABY!!!! BLOOD!)
            this.numGibs = reader.ReadInt32();
            this.gibs = new GibReference[numGibs];
            for(int i = 0; i < this.numGibs; ++i)
            {
                this.gibs[i] = GibReference.Read(reader, logger);
            }

            // Read character lights
            this.numLights = reader.ReadInt32();
            this.lights = new LightHolder[this.numLights];
            for (int i = 0; i < this.numLights; ++i)
            {
                // Once again, there's a limit of 4 for these. In this case tho, Magicka does throw an exception if the found character has more than 4 point light holders!
                if (i >= 4)
                    break;
                this.lights[i] = LightHolder.Read(reader, logger);
            }

            // Read character health data (HP and num health bars)
            // NOTE : The number of health bars should be at least 1 (duh). If the input is N <= 0, we set it to 1, just as Magicka does internally.
            this.maxHitPoints = reader.ReadSingle();
            this.numHealthBars = reader.ReadInt32();
            if (this.numHealthBars <= 0)
                this.numHealthBars = 1;


        }

        public static CharacterTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            var ans = new CharacterTemplate();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        public override void WriteInstance(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing CharacterTemplate...");

            throw new NotImplementedException("Write Character Template not implemented yet!");
        }

        #endregion
    }
}
