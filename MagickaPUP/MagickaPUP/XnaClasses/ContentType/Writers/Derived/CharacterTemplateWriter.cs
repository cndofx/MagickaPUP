using MagickaPUP.MagickaClasses.Character;
using MagickaPUP.Utility.IO;
using MagickaPUP.XnaClasses.ContentType.Writers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Specific.Derived
{
    public class CharacterTemplateWriter : TypeWriter<CharacterTemplate>
    {
        public CharacterTemplateWriter()
        { }

        public override void Write(CharacterTemplate instance, MBinaryWriter writer, DebugLogger logger = null)
        {
            CharacterTemplate.Write(instance, writer, logger);
        }
    }
}
