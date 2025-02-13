using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses.Xnb
{
    public class XnbFile
    {
        #region Variables

        public XnbHeader Header { get; set; } // TODO : Implement reading and writing for the header data...
        public XnaObject PrimaryObject { get; set; }
        public List<XnaObject> SharedResources { get; set; }

        #endregion

        #region Constructor

        public XnbFile()
        {
            this.PrimaryObject = new XnaObject();
            this.SharedResources = new List<XnaObject>();
        }

        #endregion

        #region PublicMethods

        public void SetPrimaryObject(XnaObject obj)
        {
            this.PrimaryObject = obj;
        }

        public void AddSharedResource(XnaObject obj)
        {
            this.SharedResources.Add(obj);
        }

        // MAYBE TODO : Maybe move all of the Packer and Unpacker logic to the Read and Write functions of this object? and we could also add other data such as the xnb header stuff as private or non json serializable variables and whatnot...
        // INDEED, It would make quite a lot of sense to move the read and write logic to this object rigth here to make things easier, including the header and stuff like that...

        #endregion
    }
}
