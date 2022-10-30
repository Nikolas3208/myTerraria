namespace MyTerraria
{
    class InfoItem
    {
        public static InfoItem ItemPick = new InfoItem().SetSprite(Content.ssTileItemPick, 0, 0);
        public static InfoItem ItemGround = new InfoItem().SetSprite(Content.ssTileItemGround, 0, 0);
        public static InfoItem ItemGrass = new InfoItem().SetSprite(Content.ssTileItemGround , 0, 0);
        public static InfoItem ItemStone = new InfoItem().SetSprite(Content.ssTileItemStone, 0, 0);
        public static InfoItem ItemMushroom = new InfoItem().SetSprite(Content.ssTileItemMushroom, 0, 0);
        public static InfoItem ItemTorch = new InfoItem().SetSprite(Content.ssTileItemTorch, 0, 0);
        public static InfoItem ItemBoard = new InfoItem().SetSprite(Content.ssTileItemBoard, 0, 0);
        public static InfoItem ItemAxe = new InfoItem().SetSprite(Content.ssTileItemAxe, 0, 0);
        public static InfoItem ItemIronOre = new InfoItem().SetSprite(Content.ssTileItemIronOre, 0, 0);
        public static InfoItem ItemCopperOre = new InfoItem().SetSprite(Content.ssTileItemCopperOre, 0, 0);
        public static InfoItem ItemGoldOre = new InfoItem().SetSprite(Content.ssTileItemGoldOre, 0, 0);
        public static InfoItem ItemSilverOre = new InfoItem().SetSprite(Content.ssTileItemSilverOre, 0, 0);
        public static InfoItem ItemGoldIngot = new InfoItem().SetSprite(Content.ssTileItemGoldIngot, 0, 0);
        public static InfoItem ItemCopperIngot = new InfoItem().SetSprite(Content.ssTileItemCopperIngot, 0, 0);
        public static InfoItem ItemSilverIngot = new InfoItem().SetSprite(Content.ssTileItemSilverIngot, 0, 0);
        public static InfoItem ItemIronIngot = new InfoItem().SetSprite(Content.ssTileItemIronIngot, 0, 0);
        public static InfoItem ItemSlime = new InfoItem().SetSprite(Content.ssTileItemSlime, 0, 0);
        public static InfoItem ItemSword = new InfoItem().SetSprite(Content.ssTileItemSword, 0, 0);
        //public static InfoItem ItemAcorn = new InfoItem().SetSprite(Content.ssTileItemAcorn, 0, 0);
        //public static InfoItem ItemWorkbench = new InfoItem().SetSprite(Content.ssTileItemWorkbench, 0, 0);

        //------------------------

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
