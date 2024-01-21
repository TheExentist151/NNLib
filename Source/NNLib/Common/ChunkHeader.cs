using NNLib.Chunks;

namespace NNLib.Common
{
    /// <summary>
    /// Chunk header
    /// </summary>
    public class NNChunkHeader
    {
        /// <summary>
        /// Chunk's platform ID
        /// </summary>
        public NNPlatform Platform { get; set; }

        /// <summary>
        /// Chunk's  type ID
        /// </summary>
        public NNChunkType ChunkType { get; set; }

        /// <summary>
        /// Chunk's  size
        /// </summary>
        public uint ChunkSize { get; set; }

        public void Read(BinaryReader reader)
        {
            ushort platformMagic = reader.ReadUInt16();
            Platform = (NNPlatform)Enum.ToObject(typeof(NNPlatform), platformMagic);

            ushort chunkTypeMagic = reader.ReadUInt16();
            ChunkType = (NNChunkType)Enum.ToObject(typeof(NNChunkType), chunkTypeMagic);

            ChunkSize = reader.ReadUInt32();
        }
    }
}
