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


namespace Cliffhanger
{
    public class Rock : Microsoft.Xna.Framework.GameComponent
    {
        public const float ROCKGRAVITY = -10f; // Gravity
        public Vector2 startPosition;
        public Vector2 currentPosition;
        public Vector2 velocity;
        private Rectangle rect; // Center is 0, 0; NOT upper left.
        protected const int WH = 64; //Size of texture in pixel, Assumes Width == height
        static Texture2D rockTex;

        public Rock(Game game, float startXPos, float startYPos, Vector2 velocity)
            : base(game)
        {
            startPosition = new Vector2(startXPos, startYPos);
            currentPosition = startPosition;
            this.velocity = velocity;
            rect = new Rectangle((int)startXPos,
                (int)startYPos,
                WH,
                WH);
            if (rockTex == null)
            {
                rockTex = Game.Content.Load<Texture2D>("asteroid_cell64_Warspawn_OpenGameArt");
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public void Update(GameTime gameTime, Platform ground)
        {
            if (currentPosition.Y < ground.position.Y)
                this.Dispose();

            currentPosition.X += velocity.X;
            currentPosition.Y += velocity.Y;

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            rect.X = (int)(currentPosition.X - WH / 2);
            rect.Y = (int)(currentPosition.Y + WH / 2);
            rect.Width = WH;
            rect.Height = WH;
            spriteBatch.Draw(rockTex, rect, Color.White);
        }
    }
}
