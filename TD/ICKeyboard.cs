using System;

namespace TD
{
    interface ICKeyboard
    {
        void Add(Microsoft.Xna.Framework.Input.Keys key, Action action, string param);
        void Add(Microsoft.Xna.Framework.Input.Keys key);
        bool IsKeyDown(Microsoft.Xna.Framework.Input.Keys key);
        bool IsKeyPressed(Microsoft.Xna.Framework.Input.Keys key);
        bool IsKeyReleased(Microsoft.Xna.Framework.Input.Keys key);
        bool IsKeyUp(Microsoft.Xna.Framework.Input.Keys key);
        void Update();
    }
}
