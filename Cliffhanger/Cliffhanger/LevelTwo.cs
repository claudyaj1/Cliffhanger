using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Claudy.Input;

namespace Cliffhanger
{
    public class LevelTwo : Microsoft.Xna.Framework.GameComponent
    {
        //Viewport stuff
        RenderTarget2D topScreen;
        RenderTarget2D bottomScreen;
        int bottomOffset;

        //Cliff
        Texture2D cliffTex;
        Rectangle cliffRect;
        Vector2 offsetTop;
        Vector2 offsetBottom;

        //Screen Player stuff
        Vector2 p1ScreenPos, p2ScreenPos;
        Vector2 p1ScreenVel, p2ScreenVel;
        Texture2D test;
        Boolean screenSplit;
        Color p1, p2;
        Rectangle playerBounds;


        //Player Stuff
        Player player1;
        Player player2;

        //Platform
        List<Platform> platforms;
        Platform ground;

        //Rock
        List<Rock> rocks;

        //Vine
        List<Vine> vines;

        //Rock Machine
        RockMachine guitar;

        const int FINISH = -3180;
        public bool isCompleted;
        public int victorPlayerNum;

        //Audio Effects
        SoundEffect victorySound;
        SoundEffect jumpSound;

        GraphicsDevice GraphicsDevice;
        SpriteFont font;

        public LevelTwo(Game game)
            : base(game)
        {
        }


        public void Initialize(GraphicsDevice gd)
        {
            jumpSound = Game.Content.Load<SoundEffect>("Jump");

            GraphicsDevice = gd;
            topScreen = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / 2);
            bottomScreen = new RenderTarget2D(GraphicsDevice, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height / 2);

            offsetTop = Vector2.Zero;
            offsetBottom = new Vector2(0, -topScreen.Height);

            //Player
            player1 = new Player(Game, 1, jumpSound);
            player1.Initialize();
            player1.position = new Vector2(100, 0);
            player2 = new Player(Game, 2, jumpSound);
            player2.Initialize();
            player2.position = new Vector2(400, 0);

            //Platform
            platforms = new List<Platform>();
            ground = new Platform(Game, -20, 400, 2000, 1000);
            ground.Initialize();
            platforms.Add(ground);


            //Screen Representation of players
            p1ScreenPos = new Vector2(100, 20);
            p2ScreenPos = new Vector2(GraphicsDevice.Viewport.Width - 100, 20);
            p1ScreenVel = new Vector2(0, 0);
            p2ScreenVel = new Vector2(0, 0);
            //changes the screen bounds of where the player is.
            playerBounds = new Rectangle(0, 0, 10, 60);
            screenSplit = false;

            p1 = Color.Red;
            p2 = Color.Blue;



            //Rock
            rocks = new List<Rock>();

            //Guitar (Rock Machine)
            guitar = new RockMachine(Game, 700, 200);

            //Vine
            vines = new List<Vine>();
            //vines.Add(new Vine(Game, -128,  11,     0)); // (Game, Position Y, Height/32, Lane)
            vines.Add(new Vine(Game, 96,    9,  5)); // Ground is some where about 224 .
            vines.Add(new Vine(Game, 256,   2,  1));
            vines.Add(new Vine(Game, -32,   8,  2));
            vines.Add(new Vine(Game, -160, 11,  4));
            vines.Add(new Vine(Game,   0,   2,  6));
            vines.Add(new Vine(Game, -96,   2,  7));

            vines.Add(new Vine(Game, -480, 12, 5));
            vines.Add(new Vine(Game, -576, 8, 7));
            vines.Add(new Vine(Game, -320, 5, 3));
            vines.Add(new Vine(Game, -256, 7, 1));
            vines.Add(new Vine(Game, -544, 4, 6));

            vines.Add(new Vine(Game, -608, 9, 1));
            vines.Add(new Vine(Game, -768, 6, 2));
            vines.Add(new Vine(Game, -768, 1, 3));
            vines.Add(new Vine(Game, -736, 5, 1));
            vines.Add(new Vine(Game, -960, 2, 0));

