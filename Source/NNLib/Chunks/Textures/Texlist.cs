using NNLib.Common;

namespace NNLib.Chunks.Textures
{
    /// <summary>
    /// Texture list with "N*TL" header
    /// </summary>
    public class NNTexlist
    {
        /// <summary>
        /// NN's data header
        /// </summary>
        public NNDataHeader Header; // "N*TL"

        /// <summary>
        /// <see cref="NNTexfile> structs
        /// </summary>
        public List<NNTexfile> Textures;

        /// <summary>
        /// The count of texfiles
        /// </summary>
        public uint TexfileCount { get; set; }

        public NNTexlist()
        {
            Header = new NNDataHeader();
            Textures = new List<NNTexfile>();
        }

        /// <summary>
        /// Reads a NNTexlist chunk from a file
        /// </summary>
        /// <param name="reader"><see cref="BinaryReader"> instance</param>
        /// <param name="firstChunkOffset">Position of the first chunk in a file</param>
        public void Read(BinaryReader reader, uint firstChunkOffset)
        {
            uint chunkStartingPosition = (uint)reader.BaseStream.Position;
            uint texlistsStartingPosition = chunkStartingPosition + 16;
            Header.Read(reader);
            uint chunkEndPosition = chunkStartingPosition + Header.ChunkHeader.ChunkSize;

            // Texfile count
            reader.BaseStream.Seek(chunkStartingPosition + Header.MainDataOffset, 0);
            TexfileCount = reader.ReadUInt32();
            reader.BaseStream.Seek(texlistsStartingPosition, 0);

            for(int i = 0; i < TexfileCount; i++)
            {
                NNTexfile texfile = new NNTexfile();

                texfile.Read(reader, firstChunkOffset);
                Textures.Add(texfile);
            }

            // Skipping names and unknown bytes
            while (reader.BaseStream.Position != chunkEndPosition)
            {
                reader.ReadByte();
            }
        }
    }
}
