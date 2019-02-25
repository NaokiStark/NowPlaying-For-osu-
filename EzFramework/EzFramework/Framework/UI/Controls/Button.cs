using Fabi.EzFramework.Framework.Animations;
using Fabi.EzFramework.Framework.Texture;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.UI.Controls
{
    public class Button : AnimationObject
    {
        string auxString = "";
        Vector2 textBounds = Vector2.Zero;
        public Button(string Name) : base(Name)
        {
            Caption = Name;
            Texture = TextureManager.GetTexture("button");
            Font = TextureManager.GetFont("default");
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if(auxString != Caption)
            {
                var measured = Font.MeasureString(Caption);
                textBounds = new Vector2(Size.X / 2- measured.X / 2, Size.Y / 2 - measured.Y / 2);
                auxString = Caption;
            }

            TextPosition = textBounds;
        }
    }
}
