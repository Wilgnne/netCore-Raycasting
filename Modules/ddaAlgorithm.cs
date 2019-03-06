using System;
using Colorful;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using Console = Colorful.Console;


namespace netCore_Raycasting
{
    public class FrameBuffer
    {
        private int[,] frame;
        public int[,] Frame { get => frame; }

        private int baseColor;

        int i, j;

        /// <summary>baseColor 0-255.</summary>
        public FrameBuffer (int rows, int cols, int baseColor)
        {
            this.i = rows;
            this.j = cols;
            this.baseColor = baseColor;
            this.frame = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    this.frame[i, j] = baseColor;
                }
            }
        }

        public void Clear ()
        {
            for (int i = 0; i < this.i; i++)
            {
                for (int j = 0; j < this.j; j++)
                {
                    this.frame[i, j] = baseColor;
                }
            }
        }

        public bool plot (int i, int j, int grayScale)
        {
            if (i >= this.i || j >= this.j || i < 0 || j < 0)
                return false;
            this.frame[i, j] = grayScale;
            return true;
        }

        public void Export ()
        {
            string filePath = @"Exported.pgm";
            // Delete the file if it exists.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            string text = "P2\n"+this.i+" "+this.j+"\n255\n";

            for (int i = 0; i < this.i; i++)
            {
                string line = "";
                for (int j = 0; j < this.j; j++)
                {
                    line += (this.frame[i, j].ToString() + " ");
                }
                text += (line + "\n");
            }

            // Create the file.
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(text);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
        }
    }

    public static class ddaAlgorithm
    {
        public static char mod = '$';
        public static void plotPoint(Vector2 point, FrameBuffer frameBuffer, int grayScale)
        {
            int i = (int)point.Y;
            int j = (int)point.X;
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
            TupleVector2 vectors = Vector2.Reorganize(a, b);
            plotLineHigh(vectors.A, vectors.B, frameBuffer, grayScale);
        }

        public static void ClockTest ()
        {

            IDictionary <char, Color> colors = new Dictionary<char, Color>();
            colors.Add('*', Color.FromArgb(10, 10, 10));
            colors.Add('$', Color.FromArgb(255, 10, 10));

            int w = 34;
            int h = 34;

            FrameBuffer frameBuffer = new FrameBuffer(w, h, 0);
            Vector2 a = new Vector2(15, 15);

            double angle0 = 0;
            double angle1 = 30;
            float raio0 = 10;
            float raio1 = 12;

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

                frameBuffer.Export();

                Thread.Sleep(TimeSpan.FromSeconds(1));
            }
        }
    }
}
