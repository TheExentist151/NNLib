# NNLib
WIP library for SEGA NN formats, written on C#. 

## Usage
```cs
// Loading a file
NNFile file = new NNFile();
file.Read("path/to/nn/file.xno");
```

# NNViewer
CLI tool that allows to view information about NN chunks in a file. 

# Special thanks
Special thanks goes to: 
 * Radfordhound - for writing an [article about NN Chunk Format](https://hedgedocs.com/docs/nn/common/chunk-format/) and [Texlist](https://hedgedocs.com/docs/nn/common/chunk-format/texlist/) chunk
 * TGE - for making the initial [Sonic 4 Episode I decompilation](https://github.com/tge-was-taken/Sonic4Ep1-WindowsPhone-Decompilation)
 * WamWooWam - for making an [improved decompilation](https://github.com/WamWooWam/Sonic4Ep1-WP7-Decompilation/)
 * SEGA - for making the original port of Sonic 4 Episode I on Windows Phone. 
