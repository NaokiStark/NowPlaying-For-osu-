using Fabi.EzFramework.Framework.Screen;
using Fabi.EzFramework.Framework.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Renderer
{
    public class Renderer
    {

        /// <summary>
        /// Initialzes Renderer
        /// </summary>
        /// <param name="batch">Main SpriteBatch</param>
        public Renderer(SpriteBatch batch)
        {
            Instance = this;
            _batch = batch;
            Sampler_State = SamplerState.AnisotropicClamp;
            Sprite_SortMode = SpriteSortMode.Immediate;
        }


        /// <summary>
        /// Initializes SpriteBatch
        /// </summary>
        public void Begin()
        {
            _batch.Begin(Sprite_SortMode, BlendState.AlphaBlend, Sampler_State, DepthStencilState.Default, RasterizerState.CullNone);
        }

        /// <summary>
        /// Finalizes SpriteBatch
        /// </summary>
        public void End()
        {
            _batch.End();
        }

        /// <summary>
        /// Render object
        /// </summary>
        /// <param name="obj">Object to render</param>
        /// <param name="gameTime">Game time</param>
        public void RenderObject(UIObjectBase obj, GameTime gameTime)
        {

            obj.RenderBefore(_batch, gameTime);

            if (obj.Texture != null)
            {
                var destRect = new Rectangle((int)obj.Position.X, (int)obj.Position.Y, (int)(obj.Size.X * obj.Scale), (int)(obj.Size.Y * obj.Scale));
                _batch.Draw(obj.Texture, destRect, obj.SourceRectangle, obj.TextureColor * obj.Opacity, obj.Rotation, obj.RenderOrigin, SpriteEffects.None, 0f);
            }

            if (!string.IsNullOrWhiteSpace(obj.Caption))
                _batch.DrawString(obj.Font ?? _defaultFont, obj.Caption, obj.Position + obj.TextPosition, obj.FontColor * obj.Opacity);

            obj.RenderAfter(_batch, gameTime);
        }

        public void RenderBackground(ScreenBase obj, GameTime gameTime)
        {
            if (obj.Background == null)
                return;

            float screenWidth = Main.graphics.PreferredBackBufferWidth;
            float screenHeight = Main.graphics.PreferredBackBufferHeight;

            var screenRectangle = new Rectangle((int)screenWidth / 2, (int)screenHeight / 2, (int)(((float)obj.Background.Width / (float)obj.Background.Height) * (float)screenHeight), (int)screenHeight);

            if (screenRectangle.Width < screenWidth)
            {
                screenRectangle = new Rectangle(screenRectangle.X, screenRectangle.Y, (int)screenWidth, (int)(((float)obj.Background.Height / (float)obj.Background.Width) * (float)screenWidth));
            }

            _batch.Draw(obj.Background, screenRectangle, null, Color.White * obj.BackgroundDim, 0, new Vector2(obj.Background.Width / 2, obj.Background.Height / 2), SpriteEffects.None, 0);

        }

        /// <summary>
        /// Returns Main batch instance
        /// </summary>
        /// <returns></returns>
        public SpriteBatch GetBatch()
        {
            return _batch;
        }

        /// <summary>
        /// Make sure to set instance before
        /// </summary>
        public static Renderer Instance = null;

        /// <summary>
        /// Main batch Access
        /// </summary>
        private SpriteBatch _batch;

        /// <summary>
        /// Gets or set SamplerState, Default: AnisotropicClamp
        /// </summary>
        public SamplerState Sampler_State { get; set; }

        /// <summary>
        /// Gets or set SpriteSortMode, Default: Inmediate
        /// </summary>
        public SpriteSortMode Sprite_SortMode { get; set; }

        /// <summary>
        /// Gets default font
        /// </summary>
        private SpriteFont _defaultFont { get; set; }
    }
}
