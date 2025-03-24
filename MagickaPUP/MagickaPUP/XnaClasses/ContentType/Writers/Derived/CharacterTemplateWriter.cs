using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.Utility.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class CharacterTemplateWriter : TypeReader<CharacterTemplate>
    {
        public CharacterTemplateWriter()
        { }

        public override CharacterTemplate Read(MBinaryReader reader, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }

        public override void Write(CharacterTemplate instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            throw new NotImplementedException();
        }
    }
}
