using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class Menu
    {
        protected List<Button> menuButtonList;
        private readonly Texture2D _backgroundTexture;
        private ButtonState _previousLeftButtonState;
        private Vector2 _position;
        private float _alpha;
        private float _zDepth;
        private bool _enabled = false;

        public Vector2 Position
        {
            get { return _position; }
            set
            {
                _position = value;
                foreach (Button button in menuButtonList)
                    button.Position = _position + button.LocalPosition;
            }
        }

        public float Alpha
        {
            get { return _alpha; }
            set
            {
                _alpha = value;
                foreach (Button button in menuButtonList)
                    button.Alpha = _alpha;
            }
        }

        public float ZDepth
        {
            get { return _zDepth; }
            set
            {
                _zDepth = value;
                foreach (Button button in menuButtonList)
                    button.ZDepth = _zDepth + 0.05f;
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set { _enabled = value; }
        }

        public Vector2 Size
        {
            get { return new Vector2(_backgroundTexture.Width, _backgroundTexture.Height); }
        }

        public Rectangle CollisionRectangle
        {
            get
            {
                if (Enabled)
                    return new Rectangle((int)_position.X, (int)_position.Y,
                        _backgroundTexture.Width, _backgroundTexture.Height);
                else
                    return new Rectangle(-1, -1,
                        0, 0);
            }
        }

        public List<Button> MenuButtons
        {
            get { return menuButtonList; }
        }

        public Menu(List<Button> menuButtonList, Texture2D backgroundTexture, Vector2 position)
        {
            this.menuButtonList = menuButtonList;
            this._backgroundTexture = backgroundTexture;
            this._position = position;
            this._previousLeftButtonState = ButtonState.Released;
            foreach (Button button in menuButtonList)
            {
                button.Position = position + button.LocalPosition;
            }
            _alpha = 1.0f;
        }

        public virtual void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton)
        {
            int temp = -1;
            if (_enabled)
            {
                foreach (Button button in menuButtonList)
                {
                    if (button.IsEnabled)
                    {
                        button.Update(gameTime);
                        if (button.CollisionRectangle.Intersects(pointerColRect))
                        {
                            if (!button.IsEnabled && !button.IsClicked)
                                game.PlayCue(button.OnSelectSoundName);
                            button.IsSelected = true;
                        }
                        else
                            button.IsSelected = false;
                        if ((Mouse.GetState().LeftButton == ButtonState.Pressed) &&
                            (_previousLeftButtonState == ButtonState.Released) &&
                            button.IsSelected)
                        {
                            if (!button.IsClicked)
                                game.PlayCue(button.OnClickSoundName);
                            button.IsClicked = true;
                        }
                        if (Mouse.GetState().LeftButton == ButtonState.Released)
                        {
                            button.IsClicked = false;
                            if (button.IsSelected && (_previousLeftButtonState == ButtonState.Pressed))
                            {
                                temp = menuButtonList.IndexOf(button);
                                _enabled = false;
                            }
                        }
                    }
                }
                _previousLeftButtonState = Mouse.GetState().LeftButton;
            }
            indexButton = temp;
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            if (_enabled)
            {
                if (_backgroundTexture != null)
                {
                    spriteBatch.Draw(_backgroundTexture, _position,
                        null,
                        new Color(1.0f, 1.0f, 1.0f, _alpha),
                        0,
                        Vector2.Zero,
                        1.0f,
                        SpriteEffects.None,
                        _zDepth);
                }
                foreach (Button button in menuButtonList)
                    button.Draw(gameTime, spriteBatch);
            }
        }

    }
}