using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.Tools
{

    //Предмет "Кирка"
    public class ItemPick : Item
    {
        public ItemPick(World world, Texture texture, ItemType type) : base(world, texture, type)
        {
        }

        public bool IsDestroy(Tile tile)
        {
            if (tile != null)
            {
                if (tile.type != TileType.Wood)
                    return true;
            }
            return false;
        }

        public override bool OnClickMouseButton(Tile tile)
        {
            if (tile != null)
            {
                if (IsDestroy(tile))
                {
                    return true;
                }
            }
            return false;
        }

        public override void OnWallCollided()
        {

        }
    }
}
