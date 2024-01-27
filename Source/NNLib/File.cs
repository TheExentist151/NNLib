using NNLib.Chunks;
using NNLib.Chunks.Textures;
using NNLib.Common;
using NNLib.IO;

namespace NNLib
{
    public class NNFile
    {
        /// <summary>
        /// Main "N*IF" chunk
        /// </summary>
        public NNInfoChunk InfoChunk;

        /// <summary>
        /// The NNTexlist chunk
        /// </summary>
        public NNTexlist Texlist;

        /// <summary>
        /// The OffsetList Chunk
        /// </summary>
        public NNOffsetListChunk OffsetList;

        public NNFile()
        {
            InfoChunk = new NNInfoChunk();
            OffsetList = new NNOffsetListChunk();
        }

        /// <summary>
        /// Reads a binary SEGA NN file
        /// </summary>
        /// <param name="filename">Path to the SEGA NN file</param>
        /// <exception cref="FileNotFoundException"></exception>
        public void Read(string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException($"Can't find file {filename}");

            using (FileStream fs = File.OpenRead(filename))
            {
                using (ExtendedBinaryReader reader = new ExtendedBinaryReader(fs))
                {
                    InfoChunk.Read(reader);

                    for (int i = 0; i < InfoChunk.ChunkCount; i++)
                    {
                        NNDataHeader header = new NNDataHeader();
                        header.Read(reader);

                        switch (header.ChunkHeader.ChunkType)
                        {
                            case NNChunkType.TexList:
                                reader.BaseStream.Seek(-16, SeekOrigin.Current);
                                Texlist = new NNTexlist();
                                Texlist.Read(reader, InfoChunk.FirstChunkOffset);
                                break;

                            default: reader.ReadBytes((int)header.ChunkHeader.ChunkSize); break; // Skipping unsupported chunk
                        }

                        // Skipping padding until the 'N' letter is found
                        while (reader.ReadByte() != 0x4E) // 'N'
                        {
                            reader.ReadByte();
                        }
                        reader.BaseStream.Seek(-1, SeekOrigin.Current);
                    }

                    // Offset chunk
                    reader.BaseStream.Seek(InfoChunk.OffsetChunkOffset, SeekOrigin.Begin);
                    OffsetList.Read(reader);
                }
            }
        }
    }
}
