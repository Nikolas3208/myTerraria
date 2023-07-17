using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.ItemTool
{
    //Предмет "Топор"
    public class ItemAxe : ItemTool
    {
        public ItemAxe(ItemToolType type ,int textureNumber, int maxStackSize) : base(type, textureNumber, maxStackSize)
        {
            IType = ItemType.Axe;
            Texture = Content.itemTextureList[textureNumber];
        }
    }
}
