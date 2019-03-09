using System;

namespace netCore_Raycasting.Modules.Geometry
{
    public class Line
    {
        private float m = 0.0f;
        private float c = 0.0f;
        private bool Xorientation = true;

        public float M { get { return m; } }
        public float C { get { return c; } }
        public bool XOrientation { get { return Xorientation; } }

        public Line (Vector2 a, Vector2 b)
        {
            Vector2 distance = Vector2.VectorDistance(a, b);
            if (distance.X == 0)
            {
                if(distance.Y == 0)
                {
                    this.m = float.NaN;
                    this.c = float.NaN;
                }
                else
                {
                    this.Xorientation = false;
                    this.m = distance.X/distance.Y;
                    this.c = a.X - (this.m * a.Y);
                }
            }
            else
            {
                this.m = distance.Y/distance.X;
                this.c = a.Y - (this.m * a.X);
            }
        }

        public Line (float m, float c, bool Xorientation)
        {
            this.m = m;
            this.c = c;
            this.Xorientation = Xorientation;
        }

        public float getValue (float i)
        {
            return (this.m * i) + this.c;
        }

        public override string ToString ()
        {
            if(this.Xorientation == true)
                return "y = " + this.m + " * x + " + this.c;
            return "x = " + this.m + " * y + " + this.c;
        }

        public static Vector2 Intersection (Line a, Line b)
        {
            float dm = a.M - b.M;

            if (a.XOrientation == true && b.XOrientation == false)
            {
                float y = (a.m * b.c) + a.c;
                return new Vector2(b.c, y);
            }
            else if (a.XOrientation == false && b.XOrientation == true)
            {
                float y = (b.m * a.c) + b.c;
                return new Vector2(a.c, y);
            }
            else if (a.M != float.NaN && b.M != float.NaN && dm != 0 && dm != float.NaN)
            {
                float x = (b.c - a.c) / dm;
                float y = (a.m * x) + a.c;

                return new Vector2(x, y);
            }

            return new Vector2(float.PositiveInfinity, float.PositiveInfinity);
        }
    }
}