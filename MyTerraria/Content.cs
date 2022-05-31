using SFML.Graphics;

namespace MyTerraria
{
    class Content
    {
        public const string CONTENT_DIR = "..\\Content\\";
        public static readonly string FONT_DIR = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Fonts) + "\\";

        public static SpriteSheet ssTileGround; // Ground
        public static SpriteSheet ssTileGrass; // Grass
        public static SpriteSheet ssTileSone; // Stone

        public static SpriteSheet ssTileTreeBark; // Tree
        public static SpriteSheet ssTileTreeTops; // Tree
        public static SpriteSheet ssTileVegetation; // Tree
        public static SpriteSheet ssTileDesk; // Tree
        public static SpriteSheet ssTileDesk1; // Tree

        // NPC
        public static SpriteSheet ssNpcSlime; // Слизень

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

        public static Font font;       // Шрифт

        public static void Load()
        {
            ssTileGround = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_0.png"));
            ssTileGrass = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_2.png"));
            ssTileSone = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_1.png"));

            ssTileDesk = new SpriteSheet(1, 1, true, 0, new Texture(CONTENT_DIR + "Textures\\Item_9.png"));
            ssTileDesk1 = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_30.png"));
            ssTileTreeBark = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE + 4, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_5.png"));
            ssTileTreeTops = new SpriteSheet(3, 1, true, 1, new Texture(CONTENT_DIR + "Textures\\Tree_Tops.png"));
            ssTileVegetation = new SpriteSheet(Tile.TILE_SIZE, Tile.TILE_SIZE, false, 1, new Texture(CONTENT_DIR + "Textures\\Tiles_3.png"));

            // NPC
            ssNpcSlime = new SpriteSheet(1, 2, true, 0, new Texture(CONTENT_DIR + "Textures\\npc\\slime.png"));

            // Игрок
            ssPlayerHead = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Head.png"));
            ssPlayerHair = new SpriteSheet(1, 14, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hair_11.png"));
            ssPlayerShirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shirt.png"));
            ssPlayerUndershirt = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Undershirt.png"));
            ssPlayerHands = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Hands.png"));
            ssPlayerLegs = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\legs.png"));
            ssPlayerShoes = new SpriteSheet(1, 20, true, 0, new Texture(CONTENT_DIR + "Textures\\player\\Player_Shoes.png"));

            // UI
            texUIInvertoryBack = new Texture(CONTENT_DIR + "Textures\\ui\\Inventory_Back.png");

            // Шрифт
            font = new Font(FONT_DIR + "arial.ttf");
        }
    }
}
