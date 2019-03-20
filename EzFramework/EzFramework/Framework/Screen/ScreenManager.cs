using Fabi.EzFramework.Framework.Animations;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Screen
{
    public class ScreenManager
    {
        public static ScreenBase ActualScreen;

        /// <summary>
        /// Changes display to another screen
        /// </summary>
        /// <param name="Screen"></param>
        public static void ChangeTo(ScreenBase Screen)
        {
            if (ActualScreen != null)
            {
                if(ActualScreen is AnimationBase)
                {
                    (ActualScreen as AnimationBase).FadeOut(AnimationType.Linear, 300, () =>
                    {
                        ActualScreen = Screen;
                    });
                }
                else
                {
                    ActualScreen = Screen;
                }
            }
            else
            {                
                if(Screen is AnimationBase)
                {
                    Screen.Opacity = 0;
                    ActualScreen = Screen;
                    (Screen as AnimationBase).FadeIn(AnimationType.Linear, 300);
                }
                else
                {
                    ActualScreen = Screen;
                }
                
            }

        }

        public static void Update(GameTime gameTime)
        {
            ActualScreen?.Update(gameTime);
        }

        public static void Render(GameTime gameTime)
        {
            ActualScreen?.Render(gameTime);
        }
    }
}
