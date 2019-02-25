using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fabi.EzFramework.Framework.Events;
using Fabi.EzFramework.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Fabi.EzFramework.Framework.Screen
{
    public class ScreenBase : IBasicEntity, IScreen
    {
        /// <summary>
        /// Init new Instance
        /// </summary>
        /// <param name="screenName"></param>
        public ScreenBase(string screenName)
        {
            instance = this;
            BackgroundDim = 1;
            Controls = new List<UIObjectBase>();
        }


        /// <summary>
        /// Update Loop
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Update(GameTime gameTime)
        {
            foreach (UIObjectBase control in Controls)
            {
                control.Update(gameTime);
            }
            BackgroundDim = Opacity;
        }

        /// <summary>
        /// Render Loop
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Render(GameTime gameTime)
        {
            Renderer.Renderer.Instance.RenderBackground(this, gameTime);

            foreach (UIObjectBase control in Controls)
                control.Render(gameTime);
        }

        #region Properties

        private static ScreenBase instance;
        public static ScreenBase Instance
        {
            get
            {                
                return instance;
            }
            set
            {
                instance = value;
            }
        }

        /// <summary>
        /// Object Position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Object size
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// Object Texture
        /// </summary>
        public virtual Texture2D Texture { get; set; }

        /// <summary>
        /// Object visibility
        /// </summary>
        public bool Visible { get; set; }

        /// <summary>
        /// Object Opacity
        /// </summary>
        public float Opacity { get; set; }

        /// <summary>
        /// Object Scale
        /// </summary>
        public float Scale { get; set; }

        /// <summary>
        /// Screen Background
        /// </summary>
        public virtual Texture2D Background { get; set; }

        /// <summary>
        /// Screen Opacity
        /// </summary>
        public float BackgroundDim { get; set; }

        /// <summary>
        /// Controls
        /// </summary>
        public List<UIObjectBase> Controls { get; set; }
        #endregion

        #region Events

        /// <summary>
        /// Occurs when mouse is Clicked (Down & Up)
        /// </summary>
        public event MouseEvents.MouseClick OnMouseClick;

        /// <summary>
        /// Occurs when mouse button is released
        /// </summary>
        public event MouseEvents.MouseUp OnMouseUp;

        /// <summary>
        /// Occurs when mouse button is pressed
        /// </summary>
        public event MouseEvents.MouseDown OnMouseDown;

        /// <summary>
        /// Occurs when mouse button moved over object
        /// </summary>
        public event MouseEvents.MouseMove OnMouseMove;

        /// <summary>
        /// Occurs when a key is pressed (Down & Up)
        /// </summary>
        public event KeyboardEvents.KeyPress OnKeyPress;

        /// <summary>
        /// Occurs when a key pressed down
        /// </summary>
        public event KeyboardEvents.KeyDown OnKeyDown;

        /// <summary>
        /// Occurs when a key is released
        /// </summary>
        public event KeyboardEvents.KeyUp OnKeyUp;

        #endregion
    }
}
