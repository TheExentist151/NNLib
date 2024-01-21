using NNLib.Common;

namespace NNLib.Chunks
{
    public class NNOffsetListChunk
    {
        public NNChunkHeader Header;
        public uint OffsetCount { get; set; }
        public List<uint> Offsets;
        public NNChunkType Type { get; set; }

        public NNOffsetListChunk()
        {
            Header = new NNChunkHeader();
            Offsets = new List<uint>();
        }

        public void Read(BinaryReader reader)
        {
            Header.Read(reader);
            OffsetCount = reader.ReadUInt32();
            reader.ReadUInt32(); // Padding

            for (int i = 0; i < OffsetCount; i++)
            {
                uint offset = reader.ReadUInt32();
                Offsets.Add(offset);
            }
        }
    }
}
