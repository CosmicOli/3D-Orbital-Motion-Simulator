using System.Numerics;

namespace _3D_Orbital_Motion_Simulation
{
    // This is defined independantly to any classes or structs as it is used in a multitude of locations.
    internal record OrbitInformation(double LongitudeOfAscendingNode, double ArgumentOfPeriapsis, double LongitudeOfPeriapsis, double Inclination, double Eccentricity, double SemiMajorAxis, double SemiMinorAxis, double Periapsis, double Apoapsis, double AngularMomentum, Vector3 SpecificAngularMomentum, Vector3 EccentricityVector, double TotalEnergy, double HillSphereRadius, decimal OrbitalPeriod);
}
