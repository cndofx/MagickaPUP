using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Item.SpecialAbilities
{
    // NOTE : This class does NOT exist within Magicka's code. This is just a proxy class that was added to make it easier to structure the code.
    // TODO : Get rid of this class, maybe? it's not really useful when compared to the analogue for regular abilities, since that actually does benefit from this due to its more complex derived class structure... this one just exists to bother us and that's it lol...
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
                this.SpecialAbility = new SpecialAbility(reader, logger);
            }
            else
            {
                this.SpecialAbilityRechargeTime = 0.0f;
                this.SpecialAbility = null;
            }
        }

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {
            logger?.Log(1, "Writing SpecialAbility Data...");

            throw new NotImplementedException("Write SpecialAbility Data is not implemented yet!");
        }
    }
}
