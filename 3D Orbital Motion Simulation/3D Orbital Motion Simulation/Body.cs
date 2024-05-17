namespace _3D_Orbital_Motion_Simulation
{
    internal interface Body
    {
        public abstract string Name { get; init; }
        public abstract double Mass { get; init; }
        public abstract double Radius { get; init; }
        public abstract Color Colour { get; init; }
        public abstract string ToString();
    }
}
