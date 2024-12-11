using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Final_Project_2024___2025
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D hallwayTexture, titleBackgroundTexture, runLeftTexture, runRightTexture, idleTexture;
        Rectangle window;
        Screen screen;
        MouseState mouseState;
        SpriteFont introFont;

        enum Screen
        {
            Title,
            LevelSelect,
            Level1
        }
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            window = new Rectangle(0, 0, 1000, 600);
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            // TODO: use this.Content to load your game content here
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 600;
            _graphics.ApplyChanges();

            idleTexture = Content.Load<Texture2D>("Colton Prisoner");
            titleBackgroundTexture = Content.Load<Texture2D>("titleBackground");
            hallwayTexture = Content.Load<Texture2D>("prison background");
            introFont = Content.Load<SpriteFont>("introFont");
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            if (screen == Screen.Title)
            {
                _spriteBatch.Draw(titleBackgroundTexture, window, Color.White);
                _spriteBatch.Draw(idleTexture, new Rectangle(30, 100, 266, 476), Color.White);
                _spriteBatch.DrawString(introFont, ("Colton's"), new Vector2(500, 100), Color.OrangeRed);
                _spriteBatch.DrawString(introFont, ("Jailhouse"), new Vector2(500, 200), Color.OrangeRed);

            }

            if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(hallwayTexture, window, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
