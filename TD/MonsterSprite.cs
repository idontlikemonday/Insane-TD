using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class MonsterSprite : Sprite
    {
        private readonly float _hitPointsMax;
        private readonly Texture2D _lifeTexture;
        private Vector2 _movingVect; // пронормированный вектор движения
        private Vector2 _nextPointPath; // следующая точка перегиба трактории
        private Vector2 _ranVect;

        public float HitPointsCurrent { get; set; }
        public int Armor { get; set; }
        public float PassedDistance { get; set; }
        public bool IsAlive { get; set; }
        public bool IsPoisoned { get; set; }
        public bool IsFrozen { get; set; }
        public List<Vector2> MovingPath { get; set; } // лист координат
        public bool IsMoving { get; set; } // флаг - идет ли монстр или уже на финише ;). Если false - значит монстр прошел и надо штрафовать игрока
        public int Bounty { get; set; }

        public int PoisonDamagePerSecond { get; set; }
        public float Freeze { get; set; }
        public double PoisonedPeriod { get; set; }
        public double PoisonedTime { get; set; }
        public double FrozenTime { get; set; }
        public double FrozenPeriod { get; set; }
        public double LastDamageTime { get; set; }

        public override Vector2 Direction
        {
            get
            {
                // возвращаем нормированный вектор движения, умноженный на скорость
                return new Vector2(_movingVect.X * Speed.X * (1 - Freeze), _movingVect.Y * Speed.Y * (1 - Freeze));
            }
        }

        public Rectangle CollisionRectangleMonster
        {
            get
            {
                return new Rectangle(
                    (int)position.X - collisionOffset,
                    (int)position.Y - collisionOffset,
                    (collisionOffset * 2),
                    (collisionOffset * 2));
            }
        }

        public MonsterSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, string collisionCueName, float hitPointsMax, Texture2D lifeTexture,
            int armor, List<Vector2> movingPath, int bounty)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, collisionCueName)
        {
            this._hitPointsMax = hitPointsMax;
            this._lifeTexture = lifeTexture;
            this.HitPointsCurrent = hitPointsMax;
            this.Armor = armor;
            this.PassedDistance = 0f;
            this.MovingPath = movingPath;
            this.IsAlive = true;
            this.IsPoisoned = false;
            this.IsFrozen = false;
            this.IsMoving = true;
            this.Bounty = bounty;
        }

        public MonsterSprite(Texture2D textureImage, Vector2 position,
            Point frameSize, int collisionOffset, Point currentFrame, Point sheetSize,
            Vector2 speed, int millisecondsPerFrame, string collisionCueName, float hitPointsMax,
            Texture2D lifeTexture, int armor, List<Vector2> movingPath, int bounty)
            : base(textureImage, position, frameSize, collisionOffset, currentFrame,
            sheetSize, speed, millisecondsPerFrame, collisionCueName)
        {
            this._hitPointsMax = hitPointsMax;
            this._lifeTexture = lifeTexture;
            this.HitPointsCurrent = hitPointsMax;
            this.Armor = armor;
            this.PassedDistance = 0f;
            this.MovingPath = movingPath;
            this.IsAlive = true;
            this.IsPoisoned = false;
            this.IsFrozen = false;
            this.IsMoving = true;
            this.Bounty = bounty;
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            if (IsPoisoned)
            {
                if (gameTime.TotalGameTime.TotalSeconds - LastDamageTime > 1)
                {
                    HitPointsCurrent -= PoisonDamagePerSecond;
                    LastDamageTime = gameTime.TotalGameTime.TotalSeconds;
                }
                if (PoisonedPeriod < (gameTime.TotalGameTime.TotalSeconds - PoisonedTime))
                {
                    IsPoisoned = false;
                    PoisonDamagePerSecond = 0;
                    LastDamageTime = 0;
                }
            }
            if (IsFrozen)
            {
                if (FrozenPeriod < (gameTime.TotalGameTime.TotalSeconds - FrozenTime))
                {
                    IsFrozen = false;
                    Freeze = 0;
                }
            }
            if (Armor < 0)
            {
                Armor = 0;
            }

            if (position == _nextPointPath)
            {
                MovingPath.RemoveAt(0);
                const int rnd = 10;
                var ran = new Random();
                _ranVect = new Vector2(ran.Next(-rnd, rnd), ran.Next(-rnd, rnd));
            }

            if (MovingPath.Count == 0)
                IsMoving = false;

            if (IsMoving)
            {
                _nextPointPath = MovingPath[0] + _ranVect;
                _movingVect = _nextPointPath - position;
                _movingVect.Normalize();

                if (Math.Abs(Direction.X) < Math.Abs((_nextPointPath - position).X))
                    position.X += Direction.X;
                else
                    position.X += (_nextPointPath - position).X;

                if (Math.Abs(Direction.Y) < Math.Abs((_nextPointPath - position).Y))
                    position.Y += Direction.Y;
                else
                    position.Y += (_nextPointPath - position).Y;

                PassedDistance = PassedDistance + Math.Max(Math.Abs(Speed.X * (1 - Freeze) * _movingVect.X), Math.Abs(Speed.Y * (1 - Freeze) * _movingVect.Y));  //Math.Abs(speed.X * (1 - freeze) * movingVect.X) + Math.Abs(speed.Y * (1 - freeze) * movingVect.Y);

                if (HitPointsCurrent <= 0) IsAlive = false;
            }

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            const float scaleY = 0.1f;
            double atan = Math.Atan2(Direction.Y, Direction.X);
            var monsterAlpha = (float)(atan);

            var tempColor = new Color(1.0f - ((IsPoisoned)?(0.25f):(0f)) - ((IsFrozen)?(0.25f):(0f)), 1.0f-((IsFrozen)?(0.25f):(0f)), 1.0f-((IsPoisoned)?(0.25f):(0f)));
            spriteBatch.Draw(textureImage,
                new Vector2(position.X, position.Y),
                new Rectangle(currentFrame.X * frameSize.X,
                    currentFrame.Y * frameSize.Y,
                    frameSize.X, frameSize.Y),tempColor,
                    monsterAlpha, new Vector2(textureImage.Width / 2, textureImage.Height / 2),
                1f, SpriteEffects.None, 0.49f);

            spriteBatch.Draw(_lifeTexture,
                new Vector2(position.X - textureImage.Width / 2, position.Y - textureImage.Height / 2 - 10),
                null,
                Color.LightGreen,
                0,
                Vector2.Zero,
                new Vector2(((textureImage.Width * HitPointsCurrent) / (_lifeTexture.Width * _hitPointsMax)), scaleY),
                SpriteEffects.None,
                0.91f);

            spriteBatch.Draw(_lifeTexture,
                new Vector2(position.X + textureImage.Width / 2, position.Y - textureImage.Height / 2 - 10 + _lifeTexture.Height * scaleY),
                null,
                Color.LightCoral,
                MathHelper.Pi,
                Vector2.Zero,
                new Vector2(((textureImage.Width * Math.Max(_hitPointsMax - HitPointsCurrent, 0)) / (_lifeTexture.Width * _hitPointsMax)), scaleY),
                SpriteEffects.None,
                0.91f);
        }

    }
}
