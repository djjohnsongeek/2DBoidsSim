using Apos.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Boids
{
    public class BoidGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private BoidManager BoidManager;
        private UI UI;
        private ShapeBatch _shapeBatch;

        public BoidGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Config.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Config.ScreenHeight;
            _graphics.GraphicsProfile = GraphicsProfile.HiDef;
            _graphics.SynchronizeWithVerticalRetrace = false;
            IsMouseVisible = true;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _shapeBatch = new ShapeBatch(GraphicsDevice, Content);
            BoidManager = new BoidManager();
            BoidManager.LoadBoids();
            UI = new UI(this, BoidManager);
        }

        protected override void Update(GameTime gameTime)
        {
            Input.Update(gameTime);

            if (Input.KeyWasPressed(Keys.Escape))
            {
                Exit();
            }

            if (Input.KeyWasPressed(Keys.Tab))
            {
                UI.ToggleVisibility();
            }

            if (Input.RightMouseBtnWasPressed() && !UI.Visible && !Input.LeftControlClick())
            {
                BoidManager.SetBoidsWaypoint(true);
            }
            else if (Input.LeftMouseBtnWasPressed() && !UI.Visible)
            {
                BoidManager.SetBoidsWaypoint(false);
            }

            // Adding static boids.
            if (Input.LeftControlClick())
            {
                BoidManager.AddStaticBoid();
            }

            BoidManager.UpdateBoids(gameTime);
            if (Config.CollisionsEnabled)
            {
                Physics.ResolveCollisions(BoidManager.AllBoids, gameTime);
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _shapeBatch.Begin();
            BoidManager.Draw(_shapeBatch);
            _shapeBatch.DrawRectangle(Config.BoundsOrigin, Config.BoundsSize, Color.Transparent, Color.White);
            _shapeBatch.End();
            UI.Draw();

            base.Draw(gameTime);
        }
    }
}
