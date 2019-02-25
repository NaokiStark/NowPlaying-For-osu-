using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework
{
    public interface IBasicEntity
    {
        /// <summary>
        /// Object position
        /// </summary>
        Vector2 Position { get; set; }

        /// <summary>
        /// Object Size
        /// </summary>
        Vector2 Size { get; set; }

        /// <summary>
        /// Object Texture
        /// </summary>
        Texture2D Texture { get; set; }

        /// <summary>
        /// Object Visibility
        /// </summary>
        bool Visible { get; set; }

        /// <summary>
        /// Object Opacity
        /// </summary>
        float Opacity { get; set; }

        /// <summary>
        /// Object Scaling
        /// </summary>
        float Scale { get; set; }

        /// <summary>
        /// Update loop
        /// </summary>
        /// <param name="gameTime"></param>
        void Update(GameTime gameTime);

        /// <summary>
        /// Render loop
        /// </summary>
        /// <param name="gameTime"></param>
        void Render(GameTime gameTime);



    }
}
