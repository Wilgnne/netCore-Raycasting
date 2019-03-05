using System;

namespace netCore_Raycasting
{
    public class Vector2
    {
        private int x;
        private int y;

        public int X { get => x; }
        public int Y { get => Y; }

        public Vector2 (int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 ()
        {
            //Initialized 0,0
            this.x = 0;
            this.y = 0;
        }

        public static double Distance (Vector2 a, Vector2 b)
        {
            float dx = b.X - a.X;
            float dy = b.Y - a.Y;

            return Math.Sqrt((dx * dx) + (dy * dy));
        }

        public override string ToString ()
        {
            return "Vector2 (" + this.x.ToString() + ", " + this.y.ToString() + ")";
        }
    }
}