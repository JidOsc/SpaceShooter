using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace SpaceShooter
{
    internal class Menu
    {
        Dictionary<string, List<InterfaceElement>> elements;

        public Menu(){
            elements = new()
            {
                {"labels", new List<InterfaceElement>(){
                    new Label(
                        new Vector2(50, 50),
                        new Vector2(1, 1),
                        "titleText",
                        "Onslaught"
                    )
                }},

                {"buttons", new List<InterfaceElement>(){
                    new Button(
                        new Vector2(50, 200),
                        new Vector2(300, 75),
                        "startText",
                        "Start"
                    ),

                    new Button(
                        new Vector2(50, 350),
                        new Vector2(300, 75),
                        "quitText",
                        "Quit")
                }}
            };
        }

        public void Update(GameTime gameTime)
        {
            foreach(List<InterfaceElement> list in elements.Values)
            {
                foreach(InterfaceElement element in list)
                {
                    element.Update(gameTime);
                }
            }

            foreach(Button button in elements["buttons"])
            {
                if(button.MouseInside(Game1.mouseState.Position))
                {
                    if(button.IsPressed())
                    {
                        button.Pressed();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(List<InterfaceElement> list in elements.Values)
            {
                foreach(InterfaceElement element in list)
                {
                    element.Draw(spriteBatch);
                }
            }
        }
    }
}
