using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.XnaClasses
{
    public class XnbFileObject : XnaObject
    {
        #region Variables

        public XnaObject primaryObject { get; set; }
        
        public int numSharedResources { get; set; }
        public List<XnaObject> sharedResources { get; set; }

        #endregion

        #region Constructor

        public XnbFileObject()
        {
            this.primaryObject = new XnaObject();
            this.numSharedResources = 0;
            this.sharedResources = new List<XnaObject>();
        }

        #endregion

        #region PublicMethods

        public void SetPrimaryObject(XnaObject obj)
        {
            this.primaryObject = obj;
        }

        public void AddSharedResource(XnaObject obj)
        {
            this.sharedResources.Add(obj);
            ++this.numSharedResources;
        }

        // MAYBE TODO : Maybe move all of the Packer and Unpacker logic to the Read and Write functions of this object? and we could also add other data such as the xnb header stuff as private or non json serializable variables and whatnot...
        // INDEED, It would make quite a lot of sense to move the read and write logic to this object rigth here to make things easier, including the header and stuff like that...

        #endregion
    }
}
