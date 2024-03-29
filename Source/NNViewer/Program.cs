﻿using NNLib;
using NNLib.Chunks;
using NNLib.Chunks.Textures;

namespace NNViewer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("NNViewer <file path>");
                return;
            }

            NNFile file = new NNFile();
            file.Read(args[0]);

            Console.WriteLine("Info chunk information:");
            Console.WriteLine($"Platform: \t\t{file.InfoChunk.Header.Platform}");
            Console.WriteLine($"Next chunk offset: \t{file.InfoChunk.Header.NextChunkOffset}");
            Console.WriteLine($"Chunk count: \t\t{file.InfoChunk.ChunkCount}");
            Console.WriteLine($"First chunk offset: \t{file.InfoChunk.FirstChunkOffset}");
            Console.WriteLine($"Chunks size: \t\t{file.InfoChunk.ChunksSize}");
            Console.WriteLine($"Offset chunk offset: \t{file.InfoChunk.OffsetChunkOffset}");
            Console.WriteLine($"Offset chunk size: \t{file.InfoChunk.OffsetChunkSize}");
            Console.WriteLine($"Version: \t\t{file.InfoChunk.Version}");
            Console.WriteLine();

            if (file.Texlist != null)
            {
                Console.WriteLine("Texlist chunk information: \n");
                Console.WriteLine($"Texfiles count: \t{file.Texlist.TexfileCount}");
                Console.WriteLine($"Textures: ");
                int index = 1;
                foreach (NNTexfile texture in file.Texlist.Textures)
                {
                    Console.WriteLine($"\nTexture {index}:");
                    Console.WriteLine($"Name: \t\t\t{texture.FileName}");
                    Console.WriteLine($"Minification filter: \t{texture.MinFilter}");
                    Console.WriteLine($"Magnification filter: \t{texture.MagFilter}");
                    Console.WriteLine($"Global index: \t\t{texture.GlobalIndex}");
                    Console.WriteLine($"Bank: \t\t\t{texture.Bank}");
                    index++;
                }
                Console.WriteLine();
            }

            if (file.NodeNameList != null)
            {
                Console.WriteLine("Node name list chunk information: \n");
                Console.WriteLine($"Node names count: \t{file.NodeNameList.NodeNames.Count}");
                Console.WriteLine($"Node sorting type: \t{file.NodeNameList.SortType}");
                foreach (NNNodeName name in file.NodeNameList.NodeNames)
                {
                    Console.WriteLine($"Node {name.NodeIndex}: \t\t{name.Name}");
                }
            }

            Console.ReadKey();
        }
    }
}
