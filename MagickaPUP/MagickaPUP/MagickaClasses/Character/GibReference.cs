using MagickaPUP.XnaClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.MagickaClasses.Character
{
    public class GibReference : XnaObject
    {
        #region Variables

        public string model { get; set; }
        public float mass { get; set; }
        public float scale { get; set; }

        #endregion

        #region Constructor

        public GibReference()
        {
            this.model = default;
            this.mass = 1.0f;
            this.scale = 1.0f;
        }

        #endregion

        #region PublicMethods
        #endregion

        #region PrivateMethods
        #endregion
    }
}
