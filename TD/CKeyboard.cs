using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace TD
{
    public class CKeyboard : ICKeyboard
    {
        public Console PutChar;

        public class Key
        {
            public bool Curr = false;
            public bool Prev = false;
            public Action Action = null;
            public string Param = null;
        }

        public KeyboardState State;
        public Dictionary<Keys, Key> KeyList = new Dictionary<Keys, Key>();       
        
        /// <summary>
        /// Добавляет клавишу в список прослушивания
        /// </summary>
        /// <param name="key">Клавиша для прослушивания</param> 
        public void Add(Keys key)
        {
            KeyList.Add(key, new Key());
        }

        /// <summary>
        /// Добавляет клавишу в список прослушивания и привязывает ее к 
        /// делегату
        /// </summary>
        /// <param name="key">Клавиша для прослушивания</param>      
        /// <param name="action">Метод или универсальный делегат, к которому
        /// следует привязать клавишу</param>
        /// <param name="param">Параметр прослушивания: "down", "up", "pressed"
        /// или "released"</param>   
        public void Add(Keys key, Action action, string param)
        {
            KeyList.Add(key, new Key { Action = action, Param = param });
        }

        /// <summary>
        /// Возвращает true, если клавиша зажата
        /// </summary>
        /// <param name="key">Клавиша для проверки</param>       
        /// <returns>True, если клавиша зажата
        /// </returns>
        public bool IsKeyDown(Keys key)
        {   
            return State.IsKeyDown(key); 
        }

        /// <summary>
        /// Возвращает true, если клавиша не зажата
        /// </summary>
        /// <param name="key">Клавиша для проверки</param>       
        /// <returns>True, если клавиша не зажата
        /// </returns>
        public bool IsKeyUp(Keys key)
        {
            return State.IsKeyUp(key);
        }

        /// <summary>
        /// Возвращает true, если произошло событие нажатия на клавишу
        /// </summary>
        /// <param name="key">Клавиша для проверки</param>       
        /// <returns>True, если произошло событие нажатия на клавишу
        /// </returns>
        public bool IsKeyPressed(Keys key)
        {            
            Key K = KeyList[key];    
            if (K.Prev == false && K.Curr == true)
                return true;
            else
                return false;            
        }

        /// <summary>
        /// Возвращает true, если произошло событие отпускания клавиши
        /// </summary>
        /// <param name="key">Клавиша для проверки</param>       
        /// <returns>True, если произошло событие отпускания клавиши
        /// </returns>
        public bool IsKeyReleased(Keys key)
        {
            Key K = KeyList[key];
            if (K.Prev == true && K.Curr == false)
                return true;
            else 
                return false;
        }

        /// <summary>
        /// Прослушивает клавиши из списка
        /// </summary>
        public void Update()
        {
            State = Keyboard.GetState();

            foreach (KeyValuePair<Keys,Key> K in KeyList)
            {
                K.Value.Prev = K.Value.Curr;
                if (State.IsKeyDown(K.Key))
                    K.Value.Curr = true;
                else
                    K.Value.Curr = false;
                
                if (K.Value.Action != null)
                {
                    switch (K.Value.Param)
                    {
                        case "pressed":
                            {
                                if (K.Value.Prev == false && K.Value.Curr == true)
                                    K.Value.Action();
                                break;
                            }

                        case "released":
                            {
                                if (K.Value.Prev == true && K.Value.Curr == false)
                                    K.Value.Action();
                                break;
                            }

                        case "down":
                            {
                                if (K.Value.Curr == true)
                                    K.Value.Action();
                                break;
                            }

                        case "up":
                            {
                                if (K.Value.Curr == false)
                                    K.Value.Action();
                                break;
                            }
                    }
                }
            }
        }
    }
}
