using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.UI
{
    class UIInvertory : UIWindow
    {
        public static List<UIInvertoryCell> cells { get; set; }
        public UIItemStack UIItemStack { get; set; }

        public UIInvertory()
        {
            cells = new List<UIInvertoryCell>();
            IsVisibleTitleBar = false;
            BodyColor = Color.Transparent;

            int cellCount = 10;

            for (int i = 0; i < cellCount; i++)
                AddCell();

            //cells[a].IsSelected = true;

            Size = new Vector2i((int)Content.texUIInvertoryBack.Size.X * cellCount, (int)Content.texUIInvertoryBack.Size.Y);
        }

        public void ResetSelected()
        {
            for (int i = 0; i < 10; i++)
            {
                cells[i].IsSelected = false;
            }
        }

        public void AddCell()
        {
            var UIInvertoryCell = new UIInvertoryCell(this);
            UIInvertoryCell.Position = new Vector2i(cells.Count * UIInvertoryCell.Width, 0);
            cells.Add(UIInvertoryCell);
            Childs.Add(UIInvertoryCell);
        }

        // Возвращает ячейку со свободным местом по информации о предмете
        UIInvertoryCell GetNotFullCellByInfoItem(InfoItem infoItem)
        {
            foreach (var c in cells)
                if (c.ItemStack != null && c.ItemStack.InfoItem == infoItem && !c.ItemStack.IsFull)
                    return c;

            return null;
        }

        public bool AddItemStack(UIItemStack itemStack)
        {
            UIItemStack = itemStack;
            
            var cell = GetNotFullCellByInfoItem(itemStack.InfoItem);

            if (cell != null)
            {
                cell.ItemStack = itemStack;
                return true;
            }
            else
            {
                foreach (var c in cells)
                {
                    if (c.ItemStack == null)
                    {
                        c.ItemStack = itemStack;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
