using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TD
{
    public class MenuManager : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private Pointer _pointer;
        private Menu _mainMenu;
        private NewGameMenu _newGameMenu;
        private AboutMenu _aboutMenu;
        private HelpMenu _helpMenu;
        private int _pressedButton = -1;
        private bool _mainMenuFirstRun = true;
        private readonly Vector2 _baseSpeed = new Vector2(0, -2);

        public MenuManager(Game game)
            : base(game)
        {
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            _pointer = new Pointer(Game.Content.Load<Texture2D>(@"Images\Pointer1"), new Vector2(Game.Window.ClientBounds.Width / 2, Game.Window.ClientBounds.Height / 2), 
                new Point(32, 32), new Point(0, 0),
                new Point(10, 9), 16);
            var menuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\NewGameDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameSelected"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameClicked"),
                    Game.Content.Load<Texture2D>(@"Images\NewGame"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(384, 460),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\HelpDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\HelpSelected"),
                    Game.Content.Load<Texture2D>(@"Images\HelpClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Help"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(384, 540),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\AboutDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\AboutSelected"),
                    Game.Content.Load<Texture2D>(@"Images\AboutClicked"),
                    Game.Content.Load<Texture2D>(@"Images\About"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(384, 620),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\ExitDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\ExitSelected"),
                    Game.Content.Load<Texture2D>(@"Images\ExitClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Exit"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(384, 700),
                    5,
                    6,
                    true)
            };
            var backgroundTexture = Game.Content.Load<Texture2D>(@"Images\background");
            _mainMenu = new Menu(menuButtonList,backgroundTexture,Vector2.Zero) {Enabled = true};
            menuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\NewGameDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameSelected"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameClicked"),
                    Game.Content.Load<Texture2D>(@"Images\NewGame"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(100, 200),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\NewGameDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameSelected"),
                    Game.Content.Load<Texture2D>(@"Images\NewGameClicked"),
                    Game.Content.Load<Texture2D>(@"Images\NewGame"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadow"),
                    @"blip1",
                    @"click",
                    new Vector2(100, 500),
                    5,
                    6,
                    false)
            };

            #region NEW GAME
            _newGameMenu = new NewGameMenu(menuButtonList, null, Vector2.Zero)
            {
                TextureInputField = Game.Content.Load<Texture2D>(@"Images\InputField"),
                NameFont = Game.Content.Load<SpriteFont>(@"fonts\arial"),
                Enabled = false
            };

            #endregion

            #region ABOUT
            
            var fontSpriteListContent = new List<FontSprite>();
            var stringMessageList = new List<string>();
            var elapsedTimesList = new List<double>();
            var fontUsedList = new List<SpriteFont>();
            var fontColorList = new List<Color>();

            const double timeKoef = 3.0;

            var BOS48Font = Game.Content.Load<SpriteFont>(@"fonts\BookmanOldStyleFont48");
            var BOS36Font = Game.Content.Load<SpriteFont>(@"fonts\BookmanOldStyleFont");
            var BOS24Font = Game.Content.Load<SpriteFont>(@"fonts\BookmanOldStyleFont24");
            var BOS18Font = Game.Content.Load<SpriteFont>(@"fonts\BookmanOldStyleFont18");
            var Tahoma10Font = Game.Content.Load<SpriteFont>(@"fonts\TahomaFont");

            // 0
            stringMessageList.Add("3\"Д\" Developers Team представляет");
            elapsedTimesList.Add(0 * timeKoef);
            stringMessageList.Add("Курсовая работа по дисциплине");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("Человеко-Машинное Взаимодействие");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("IMPOSSIBLE");
            elapsedTimesList.Add(600 * timeKoef);
            stringMessageList.Add("Tower Defense");
            elapsedTimesList.Add(150 * timeKoef);
            // 5
            stringMessageList.Add("В работе принимали участие :");
            elapsedTimesList.Add(400 * timeKoef);
            stringMessageList.Add("Денис \"Prodenx\" Иовлев ");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("Дмитрий \"Just MiT\" Горбунков ");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("Дмитрий \"Devdits\" Дорофеев ");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("Руководитель проекта");
            elapsedTimesList.Add(400 * timeKoef);
            // 10
            stringMessageList.Add("Шелестов Александр Андреевич");
            elapsedTimesList.Add(200 * timeKoef);
            stringMessageList.Add("Проект выполнен в среде");
            elapsedTimesList.Add(800 * timeKoef);
            stringMessageList.Add("Microsoft® Visual Studio 2008");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("на языке программирования C#");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("В проекте использованы музыкальные материалы :");
            elapsedTimesList.Add(800 * timeKoef);
            // 15
            stringMessageList.Add("\"Saw\" Soundtrack");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("\"Doom\" Soundtrack");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("\"Battletoads\" Soundtrack");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("Красивый лабиринт сделан программой");
            elapsedTimesList.Add(600 * timeKoef);
            stringMessageList.Add("Magic Particles 3D");
            elapsedTimesList.Add(100 * timeKoef);
            // 20
            stringMessageList.Add("Изображения башен, снарядов и монстров");
            elapsedTimesList.Add(600 * timeKoef);
            stringMessageList.Add("выполнены в программе Paint.NET");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("За идею спасибо компаниям");
            elapsedTimesList.Add(600 * timeKoef);
            stringMessageList.Add("Blizzard Entertainment");
            elapsedTimesList.Add(100 * timeKoef);
            stringMessageList.Add("Armor Games");
            elapsedTimesList.Add(100 * timeKoef);

            stringMessageList.Add("Томский Государственный Университет Систем Управления и Радиоэлектроники (ТУСУР)" + "\nФакультет Систем Управления" + "\nКафедра Автоматизированных Систем Управления" + "\n©    2010");
            elapsedTimesList.Add(2300 * timeKoef);

            for (int i = 0; i < stringMessageList.Count - 1; i++)
            {
                fontUsedList.Add(BOS36Font);
            }
            fontUsedList[3] = BOS48Font;
            fontUsedList[4] = BOS48Font;
            for (int i = 11; i <= 24; i++)
            {
                fontUsedList[i] = BOS24Font;
            }

            for (int i = 0; i < stringMessageList.Count - 1; i++)
            {
                fontColorList.Add(Color.White);
            }
            fontColorList[3] = Color.LightCoral;
            fontColorList[4] = Color.LightCoral;
            fontColorList[6] = Color.Coral;
            fontColorList[7] = Color.Coral;
            fontColorList[8] = Color.Coral;
            fontColorList[10] = Color.Yellow;

            for (int i = 0; i < stringMessageList.Count - 1; i++)
            {
                fontSpriteListContent.Add(new FontSprite(fontUsedList[i],
                    stringMessageList[i],
                    fontColorList[i],
                    new Vector2(512, 800),
                    new Vector2(512, -100),
                    fontUsedList[i].MeasureString(stringMessageList[i]),
                    _baseSpeed
                    ));
            }

            fontSpriteListContent.Add(new FontSprite(Tahoma10Font,
                    stringMessageList[stringMessageList.Count - 1],
                    Color.White,
                    new Vector2(400, 800),
                    new Vector2(400, 700),
                    new Vector2(0, 0),
                    _baseSpeed
                    ));

            for (int i = 0; i < elapsedTimesList.Count - 1; i++)
            {
                elapsedTimesList[i + 1] = elapsedTimesList[i] + elapsedTimesList[i + 1];
            }

            var aboutMenuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\BackDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\BackSelected"),
                    Game.Content.Load<Texture2D>(@"Images\BackClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Back"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadowSmall"),
                    @"blip1",
                    @"click",
                    new Vector2(5, 735),
                    5,
                    6,
                    true)
            };

            _aboutMenu = new AboutMenu(aboutMenuButtonList,
                null,
                Vector2.Zero,
                fontSpriteListContent,
                elapsedTimesList)
            {
                Enabled = false
            };

            #endregion

            #region HELP

            const int listMax = 3;

            var fontHelpSpriteList = new List<List<FontSprite>>(listMax);
            var imageHelpSpriteList = new List<List<ImageSprite>>(listMax);
            var stringHelpMessageList = new List<List<string>>(listMax);
            var fontHelpUsedList = new List<List<SpriteFont>>(listMax);
            var fontHelpColorList = new List<List<Color>>(listMax);
            var messageHelpCoordList = new List<List<Vector2>>(listMax);
            var linesHelpMeasureList = new List<List<Vector2>>(listMax);

            for (int i = 0; i < listMax; i++)
            {
                fontHelpSpriteList.Add(new List<FontSprite>());
                imageHelpSpriteList.Add(new List<ImageSprite>());
                stringHelpMessageList.Add(new List<string>());
                fontHelpUsedList.Add(new List<SpriteFont>());
                fontHelpColorList.Add(new List<Color>());
                messageHelpCoordList.Add(new List<Vector2>());
                linesHelpMeasureList.Add(new List<Vector2>());
            }

            int listNum = 0;

            stringHelpMessageList[listNum].Add("1.  Цель игры - пропустить как можно меньшее число противников.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 20));
            stringHelpMessageList[listNum].Add("2.  Используйте башни для их уничтожения.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 50));
            stringHelpMessageList[listNum].Add("     Строить башни можно только на зеленой траве.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 80));
            stringHelpMessageList[listNum].Add("     У каждой башни можно индивидуально улучшать характеристики :");
            messageHelpCoordList[listNum].Add(new Vector2(20, 110));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\ForHelp\Characters"), new Vector2(400, 150)));
            stringHelpMessageList[listNum].Add(" - радиус поражения");
            messageHelpCoordList[listNum].Add(new Vector2(350, 400));
            stringHelpMessageList[listNum].Add(" - наносимый урон");
            messageHelpCoordList[listNum].Add(new Vector2(350, 430));
            stringHelpMessageList[listNum].Add(" - скорость поворота башни");
            messageHelpCoordList[listNum].Add(new Vector2(350, 460));
            stringHelpMessageList[listNum].Add(" - скорость атаки башни");
            messageHelpCoordList[listNum].Add(new Vector2(350, 490));
            stringHelpMessageList[listNum].Add(" - количество выпускаемых снарядов");
            messageHelpCoordList[listNum].Add(new Vector2(350, 520));
            stringHelpMessageList[listNum].Add(" - радиус поражения снаряда");
            messageHelpCoordList[listNum].Add(new Vector2(350, 550));

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpUsedList[listNum].Add(BOS18Font);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpColorList[listNum].Add(Color.White);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpSpriteList[listNum].Add(new FontSprite(fontHelpUsedList[listNum][i],
                    stringHelpMessageList[listNum][i],
                    fontHelpColorList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    Vector2.Zero,
                    Vector2.Zero
                    ));
            }

            //===================== ВТОРОЙ ЛИСТ ==================

            listNum++;

            stringHelpMessageList[listNum].Add("     У каждой башни указан радиус поражения.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 20));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\ForHelp\GreenCircle"), new Vector2(400, 70)));
            stringHelpMessageList[listNum].Add("3.  Постойка новых башен и улучшения существующих стоят денег.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 300));
            stringHelpMessageList[listNum].Add("     Чтобы получать деньги - убивайте монстров.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 330));
            stringHelpMessageList[listNum].Add("4.  Некоторые башни обладают специальными эффектами.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 380));
            stringHelpMessageList[listNum].Add("     Синяя башня снижает скорость передвижения монстра не короткое время.");
            messageHelpCoordList[listNum].Add(new Vector2(50, 410));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\BlueTowerButton"), new Vector2(500, 460)));
            stringHelpMessageList[listNum].Add("     Зеленая башня наносит урон монстру в течение времени.");
            messageHelpCoordList[listNum].Add(new Vector2(50, 510));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\GreenTowerButton"), new Vector2(500, 560)));
            stringHelpMessageList[listNum].Add("     Желтая башня снижает броню монстра.");
            messageHelpCoordList[listNum].Add(new Vector2(50, 610));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\YellowTowerButton"), new Vector2(500, 660)));

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpUsedList[listNum].Add(BOS18Font);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpColorList[listNum].Add(Color.White);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpSpriteList[listNum].Add(new FontSprite(fontHelpUsedList[listNum][i],
                    stringHelpMessageList[listNum][i],
                    fontHelpColorList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    Vector2.Zero,
                    Vector2.Zero
                    ));
            }

            //===================== ТРЕТИЙ ЛИСТ ==================

            listNum++;
            
            stringHelpMessageList[listNum].Add("5.  Монстры появляются волнами и идут по лабиринту к выходу.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 20));
            stringHelpMessageList[listNum].Add("     После того, как все монстры волны уничтожены или прошли к выходу,");
            messageHelpCoordList[listNum].Add(new Vector2(20, 50));
            stringHelpMessageList[listNum].Add("     у вас есть 15 секунд, чтобы подготовиться к следующей волне.");
            messageHelpCoordList[listNum].Add(new Vector2(20, 80));
            stringHelpMessageList[listNum].Add("     Каждая шестая волна - БОСС - одиночный сильный противник");
            messageHelpCoordList[listNum].Add(new Vector2(20, 130));
            stringHelpMessageList[listNum].Add("     с большим количеством жизней и брони");
            messageHelpCoordList[listNum].Add(new Vector2(20, 160));
            stringHelpMessageList[listNum].Add("     После каждых шести волн, противники усиливаются");
            messageHelpCoordList[listNum].Add(new Vector2(20, 210));
            stringHelpMessageList[listNum].Add("     и убить их становится сложнее");
            messageHelpCoordList[listNum].Add(new Vector2(20, 240));
            stringHelpMessageList[listNum].Add("6.  В правом нижнем углу можно увидеть какая волна ожидаеся следующей");
            messageHelpCoordList[listNum].Add(new Vector2(20, 320));
            stringHelpMessageList[listNum].Add("     Нажав на желтую стрелку, можно немедленно вызвать следующую волну");
            messageHelpCoordList[listNum].Add(new Vector2(20, 350));
            imageHelpSpriteList[listNum].Add(new ImageSprite(Game.Content.Load<Texture2D>(@"Images\ForHelp\NextWave"), new Vector2(400, 390)));

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpUsedList[listNum].Add(BOS18Font);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpColorList[listNum].Add(Color.White);
            }

            for (int i = 0; i < stringHelpMessageList[listNum].Count; i++)
            {
                fontHelpSpriteList[listNum].Add(new FontSprite(fontHelpUsedList[listNum][i],
                    stringHelpMessageList[listNum][i],
                    fontHelpColorList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    messageHelpCoordList[listNum][i],
                    Vector2.Zero,
                    Vector2.Zero
                    ));
            }

            var helpMenuButtonList = new List<Button>
            {
                new Button(Game.Content.Load<Texture2D>(@"Images\BackDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\BackSelected"),
                    Game.Content.Load<Texture2D>(@"Images\BackClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Back"),
                    Game.Content.Load<Texture2D>(@"Images\MenuButtonShadowSmall"),
                    @"blip1",
                    @"click",
                    new Vector2(5, 735),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\LeftDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\Left"),
                    Game.Content.Load<Texture2D>(@"Images\LeftClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Left"),
                    Game.Content.Load<Texture2D>(@"Images\Left"),
                    @"blip1",
                    @"click",
                    new Vector2(400, 735),
                    5,
                    6,
                    true),
                new Button(Game.Content.Load<Texture2D>(@"Images\RightDisabled"),
                    Game.Content.Load<Texture2D>(@"Images\Right"),
                    Game.Content.Load<Texture2D>(@"Images\RightClicked"),
                    Game.Content.Load<Texture2D>(@"Images\Right"),
                    Game.Content.Load<Texture2D>(@"Images\Right"),
                    @"blip1",
                    @"click",
                    new Vector2(600, 735),
                    5,
                    6,
                    true)
            };

            _helpMenu = new HelpMenu(helpMenuButtonList,
                null,
                Vector2.Zero,
                fontHelpSpriteList,
                imageHelpSpriteList,
                listMax)
            {
                Enabled = false
            };

            #endregion

            base.LoadContent();
        }

        void ChangeFromMainMenu(int pressedButton)
        {
            switch (pressedButton)
            {
                case 0:
                    {
                        Enabled = false;
                        Visible = false;
                        break;
                    }
                case 1:
                    {
                        _helpMenu.Enabled = true;
                        _helpMenu.CurrentList = 0;
                        break;
                    }
                case 2:
                    {
                        _aboutMenu.Enabled = true;
                        ((Game)Game).StopPlayTrackCue();
                        ((Game)Game).PlayTrackCue("About");
                        break;
                    }
                case 3:
                    {
                        Game.Exit();
                        break;
                    }
            }
        }

        void ChangeFromNewGameMenu()
        {
            switch (_pressedButton)
            {
                case 0:
                    {
                        Enabled = false;
                        Visible = false;
                        break;
                    }
                case 1:
                    {
                        _mainMenu.Enabled = true;
                        break;
                    }
            }
        }

        void ChangeFromAboutMenu()
        {
            switch (_pressedButton)
            {
                case 0:
                    {
                        _aboutMenu.Enabled = false;
                        _mainMenu.Enabled = true;

                        _aboutMenu.ElapsedTime = 0;
                        _aboutMenu.CurrentLineToGo = 0;

                        for (int i = 0; i < _aboutMenu.FontSpriteListContent.Count - 1; i++)
                        {
                            _aboutMenu.FontSpriteListContent[i].Position = new Vector2(512, 800);
                            _aboutMenu.FontSpriteListContent[i].Direction = _baseSpeed;
                        }
                        _aboutMenu.FontSpriteListContent[_aboutMenu.FontSpriteListContent.Count - 1].Position = new Vector2(400, 800);
                        _aboutMenu.FontSpriteListContent[_aboutMenu.FontSpriteListContent.Count - 1].Direction = _baseSpeed;
                        _aboutMenu.FontSpriteList = new List<FontSprite>();
                        ((Game)Game).StopPlayTrackCue();
                        ((Game)Game).PlayTrackCue("menu");
                        break;
                    }
            }
        }

        void ChangeFromHelpMenu()
        {
            switch (_pressedButton)
            {
                case 0:
                    {
                        _helpMenu.Enabled = false;
                        _mainMenu.Enabled = true;
                        break;
                    }
                case 1:
                    {
                        if (_helpMenu.CurrentList > 0)
                        {
                            _helpMenu.CurrentList--;
                        }
                        _helpMenu.Enabled = true;
                        break;
                    }
                case 2:
                    {
                        if (_helpMenu.CurrentList < _helpMenu.MaxList - 1)
                        {
                            _helpMenu.CurrentList++;
                        }
                        _helpMenu.Enabled = true;
                        break;
                    }
            }
        }

        public override void Update(GameTime gameTime)
        {
            _pointer.Update(gameTime, Game.Window.ClientBounds);
            _mainMenu.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
            if (_mainMenuFirstRun)
            {
                ((Game)Game).PlayTrackCue("menu");
                _mainMenuFirstRun = false;
            }
            ChangeFromMainMenu(_pressedButton);
            _newGameMenu.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
            ChangeFromNewGameMenu();
            _aboutMenu.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton);
            ChangeFromAboutMenu();
            _helpMenu.Update(gameTime, _pointer.CollisionRect, ((Game)Game), out _pressedButton, _helpMenu.CurrentList);
            ChangeFromHelpMenu();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            if (_aboutMenu.Enabled == true)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            if (_helpMenu.Enabled == true)
            {
                GraphicsDevice.Clear(Color.Black);
            }
            _spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied);
            _mainMenu.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
            _newGameMenu.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
            _aboutMenu.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds);
            _helpMenu.Draw(gameTime, _spriteBatch, Game.Window.ClientBounds, _helpMenu.CurrentList);
            _pointer.Draw(gameTime, _spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}