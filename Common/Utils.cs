using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace Common
{
    public class Utils
    {
        [DllImport("shell32.dll", EntryPoint = "ShellExecute")]
        public static extern long ShellExecute(int hwnd, string cmd, string file, string param1, string param2, int swmode);

        public static long OsExecuteOpen(string command)
        {
            return ShellExecute(0, "open", command, "", "", 5);
        }

        public static long OsExecuteMailTo(string mail)
        {
            return ShellExecute(0, "mailto", mail, "", "", 5);
        }

        
    }
}
