using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class Timer
    {
        private readonly int _t;
        private int _startTime;
        private int _currentTime;

        public bool IsRunning { get; set; }

        public Timer(int T)
        {
            this._t = T;
            _currentTime = T;
            IsRunning = false;
        }

        public int CurrentTime
        {
            get
            {
                return _currentTime;
            }
            set
            {
                _currentTime = value;
            }
        }

        public void Update(GameTime gameTime)
        {
            if (IsRunning)
            {
                if (_currentTime == 0)
                    IsRunning = false;
                else
                {
                    _currentTime -= (int)gameTime.TotalGameTime.TotalSeconds - _startTime;
                    _startTime = (int)gameTime.TotalGameTime.TotalSeconds;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            spriteBatch.DrawString(font,
                    "ВРЕМЯ ДО СЛЕДУЮЩЕЙ ВОЛНЫ \n " + "                " + _currentTime.ToString(),
                    new Vector2(790, 700),
                    Color.DarkBlue,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    1);
        }

        public void Run(GameTime gameTime)
        {
            _startTime = (int)gameTime.TotalGameTime.TotalSeconds;
            _currentTime = _t;
            IsRunning = true;
        }

    }
}
