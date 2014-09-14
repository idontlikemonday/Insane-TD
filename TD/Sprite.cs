using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    abstract class Sprite
    {
        protected Texture2D textureImage;
        protected Vector2 position;
        protected Point frameSize;
        protected int collisionOffset;
        protected Point currentFrame;
        protected Point sheetSize;
        int _timeSinceLastFrame = 0;
        readonly int _millisecondsPerFrame;
        protected Vector2 Speed;
        const int DefaultMillisecondsPerFrame = 16;
        
        public string CollisionCueName { get; private set; }

        public abstract Vector2 Direction { get; }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Texture2D TextureImage
        {
            get { return textureImage; }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                return new Rectangle(
                    (int)position.X + collisionOffset,
                    (int)position.Y + collisionOffset,
                    frameSize.X - (collisionOffset * 2),
                    frameSize.Y - (collisionOffset * 2));
            }
        }

        protected Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            string collisionCueName)
            : this(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, DefaultMillisecondsPerFrame, collisionCueName)
        {
        }

        protected Sprite()
        {
        }

        protected Sprite(Texture2D textureImage, Vector2 position, Point frameSize,
            int collisionOffset, Point currentFrame, Point sheetSize, Vector2 speed,
            int millisecondsPerFrame, string collisionCueName)
        {
            this.textureImage = textureImage;
            this.position = position;
            this.frameSize = frameSize;
            this.collisionOffset = collisionOffset;
            this.currentFrame = currentFrame;
            this.sheetSize = sheetSize;
            this.Speed = speed;
            this.CollisionCueName = collisionCueName;
            this._millisecondsPerFrame = millisecondsPerFrame;
        }

        protected Sprite(Sprite sprite)
        {
            this.textureImage = sprite.textureImage;
            this.position = sprite.position;
            this.frameSize = sprite.frameSize;
            this.collisionOffset = sprite.collisionOffset;
            this.currentFrame = sprite.currentFrame;
            this.sheetSize = sprite.sheetSize;
            this.Speed = sprite.Speed;
            this.CollisionCueName = sprite.CollisionCueName;
            this._millisecondsPerFrame = sprite._millisecondsPerFrame;
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _millisecondsPerFrame)
            {
                _timeSinceLastFrame = 0;
                ++currentFrame.X;
                if (currentFrame.X >= sheetSize.X)
                {
                    currentFrame.X = 0;
                    ++currentFrame.Y;
                    if (currentFrame.Y >= sheetSize.Y)
                        currentFrame.Y = 0;
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                Color.White, 0, Vector2.Zero,
                1f, SpriteEffects.None, 0);
        }

    }
}
