using System;
using netCore_Raycasting.Modules.Geometry;
using netCore_Raycasting.Modules.RayCasting;

namespace netCore_Raycasting
{
    class Program
    {
        static void Main(string[] args)
        {
            Vector2[] vertex = new Vector2[]
            {
                new Vector2(0, 0),
                new Vector2(0, 10),
                new Vector2(10, 10),
                new Vector2(10, 0)
            };

            Modules.intTuple[] edge = new Modules.intTuple[]
            {
                new Modules.intTuple(0, 1),
                new Modules.intTuple(1, 2),
                new Modules.intTuple(2, 3),
                new Modules.intTuple(3, 0)

            };

            Map mapa = new Map (vertex, edge);
            Player player = new Player (new Vector2 (5, 3), 41, 30);

            Vector2 intersect = mapa.RayCasting(player, 0);

            double distance = Vector2.Distance(intersect, player.Position);
            Console.WriteLine(distance);
        }
    }
}