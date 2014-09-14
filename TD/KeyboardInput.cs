using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    class KeyboardInput
    {
        readonly InputHandler _input;
        string _exp = "";
        bool _toUpper = false;

        public KeyboardInput()
        {
            _input = new InputHandler();
            _input.Keyboard.Add(Keys.LeftShift, delegate { _toUpper = true; }, "down");
            _input.Keyboard.Add(Keys.A, delegate { _exp = _toUpper ? "A" : "a"; }, "pressed");
            _input.Keyboard.Add(Keys.B, delegate { _exp = _toUpper ? "B" : "b"; }, "pressed");
            _input.Keyboard.Add(Keys.C, delegate { _exp = _toUpper ? "C" : "c"; }, "pressed");
            _input.Keyboard.Add(Keys.D, delegate { _exp = _toUpper ? "D" : "d"; }, "pressed");
            _input.Keyboard.Add(Keys.E, delegate { _exp = _toUpper ? "E" : "e"; }, "pressed");
            _input.Keyboard.Add(Keys.F, delegate { _exp = _toUpper ? "F" : "f"; }, "pressed");
            _input.Keyboard.Add(Keys.G, delegate { _exp = _toUpper ? "G" : "g"; }, "pressed");
            _input.Keyboard.Add(Keys.H, delegate { _exp = _toUpper ? "H" : "h"; }, "pressed");
            _input.Keyboard.Add(Keys.I, delegate { _exp = _toUpper ? "I" : "i"; }, "pressed");
            _input.Keyboard.Add(Keys.J, delegate { _exp = _toUpper ? "J" : "j"; }, "pressed");
            _input.Keyboard.Add(Keys.K, delegate { _exp = _toUpper ? "K" : "k"; }, "pressed");
            _input.Keyboard.Add(Keys.L, delegate { _exp = _toUpper ? "L" : "l"; }, "pressed");
            _input.Keyboard.Add(Keys.M, delegate { _exp = _toUpper ? "M" : "m"; }, "pressed");
            _input.Keyboard.Add(Keys.N, delegate { _exp = _toUpper ? "N" : "n"; }, "pressed");
            _input.Keyboard.Add(Keys.O, delegate { _exp = _toUpper ? "O" : "o"; }, "pressed");
            _input.Keyboard.Add(Keys.P, delegate { _exp = _toUpper ? "P" : "p"; }, "pressed");
            _input.Keyboard.Add(Keys.Q, delegate { _exp = _toUpper ? "Q" : "q"; }, "pressed");
            _input.Keyboard.Add(Keys.R, delegate { _exp = _toUpper ? "R" : "r"; }, "pressed");
            _input.Keyboard.Add(Keys.S, delegate { _exp = _toUpper ? "S" : "s"; }, "pressed");
            _input.Keyboard.Add(Keys.T, delegate { _exp = _toUpper ? "T" : "t"; }, "pressed");
            _input.Keyboard.Add(Keys.U, delegate { _exp = _toUpper ? "U" : "u"; }, "pressed");
            _input.Keyboard.Add(Keys.V, delegate { _exp = _toUpper ? "V" : "v"; }, "pressed");
            _input.Keyboard.Add(Keys.W, delegate { _exp = _toUpper ? "W" : "w"; }, "pressed");
            _input.Keyboard.Add(Keys.X, delegate { _exp = _toUpper ? "X" : "x"; }, "pressed");
            _input.Keyboard.Add(Keys.Y, delegate { _exp = _toUpper ? "Y" : "y"; }, "pressed");
            _input.Keyboard.Add(Keys.Z, delegate { _exp = _toUpper ? "Z" : "z"; }, "pressed");
            _input.Keyboard.Add(Keys.Back, delegate { _exp = "BACKSPACE"; }, "pressed");
        }

        public string ReadKey()
        {
            _input.Keyboard.Update();
            string temp = _exp;
            _exp = "";
            _toUpper = false;
            return temp;
        }
    }
}
