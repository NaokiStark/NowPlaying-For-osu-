using Fabi.EzFramework.Framework.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.UI
{
    public interface IUIObject
    {
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

        /// <summary>
        /// Object caption 
        /// </summary>
        string Caption { get; set; }

        /// <summary>
        /// Object Font
        /// </summary>
        SpriteFont Font { get; set; }

        /// <summary>
        /// Texture Color
        /// </summary>
        Color TextureColor { get; set; }

        /// <summary>
        /// Font Color
        /// </summary>
        Color FontColor { get; set; }

        /// <summary>
        /// Renders before main object render
        /// </summary>
        /// <param name="gameTime"></param>
        void RenderBefore(SpriteBatch batch, GameTime gameTime);

        /// <summary>
        /// Renders after main object render
        /// </summary>
        /// <param name="gameTime"></param>
        void RenderAfter(SpriteBatch batch, GameTime gameTime);
    }
}
