using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class Text
    {
        private string _text;
        private Vector2 _position;
        private readonly SpriteFont _font;

        public Vector2 Position
        {
            set { _position = value; }
        }

        public string OutText
        {
            get { return _text; }
            set { _text = value; }
        }

        public Text(Vector2 pos, SpriteFont font)
        {
            _text = "";
            _position = pos;
            this._font = font;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(_font,
                    _text,
                    _position,
                    Color.Yellow,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    1.0f);
        }

    }
}
