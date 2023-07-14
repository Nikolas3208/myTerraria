using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.ItemTile
{
    public class ItemGround : ItemTile
    {
        public ItemGround(World world, Texture texture, ItemType type, TileType tileType) : base(world, texture, type, tileType)
        {
            
        }
    }
}
