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
    public class Vine : Microsoft.Xna.Framework.GameComponent
    {
        Texture2D vine;
        Texture2D vineTop;
        Texture2D vineBottom;
        public Rectangle vineRect;
        public Vector2 position;
        int height, width;
        public int lane;

        public Vine(Game game, int y, int heightUnits, int lane)
            : base(game)
        {
            position.X = 100 * lane + 75 ;
            position.Y = y;
            height = heightUnits * 32;
            width = 32;
            this.lane = lane;
        }


        public override void Initialize()
        {
            vine = Game.Content.Load<Texture2D>("vine");
            vineTop = Game.Content.Load<Texture2D>("vineTop");
            vineBottom = Game.Content.Load<Texture2D>("vineBottom");
            vineRect = new Rectangle((int)position.X, (int)position.Y, width, height);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            Rectangle drawRect = new Rectangle(vineRect.X, vineRect.Y + (int)offset.Y, vineRect.Width, vineRect.Height);
            
            spriteBatch.Draw(vine, drawRect, new Rectangle(0, 0, vineRect.Width, vineRect.Height), Color.White);
            spriteBatch.Draw(vineTop, new Vector2(drawRect.X, drawRect.Y - 8), Color.White);
            spriteBatch.Draw(vineBottom, new Vector2(drawRect.X, drawRect.Y + drawRect.Height), Color.White);

        }
    }
}
