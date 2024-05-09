using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BCTRLibrary
{
    public class BCTR
    {
        public char[] BCTR_Header { get; set; }
        public int NameSetCount { get; set; }

        public List<NameSet> NameSetList { get; set; }
        public class NameSet
        {
            public char[] NameCharArray;
            public string Name => new string(NameCharArray);

            public short ID { get; set; }
            public short CharArrayOffsetPos { get; set; } //From Char[] Array

            public void GetNameCharArray(char[] CharArray)
            {
                List<char> chars = new List<char>();
                foreach (var i in CharArray.Skip(CharArrayOffsetPos).ToArray())
                {
                    if (i != '\0')
                    {
                        chars.Add(i);
                    }
                    else
                    {
                        break;
                    }
                }

                NameCharArray = chars.ToArray();
            }

            public void ReadNameSet(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                ID = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                CharArrayOffsetPos = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            }

            public NameSet(short ID, short CharPos)
            {
                this.ID = ID;
                this.CharArrayOffsetPos = CharPos;
            }

            public NameSet()
            {
                ID = 0;
                CharArrayOffsetPos = 0;
            }
        }

        public enum UnknownType
        {
            Type0 = 0,
            Type1 = 1
        }

        public int UnknownData0 { get; set; }
        public UnknownType Unknown_Type
        {
            get
            {
                return (UnknownType)Enum.ToObject(typeof(UnknownType), UnknownData0);
            }
        }

        public byte[] UnknownByteArray { get; set; } //0x4
        public short UnknownShortData0 { get; set; }

        public short UnknownDataSet3_Count { get; set; }

        public UnknownData2 UnknownData_2 { get; set; }
        public class UnknownData2
        {
            public int Value1 { get; set; }
            public int Value2 { get; set; }
            public int Value3 { get; set; }
            public int Value4 { get; set; }
            public int Value5 { get; set; }
            public int Value6 { get; set; }
            public int Value7 { get; set; }
            public int Value8 { get; set; }
            public int Value9 { get; set; }
            public int Value10 { get; set; }

            public void Read_UnknownData2(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                Value1 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value2 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value3 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value4 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value5 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value6 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value7 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value8 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value9 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                Value10 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            }

            public UnknownData2()
            {
                Value1 = 0;
                Value2 = 0;
                Value3 = 0;
                Value4 = 0;
                Value5 = 0;
                Value6 = 0;
                Value7 = 0;
                Value8 = 0;
                Value9 = 0;
                Value10 = 0;
            }
        }

        public List<UnknownData3> UnknownData3_List { get; set; }
        public class UnknownData3
        {
            public char[] NameCharArray;
            public string Name => new string(NameCharArray);

            public short CharArrayOffsetPos { get; set; }
            public short Index { get; set; }
            public float UnknownFloatData1 { get; set; }
            public float UnknownFloatData2 { get; set; }
            public byte[] UnknownByteArray1 { get; set; } //0x4
            public byte[] UnknownByteArray2 { get; set; } //0x4

            public void GetNameCharArray(char[] CharArray)
            {
                List<char> chars = new List<char>();
                foreach (var i in CharArray.Skip(CharArrayOffsetPos).ToArray())
                {
                    if (i != '\0')
                    {
                        chars.Add(i);
                    }
                    else
                    {
                        break;
                    }
                }

                NameCharArray = chars.ToArray();
            }

            public void Read_UnknownData3(BinaryReader br, byte[] BOM)
            {
                EndianConvert endianConvert = new EndianConvert(BOM);
                CharArrayOffsetPos = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                Index = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
                UnknownFloatData1 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                UnknownFloatData2 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
                UnknownByteArray1 = endianConvert.Convert(br.ReadBytes(4));
                UnknownByteArray2 = endianConvert.Convert(br.ReadBytes(4));
            }

            public UnknownData3()
            {
                CharArrayOffsetPos = 0;
                Index = 0;
                UnknownFloatData1 = 0;
                UnknownFloatData2 = 0;
                UnknownByteArray1 = new byte[4];
                UnknownByteArray2 = new byte[4];
            }
        }

        public byte[] UnknownData1 { get; set; } //0x4, UnknownData0 = 1 => true, 0 => false (No Write)

        public int CharArrayLength { get; set; }
        public char[] CharArray { get; set; }
        
        public void ReadBCTR(BinaryReader br, byte[] BOM)
        {
            EndianConvert endianConvert = new EndianConvert(BOM);

            BCTR_Header = br.ReadChars(4);
            NameSetCount = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (NameSetCount != 0)
            {
                for (int i = 0; i < NameSetCount; i++)
                {
                    NameSet nameSet = new NameSet();
                    nameSet.ReadNameSet(br, BOM);
                    NameSetList.Add(nameSet);
                }
            }

            UnknownData0 = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            UnknownByteArray = endianConvert.Convert(br.ReadBytes(4));
            UnknownShortData0 = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);
            UnknownDataSet3_Count = BitConverter.ToInt16(endianConvert.Convert(br.ReadBytes(2)), 0);

            UnknownData_2.Read_UnknownData2(br, BOM);

            for (int i = 0; i < UnknownDataSet3_Count; i++)
            {
                UnknownData3 unknownData3 = new UnknownData3();
                unknownData3.Read_UnknownData3(br, BOM);
                UnknownData3_List.Add(unknownData3);
            }

            if (UnknownData0 == 1)
            {
                UnknownData1 = endianConvert.Convert(br.ReadBytes(4));
            }
            else if (UnknownData0 == 0)
            {
                UnknownData1 = null;
            }
            else
            {
                throw new Exception("[Error] Count : " + UnknownData0);
            }

            CharArrayLength = BitConverter.ToInt32(endianConvert.Convert(br.ReadBytes(4)), 0);
            if (CharArrayLength > 0)
            {
                CharArray = br.ReadChars(CharArrayLength);

                foreach (var item in NameSetList)
                {
                    item.GetNameCharArray(CharArray);
                }

                foreach (var item in UnknownData3_List)
                {
                    item.GetNameCharArray(CharArray);
                }
            }
        }

        public BCTR()
        {
            BCTR_Header = "BCTR".ToCharArray();
            NameSetCount = 0;
            NameSetList = new List<NameSet>();
            UnknownData0 = 0;

            UnknownByteArray = new byte[4];
            UnknownShortData0 = 0;

            UnknownDataSet3_Count = 0;

            UnknownData_2 = new UnknownData2();

            UnknownData3_List = new List<UnknownData3>();

            UnknownData1 = new byte[4];

            CharArrayLength = 0;
            CharArray = new List<char>().ToArray();
        }
    }
}
