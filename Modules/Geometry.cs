using System;

namespace netCore_Raycasting
{

    /// <summary>The Tuple of thow Vector2</summary>
    public class TupleVector2
    {
        private Vector2 a;
        private Vector2 b;

        public Vector2 A { get => a; }
        public Vector2 B { get => b; }

        public TupleVector2 (Vector2 a, Vector2 b)
        {
            this.a = a;
            this.b = b;
        }
    }

    /// <summary>
    /// The Vector2 class.
    /// The Geometry Vector of thow dimentions representation.
    /// </summary>
    /// <remarks>This class can distance, vector distance and reorganize.</remarks>
    public class Vector2
    {
        private float x;
        private float y;
        public float X { get {return x; } }
        public float Y { get {return y; } }

        /// <summary>Initializes the vector with two values.</summary>
        public Vector2 (float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        /// <summary>Initializes the vector whith zeros.</summary>
        public Vector2 ()
        {
            this.x = 0;
            this.y = 0;
        }

        /// <returns>The distance of thow points.</returns>
        public static double Distance (Vector2 a, Vector2 b)
        {
            Vector2 distance = Vector2.VectorDistance(a, b);

            return Math.Sqrt((distance.X * distance.X) + (distance.Y * distance.Y));
        }

        /// <returns>The vector distance of thow points.</returns>
        public static Vector2 VectorDistance (Vector2 a, Vector2 b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;

            return new Vector2(dx, dy);
        }

        /// <returns>the vectors organized incrementally</returns>
        public static TupleVector2 Reorganize (Vector2 a, Vector2 b)
        {
            TupleVector2 va = new TupleVector2(a, b);
            TupleVector2 vb = new TupleVector2(b, a);

            if (Math.Abs(b.Y - a.Y) < Math.Abs(b.X - a.X))
            {
                if (a.X > b.X)
                    return vb;
                else
                    return va;
            }
            else if (a.Y > b.Y)
                return vb;

            return va;

        }

        /// <returns>The x and y components of the vector.</returns>
        public override string ToString ()
        {
            return "Vector2 (" + this.x.ToString() + ", " + this.y.ToString() + ")";
        }
    }

    public static class NumericExtensions
    {

        /// <summary>Convert to Radians.</summary>
        /// <returns>The value in radians</returns>
        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}