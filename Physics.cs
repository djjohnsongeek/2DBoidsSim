using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Boids
{
    class Physics
    {
        public static void ResolveCollisions(List<Boid> boids, GameTime gameTime)
        {
            foreach (var boid in boids)
            {
                foreach (var possible in boids)
                {
                    if (boid.Id == possible.Id)
                    {
                        continue;
                    }

                    float safeDistance = boid.Radius + possible.Radius;
                    float distance = Vector2.Distance(boid.Position, possible.Position);
                    if (distance < safeDistance)
                    {
                        float power = safeDistance + (distance / safeDistance);
                        float angle = GetAngle(boid.Position, possible.Position);
                        float overlap = safeDistance - distance;
                        var repelVelocity = GetRepelVelocity(angle, power) * (float)gameTime.ElapsedGameTime.TotalSeconds;
                        var brutalRepelVelocity = GetBrutalRepelVelocity(angle, power, overlap);

                        if  (boid.Static && possible.Static || !boid.Static && !possible.Static)
                        {
                            boid.Position += repelVelocity;
                            possible.Position -= repelVelocity;
                        }
                        else if (boid.Static)
                        {
                            possible.Position -= brutalRepelVelocity;
                        }
                        else if (possible.Static)
                        {
                            boid.Position += brutalRepelVelocity;
                        }

                        //boid.Position += repelVelocity / 2;
                        //possible.Position -= repelVelocity / 2;
                    }
                }
            }
        }

        private static Vector2 GetBrutalRepelVelocity(float angle, float power, float overlap)
        {
            var repelV = new Vector2(
                GetRepelX(angle, power),
                GetRepelY(angle, power)
            );

            repelV.Normalize();

            return repelV * overlap;
        }

        public static Vector2 GetRepelVelocity(float angle, float power)
        {
            var repelV = new Vector2(
                GetRepelX(angle, power),
                GetRepelY(angle, power));

            return repelV * Config.CollisionRepelMultiplier;
        }

        private static float GetRepelX(double angle, float power)
        {
            return (float)Math.Cos(angle) * power;
        }

        private static float GetRepelY(double angle, float power)
        {
            return (float)Math.Sin(angle) * power;
        }

        public static float GetAngle(Vector2 position1, Vector2 position2)
        {
            var angle = (float)Math.Atan2(position1.Y - position2.Y, position1.X - position2.X);
            return angle;
        }
    }
}
