using System;
using netCore_Raycasting.Modules.Geometry;

namespace netCore_Raycasting.Modules
{
    public static class NumericExtensions
    {

        /// <summary>Convert to Radians.</summary>
        /// <returns>The value in radians</returns>
        public static double ToRadians(double val)
        {
            return (Math.PI / 180) * val;
        }

        public static float Max (float[] vals)
        {
            float att = vals[0];

            for (int i = 0; i < vals.Length; i++)
            {
                if (vals[i] > att)
                {
                    att = vals[i];
                }
            }

            return att;
        }

        public static float[] VectorsToArray (Vector2[] elem, Vector2.Type type)
        {
            float[] array = new float[elem.Length];

            if (type == Vector2.Type.x)
            {
                for (int i = 0; i < elem.Length; i++)
                {
                    array[i] = elem[i].X;
                }
            }
            else
            {
                for (int i = 0; i < elem.Length; i++)
                {
                    array[i] = elem[i].Y;
                }
            }

            return array;
        }
    }

    public struct intTuple
    {
        private int a;
        private int b;

        public int A { get { return a;} }
        public int B { get { return b;} }

        public intTuple (int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
}