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
    internal class Label : InterfaceElement
    {
        public string
            text;

        public Label(Vector2 position, Vector2 size, string identifier, string text) : base(position, size, identifier)
        {
            this.text = text;
        }
    }
}
