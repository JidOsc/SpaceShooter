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
    internal class Button : Label
    {
        Color
            startColor = Color.LightGray,
            currentColor;

        public Button(Vector2 position, Vector2 size, string identifier, string text) : base(position, size, identifier, text)
        {
            texture = Common.textures["projectile"];
            currentColor = startColor;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw(
                texture,
                position,
                new Rectangle(position.ToPoint(), size.ToPoint()),
                currentColor
                );

            _spriteBatch.DrawString(
                font,
                text,
                (position + size / 2 - font.MeasureString(text) / 2),
                Color.Black
                );
        }

        public void Pressed()
        {
            Game1.ExecuteButton(identifier);
        }
        
        public bool MouseInside(Point mousePosition)
        {
            bool mouseInside = 
                mousePosition.X >= hitbox.Location.X &&
                mousePosition.X <= hitbox.Size.X + hitbox.Location.X &&
                mousePosition.Y >= hitbox.Location.Y &&
                mousePosition.Y <= hitbox.Size.Y + hitbox.Location.Y;

            if(mouseInside)
            {
                currentColor = startColor * 0.6f;
            }
            else
            {
                currentColor = startColor;
            }

            return mouseInside;
        }
    }   
}
