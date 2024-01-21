using NNLib.Common;

namespace NNLib.Chunks
{
    public class NNFileNameChunk
    {
        public NNChunkHeader Header; // "NFN0"
        public List<string> Names;

        public NNFileNameChunk()
        {
            Header = new NNChunkHeader();
            Names = new List<string>();
        }
    }
}
