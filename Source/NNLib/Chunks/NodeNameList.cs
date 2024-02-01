using NNLib.Common;
using NNLib.IO;

namespace NNLib.Chunks
{
    /// <summary>
    /// Node name list with "N*NN" header
    /// </summary>
    public class NNNodeNameList
    {
        /// <summary>
        /// NN's data header
        /// </summary>
        public NNDataHeader Header { get; set; }

        /// <summary>
        /// The sorting type
        /// </summary>
        public NNNodeNameSortType SortType { get; set; }

        /// <summary>
        /// The node names
        /// </summary>
        public List<NNNodeName> NodeNames { get; set; }

        private NNChunkType m_ChunkType = NNChunkType.NodeNameList;

        public NNNodeNameList()
        {
            NodeNames = new List<NNNodeName>();
        }

        public NNNodeNameList(NNDataHeader header)
        {
            if (header.ChunkHeader.ChunkType == m_ChunkType)
            {
                Header = header;
                NodeNames = new List<NNNodeName>();
            }
            else throw new ArgumentException($"The provided chunk header has and invalid chunk type. Expected: {m_ChunkType}, got: {header.ChunkHeader.ChunkType}");
        }

        /// <summary>
        /// Reads a NN chunk with node name list (NN) type
        /// </summary>
        /// <param name="reader">ExtendedBinaryReader instance</param>
        /// <param name="firstChunkOffset">Position of the first chunk in a file</param>
        public void Read(ExtendedBinaryReader reader, uint firstChunkOffset)
        {
            if (Header == null)
            {
                Header = new NNDataHeader();
                Header.Read(reader);
            }

            long pos = reader.BaseStream.Position;
            reader.BaseStream.Seek(Header.MainDataOffset + firstChunkOffset, 0);

            uint sortType = reader.ReadUInt32();
            SortType = (NNNodeNameSortType)Enum.ToObject(typeof(NNNodeNameSortType), sortType);
            uint nodeNameCount = reader.ReadUInt32();
            reader.BaseStream.Seek(pos, 0);

            for (int i = 0; i < nodeNameCount; i++)
            {
                NNNodeName name = new NNNodeName();
                name.NodeIndex = reader.ReadUInt32();

                uint nameOffset = reader.ReadUInt32();
                pos = reader.BaseStream.Position;
                reader.BaseStream.Seek(nameOffset + firstChunkOffset, 0);
                name.Name = reader.ReadNullTerminatedString();
                reader.BaseStream.Seek(pos, 0);
                NodeNames.Add(name);
            }
        }

        /// <summary>
        /// Writes a NN chunk with node name list (NN) type
        /// </summary>
        /// <param name="writer">ExtendedBinaryWriter instance</param>
        /// <param name="firstChunkOffset">Position of the first chunk in a file</param>
        public void Write(ExtendedBinaryWriter writer, uint firstChunkOffset)
        {
            long startingPosition = writer.BaseStream.Position;
            uint nextChunkOffset = ((uint)startingPosition) + 16;
            List<uint> nameOffsets = new List<uint>();

            writer.WriteNulls(16); // Placeholder for the header

            uint nodesOffset = (uint)writer.BaseStream.Position;
            foreach (NNNodeName name in NodeNames)
            {
                writer.Write(name.NodeIndex);
                nameOffsets.Add((uint)writer.BaseStream.Position);
                writer.WriteNulls(4);
                nextChunkOffset += 8;
            }

            uint mainDataOffset = (uint)writer.BaseStream.Position;
            writer.Write((uint)SortType);
            writer.Write(NodeNames.Count);
            writer.Write(nodesOffset - 32);
            nextChunkOffset += 12;

            for (int i = 0; i < nameOffsets.Count; i++)
            {
                // Writing the name
                long nameOffset = writer.BaseStream.Position;
                writer.WriteNullTerminatedString(NodeNames[i].Name);
                long pos = writer.BaseStream.Position;

                // Writing the offset to the name
                writer.BaseStream.Seek(nameOffsets[i], 0);
                writer.Write(nameOffset - firstChunkOffset);
                writer.BaseStream.Seek(pos, 0);

                nextChunkOffset += (uint)NodeNames[i].Name.Length + 1; // Name length + null terminator
            }

            // TODO: padding
            long endingPosition = writer.BaseStream.Position;
            writer.BaseStream.Seek(startingPosition, 0);
            Header.MainDataOffset = mainDataOffset - 32;
            Header.ChunkHeader.NextChunkOffset = nextChunkOffset;
            Header.Write(writer);
            writer.BaseStream.Position = endingPosition;

            // TODO: add offsets to the offset table
        }
    }

    /// <summary>
    /// Sorting types for NN's node name lists
    /// </summary>
    public enum NNNodeNameSortType
    {
        Index,
        Name
    }
}

