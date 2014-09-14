using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class TowerSprite : Sprite
    {
        private readonly Texture2D _tower;
        private readonly Texture2D _zone;
        private readonly Texture2D _shell;
        private Vector2 _shellPosition;
        private Boolean _captureFlag = false;
        private readonly Boolean _isArmor = false;
        private readonly Boolean _isPoison = false;
        private readonly Boolean _isIce = false;
        private MonsterSprite _enemy;
        private readonly List<MonsterSprite> _monsterSpriteList;
        private List<MonsterSprite> _detectedMonstersList;
        private readonly List<ShellSprite> _shellSpriteList;
        private readonly List<SpecialEffect> _fireList;
        private double _blowTimeSinceLastBlow = 0;
        private double _lastBlowTime = 0;
        private readonly double _shellFlySpeed = 0;
        private readonly Game _game;
        private readonly float _duration = 0;
        private readonly int _ratio = 0;
        private double _rotate = 0;
        private double _towerAttackSpeed;
        private readonly double _baseAttackSpeed;
        private readonly double _maxAttackSpeed;
        private Vector2 _norma = new Vector2(0, -1);


        public int RadiusUpgradeCost { get; set; }
        public int DamageUpgradeCost { get; set; }
        public int RotateSpeedUpgradeCost { get; set; }
        public int AttackSpeedUpgradeCost { get; set; }
        public int OneTimeShellsCountUpgradeCost { get; set; }
        public int DamageRadiusUpgradeCost { get; set; }
        public int Cost { get; private set; }
        public double BaseRadius { get; private set; }
        public double MaxRadius { get; private set; }
        public double Radius { get; private set; }
        public int MaxDamage { get; private set; }
        public int Damage { get; private set; }
        public float MaxRotateSpeed { get; private set; }
        public float RotateSpeed { get; private set; }

        public double ExtraRadius
        {
            get { return Radius - BaseRadius;}
        }

        public int BaseDamage { get; private set; }

        public int ExtraDamage
        {
            get { return Damage - BaseDamage; }
        }

        public float BaseRotateSpeed { get; private set; }

        public float ExtraRotateSpeed
        {
            get { return (float)Math.Round(RotateSpeed - BaseRotateSpeed, 2); }
        }

        public double BaseAttackSpeed
        {
            get { return 1000 / _baseAttackSpeed; }
        }

        public double ExtraAttackSpeed
        {
            get { return Math.Round(AttackSpeed - BaseAttackSpeed, 1); }
        }

        public int BaseOneTimeShellsCount { get; private set; }

        public int ExtraOneTimeShellCount
        {
            get { return OneTimeShellsCount - BaseOneTimeShellsCount; }
        }

        public double BaseDamageRadius { get; private set; }

        public double ExtraDamageRadius
        {
            get { return DamageRadius - BaseDamageRadius; }
        }

        public double MaxAttackSpeed
        {
            get { return 1000.0 / _maxAttackSpeed; }
        }

        public double AttackSpeed
        {
            get { return 1000 / _towerAttackSpeed; }
        }

        public int MaxOneTimeShellsCount { get; private set; }
        public int OneTimeShellsCount { get; private set; }
        public double MaxDamageRadius { get; private set; }
        public double DamageRadius { get; private set; }
        public Color Color { private get; set; }
        public Boolean DrawDamageZone { get; set; }
        public SpecialEffect BuildingEffect { get; set; }
        public SpecialEffect FireEffect { get; set; }

        public override Vector2 Direction
        {
            get { return Speed; }
        }

        public Rectangle TowerCollisionRectangle
        {
            get
            {
                return new Rectangle((int)Position.X - frameSize.X / 2, (int)Position.Y - frameSize.Y / 2,
                    frameSize.X, frameSize.Y);
            }
        }



        public void IncreaseRadius()
        {
            Radius+=(int)(BaseRadius*0.1);
        }

        public void IncreaseRotateSpeed()
        {
            RotateSpeed += 0.1f;
        }

        public void IncreaseFireSpeed()
        {
            _towerAttackSpeed -= 100;
        }

        public void IncreaseDamage()
        {
            Damage += (int)(BaseDamage * 0.2);
        }

        public void IncreaseOneTimeShellsCount()
        {
            OneTimeShellsCount++;
        }

        public void IncreaseDamageRadius()
        {
            DamageRadius += BaseDamageRadius *  0.2;
        }

        public void BuildTowerHere()
        {
            BuildingEffect.Run(_game);
        }

        public TowerSprite(Texture2D towerTexture, Texture2D damageZone, 
            Texture2D shell, Vector2 towerPosition, double towerDamageRadius, int towerAttackSpeed, 
            float towerRotateSpeed, int towerDamage, int oneTimeShellsCount, float damageRadius, double shellFlySpeed,
            List<MonsterSprite> monsterSpriteList, List<ShellSprite> shellSpriteList, Boolean isIce, Boolean isPoison,
            Boolean isArmor, float duration, 
            int ratio, Point frameSize, Game game, double maxRadius, int maxDamage, float maxRotateSpeed, double maxAttackSpeed,
            int maxOneTimeShellsCount, double maxDamageRadius, int cost,
            int radiusUpgradeCost,
            int damageUpgradeCost,
            int rotateSpeedUpgradeCost,
            int attackSpeedUpgradeCost,
            int oneTimeShellsCountUpgradeCost,
            int damageRadiusUpgradeCost)
            : base()
        {
            this._tower = towerTexture;
            this._zone = damageZone;
            this._shell = shell;
            this.Position = towerPosition;
            this.Radius = towerDamageRadius;
            this._monsterSpriteList = monsterSpriteList;
            this._shellSpriteList = shellSpriteList;
            this._isArmor = isArmor;
            this._isIce = isIce;
            this._isPoison = isPoison;
            this._duration = duration;
            this._ratio = ratio;
            this._shellFlySpeed = shellFlySpeed;
            this._towerAttackSpeed = towerAttackSpeed;
            this.RotateSpeed = towerRotateSpeed;
            this.Damage = towerDamage;
            this._lastBlowTime = -towerAttackSpeed;
            this.DrawDamageZone = false;
            this.BuildingEffect = null;
            this.Color = Color.White;
            this.frameSize = frameSize;
            this.FireEffect = null;
            this._fireList = new List<SpecialEffect>();
            this._game = game;
            this.OneTimeShellsCount = oneTimeShellsCount;
            this.BaseDamage = towerDamage;
            this.BaseDamageRadius = damageRadius;
            this.BaseRadius = Radius;
            this.BaseRotateSpeed = towerRotateSpeed;
            this.BaseOneTimeShellsCount = oneTimeShellsCount;
            this._baseAttackSpeed = towerAttackSpeed;
            this.DamageRadius = damageRadius;
            this.MaxRadius = maxRadius;
            this.MaxDamage = maxDamage;
            this.MaxRotateSpeed = maxRotateSpeed;
            this._maxAttackSpeed = maxAttackSpeed;
            this.MaxOneTimeShellsCount = maxOneTimeShellsCount;
            this.MaxDamageRadius = maxDamageRadius;
            this.Cost = cost;
            this.RadiusUpgradeCost = radiusUpgradeCost;
            this.DamageUpgradeCost = damageUpgradeCost;
            this.RotateSpeedUpgradeCost = rotateSpeedUpgradeCost;
            this.AttackSpeedUpgradeCost = attackSpeedUpgradeCost;
            this.OneTimeShellsCountUpgradeCost = oneTimeShellsCountUpgradeCost;
            this.DamageRadiusUpgradeCost = damageRadiusUpgradeCost;
        }

        public TowerSprite(TowerSprite tower): 
            base((Sprite)tower)
        {
            this._tower = tower._tower;
            this._zone = tower._zone;
            this._shell = tower._shell;
            this.Position = tower.Position;
            this.Radius = tower.Radius;
            this._monsterSpriteList = tower._monsterSpriteList;
            this._shellSpriteList = tower._shellSpriteList;
            this._isArmor = tower._isArmor;
            this._isIce = tower._isIce;
            this._isPoison = tower._isPoison;
            this._duration = tower._duration;
            this._ratio = tower._ratio;
            this._shellFlySpeed = tower._shellFlySpeed;
            this._towerAttackSpeed = tower._towerAttackSpeed;
            this.RotateSpeed = tower.RotateSpeed;
            this.Damage = tower.Damage;
            this._lastBlowTime = -tower._towerAttackSpeed;
            this.DrawDamageZone = false;
            this.BuildingEffect = tower.BuildingEffect;
            this.Color = Color.White;
            this.FireEffect = null;
            this._fireList = new List<SpecialEffect>();
            this._game = tower._game;
            this.OneTimeShellsCount = tower.OneTimeShellsCount;
            this.BaseDamage = tower.Damage;
            this.BaseDamageRadius = tower.DamageRadius;
            this.BaseRadius = tower.Radius;
            this.BaseRotateSpeed = tower.RotateSpeed;
            this.BaseOneTimeShellsCount = tower.OneTimeShellsCount;
            this._baseAttackSpeed = tower._towerAttackSpeed;
            this.DamageRadius = tower.DamageRadius;
            this.MaxRadius = tower.MaxRadius;
            this.MaxDamage = tower.MaxDamage;
            this.MaxRotateSpeed = tower.MaxRotateSpeed;
            this._maxAttackSpeed = tower._maxAttackSpeed;
            this.MaxOneTimeShellsCount = tower.MaxOneTimeShellsCount;
            this.MaxDamageRadius = tower.MaxDamageRadius;
            this.Cost = tower.Cost;
            this.RadiusUpgradeCost = tower.RadiusUpgradeCost;
            this.DamageUpgradeCost = tower.DamageUpgradeCost;
            this.RotateSpeedUpgradeCost = tower.RotateSpeedUpgradeCost;
            this.AttackSpeedUpgradeCost = tower.AttackSpeedUpgradeCost;
            this.OneTimeShellsCountUpgradeCost = tower.OneTimeShellsCountUpgradeCost;
            this.DamageRadiusUpgradeCost = tower.DamageRadiusUpgradeCost;
        }

        private void Aim()
        {
            // Смотрим, монстр в радиусе обстрела или нет?
            _enemy = null;
            _detectedMonstersList = new List<MonsterSprite>();
            if (_monsterSpriteList.Count == 0)
            {
                _captureFlag = false;
                return;
            }
            float maxDistance = 0;
            foreach (MonsterSprite s in _monsterSpriteList)
            {
                double tempD = Math.Sqrt(Math.Pow(Position.X - s.Position.X, 2) + Math.Pow(Position.Y - s.Position.Y, 2));
                if (tempD <= Radius)
                    if ((s.Distance > maxDistance))
                    {
                        _enemy = s;
                        maxDistance = s.Distance;
                    }
                    else
                        _detectedMonstersList.Add(s);
            }
            // Если враг в зоне поражения
            if (_enemy != null)
            {
                Vector2 aimVector = new Vector2(_enemy.Position.X - Position.X, _enemy.Position.Y - Position.Y);
                float aimAngle = (float)Math.Acos((_norma.X * aimVector.X + _norma.Y * aimVector.Y) / (_norma.Length() * aimVector.Length()));
                if (Position.X > _enemy.Position.X)
                    aimAngle = MathHelper.Pi * 2 - aimAngle;
                if (aimAngle > _rotate)
                {
                    if (aimAngle - _rotate > Math.PI)
                        _rotate += Math.PI * 2;
                }
                else
                    if (_rotate - aimAngle > Math.PI)
                        _rotate -= Math.PI * 2;
                if (_rotate > aimAngle)
                    _rotate -= Math.Min(0.08 * RotateSpeed, _rotate - aimAngle);
                else
                    _rotate += Math.Min(0.08 * RotateSpeed, aimAngle - _rotate);

                if (Math.Abs(_rotate - aimAngle) < (0.08 * RotateSpeed))
                    _captureFlag = true;
                else
                    _captureFlag = false;
            }
        }

        public override void Update(GameTime gameTime, Rectangle clientBounds)
        {
            BuildingEffect.SetPosition(Position);
            Aim();
            if ((_captureFlag))
            {
                _blowTimeSinceLastBlow = gameTime.TotalGameTime.TotalMilliseconds - _lastBlowTime;
                if (_blowTimeSinceLastBlow >= _towerAttackSpeed)
                {
                    _blowTimeSinceLastBlow = 0;
                    _lastBlowTime = gameTime.TotalGameTime.TotalMilliseconds;
                    _shellPosition.X = Position.X + (float)(_tower.Width / 2 * Math.Sin(_rotate));
                    _shellPosition.Y = Position.Y - (float)(_tower.Height / 2 * Math.Cos(_rotate));
                    _shellSpriteList.Add(new ShellSprite(
                                _shell,
                                _shellPosition, new Point(_shell.Width, _shell.Height), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2((float)(_shellFlySpeed), (float)(_shellFlySpeed)), "",
                                _enemy, Damage, _isArmor, _isIce, _isPoison, _duration, _ratio, DamageRadius, _monsterSpriteList));
                    for (int i = 0; i < OneTimeShellsCount - 1; i++)
                    {
                        if (_detectedMonstersList.Count != 0)
                        {
                            _shellSpriteList.Add(new ShellSprite(
                                _shell,
                                _shellPosition, new Point(_shell.Width, _shell.Height), 0, new Point(0, 0),
                                new Point(1, 1), new Vector2((float)(_shellFlySpeed), (float)(_shellFlySpeed)), "",
                                _detectedMonstersList.Last(), Damage, _isArmor, _isIce, _isPoison, _duration, _ratio, DamageRadius, _monsterSpriteList));
                            _detectedMonstersList.RemoveAt(_detectedMonstersList.Count - 1);
                        }
                        else
                            break;
                        _shellSpriteList.ElementAt(_shellSpriteList.Count - 1).Update(gameTime, clientBounds);
                    }
                    if (FireEffect != null)
                    {
                        if (_fireList.Count > 0)
                            _fireList.Last().Depth = 0.9f;
                        _fireList.Add(new SpecialEffect(FireEffect));
                        _fireList.Last().Position = Position;
                        _fireList.Last().Angle = (float)_rotate;
                        _fireList.Last().Run(_game);
                    }
                    _captureFlag = false;
                }
            }
            BuildingEffect.Update(gameTime);
            List<int> indexList = new List<int>();
            foreach (SpecialEffect effect in _fireList)
            {
                effect.Update(gameTime);
                if (!effect.IsEnabled)
                    indexList.Add(_fireList.IndexOf(effect));
            }
            foreach (int index in indexList)
                _fireList.RemoveAt(index);

            base.Update(gameTime, clientBounds);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_tower, Position, null, Color, (float)_rotate, new Vector2(_tower.Width / 2, _tower.Height / 2), 1f, SpriteEffects.None, 1);
            if (DrawDamageZone)
                spriteBatch.Draw(_zone, Position, null, Color.White * 0.4f, 0.0f, 
                    new Vector2(_zone.Width / 2, _zone.Height / 2), (float)(Radius / 100), 
                    SpriteEffects.None, 0.1f);
            BuildingEffect.Draw(gameTime, spriteBatch);
            foreach (SpecialEffect effect in _fireList)
                effect.Draw(gameTime, spriteBatch);
        }
    }
}