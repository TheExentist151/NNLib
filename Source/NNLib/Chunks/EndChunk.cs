using NNLib.Common;

namespace NNLib.Chunks
{
    public class NNEndChunk
    {
        public NNChunkHeader Header; // "NEND"

        public NNEndChunk()
        {
            Header = new NNChunkHeader();
        }
    }
}
