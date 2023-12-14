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
    internal class Upgrade : InterfaceElement
    {
        public ShipStats statsModifier = new();

        public Upgrade(Vector2 position, Vector2 size, string identifier) : base(position, size, identifier)
        {

        }

        public Upgrade(Vector2 position, Vector2 size, string identifier, ShipStats stats) : base(position, size, identifier)
        {
            statsModifier = stats;
        }

        public void ChangeRandomStat()
        {
            int randomField = Common.random.Next(0, statsModifier.GetType().GetFields().Length);
            statsModifier.GetType().GetFields()[randomField].SetValue(statsModifier, Common.random.Next(-5, 6));
        }

        public void ModifyStats(PlayerShip player)
        {
            var fields = statsModifier.GetType().GetFields();
            var playerFields = player.stats.GetType().GetFields();

            for (int i = 0; i < fields.Length; i++)
            {
                playerFields[i].SetValue(player.stats, (float)playerFields[i].GetValue(player.stats) + (float)fields[i].GetValue(statsModifier));
            }
        }
    }
}
