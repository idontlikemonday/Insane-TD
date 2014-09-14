using System;
using System.Collections.Generic;
using System.Linq;

namespace TD
{
    public delegate void Action();
    public delegate void Console(string ch);

    public class InputHandler
    {
        public CKeyboard Keyboard = new CKeyboard();
        public void Update()
        {
            Keyboard.Update();
        }
    }
}

