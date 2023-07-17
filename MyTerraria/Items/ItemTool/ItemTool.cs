using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Items.ItemTool
{
    public enum ItemToolType
    {
        None,
        CoperAxe,
        CoperPick,
        IronAxe,
        IronPick,
        SilverAxe,
        SilverPick,
        GoldAxe,
        GoldPick
    }

    public class ItemTool : ItemBase
    {
        public ItemToolType Type { get; protected set; }
        public ItemTool(ItemToolType type, int textureNumber, int maxStackSize) : base(textureNumber, maxStackSize)
        {
            Type = type;
        }
    }
}