using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class AboutMenu : Menu
    {
        private readonly List<double> _elapsedTimesList = new List<double>();

        public List<FontSprite> FontSpriteListContent = new List<FontSprite>();
        public List<FontSprite> FontSpriteList = new List<FontSprite>();
        public double ElapsedTime;
        public int CurrentLineToGo = 0;

        public AboutMenu(List<Button> menuButtonList,
            Texture2D backgroundTexture,
            Vector2 position,
            List<FontSprite> fontSpriteListContent,
            List<double> elapsedTimesList)
            : base(menuButtonList, backgroundTexture, position)
        {
            this.FontSpriteListContent = fontSpriteListContent;
            this._elapsedTimesList = elapsedTimesList;
        }

        public override void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton)
        {
            if (Enabled)
            {
                ElapsedTime += gameTime.ElapsedGameTime.Milliseconds;

                if (_elapsedTimesList.Count > CurrentLineToGo)
                {
                    if (ElapsedTime > _elapsedTimesList[CurrentLineToGo])
                    {
                        FontSpriteList.Add(FontSpriteListContent[CurrentLineToGo]);
                        CurrentLineToGo++;
                    }
                }

                foreach (FontSprite s in FontSpriteList)
                {
                    s.Update(gameTime);
                }
            }

            base.Update(gameTime, pointerColRect, game, out indexButton);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            base.Draw(gameTime, spriteBatch, clientBounds);

            if (Enabled)
            {
                foreach (FontSprite s in FontSpriteList)
                {
                    s.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
