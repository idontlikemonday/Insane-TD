using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class Wave
    {
        public Texture2D Monster { get; set; }
        public List<MonsterSprite> MonsterList { get; set; }

        public Wave(Texture2D monster,
                    Point frameSize,
                    Texture2D lifeBar,
                    float hitPoints,
                    Vector2 speed,
                    int armor,
                    int bounty,
                    int count,
                    List<Vector2> path)
        {
            this.Monster = monster;
            MonsterList = new List<MonsterSprite>();
            var rand = new Random(DateTime.Today.Millisecond);
            for (int i = 0; i < count; i++)
            {
                MonsterList.Add(new MonsterSprite(monster,
                    new Vector2(rand.Next((int) path[0].X - 100, (int) path[0].X - 100), rand.Next(-500, 0)), frameSize,
                    4, new Point(0, 0),
                    new Point(1, 1), speed, "", hitPoints,
                    lifeBar, armor, new List<Vector2>(path), bounty));
            }
            SortMonsterList();
        }

        public void SortMonsterList()
        {
            var temp = new List<MonsterSprite>();
            while (MonsterList.Count > 0)
            {
                float min = MonsterList[0].Position.Y;
                int minIndex = 0;
                for (int i = 0; i < MonsterList.Count; i++)
                    if (MonsterList[i].Position.Y < min)
                    {
                        min = MonsterList[i].Position.Y;
                        minIndex = i;
                    }
                temp.Add(MonsterList[minIndex]);
                temp.Last().Distance += 7*(temp.Count - 1);
                MonsterList.RemoveAt(minIndex);
            }
            MonsterList = temp;
        }

    }
}
