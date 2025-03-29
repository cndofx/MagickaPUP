using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item.SpecialAbilities
{
    // NOTE : This class does NOT exist within Magicka's code. This is just a proxy class that was added to make it easier to structure the code.
    public class SpecialAbilityStorage
    {
        public bool HasSpecialAbility { get; set; }
        public float SpecialAbilityRechargeTime { get; set; }
        public SpecialAbility SpecialAbility { get; set; }

        public SpecialAbilityStorage()
        {
            this.HasSpecialAbility = false;
            this.SpecialAbilityRechargeTime = 0.0f;
            this.SpecialAbility = null;
        }

        public SpecialAbilityStorage(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading SpecialAbility Data...");

            this.HasSpecialAbility = reader.ReadBoolean();
            if (this.HasSpecialAbility)
            {
                this.SpecialAbilityRechargeTime = reader.ReadSingle();
                this.SpecialAbility = null; // TODO : Implement SpecialAbility reading...
            }

            throw new NotImplementedException("Read SpecialAbility Data is not implemented yet!");
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpecialAbility Data...");

            throw new NotImplementedException("Write SpecialAbility Data is not implemented yet!");
        }
    }
}
