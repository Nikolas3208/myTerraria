using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyTerraria.Items.ItemTile
{
    // Предмет "Плитка"
    public class ItemTile : Item
    {
        public ItemTile(World world, Texture texture, ItemType type, TileType tileType) : base(world, texture, type)
        {
            MaxCountInStack = 99;
            this.TileType = tileType;
        }

        public override bool OnClickMouseButton(Tile tile)
        {
            return false;
        }

        public override void OnWallCollided()
        {
        }
    }
}
