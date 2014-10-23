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

        private Vector2 _shellFlyingVector = new Vector2(0, 0);

        public bool IsFly { get; set; }

        public override Vector2 Direction
        {
            get { return Speed; }
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
            if (_goal == null || _goal.IsAlive == false)
            {
                IsFly = false;
            }
            else
            {
                _shellFlyingVector.X = -((position.X) - (_goal.CollisionRectangleMonster.Center.X));
                _shellFlyingVector.Y = -((position.Y) - (_goal.CollisionRectangleMonster.Center.Y));

                float speedT = Math.Min(Speed.X, _shellFlyingVector.Length());

                _shellFlyingVector.Normalize();

                UpdateAlpha();
                UpdatePosition(speedT);

                if (Collide())
                {
                    var damageToDeal = _damage > _goal.Armor ? _damage - _goal.Armor : 1;
                    _goal.HitPointsCurrent -= damageToDeal;
                    if (_damageRadius > 0.0)
                    {
                        DealDamageByAoe(damageToDeal);
                    }
                    ApplySpecialEffectsToGoal(gameTime);
                    IsFly = false;
                }
            }
            base.Update(gameTime, clientBounds);
        }

        private void UpdateAlpha()
        {
            _alpha = (float) Math.Acos(_shellFlyingVector.X/_shellFlyingVector.Length());

            if (_shellFlyingVector.Y < 0)
            {
                _alpha = (float) (2*Math.PI - _alpha);
            }
        }

        private void UpdatePosition(float speedT)
        {
            position.X += speedT*_shellFlyingVector.X;
            position.Y += speedT*_shellFlyingVector.Y;
        }

        protected bool Collide()
        {
            return _goal.CollisionRectangleMonster.Contains((int)position.X, (int)position.Y);
        }

        private void DealDamageByAoe(float damageToDeal)
        {
            foreach (MonsterSprite monster in _monsterSpriteList)
            {
                double distanceToMonster = Math.Sqrt(Math.Pow(Position.X - monster.Position.X, 2) +
                                                     Math.Pow(Position.Y - monster.Position.Y, 2));
                if ((distanceToMonster <= _damageRadius) && !monster.Equals(_goal))
                {
                    monster.HitPointsCurrent -= damageToDeal;
                }
            }
        }

        private void ApplySpecialEffectsToGoal(GameTime gameTime)
        {
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
                _goal.PoisonDamagePerSecond = (int) (_poisonDamagePerSecond*_damage);
                _goal.PoisonedPeriod = _poisonPeriod;
                _goal.PoisonedTime = gameTime.TotalGameTime.TotalSeconds;
                _goal.IsPoisoned = true;
            }
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
