using Fabi.EzFramework.Framework.Animations;
using Fabi.EzFramework.Framework.Events;
using Fabi.EzFramework.Framework.Renderer;
using Fabi.EzFramework.Framework.Screen;
using Fabi.EzFramework.Framework.Texture;
using Fabi.EzFramework.Framework.UI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Tests
{
    public class ScreenTest : AnimationBase
    {
        Button btn1;
        Button btn2;

        public ScreenTest(string screenName) : base(screenName)
        {
            SpriteFont defaultFont = TextureManager.GetFont("sniglet");
            btn1 = new Button("btn1") {
                Caption = "Hello World",
                FontColor = Color.White,
                Font = defaultFont,
                Size = new Vector2(defaultFont.MeasureString("Hello World").X + 20, defaultFont.MeasureString("Hello World").Y + 20),
            };

            btn1.OnMouseDown += Btn1_OnMouseDown;
            btn1.OnMouseUp += Btn1_OnMouseUp;
            btn1.OnKeyDown += Btn1_OnKeyDown;
            btn1.OnKeyUp += Btn1_OnKeyUp;
            btn1.OnMouseMove += Btn1_OnMouseMove;


            btn2 = new Button("btn2") {
                Caption = "uwu",
                FontColor = Color.White,
                Visible = true,
                Font = defaultFont,
                Opacity = 0,
                Position = new Vector2(50, 50),
            };

            Controls.Add(btn1);
            Controls.Add(btn2);
            
        }

        private void Btn1_OnMouseMove(object sender, Vector2 mousePosition, MouseButtons mouseButton)
        {
            btn2.Position = mousePosition;
           
        }

        private void Btn1_OnKeyUp(object sender, Keys key, KeyboardState keyboardState)
        {
            btn2.FadeOut(Framework.Animations.AnimationType.Ease, 200);
        }

        private void Btn1_OnKeyDown(object sender, Keys key, KeyboardState keyboardState)
        {
            btn2.Caption = key.ToString();
            btn2.FadeIn(Framework.Animations.AnimationType.Ease, 200);
        }

        private void Btn1_OnMouseUp(object sender, Vector2 mousePosition, MouseButtons mouseButton)
        {
            btn2.FadeOut(Framework.Animations.AnimationType.Ease, 200);

        }

        private void Btn1_OnMouseDown(object sender, Vector2 mousePosition, MouseButtons mouseButton)
        {
            
            btn2.Caption = $"Mouse down: {mouseButton.ToString()}";
            btn2.FadeIn(Framework.Animations.AnimationType.Ease, 200);

        }
    }
}
