using System;
using System.Threading;
using netCore_Raycasting.Modules.Geometry;

namespace netCore_Raycasting.Modules.DDA_Algorithm
{
    public static class LineGeneration
    {
        public static void plotPoint(Vector2 point, FrameBuffer frameBuffer, int grayScale)
        {
            int i = (int) Math.Floor(point.Y);
            int j = (int) Math.Floor(point.X);
            frameBuffer.plot(i, j, grayScale);
        }

        static void plotLineHigh (Vector2 a, Vector2 b, FrameBuffer frameBuffer, int grayScale)
        {
            Vector2 distance = Vector2.VectorDistance(a, b);
            int steps = (int)Math.Abs(distance.X);
            if (Math.Abs(distance.Y) > steps)
                steps = (int)Math.Abs(distance.Y);

            float Xinc = distance.X / steps;
            float Yinc = distance.Y / steps;

            float x = a.X;
            float y = a.Y;

            for (int i = 0; i <= steps; i++)
            {
                plotPoint(new Vector2(x, y), frameBuffer, grayScale);
                x += Xinc;
                y += Yinc;
            }
        }

        public static void plotLine (Vector2 a, Vector2 b, FrameBuffer frameBuffer, int grayScale)
        {
            Vector2Tuple vectors = Vector2.Reorganize(a, b);
            plotLineHigh(vectors.A, vectors.B, frameBuffer, grayScale);
        }

        public static void ClockTest ()
        {
            int w = 500;
            int h = 500;

            FrameBuffer frameBuffer = new FrameBuffer(w, h, 0);
            Vector2 a = new Vector2(250, 250);

            double angle0 = 0;
            double angle1 = 30;
            float raio0 = 125;
            float raio1 = 75;

            while (true)
            {
                frameBuffer.Clear();
                double cos = Math.Cos(NumericExtensions.ToRadians(angle0));
                double sin = Math.Sin(NumericExtensions.ToRadians(angle0));
                Vector2 b = new Vector2((float)(cos * raio0) + a.X, (float)(sin * raio0) + a.Y);
                plotLine(a, b, frameBuffer, 255);

                cos = Math.Cos(NumericExtensions.ToRadians(angle1));
                sin = Math.Sin(NumericExtensions.ToRadians(angle1));
                b = new Vector2((float)(cos * raio1) + a.X, (float)(sin * raio1) + a.Y);
                plotLine(a, b, frameBuffer, 125);

                angle0 += 1f;
                angle1 += 3f;

                frameBuffer.Export("clock.pgm");

                Thread.Sleep(TimeSpan.FromSeconds(0.5f));
            }
        }
    }
}