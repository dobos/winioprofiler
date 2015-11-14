using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elte.WinIOProfiler
{
    public static class Util
    {
        public static string HexToChar(string hex)
        {
            string res = String.Empty;

            for (int i = 0; i < hex.Length / 2; i++)
            {
                // Take two characters (two digit hex number) and get character
                int v = int.Parse(hex.Substring(2 * i, 2), System.Globalization.NumberStyles.AllowHexSpecifier);
                res += char.ConvertFromUtf32(v);
            }

            return res;
        }

        public static string FormatFileSize(ulong size)
        {
            int order = 0;
            double len = size;

            while (len >= 1024 && order + 1 < Constants.FileSizeUnits.Length)
            { 
                order++; 
                len /= Constants.KiloByte; 
            } 
    
            return String.Format("{0:F0} {1}", len, Constants.FileSizeUnits[order]); 
        }
    }
}
