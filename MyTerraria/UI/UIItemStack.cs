using MyTerraria.Items;
using MyTerraria.Items.ItemTile;
using MyTerraria.Items.Tools;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.UI
{
    class UIItemStack : UIBase
    {
        public int itemCount { get; set; }
        public int ItemCount
        {
            get { return itemCount; }
            set
            {
                itemCount = value;
                if (textCount != null)
                {
                    textCount.DisplayedString = itemCount.ToString();
                    var textRect = textCount.GetGlobalBounds();
                    textCount.Position = new Vector2f((int)(rectShape.Size.X / 2 - textRect.Width / 2), (int)(rectShape.Size.Y - textCount.CharacterSize - 5));
                }
            }
        }
        public int ItemCountMax
        {
            get
            {
                return Item.MaxCountInStack;
            }
        }
        public bool IsFull
        {
            get { return ItemCount >= ItemCountMax;  }
        }

        public Item Item { get; private set; }

        RectangleShape rectShapeImage;
        Text textCount;

        public UIItemStack(ItemTile item, int count)
        {
            Item = item;

            var rectSize = (Vector2f)Content.texUIInvertoryBack.Size;
            rectShape = new RectangleShape(rectSize);
            rectShape.FillColor = Color.Transparent;

            var imgSize = new Vector2f(item.Texture.Size.X, item.Texture.Size.Y);
            rectShapeImage = new RectangleShape(imgSize);
            rectShapeImage.Position = rectSize / 2 - imgSize / 2;
            rectShapeImage.Texture = item.Texture;

            textCount = new Text("0", Content.font, 15);
            textCount.FillColor = Color.Black;

            ItemCount = count;
        }

        public UIItemStack(ItemPick item, int count)
        {
            Item = item;

            var rectSize = (Vector2f)Content.texUIInvertoryBack.Size;
            rectShape = new RectangleShape(rectSize);
            rectShape.FillColor = Color.Transparent;

            var imgSize = new Vector2f(item.Texture.Size.X, item.Texture.Size.Y);
            rectShapeImage = new RectangleShape(imgSize);
            rectShapeImage.Position = rectSize / 2 - imgSize / 2;
            rectShapeImage.Texture = item.Texture;

            if (item.MaxCountInStack > 1)
            {
                textCount = new Text("0", Content.font, 15);
                textCount.FillColor = Color.Black;
            }
            else
            {
                textCount = null;
            }

            ItemCount = count;
        }

        public UIItemStack(ItemAxe item, int count)
        {
            Item = item;

            var rectSize = (Vector2f)Content.texUIInvertoryBack.Size;
            rectShape = new RectangleShape(rectSize);
            rectShape.FillColor = Color.Transparent;

            var imgSize = new Vector2f(item.Texture.Size.X, item.Texture.Size.Y);
            rectShapeImage = new RectangleShape(imgSize);
            rectShapeImage.Position = rectSize / 2 - imgSize / 2;
            rectShapeImage.Texture = item.Texture;

            if (item.MaxCountInStack > 1)
            {
                textCount = new Text("0", Content.font, 15);
                textCount.FillColor = Color.Black;
            }
            else
            {
                textCount = null;
            }

            ItemCount = count;
        }

        public void ClearUIInvertory()
        {
            rectShapeImage.Texture = null;
            textCount = null;
            Item = null;
        }

        public override void OnDragBegin()
        {
            if (Parent != null && Parent is UIInvertoryCell)
                (Parent as UIInvertoryCell).ItemStack = null;

            base.OnDragBegin();
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            base.Draw(target, states);
            states.Transform *= Transform;

            if(rectShapeImage.Texture != null)
                target.Draw(rectShapeImage, states);
            if(textCount != null)
                target.Draw(textCount, states);

            if(itemCount == 0)
                ClearUIInvertory();
        }

        public override void OnDrop(UIBase ui)
        {
            if (ui is UIItemStack)
            {
                var itemSrc = ui as UIItemStack;
                var itemDest = this;

                if (itemSrc.Item.type == itemDest.Item.type && itemSrc.Item.TileType == itemDest.Item.TileType && !itemDest.IsFull && itemDest.ItemCount + itemSrc.ItemCount < itemDest.ItemCountMax)
                    itemDest.ItemCount += itemSrc.ItemCount;
                else
                    ui.OnCancelDrag();
            }
        }
    }
}
