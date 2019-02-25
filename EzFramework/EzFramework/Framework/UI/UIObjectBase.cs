using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fabi.EzFramework.Framework.Events;
using Fabi.EzFramework.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Fabi.EzFramework.Framework.UI
{
    public class UIObjectBase : IBasicEntity, IUIObject
    {

        public UIObjectBase(string Name)
        {

            TextureColor = Color.White;
            Opacity = 1;
            Scale = 1;
            Visible = true;
            FontColor = Color.Black;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Update Loop
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Update(GameTime gameTime)
        {
            
            UpdateCursorAndKeyboardEvents();
        }

        /// <summary>
        /// Update cursor and keyboard events
        /// </summary>
        private void UpdateCursorAndKeyboardEvents()
        {
            if (!Visible)
                return;

            MouseEvent mouseState = MouseHandler.GetState();
            KeyboardState kbState = KeyboardHandler.GetState();
            var mouseSize = new Vector2(5);

            var mouseRect = new Rectangle((int)mouseState.X, (int)mouseState.Y, (int)mouseSize.X, (int)mouseSize.Y);

            var thisRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);

            //First check position
            if (mouseRect.Intersects(thisRect))
            {
                //Second, check buttons

                //Left
                if (LastMouseCheck.LeftButton == ButtonState.Released && mouseState.LeftButton == ButtonState.Pressed)
                {
                    OnMouseDown?.Invoke(this, mouseState.Position, MouseButtons.Left);
                }

                if (LastMouseCheck.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    OnMouseUp?.Invoke(this, mouseState.Position, MouseButtons.Left);
                    OnMouseClick?.Invoke(this, mouseState.Position, MouseButtons.Left);
                }

                //Right
                if (LastMouseCheck.RightButton == ButtonState.Released && mouseState.RightButton == ButtonState.Pressed)
                {
                    OnMouseDown?.Invoke(this, mouseState.Position, MouseButtons.Right);
                }

                if (LastMouseCheck.RightButton == ButtonState.Pressed && mouseState.RightButton == ButtonState.Released)
                {
                    OnMouseUp?.Invoke(this, mouseState.Position, MouseButtons.Right);
                    OnMouseClick?.Invoke(this, mouseState.Position, MouseButtons.Right);
                }

                //Middle
                if (LastMouseCheck.MiddleButton == ButtonState.Released && mouseState.MiddleButton == ButtonState.Pressed)
                {
                    OnMouseDown?.Invoke(this, mouseState.Position, MouseButtons.Middle);
                }

                if (LastMouseCheck.MiddleButton == ButtonState.Pressed && mouseState.MiddleButton == ButtonState.Released)
                {
                    OnMouseUp?.Invoke(this, mouseState.Position, MouseButtons.Middle);
                    OnMouseClick?.Invoke(this, mouseState.Position, MouseButtons.Middle);
                }

                //Check move

                if (LastMouseCheck.Position != mouseState.Position)
                {
                    OnMouseMove?.Invoke(this, mouseState.Position, MouseButtons.None);
                }

                //Hook Keyboard

                if(kbState.GetPressedKeys().Count() > 0 && LastKeyboardState.GetPressedKeys().Count() > 0)
                {
                    //check pressed keys
                    List<Keys> pressedKeys = kbState.GetPressedKeys().Except(LastKeyboardState.GetPressedKeys()).ToList();

                    foreach(Keys p in pressedKeys)
                    {
                        OnKeyPress?.Invoke(this, p, kbState);
                    }

                    //check released keys
                    List<Keys> releasedKeys = LastKeyboardState.GetPressedKeys().Except(kbState.GetPressedKeys()).ToList();

                    foreach(Keys r in releasedKeys)
                    {
                        OnKeyUp?.Invoke(this, r, kbState);
                        OnKeyPress?.Invoke(this, r, kbState);
                    }
                }               

            }
            else
            {
                // Leave == Release

                //Left
             
            }

            LastMouseCheck = mouseState;
            LastKeyboardState = kbState;
        }

        /// <summary>
        /// Renders before main object render, you can override this method to render things before main texture render
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void RenderBefore(SpriteBatch batch, GameTime gameTime)
        {

        }

        /// <summary>
        /// Renders after main object render, you can override this method to render things after main texture render
        /// </summary>
        /// <param name="gameTime"></param>
        public virtual void RenderAfter(SpriteBatch batch, GameTime gameTime)
        {

        }

        /// <summary>
        /// Render Loop, Is not recommended override this method, use instead "RenderAfter" or "RenderBefore"
        /// </summary>
        /// <param name="gameTime">Game time</param>
        public virtual void Render(GameTime gameTime)
        {
            if (Visible)
                Renderer.Renderer.Instance.RenderObject(this, gameTime);
        }


        #region Variables

        /// <summary>
        /// Auxiliar size
        /// </summary>
        private Vector2? _auxSize = null;

        /// <summary>
        /// Last Mouse Check
        /// </summary>
        public MouseEvent LastMouseCheck = new MouseEvent
        {
            LeftButton = ButtonState.Released,
            RightButton = ButtonState.Released,
            MiddleButton = ButtonState.Released,
            Position = Vector2.Zero,
            ScrollWheelValue = 0,
            X = 0,
            Y = 0
        };

        /// <summary>
        /// Last Keyboard state
        /// </summary>
        public KeyboardState LastKeyboardState = Keyboard.GetState();

        /// <summary>
        /// Text Position
        /// </summary>
        public Vector2 TextPosition { get; set; }

        #endregion

        #region Properties

        /// <summary>
        /// Object Position
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets object size, if isn't defined, returns the original texture size
        /// </summary>
        public Vector2 Size
        {
            get
            {
                if (_auxSize == null)
                {
                    if (Texture == null)
                    {
                        return Vector2.Zero;
                    }
                    else
                    {
                        return new Vector2(Texture.Width, Texture.Height);
                    }
                }

                return _auxSize.GetValueOrDefault();
            }
            set
            {
                _auxSize = value;
            }
        }

        /// <summary>
        /// Object Texture
        /// </summary>
        public Texture2D Texture { get; set; }

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
        /// Object caption || Optional
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// Object Font || Optional
        /// </summary>
        public SpriteFont Font { get; set; }

        /// <summary>
        /// Texture Color
        /// </summary>
        public Color TextureColor { get; set; }

        /// <summary>
        /// Font Color
        /// </summary>
        public Color FontColor { get; set; }

        /// <summary>
        /// Source rectangle
        /// </summary>
        public Rectangle? SourceRectangle { get; set; }

        /// <summary>
        /// Rotation of sprite
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// Render Origin
        /// </summary>
        public Vector2 RenderOrigin { get; set; }
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
