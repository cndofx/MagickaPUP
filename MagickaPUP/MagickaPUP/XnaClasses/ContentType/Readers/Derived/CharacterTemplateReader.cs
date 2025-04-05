using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.Utility.IO;
using MagickaPUP.Utility.IO.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class CharacterTemplateReader : TypeReader<CharacterTemplate>
    {
        public CharacterTemplateReader()
        { }

        public override CharacterTemplate Read(CharacterTemplate instance, MBinaryReader reader, DebugLogger logger = null)
        {
            CharacterTemplate ans = null;
            switch (reader.GameVersion)
            {
                default:
                case GameVersion.Auto: // NOTE : This may or may not be a terrible fucking idea, but I could not come up with anything better after thinking a lot about it...
                    try
                    {
                        ans = ReadNew(instance, reader, logger);
                    }
                    catch
                    {
                        ans = ReadOld(instance, reader, logger);
                    }
                    break; // If all else fails, then the input data is malformed as fuck!

                case GameVersion.Old:
                    ans = ReadOld(instance, reader, logger);
                    break;

                case GameVersion.New:
                    ans = ReadNew(instance, reader, logger);
                    break;
            }
            return ans;
        }

        private CharacterTemplate ReadOld(CharacterTemplate instance, MBinaryReader reader, DebugLogger logger = null)
        {
            CharacterTemplate ans = new CharacterTemplate();
            ans.ReadInstance(reader, logger);
            return ans;
        }

        private CharacterTemplate ReadNew(CharacterTemplate instance, MBinaryReader reader, DebugLogger logger = null)
        {
            CharacterTemplate ans = new CharacterTemplate();
            ans.ReadInstance(reader, logger);
            return ans;
        }
    }
}
