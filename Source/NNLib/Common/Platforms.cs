namespace NNLib.Common
{
    // TODO: make this as an interface or smth

    /// <summary>
    /// Supported platforms in NN library
    /// </summary>
    public enum NNPlatform
    { 
        /// <summary>
        /// Xbox 360 / Original, little endian, 16 alignment
        /// </summary>
        NX,

        /// <summary>
        /// PlayStation 2, little endian, 16 (?) alignment
        /// </summary>
        NS,

        /// <summary>
        /// Gamecube / Wii, big endian, 32 alignment
        /// </summary>
        NG,

        /// <summary>
        /// PlayStation 3, little (?) endian, 16 (?) alignment
        /// </summary>
        NC,

        /// <summary>
        /// PSP, little endian, 16 (?) alignment
        /// </summary>
        NU,

        /// <summary>
        /// Nintendo DS / 3DS, little endian, 16 (?) alignment
        /// </summary>
        DS,

        /// <summary>
        /// iOS / Android / Windows Phone, little endian, 16 (?) alignment
        /// </summary>
        NI,

        /// <summary>
        /// Xbox 360 (wtf), little (?) endian, 16 (?) alignment
        /// </summary>
        NE,

        /// <summary>
        /// Xbox 360 / PlayStation 3 (again? wtf?), little (?) endian, 16 (?) alignment
        /// </summary>
        NY,

        /// <summary>
        /// Other platforms, little endian, 16 alignment
        /// </summary>
        NZ
    }
}
