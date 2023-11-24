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
    internal class InterfaceElement : GameObject
    {

        public InterfaceElement(Vector2 position, Vector2 size, string identifier) : base(position, size, identifier)
        {

        }
    }
}
