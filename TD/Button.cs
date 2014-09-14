using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class Button
    {
        private Vector2 _position;
        private readonly Vector2 _localPosition;
        private readonly int _downOffset;
        private readonly int _rightOffset;
        private bool _isEnabled;
        private bool _isSelected;
        private bool _isClicked;
        private float _alpha;
        private float _zDepth;
        private Texture2D _texture;
        private readonly Texture2D _textureImageDisabledButton;
        private readonly Texture2D _textureImageSelectedButton;
        private readonly Texture2D _textureImageClickedButton;
        private readonly Texture2D _textureImageButton;
        private readonly Texture2D _textureImageButtonShadow;

        public string OnSelectSoundName;
        public string OnClickSoundName;

        public Rectangle CollisionRectangle
        {
            get
            {
                return (new Rectangle((int)_position.X, (int)_position.Y,
                    _texture.Width - _rightOffset, _texture.Height - _downOffset));
            }
        }

        public Vector2 Position
        {
            set { _position = value; }
        }

        public float ZDepth
        {
            set { _zDepth = value; }
        }

        public Vector2 LocalPosition
        {
            get { return _localPosition; }
        }

        public float Alpha
        {
            set { _alpha = value; }
        }

        public bool IsClicked
        {
            get { return _isClicked; }
            set { _isClicked = value; }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; }
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; }
        }

        public Button(Texture2D textureImageDisabledButton,
            Texture2D textureImageSelectedButton,
            Texture2D textureImageClickedButton,
            Texture2D textureImageButton,
            Texture2D textureImageButtonShadow,
            string onSelectSoundName,
            string onClickSoundName,
            Vector2 position,
            int downOffset,
            int rightOffset,
            bool isEnabled)
        {
            this._textureImageDisabledButton = textureImageDisabledButton;
            this._textureImageSelectedButton = textureImageSelectedButton;
            this._textureImageClickedButton = textureImageClickedButton;
            this._textureImageButton = textureImageButton;
            this._textureImageButtonShadow = textureImageButtonShadow;
            this.OnSelectSoundName = onSelectSoundName;
            this.OnClickSoundName = onClickSoundName;
            this._position = position;
            this._localPosition = position;
            this._downOffset = downOffset;
            this._rightOffset = rightOffset;
            this._isEnabled = isEnabled;
            this._isSelected = false;
            this._isClicked = false;
            this._texture = textureImageDisabledButton;
            this._alpha = 1.0f;
            this._zDepth = 0.9f;
        }

        public void Update(GameTime gameTime)
        {
            if (!_isEnabled)
                _texture = _textureImageDisabledButton;
            else
            {
                if (!_isSelected)
                    _texture = _textureImageButton;
                if (_isSelected)
                    _texture = _textureImageSelectedButton;
                if (_isClicked)
                    _texture = _textureImageClickedButton;
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture,
                _position,
                null,
                new Color(1.0f, 1.0f, 1.0f, _alpha), 
                0, 
                Vector2.Zero,
                1f, 
                SpriteEffects.None, 
                _zDepth);
            if (_isEnabled)
            {
                spriteBatch.Draw(_textureImageButtonShadow,
                    _position,
                    null,
                    new Color(1.0f, 1.0f, 1.0f, 0.7f),
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    _zDepth - 0.01f);
            }
        }
    }
}
