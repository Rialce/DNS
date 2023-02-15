using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    enum Endian
    {
        Little, Big
    }

    class ReverseEndian
    {
        public static byte[] Reverse(byte[] bytes, Endian endian)
        {
            if(BitConverter.IsLittleEndian ^ endian == Endian.Little)
                return bytes.Reverse().ToArray();
            else
                return bytes;
        }
    }
}
