using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fabi.EzFramework.Framework.Texture
{
    public class TextureManager
    {
        /// <summary>
        /// Texture dict
        /// </summary>
        public static Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public static Dictionary<string, SpriteFont> Fonts = new Dictionary<string, SpriteFont>();


        public static ContentManager Content;

        /// <summary>
        /// Get loaded texture or loads if not loaded from Content
        /// </summary>
        /// <param name="texture">Texture file name</param>
        /// <param name="refresh">Refresh texture option</param>
        /// <param name="Content">Content Manager</param>
        /// <returns></returns>
        public static Texture2D GetTexture(string texture, bool refresh = false)
        {

            try
            {
                if (!_loaded(Textures, texture) || refresh)
                {
                    _addOrUpdate(Textures, texture, Content.Load<Texture2D>(texture));
                }

                return _getTexture(Textures, texture);
            }
            catch
            {
                return null; //ToDo, add Fail texture here
            }

        }

        /// <summary>
        ///  Get loaded font or loads if not loaded from Content
        /// </summary>
        /// <param name="font"></param>
        /// <returns></returns>
        public static SpriteFont GetFont(string font)
        {
            try
            {
                if (!_loaded(Fonts, font))
                {
                    return _addOrUpdate(Fonts, font, Content.Load<SpriteFont>(font));
                }

                return _getFont(Fonts, font);
            }
            catch
            {
                return null; //ToDo, add Fail texture here
            }

        }

        private static bool _loaded(Dictionary<string, Texture2D> dic, string key)
        {

            if (dic.TryGetValue(key, out Texture2D val))
            {
                return true;
            }

            return false;
        }

        private static bool _loaded(Dictionary<string, SpriteFont> dic, string key)
        {

            if (dic.TryGetValue(key, out SpriteFont val))
            {
                return true;
            }

            return false;
        }

        private static Texture2D _getTexture(Dictionary<string, Texture2D> dic, string key)
        {
            Texture2D val;
            if (dic.TryGetValue(key, out val))
            {
                return val;
            }

            return null;
        }

        private static SpriteFont _getFont(Dictionary<string, SpriteFont> dic, string key)
        {
            SpriteFont val;
            if (dic.TryGetValue(key, out val))
            {
                return val;
            }

            return null;
        }

        private static Texture2D _addOrUpdate(Dictionary<string, Texture2D> dic, string key, Texture2D value)
        {
            Texture2D val;
            if (dic.TryGetValue(key, out val))
            {
                // yay, value exists!
                dic[key] = value;
                return dic[key];
            }
            else
            {
                // darn, lets add the value
                dic.Add(key, value);
                return value;
            }
        }

        private static SpriteFont _addOrUpdate(Dictionary<string, SpriteFont> dic, string key, SpriteFont value)
        {
            SpriteFont val;
            if (dic.TryGetValue(key, out val))
            {
                // yay, value exists!
                dic[key] = value;
                return dic[key];
            }
            else
            {
                // darn, lets add the value
                dic.Add(key, value);
                return value;
            }
        }
    }
}
