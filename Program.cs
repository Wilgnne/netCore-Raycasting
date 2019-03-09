using System;
using System.Threading;
using netCore_Raycasting.Modules.Geometry;
using netCore_Raycasting.Modules.RayCasting;
using netCore_Raycasting.Modules.DDA_Algorithm;

namespace netCore_Raycasting
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector2[] vertex = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 40),
                new Vector2(40, 60),
                new Vector2(60, 0)
            };

            Modules.intTuple[] edge = new Modules.intTuple[]
            {
                new Modules.intTuple(0, 1),
                new Modules.intTuple(1, 2),
                new Modules.intTuple(2, 3),
                new Modules.intTuple(3, 0),

            };

            RaycastingProperties properties = new RaycastingProperties(0, 50, 10, 250, 0);

            Map mapa = new Map (vertex, edge);
            Player player = new Player (new Vector2 (5, 3), 0, 15);

            while (true)
            {
                player = new Player (new Vector2 (5, 3), player.Rotation + 1, 30);
                Engine.ExportViewPGM (player, mapa, new Vector2(300, 600), properties, "export.pgm");
            }

            //FrameBuffer frame = new FrameBuffer(100, 100, 0);

            /*
            while (true)
            {
                frame.Clear();
                for (int i = 0; i < 100; i++)
                {
                    Random r = new Random();
                    int height = r.Next(0, 50);
                    int minH = (int)((100/2) - (height/2) + properties.Horizont);
                    int maxH = (int)((100/2) + (height/2) + properties.Horizont);

                    LineGeneration.plotLine(new Vector2(i, minH), new Vector2(i, maxH), frame, properties.getColor(height, 255,50));
                    frame.Export("test.pgm");
                    Thread.Sleep(TimeSpan.FromSeconds(0.5f));
                }
            }

            */
        }
    }
}