using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    public class Game : Microsoft.Xna.Framework.Game
    {
        private MenuManager _menuManager;
        private GameProcessManager _gameProcessManager;
        private AudioEngine _audioEngine;
        private WaveBank _waveBank;
        private SoundBank _soundBank;
        private Cue _trackCue;

        public Game()
        {
            var graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1024;
            TargetElapsedTime = new TimeSpan(0, 0, 0, 0, 16);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            _menuManager = new MenuManager(this);
            _gameProcessManager = new GameProcessManager(this);
            _gameProcessManager.Enabled = false;
            _gameProcessManager.Visible = false;
            Components.Add(_menuManager);
            Components.Add(_gameProcessManager);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _audioEngine = new AudioEngine(@"Content\Audio\GameAudio.xgs");
            _waveBank = new WaveBank(_audioEngine, @"Content\Audio\Wave Bank.xwb");
            _soundBank = new SoundBank(_audioEngine, @"Content\Audio\Sound Bank.xsb");
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();
            if (!_menuManager.Enabled)
            {
                _gameProcessManager.Enabled = true;
                _gameProcessManager.Visible = true;
            }
            _audioEngine.Update();
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }

        public void PlayCue(string cueName)
        {
            _soundBank.PlayCue(cueName);
        }

        public void PlayTrackCue(string name)
        {
            _trackCue = _soundBank.GetCue(name);
            _trackCue.Play();
        }

        public void StopPlayTrackCue()
        {
            _trackCue.Stop(AudioStopOptions.Immediate);
        }

    }
}
