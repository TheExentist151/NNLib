using NNLib.IO;

namespace NNLib.Common
{
    /// <summary>
    /// Header for data chunks
    /// </summary>
    public class NNDataHeader
    {
        /// <summary>
        /// Header
        /// </summary>
        public NNChunkHeader ChunkHeader;

        /// <summary>
        /// Offset to the main data relative to the current position
        /// </summary>
        public uint MainDataOffset { get; set; }

        /// <summary>
        /// Always 0
        /// </summary>
        public uint Version { get; set; }

        public NNDataHeader()
        {
            ChunkHeader = new NNChunkHeader();
        }

        public void Read(ExtendedBinaryReader reader)
        {
            ChunkHeader.Read(reader);
            MainDataOffset = reader.ReadUInt32();
            Version = reader.ReadUInt32();
        }
    }
}
