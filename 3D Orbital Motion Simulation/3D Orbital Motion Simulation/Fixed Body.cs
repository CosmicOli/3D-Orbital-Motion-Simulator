using System.Numerics;

namespace _3D_Orbital_Motion_Simulation
{
    internal class FixedBody : Body
    {
        public string Name { get; init; }

        public Vector3 Position { get; init; }

        public double Mass { get; init; }

        public double Radius { get; init; }

        public Color Colour { get; init; }

        public FixedBody(Color Colour, string Name, Vector3 Position, double Mass, double Radius)
        {
            this.Name = Name;
            this.Position = Position;
            this.Mass = Mass;
            this.Radius = Radius;
            this.Colour = Colour;
        }

        // Instead of using commas, a comma and pipe symbol together are used to stop ambiguity in places such as converting a vector into a string.
        public override string ToString()
        {
            string output = "";
            output += Name              + ",|";
            output += Mass.ToString()   + ",|";
            output += Radius.ToString() + ",|";
            output += Colour.ToArgb().ToString() + ",|";
            output += Position.ToString() + "\n";

            return output;
        }
    }
}
