using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagickaPUP.IO
{
    public enum MessageType
    {
        Default = 0,
        Warning,
        Error
    }

    public class DebugLogger
    {
        #region Variables

        private string name;
        private int debugLevel;

        #endregion

        #region Properties

        public string Name { get { return this.name; } set { this.name = value; } }
        public int DebugLevel { get { return this.debugLevel; } set { this.debugLevel = value; } }

        #endregion

        #region Constructor

        public DebugLogger(string chosenLoggerName, int chosenDebugLevel = 1)
        {
            this.name = chosenLoggerName;
            this.debugLevel = chosenDebugLevel;
        }

        #endregion

        #region Public Methods

        public void Log(int debugLevelRequired, string msg)
        {
            Println(debugLevelRequired, $"[{this.name}] : {msg}");
        }

        #endregion

        #region PrivateMethods

        public void Print(int debugLevelRequired, string msg)
        {
            if (this.debugLevel >= debugLevelRequired)
                Console.Write(msg);
        }

        public void Println(int debugLevelRequired, string msg)
        {
            if (this.debugLevel >= debugLevelRequired)
                Console.WriteLine(msg);
        }

        private static ConsoleColor GetMessageTypeColor(MessageType category)
        {
            ConsoleColor color = ConsoleColor.Gray;

            switch (category)
            {
                default:
                case MessageType.Default:
                    color = ConsoleColor.Gray;
                    break;
                case MessageType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case MessageType.Error:
                    color = ConsoleColor.Red;
                    break;
            }

            return color;
        }

        private static void SetConsoleColor(MessageType category)
        {
            Console.ForegroundColor = GetMessageTypeColor(category);
        }

        private static void SetConsoleColor(int debugLevel)
        {
            SetConsoleColor((MessageType)debugLevel);
        }

        #endregion
    }
}
