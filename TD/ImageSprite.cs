using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class ImageSprite : Sprite
    {
        public ImageSprite(Texture2D textureImage, Vector2 position)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = new Point(textureImage.Width, textureImage.Height);
            this.sheetSize = new Point(1, 1);
            this.currentFrame = new Point(0, 0);
            this.Speed = new Vector2(0, 0);
        }

        public override Vector2 Direction
        {
            get { return Speed; }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            base.Draw(gameTime, spriteBatch);
        }
    }
}
