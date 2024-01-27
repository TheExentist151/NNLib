using NNLib.Common;
using System.Numerics;
using System.Text;

namespace NNLib.IO
{
    public class ExtendedBinaryReader : BinaryReader
    {
        public Endian Endianness { get; set; }
        public ExtendedBinaryReader(Stream input) : base(input)
        {
            Endianness = Endian.Little;
        }

        public ExtendedBinaryReader(Stream input, Endian endianness) : base(input)
        {
            Endianness = endianness;
        }

        public string ReadNullTerminatedString()
        {
            bool isEnded = false;
            StringBuilder sb = new StringBuilder();

            while (isEnded != true)
            {
                char character = ReadChar();
                if (character != 0x00)
                    sb.Append(character);
                else isEnded = true;
            }

            return sb.ToString();
        }

        // TODO: reading normal strings

        public override short ReadInt16()
        {
            byte[] data = ReadBytes(2);
            if (Endianness == Endian.Big)
                return (short)(data[0] << 8 | data[1]);
            else
                return (short)(data[1] << 8 | data[0]);
        }

        public override ushort ReadUInt16()
        {
            byte[] data = ReadBytes(2);
            if (Endianness == Endian.Big)
                return (ushort)(data[0] << 8 | data[1]);
            else
                return (ushort)(data[1] << 8 | data[0]);
        }

        public override int ReadInt32()
        {
            byte[] data = ReadBytes(4);
            if (Endianness == Endian.Big)
                return data[0] << 24 | data[1] << 16 | data[2] << 8 | data[3];
            else
                return data[3] << 24 | data[2] << 16 | data[1] << 8 | data[0];
        }

        public override uint ReadUInt32()
        {
            byte[] data = ReadBytes(4);
            if (Endianness == Endian.Big)
                return (uint)(data[0] << 24 | data[1] << 16 | data[2] << 8 | data[3]);
            else
                return (uint)(data[3] << 24 | data[2] << 16 | data[1] << 8 | data[0]);
        }

        // https://github.com/Radfordhound/HedgeLib/blob/directX/HedgeLib/IO/ExtendedBinary.cs#L299
        public unsafe override float ReadSingle()
        {
            uint data = ReadUInt32();
            return *((float*)&data);
        }

        public override long ReadInt64()
        {
            byte[] data = ReadBytes(8);

            if (Endianness == Endian.Big)
                return (long)data[0] << 56 | (long)data[1] << 48 | (long)data[2] << 40 | (long)data[3] << 32 |
                    (long)data[4] << 24 | (long)data[5] << 16 | (long)data[6] << 8 |    data[7];
            else
                return (long)data[7] << 56 | (long)data[6] << 48 | (long)data[5] << 40 | (long)data[4] << 32 |
                    (long)data[3] << 24 | (long)data[2] << 16 | (long)data[1] << 8 | (long)data[0];
        }

        public override ulong ReadUInt64()
        {
            byte[] data = ReadBytes(8);

            if (Endianness == Endian.Big)
                return (ulong)data[0] << 56 | (ulong)data[1] << 48 | (ulong)data[2] << 40 | (ulong)data[3] << 32 |
                    (ulong)data[4] << 24 | (ulong)data[5] << 16 | (ulong)data[6] << 8 | data[7];
            else
                return (ulong)data[7] << 56 | (ulong)data[6] << 48 | (ulong)data[5] << 40 | (ulong)data[4] << 32 |
                    (ulong)data[3] << 24 | (ulong)data[2] << 16 | (ulong)data[1] << 8 | (ulong)data[0];
        }

        // TODO: test this
        public Vector3 ReadVector3()
        {
            float X = ReadSingle();
            float Y = ReadSingle();
            float Z = ReadSingle();

            Vector3 result = new Vector3(X, Y, Z);
            return result;
        }
    }
}
