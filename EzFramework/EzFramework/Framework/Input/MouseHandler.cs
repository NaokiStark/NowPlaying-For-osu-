using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Input
{
    public class MouseHandler
    {
        static ButtonState MiddleButton = ButtonState.Released;
        static ButtonState LeftButton = ButtonState.Released;
        static ButtonState RightButton = ButtonState.Released;
        static Vector2 Position = Vector2.Zero;
        static int ScrollWheelValue = 0;

        

        public static MouseEvent GetState()
        {

            MouseState actualState = Mouse.GetState();
            //var actualMode = Screen.ScreenModeManager.GetActualMode();
            
            return new MouseEvent()
            {
                Position = new Vector2(actualState.X, actualState.Y),
                LeftButton = actualState.LeftButton,
                RightButton = actualState.RightButton,
                MiddleButton = actualState.MiddleButton,
                X = actualState.X,
                Y = actualState.Y,
                ScrollWheelValue = actualState.ScrollWheelValue
            };
        }

        public static MouseEvent GetStateNonScaled()
        {

            MouseState actualState = Mouse.GetState();

            return new MouseEvent()
            {
                Position = new Vector2(actualState.X, actualState.Y),
                LeftButton = actualState.LeftButton,
                RightButton = actualState.RightButton,
                MiddleButton = actualState.MiddleButton,
                X = actualState.X,
                Y = actualState.Y,
                ScrollWheelValue = actualState.ScrollWheelValue
            };
        }
        
    }

    public class MouseEvent
    {
        public Vector2 Position { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public ButtonState MiddleButton { get; set; }
        public ButtonState LeftButton { get; set; }
        public ButtonState RightButton { get; set; }
        public int ScrollWheelValue { get; set; }

    }
}
