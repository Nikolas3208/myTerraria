using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Worlds.Tile
{
    public class TileGround : BaseTile
    {
        public TileGround()
        {
            tileHealth = 5;
            type = TileType.Ground;

            SetSpriteShit();

            UpdateView();
        }

        public override void BreakTile()
        {
            base.BreakTile();
        }
    }
}
