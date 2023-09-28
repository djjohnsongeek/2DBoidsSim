using Microsoft.Xna.Framework;

namespace Boids
{
    static class Config
    {
        internal static float BoundsMargin = 60; //200
        internal static float BoundRepelFactor = 50; //1
        internal static float CohesionFactor = .25f; //.005
        internal static float SeperationMinDistance = 3.5f; //20
        internal static float SeperationFactor = .8f; //.05
        internal static float GroupAlignmentFactor = .5f; //.05
        internal static float DestinationFactor = 0.339f; // 1
        internal static float ArrivalDrag = 0;
        internal static float ArrivalSpeedLimit = 8f;
        internal static int BoidCount = 50; //50
        internal static float MaxSpeed = 250; //200
        internal static int ScreenWidth = 1200; //1200
        internal static int ScreenHeight = 800; //800
        internal static float BoidVision = 100; // 75
        internal static bool CollisionsEnabled = true;
        internal static float CollisionRepelMultiplier = 1.5f;
        internal static bool WaypointMovementOnly = false;
        internal static float BoidVisionSquared = BoidVision * BoidVision;
        internal static float AvoidanceFactor = 1;
        internal static float AvoidanceMinDistance = 0;
        internal static Vector2 BoundsOrigin => new Vector2(BoundsMargin, BoundsMargin);
        internal static Vector2 BoundsSize => new Vector2(ScreenWidth - BoundsMargin * 2, ScreenHeight - BoundsMargin * 2);
    }
}
