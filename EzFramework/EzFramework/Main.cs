using System;
using System.Collections.Generic;
using System.Linq;
using Fabi.EzFramework.Framework.Renderer;
using Fabi.EzFramework.Framework.Screen;
using Fabi.EzFramework.Framework.Texture;
using Fabi.EzFramework.Tests;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Fabi.EzFramework
{
    //  EzFramework:
    //    EzFramework makes more easy, clean and better code for your games

    //    Please, don't touch this file unless you knowing what are you doing, if you need to reference any
    //    part of this, use App.GameInstance to get this instance reference.
        
    //    Thank you!


    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Main : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Main()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            TextureManager.Content = Content;
            
        }

        protected override void Initialize()
        {
           
            base.Initialize();

            new App.App(this).Initialize();
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //EZ: creates a new Renderer
            new Renderer(spriteBatch);
            
        }

        protected override void UnloadContent()
        {
            App.App.Instance.Unload();
        }
        
        protected override void Update(GameTime gameTime)
        {

            base.Update(gameTime);

            //Update all
            App.App.Instance.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
                        
            base.Draw(gameTime);
            
            //Render All
            App.App.Instance.Render(gameTime);
        }
    }
}
