using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class ShellSprite : Sprite
    {
        private readonly MonsterSprite _goal;
        private readonly List<MonsterSprite> _monsterSpriteList;
        private float _alpha;

        private readonly Boolean _isArmor = false;
        private readonly Boolean _isIce = false;
        private readonly Boolean _isPoison = false;

        private readonly double _poisonDamagePerSecond = 0;
        private readonly double _poisonPeriod = 0;
        private readonly float _freeze = 0;
        private readonly double _frozenPeriod = 0;
        private readonly int _decArmor = 0;
        private readonly double _damageRadius;
        private readonly float _damage;

        public bool IsFly { get; set; }

        public override Vector2 Direction
        {
            get { return Speed; }
        }

        protected bool Collide()
        {
            return _goal.CollisionRectangleMonster.Contains((int)position.X, (int)position.Y);
        }

        public ShellSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, string collisionCueName, MonsterSprite goal, float damage, Boolean isArmor, Boolean isIce, Boolean isPoison, float duration,int ratio,
            double damageRadius, List<MonsterSprite> MSL)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName)
        {
            this._goal = goal;
            this._damage = damage;
            this.IsFly = true;
            this._isArmor = isArmor;
            this._isIce = isIce;
            this._isPoison = isPoison;
            if (this._isIce) 
            {
                _freeze = duration;
                _frozenPeriod = ratio;
            }
            if (this._isPoison) 
            {
                _poisonDamagePerSecond = duration;
                _poisonPeriod = ratio;
            }
            if (this._isArmor)
            {
                this._decArmor = ratio;
            }
            this._damageRadius = damageRadius;
            this._monsterSpriteList = MSL;
        }

        public ShellSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, string collisionCueName, MonsterSprite goal,
            float damage, Boolean isArmor, Boolean isIce, Boolean isPoison)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, collisionCueName)
        {
            this._goal = goal;
            this._damage = damage;
            this.IsFly = true;
            this._isArmor = isArmor;
            this._isIce = isIce;
            this._isPoison = isPoison;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            var arMon = new Vector2(0, 0);

            if (_goal == null)
            {
                IsFly = false;
            }
            else
            {
                if (_goal.IsAlive == false)
                {
                    IsFly = false;
                }

                arMon.X = -((position.X) - (_goal.CollisionRectangleMonster.Center.X));
                arMon.Y = -((position.Y) - (_goal.CollisionRectangleMonster.Center.Y));

                float speedT = Math.Min(Speed.X, arMon.Length());

                arMon.Normalize();

                _alpha = (float)Math.Acos(arMon.X / arMon.Length());

                if (arMon.Y < 0)
                    _alpha = (float)(2 * Math.PI - _alpha);

                position.X += speedT * arMon.X;
                position.Y += speedT * arMon.Y;

                if (Collide())
                {
                    _goal.HitPointsCurrent = _goal.HitPointsCurrent + ((_goal.Armor - _damage < 0) ? (_goal.Armor - _damage) : (-1));
                    if (_damageRadius > 0.0)
                        foreach (MonsterSprite s in _monsterSpriteList)
                        {
                            double tempD = Math.Sqrt(Math.Pow(Position.X - s.Position.X, 2) + Math.Pow(Position.Y - s.Position.Y, 2));
                            if (tempD <= _damageRadius)
                                s.HitPointsCurrent = s.HitPointsCurrent + ((_goal.Armor - _damage < 0) ? (_goal.Armor - _damage) : (-1));
                        }
                    if (_goal.HitPointsCurrent < 0)
                        _goal.HitPointsCurrent = 0;
                    if (_isArmor)
                    {
                        _goal.Armor -= _decArmor;
                    }
                    if (_isIce)
                    {
                        _goal.Freeze = _freeze;
                        _goal.IsFrozen = true;
                        _goal.FrozenTime = gameTime.TotalGameTime.TotalSeconds;
                        _goal.FrozenPeriod = _frozenPeriod;
                    }
                    if (_isPoison)
                    {
                        _goal.PoisonDamagePerSecond = (int) (_poisonDamagePerSecond * _damage);
                        _goal.PoisonedPeriod = _poisonPeriod;
                        _goal.PoisonedTime = gameTime.TotalGameTime.TotalSeconds;
                        _goal.IsPoisoned = true;
                    }
                    IsFly = false;

                }
            }
            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureImage,
                position,
                null,
                Color.White,
                _alpha,
                new Vector2(textureImage.Width / 2, textureImage.Height / 2),
                1,
                SpriteEffects.None,
                0.51f);
        }
    }
}
