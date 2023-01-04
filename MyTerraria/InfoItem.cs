using System.Collections.Generic;
using System.Threading;

namespace MyTerraria
{
    public enum ToolType
    {
        None,
        Pick,
        Axe
    }

    public enum WeaponType
    {
        None,
        Middle,
        Ranged
    }
    public class InfoItem
    {
        public static List<InfoItem> InfoItems = new List<InfoItem>();
        
        public static InfoItem ItemGround = new InfoItem(TileType.GROUND).SetSprite(Content.ssTileItemGround, 0, 0);
        public static InfoItem ItemGrass = new InfoItem(TileType.GRASS).SetSprite(Content.ssTileItemGround , 0, 0);
        public static InfoItem ItemStone = new InfoItem(TileType.STONE).SetSprite(Content.ssTileItemStone, 0, 0);
        public static InfoItem ItemMushroom = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemTorch = new InfoItem(TileType.TORCH).SetSprite(Content.ssTileItemTorch, 0, 0);
        public static InfoItem ItemBoard = new InfoItem(TileType.TREEBARK).SetSprite(Content.ssTileItemBoard, 0, 0);
        public static InfoItem ItemBoardTops = new InfoItem(TileType.TREETOPS).SetSprite(Content.ssTileItemBoard, 0, 0);
        public static InfoItem ItemIronOre = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemIronOre, 0, 0);
        public static InfoItem ItemCopperOre = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemCopperOre, 0, 0);
        public static InfoItem ItemGoldOre = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemGoldOre, 0, 0);
        public static InfoItem ItemSilverOre = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemSilverOre, 0, 0);
        public static InfoItem ItemGoldIngot = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemGoldIngot, 0, 0);
        public static InfoItem ItemCopperIngot = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemCopperIngot, 0, 0);
        public static InfoItem ItemSilverIngot = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemSilverIngot, 0, 0);
        public static InfoItem ItemIronIngot = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemIronIngot, 0, 0);
        public static InfoItem ItemSlime = new InfoItem(TileType.NONE).SetSprite(Content.ssTileItemSlime, 0, 0);

        //-----------------------

        public static InfoItem ItemPick = new InfoItem(ToolType.Pick).SetSprite(Content.ssTileItemPick, 0, 0);
        public static InfoItem ItemAxe = new InfoItem(ToolType.Axe).SetSprite(Content.ssTileItemAxe, 0, 0);

        //-----------------------

        public static InfoItem ItemSword = new InfoItem(WeaponType.Middle).SetSprite(Content.ssTileItemSword, 0, 0);

        //------------------------

        public TileType Tiletype;
        public ToolType Tooltype;
        public WeaponType Weapontype;

        public InfoItem(TileType type)
        {
            Tiletype = type;
        }

        public InfoItem(ToolType type)
        {
            Tooltype = type;
        }
        public InfoItem(WeaponType type)
        {
            Weapontype = type;
        }

        public static void Colectionsgen()
        {
            InfoItems.Add(ItemPick);
            InfoItems.Add(ItemGround);
            InfoItems.Add(ItemGrass);
            InfoItems.Add(ItemStone);
            InfoItems.Add(ItemMushroom);
            InfoItems.Add(ItemTorch);
            InfoItems.Add(ItemBoard);
            InfoItems.Add(ItemBoardTops);
            InfoItems.Add(ItemAxe);
            InfoItems.Add(ItemIronOre);
            InfoItems.Add(ItemCopperOre);
            InfoItems.Add(ItemGoldOre);
            InfoItems.Add(ItemSilverOre);
            InfoItems.Add(ItemGoldIngot);
            InfoItems.Add(ItemCopperIngot);
            InfoItems.Add(ItemSilverIngot);
            InfoItems.Add(ItemIronIngot);
            InfoItems.Add(ItemSlime);
            InfoItems.Add(ItemSword);
        }

        public SpriteSheet SpriteSheet { get; private set; }
        public int SpriteI { get; private set; }
        public int SpriteJ { get; private set; }
        // Максимальное кол-во предметов в стеке
        public int MaxCountInStack { get; internal set; } = 99;

        public InfoItem SetSprite(SpriteSheet ss, int i, int j)
        {
            SpriteSheet = ss;
            SpriteI = i;
            SpriteJ = j;
            return this;
        }

        public InfoItem SetMaxCountInStack(int value)
        {
            MaxCountInStack = value;
            return this;
        }
    }
}
