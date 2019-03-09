using System;
using System.IO;
using System.Text;
using System.Threading;
using netCore_Raycasting.Modules.Geometry;
using netCore_Raycasting.Modules.DDA_Algorithm;

namespace netCore_Raycasting.Modules.RayCasting
{
    public class RaycastingProperties
    {
        private Line convert;

        private Vector2 min;
        private Vector2 max;

        private float horizont;

        public Line Convert { get { return convert; } }
        public float Horizont { get { return horizont; } }

        public RaycastingProperties (float minRealDis, float maxRealDis, float minScreenH, float maxScreenH, float horizont)
        {
            this.min = new Vector2 (minRealDis, maxScreenH);
            this.max = new Vector2 (maxRealDis, minScreenH);
            this.horizont = horizont;

            Vector2Tuple tuple = Vector2.Reorganize(this.min, this.max);

            this.convert = new Line(tuple.A, tuple.B);
        }

        public float getHeight (float realDistance)
        {

            if (realDistance >= this.min.X )
            {
                if(realDistance <= this.max.X)
                {
                    return this.convert.getValue(realDistance);
                }
                return this.max.X;
            }
            return this.min.X;
        }

        public int getColor (float realDistance, int minColor, int maxColor)
        {
            Line colorGradient = new Line(new Vector2(this.min.X, maxColor), new Vector2(this.max.X, minColor));
            if (realDistance >= this.min.X )
            {
                if(realDistance <= this.max.X)
                {
                    return (int)colorGradient.getValue(realDistance);
                }
                return maxColor;
            }
            return minColor;
        }
    }

    public static class Engine
    {
        public static void FileWrite (string adrr, string content)
        {
            string filePath = @""+adrr;

            // Delete the file if it exists.
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Create the file.
            using (FileStream fs = File.Create(filePath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(content);
                // Add some information to the file.
                fs.Write(info, 0, info.Length);
            }
        }

        public static void ExportViewPGM (Player player, Map map, Vector2 resolution, RaycastingProperties properties, string adrr)
        {
            FrameBuffer frame = new FrameBuffer((int)resolution.X, (int)resolution.Y, 0);

            float angleInc = player.FOV/resolution.Y;
            float angleAtt = player.Rotation - (player.FOV / 2);
            float angleFinal = player.Rotation + (player.FOV / 2);

            int index = 0;

            while (angleAtt <= angleFinal)
            {
                Vector2 intersect = map.RayCasting(player, angleAtt);

                if (intersect == null)
                    intersect = new Vector2(float.PositiveInfinity, float.PositiveInfinity);
                else
                {
                    map.ViewRay("map.pgm", player.Position, intersect, 0, 255, 125);
                    Thread.Sleep(TimeSpan.FromSeconds(0.5f));
                }

                float distance = (float)Vector2.Distance(intersect, player.Position);

                float height = properties.getHeight((float) distance);

                int minH = (int)((resolution.X / 2) - (height / 2));
                int maxH = (int)((resolution.X / 2) + (height / 2));


                LineGeneration.plotLine(new Vector2(index, minH), new Vector2(index, maxH), frame, properties.getColor(distance, 255, 125));

                Console.WriteLine(minH+", "+ maxH +", "+index);


                //Console.WriteLine(index);

                angleAtt += angleInc;
                index += 1;
            }

            frame.Export("test.pgm");
            //Thread.Sleep(TimeSpan.FromSeconds(0.5f));

            //frame.Export(adrr);
        }
    }
}