using NNLib.Common;
using System.Numerics;

namespace NNLib.IO
{
    public class ExtendedBinaryWriter : BinaryWriter
    {
        public Endian Endianness { get; set; }

        public ExtendedBinaryWriter(Stream input) : base(input)
        {
            Endianness = Endian.Little;
        }

        public ExtendedBinaryWriter(Stream input, Endian endianness) : base(input)
        {
            Endianness = endianness;
        }

        public override void Write(short src)
        {
            byte[] data = new byte[2];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 8);
                data[1] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
            }
            base.Write(data);
        }

        public override void Write(ushort src)
        {
            byte[] data = new byte[2];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 8);
                data[1] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
            }
            base.Write(data);
        }

        public override void Write(int src)
        {
            byte[] data = new byte[4];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 24);
                data[1] = (byte)(src >> 16);
                data[2] = (byte)(src >> 8);
                data[3] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
                data[2] = (byte)(src >> 16);
                data[3] = (byte)(src >> 24);
            }
            base.Write(data);
        }

        public override void Write(uint src)
        {
            byte[] data = new byte[4];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 24);
                data[1] = (byte)(src >> 16);
                data[2] = (byte)(src >> 8);
                data[3] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
                data[2] = (byte)(src >> 16);
                data[3] = (byte)(src >> 24);
            }
            base.Write(data);
        }

        // https://github.com/Radfordhound/HedgeLib/blob/directX/HedgeLib/IO/ExtendedBinary.cs#L765
        public unsafe override void Write(float src)
        {
            Write(*((uint*)&src));
        }

        public override void Write(long src)
        {
            byte[] data = new byte[8];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 56);
                data[1] = (byte)(src >> 48);
                data[2] = (byte)(src >> 40);
                data[3] = (byte)(src >> 32);
                data[4] = (byte)(src >> 24);
                data[5] = (byte)(src >> 16);
                data[6] = (byte)(src >> 8);
                data[7] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
                data[2] = (byte)(src >> 16);
                data[3] = (byte)(src >> 24);
                data[4] = (byte)(src >> 32);
                data[5] = (byte)(src >> 40);
                data[6] = (byte)(src >> 48);
                data[7] = (byte)(src >> 56);
            }
            base.Write(data);
        }

        public override void Write(ulong src)
        {
            byte[] data = new byte[8];
            if (Endianness == Endian.Big)
            {
                data[0] = (byte)(src >> 56);
                data[1] = (byte)(src >> 48);
                data[2] = (byte)(src >> 40);
                data[3] = (byte)(src >> 32);
                data[4] = (byte)(src >> 24);
                data[5] = (byte)(src >> 16);
                data[6] = (byte)(src >> 8);
                data[7] = (byte)src;
            }
            else
            {
                data[0] = (byte)src;
                data[1] = (byte)(src >> 8);
                data[2] = (byte)(src >> 16);
                data[3] = (byte)(src >> 24);
                data[4] = (byte)(src >> 32);
                data[5] = (byte)(src >> 40);
                data[6] = (byte)(src >> 48);
                data[7] = (byte)(src >> 56);
            }
            base.Write(data);
        }

        public void Write(Vector3 src)
        {
            Write(src.X);
            Write(src.Y);
            Write(src.Z);
        }

        public void Write(Vector2 src)
        {
            Write(src.X);
            Write(src.Y);
        }

        public void Write(Vector4 src)
        {
            Write(src.X);
            Write(src.Y);
            Write(src.Z);
            Write(src.W);
        }

        public void WriteNullTerminatedString(string src)
        {
            foreach (char character in src)
                Write(character);
            Write((byte)0x00);
        }

        public void WriteNulls(uint count)
        {
            for (int i = 0; i < count; i++)
                Write((byte)0x00);
        }

        // TODO: writing normal strings
    }
}
