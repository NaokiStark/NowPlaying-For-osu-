using Fabi.EzFramework.Framework.Screen;
using Fabi.EzFramework.Framework.Texture;
using Fabi.EzFramework.Framework.UI.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.App.Screens
{
    public class NowPlayingScreen : ScreenBase
    {
        Label nowPlayingText;

        int maxCheckProcess = 1000;
        int maxCheckElapsed = 0;
        
        public NowPlayingScreen(string screenName) : base(screenName)
        {
            SpriteFont defaultFont = TextureManager.GetFont("sniglet");

            nowPlayingText = new Label("nptxt")
            {
                Caption = "Waiting for osu!",
                Font = defaultFont,
                Position = new Vector2(10, 10),
                Marquee = true,
                FontColor = Color.White,
                Size = new Vector2(Main.graphics.PreferredBackBufferWidth, 0),
                MarqueeMs = 100,
                Scale = 1.2f,
            };

            Controls.Add(nowPlayingText);
        }

        public override void Update(GameTime gameTime)
        {
            nowPlayingText.Size = new Vector2(Main.graphics.PreferredBackBufferWidth, 0);

            maxCheckElapsed += gameTime.ElapsedGameTime.Milliseconds;
            if (maxCheckElapsed >= maxCheckProcess)
            {
                maxCheckElapsed = 0;
                checkOsuTitle();
            }                      

            base.Update(gameTime);
        }

        private void checkOsuTitle()
        {
            Process[] processes = Process.GetProcessesByName("osu!");

            if(processes.Length < 1)
            {
                nowPlayingText.Caption = "Waiting for osu!";
                return;
            }

            string wndTitle = processes[0].MainWindowTitle;

            string[] clipped = wndTitle.Split(new string[] { "osu!  - ", "osu! - " }, StringSplitOptions.None);

            if(clipped.Length < 2)
            {
                nowPlayingText.Caption = "Idle";
                return;
            }

            nowPlayingText.Caption = clipped[1];
        }
    }
}
