using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    internal class Button : Label
    {
        Color
            startColor = Color.LightGray, //färg på bakgrund
            currentColor;

        public Button(Vector2 position, Vector2 size, string identifier, string text) : base(position, size, identifier, text)
        {
            texture = Common.textures["projectile"]; //enfärgade bakgrunden använder samma enpixliga textur
            currentColor = startColor;
            layer = 0;
        }

        public override void Draw(SpriteBatch _spriteBatch)
        {
            _spriteBatch.Draw( //ritar ut bakgrunden, sen texten på knappen
                texture,
                position,
                new Rectangle(position.ToPoint(), size.ToPoint()),
                currentColor, 
                0, 
                Vector2.Zero, 
                1, 
                SpriteEffects.None, 
                layer + 0.01f
                );

            _spriteBatch.DrawString(
                font,
                text,
                (position + size / 2 - font.MeasureString(text) / 2),
                Color.Black, 
                0, 
                Vector2.Zero, 
                1, 
                SpriteEffects.None, 
                layer
                );
        }

        public void Pressed()
        {
            Game1.ExecuteButton(identifier); //skickar signal till Game1 om att göra något
        }
        
        public bool IsPressed()
        {
            return Game1.mouseState.LeftButton == ButtonState.Pressed && Game1.previousMouseState.LeftButton != ButtonState.Pressed; //musen nedtryckt för första gången
        }

        public bool MouseInside(Point mousePosition)
        {
            bool mouseInside = 
                mousePosition.X >= hitbox.Location.X &&
                mousePosition.X <= hitbox.Size.X + hitbox.Location.X &&
                mousePosition.Y >= hitbox.Location.Y &&
                mousePosition.Y <= hitbox.Size.Y + hitbox.Location.Y; //höger om vänsterkant, under överkant osv

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
