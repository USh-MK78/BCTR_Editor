using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCTRLibrary
{
    public class ReadByteLine
    {
        public List<byte> charByteList { get; set; }

        public ReadByteLine(List<byte> Input)
        {
            charByteList = Input;
        }

        public ReadByteLine(List<char> Input)
        {
            charByteList = Input.Select(x => (byte)x).ToArray().ToList();
        }

        public void ReadByte(BinaryReader br, byte Split = 0x00)
        {
            //var br = br.BaseStream;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                byte PickStr = br.ReadByte();
                charByteList.Add(PickStr);
                if (PickStr == Split)
                {
                    break;
                }
            }
        }

        public void ReadMultiByte(BinaryReader br, byte Split = 0x00)
        {
            //var br = br.BaseStream;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                byte[] PickStr = br.ReadBytes(2);
                charByteList.Add(PickStr[0]);
                charByteList.Add(PickStr[1]);
                if (PickStr[0] == Split)
                {
                    break;
                }
            }
        }

        public void ReadByte(BinaryReader br, char Split = '\0')
        {
            //var br = br.BaseStream;
            while (br.BaseStream.Position != br.BaseStream.Length)
            {
                byte PickStr = br.ReadByte();
                charByteList.Add(PickStr);
                if (PickStr == Split)
                {
                    break;
                }
            }
        }

        public void WriteByte(BinaryWriter bw)
        {
            bw.Write(ConvertToCharArray());
        }

        public char[] ConvertToCharArray()
        {
            return charByteList.Select(x => (char)x).ToArray();
        }

        public int GetLength()
        {
            return charByteList.ToArray().Length;
        }
    }
}
