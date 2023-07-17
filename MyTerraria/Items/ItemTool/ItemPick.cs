using MyTerraria.Worlds;
using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.ItemTool
{

    //Предмет "Кирка"
    public class ItemPick : ItemTool
    {
        public ItemPick(ItemToolType type, int textureNumber, int maxStackSize) : base(type, textureNumber, maxStackSize)
        {
            IType = ItemType.Pick;
            Texture = Content.itemTextureList[textureNumber];
        }
    }
}
