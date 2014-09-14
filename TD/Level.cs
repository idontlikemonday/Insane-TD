using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TD
{
    class Level
    {
        private readonly Texture2D _trap;
        private readonly Texture2D _environment;

        public List<Wave> Waves { get; set; }
        public TrapRectangle TRectangle { get; set; }
        public Timer Timer { get; set; }
        public int Money { get; set; }
        public int Lifes { get; set; }

        public Level (
            Texture2D trap,
            Texture2D environment,
            List<Texture2D> monsterTextures,
            List<Point> monsterFrmameSizes,
            Texture2D lifeBar,
            List<float> monsterHitpoints,
            List<int> monsterArmors,
            List<Vector2> monsterSpeeds,
            List<int> monsterBountyes,
            List<int> monsterCounts,
            List<Vector2> path,
            int repeatCount,
            int money,
            int lifes)
        {
            this._trap = trap;
            this._environment = environment;
            Waves = new List<Wave>
            {
                new Wave(monsterTextures[0], monsterFrmameSizes[0], lifeBar, monsterHitpoints[0], monsterSpeeds[0],
                    monsterArmors[0],
                    monsterBountyes[0], 0, path)
            };
            for (int i = 0; i < monsterTextures.Count; i++)
                Waves.Add(new Wave(
                    monsterTextures[i], monsterFrmameSizes[i], lifeBar, monsterHitpoints[i], monsterSpeeds[i], monsterArmors[i],
                    monsterBountyes[i], monsterCounts[i], path));
            for (int i = 0; i < repeatCount - 1; i++)
                for (int j = 0; j < monsterTextures.Count; j++)
                    Waves.Add(new Wave(
                        monsterTextures[j], monsterFrmameSizes[j], lifeBar, monsterHitpoints[j] + monsterHitpoints[j] * (i + 1) * 0.5f,
                        monsterSpeeds[j], (int)(monsterArmors[j] + monsterArmors[j] * (i + 1) * 0.5),
                        (int)(monsterBountyes[j] + monsterBountyes[j] * (i + 1) * 0.5), monsterCounts[j], path));
            Timer = new Timer(5);
            TRectangle = new TrapRectangle(path, 48);
            this.Money = money;
            this.Lifes = lifes;
        }

        public void Update(GameTime gameTime, List<MonsterSprite> monsterList)
        {
            if (Waves.Count > 0)
            {
                if ((monsterList.Count == 0) && (!Timer.IsRunning))
                {
                    if (Waves[0].MonsterList.Count != 0)
                        Timer.Run(gameTime);
                    Waves.RemoveAt(0);
                }
            }
            Timer.Update(gameTime);
            if (!Timer.IsRunning)
                if ((monsterList.Count == 0) && (Waves.Count > 0))
                    AddMonsters(monsterList);
        }

        public void AddMonsters(List<MonsterSprite> monsterList)
        {
            monsterList.AddRange(Waves[0].MonsterList);
        }

        public void Draw(SpriteBatch spriteBatch, SpriteFont font)
        {
            Timer.Draw(spriteBatch, font);
            spriteBatch.Draw(_environment, new Rectangle(0, 0, 768, 768), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.505f);
            spriteBatch.Draw(_trap, new Rectangle(0, 0, 768, 768), null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0f);
        }

    }
}
