namespace NNLib.Common
{
    // TODO: make this as an interface or smth
    // TODO: maybe the values should be in hex? 

    /// <summary>
    /// Supported platforms in NN library
    /// </summary>
    public enum NNPlatform
    { 
        /// <summary>
        /// Xbox 360 / Original, little endian, 16 alignment
        /// </summary>
        NX = 22606,

        /// <summary>
        /// PlayStation 2, little endian, 16 (?) alignment
        /// </summary>
        NS = 21326,

        /// <summary>
        /// Gamecube / Wii, big endian, 32 alignment
        /// </summary>
        NG = 18254,

        /// <summary>
        /// PlayStation 3, little (?) endian, 16 (?) alignment
        /// </summary>
        NC = 17230,

        /// <summary>
        /// PSP, little endian, 16 (?) alignment
        /// </summary>
        NU = 21838,

        /// <summary>
        /// Nintendo DS / 3DS, little endian, 16 (?) alignment
        /// </summary>
        DS = 21316,

        /// <summary>
        /// iOS / Android / Windows Phone, little endian, 16 (?) alignment
        /// </summary>
        NI = 18766,

        /// <summary>
        /// Xbox 360 (wtf), little (?) endian, 16 (?) alignment
        /// </summary>
        NE = 17742,

        /// <summary>
        /// Xbox 360 / PlayStation 3 (again? wtf?), little (?) endian, 16 (?) alignment
        /// </summary>
        NY = 22862,

        /// <summary>
        /// Other platforms, little endian, 16 alignment
        /// </summary>
        NZ = 23118
    }
}
