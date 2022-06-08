namespace LabOfKiwi.Science
{
    public static class Constants
    {
        /// <summary>
        /// Gravitational constant.
        /// </summary>
        public static readonly GravitationalConst.Measurement GravitationalConstant = new(6.6743015e-11);

        /// <summary>
        /// Speed of light.
        /// </summary>
        public static readonly Speed.Measurement SpeedOfLight = new(299792458);

        /// <summary>
        /// Speed of sound in dry air at 20°C.
        /// </summary>
        public static readonly Speed.Measurement SpeedOfSound = new(343);
    }
}
