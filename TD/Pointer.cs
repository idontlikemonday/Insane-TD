using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class Pointer
    {
        private readonly Texture2D _textureImage;
        private Point _currentFrame;
        private Point _sheetSize;
        private int _timeSinceLastFrame = 0;
        private readonly int _millisecondsPerFrame;
        protected Vector2 position;
        protected List<Vector2> oldPositionList;
        protected List<int> copyLifeTimeList;
        protected Point frameSize;

        public Rectangle CollisionRect
        {
            get
            {
                return new Rectangle((int)position.X, (int)position.Y, 1, 1);
            }
        }

        public Pointer(Texture2D textureImage, Vector2 position, Point frameSize,
            Point currentFrame, Point sheetSize, int millisecondsPerFrame)
        {
            this._textureImage = textureImage;
            this.position = position;
            this.oldPositionList = new List<Vector2>();
            this.copyLifeTimeList = new List<int>();
            this.frameSize = frameSize;
            this._currentFrame = currentFrame;
            this._sheetSize = sheetSize;
            this._millisecondsPerFrame = millisecondsPerFrame;
            Mouse.SetPosition((int)position.X, (int)position.Y);
        }

        public virtual void Update(GameTime gameTime, Rectangle clientBounds)
        {
            Vector2 oldPosition = position;
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (_timeSinceLastFrame > _millisecondsPerFrame)
            {
                _timeSinceLastFrame = 0;
                ++_currentFrame.X;
                if (_currentFrame.X >= _sheetSize.X)
                {
                    _currentFrame.X = 0;
                    ++_currentFrame.Y;
                    if (_currentFrame.Y >= _sheetSize.Y)
                        _currentFrame.Y = 0;
                }
            }
            position.X = Mouse.GetState().X;
            position.Y = Mouse.GetState().Y;
            if (position.X < 0)
                position.X = 0;
            if (position.Y < 0)
                position.Y = 0;
            if (position.X > clientBounds.Width)
                position.X = clientBounds.Width;
            if (position.Y > clientBounds.Height)
                position.Y = clientBounds.Height;
            if ((position.X != oldPosition.X) ||
                    (position.Y != oldPosition.Y))
            {
                oldPositionList.Add(position);
                copyLifeTimeList.Add(70);
            }
            if (oldPositionList.Count > 0)
            {
                for (int i = 0; i < copyLifeTimeList.Count; i++ )
                    copyLifeTimeList[i] -= gameTime.ElapsedGameTime.Milliseconds;
                if (copyLifeTimeList[0] <= 0)
                {
                    oldPositionList.RemoveAt(0);
                    copyLifeTimeList.RemoveAt(0);
                }
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textureImage,
                position,
                new Rectangle(_currentFrame.X * frameSize.X,
                    _currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    1.0f);
            foreach (Vector2 oldPosition in oldPositionList)
                spriteBatch.Draw(_textureImage,
                oldPosition,
                new Rectangle(_currentFrame.X * frameSize.X,
                    _currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0.99f);
        }
    }
}
