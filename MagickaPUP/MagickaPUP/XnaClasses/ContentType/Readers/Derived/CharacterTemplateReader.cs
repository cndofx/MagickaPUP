using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.Utility.IO;
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

        public override CharacterTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
