using System.Text;

namespace NNLib.Chunks.Textures
{
    public class NNTexfile
    {
        /// <summary>
        /// Texfile type, as specified in <see cref="NNTexfileType">
        /// </summary>
        public NNTexfileType Type { get; set; }

        /// <summary>
        /// The texture's file name
        /// </summary>
        public string? FileName { get; set; }

        /// <summary>
        /// The minification filter used on the texture
        /// </summary>
        public NNTexfileMinFilter MinFilter { get; set; }

        /// <summary>
        /// The magnification filter used on the texture
        /// </summary>
        public NNTexfileMagFilter MagFilter { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public uint GlobalIndex { get; set; }

        /// <summary>
        /// Unknown
        /// </summary>
        public uint Bank { get; set; }
    }

    /// <summary>
    /// The minification filter types for <see cref="NNTexfile">
    /// </summary>
    public enum NNTexfileMinFilter
    {
        Nearest = 0,
        Linear = 1,

        Nearest_Mipmap_Nearest = 2,
        Nearest_Mipmap_Linear = 3,

        Linear_Mipmap_Nearest = 4,
        Linear_Mipmap_Linear = 5,

        Anisotropic = 6,
        Anisotropic2 = 6,

        Anisotropic_Mipmap_Nearest = 7,
        Anisotropic2_Mipmap_Nearest = 7,

        Anisotropic_Mipmap_Linear = 8,
        Anisotropic2_Mipmap_Linear = 8,

        Anisotropic4 = 9,
        Anisotropic4_Mipmap_Nearest = 10,
        Anisotropic4_Mipmap_Linear = 11,

        Anisotropic8 = 12,
        Anisotropic8_Mipmap_Nearest = 13,
        Anisotropic8_Mipmap_Linear = 14
    }

    /// <summary>
    /// The magnification filter for <see cref="NNTexfile">
    /// </summary>
    public enum NNTexfileMagFilter
    {
        Nearest = 0,
        Linear = 1,
        Anisotropic = 2
    }

    /// <summary>
    /// Types for Texfiles
    /// </summary>
    public enum NNTexfileType
    {
        /// <summary>
        /// TODO
        /// </summary>
        Mask = 255,

        /// <summary>
        /// Default format
        /// </summary>
        Default = 0,

        /// <summary>
        /// If set, then texture format is .gim (used on PSP)
        /// </summary>
        GIMTexture = 1,

        /// <summary>
        /// If set, then the file name field is ignored
        /// </summary>
        NoFilename = 256,

        /// <summary>
        /// If set, them the Min and Mag filters should be ignored
        /// </summary>
        NoFilter = 512,

        /// <summary>
        /// If set, then the GlobalIndex field is used
        /// </summary>
        LightGlobalIndex = 1024,

        /// <summary>
        /// If set, then the Bank field is used
        /// </summary>
        ListBank = 2048
    }
}
