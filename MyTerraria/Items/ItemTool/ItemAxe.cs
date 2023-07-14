using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.Tools
{
    public class ItemAxe : Item
    {
        public float Power;
        public float Speed;

        public ItemAxe(World world, Texture texture, ItemType type) : base(world, texture, type)
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
