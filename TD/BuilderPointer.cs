using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class BuilderPointer: Pointer
    {
        private readonly Rectangle _fieldRectangle;
        private TowerSprite _pickedTower;
        private UpgradePanel _upgradePanel;
        private readonly Text _text;

        public TowerSprite PickedTower
        {
            get { return _pickedTower; }
            set { _pickedTower = value; }
        }

        public UpgradePanel UpgradePanel
        {
            get { return _upgradePanel; }
            set { _upgradePanel = value; }
        }

        public Text OutText
        {
            get { return _text; }
        }

        public BuilderPointer(Texture2D textureImage, string clickSoundName, Vector2 position,
            Point frameSize, Point currentFrame, Point sheetSize, int millisecondsPerFrame, Rectangle fieldRectangle, SpriteFont font)
            : base(textureImage, position,
            frameSize, currentFrame, sheetSize, millisecondsPerFrame)
        {
            _pickedTower = null;
            this._fieldRectangle = fieldRectangle;
            _text = new Text(position, font);
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            base.Update(gameTime, clientBounds);
            if (_pickedTower != null)
                if (CollisionRect.Intersects(_fieldRectangle))
                    _pickedTower.Position = new Vector2(((float)Math.Truncate(position.X / 12)) * 12, 
                        ((float)Math.Truncate(position.Y / 12)) * 12);
                else
                    _pickedTower.Position = position;
            if (_upgradePanel.Enabled && !_upgradePanel.IsStanded)
            {
                Vector2 temp = position + new Vector2(21f, 21f) + _upgradePanel.Size;
                Vector2 newPosition = position;
                if (temp.X > 768)
                    newPosition.X = newPosition.X - _upgradePanel.Size.X;
                else
                    newPosition.X = newPosition.X + 21f;
                if (temp.Y > 768)
                    newPosition.Y = newPosition.Y - _upgradePanel.Size.Y;
                else
                    newPosition.Y = newPosition.Y + 21f;
                _upgradePanel.Position = newPosition;
            }
            if (_text.OutText != "")
                _text.Position = new Vector2(position.X + 16, position.Y);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (_pickedTower != null)
                _pickedTower.Draw(gameTime, spriteBatch);
            else
                base.Draw(gameTime, spriteBatch);
            if (_text.OutText != "")
                _text.Draw(spriteBatch);
        }

    }
}
