using Fabi.EzFramework.Framework.Texture;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.UI.Controls
{
    public class Label : Animations.AnimationObject
    {
        public bool Marquee { get; set; }


        private string captionHelper = "";
        private string captionMarqueeHelper = "";

        public int MarqueeMs = 100;
        private int __marqueeElapsed = 0;

        private Vector2 _measuredText;

        public override string Caption
        {
            get
            {
                return captionHelper;
            }
            set
            {
                if(value == captionHelper)
                {
                    return;
                }
                __marqueeStr = new StringBuilder(value + "            ");
                captionHelper = value;
                _measuredText = Font.MeasureString(value);
            }
        }

        StringBuilder __marqueeStr;

        public Label(string Name) : base(Name)
        {
            Font = TextureManager.GetFont("default");
            Caption = Name;

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Marquee && _measuredText.X > Size.X)
            {
                __marqueeElapsed += gameTime.ElapsedGameTime.Milliseconds;
                if(__marqueeElapsed >= MarqueeMs)
                {
                    __marqueeElapsed = 0;
                    __marqueeStr.Append(__marqueeStr[0]);
                    __marqueeStr.Remove(0, 1);
                    captionMarqueeHelper = __marqueeStr.ToString();                    
                }
            }
        }

        public override void Render(GameTime gameTime)
        {
            if (!Marquee || _measuredText.X < Size.X)
            {
                base.Render(gameTime);
            }
            else
            {
                //Save caption
                string cC = captionHelper;

                captionHelper = captionMarqueeHelper;

                base.Render(gameTime);

                //Return caption
                captionHelper = cC;
                
            }
            
        }

    }
}
