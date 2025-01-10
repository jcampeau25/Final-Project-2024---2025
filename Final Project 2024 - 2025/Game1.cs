using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Final_Project_2024___2025
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D hallwayTexture, titleBackgroundTexture, idleTexture, buttonTexture, playerTexture;
        Texture2D cabinetTexture, cabinetShutTexture, cabinetOpenTexture, doorTexture, twoLayerTexture;
        Texture2D rectangleTexture, wallTexture, guardLeftTexture, guardRightTexture, guardUpTexture,guardDownTexture;
        Rectangle window, playButtonRect, levelButtonRect, playerRect, doorRect, guard1Rect;
        Vector2 playerSpeed, guardSpeed;
        Screen screen;
        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState;
        SpriteFont introFont, buttonFont;

        SpriteEffects guard1Direction;


        int runRight = 0;
        int runLeft = 0;
        float runSeconds = 0f;
        bool jump = false;
        bool onGround = false;
        bool inCabinet = false;

        List<Texture2D> runRightTextures;
        List<Texture2D> runLeftTextures;
        List<Rectangle> wallRects1;
        enum Screen
        {
            Title,
            LevelSelect,
            Tutorial,
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
            playerRect = new Rectangle(20, 500, 100, 100);
            playerSpeed = new Vector2();
            guardSpeed = new Vector2();
            runRightTextures = new List<Texture2D>();
            runLeftTextures = new List<Texture2D>();
            wallRects1 = new List<Rectangle>();
            doorRect = new Rectangle(875, 450, 110, 180);
            guard1Rect = new Rectangle(430, 200, 75, 85);
            wallRects1.Add(new Rectangle(0, 470, 700, 20));
            guardSpeed.Y = 5;
            guard1Direction = SpriteEffects.None;


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
            rectangleTexture = Content.Load<Texture2D>("Rectangle");
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

            guardLeftTexture = Content.Load<Texture2D>("guard left");
            guardRightTexture = Content.Load<Texture2D>("guard right");
            guardUpTexture = Content.Load<Texture2D>("guard up");
            guardDownTexture = Content.Load<Texture2D>("guard down");


            wallTexture = Content.Load<Texture2D>("brick wall");
            cabinetShutTexture = Content.Load<Texture2D>("cabinet shut");
            cabinetOpenTexture = Content.Load<Texture2D>("cabinet open");
            twoLayerTexture = Content.Load<Texture2D>("2 layer jail");
            doorTexture = Content.Load<Texture2D>("prison door");
            playerTexture = idleTexture;



        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();
            playerSpeed = new Vector2(0, 0);


            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Title)
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (playButtonRect.Contains(mouseState.Position))
                        screen = Screen.Tutorial;
                }
            if (screen != Screen.Title && screen != Screen.LevelSelect)
            {
                    if (keyboardState.IsKeyDown(Keys.W))
                        playerSpeed.Y -= 7;

                    if (keyboardState.IsKeyDown(Keys.S))
                        playerSpeed.Y += 7;


                
                    if (playerRect.Bottom >= 600)
                        onGround = true;

                    if (playerRect.Bottom < 600)
                        onGround = false;

                    if (keyboardState.IsKeyDown(Keys.D))
                    {
                        playerSpeed.X += 7;
                        runSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (runSeconds >= 0.25)
                        {
                            runSeconds = 0;
                            runRight += 1;
                        }
                        if (runRight > 2)
                            runRight = 0;
                        playerTexture = runRightTextures[runRight];
                    }
                    if (keyboardState.IsKeyDown(Keys.A))
                    {
                        playerSpeed.X -= 7;
                        runSeconds += (float)gameTime.ElapsedGameTime.TotalSeconds;
                        if (runSeconds >= 0.25)
                        {
                            runSeconds = 0;
                            runLeft += 1;
                        }

                        if (runLeft > 2)
                            runLeft = 0;
                        playerTexture = runLeftTextures[runLeft];
                    }

                    if (keyboardState.IsKeyUp(Keys.A) && keyboardState.IsKeyUp(Keys.D))
                        playerTexture = idleTexture;

                if (screen == Screen.Tutorial)
                {
                    

                    if (guard1Rect.Top <= 10 || guard1Rect.Bottom >= 310)
                    {
                        guardSpeed.Y *= -1;
                        if (guardSpeed.Y > 0)
                            guard1Direction = SpriteEffects.None;
                        else
                            guard1Direction = SpriteEffects.FlipVertically;

                    }




                    guard1Rect.Y += (int)guardSpeed.Y;
                    foreach (Rectangle wall in wallRects1)
                        if (playerRect.Intersects(wall))
                            playerRect.Offset(-playerSpeed);

                }
                

                playerRect.Offset (playerSpeed);


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
                _spriteBatch.DrawString(buttonFont, ("LEARN"), new Vector2(590, 345), Color.Red);
                _spriteBatch.DrawString(buttonFont, ("LEVELS"), new Vector2(580, 445), Color.Red);
            }

            if (screen == Screen.Tutorial)
            {
                _spriteBatch.Draw(rectangleTexture, window, Color.Black);
                _spriteBatch.DrawString(buttonFont, ("W/A/S/D To Move"), new Vector2(50, 10), Color.Red);
                _spriteBatch.DrawString(buttonFont, ("Get To The Door"), new Vector2(550, 10), Color.Red);
                _spriteBatch.Draw(doorTexture, doorRect, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.White);
                _spriteBatch.Draw(guardDownTexture, guard1Rect, null, Color.White, 0f, Vector2.Zero, guard1Direction, 1f);
                _spriteBatch.Draw(wallTexture, new Rectangle(0, 470, 700, 20), Color.White);
                _spriteBatch.Draw(wallTexture, new Rectangle(850, 340, 20, 260), Color.White);
                _spriteBatch.Draw(wallTexture, new Rectangle(100, 320, 770, 20), Color.White);
                _spriteBatch.Draw(wallTexture, new Rectangle(400, 0, 20, 180), Color.White);
                _spriteBatch.Draw(wallTexture, new Rectangle(510, 0, 20, 180), Color.White);



            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
