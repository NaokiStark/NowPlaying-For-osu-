using Fabi.EzFramework.App.Screens;
using Fabi.EzFramework.Framework.Renderer;
using Fabi.EzFramework.Framework.Screen;
using Fabi.EzFramework.Tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.App
{
    public class App
    {

        public Game GameInstance;
        public static App Instance;
             

        public App(Game gameInstance)
        {
            Instance = this;
            GameInstance = gameInstance;
            
        }

        public void Initialize()
        {
            ScreenManager.ChangeTo(new NowPlayingScreen("NowPlayingScreen"));
            GameInstance.IsMouseVisible = true;
            Main.graphics.PreferredBackBufferHeight = 40;
            Main.graphics.PreferredBackBufferWidth = 400;
            Main.graphics.ApplyChanges();
        }
        
        public void Update(GameTime gameTime)
        {

            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                GameInstance.Exit();

            ScreenManager.Update(gameTime);
        }

        public void Render(GameTime gameTime)
        {
            Renderer.Instance.Begin();
            ScreenManager.Render(gameTime);
            Renderer.Instance.End();
        }

        internal void Unload()
        {
            
        }
    }
}
