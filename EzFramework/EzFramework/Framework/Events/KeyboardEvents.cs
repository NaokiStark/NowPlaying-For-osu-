using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Events
{
    public static class KeyboardEvents
    {
        /// <summary>
        /// Keyboard Press action (Up & Down)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="key">key pressed</param>
        /// <param name="keyboardState">keyboard state</param>
        public delegate void KeyPress(object sender, Keys key, KeyboardState keyboardState);

        /// <summary>
        /// Keyboard Down action
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="key">key pressed</param>
        /// <param name="keyboardState">keyboard state</param>
        public delegate void KeyDown(object sender, Keys key, KeyboardState keyboardState);

        /// <summary>
        /// Keyboard Up action
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="key">key pressed</param>
        /// <param name="keyboardState">keyboard state</param>
        public delegate void KeyUp(object sender, Keys key, KeyboardState keyboardState);
    }
}
