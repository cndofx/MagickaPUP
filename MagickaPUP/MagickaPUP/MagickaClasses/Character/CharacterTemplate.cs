using MagickaPUP.IO;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MagickaPUP.MagickaClasses.Data;

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
