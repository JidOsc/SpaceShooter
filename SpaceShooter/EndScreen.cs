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
    internal class EndScreen
    {
        Dictionary<string, List<InterfaceElement>> elements = new()
            {
                {"labels", new List<InterfaceElement>()
                    {
                    new Label(
                        new Vector2(Game1.window.Size.X / 2, 100),
                        new Vector2(1, 1),
                        "gameoverText",
                        "Highscores: "
                        ),

                    new Label(
                        new Vector2(Game1.window.Size.X / 2, 50),
                        new Vector2(1, 1),
                        "gameoverText",
                        "Game Over"
                        )
                    }
                },

                {"buttons", new List<InterfaceElement>()
                    {
                    new Button(
                        new Vector2(Game1.window.Size.X / 2 - 100, 600),
                        new Vector2(200, 50),
                        "gameoverText",
                        "Retry?"
                        )
                    }
                }
            };

        public EndScreen(int score)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            foreach (List<InterfaceElement> list in elements.Values)
            {
                foreach (InterfaceElement element in list)
                {
                    element.Update(gameTime);
                }
            }

            foreach (Button button in elements["buttons"])
            {
                if (button.MouseInside(Game1.mouseState.Position))
                {
                    if (Game1.mouseState.LeftButton == ButtonState.Pressed)
                    {
                        button.Pressed();
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (List<InterfaceElement> list in elements.Values)
            {
                foreach (InterfaceElement element in list)
                {
                    element.Draw(spriteBatch);
                }
            }
        }

        public void ShowHighscores(Dictionary<string, int> highscores)
        {
            Label highscoreList = (Label)elements["labels"][0];

            highscoreList.text = "Highscores: \n";

            for(int i = 0; i < highscores.Count; i++)
            {
                highscoreList.text += highscores.ElementAt(i).Key + ": " + highscores.ElementAt(i).Value + "\n";
            }
        }
    }
}
