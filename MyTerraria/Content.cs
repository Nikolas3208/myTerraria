using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;

namespace MyTerraria
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content\\";
        public static readonly string FONT_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts) + "\\";

        public static Sprite ssBackgroundSky;
        public static Sprite ssBackgroundMountains;
        public static Texture ssTextureBackgroundSky;
        //Tile
        public static SpriteSheet ssTileGround; // Ground
        public static SpriteSheet ssTileSand; // Sand
        public static SpriteSheet ssTileGrass; // Grass
        public static SpriteSheet ssTileGrassDistortion; // Grass
        public static SpriteSheet ssTileStone; // Stone
        public static SpriteSheet ssTileStoneDistortion; // Stone
        public static SpriteSheet ssTileIronOre;//Iron Ore
        public static SpriteSheet ssTileTreeBark; // Tree
        public static SpriteSheet ssTileTreeTops; // Tree
        public static SpriteSheet ssTileTreeTopsDistortion; // Tree
        public static SpriteSheet ssTileVegetation; // Vegetation
        public static SpriteSheet ssTileBoard; // Board
        public static SpriteSheet ssTileTorch; // Board
        //Wall
        public static SpriteSheet ssWallGround; // Ground
        public static SpriteSheet ssWallGrass; // Grass
        public static SpriteSheet ssWallStone; // Stone
        public static SpriteSheet ssWallIronOre;//Iron Ore
        public static SpriteSheet ssWallTreeBark; // Tree
        public static SpriteSheet ssWallTreeTops; // Tree
        public static SpriteSheet ssWallVegetation; // Vegetation
        public static SpriteSheet ssWallBoard; // Board
        //Items
        public static SpriteSheet ssTileItemPick;//Pick
        public static SpriteSheet ssTileItemGround;//Ground
        public static SpriteSheet ssTileItemStone;//Stone
        public static SpriteSheet ssTileItemMushroom;//Mushroom
        public static SpriteSheet ssTileItemTorch;//Torch
        public static SpriteSheet ssTileItemBoard; // Board
        public static SpriteSheet ssTileItemAxe;//Axe
        public static SpriteSheet ssTileItemIronOre;//Iron Ore
        public static SpriteSheet ssTileItemCopperOre;//Copper Ore
        public static SpriteSheet ssTileItemGoldOre;//Gold Ore
        public static SpriteSheet ssTileItemSilverOre;//Silver Ore
        public static SpriteSheet ssTileItemGoldIngot;//Gold Ingot
        public static SpriteSheet ssTileItemCopperIngot;//Gold Ingot
        public static SpriteSheet ssTileItemSilverIngot;//Silver Ingot
        public static SpriteSheet ssTileItemIronIngot;//Iron Ingot
        public static SpriteSheet ssTileItemSlime;//Slime
        public static SpriteSheet ssTileItemSword;//Sword
        //public static SpriteSheet ssTileItemAcorn;//Acorn
        //public static SpriteSheet ssTileItemWorkbench;//Workbench


        // NPC
        public static SpriteSheet ssNpcSlime; // Слизень
        public static SpriteSheet ssNpcFlyingEye; // Глаз

        // Игрок
        public static SpriteSheet ssPlayerHead;        // Голова
        public static SpriteSheet ssPlayerHair;        // Волосы
        public static SpriteSheet ssPlayerShirt;       // Рубашка
        public static SpriteSheet ssPlayerUndershirt;  // Рукава
        public static SpriteSheet ssPlayerHands;       // Кисти
        public static SpriteSheet ssPlayerLegs;        // Ноги
        public static SpriteSheet ssPlayerShoes;       // Обувь

        // UI
        public static Texture texUIInvertoryBack;      // Инвертарь

        public static Font font = new Font(FONT_DIR + "arial.ttf");    // Шрифт

        public static void Load()
        {
            //Background
            ssTextureBackgroundSky = new Texture(CONTENT_DIR + "Textures\\background\\Background_0.png");
            ssBackgroundSky = new Sprite(ssTextureBackgroundSky);

            //TilleBlock
            ssTileGround = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false,1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_0.png"));
            ssTileSand = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_53.png"));
            ssTileStone = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_1.png"));
            ssTileStoneDistortion = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_22.png"));
            ssTileGrass = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_2.png"));
            ssTileGrassDistortion = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_23.png"));
            ssTileVegetation = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_3.png"));
            ssTileTreeBark = new SpriteSheet(20, 20, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_5.png"));
            ssTileIronOre = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_6.png"));
            ssTileBoard = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_30.png"));
            ssTileTorch = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_4.png"));
            //Wall
            ssWallGround = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_2.png"));
            ssWallStone = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_1.png"));
            ssWallBoard = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_4.png"));

            //Верхущка дерева
            ssTileTreeTops = new SpriteSheet(80, 80, false, 1, new Texture(CONTENT_DIR + "Textures\\trees\\Tree_Tops.png"));
            ssTileTreeTopsDistortion = new SpriteSheet(3, 1, true, 1, new Texture(CONTENT_DIR + "Textures\\trees\\Tree_Tops_1.png"));

            //Items
            ssTileItemPick = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_1.png"));
            ssTileItemGround = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_2.png"));
            ssTileItemStone = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_3.png"));
            ssTileItemMushroom = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_5.png"));
            ssTileItemTorch = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_8.png"));
            ssTileItemBoard = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_9.png"));
            ssTileItemAxe = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_10.png"));
            ssTileItemIronOre = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_11.png"));
            ssTileItemCopperOre = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_12.png"));
            ssTileItemGoldOre = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_13.png"));
            ssTileItemSilverOre = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_14.png"));
            ssTileItemGoldIngot = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_19.png"));
            ssTileItemCopperIngot = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_20.png"));
            ssTileItemSilverIngot = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_21.png"));
            ssTileItemIronIngot = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_22.png"));
            ssTileItemSlime = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_23.png"));
            ssTileItemSword = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_24.png"));

            // NPC
            ssNpcSlime = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\NPC_1.png"));
            ssNpcFlyingEye = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\NPC_2.png"));

            // Игрок
            ssPlayerHead = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Head.png"));
            ssPlayerHair = new SpriteSheet(1, 14, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hair_1.png"));
            ssPlayerShirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shirt.png"));
            ssPlayerUndershirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Undershirt.png"));
            ssPlayerHands = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hands.png"));
            ssPlayerLegs = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Pants.png"));
            ssPlayerShoes = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shoes.png"));

            // UI
            texUIInvertoryBack = new Texture(CONTENT_DIR + "Textures\\ui\\Inventory_Back.png");

            //Sound

            
        }
    }
}
