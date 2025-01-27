using MagickaPUP.MagickaClasses.Data;
using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    // NOTE : This struct goes unused because of an obvious problem: each time Damage is read within Magicka's code, it is performed with completely different
    // orders and data types, as the reads are performed inline, so pretty much this is useless for us, specially since sometimes damage has float fields and sometimes
    // integer fields. Under the hood within Magicka it is all turned into floats, but if we did the same we would have some precission problems and unncessary
    // conversions in between, so it's better to just hard code this shit for now I suppose...

    // For now I'll just make a compromise on the precission loss and fuck it. Things would be easier with unions, or maybe make 2 different Damage types for mpup,
    // but whatever. We'll just convert to float, which is what Magicka does anyway, so we can be at least consistent enough where users don't have to question their
    // sanity...

    public struct Damage
    {
        public AttackProperties AttackProperty { get; set; }
        public Elements Element { get; set; }
        public float Amount { get; set; }
        public float Magnitude { get; set; }

        public Damage(AttackProperties attackProperty = default, Elements element = default, float amount = 1.0f, float magnitude = 1.0f)
        {
            this.AttackProperty = attackProperty;
            this.Element = element;
            this.Amount = amount;
            this.Magnitude = magnitude;
        }

        /*
        public void Read_iiff()
        { }

        public void Read_iiif()
        { }

        public void Read_iiii()
        { }
        */
    }
}
