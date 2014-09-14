using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TD
{
    public class GameProcessManager : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private BuilderPointer _pointer;
        private Interface _gameInterface;
        private UpgradePanel _upgradePanel;
        private int _pressedButton = -1;
        private int _upgradePanelPressedButton = -1;
        private Rectangle _fieldRectangle;
        private readonly List<TowerSprite> _towerList;
        private List<MonsterSprite> _monsterList;
        private readonly List<ShellSprite> _shellList;
        private TowerSprite _blackTower;
        private TowerSprite _yellowTower;
        private TowerSprite _redTower;
        private TowerSprite _blueTower;
        private TowerSprite _greenTower;
        private SpecialEffect _onBuildEffect;
        private SpecialEffect _blackBlowEffect;
        private SpecialEffect _redBlowEffect;
        private SpecialEffect _greenBlowEffect;
        private Level _level1;
        private SpriteFont _font;
        private bool _firstUpdate = true;
        private int _pickedTowerIndex = -1;
        private bool _defeat = false;
        private bool _victory = false;

        public GameProcessManager(Game game)
            : base(game)
        {
            _towerList = new List<TowerSprite>();
            _monsterList = new List<MonsterSprite>();
            _shellList = new List<ShellSprite>();
        }

        public override void Initialize()
        {
            _fieldRectangle = new Rectangle(24, 24, 732, 732);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            var font = Game.Content.Load<SpriteFont>(@"fonts\arial");
            this._font = font;
            var mov1 = new List<Vector2>
            {
                new Vector2(240, 0),
                new Vector2(240, 144),
                new Vector2(96, 144),
                new Vector2(96, 384),
                new Vector2(240, 384),
                new Vector2(240, 288),
                new Vector2(480, 288),
                new Vector2(480, 528),
                new Vector2(96, 528),
                new Vector2(96, 672),
                new Vector2(672, 672),
                new Vector2(672, 144),
                new Vector2(432, 144),
                new Vector2(432, 48)
            };
            var tempMonsterTexturesList = new List<Texture2D>
            {
                Game.Content.Load<Texture2D>(@"Images\2"),
                Game.Content.Load<Texture2D>(@"Images\1"),
                Game.Content.Load<Texture2D>(@"Images\4"),
                Game.Content.Load<Texture2D>(@"Images\3"),
                Game.Content.Load<Texture2D>(@"Images\5"),
                Game.Content.Load<Texture2D>(@"Images\boss1")
            };
            var tempMonsterFrameSizes = new List<Point>
            {
                new Point(32, 32),
                new Point(32, 32),
                new Point(32, 32),
                new Point(32, 32),
                new Point(32, 32),
                new Point(48, 48)
            };
            var tempMonsterHitPointsList = new List<float> {100, 200, 100, 200, 125, 1200};
            var tempMonsterArmorList = new List<int> {0, 0, 30, 60, 20, 80};
            var tempMonsterSpeedList = new List<Vector2>
            {
                new Vector2(1, 1),
                new Vector2(1, 1),
                new Vector2(0.8f, 0.8f),
                new Vector2(0.6f, 0.6f),
                new Vector2(1.5f, 1.5f),
                new Vector2(1.2f, 1.2f)
            };
            var tempMonsterBountyList = new List<int> {1, 1, 2, 6, 3, 10};
            var tempMonsterCountList = new List<int> {10, 15, 15, 6, 12, 1};

            _level1 = new Level(
                Game.Content.Load<Texture2D>(@"Images/Texture3"),
                Game.Content.Load<Texture2D>(@"Images/01_Wider"),
                tempMonsterTexturesList,
                tempMonsterFrameSizes,
                Game.Content.Load<Texture2D>(@"Images\lifeBar"),
                tempMonsterHitPointsList,
                tempMonsterArmorList,
                tempMonsterSpeedList,
                tempMonsterBountyList,
                tempMonsterCountList,
                mov1, 10, 500, 50);
            _monsterList = _level1.Waves[0].MonsterList;
            _pointer = new BuilderPointer(Game.Content.Load<Texture2D>(@"Images\Pointer"),
                null, new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2),
                new Point(21, 21), new Point(1, 1),
                new Point(1, 1), 16, _fieldRectangle, font);
            var texturePanelTool = Game.Content.Load<Texture2D>(@"Images\PanelTool");
            var menuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\BlackTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\BlackTowerButtonSelected"),
                    Game.Content.Load<Texture2D>(@"Images\BlackTowerButtonClicked"),
                    Game.Content.Load<Texture2D>(@"Images\BlackTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\TowerButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(12, 12),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\RedTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\RedTowerButtonSelected"),
                    Game.Content.Load<Texture2D>(@"Images\RedTowerButtonClicked"),
                    Game.Content.Load<Texture2D>(@"Images\RedTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\TowerButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(72, 12),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\GreenTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\GreenTowerButtonSelected"),
                    Game.Content.Load<Texture2D>(@"Images\GreenTowerButtonClicked"),
                    Game.Content.Load<Texture2D>(@"Images\GreenTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\TowerButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(132, 12),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\BlueTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\BlueTowerButtonSelected"),
                    Game.Content.Load<Texture2D>(@"Images\BlueTowerButtonClicked"),
                    Game.Content.Load<Texture2D>(@"Images\BlueTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\TowerButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(192, 12),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\YellowTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\YellowTowerButtonSelected"),
                    Game.Content.Load<Texture2D>(@"Images\YellowTowerButtonClicked"),
                    Game.Content.Load<Texture2D>(@"Images\YellowTowerButton"),
                    Game.Content.Load<Texture2D>(@"Images\TowerButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(12, 72),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Right"),
                    Game.Content.Load<Texture2D>(@"Images\right"),
                    Game.Content.Load<Texture2D>(@"Images\rightClicked"),
                    Game.Content.Load<Texture2D>(@"Images\right"),
                    Game.Content.Load<Texture2D>(@"Images\right"),
                    @"blip1",
                    @"click",
                    new Vector2(190, 633),
                    5,
                    6,
                    true)
            };
            _onBuildEffect = new SpecialEffect(Game.Content.Load<Texture2D>(@"Images\BuildingTower"),
                                    null,
                                    new Point(15, 5),
                                    new Point(128, 128),
                                    new Point(3, 0),
                                    Color.White,
                                    new Vector2(300, 350),
                                    30,
                                    0,
                                    0,
                                    0.0f,
                                    0.88f,
                                    false);
            _blackBlowEffect = new SpecialEffect(Game.Content.Load<Texture2D>(@"Images/Blow"),
                @"blast", new Point(12, 8), new Point(64, 64), new Point(0, 0), Color.White, Vector2.Zero,
                5, 0, -48, 0, 1, false);
            _redBlowEffect = new SpecialEffect(Game.Content.Load<Texture2D>(@"Images/Blow"),
                @"fire", new Point(12, 8), new Point(64, 64), new Point(0, 0), Color.White, Vector2.Zero,
                5, 0, -48, 0, 1, false);
            _greenBlowEffect = new SpecialEffect(Game.Content.Load<Texture2D>(@"Images/Blow"),
                @"poison", new Point(12, 8), new Point(64, 64), new Point(0, 0), Color.White, Vector2.Zero,
                5, 0, -48, 0, 1, false);
            _yellowTower = new TowerSprite(Game.Content.Load<Texture2D>(@"Images/Armor"),
                                Game.Content.Load<Texture2D>(@"Images/circle"),
                                Game.Content.Load<Texture2D>(@"Images/ArmorShell"),
                                new Vector2(300, 350), 150, 1000, 0.3f, 10, 1, 0.0f, 10, _monsterList, _shellList, false, false, true, 0, 5, new Point(48, 48), (Game)Game,
                                300, 20, 1, 500, 1, 0.0f,
                                110,
                                5,
                                6,
                                5,
                                40,
                                70,
                                30);
            _redTower = new TowerSprite(Game.Content.Load<Texture2D>(@"Images/Fire"),
                                Game.Content.Load<Texture2D>(@"Images/circle"),
                                Game.Content.Load<Texture2D>(@"Images/fireshell"),
                                new Vector2(200, 250), 150, 80, 0.5f, 10, 1, 5, 4, _monsterList, _shellList, false, false, false, 0, 0, new Point(48, 48), (Game)Game,
                                300, 300, 1.2f, 500, 3, 25f,
                                50,
                                5,
                                8,
                                5,
                                10,
                                40,
                                10);
            _blueTower = new TowerSprite(Game.Content.Load<Texture2D>(@"Images/Ice"),
                                Game.Content.Load<Texture2D>(@"Images/circle"),
                                Game.Content.Load<Texture2D>(@"Images/iceshell"),
                                new Vector2(200, 250), 150, 1000, 0.5f, 10, 1, 0.0f, 3, _monsterList, _shellList, true, false, false, 0.3f, 2, new Point(48, 48), (Game)Game,
                                300, 50, 1.2f, 700, 2, 0.0f,
                                70,
                                7,
                                10,
                                5,
                                10,
                                50,
                                30);
            _greenTower = new TowerSprite(Game.Content.Load<Texture2D>(@"Images/Poison"),
                               Game.Content.Load<Texture2D>(@"Images/circle"),
                               Game.Content.Load<Texture2D>(@"Images/poisonshell"),
                               new Vector2(200, 250), 150, 1000, 0.5f, 50, 1, 0.0f, 3, _monsterList, _shellList, false, true, false, 0.5f, 5, new Point(48, 48), (Game)Game,
                               300, 200, 1.2f, 700, 1, 0.0f,
                               80,
                               5,
                               20,
                               5,
                               10,
                               70,
                               30);
            _blackTower = new TowerSprite(Game.Content.Load<Texture2D>(@"Images/Base"),
                                Game.Content.Load<Texture2D>(@"Images/circle"),
                                Game.Content.Load<Texture2D>(@"Images/BaseShell"),
                                new Vector2(200, 250), 150, 1000, 0.5f, 20, 1, 0, 4, _monsterList, _shellList, false, false, false, 0, 0, new Point(48, 48), (Game)Game,
                                300, 150, 1.2f, 200, 1, 0f,
                                10,
                                8,
                                8,
                                5,
                                5,
                                3,
                                3);
            _gameInterface = new Interface(menuButtonList, texturePanelTool, new Vector2(768, 0), font, _level1,
                Game.Content.Load<Texture2D>(@"Images\Dollar"), _pointer,
                _blackTower, _redTower, _greenTower, _blueTower, _yellowTower) {Enabled = true};
            texturePanelTool = Game.Content.Load<Texture2D>(@"Images\UpgradePanel");

            menuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 6),
                    0,
                    0,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 36),
                    0,
                    0,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 66),
                    0,
                    0,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 96),
                    0,
                    0,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 123),
                    0,
                    0,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\Plus_disabled"),
                    Game.Content.Load<Texture2D>(@"Images\PlusSelected"),
                    Game.Content.Load<Texture2D>(@"Images\Plus2"),
                    Game.Content.Load<Texture2D>(@"Images\Plus1"),
                    Game.Content.Load<Texture2D>(@"Images\PlusShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(116, 150),
                    0,
                    0,
                    true)
            };
            _upgradePanel = new UpgradePanel(menuButtonList, texturePanelTool, Vector2.Zero, font,
                Game.Content.Load<Texture2D>(@"Images\Damage"),
                Game.Content.Load<Texture2D>(@"Images\Radius"),
                Game.Content.Load<Texture2D>(@"Images\Rotate"),
                Game.Content.Load<Texture2D>(@"Images\Speed"),
                Game.Content.Load<Texture2D>(@"Images\Count"),
                Game.Content.Load<Texture2D>(@"Images\Damage_zone"),
                _level1,
                _pointer)
            {
                Alpha = 0.5f,
                ZDepth = 0.9f,
                IsStanded = false,
                Enabled = false
            };
            _pointer.UpgradePanel = _upgradePanel;
            base.LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if (_firstUpdate)
            {
                _level1.Timer.Run(gameTime);
                ((Game)Game).StopPlayTrackCue();
                ((Game)Game).PlayTrackCue("About");
                _firstUpdate = false;
            }
            _level1.Update(gameTime, _monsterList);
            TowerSprite tempSelectedTowerSprite = null;
            _gameInterface.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
            _gameInterface.Enabled = true;
            if ((_pressedButton == 0) || (Keyboard.GetState().IsKeyDown(Keys.Q)))
            {
                if (_level1.Money >= _blackTower.Cost)
                {
                    _pointer.PickedTower = new TowerSprite(_blackTower)
                    {
                        BuildingEffect = new SpecialEffect(_onBuildEffect),
                        FireEffect = new SpecialEffect(_blackBlowEffect),
                        DrawDamageZone = true
                    };
                    _pointer.UpgradePanel.IsStanded = false;
                    foreach (TowerSprite tower in _towerList)
                        tower.DrawDamageZone = true;
                    _pickedTowerIndex = 0;
                }
            }
            if ((_pressedButton == 1) || (Keyboard.GetState().IsKeyDown(Keys.W)))
            {
                if (_level1.Money >= _redTower.Cost)
                {
                    _pointer.PickedTower = new TowerSprite(_redTower);
                    _pointer.PickedTower.BuildingEffect = new SpecialEffect(_onBuildEffect);
                    _pointer.PickedTower.FireEffect = new SpecialEffect(_redBlowEffect);
                    _pointer.PickedTower.DrawDamageZone = true;
                    _pointer.UpgradePanel.IsStanded = false;
                    foreach (TowerSprite tower in _towerList)
                        tower.DrawDamageZone = true;
                    _pickedTowerIndex = 1;
                }
            }
            if ((_pressedButton == 2) || (Keyboard.GetState().IsKeyDown(Keys.E)))
            {
                if (_level1.Money >= _greenTower.Cost)
                {
                    _pointer.PickedTower = new TowerSprite(_greenTower);
                    _pointer.PickedTower.BuildingEffect = new SpecialEffect(_onBuildEffect);
                    _pointer.PickedTower.FireEffect = new SpecialEffect(_blackBlowEffect);
                    _pointer.PickedTower.DrawDamageZone = true;
                    _pointer.UpgradePanel.IsStanded = false;
                    foreach (TowerSprite tower in _towerList)
                        tower.DrawDamageZone = true;
                    _pickedTowerIndex = 2;
                }
            }
            if ((_pressedButton == 3) || (Keyboard.GetState().IsKeyDown(Keys.R)))
            {
                if (_level1.Money >= _blueTower.Cost)
                {
                    _pointer.PickedTower = new TowerSprite(_blueTower);
                    _pointer.PickedTower.BuildingEffect = new SpecialEffect(_onBuildEffect);
                    _pointer.PickedTower.FireEffect = new SpecialEffect(_blackBlowEffect);
                    _pointer.PickedTower.DrawDamageZone = true;
                    _pointer.UpgradePanel.IsStanded = false;
                    foreach (TowerSprite tower in _towerList)
                        tower.DrawDamageZone = true;
                    _pickedTowerIndex = 3;
                }
            }
            if ((_pressedButton == 4) || (Keyboard.GetState().IsKeyDown(Keys.A)))
            {
                if (_level1.Money >= _yellowTower.Cost)
                {
                    _pointer.PickedTower = new TowerSprite(_yellowTower);
                    _pointer.PickedTower.BuildingEffect = new SpecialEffect(_onBuildEffect);
                    _pointer.PickedTower.FireEffect = new SpecialEffect(_blackBlowEffect);
                    _pointer.PickedTower.DrawDamageZone = true;
                    _pointer.UpgradePanel.IsStanded = false;
                    foreach (TowerSprite tower in _towerList)
                        tower.DrawDamageZone = true;
                    _pickedTowerIndex = 4;
                }
            }
            if ((_pressedButton == 5) || (Keyboard.GetState().IsKeyDown(Keys.Space)))
            {
                if (_level1.Timer.IsRunning)
                    _level1.Timer.CurrentTime = 0;
                else
                {
                    if (_level1.Waves.Count > 1)
                    {
                        _level1.Waves.RemoveAt(0);
                        _level1.AddMonsters(_monsterList);
                    }
                }
            }
            if (_pointer.PickedTower != null)
            {
                bool temp = false;
                _pointer.PickedTower.Color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                foreach (TowerSprite tower in _towerList)
                {
                    if (tower.TowerCollisionRectangle.Intersects(_pointer.PickedTower.TowerCollisionRectangle))
                    {
                        temp = true;
                        _pointer.PickedTower.Color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                        break;
                    }
                }
                if (!_fieldRectangle.Contains(new Point((int)_pointer.PickedTower.Position.X, (int)_pointer.PickedTower.Position.Y)) ||
                    _level1.TRectangle.Intersects(_pointer.PickedTower.TowerCollisionRectangle))
                {
                    _pointer.PickedTower.Color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
                    temp = true;
                }
                if ((Mouse.GetState().LeftButton == ButtonState.Pressed) && !temp)
                {
                    _pointer.PickedTower.Color = Color.White;
                    _level1.Money -= _pointer.PickedTower.Cost;
                    _towerList.Add(new TowerSprite(_pointer.PickedTower));
                    _towerList.Last().BuildingEffect = new SpecialEffect(_onBuildEffect);
                    switch (_pickedTowerIndex)
                    {
                        case 0:
                            {
                                _towerList.Last().FireEffect = new SpecialEffect(_blackBlowEffect);
                                break;
                            }
                        case 1:
                            {
                                _towerList.Last().FireEffect = new SpecialEffect(_redBlowEffect);
                                break;
                            }
                        case 2:
                            {
                                _towerList.Last().FireEffect = new SpecialEffect(_greenBlowEffect);
                                break;
                            }
                        case 3:
                            {
                                _towerList.Last().FireEffect = new SpecialEffect(_blackBlowEffect);
                                break;
                            }
                        case 4:
                            {
                                _towerList.Last().FireEffect = new SpecialEffect(_blackBlowEffect);
                                break;
                            }
                    }
                    _towerList.Last().BuildTowerHere();
                    _towerList.Last().DrawDamageZone = true;
                    if ((Keyboard.GetState().IsKeyUp(Keys.LeftControl)) || (_level1.Money < _pointer.PickedTower.Cost))
                        _pointer.PickedTower = null;
                }
            }
            if (!_upgradePanel.IsStanded)
                _upgradePanel.Enabled = false;
            foreach (TowerSprite tower in _towerList)
            {
                tower.Update(gameTime, Game.Window.ClientBounds);
                if (!_pointer.CollisionRect.Intersects(_upgradePanel.CollisionRectangle))
                    if ((_pointer.CollisionRect.Intersects(tower.TowerCollisionRectangle)) &&(_pointer.PickedTower == null))
                    {
                        tower.DrawDamageZone = true;
                        if (!_upgradePanel.IsStanded && Keyboard.GetState().IsKeyUp(Keys.LeftControl))
                        {
                            _upgradePanel.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
                            _upgradePanel.SelectedTower = tower;
                            _upgradePanel.Enabled = true;
                        }
                        tempSelectedTowerSprite = tower;
                    }
                    else
                        if ((_pointer.PickedTower == null))
                            tower.DrawDamageZone = false;
            }
            _upgradePanel.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _upgradePanelPressedButton);
            _pointer.Update(gameTime, Game.Window.ClientBounds);
            if ((Mouse.GetState().LeftButton == ButtonState.Pressed) && _upgradePanel.Enabled)
            {
                if (tempSelectedTowerSprite != null)
                {
                    _upgradePanel.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
                    _upgradePanel.SelectedTower = tempSelectedTowerSprite;
                    _upgradePanel.IsStanded = false;
                    _upgradePanel.Enabled = true;
                    _pointer.Update(gameTime, Game.Window.ClientBounds);
                }
                _upgradePanel.IsStanded = true;
            }
            if ((Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                _pointer.PickedTower = null;
                _upgradePanel.IsStanded = false;
                if (_upgradePanel.SelectedTower != null)
                {
                    _upgradePanel.SelectedTower.DrawDamageZone = false;
                    _upgradePanel.SelectedTower = null;
                }
            }
            for (int i = 0; i < _monsterList.Count; i++)
            {
                _monsterList[i].Update(gameTime, Game.Window.ClientBounds);
                if ((_monsterList[i].IsAlive == false) || (_monsterList[i].IsMoving == false))
                {
                    if (!_monsterList[i].IsMoving)
                        _level1.Lifes -= 1;
                    else
                        _level1.Money += _monsterList[i].Bounty;
                    _monsterList.RemoveAt(i);
                    i--;
                }
            }
            for (int i = 0; i < _shellList.Count; i++)
            {
                _shellList[i].Update(gameTime, Game.Window.ClientBounds);
                if (!_shellList[i].IsFly)
                {
                    _shellList.RemoveAt(i);
                    i--;
                }
            }
            if (_level1.Lifes < 1)
            {
                _defeat = true;
            }
            if ((_level1.Waves.Count == 0) && (_level1.Lifes > 0))
            {
                _victory = true;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            if ((!_defeat) && (!_victory))
            {

                _gameInterface.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
                _upgradePanel.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
                foreach (TowerSprite tower in _towerList)
                    tower.Draw(gameTime, _spriteBatch);
                foreach (MonsterSprite monster in _monsterList)
                    monster.Draw(gameTime, _spriteBatch);
                foreach (ShellSprite shell in _shellList)
                    shell.Draw(gameTime, _spriteBatch);
                _pointer.Draw(gameTime, _spriteBatch);
                _upgradePanel.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
                _level1.Draw(_spriteBatch, _font);
            }
            if (_defeat)
            {
                _spriteBatch.Draw(Game.Content.Load<Texture2D>(@"Images/Defeat"), new Rectangle(0, 0, 1024, 768), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
            }
            if (_victory)
            {
                _spriteBatch.Draw(Game.Content.Load<Texture2D>(@"Images/Victory"), new Rectangle(0, 0, 1024, 768), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 1f);
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}