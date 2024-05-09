using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCTRLibrary
{
    public class EndianConvert
    {
        public enum Endian
        {
            BigEndian = 65534,
            LittleEndian = 65279
        }

        public byte[] BOM { get; set; }
        public Endian Endians => EndianCheck();

        public EndianConvert(byte[] InputBOM)
        {
            BOM = InputBOM;
        }

        //public EndianConvert(Endian endian)
        //{
        //    short eds = (short)endian;
        //    byte[] bytes = BitConverter.GetBytes(eds);
        //    BOM = bytes;
        //}

        public static byte[] GetEnumEndianToBytes(Endian endian)
        {
            short eds = (short)endian;
            return BitConverter.GetBytes(eds);
        }

        public Endian EndianCheck()
        {
            bool LE = BOM.SequenceEqual(new byte[] { 0xFF, 0xFE });
            bool BE = BOM.SequenceEqual(new byte[] { 0xFE, 0xFF });

            Endian BOMSetting = Endian.BigEndian;

            if ((LE || BE) == true)
            {
                if (LE == true) BOMSetting = Endian.LittleEndian;
                if (BE == true) BOMSetting = Endian.BigEndian;
            }

            return BOMSetting;
        }

        public byte[] Convert(byte[] Input)
        {
            if (Endians == Endian.BigEndian)
            {
                return Input.Reverse().ToArray();
            }
            if (Endians == Endian.LittleEndian)
            {
                return Input;
            }

            return Input;
        }
    }
}
