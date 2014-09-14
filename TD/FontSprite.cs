using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class FontSprite
    {
        protected SpriteFont spriteFont;
        protected String stringMessage;
        protected Color fontColor;
        protected Vector2 position;
        protected Vector2 finalPosition;
        protected Vector2 origin;
        protected Vector2 speed;

        public FontSprite(SpriteFont spriteFont, String stringMessage,
            Color fontColor, Vector2 position, Vector2 finalPosition,
            Vector2 origin, Vector2 speed)
        {
            this.spriteFont = spriteFont;
            this.stringMessage = stringMessage;
            this.fontColor = fontColor;
            this.position = position;
            this.finalPosition = finalPosition;
            this.origin = origin;
            this.speed = speed;
        }

        public Vector2 Direction
        {
            get { return speed; }
            set { speed = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public void Update(GameTime gameTime)
        {
            position += Direction;
            if ((Math.Abs((position - finalPosition).X) < Math.Abs(Direction.X))
                || (Math.Abs((position - finalPosition).Y) < Math.Abs(Direction.Y))
                || (position == finalPosition))
            {
                position = finalPosition;
                speed = Vector2.Zero;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(spriteFont,
                stringMessage,
                position,
                fontColor,
                0f,
                new Vector2(origin.X / 2, origin.Y / 2),
                1,
                SpriteEffects.None,
                0.5f);
        }
    }
}
