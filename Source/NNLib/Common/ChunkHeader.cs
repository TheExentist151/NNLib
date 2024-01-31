using NNLib.Chunks;
using NNLib.IO;

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
        /// Offset to the next chunk
        /// </summary>
        public uint NextChunkOffset { get; set; }

        public void Read(ExtendedBinaryReader reader)
        {
            ushort platformMagic = reader.ReadUInt16();
            Platform = (NNPlatform)Enum.ToObject(typeof(NNPlatform), platformMagic);

            ushort chunkTypeMagic = reader.ReadUInt16();
            ChunkType = (NNChunkType)Enum.ToObject(typeof(NNChunkType), chunkTypeMagic);

            NextChunkOffset = reader.ReadUInt32();
        }

        public void Write(ExtendedBinaryWriter writer)
        {
            writer.Write((ushort)Platform);
            writer.Write((ushort)ChunkType);
            writer.Write(NextChunkOffset);
        }
    }
}
