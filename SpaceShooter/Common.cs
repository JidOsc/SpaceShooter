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
        public static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>()
        {
            {"player", null }
            //"enemy", null }
        };

        public static void LoadContent(ContentManager contentManager)
        {
            foreach(string textureName in textures.Keys)
            {
                textures[textureName] = contentManager.Load<Texture2D>(textureName);
            }
        }

        public static float VectorToRotation(Vector2 delta)
        {
            return 1f;
        }

        public static Vector2 RotationToVector(float rotation)
        {
            return new Vector2(1, 0);
        }
    }
}
