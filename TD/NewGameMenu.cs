using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class NewGameMenu: Menu
    {
        private string _playerName;
        private Vector2 _headerParam;
        private Vector2 _playerNameParam;
        private readonly KeyboardInput _keyboard;

        public Texture2D TextureInputField;
        public SpriteFont NameFont;

        public NewGameMenu(List<Button> menuButtonList, Texture2D backgroundTexture, Vector2 position)
            : base(menuButtonList, backgroundTexture, position)
        {
            TextureInputField = null;
            NameFont = null;
            _keyboard = new KeyboardInput();
            _playerName = "New Player";
        }

        public override void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton)
        {
            if (Enabled)
            {
                _headerParam = NameFont.MeasureString("ENTER YOUR NAME");
                _playerNameParam = NameFont.MeasureString(_playerName);
                string symbol = _keyboard.ReadKey();
                if (symbol == "BACKSPACE")
                {
                    if (_playerName.Length != 0)
                        _playerName = _playerName.Remove(_playerName.Length - 1);
                }
                else
                    _playerName = _playerName.Insert(_playerName.Length, symbol);
            }
            base.Update(gameTime, pointerColRect, game, out indexButton);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            base.Draw(gameTime, spriteBatch, clientBounds);
            if (Enabled)
            {
                if (TextureInputField != null)
                    spriteBatch.Draw(TextureInputField,
                    new Vector2(clientBounds.Width / 2, clientBounds.Height / 2),
                    null,
                    Color.White,
                    0,
                    new Vector2(TextureInputField.Width / 2, TextureInputField.Height / 2),
                    1f,
                    SpriteEffects.None,
                    0.9f);
                spriteBatch.DrawString(NameFont,
                    _playerName,
                    new Vector2(clientBounds.Width / 2, clientBounds.Height / 2),
                    Color.DarkBlue,
                    0,
                    new Vector2(_playerNameParam.X / 2, _playerNameParam.Y / 2),
                    1,
                    SpriteEffects.None,
                    1);
                spriteBatch.DrawString(NameFont,
                    "ENTER YOUR NAME",
                    new Vector2(clientBounds.Width / 2, 50),
                    Color.DarkBlue,
                    0,
                    new Vector2(_headerParam.X / 2, _headerParam.Y / 2),
                    3,
                    SpriteEffects.None,
                    1);
            }
        }

    }
}
