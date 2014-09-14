using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class HelpMenu : Menu
    {
        public int CurrentList = 0;
        public int MaxList = 1;
        public List<List<FontSprite>> FontSpriteList = new List<List<FontSprite>>();
        public List<List<ImageSprite>> ImageSpriteList = new List<List<ImageSprite>>();

        public HelpMenu(List<Button> menuButtonList,
            Texture2D backgroundTexture,
            Vector2 position,
            List<List<FontSprite>> fontSpriteList,
            List<List<ImageSprite>> imageSpriteList,
            int maxList)
            : base(menuButtonList, backgroundTexture, position)
        {
            this.FontSpriteList = fontSpriteList;
            this.ImageSpriteList = imageSpriteList;
            this.MaxList = maxList;
        }

        public void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton, int currentList)
        {
            if (Enabled)
            {
                for (int i = 0; i < FontSpriteList[currentList].Count; ++i)
                {
                    FontSprite s = FontSpriteList[currentList][i];
                    s.Update(gameTime);
                }
                if (currentList == 0)
                {
                    menuButtonList[1].IsEnabled = false;
                    menuButtonList[1].Update(gameTime);
                }
                else
                {
                    menuButtonList[1].IsEnabled = true;
                }

                if (currentList == MaxList - 1)
                {
                    menuButtonList[2].IsEnabled = false;
                    menuButtonList[2].Update(gameTime);
                }
                else
                {
                    menuButtonList[2].IsEnabled = true;
                }
                
            }

            base.Update(gameTime, pointerColRect, game, out indexButton);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds, int currentList)
        {
            base.Draw(gameTime, spriteBatch, clientBounds);

            if (Enabled)
            {
                foreach (FontSprite s in FontSpriteList[currentList])
                {
                    s.Draw(gameTime, spriteBatch);
                }
                foreach (ImageSprite s in ImageSpriteList[currentList])
                {
                    s.Draw(gameTime, spriteBatch);
                }
            }
        }
    }
}
