using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

namespace SpaceShooter
{
    internal class Common
    {
        static public Random random = new Random();

        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>()
        {
            {"player2", null },
            {"projectile", null},
            {"enemy2", null }
        };

        public static Dictionary<string, SpriteFont> fonts = new Dictionary<string, SpriteFont>()
        {
            {"scoreFont", null},
            {"titleFont", null},
            {"startFont", null},
            {"gameoverFont", null}
        };

        public static void LoadContent(ContentManager contentManager)
        {
            foreach(string textureName in textures.Keys)
            {
                textures[textureName] = contentManager.Load<Texture2D>(textureName);
            }
            foreach(string fontName in fonts.Keys)
            {
                fonts[fontName] = contentManager.Load<SpriteFont>(fontName);
            }
        }

        public static float VectorToRotation(Vector2 delta)
        {
            return (float)Math.Atan2(delta.Y, delta.X);
        }

        public static Vector2 RotationToVector(float rotation)
        {
            return new Vector2(1, 0);
        }
    }
}
