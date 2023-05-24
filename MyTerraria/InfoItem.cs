using System.Collections.Generic;
using System.Threading;

using MyTerraria.Worlds;

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
        
        public static InfoItem ItemGround = new InfoItem(TileType.Ground, Tiles.Tile).SetSprite(Content.ssTileItemGround, 0, 0);
        public static InfoItem ItemGroundWall = new InfoItem(TileType.GroundWall, Tiles.Wall).SetSprite(Content.ssWallItemGround, 0, 0);
        public static InfoItem ItemStone = new InfoItem(TileType.Stone, Tiles.Tile).SetSprite(Content.ssTileItemStone, 0, 0);
        public static InfoItem ItemStoneWall = new InfoItem(TileType.StoneWall, Tiles.Wall).SetSprite(Content.ssWallItemStone, 0, 0);
        public static InfoItem ItemMushroom = new InfoItem(TileType.Mushroom, Tiles.Wall).SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemVegetation = new InfoItem(TileType.Vegetation, Tiles.Wall).SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemTorch = new InfoItem(TileType.Torch, Tiles.Wall).SetSprite(Content.ssTileItemTorch, 0, 0);
        public static InfoItem ItemBoard = new InfoItem(TileType.Board, Tiles.Tile).SetSprite(Content.ssTileItemBoard, 0, 0);
        public static InfoItem ItemBoardWall = new InfoItem(TileType.BoardWall, Tiles.Wall).SetSprite(Content.ssWallItemBoard, 0, 0);

        public static InfoItem ItemTreeSapling = new InfoItem(TileType.Treesapling, Tiles.Tile).SetSprite(Content.ssTileItemAcorn, 0, 0);
        
        public static InfoItem ItemIronOre = new InfoItem(TileType.Ironore, Tiles.Tile).SetSprite(Content.ssTileItemIronOre, 0, 0);
        public static InfoItem ItemCopperOre = new InfoItem(TileType.Coperore, Tiles.Tile).SetSprite(Content.ssTileItemCopperOre, 0, 0);
        public static InfoItem ItemGoldOre = new InfoItem(TileType.Goldore, Tiles.Tile).SetSprite(Content.ssTileItemGoldOre, 0, 0);
        public static InfoItem ItemSilverOre = new InfoItem(TileType.Silverore, Tiles.Tile).SetSprite(Content.ssTileItemSilverOre, 0, 0);

        //-----------------------

        public static InfoItem ItemPick = new InfoItem(ToolType.Pick).SetSprite(Content.ssTileItemPick, 0, 0);
        public static InfoItem ItemAxe = new InfoItem(ToolType.Axe).SetSprite(Content.ssTileItemAxe, 0, 0);

        //-----------------------

        public static InfoItem ItemSword = new InfoItem(WeaponType.Middle).SetSprite(Content.ssTileItemSword, 0, 0);

        //------------------------

        public TileType Tiletype;
        public ToolType Tooltype;
        public WeaponType Weapontype;
        public Tiles Tiles;

        public InfoItem(TileType type, Tiles tiles)
        {
            Tiletype = type;
            Tiles = tiles;
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
            InfoItems.Add(ItemGroundWall);
            InfoItems.Add(ItemStone);
            InfoItems.Add(ItemStoneWall);
            InfoItems.Add(ItemMushroom);
            InfoItems.Add(ItemTorch);
            InfoItems.Add(ItemBoard);
            InfoItems.Add(ItemBoardWall);
            InfoItems.Add(ItemTreeSapling);
            InfoItems.Add(ItemAxe);
            InfoItems.Add(ItemIronOre);
            InfoItems.Add(ItemCopperOre);
            InfoItems.Add(ItemGoldOre);
            InfoItems.Add(ItemSilverOre);
            InfoItems.Add(ItemSword);
        }

        public SpriteSheet SpriteSheet { get; private set; }
        public int SpriteI { get; private set; }
        public int SpriteJ { get; private set; }
        // Максимальное кол-во предметов в стеке
        public int MaxCountInStack { get; private set; } = 99;

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
