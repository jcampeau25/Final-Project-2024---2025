using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Final_Project_2024___2025
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D hallwayTexture, titleBackgroundTexture, idleTexture, buttonTexture, playerTexture;
        Texture2D doorTexture, keycardTexture;
        Texture2D rectangleTexture, wallTexture, guardLeftTexture, guardRightTexture, guardUpTexture,guardDownTexture;
        Rectangle window, playButtonRect, levelButtonRect, levelPassRect, playerRect, doorRect, keycardRect, guardRect1A, guardRect2A, guardRect2B, guardRect2C, guardRect3A, guardRect3B;
        Rectangle level1Rect, level2Rect, level3Rect, level4Rect, level5Rect;
        Rectangle startLevel1Rect, startLevel2Rect, startLevel3Rect, startLevel4Rect, startLevel5Rect;
        Vector2 playerSpeed, guard1ASpeed, guard2ASpeed, guard2BSpeed, guard2CSpeed;
        Screen screen;
        MouseState mouseState, prevMouseState;
        KeyboardState keyboardState;
        SpriteFont introFont, buttonFont, keycardFont;

        SpriteEffects guard1ADirection, guard2ADirection, guard2BDirection, guard2CDirection;


        int runRight = 0;
        int runLeft = 0;
        float runSeconds = 0f;

        bool keycard;

        List<Texture2D> runRightTextures;
        List<Texture2D> runLeftTextures;
        List<Rectangle> wallRects1, wallRects2, wallRects3, wallRects4, wallRects5;
        List<Rectangle> guardRects1;

        enum Screen
        {
            Title,
            LevelSelect,
            PassLevel,
            Level1,
            Level2,
            Level3,
            Level4,
            Level5
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
            levelPassRect = new Rectangle(400, 250, 200, 100);
            playerRect = new Rectangle(20, 500, 80, 80);
            playerSpeed = new Vector2();

            guard1ASpeed = new Vector2(0, 5);

            guard2ASpeed = new Vector2(0, 7);
            guard2BSpeed = new Vector2(5, 0);
            guard2CSpeed = new Vector2(0, 2);

            runRightTextures = new List<Texture2D>();
            runLeftTextures = new List<Texture2D>();
            wallRects1 = new List<Rectangle>();
            wallRects2 = new List<Rectangle>();
            wallRects3 = new List<Rectangle>();
            wallRects4 = new List<Rectangle>();
            wallRects5 = new List<Rectangle>();

            startLevel1Rect = new Rectangle(20, 500, 80, 80);
            startLevel2Rect = new Rectangle(5, 5, 80, 80);
            startLevel3Rect = new Rectangle(450, 500, 80, 80);
            //guardRects1 = new List<Rectangle>();
            
            wallRects1.Add(new Rectangle(0, 470, 700, 20));
            wallRects1.Add(new Rectangle(850, 340, 20, 260));
            wallRects1.Add(new Rectangle(150, 320, 720, 20));
            wallRects1.Add(new Rectangle(400, 0, 20, 180));
            wallRects1.Add(new Rectangle(510, 0, 20, 180));

            wallRects2.Add(new Rectangle(100, 0, 20, 240));
            wallRects2.Add(new Rectangle(100, 360, 20, 100));
            wallRects2.Add(new Rectangle(100, 460, 600, 20));
            wallRects2.Add(new Rectangle(810, 460, 210, 20));
            wallRects2.Add(new Rectangle(810, 100, 20, 360));
            wallRects2.Add(new Rectangle(680, 100, 20, 260));
            wallRects2.Add(new Rectangle(220, 340, 460, 20));
            wallRects2.Add(new Rectangle(220, 220, 460, 20));


            wallRects3.Add(new Rectangle(150, 400, 300, 20));
            wallRects3.Add(new Rectangle(550, 400, 300, 20));
            wallRects3.Add(new Rectangle(150, 0, 20, 400));
            wallRects3.Add(new Rectangle(830, 0, 20, 400));
            wallRects3.Add(new Rectangle(430, 100, 20, 300));
            wallRects3.Add(new Rectangle(550, 100, 20, 300));


            guard1ADirection = SpriteEffects.None;

            guard2ADirection = SpriteEffects.None;
            guard2BDirection = SpriteEffects.None;
            guard2CDirection = SpriteEffects.None;


            guardRect1A = new Rectangle(435, 200, 50, 65);

            guardRect2A = new Rectangle(720, 20, 50, 65);
            guardRect2B = new Rectangle(560, 260, 65, 50);
            guardRect2C = new Rectangle(130, 10, 50, 65);

            guardRect3A = new Rectangle(30, 380, 50, 65);
            guardRect3B = new Rectangle(200, 20, 50, 65);

            level1Rect = new Rectangle(260, 200, 100, 100);
            level2Rect = new Rectangle(460, 200, 100, 100);
            level3Rect = new Rectangle(660, 200, 100, 100);
            level4Rect = new Rectangle(360, 325, 100, 100);
            level5Rect = new Rectangle(560, 325, 100, 100);



            screen = Screen.Title;

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
            keycardFont = Content.Load<SpriteFont>("keycardFont");
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
            keycardTexture = Content.Load<Texture2D>("keycard");
            doorTexture = Content.Load<Texture2D>("prison door");
            playerTexture = idleTexture;



        }

        protected override void Update(GameTime gameTime)
        {
            prevMouseState = mouseState;
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();



            this.Window.Title = $"x = {mouseState.X}, y = {mouseState.Y}";

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (screen == Screen.Title)
            {


                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (playButtonRect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level1;
                        doorRect = new Rectangle(875, 450, 110, 180);
                        keycard = true;

                    }
                    if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                    {
                        if (levelButtonRect.Contains(mouseState.Position))
                            screen = Screen.LevelSelect;
                    }
                }
            }
            else if (screen == Screen.LevelSelect)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (level1Rect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level1;
                        playerRect = startLevel1Rect;
                        doorRect = new Rectangle(875, 450, 110, 180);
                        keycard = true;
                    }
                    else if (level2Rect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level2;
                        playerRect = startLevel2Rect;
                        doorRect = new Rectangle(850, 280, 110, 180);
                        keycard = false;
                        keycardRect = new Rectangle(860, 500, 50, 85);
                    }

                    else if (level3Rect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level3;
                        playerRect = startLevel3Rect;
                        keycard = false;
                        keycardRect = new Rectangle(30, 130, 50, 85);


                    }

                    else if (level4Rect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level4;
                    }
                    else if (level5Rect.Contains(mouseState.Position))
                    {
                        screen = Screen.Level5;
                    }
                }
            }
            else if (screen == Screen.Level1)
            {
                GetPlayerSpeed(gameTime);
                guardRect1A.Offset(guard1ASpeed);

                if (guardRect1A.Top <= 10 || guardRect1A.Bottom >= 310)
                {
                    guard1ASpeed.Y *= -1;
                    if (guard1ASpeed.Y > 0)
                        guard1ADirection = SpriteEffects.None;
                    else
                        guard1ADirection = SpriteEffects.FlipVertically;

                }






                if (playerRect.Intersects(guardRect1A))
                    playerRect = startLevel1Rect;

                foreach (Rectangle wall in wallRects1)
                    if (playerRect.Intersects(wall))
                        playerRect.Offset(-playerSpeed);


            
            }

            else if (screen == Screen.Level2)
            {
                GetPlayerSpeed(gameTime);

                foreach (Rectangle wall in wallRects2)
                    if (playerRect.Intersects(wall))
                        playerRect.Offset(-playerSpeed);

                guardRect2A.Offset(guard2ASpeed);

                if (guardRect2A.Top <= 0 || guardRect2A.Bottom >= 600)
                {
                    guard2ASpeed.Y *= -1;
                    if (guard2ASpeed.Y > 0)
                        guard2ADirection = SpriteEffects.None;
                    else
                        guard2ADirection = SpriteEffects.FlipVertically;

                }

                guardRect2B.Offset(guard2BSpeed);

                if (guardRect2B.Left <= 0 || guardRect2B.Right >= 680)
                {
                    guard2BSpeed.X *= -1;
                    if (guard2BSpeed.X > 0)
                        guard2BDirection = SpriteEffects.None;
                    else
                        guard2BDirection = SpriteEffects.FlipHorizontally;

                }

                guardRect2C.Offset(guard2CSpeed);


                if (guardRect2C.Top <= 0 || guardRect2C.Bottom >= 460)
                {
                    guard2CSpeed.Y *= -1;
                    if (guard2CSpeed.Y > 0)
                        guard2CDirection = SpriteEffects.None;
                    else
                        guard2CDirection = SpriteEffects.FlipVertically;

                }

                if (playerRect.Intersects(guardRect2A) || playerRect.Intersects(guardRect2B) || playerRect.Intersects(guardRect2C))
                    playerRect = startLevel2Rect;

            }

            if (screen == Screen.Level3)
            {
                GetPlayerSpeed(gameTime);





                foreach (Rectangle wall in wallRects3)
                    if (playerRect.Intersects(wall))
                        playerRect.Offset(-playerSpeed);
            }



            else if (screen == Screen.PassLevel)
            {
                if (mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
                {
                    if (levelPassRect.Contains(mouseState.Position))
                        screen = Screen.LevelSelect;
                }


            }
            base.Update(gameTime);
        }

        public void GetPlayerSpeed(GameTime gameTime)
        {
            playerSpeed = Vector2.Zero;


            if (keyboardState.IsKeyDown(Keys.W))
                playerSpeed.Y -= 7;

            if (keyboardState.IsKeyDown(Keys.S))
                playerSpeed.Y += 7;
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

            playerRect.Offset(playerSpeed);

            if (playerRect.Top < 0 || playerRect.Bottom > 600 || playerRect.Left < 0 || playerRect.Right > 1000)
                playerRect.Offset(-playerSpeed);

            if (playerRect.Intersects(keycardRect))
                keycard = true;




            if (keycard == true && doorRect.Contains(playerRect))
                screen = Screen.PassLevel;

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
                _spriteBatch.DrawString(buttonFont, ("LEVEL 1"), new Vector2(575, 345), Color.Red);
                _spriteBatch.DrawString(buttonFont, ("LEVELS"), new Vector2(580, 445), Color.Red);
            }
            if (screen == Screen.LevelSelect)
            {
                _spriteBatch.Draw(titleBackgroundTexture, window, Color.White);
                _spriteBatch.Draw(rectangleTexture, level1Rect, Color.CornflowerBlue);
                _spriteBatch.Draw(rectangleTexture, level2Rect, Color.CornflowerBlue);
                _spriteBatch.Draw(rectangleTexture, level3Rect, Color.CornflowerBlue);
                _spriteBatch.Draw(rectangleTexture, level4Rect, Color.CornflowerBlue);
                _spriteBatch.Draw(rectangleTexture, level5Rect, Color.CornflowerBlue);
                _spriteBatch.DrawString(introFont, ("1"), new Vector2(300, 190), Color.Red);
                _spriteBatch.DrawString(introFont, ("2"), new Vector2(490, 190), Color.Red);
                _spriteBatch.DrawString(introFont, ("3"), new Vector2(690, 190), Color.Red);
                _spriteBatch.DrawString(introFont, ("4"), new Vector2(390, 315), Color.Red);
                _spriteBatch.DrawString(introFont, ("5"), new Vector2(590, 315), Color.Red);

            }




            if (screen == Screen.Level1)
            {
                _spriteBatch.Draw(rectangleTexture, window, Color.Black);
                _spriteBatch.DrawString(buttonFont, ("W/A/S/D To Move"), new Vector2(50, 10), Color.Red);
                _spriteBatch.DrawString(buttonFont, ("Get To The Door"), new Vector2(550, 10), Color.Red);
                _spriteBatch.Draw(doorTexture, doorRect, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.White);
                _spriteBatch.Draw(guardDownTexture, guardRect1A, null, Color.White, 0f, Vector2.Zero, guard1ADirection, 1f);
                _spriteBatch.Draw(wallTexture, wallRects1[0], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects1[1], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects1[2], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects1[3], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects1[4], Color.White);



               


            }

            if (screen == Screen.Level2)
            {
                _spriteBatch.Draw(rectangleTexture, window, Color.Black);
                _spriteBatch.Draw(doorTexture, doorRect, Color.White);
                _spriteBatch.Draw(playerTexture, playerRect, Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[0], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[1], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[2], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[3], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[4], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[5], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[6], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects2[7], Color.White);
                _spriteBatch.Draw(guardDownTexture, guardRect2A, null, Color.White, 0f, Vector2.Zero, guard2ADirection, 1f);
                _spriteBatch.Draw(guardRightTexture, guardRect2B, null, Color.White, 0f, Vector2.Zero, guard2BDirection, 1f);
                _spriteBatch.Draw(guardDownTexture, guardRect2C, null, Color.White, 0f, Vector2.Zero, guard2CDirection, 1f);

                if (keycard == false)
                    _spriteBatch.Draw(keycardTexture, keycardRect, Color.White);

                if (keycard == false && doorRect.Contains(playerRect))
                    _spriteBatch.DrawString(keycardFont, "Keycard Required", new Vector2(840, 230), Color.Red);

            }

            if (screen == Screen.Level3)
            {
                _spriteBatch.Draw(rectangleTexture, window, Color.Black);
                _spriteBatch.Draw(playerTexture, playerRect, Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[0], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[1], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[2], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[3], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[4], Color.White);
                _spriteBatch.Draw(wallTexture, wallRects3[5], Color.White);

                if (keycard == false)
                    _spriteBatch.Draw(keycardTexture, keycardRect, Color.White);
            }


            if (screen == Screen.PassLevel)
            {
                _spriteBatch.Draw(rectangleTexture, window, Color.Black);
                _spriteBatch.Draw(rectangleTexture, levelPassRect, Color.Red);
                _spriteBatch.DrawString(introFont, ("LEVEL PASSED"), new Vector2(270, 75), Color.White);
                _spriteBatch.DrawString(buttonFont, ("LEVELS"), new Vector2(420, 270), Color.White);


            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
