using netCore_Raycasting.Modules.Geometry;

namespace netCore_Raycasting.Modules.RayCasting
{
    public class Player
    {
        private Vector2 position;
        private float rotation;
        private float fov;

        public Vector2 Position { get { return position; } }
        public float Rotation { get { return rotation; } }
        public float FOV { get { return fov; } }
        public Player (Vector2 position, float rotation, float fov)
        {
            this.position = position;
            this.rotation = rotation;
            this.fov = fov;
        }

        public Player ()
        {
            this.position = new Vector2();
            this.rotation = 0.0f;
            this.fov = 30f;
        }
    }
}