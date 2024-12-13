using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Project_2024___2025
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D hallwayTexture, titleBackgroundTexture, runLeftTexture1, runLeftTexture2, runLeftTexture3, runRightTexture1, runRightTexture2, runRightTexture3, idleTexture, buttonTexture, playerTexture;
        Rectangle window, playButtonRect, levelButtonRect, playerRect;
        Vector2 playerSpeed;
        Screen screen;
        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState;
        SpriteFont introFont, buttonFont;

        int runRight = 0;
        int runLeft = 0;

        List<Texture2D> runRightTextures;
        List<Texture2D> runLeftTextures;
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

            window = new Rectangle(0, 0, 1000, 600);
            playButtonRect = new Rectangle(568, 330, 180, 75);
            levelButtonRect = new Rectangle(568, 430, 180, 75);
            playerRect = new Rectangle(20, 350, 250, 250);
            playerSpeed = new Vector2();
            runRightTextures = new List<Texture2D>();
            runLeftTextures = new List<Texture2D>();


            base.Initialize();
            

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
            buttonFont = Content.Load<SpriteFont>("ButtonFont");
            buttonTexture = Content.Load<Texture2D>("rectangle");
            runRightTextures.Add(Content.Load<Texture2D>("coltonRunRight"));
            runRightTextures.Add(Content.Load<Texture2D>("coltonRunRight2"));
            runRightTextures.Add(Content.Load<Texture2D>("coltonRunRight3"));
            runLeftTextures.Add(Content.Load<Texture2D>("coltonRunLeft1"));
            runLeftTextures.Add(Content.Load<Texture2D>("coltonRunLeft2"));
            runLeftTextures.Add(Content.Load<Texture2D>("coltonRunLeft3"));
            playerTexture = idleTexture;



        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            playerSpeed = Vector2.Zero;

            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Title)
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (playButtonRect.Contains(mouseState.Position))
                        screen = Screen.Level1;
                }
            if (screen != Screen.Title)
            {
                if (keyboardState.IsKeyDown(Keys.D))
                {
                    playerSpeed.X += 5;
                    runRight += 1;
                    if (runRight > 2)
                        runRight = 0;
                    playerTexture = runRightTextures[runRight];
                }
                if (keyboardState.IsKeyDown(Keys.A))
                {
                    playerSpeed.X -= 5;
                    runLeft += 1;
                    if (runLeft > 2)
                        runLeft = 0;
                    playerTexture = runLeftTextures[runLeft];
                }

                playerRect.X += (int)playerSpeed.X;

            }
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
                _spriteBatch.DrawString(introFont, ("  Colton's"), new Vector2(500, 100), Color.Red);
                _spriteBatch.DrawString(introFont, ("Jailhouse 2"), new Vector2(500, 200), Color.Red);
                _spriteBatch.Draw(buttonTexture, playButtonRect, Color.CornflowerBlue);
                _spriteBatch.Draw(buttonTexture, levelButtonRect, Color.CornflowerBlue);
                _spriteBatch.DrawString(buttonFont, ("PLAY"), new Vector2(605, 345), Color.Red);
                _spriteBatch.DrawString(buttonFont, ("LEVELS"), new Vector2(580, 445), Color.Red);
            }

            if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(hallwayTexture, window, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.White);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
