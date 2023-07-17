using SFML.Graphics;
using SFML.Audio;
using System.Collections.Generic;
using System;
using System.Windows.Forms;

using MyTerraria.Worlds;

namespace MyTerraria
{
    class Content
    { 
        public const int WALLWIDTH = 45;
        public const int WALLHEIGHT = 39;

        public const string CONTENT_DIR = "..\\Content\\";
        //public static readonly string FONT_DIR = 

        public static Sprite ssBackgroundSky;
        public static Sprite ssBackgroundMountains;
        public static Texture ssBackgroundMenu;
        public static Texture ssTextureBackgroundSky;

        //---------------------------------------------

        //Tile
        public static List<SpriteSheet> ssTileList = new List<SpriteSheet>();

        //---------------------------------------------


        //Items
        public static List<Texture> itemTextureList = new List<Texture>();

        //---------------------------------------------

        // NPC
        public static SpriteSheet ssNpcSlime; // Слизень
        public static SpriteSheet ssNpcFlyingEye; // Глаз

        //---------------------------------------------

        // Игрок
        public static SpriteSheet ssPlayerHead;        // Голова
        public static SpriteSheet ssPlayerHair;        // Волосы
        public static SpriteSheet ssPlayerShirt;       // Рубашка
        public static SpriteSheet ssPlayerUndershirt;  // Рукава
        public static SpriteSheet ssPlayerHands;       // Кисти
        public static SpriteSheet ssPlayerLegs;        // Ноги
        public static SpriteSheet ssPlayerShoes;       // Обувь

        //---------------------------------------------

        // UI
        public static Texture texUIInvertoryBack;      // Инвертарь

        //---------------------------------------------

        public static Music mRunNpc;
        public static Music mDig_0;
        public static Music mGrab;

        //---------------------------------------------

        //
        public static Font font = new Font(CONTENT_DIR + "Fonts\\arial.ttf");    // Шрифт

        public static void Load()
        {
            try
            {
                //Background
                ssTextureBackgroundSky = new Texture(CONTENT_DIR + "Textures\\background\\Background_0.png");
                ssBackgroundSky = new Sprite(ssTextureBackgroundSky);
                ssBackgroundMenu = new Texture(CONTENT_DIR + "Textures\\background\\Background_Menu.png");

                //---------------------------------------------

                //TilleBlock
                /*ssTileGround = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_0.png"));
                ssTileStone = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_1.png"));
                ssTileGrass = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_2.png"));
                ssTileSand = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_2.png"));
                ssTileVegetation = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_3.png"));
                ssTileTorch = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_4.png"));
                ssTileTreeBark = new SpriteSheet(20, 20, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_5.png"));
                ssTileIronOre = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_6.png"));
                ssTileCoperOre = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_7.png"));
                ssTileGoldOre = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_8.png"));
                ssTileSilverOre = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_9.png"));
                ssTileSaplingTree = new SpriteSheet(540 / 30, 38, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_20.png"));
                ssTileBoard = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\tiles\\Tiles_30.png"));*/

                for (int i = 0; i < 10; i++)
                {
                    ssTileList.Add(new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + $"Textures\\tiles\\Tiles_{i}.png")));
                }

                //----------------------------------------------

                //Wall
                /*ssWallStone = new SpriteSheet(32, 32, false, 3, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_1.png"));
                ssWallGround = new SpriteSheet(32, 32, false, 3, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_2.png"));
                ssWallBoard = new SpriteSheet(32, 32, false, 3, new Texture(CONTENT_DIR + "Textures\\walls\\Wall_4.png"));

                //---------------------------------------------

                //Верхущка дерева
                ssTileTreeTops = new SpriteSheet(80, 80, false, 1, new Texture(CONTENT_DIR + "Textures\\trees\\Tree_Tops.png"));
                ssTileTreeTopsDistortion = new SpriteSheet(3, 1, true, 1, new Texture(CONTENT_DIR + "Textures\\trees\\Tree_Tops_1.png"));
                */
                //---------------------------------------------

                //Items
               /* ssTileItemPick = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_1.png");
                ssTileItemGround = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_2.png");
                ssTileItemStone = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_3.png");
                ssTileItemMushroom = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_5.png");
                ssTileItemTorch = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_8.png");
                ssTileItemBoard = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_9.png");
                ssTileItemAxe = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_10.png");
                ssTileItemIronOre = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_11.png");
                ssTileItemCopperOre = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_12.png");
                ssTileItemGoldOre = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_13.png");
                ssTileItemSilverOre = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_14.png");
                ssTileItemGoldIngot = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_19.png");
                ssTileItemCopperIngot = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_20.png");
                ssTileItemSilverIngot = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_21.png");
                ssTileItemIronIngot = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_22.png");
                ssTileItemSlime = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_23.png");
                ssTileItemSword = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_24.png");
                ssTileItemAcorn = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_27.png");

                ssWallItemStone = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_26.png");
                ssWallItemGround = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_30.png");
                ssWallItemBoard = new Texture(CONTENT_DIR + "Textures\\ui\\items\\Item_93.png");*/

                //Загрузка текстур item-ов
                for (int i = 0; i < 30; i++)
                {
                    itemTextureList.Add(new Texture(CONTENT_DIR + $"Textures\\ui\\items\\Item_{i}.png"));
                }

                //---------------------------------------------

                // NPC
                ssNpcSlime = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\NPC_1.png"));
                ssNpcFlyingEye = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\NPC_2.png"));

                //---------------------------------------------

                // Игрок
                ssPlayerHead = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Head.png"));
                ssPlayerHair = new SpriteSheet(1, 14, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hair_1.png"));
                ssPlayerShirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shirt.png"));
                ssPlayerUndershirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Undershirt.png"));
                ssPlayerHands = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hands.png"));
                ssPlayerLegs = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Pants.png"));
                ssPlayerShoes = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shoes.png"));

                //---------------------------------------------

                // UI
                texUIInvertoryBack = new Texture(CONTENT_DIR + "Textures\\ui\\Inventory_Back.png");

                //---------------------------------------------

                //Sound

                mRunNpc = new Music(CONTENT_DIR + "Sounds\\Run.wav");
                mDig_0 = new Music(CONTENT_DIR + "Sounds\\Dig_0.wav");
                mGrab = new Music(CONTENT_DIR + "Sounds\\Grab.wav");
            }
            catch (Exception ex)
            {
                DialogResult dialogResult = MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK);
                if (dialogResult == DialogResult.OK)
                    Program.Window.Close();
            }
            
        }
    }
}
