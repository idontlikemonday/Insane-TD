using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class SpecialEffect
    {
        private readonly Texture2D _texture;
        private readonly string _soundEffectName;
        private Point _sheetSize;
        private Point _frameSize;
        private Point _currentFrame;
        private readonly Color _color;
        private readonly int _millisecondsPerFrame;
        private readonly int _originOffsetX;
        private readonly int _originOffsetY;
        private int _timeSinceLastFrame;
        private readonly Point _startPoint;

        public Vector2 Position { get; set; }
        public float Angle { get; set; }
        public float Depth { get; set; }
        public bool IsEnabled { get; set; }


        public SpecialEffect(Texture2D texture,
            string soundEffectName,
            Point sheetSize,
            Point frameSize,
            Point currentFrame,
            Color color,
            Vector2 position,
            int millisecondsPerFrame,
            int originOffsetX,
            int originOffsetY,
            float angle,
            float depth,
            bool isEnabled)
        {
            this._texture = texture;
            this._soundEffectName = soundEffectName;
            this._sheetSize = sheetSize;
            this._frameSize = frameSize;
            this._currentFrame = currentFrame;
            this._color = color;
            this.Position = position;
            this._millisecondsPerFrame = millisecondsPerFrame;
            this._originOffsetX = originOffsetX;
            this._originOffsetY = originOffsetY;
            this.Angle = angle;
            this.Depth = depth;
            this.IsEnabled = isEnabled;
            this._startPoint = currentFrame;
        }

        public SpecialEffect(SpecialEffect effect)
        {
            this._texture = effect._texture;
            this._soundEffectName = effect._soundEffectName;
            this._sheetSize = effect._sheetSize;
            this._frameSize = effect._frameSize;
            this._currentFrame = effect._currentFrame;
            this._color = effect._color;
            this.Position = effect.Position;
            this._millisecondsPerFrame = effect._millisecondsPerFrame;
            this._originOffsetX = effect._originOffsetX;
            this._originOffsetY = effect._originOffsetY;
            this.Angle = effect.Angle;
            this.Depth = effect.Depth;
            this.IsEnabled = effect.IsEnabled;
            this._startPoint = effect._currentFrame;
        }

        public void Update(GameTime gameTime)
        {
            _timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (IsEnabled)
            {
                if (_timeSinceLastFrame > _millisecondsPerFrame)
                {
                    _timeSinceLastFrame = 0;
                    ++_currentFrame.X;
                    if (_currentFrame.X == _sheetSize.X)
                    {
                        _currentFrame.X = 0;
                        ++_currentFrame.Y;
                        if (_currentFrame.Y == _sheetSize.Y)
                        {
                            IsEnabled = false;
                            _currentFrame = _startPoint;
                        }
                    }
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (IsEnabled)
            {
                spriteBatch.Draw(_texture,
                    Position,
                    new Rectangle(_currentFrame.X * _frameSize.X,
                        _currentFrame.Y * _frameSize.Y,
                        _frameSize.X, _frameSize.Y),
                        _color,
                        Angle,
                        new Vector2(_frameSize.X / 2.0f - _originOffsetX, _frameSize.Y / 2.0f - _originOffsetY),
                    1f,
                    SpriteEffects.None,
                    Depth);
            }
        }

        public void Run(Game game)
        {
            if (_soundEffectName != null)
                game.PlayCue(_soundEffectName);
            IsEnabled = true;
        }

        public void Stop()
        {
            IsEnabled = false;
            _currentFrame.X = 0;
            _currentFrame = _startPoint;
            _timeSinceLastFrame = 0;
        }

        public void SetPosition(Vector2 pos)
        {
            this.Position = pos;
        }

    }
}