            //Over 1000
            vines.Add(new Vine(Game, -1024, 15, 5));
            vines.Add(new Vine(Game, -1056, 6,  3));
            vines.Add(new Vine(Game, -1120, 4,  4));
            vines.Add(new Vine(Game, -1152, 6,  7));
            vines.Add(new Vine(Game, -1216, 4,  1));
            vines.Add(new Vine(Game, -1248, 2,  7));
            vines.Add(new Vine(Game, -1344, 2,  7));
            vines.Add(new Vine(Game, -1440, 1,  7));

            ///
            vines.Add(new Vine(Game, -1280, 3, 6));
            vines.Add(new Vine(Game, -1344, 3, 5));
            vines.Add(new Vine(Game, -1312, 3, 4));
            vines.Add(new Vine(Game, -1280, 5, 3));
            vines.Add(new Vine(Game, -1344, 4, 2));
            vines.Add(new Vine(Game, -1280, 4, 1));
            //vines.Add(new Vine(Game, -1248, 7, 0));

            //After 1400
            vines.Add(new Vine(Game, -1440, 2, 6));
            vines.Add(new Vine(Game, -1504, 1, 2));
            vines.Add(new Vine(Game, -1536, 2, 5));
            vines.Add(new Vine(Game, -1568, 1, 4));
            vines.Add(new Vine(Game, -1600, 1, 3));
            vines.Add(new Vine(Game, -1664, 1, 4));
            vines.Add(new Vine(Game, -1952, 7, 4));
            vines.Add(new Vine(Game, -2016, 8, 1));
            vines.Add(new Vine(Game, -1824, 6, 2));

            //After 2000
            vines.Add(new Vine(Game, -2016, 4, 4));
            vines.Add(new Vine(Game, -2112, 4, 2));
            vines.Add(new Vine(Game, -2176, 3, 3));
            vines.Add(new Vine(Game, -2176, 3, 7));
            vines.Add(new Vine(Game, -2336, 12, 6));

            vines.Add(new Vine(Game, -2464, 4, 7));
            vines.Add(new Vine(Game, -2464, 7, 1));
            vines.Add(new Vine(Game, -2560, 11, 2));
            vines.Add(new Vine(Game, -2560, 3, 6));
            vines.Add(new Vine(Game, -2688, 5, 3));
            vines.Add(new Vine(Game, -2720, 6, 4));
            vines.Add(new Vine(Game, -2784, 5, 5));
            vines.Add(new Vine(Game, -2784, 6, 7));

            vines.Add(new Vine(Game, -2800, 3, 2));
            vines.Add(new Vine(Game, -2848, 3, 6));
            vines.Add(new Vine(Game, -2944, 5, 1));
            vines.Add(new Vine(Game, -2848, 3, 7));
            vines.Add(new Vine(Game, -3008, 4, 2));
            vines.Add(new Vine(Game, -3136, 7, 3));
            vines.Add(new Vine(Game, -3136, 7, 5));

            foreach (Vine vine in vines)
            {
                vine.Initialize();
            }


            isCompleted = false;
            victorPlayerNum = 0;


            base.Initialize();
        }

        public void LoadContent()
        {
            cliffTex = Game.Content.Load<Texture2D>("cliff");
            cliffRect = new Rectangle(0, GraphicsDevice.Viewport.Height - cliffTex.Height * 2, GraphicsDevice.Viewport.Width * 2, cliffTex.Height * 2);
            font = Game.Content.Load<SpriteFont>("Consolas");
            test = Game.Content.Load<Texture2D>("blankTex");
            victorySound = Game.Content.Load<SoundEffect>("Flashpoint001");
        }

