using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class UpgradePanel: Menu
    {
        private TowerSprite _selectedTower;
        private readonly Texture2D _damageIconTexture;
        private readonly Texture2D _radiusIconTexture;
        private readonly Texture2D _rotateSpeedIconTexture;
        private readonly Texture2D _attackSpeedIconTexture;
        private readonly Texture2D _shellsCountIconTexture;
        private readonly Texture2D _shellsDamageRadiusIconTexture;
        private readonly SpriteFont _font;
        private bool _isStanded;
        private readonly Level _level;
        private readonly BuilderPointer _pointer;

        public TowerSprite SelectedTower
        {
            get { return _selectedTower; }
            set { _selectedTower = value; }
        }

        public bool IsStanded
        {
            get { return _isStanded; }
            set
            {
                _isStanded = value;
                if (_isStanded)
                {
                    Alpha = 1.0f;
                    Enabled = true;
                }
                else
                {
                    Alpha = 0.5f;
                    Enabled = false;
                }
            }
        }

        public UpgradePanel(List<Button> menuButtonList, Texture2D backgroundTexture, Vector2 position, SpriteFont font,
            Texture2D damageIconTexture,
            Texture2D radiusIconTexture,
            Texture2D rotateSpeedIconTexture,
            Texture2D attackSpeedIconTexture,
            Texture2D shellsCountIconTexture,
            Texture2D shellsDamageRadiusIconTexture,
            Level level,
            BuilderPointer pointer)
            : base(menuButtonList, backgroundTexture, position)
        {
            this._font = font;
            this._damageIconTexture = damageIconTexture;
            this._radiusIconTexture = radiusIconTexture;
            this._rotateSpeedIconTexture = rotateSpeedIconTexture;
            this._attackSpeedIconTexture = attackSpeedIconTexture;
            this._shellsCountIconTexture = shellsCountIconTexture;
            this._shellsDamageRadiusIconTexture = shellsDamageRadiusIconTexture;
            this._level = level;
            this._pointer = pointer;
        }

        public override void Update(GameTime gameTime, Rectangle pointerColRect, Game game, out int indexButton)
        {
            if (Enabled)
            {
                if (_selectedTower.Radius >= _selectedTower.MaxRadius)
                {
                    this.MenuButtons[0].IsEnabled = false;
                    this.MenuButtons[0].Update(gameTime);
                }
                else
                    this.MenuButtons[0].IsEnabled = true;
                if (_selectedTower.Damage >= _selectedTower.MaxDamage)
                {
                    this.MenuButtons[1].IsEnabled = false;
                    this.MenuButtons[1].Update(gameTime);
                }
                else
                    this.MenuButtons[1].IsEnabled = true;
                if (_selectedTower.RotateSpeed >= _selectedTower.MaxRotateSpeed)
                {
                    this.MenuButtons[2].IsEnabled = false;
                    this.MenuButtons[2].Update(gameTime);
                }
                else
                    this.MenuButtons[2].IsEnabled = true;
                if (_selectedTower.AttackSpeed >= _selectedTower.MaxAttackSpeed)
                {
                    this.MenuButtons[3].IsEnabled = false;
                    this.MenuButtons[3].Update(gameTime);
                }
                else
                    this.MenuButtons[3].IsEnabled = true;
                if (_selectedTower.OneTimeShellsCount >= _selectedTower.MaxOneTimeShellsCount)
                {
                    this.MenuButtons[4].IsEnabled = false;
                    this.MenuButtons[4].Update(gameTime);
                }
                else
                    this.MenuButtons[4].IsEnabled = true;
                if (_selectedTower.DamageRadius >= _selectedTower.MaxDamageRadius)
                {
                    this.MenuButtons[5].IsEnabled = false;
                    this.MenuButtons[5].Update(gameTime);
                }
                else
                    this.MenuButtons[5].IsEnabled = true;
            }
            if (_pointer.OutText.OutText == "")
            {
                _pointer.OutText.OutText = "";
                if (_isStanded)
                {
                    if (menuButtonList[0].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.RadiusUpgradeCost.ToString() + "$";
                    if (menuButtonList[1].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.DamageUpgradeCost.ToString() + "$";
                    if (menuButtonList[2].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.RotateSpeedUpgradeCost.ToString() + "$";
                    if (menuButtonList[3].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.AttackSpeedUpgradeCost.ToString() + "$";
                    if (menuButtonList[4].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.OneTimeShellsCountUpgradeCost.ToString() + "$";
                    if (menuButtonList[5].IsSelected)
                        _pointer.OutText.OutText = _selectedTower.DamageRadiusUpgradeCost.ToString() + "$";
                }
            }
            else
                if (_pointer.OutText.OutText[0] != 'T')
                {
                    _pointer.OutText.OutText = "";
                    if (_isStanded)
                    {
                        if (menuButtonList[0].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.RadiusUpgradeCost.ToString() + "$";
                        if (menuButtonList[1].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.DamageUpgradeCost.ToString() + "$";
                        if (menuButtonList[2].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.RotateSpeedUpgradeCost.ToString() + "$";
                        if (menuButtonList[3].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.AttackSpeedUpgradeCost.ToString() + "$";
                        if (menuButtonList[4].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.OneTimeShellsCountUpgradeCost.ToString() + "$";
                        if (menuButtonList[5].IsSelected)
                            _pointer.OutText.OutText = _selectedTower.DamageRadiusUpgradeCost.ToString() + "$";
                    }
                }
            base.Update(gameTime, pointerColRect, game, out indexButton);
            if (Enabled)
                _selectedTower.DrawDamageZone = true;
            if (indexButton != -1)
            {
                Enabled = true;
                switch (indexButton)
                {
                    case 0:
                        {
                            if (_level.Money >= _selectedTower.RadiusUpgradeCost)
                            {
                                _selectedTower.IncreaseRadius();
                                _level.Money -= _selectedTower.RadiusUpgradeCost;
                                _selectedTower.RadiusUpgradeCost += (int)(_selectedTower.RadiusUpgradeCost * 0.2);
                            }
                            break;
                        }
                    case 1:
                        {
                            if (_level.Money >= _selectedTower.DamageUpgradeCost)
                            {
                                _selectedTower.IncreaseDamage();
                                _level.Money -= _selectedTower.DamageUpgradeCost;
                                _selectedTower.DamageUpgradeCost += (int)(_selectedTower.DamageUpgradeCost * 0.1);
                            }
                            break;
                        }
                    case 2:
                        {
                            if (_level.Money >= _selectedTower.RotateSpeedUpgradeCost)
                            {
                                _selectedTower.IncreaseRotateSpeed();
                                _level.Money -= _selectedTower.RotateSpeedUpgradeCost;
                                _selectedTower.RotateSpeedUpgradeCost += (int)(_selectedTower.RotateSpeedUpgradeCost * 0.2);
                            }
                            break;
                        }
                    case 3:
                        {
                            if (_level.Money >= _selectedTower.AttackSpeedUpgradeCost)
                            {
                                _selectedTower.IncreaseFireSpeed();
                                _level.Money -= _selectedTower.AttackSpeedUpgradeCost;
                                _selectedTower.AttackSpeedUpgradeCost += (int)(_selectedTower.AttackSpeedUpgradeCost * 0.2);
                            }
                            break;
                        }
                    case 4:
                        {
                            if (_level.Money >= _selectedTower.OneTimeShellsCountUpgradeCost)
                            {
                                _selectedTower.IncreaseOneTimeShellsCount();
                                _level.Money -= _selectedTower.OneTimeShellsCountUpgradeCost;
                                _selectedTower.OneTimeShellsCountUpgradeCost += (int)(_selectedTower.OneTimeShellsCountUpgradeCost * 0.2);
                            }
                            break;
                        }
                    case 5:
                        {
                            if (_level.Money >= _selectedTower.DamageRadiusUpgradeCost)
                            {
                                _selectedTower.IncreaseDamageRadius();
                                _level.Money -= _selectedTower.DamageRadiusUpgradeCost;
                                _selectedTower.DamageRadiusUpgradeCost += (int)(_selectedTower.DamageRadiusUpgradeCost * 0.2);
                            }
                            break;
                        }
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch, Rectangle clientBounds)
        {
            base.Draw(gameTime, spriteBatch, clientBounds);
            if (Enabled)
            {
                //radius
                spriteBatch.Draw(_radiusIconTexture,
                    Position + new Vector2(12, 6),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseRadius.ToString(),
                    Position + new Vector2(42, 9),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                string temp = _selectedTower.BaseRadius.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraRadius.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 9),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                // Damage
                spriteBatch.Draw(_damageIconTexture,
                    Position + new Vector2(12, 36),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseDamage.ToString(),
                    Position + new Vector2(42, 39),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                temp = _selectedTower.BaseDamage.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraDamage.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 39),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                // rotate
                spriteBatch.Draw(_rotateSpeedIconTexture,
                    Position + new Vector2(12, 66),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseRotateSpeed.ToString(),
                    Position + new Vector2(42, 69),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                temp = _selectedTower.BaseRotateSpeed.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraRotateSpeed.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 69),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                // attackSpeed
                spriteBatch.Draw(_attackSpeedIconTexture,
                    Position + new Vector2(12, 96),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseAttackSpeed.ToString(),
                    Position + new Vector2(42, 99),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                temp = _selectedTower.BaseAttackSpeed.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraAttackSpeed.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 99),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                // shellsCount
                spriteBatch.Draw(_shellsCountIconTexture,
                    Position + new Vector2(12, 123),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseOneTimeShellsCount.ToString(),
                    Position + new Vector2(42, 126),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                temp = _selectedTower.BaseOneTimeShellsCount.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraOneTimeShellCount.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 126),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                // damageZone
                spriteBatch.Draw(_shellsDamageRadiusIconTexture,
                    Position + new Vector2(12, 150),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                spriteBatch.DrawString(_font,
                    _selectedTower.BaseDamageRadius.ToString(),
                    Position + new Vector2(42, 153),
                    Color.White,
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
                temp = _selectedTower.BaseDamageRadius.ToString();
                spriteBatch.DrawString(_font,
                    "+" + _selectedTower.ExtraDamageRadius.ToString(),
                    Position + new Vector2(42 + _font.MeasureString(temp).X + 10, 153),
                    new Color(0.0f, 1.0f, 0.0f, 1.0f),
                    0,
                    Vector2.Zero,
                    1.0f,
                    SpriteEffects.None,
                    ZDepth + 0.01f);
            }
        }

    }
}
