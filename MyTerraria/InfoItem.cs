using System;
using System.Collections.Generic;
using System.Threading;
using MyTerraria.Items;
using MyTerraria.Items.ItemTile;
using MyTerraria.Worlds;
using SFML.Audio;

namespace MyTerraria
{
    public class InfoItem
    {
        public static List<InfoItem> InfoItems = new List<InfoItem>();
        public static Dictionary<TileType, Item> itemTile = new Dictionary<TileType, Item>();

        public static ItemTile itemGround = new ItemGround(Program.Game.World, Content.ssTileItemGround.Texture, Items.ItemType.Tile, TileType.Ground);
        public static ItemTile itemStone = new ItemGround(Program.Game.World, Content.ssTileItemStone.Texture, Items.ItemType.Tile, TileType.Stone);
        public static ItemTile itemWood = new ItemGround(Program.Game.World, Content.ssTileItemBoard.Texture, Items.ItemType.Tile, TileType.Wood);

        /*public static InfoItem ItemGround = new InfoItem(true, TileType.Ground).SetSprite(Content.ssTileItemGround, 0, 0);
        public static InfoItem ItemGroundWall = new InfoItem(true ,TileType.GroundWall).SetSprite(Content.ssWallItemGround, 0, 0);
        public static InfoItem ItemStone = new InfoItem(true, TileType.Stone).SetSprite(Content.ssTileItemStone, 0, 0);
        public static InfoItem ItemStoneWall = new InfoItem(true, TileType.StoneWall).SetSprite(Content.ssWallItemStone, 0, 0);
        public static InfoItem ItemMushroom = new InfoItem(true, TileType.Mushroom).SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemVegetation = new InfoItem(true, TileType.Vegetation).SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemTorch = new InfoItem(true, TileType.Torch).SetSprite(Content.ssTileItemTorch, 0, 0);
        public static InfoItem ItemBoard = new InfoItem(true, TileType.Board).SetSprite(Content.ssTileItemBoard, 0, 0);
        public static InfoItem ItemBoardWall = new InfoItem(true, TileType.BoardWall).SetSprite(Content.ssWallItemBoard, 0, 0);

        public static InfoItem ItemTreeSapling = new InfoItem(true, TileType.Treesapling).SetSprite(Content.ssTileItemAcorn, 0, 0);
        
        public static InfoItem ItemIronOre = new InfoItem(true, TileType.Ironore).SetSprite(Content.ssTileItemIronOre, 0, 0);
        public static InfoItem ItemCopperOre = new InfoItem(true, TileType.Coperore).SetSprite(Content.ssTileItemCopperOre, 0, 0);
        public static InfoItem ItemGoldOre = new InfoItem(true, TileType.Goldore).SetSprite(Content.ssTileItemGoldOre, 0, 0);
        public static InfoItem ItemSilverOre = new InfoItem(true, TileType.Silverore).SetSprite(Content.ssTileItemSilverOre, 0, 0);*/

        //-----------------------

        /*public static InfoItem ItemPick = new InfoItem(false, TileType.Ground).SetSprite(Content.ssTileItemPick, 0, 0).SetMaxCountInStack(1);
        public static InfoItem ItemAxe = new InfoItem(false, TileType.Wood).SetSprite(Content.ssTileItemAxe, 0, 0).SetMaxCountInStack(1);*/

        //-----------------------

        //public static InfoItem ItemSword = new InfoItem(WeaponType.Middle).SetSprite(Content.ssTileItemSword, 0, 0);

        //------------------------

        public bool IsTile;
        public TileType type;

        public InfoItem(bool isTile, TileType type)
        {
            IsTile = isTile;
            this.type = type;
        }

        public static void Colectionsgen()
        {
            itemTile.Add(itemGround.TileType, itemGround);
            itemTile.Add(itemStone.TileType, itemStone);
            itemTile.Add(itemWood.TileType, itemWood);

            //InfoItems.Add(ItemPick);
            /*InfoItems.Add(ItemGround);
            InfoItems.Add(ItemGroundWall);
            InfoItems.Add(ItemStone);
            InfoItems.Add(ItemStoneWall);
            InfoItems.Add(ItemMushroom);
            InfoItems.Add(ItemTorch);
            InfoItems.Add(ItemBoard);
            InfoItems.Add(ItemBoardWall);
            InfoItems.Add(ItemTreeSapling);
            //InfoItems.Add(ItemAxe);
            InfoItems.Add(ItemIronOre);
            InfoItems.Add(ItemCopperOre);
            InfoItems.Add(ItemGoldOre);
            InfoItems.Add(ItemSilverOre);*/
            //InfoItems.Add(ItemSword);
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

        public static InfoItem GetItem(TileType type)
        {
            foreach (InfoItem item in InfoItems)
            {
                if (item.type == type && item.IsTile)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
