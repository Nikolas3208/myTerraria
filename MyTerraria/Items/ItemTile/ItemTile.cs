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
    public class ItemTile : ItemBase
    {
        /// <summary>
        /// Конструктор класса "Плитка"
        /// </summary>
        /// <param name="tile">Плитка</param>
        /// <param name="textureNumber">Номер текстуры</param>
        /// <param name="maxStackSize">Максимальное количество обектов в стаке</param>
        public ItemTile(TileType type, int textureNumber, int maxStackSize) : base(textureNumber, maxStackSize)
        {
            IType = ItemType.Tile;
            tileType = type;

            Texture = Content.itemTextureList[textureNumber];
        }
    }
}
