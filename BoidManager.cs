using Apos.Shapes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Boids
{
    class BoidManager
    {
        // Responds to waypoints, needs to be updated
        public List<Boid> ActiveBoids;

        // Cannot be pushed
        public List<Boid> StaticBoids;

        // Used for physics, and drawing
        public List<Boid> AllBoids;


        public Waypoint Waypoint;
        public float BoidsRadiusAverage;

        public BoidManager()
        {
            AllBoids = new List<Boid>();
            ActiveBoids = new List<Boid>();
            StaticBoids = new List<Boid>();
            Waypoint = new Waypoint(Vector2.Zero, 5f);
        }

        public void SetBoidsWaypoint(bool active)
        {
            if (active)
            {
                Waypoint.Position = Input.MousePosition;
                Waypoint.Enabled = true;
                Waypoint.Radius = BoidsRadiusAverage;
            }
            else
            {
                Waypoint.Position = Vector2.Zero;
                Waypoint.Enabled = false;
            }

            foreach (var boid in ActiveBoids)
            {
                if (active)
                {
                    boid.Waypoint = Waypoint;
                    boid.Idle = false;
                }
                else
                {
                    boid.Waypoint = null;
                }
            }
        }

        public void UpdateBoids(GameTime gameTime)
        {
            foreach (Boid b in ActiveBoids)
            {
                if (!b.Idle)
                {
                    var cohesionV = GetCohesionVelocity(b);
                    var seperationV = GetSeperationVelocity(b);
                    var groupV = GetGroupVelocity(b);
                    var boundsV = GetBoundsVelocity(b);
                    var destinationV = GetDestinationVelocity(b);
                    var avoidV = GetAvoidVelocity(b);

                    b.Velocity = cohesionV + seperationV + groupV + b.Velocity + boundsV + destinationV + avoidV;

                    if (BoidInArrivalCircle(b))
                    {
                        b.Velocity = b.Velocity * Config.ArrivalDrag;
                        if (b.Velocity.Length() < Config.ArrivalSpeedLimit)
                        {
                            b.Idle = true;
                            b.Waypoint = null;

                            Waypoint.Radius += (float)Math.Cbrt(BoidsRadiusAverage);
                        }
                    }
                    b.Velocity = ClampVelocity(b.Velocity);
                    b.Position += b.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }
        }

        public void LoadBoids()
        {
            BoidsRadiusAverage = 0;

            for (int i = 0; i < Config.BoidCount; i++)
            {
                var activeBoid = new Boid(
                    id: i,
                    position: new Vector2(Calc.RandomFloat() * Config.ScreenWidth, Calc.RandomFloat() * Config.ScreenHeight),
                    velocity: new Vector2(Calc.RandomInt(-100, 100), Calc.RandomInt(-100, 100))
                 );

                activeBoid.Idle = false;

                ActiveBoids.Add(activeBoid);
                AllBoids.Add(activeBoid);

                BoidsRadiusAverage += ActiveBoids[i].Radius;
            }
            BoidsRadiusAverage /= ActiveBoids.Count;
        }

        public void AddStaticBoid()
        {
            var newBoid = new Boid(AllBoids.Count, Input.MousePosition, Vector2.Zero);
            newBoid.Idle = true;
            newBoid.Static = true;

            StaticBoids.Add(newBoid);
            AllBoids.Add(newBoid);
        }

        public void Reset()
        {
            ActiveBoids.Clear();
            AllBoids.Clear();
            StaticBoids.Clear();
            LoadBoids();
        }

        public void Draw(ShapeBatch shapeBatch)
        {
            if (Waypoint.Enabled)
            {
                shapeBatch.DrawCircle(Waypoint.Position, Waypoint.Radius, Color.Transparent, Color.White);
            }
            foreach (Boid b in AllBoids)
            {
                shapeBatch.DrawCircle(b.Position, b.Radius, b.Color, b.BorderColor, 1);
            }
        }

        private Vector2 GetCohesionVelocity(Boid boid)
        {
            Vector2 center = Vector2.Zero;
            int count = 0;

            foreach (Boid b in ActiveBoids)
            {
                if (Vector2.DistanceSquared(boid.Position, b.Position) < Config.BoidVisionSquared)
                {
                    center += b.Position;
                    count++;
                }

            }

            center /= count;
            return (center - boid.Position) * Config.CohesionFactor;
        }

        private Vector2 GetSeperationVelocity(Boid boid)
        {
            Vector2 seperationVelocity = Vector2.Zero;
            // Do we need count?

            foreach (Boid b in ActiveBoids)
            {
                if (b.Id != boid.Id)
                {
                    var distance = Vector2.Distance(boid.Position, b.Position);
                    var avoidDistance = b.Radius + boid.Radius + Config.SeperationMinDistance;

                    if (distance < avoidDistance)
                    {
                        seperationVelocity += (boid.Position - b.Position);
                    }
                }
            }

            return seperationVelocity * Config.SeperationFactor;
        }

        private Vector2 GetGroupVelocity(Boid boid)
        {
            Vector2 averageVelocity = Vector2.Zero;
            int count = 0;
            foreach (Boid b in ActiveBoids)
            {
                if (Vector2.DistanceSquared(boid.Position, b.Position) < Config.BoidVisionSquared)
                {
                    averageVelocity += b.Velocity;
                    count++;
                }

            }

            averageVelocity /= count;
            return (averageVelocity - boid.Velocity) * Config.GroupAlignmentFactor;
        }

        private Vector2 GetBoundsVelocity(Boid boid)
        {
            Vector2 turnVelocity = Vector2.Zero;

            if (boid.Position.X < Config.BoundsMargin)
            {
                turnVelocity.X += Config.BoundRepelFactor;
            }
            if (boid.Position.X > Config.ScreenWidth - Config.BoundsMargin)
            {
                turnVelocity.X -= Config.BoundRepelFactor;
            }
            if (boid.Position.Y < Config.BoundsMargin)
            {
                turnVelocity.Y += Config.BoundRepelFactor;
            }
            if (boid.Position.Y > Config.ScreenHeight - Config.BoundsMargin)
            {
                turnVelocity.Y -= Config.BoundRepelFactor;
            }
            return turnVelocity;
        }

        private Vector2 GetDestinationVelocity(Boid boid)
        {
            var destVelocity = Vector2.Zero;
            if (Waypoint.Enabled)
            {
                destVelocity = Waypoint.Position - boid.Position;
                destVelocity.Normalize();
                destVelocity *= Config.MaxSpeed;
            }
            return destVelocity * Config.DestinationFactor;
        }

        private Vector2 GetAvoidVelocity(Boid boid)
        {
            Vector2 avoidVelocity = Vector2.Zero;

            foreach (Boid b in StaticBoids)
            {
                var distance = Vector2.Distance(boid.Position, b.Position);
                var avoidDistance = b.Radius + boid.Radius + Config.AvoidanceMinDistance;

                if (distance < avoidDistance)
                {
                    avoidVelocity += (boid.Position - b.Position);
                }
            }
            // make the avoid velocity taper off depeding on the distance
            return avoidVelocity * Config.AvoidanceFactor;
        }

        private bool BoidInArrivalCircle(Boid b)
        {
            return Vector2.DistanceSquared(b.Position, Waypoint.Position) < Waypoint.Radius * Waypoint.Radius;
        }

        private Vector2 ClampVelocity(Vector2 velocity)
        {
            if (velocity.Length() > Config.MaxSpeed)
            {
                velocity.Normalize();
                velocity *= Config.MaxSpeed;
            }
            return velocity;
        }
    }
}
