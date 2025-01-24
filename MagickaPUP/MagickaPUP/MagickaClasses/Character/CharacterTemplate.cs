using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.MagickaClasses.Audio;

namespace MagickaPUP.MagickaClasses.Character
{
    public class CharacterTemplate : XnaObject
    {
        #region Variables

        // ID Strings
        public string characterID { get; set; }
        public string characterDisplayID { get; set; }

        // Enum data
        public Factions faction { get; set; }
        public BloodType bloodType { get; set; }
        
        // Flags
        public bool isEthereal { get; set; }
        public bool looksEthereal { get; set; }
        public bool isFearless { get; set; }
        public bool isUncharmable { get; set; }
        public bool isNonSlippery { get; set; }
        public bool hasFairy { get; set; }
        public bool canSeeInvisible { get; set; }

        // Sounds
        public KeyValuePair<string, Banks>[] attachedSounds { get; set; }

        #endregion

        #region Constructor
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
            int numSounds = reader.ReadInt32();
            for (int i = 0; i < numSounds; ++i)
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
