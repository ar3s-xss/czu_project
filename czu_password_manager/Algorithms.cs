using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace czu_password_manager
{
    internal class Algorithms
    {
        internal int[] masterFile = { 0x7a, 0x6e, 0x66, 0x67, 0x72, 0x65, 0x55, 0x6e, 0x66, 0x75, 0x2e, 0x67, 0x6b, 0x67 };
        internal int[] vaultFile = { 0x69, 0x6e, 0x68, 0x79, 0x67, 0x2e, 0x6b, 0x7a, 0x79 };
        internal string Rot1_3(int[] data)
        {
            string realfileName = String.Empty;
            foreach(int hex in data)
            {
                if (hex >= 0x41 && hex <= 0x5A)
                {
                    realfileName += (char)((hex - 0x41 + 0xD) % 0x1A + 0x41);
                } else if (hex >= 0x61 && hex <= 0x7A)
                {
                    realfileName += (char)((hex - 0x61 + 0xD) % 0x1A + 0x61);
                }
                else
                {
                    realfileName += (char)hex;
                }
            }
            return realfileName;
        }

    }
}
