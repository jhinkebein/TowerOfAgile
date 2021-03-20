using System;
using System.Collections.Generic;
using System.Text;

namespace TowerOfAgile.Models
{
    //Creates a code, either alphanumeric or just numeric
    //Making it super in depth so if I need it again I have it
    public enum KeyLength //alphanumeric
    {
        l16 = 16, l20 = 20, l24 = 24, l28 = 28, l32 = 32
    }
    public enum KeyNumLength //For numeric only
    {
        n4 = 4, n8 = 8, n12 = 12
    }
    public static class RandomCodeGenerator
    {
        private static string AppendStr(int length, string str, char[] newKey)
        {
            string newKeyStr = String.Empty;
            int k = 0;
            for (int i = 0; i < length; i++)
            {
                for (k = i; k < 4 + i; k++)
                {
                    newKeyStr += newKey[k];
                }
                if (k == length)
                {
                    break;
                }
                else
                {
                    i = k - 1;
                    newKeyStr += str;
                }
            }
            return newKeyStr;
        }
       public static string GetKeyAlphaNumeric(KeyLength l)
        {
            Guid g = Guid.NewGuid();
            string randStr = g.ToString("N");
            string tracStr = randStr.Substring(0, (int)l);
            tracStr = tracStr.ToUpper();
            char[] newKey = tracStr.ToCharArray();
            string newAlphaNumKey = String.Empty;
            switch(l)
            {
                case KeyLength.l16:
                    newAlphaNumKey = AppendStr(16, "-", newKey);
                    break;
                case KeyLength.l20:
                    newAlphaNumKey = AppendStr(20, "-", newKey);
                    break;
                case KeyLength.l24:
                    newAlphaNumKey = AppendStr(24, "-", newKey);
                    break;
                case KeyLength.l28:
                    newAlphaNumKey = AppendStr(28, "-", newKey);
                    break;
                case KeyLength.l32:
                    newAlphaNumKey = AppendStr(32, "-", newKey);
                    break;
            }
            return newAlphaNumKey;
        }
        public static string GetKeyNumeric(KeyNumLength l)
        {
            Random r = new Random();
            double k = Math.Round(r.NextDouble() * Math.Pow(10, (int)l) + 4);
            return k.ToString().Substring(0, (int)l);
        }
    }
}
