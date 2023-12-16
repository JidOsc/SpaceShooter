using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System.Timers;

namespace SpaceShooter
{
    internal class Upgrade : Button
    {
        public ShipStats statsModifier = new();
        string description = "";

        public Upgrade(Vector2 position, Vector2 size, string identifier, string text) : base(position, size, identifier, text)
        {
            texture = Common.textures["projectile"];
        }

        public Upgrade(Vector2 position, Vector2 size, string identifier, string text, ShipStats stats) : base(position, size, identifier, text)
        {
            texture = Common.textures["projectile"];
            statsModifier = stats;
            //används inte nu men lär komma till användning om det blir specifika uppgraderingar
        }

        public void GenerateText()
        {
            text = "Upgrade:\n" + description;
        }

        public void ChangeRandomStat() //ändrar en slumpmässig variabel
        {
            int randomField = Common.random.Next(0, statsModifier.GetType().GetFields().Length);

            var field = statsModifier.GetType().GetFields()[randomField];
            //en slumpmässigt vald variabel i ShipStats

            short valueToModifyBy = (short)Common.random.Next(-5, 6);

            field.SetValue(statsModifier, valueToModifyBy);
            description = field.Name + ": " + valueToModifyBy;

            GenerateText();
        }

        public void ModifyStats(PlayerShip player) //lägger till alla ändringar i ShipStats
        {
            var fields = statsModifier.GetType().GetFields();
            var playerFields = player.stats.GetType().GetFields();

            for (int i = 0; i < fields.Length; i++) //perfekt för att jag behöver inte ändra något när nya variabler läggs till, så länge de är floats
            {                                       //rykten säger dock att det inte är särskilt prestandavänligt
                playerFields[i].SetValue(player.stats, (float)playerFields[i].GetValue(player.stats) + (float)fields[i].GetValue(statsModifier));
            }
        }
    }
}
