using System.Numerics;

namespace _3D_Orbital_Motion_Simulation
{
    internal struct TransformationMatrix3x3
    {
        private double[,] Contents { get; set; }

        public TransformationMatrix3x3(double[,] Contents)
        {
            if (Contents.GetLength(0) != 3 || Contents.GetLength(1) != 3)
            {
                throw new Exception("Incorrect dimensions inputted when creating the matrix.");
            }

            this.Contents = Contents;
        }

        public Vector3 Transform(Vector3 Point)
        {
            Vector3 transformedPoint;

            transformedPoint.X = Convert.ToSingle(Point.X * Contents[0, 0] + Point.Y * Contents[1, 0] + Point.Z * Contents[2, 0]);
            transformedPoint.Y = Convert.ToSingle(Point.X * Contents[0, 1] + Point.Y * Contents[1, 1] + Point.Z * Contents[2, 1]);
            transformedPoint.Z = Convert.ToSingle(Point.X * Contents[0, 2] + Point.Y * Contents[1, 2] + Point.Z * Contents[2, 2]);

            return transformedPoint;
        }
    }
}
