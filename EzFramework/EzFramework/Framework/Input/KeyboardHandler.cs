using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Input
{
    public class KeyboardHandler
    {
        /// <summary>
        /// Gets keyboard state
        /// </summary>
        /// <returns>A KeyboardState object</returns>
        public static KeyboardState GetState()
        {
            return Keyboard.GetState();
        }

    }
}
