using NNLib.Common;
using NNLib.IO;

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

        private NNChunkType m_ChunkType = NNChunkType.TexList;

        public NNTexlist()
        {
            Textures = new List<NNTexfile>();
        }

        public NNTexlist(NNDataHeader header)
        {
            if (header.ChunkHeader.ChunkType == m_ChunkType)
            {
                Header = header;
                Textures = new List<NNTexfile>();
            }
            else throw new ArgumentException($"The provided chunk header has and invalid chunk type. Expected: {m_ChunkType}, got: {header.ChunkHeader.ChunkType}");
        }

        /// <summary>
        /// Reads a NNTexlist chunk from a file
        /// </summary>
        /// <param name="reader"><see cref="ExtendedBinaryReader"> instance</param>
        /// <param name="firstChunkOffset">Position of the first chunk in a file</param>
        public void Read(ExtendedBinaryReader reader, uint firstChunkOffset)
        {
            uint chunkStartingPosition = (uint)reader.BaseStream.Position;
            if (Header == null)
            {
                Header = new NNDataHeader();
                Header.Read(reader);
            }
            else chunkStartingPosition -= 16;

            uint texlistsStartingPosition = chunkStartingPosition + 16;
            uint chunkEndPosition = chunkStartingPosition + Header.ChunkHeader.NextChunkOffset;

            // Texfile count
            reader.BaseStream.Seek(chunkStartingPosition + Header.MainDataOffset, 0);
            TexfileCount = reader.ReadUInt32();
            reader.BaseStream.Seek(texlistsStartingPosition, 0);

            for(int i = 0; i < TexfileCount; i++)
            {
                NNTexfile texfile = new NNTexfile();

                uint type = reader.ReadUInt32();
                uint namePos = reader.ReadUInt32();
                texfile.Type = (NNTexfileType)Enum.ToObject(typeof(NNTexfileType), type);

                ushort minFilter = reader.ReadUInt16();
                texfile.MinFilter = (NNTexfileMinFilter)Enum.ToObject(typeof(NNTexfileMinFilter), minFilter);

                ushort magFilter = reader.ReadUInt16();
                texfile.MagFilter = (NNTexfileMagFilter)Enum.ToObject(typeof(NNTexfileMagFilter), magFilter);

                texfile.GlobalIndex = reader.ReadUInt32();
                texfile.Bank = reader.ReadUInt32();

                // Reading name
                // TODO: write a new method for reading null-terminated strings
                // or use another binary reading/writing library
                long position = reader.BaseStream.Position;
                reader.BaseStream.Seek(firstChunkOffset + namePos, 0);

                texfile.FileName = reader.ReadNullTerminatedString();

                reader.BaseStream.Seek(position, 0);

                Textures.Add(texfile);
            }

            // Skipping names and unknown bytes
            while (reader.BaseStream.Position != chunkEndPosition)
            {
                reader.ReadByte();
            }
        }

        // TODO: texfiles can have embedded texture data in them. 
        // We should support them!
        public void Write(ExtendedBinaryWriter writer, uint firstChunkOffset)
        {
            List<long> nameOffsets = new List<long>();
            foreach (NNTexfile texture in Textures)
            {
                writer.Write((uint)texture.Type);

                nameOffsets.Add(writer.BaseStream.Position);
                writer.Write(0); // name offset, placeholder

                writer.Write((ushort)texture.MinFilter);
                writer.Write((ushort)texture.MagFilter);

                writer.Write(texture.GlobalIndex);
                writer.Write(texture.Bank);
            }

            // File count (or name count)
            writer.Write(Textures.Count);

            // Unknown value
            writer.Write(0x10);

            // Writing names
            for(int i = 0; i < Textures.Count; i++)
            {
                // Offsets
                long pos = writer.BaseStream.Position;
                writer.BaseStream.Position = nameOffsets[i];
                writer.Write((uint)pos - firstChunkOffset);
                writer.BaseStream.Position = pos;

                // Name
                writer.WriteNullTerminatedString(Textures[i].FileName);
            }
        }
    }
}
