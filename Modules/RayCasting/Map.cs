using System;
using System.Collections.Generic;
using netCore_Raycasting.Modules.Geometry;
using netCore_Raycasting.Modules.DDA_Algorithm;

namespace netCore_Raycasting.Modules.RayCasting
{
    public class Map
    {
        private Vector2[] vertex;
        private intTuple[] edge;

        private Vector2 size;
        public Map (Vector2[] vertex, intTuple[] edge)
        {
            this.vertex = vertex;
            this.edge = edge;
            this.size = this.Size(vertex);
        }

        public Vector2 RayCasting (Player player, float angle)
        {
            double radians = NumericExtensions.ToRadians((double)angle);
            float cos = (float) Math.Cos(radians) + player.Position.X;
            float sin = (float) Math.Sin(radians) + player.Position.Y;

            Vector2 ray = new Vector2(cos, sin);
            Vector2 signalRay = Vector2.VectorDistance(player.Position, ray);

            Line playerLine = new Line(player.Position, ray);

            List<Vector2> intersections = new List<Vector2>();

            Vector2 finalPoint = null;

            foreach (intTuple item in this.edge)
            {
                Vector2Tuple points = Vector2.Reorganize(this.vertex [item.A],
                    this.vertex [item.B]);

                Line lineMap = new Line(points.A, points.B);

                Vector2 intersect = Line.Intersection(playerLine, lineMap);

                Vector2 signalIntersect = Vector2.VectorDistance(player.Position, intersect);

                bool signalX = float.IsNegative(signalRay.X) == float.IsNegative(signalIntersect.X);
                bool signalY = float.IsNegative(signalRay.Y) == float.IsNegative(signalIntersect.Y);

                bool XinLimit = intersect.X >= points.A.X && intersect.X <= points.B.X;
                bool YinLimit = intersect.Y >= points.A.Y && intersect.Y <= points.B.Y;

                if (signalX && signalY && XinLimit && YinLimit)
                {
                    intersections.Add(intersect);

                    if(finalPoint == null)
                        finalPoint = intersect;
                    else if (Vector2.Distance(intersect, player.Position) < Vector2.Distance(finalPoint, player.Position))
                        finalPoint = intersect;
                }
            }

            Console.WriteLine(finalPoint);
            return finalPoint;
        }

        /// <summary>
        /// adrr contains the save address of the generated image,
        /// and must have the extension .pgm;
        /// Example /home/user/image.pgm
        /// </summary>
        public void ExportMapPGM (string adrr, int baseColor, int lineColor)
        {
            FrameBuffer frame = new FrameBuffer((int)this.size.X + 1,
                (int)this.size.Y + 1, baseColor);

            foreach (intTuple item in this.edge)
            {
                Vector2 a = this.vertex[item.A];
                Vector2 b = this.vertex[item.B];

                LineGeneration.plotLine(a, b, frame, lineColor);
            }
            frame.Export(adrr);
        }

        public void ViewRay (string adrr, Vector2 pos, Vector2 intersect, int baseColor, int lineColor, int PlayerColor)
        {
            FrameBuffer frame = new FrameBuffer((int)this.size.X + 1,
                (int)this.size.Y + 1, baseColor);

            foreach (intTuple item in this.edge)
            {
                Vector2 a = this.vertex[item.A];
                Vector2 b = this.vertex[item.B];

                LineGeneration.plotLine(a, b, frame, lineColor);
            }

            LineGeneration.plotLine(pos, intersect, frame, 255);
            LineGeneration.plotPoint(pos, frame, PlayerColor);

            frame.Export(adrr);
        }

        Vector2 Size (Vector2[] vertex)
        {
            float x = NumericExtensions.Max(
                NumericExtensions.VectorsToArray(vertex, Vector2.Type.x));
            float y = NumericExtensions.Max(
                NumericExtensions.VectorsToArray(vertex, Vector2.Type.y));

            return new Vector2(x, y);
        }
    }
}