using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class Interface: Menu
    {
        public Texture2D TexturePanelTool;
        private readonly SpriteFont _font;
        private readonly Level _level;
        private readonly int _maxWaves = 0;
        private readonly Texture2D _money;
        private readonly BuilderPointer _pointer;
        private readonly TowerSprite _black;
        private readonly TowerSprite _red;
        private readonly TowerSprite _green;
        private readonly TowerSprite _blue;
        private readonly TowerSprite _yellow;

        public Interface(List<Button> menuButtonList, Texture2D backgroundTexture, Vector2 position, SpriteFont font, Level level, Texture2D money,
            BuilderPointer pointer,
            TowerSprite black,
            TowerSprite red,
            TowerSprite green,
            TowerSprite blue,
            TowerSprite yellow)
            : base(menuButtonList, backgroundTexture, position)
        {
            TexturePanelTool = null;
            this._font = font;
            this._level = level;
            this._maxWaves = level.Waves.Count - 1;
            this._money = money;
            this._pointer = pointer;
            this._black = black;
            this._red = red;
            this._green = green;
            this._blue = blue;
            this._yellow = yellow;
        }

        public override void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton)
        {
            base.Update(gameTime, pointerColRect, game, out indexButton);
            _pointer.OutText.OutText = "";
            if (menuButtonList[0].IsSelected)
                _pointer.OutText.OutText = "T " + _black.Cost.ToString() + "$";
            if (menuButtonList[1].IsSelected)
                _pointer.OutText.OutText = "T " + _red.Cost.ToString() + "$";
            if (menuButtonList[2].IsSelected)
                _pointer.OutText.OutText = "T " + _green.Cost.ToString() + "$";
            if (menuButtonList[3].IsSelected)
                _pointer.OutText.OutText = "T " + _blue.Cost.ToString() + "$";
            if (menuButtonList[4].IsSelected)
                _pointer.OutText.OutText = "T " + _yellow.Cost.ToString() + "$";
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            base.Draw(gameTime, spriteBatch, clientBounds);
            if (Enabled)
            {
                if (TexturePanelTool != null)
                {
                    spriteBatch.Draw(TexturePanelTool,
                        new Vector2(768, 0),
                        null,
                        new Color(1.0f, 1.0f, 1.0f, 0.7f),
                        0,
                        Vector2.Zero,
                        1f,
                        SpriteEffects.None,
                        0.8f);
                }
                spriteBatch.Draw(_money,
                            new Vector2(800, 510),
                            null,
                            Color.White,
                            0,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.8f);
                spriteBatch.DrawString(_font,
                    _level.Money.ToString(),
                    new Vector2(825, 515),
                    Color.DarkBlue,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0.81f);
                spriteBatch.DrawString(_font,
                    "ВОЛНА " + Math.Min(_maxWaves, _maxWaves - _level.Waves.Count + 1) + " из " + (_maxWaves),
                    new Vector2(800, 550),
                    Color.DarkBlue,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0.81f);

                spriteBatch.DrawString(_font,
                    "СЛЕДУЮЩАЯ ВОЛНА:",
                    new Vector2(800, 600),
                    Color.DarkBlue,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0.81f);

                if (_level.Waves.Count > 1)
                    spriteBatch.Draw(_level.Waves[1].Monster,
                            new Vector2(950, 590),
                            null,
                            Color.White,
                            0,
                            Vector2.Zero,
                            1f,
                            SpriteEffects.None,
                            0.8f);

                spriteBatch.DrawString(_font,
                   "Количество жизней:",
                   new Vector2(800, 490),
                   Color.DarkBlue,
                   0,
                   Vector2.Zero,
                   1f,
                   SpriteEffects.None,
                   0.81f);
                spriteBatch.DrawString(_font,
                   _level.Lifes.ToString(),
                   new Vector2(950, 490),
                   Color.DarkBlue,
                   0,
                   Vector2.Zero,
                   1f,
                   SpriteEffects.None,
                   0.81f);
            }
        }
    }
}
