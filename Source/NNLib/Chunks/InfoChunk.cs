using NNLib.Common;

namespace NNLib.Chunks
{
    public class NNInfoChunk
    {
        /// <summary>
        /// "N*IF" header
        /// </summary>
        public NNChunkHeader Header; // "N*IF"

        /// <summary>
        /// The count of chunks
        /// </summary>
        public uint ChunkCount { get; set; }

        /// <summary>
        /// Offset to the first chunk in the file
        /// </summary>
        public uint FirstChunkOffset { get; set; }

        /// <summary>
        /// Combined size of all chunks including padding
        /// </summary>
        public uint ChunksSize { get; set; }

        /// <summary>
        /// "NOF0" chunk offset
        /// </summary>
        public uint OffsetChunkOffset { get; set; }

        /// <summary>
        /// The size of "NOF0" chunk
        /// </summary>
        public uint OffsetChunkSize { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public uint Version { get; set; }   // https://hedgedocs.com/docs/nn/common/chunk-format/#info-chunk - "1 in most files, 0 in some newer variants."

        public NNInfoChunk()
        {
            Header = new NNChunkHeader();
        }

        public void Read(BinaryReader reader)
        {
            Header.Read(reader);
            ChunkCount = reader.ReadUInt32();
            FirstChunkOffset = reader.ReadUInt32();
            ChunksSize = reader.ReadUInt32();
            OffsetChunkOffset = reader.ReadUInt32();
            OffsetChunkSize = reader.ReadUInt32();
            Version = reader.ReadUInt32();
        }
    }
}
