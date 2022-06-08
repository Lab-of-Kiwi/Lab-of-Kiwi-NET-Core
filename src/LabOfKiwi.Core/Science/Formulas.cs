using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabOfKiwi.Science
{
    public static class Formulas
    {
        public static Acceleration.Measurement AccelerationDueToGravity(Mass.Measurement mass, Length.Measurement distance)
        {
            return (Acceleration.Measurement)(Constants.GravitationalConstant * mass / distance.Square());
        }

        /// <summary>
        /// Calculates the total energy of matter if converted 100% to energy. Einstein's famous E=mc² formula.
        /// </summary>
        /// 
        /// <param name="mass">The mass of the matter.</param>
        /// <returns>The energy if the matter is converted 100% to energy.</returns>
        public static Energy.Measurement EnergyMassEquivalence(Mass.Measurement mass)
        {
            return mass * Constants.SpeedOfLight * Constants.SpeedOfLight;
        }

        /// <summary>
        /// Calculates the total mass of energy if converted 100% to matter. Einstein's famous E=mc² formula.
        /// </summary>
        /// 
        /// <param name="energy">The energy.</param>
        /// <returns>The matter if the energy is converted 100% to matter.</returns>
        public static Mass.Measurement EnergyMassEquivalence(Energy.Measurement energy)
        {
            return (energy / Constants.SpeedOfLight) / Constants.SpeedOfLight;
        }

        /// <summary>
        /// Calculates the attractive force between two point-like bodies.
        /// </summary>
        /// 
        /// <param name="mass1">The mass of body 1.</param>
        /// <param name="mass2">The mass of body 2.</param>
        /// <param name="distance">The distance between both bodies.</param>
        /// <returns>The attractive force between the two bodies.</returns>
        public static Force.Measurement Gravitation(Mass.Measurement mass1, Mass.Measurement mass2, Length.Measurement distance)
        {
            return (Force.Measurement)(Constants.GravitationalConstant * mass1 * mass2 / distance.Square());
        }
    }
}
