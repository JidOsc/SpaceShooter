using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SpaceShooter
{
    internal class Upgrade
    {
        public ShipStats statsModifier = new();

        public Upgrade(ShipStats stats)
        {
            statsModifier = stats;
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
