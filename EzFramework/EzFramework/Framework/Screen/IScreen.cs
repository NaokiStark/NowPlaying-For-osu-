using Fabi.EzFramework.Framework.Events;
using Fabi.EzFramework.Framework.UI;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Screen
{
    public interface IScreen
    {

        /// <summary>
        /// Screen Background
        /// </summary>
        Texture2D Background { get; set; }
        
        /// <summary>
        /// Background Opacity
        /// </summary>
        float BackgroundDim { get; set; }

        #region MouseEvents

        /// <summary>
        /// Occurs when mouse is Clicked (Down & Up)
        /// </summary>
        event MouseEvents.MouseClick OnMouseClick;

        /// <summary>
        /// Occurs when mouse button is released
        /// </summary>
        event MouseEvents.MouseUp OnMouseUp;

        /// <summary>
        /// Occurs when mouse button is pressed
        /// </summary>
        event MouseEvents.MouseDown OnMouseDown;

        /// <summary>
        /// Occurs when mouse button moved over object
        /// </summary>
        event MouseEvents.MouseMove OnMouseMove;

        #endregion

        #region KeyboardEvents

        /// <summary>
        /// Occurs when a key is pressed (Down & Up)
        /// </summary>
        event KeyboardEvents.KeyPress OnKeyPress;

        /// <summary>
        /// Occurs when a key pressed down
        /// </summary>
        event KeyboardEvents.KeyDown OnKeyDown;

        /// <summary>
        /// Occurs when a key is released
        /// </summary>
        event KeyboardEvents.KeyUp OnKeyUp;

        #endregion

        List<UIObjectBase> Controls { get; set; }
    }
}
