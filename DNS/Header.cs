using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Diagnostics.SymbolStore;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DNS
{
    public enum Opcode
    {
        Query = 0,
        IQuery = 1,
        Status = 2,
        Notify = 4,
        Update = 5,
        DSO = 6,
    }

    public class Header
    {
        private const int ID_OFFSET = 0;
        private const int FLAG_FRONT_OFFSET = 2;
        private const int FLAG_REAR_OFFSET = 3;
        private const int QDCOUNT_OFFSET = 4;
        private const int ANCOUNT_OFFSET = 6;
        private const int NSCOUNT_OFFSET = 8;
        private const int ARCOUNT_OFFSET = 10;

        private byte[] _headerSection = new byte[12];
        public byte[] HeaderSection
        {
            get { return _headerSection; }
        }

        public Header()
        {
            //Default
            SetRDFlag(true);
            SetQDCOUNT(1);
        }

        private void CopyUShort(ushort value, int offset)
        {
            var bytes = ReverseEndian.Reverse(BitConverter.GetBytes(value), Endian.Big);

            for(int i = 0; i < bytes.Length; i++)
                _headerSection[i + offset] = bytes[i];
        }

        public void SetId(ushort value)
        {
            CopyUShort(value, ID_OFFSET);
        }

        public void SetQRFlag(bool value)
        {
            if (value)
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] | 0x80);
            else
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] & 0x7F);
        }

        public void SetOpcodeFlag(Opcode opcode)
        {
            _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] | ((int)opcode << 3));
        }

        public void SetAAFlag(bool aa)
        {
            if (aa)
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] | 0x04);
            else
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] & 0xFB);
        }

        public void SetTCFlag(bool tc)
        {
            if (tc)
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] | 0x02);
            else
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] & 0xFD);
        }

        public void SetRDFlag(bool value)
        {
            if (value)
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] | 0x01);
            else
                _headerSection[FLAG_FRONT_OFFSET] = (byte)(_headerSection[FLAG_FRONT_OFFSET] & 0xFE);
        }

        public void SetRAFlag(bool ra)
        {
            if (ra)
                _headerSection[FLAG_REAR_OFFSET] = (byte)(_headerSection[FLAG_REAR_OFFSET] | 0x80);
            else
                _headerSection[FLAG_REAR_OFFSET] = (byte)(_headerSection[FLAG_REAR_OFFSET] & 0x7F);
        }

        public void SetQDCOUNT(ushort qdcount)
        {
            CopyUShort(qdcount, QDCOUNT_OFFSET);
        }

        public void SetANCOUNT(ushort ancount)
        {
            CopyUShort(ancount, ANCOUNT_OFFSET);
        }

        public void SetNSCOUNT(ushort nscount)
        {
            CopyUShort(nscount, NSCOUNT_OFFSET);
        }

        public void SetARCOUNT(ushort arcount)
        {
            CopyUShort(arcount, ARCOUNT_OFFSET);
        }
    }
}
