using MagickaPUP.Utility.IO;
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

        public XnbFile(MBinaryReader reader, DebugLogger logger = null)
        {
            logger?.Log(1, "Reading XNB File...");

            // Validate the input data to check if it is a valid XNB file
            logger?.Log(1, "Validating XNB File...");
            char x = reader.ReadChar();
            char n = reader.ReadChar();
            char b = reader.ReadChar();
            string headerString = $"{x}{n}{b}";
            if (!(x == 'X' && n == 'N' && b == 'B'))
            {
                logger.Log(1, $"Header \"{headerString}\" is not valid!");
                return;
            }
            logger?.Log(1, "Header \"XNB\" is valid!");

            // Perform platform validation.
            // Check if the platform is Windows. (No other platforms are supported in Magicka, so it really can't be anything else...)
            char platform = reader.ReadChar();
            if (platform != 'w')
            {
                logger.Log(1, $"Platform \"{platform}\" is not valid.");
                return;
            }
            logger.Log(1, $"Platform \"{platform}\" is valid (Windows)");


        }

        #endregion

        #region PublicMethods

        public void Write(MBinaryWriter writer, DebugLogger logger = null)
        {

        }

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
