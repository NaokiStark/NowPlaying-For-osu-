using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Events
{
    public static class MouseEvents
    {
        /// <summary>
        /// Mouse Click event handler (Down & Up)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="mousePosition">MousePosition</param>
        /// <param name="mouseButton">Button Pressed</param>
        public delegate void MouseClick(object sender, Vector2 mousePosition, MouseButtons mouseButton);

        /// <summary>
        /// Mouse up event handler (Down & Up)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="mousePosition">MousePosition</param>
        /// <param name="mouseButton">Button Pressed</param>
        public delegate void MouseUp(object sender, Vector2 mousePosition, MouseButtons mouseButton);

        /// <summary>
        /// Mouse down event handler (Down & Up)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="mouseButton">Button Pressed</param>
        public delegate void MouseDown(object sender, Vector2 mousePosition, MouseButtons mouseButton);

        /// <summary>
        /// Mouse move event handler (Down & Up)
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="mouseButton">Button Pressed</param>
        public delegate void MouseMove(object sender, Vector2 mousePosition, MouseButtons mouseButton);

    }
}
