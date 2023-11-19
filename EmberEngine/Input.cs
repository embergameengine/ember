using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmberEngine
{
    public static class Input
    {
        static internal IKeyboard keyboard;

        public static bool GetKey(Key key)
        {
            return keyboard.IsKeyPressed(key);
        }
    }
}
