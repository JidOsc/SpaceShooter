using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    internal class PlayerShip : KineticObject
    {
        Vector2 relationToMouse;

        public PlayerShip(Vector2 position, Vector2 size, string identifier, Vector2 velocity) : base(position, size, identifier, velocity)
        {
            texture = Common.textures["player"];
        }

        new public void Update()
        {
            Vector2 mousePosition = Game1.mouseState.Position.ToVector2();
            relationToMouse = new Vector2(position.X - mousePosition.X, position.Y - mousePosition.Y);

            rotation = (float)Math.Atan2(relationToMouse.Y, relationToMouse.X);
        }
    }
}
