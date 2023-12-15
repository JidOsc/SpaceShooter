using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    /* ATT GÖRA LISTA
     *
     * - Tweaka spawnrate
     * - Be om ett namn
     * - Fixa knappen
     * - Efter en viss mängd score får du välja mellan uppgraderingar
     * 
     * om tid
     * - Inte omedelbar rotation
     * - Spara uppgraderingstyper i en fil
     * - Playfab server som sparar highscores
     * - Olika fiendetyper
     * - Stjärnor och planeter i bakgrunden
     * - Ljudeffekter för skott, explosion och att åka
     * - explosionanimation
     * - annan animation för skeppet när den åker
     * 
     */
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static MouseState mouseState;
        public static KeyboardState keyboardState;
        public static Rectangle window;

        static Level level;
        static Menu menu;
        static EndScreen endScreen;

        static Dictionary<string, int> highscores;

        static string scoreFilepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"onslaughtHighscores.txt");

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            CheckIfFileExists();
            Common.LoadContent(Content);
            
            menu = new Menu();
            //level = new Level();

            ToggleFullscreen(true);

            window.Width = _graphics.PreferredBackBufferWidth;
            window.Height = _graphics.PreferredBackBufferHeight;
            window.Location = Window.Position;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (menu != null)
            {
                menu.Update(gameTime);
            }
            else if(level != null)
            {
                level.Update(gameTime);
            }
            else if(endScreen != null)
            {
                endScreen.Update(gameTime);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(sortMode: SpriteSortMode.BackToFront, samplerState: SamplerState.PointClamp);

            if (menu != null)
            {
                menu.Draw(_spriteBatch);
            }
            else if(level != null)
            {
                level.Draw(_spriteBatch);
            }
            else if(endScreen != null)
            {
                endScreen.Draw(_spriteBatch);
            }

            _spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        static void CheckIfFileExists()
        {
            if (File.Exists(scoreFilepath))
            {
                highscores = JsonSerializer.Deserialize<Dictionary<string, int>>(File.ReadAllText(scoreFilepath));
            }
            else
            {
                highscores = new Dictionary<string, int>();
            }
        }

        static void StartLevel()
        {
            level = new Level();
        }

        void ToggleFullscreen(bool toggled)
        {
            Window.IsBorderless = toggled;
            Window.AllowUserResizing = toggled;

            if(toggled)
            {
                _graphics.PreferredBackBufferHeight = 1080;
                _graphics.PreferredBackBufferWidth = 1920;
                _graphics.ApplyChanges();
            }
        }

        public static void ExecuteButton(string button)
        {
            switch(button)
            {
                case "startText":
                    StartLevel();
                    menu = null;
                    break;
                
                case "exitButton":
                    
                    break;

                case "gameoverText":
                    StartLevel();
                    endScreen = null;
                    break;
            }
        }

        public static void GameOver(int score)
        {
            UpdateHighscore(score);

            endScreen = new EndScreen(score);
            endScreen.ShowHighscores(highscores);

            level = null;
        }

        static void AddHighscore(int score)
        {
            highscores.Add(AskForName(), score);
        }

        static void UpdateHighscore(int score)
        {
            if (highscores.Count >= 10)
            {
                if (score > highscores.Values.Min())
                {
                    AddHighscore(score);
                    highscores.Remove(highscores.Keys.Min());
                }
            }
            else
            {
                AddHighscore(score);
            }

            highscores = highscores.OrderByDescending(key => key.Value).ToDictionary(key => key.Key, key => key.Value);
            SaveHighscore();
        }

        static void SaveHighscore()
        {
            File.WriteAllText(scoreFilepath, JsonSerializer.Serialize(highscores));
        }

        static string AskForName()
        {
            string tempString = "";

            for(int i = 0; i < 5; i++)
            {
                tempString += Convert.ToChar(Common.random.Next(97, 122));
            }
            return tempString;
        }
    }
}