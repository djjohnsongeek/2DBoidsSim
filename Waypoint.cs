using Microsoft.Xna.Framework;

namespace Boids
{
    class Waypoint
    {
        public Vector2 Position;
        public float Radius;
        public bool Enabled;

        public Waypoint(Vector2 position, float radius, bool enabled = false)
        {
            Position = position;
            Radius = radius;
            Enabled = enabled;
        }
    }
}
