using System;
using System.IO;
using System.Text;
using netCore_Raycasting.Modules.RayCasting;

namespace netCore_Raycasting.Modules.DDA_Algorithm
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

        public void Export (string addr)
        {
            string filePath = @""+addr;

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

            Engine.FileWrite(filePath, text);
        }
    }
}