        public void reset()
        {
            offsetTop = Vector2.Zero;
            offsetBottom = new Vector2(0, -topScreen.Height);

            //Player
            player1 = new Player(Game, 1, jumpSound);
            player1.Initialize();
            player1.position = new Vector2(100, 0);
            player2 = new Player(Game, 2, jumpSound);
            player2.Initialize();
            player2.position = new Vector2(400, 0);
            //Screen Representation of players
            p1ScreenPos = new Vector2(100, 20);
            p2ScreenPos = new Vector2(GraphicsDevice.Viewport.Width - 100, 20);
            p1ScreenVel = new Vector2(0, 0);
            p2ScreenVel = new Vector2(0, 0);
            screenSplit = false;
            isCompleted = false;
        }
        public void Update(GameTime gameTime, ClaudyInput input, Rectangle titleSafeRect)
        {
            ground.Update(gameTime);

            #region Platform Collision
            foreach (Platform platform in platforms)
            {
                if (player1.vel.Y >= 0)
                {

                    if (Collision.PlayerPlatformCollision(player1, platform))
                    {
                        //state = PlayerState.standing;
                        player1.canjump = true;
                        if (!screenSplit)
                            p1ScreenPos.Y = platform.position.Y - playerBounds.Height + offsetTop.Y;
                        else
                            p1ScreenPos.Y = platform.position.Y - playerBounds.Height + offsetBottom.Y + GraphicsDevice.Viewport.Height / 2;
                        break;
                    }
                    else
                    {
                        //state = PlayerState.falling;
                        player1.canjump = false;
                    }
                }
            }
            foreach (Platform platform in platforms)
            {
                if (player2.vel.Y >= 0)
                {
                    if (Collision.PlayerPlatformCollision(player2, platform))
                    {
                        //state = PlayerState.standing;
                        player2.canjump = true;
                        if (!screenSplit)
                            p2ScreenPos.Y = platform.position.Y - playerBounds.Height + offsetTop.Y;
                        else
                            p2ScreenPos.Y = platform.position.Y - playerBounds.Height + offsetBottom.Y + GraphicsDevice.Viewport.Height / 2;
                        break;
                    }
                    else
                    {
                        //state = PlayerState.falling;
                        player2.canjump = false;
                    }
                }
            }
            #endregion //Platform Collision

            foreach (Rock r in rocks)
            {
                r.Update(gameTime, ground);
            }
            for (int i = 0; i < rocks.Count; i++)
            {
                if (!rocks[i].Enabled) // Remove any rocks deemed to be disposed of.
                {
                    rocks.RemoveAt(i);
                }
            }

            #region Rock Machine Collision
            if (guitar.IsActive &&
                (player1.hitbox.Intersects(guitar.rect) || player2.hitbox.Intersects(guitar.rect)))
            {
                Random rng = new Random();
                for (int i = 0; i < 10; i++)
                {
                    Rock r = new Rock(Game,
                        (float)rng.Next(1000),
                        -(float)rng.Next(2500),
                        Rock.SUGGESTED_UP_R_VELOCITY, 3);
                    rocks.Add(r);
                }
                for (int i = 0; i < 10; i++)
                {
                    Rock r = new Rock(Game,
                        (float)rng.Next(1000),
                        -(float)rng.Next(2500),
                        Rock.SUGGESTED_UP_L_VELOCITY, 3);
                    rocks.Add(r);
                }
                guitar.Fired(gameTime);
            }
            guitar.Update(gameTime);
            #endregion

            #region Vine Collision
            foreach (Vine vine in vines)
            {
                if (Collision.PlayerVineCollision(player1, vine, gameTime) && input.GetAs8DirectionLeftThumbStick(player1.Num).Y > 0)
                {
                    player1.vel.Y = -2f;
                    player1.vel.X *= .5f;

                    if (player1.position.Y + player1.hitbox.Height > vine.position.Y && player1.position.Y + player1.hitbox.Height < vine.position.Y + 4)
                    {
                        player1.vel.Y = 0;
                    }
                }


                if (player1.vel.Y >= 0)
                {
                    if (Collision.PlayerVineCollision(player1, vine, gameTime))
                    {
                        //state = PlayerState.standing;
                        player1.canjump = true;
                        player1.vel.X *= .5f;
                        player1.vel.Y = 0;
                        break;
                    }
                    else
                    {
                        //state = PlayerState.falling;
                        //player1.canjump = false;
                    }

                }

            }
            foreach (Vine vine in vines)
            {
                if (Collision.PlayerVineCollision(player2, vine, gameTime) && input.GetAs8DirectionLeftThumbStick(player2.Num).Y > 0)
                {
                    player2.vel.Y = -2;
                    player2.vel.X *= .5f;

                    if (player2.position.Y + player2.hitbox.Height > vine.position.Y && player2.position.Y + player2.hitbox.Height < vine.position.Y + 4)
                    {
                        player2.vel.Y = 0;
                    }
                }


                if (player2.vel.Y >= 0)
                {
                    if (Collision.PlayerVineCollision(player2, vine, gameTime))
                    {
                        //state = PlayerState.standing;
                        player2.canjump = true;
                        player2.vel.X *= .5f;
                        player2.vel.Y = 0;
                        break;
                    }
                    else
                    {
                        //state = PlayerState.falling;
                        //player1.canjump = false;
                    }
                }
            }
            #endregion //Vine Collision

            //Player updates
            player1.Update(gameTime, titleSafeRect);
            player2.Update(gameTime, titleSafeRect);


            #region Throw Rocks (requires knowledge of player & of the rock list)

            //PLAYER 1
            if (input.GamepadByID[player1.Num].Triggers.Left > 0.5f &&
                input.PreviousGamepadByID[player1.Num].Triggers.Left <= 0.5f)
            {
                Rock r = new Rock(Game,
                    player1.hitbox.X, player1.hitbox.Y,
                    Rock.SUGGESTED_SIDE_L_VELOCITY,
                    player1.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.GamepadByID[player1.Num].Triggers.Right > 0.5f &&
                input.PreviousGamepadByID[player1.Num].Triggers.Right <= 0.5f)
            {
                Rock r = new Rock(Game,
                    player1.hitbox.X + player1.hitbox.Width, player1.hitbox.Y,
                    Rock.SUGGESTED_SIDE_R_VELOCITY,
                    player1.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.isFirstPress(Buttons.RightShoulder, player1.Num))
            {
                Rock r = new Rock(Game,
                    player1.hitbox.X, player1.hitbox.Y,
                    Rock.SUGGESTED_UP_R_VELOCITY,
                    player1.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.isFirstPress(Buttons.LeftShoulder, player1.Num))
            {
                Rock r = new Rock(Game,
                    player1.hitbox.X + player1.hitbox.Width, player1.hitbox.Y,
                    Rock.SUGGESTED_UP_L_VELOCITY,
                    player1.Num);
                r.Initialize();
                rocks.Add(r);
            }
            //PLAYER 2
            if (input.GamepadByID[player2.Num].Triggers.Left > 0.5f &&
                input.PreviousGamepadByID[player2.Num].Triggers.Left <= 0.5f)
            {
                Rock r = new Rock(Game,
                    player2.hitbox.X, player2.hitbox.Y,
                    Rock.SUGGESTED_SIDE_L_VELOCITY,
                    player2.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.GamepadByID[player2.Num].Triggers.Right > 0.5f &&
                input.PreviousGamepadByID[player2.Num].Triggers.Right <= 0.5f)
            {
                Rock r = new Rock(Game,
                    player2.hitbox.X + player2.hitbox.Width, player2.hitbox.Y,
                    Rock.SUGGESTED_SIDE_R_VELOCITY,
                    player2.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.isFirstPress(Buttons.RightShoulder, player2.Num))
            {
                Rock r = new Rock(Game,
                    player2.hitbox.X, player2.hitbox.Y,
                    Rock.SUGGESTED_UP_R_VELOCITY,
                    player2.Num);
                r.Initialize();
                rocks.Add(r);
            }
            if (input.isFirstPress(Buttons.LeftShoulder, player2.Num))
            {
                Rock r = new Rock(Game,
                    player2.hitbox.X + player2.hitbox.Width, player2.hitbox.Y,
                    Rock.SUGGESTED_UP_L_VELOCITY,
                    player2.Num);
                r.Initialize();
                rocks.Add(r);
            }
            #endregion


            p1ScreenVel.Y = -player1.vel.Y;
            p2ScreenVel.Y = -player2.vel.Y;

            p1ScreenPos.Y -= p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
            p2ScreenPos.Y -= p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;


            #region splitScreen Movement
            if (p1ScreenPos.Y < titleSafeRect.Y + 20)
            {
                p1ScreenPos.Y = titleSafeRect.Y + 20;
                offsetTop.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;

                if (p2ScreenPos.Y > titleSafeRect.Height - playerBounds.Height - 5)
                {
                    screenSplit = true;
                }

                if (!screenSplit)
                {
                    p2ScreenPos.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    offsetBottom.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                }
            }
            if (p2ScreenPos.Y < titleSafeRect.Y + 20)
            {
                p2ScreenPos.Y = titleSafeRect.Y + 20;
                offsetTop.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;

                if (p1ScreenPos.Y > titleSafeRect.Height - playerBounds.Height - 5 && !screenSplit)
                {
                    screenSplit = true;
                }

                if (!screenSplit)
                {
                    p1ScreenPos.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    offsetBottom.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                }
            }
            else if (p2ScreenPos.Y > titleSafeRect.Height - playerBounds.Height)
            {
                offsetBottom.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                p2ScreenPos.Y = titleSafeRect.Height - playerBounds.Height;
                if (p1ScreenPos.Y < titleSafeRect.Y && !screenSplit)
                {
                    screenSplit = true;
                }

                if (!screenSplit)
                {
                    p1ScreenPos.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    offsetTop.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                }
            }
            if (p1ScreenPos.Y > titleSafeRect.Height - playerBounds.Height)
            {
                offsetBottom.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                p1ScreenPos.Y = titleSafeRect.Height - playerBounds.Height;
                if (p2ScreenPos.Y < titleSafeRect.Y && !screenSplit)
                {
                    screenSplit = true;
                }

                if (!screenSplit)
                {
                    p2ScreenPos.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    offsetTop.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                }
            }


            if (screenSplit)
            {
                if (p1ScreenPos.Y > GraphicsDevice.Viewport.Height / 2 - playerBounds.Height && p1ScreenPos.Y < GraphicsDevice.Viewport.Height / 2)
                {
                    p1ScreenPos.Y = GraphicsDevice.Viewport.Height / 2 - playerBounds.Height;
                    offsetTop.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    if (offsetTop.Y <= offsetBottom.Y + GraphicsDevice.Viewport.Height / 2)
                        screenSplit = false;

                }
                if (p2ScreenPos.Y > GraphicsDevice.Viewport.Height / 2 - playerBounds.Height && p2ScreenPos.Y < GraphicsDevice.Viewport.Height / 2)
                {
                    p2ScreenPos.Y = GraphicsDevice.Viewport.Height / 2 - playerBounds.Height;
                    offsetTop.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    if (offsetTop.Y <= offsetBottom.Y + GraphicsDevice.Viewport.Height / 2)
                        screenSplit = false;

                }
                if (p2ScreenPos.Y < GraphicsDevice.Viewport.Height / 2 + playerBounds.Height && p2ScreenPos.Y > GraphicsDevice.Viewport.Height / 2)
                {
                    p2ScreenPos.Y = GraphicsDevice.Viewport.Height / 2 + playerBounds.Height;
                    offsetBottom.Y += p2ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    if (offsetTop.Y <= offsetBottom.Y + GraphicsDevice.Viewport.Height / 2)
                        screenSplit = false;

                }
                if (p1ScreenPos.Y < GraphicsDevice.Viewport.Height / 2 + playerBounds.Height && p1ScreenPos.Y > GraphicsDevice.Viewport.Height / 2)
                {
                    p1ScreenPos.Y = GraphicsDevice.Viewport.Height / 2 + playerBounds.Height;
                    offsetBottom.Y += p1ScreenVel.Y * gameTime.ElapsedGameTime.Milliseconds / 10f;
                    if (offsetTop.Y <= offsetBottom.Y + GraphicsDevice.Viewport.Height / 2)
                        screenSplit = false;

                }
            }




            if (offsetTop.Y <= offsetBottom.Y + GraphicsDevice.Viewport.Height / 2)
            {
                screenSplit = false;
                offsetBottom.Y = offsetTop.Y - GraphicsDevice.Viewport.Height / 2;
            }
            #endregion


            #region Rock Collisions
            foreach (Rock r in rocks)
            {
                if (Collision.PlayerRockCollision(player1, r))
                {
                    continue;
                }
                else
                {
                    Collision.PlayerRockCollision(player2, r);
                }
            }
            #endregion // Rock Collisions

            if (player1.position.Y <= FINISH || player2.position.Y <= FINISH)
            {
                victorySound.Play();
                isCompleted = true;
                if (player1.position.Y < player2.position.Y)
                    victorPlayerNum = 1;
                else if (player2.position.Y < player1.position.Y)
                    victorPlayerNum = 2;
            }
            base.Update(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            //Draw stuff in the top renderTarget
            GraphicsDevice.SetRenderTarget(topScreen);
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null); // TODO: Change to LinearWrap before submitting to Dr. Birmingham.

            #region Top Viewport
            {
                spriteBatch.Draw(cliffTex, new Rectangle(cliffRect.X, cliffRect.Y + (int)offsetTop.Y, cliffRect.Width, cliffRect.Height), Color.White);

                //Vines
                foreach (Vine vine in vines)
                {
                    vine.Draw(spriteBatch, offsetTop);
                }
                spriteBatch.Draw(cliffTex, new Rectangle(cliffRect.X, -3160 + (int)offsetTop.Y, cliffRect.Width, 3), Color.LawnGreen);
                guitar.Draw(spriteBatch, offsetTop);
                player1.Draw(spriteBatch, offsetTop);
                player2.Draw(spriteBatch, offsetTop);
                ground.Draw(spriteBatch, offsetTop);
                foreach (Rock r in rocks)
                {
                    r.Draw(spriteBatch, offsetTop);
                }
            }
            #endregion //Top Viewport

            spriteBatch.End();

            //Draw stuff in the bottom renderTarget; Use an offset
            GraphicsDevice.SetRenderTarget(bottomScreen);
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.LinearWrap, null, null);

            bottomOffset = GraphicsDevice.Viewport.Height;

            #region Bottom Viewport
            {
                spriteBatch.Draw(cliffTex, new Rectangle(cliffRect.X, cliffRect.Y + (int)offsetBottom.Y, cliffRect.Width, cliffRect.Height), Color.White);

                //Vines
                foreach (Vine vine in vines)
                {
                    vine.Draw(spriteBatch, offsetBottom);
                }
                spriteBatch.Draw(cliffTex, new Rectangle(cliffRect.X, -3160 + (int)offsetBottom.Y, cliffRect.Width, 3), Color.LawnGreen);
                guitar.Draw(spriteBatch, offsetBottom);
                player1.Draw(spriteBatch, offsetBottom);
                player2.Draw(spriteBatch, offsetBottom);
                ground.Draw(spriteBatch, offsetBottom);
                foreach (Rock r in rocks)
                {
                    r.Draw(spriteBatch, offsetBottom);
                }
            }
            #endregion //Bottom Viewport

            spriteBatch.End();

            //Draw the renderTargets
            GraphicsDevice.SetRenderTarget(null);
            spriteBatch.Begin();
            spriteBatch.Draw(topScreen, new Vector2(0, 0), Color.White);
            spriteBatch.Draw(bottomScreen, new Vector2(0, GraphicsDevice.Viewport.Height / 2), Color.White);
            //Debugging draw statements
            //spriteBatch.Draw(test, new Rectangle((int)p1ScreenPos.X, (int)p1ScreenPos.Y, playerBounds.Width, playerBounds.Height), p1);
            //spriteBatch.Draw(test, new Rectangle((int)p2ScreenPos.X, (int)p2ScreenPos.Y, playerBounds.Width, playerBounds.Height), p2);
            //spriteBatch.DrawString(font, "bool: " + player1.canjump.ToString(), new Vector2(0, 0), Color.Black);
            //spriteBatch.DrawString(font, "bool: " + player1.vel.ToString(), new Vector2(0, 50), Color.Orange);
            //spriteBatch.DrawString(font, player1.position.Y.ToString(), new Vector2(0f, 200f), Color.Yellow);
            spriteBatch.End();

        }
    }
}
