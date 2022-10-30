using MyTerraria.Items;
using MyTerraria.NPC;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyTerraria
{
    class World : Transformable, Drawable
    {
        // Кол-во плиток по ширине и высоте
        public const int WORLD_WIDTH = 500;
        public const int WORLD_HEIGHT = 500;

        /*public Tile upTile = GetTile(i, j - 1);     // Верхний сосед
        public Tile downTile = GetTile(i, j + 1);   // Нижний сосед
        public Tile leftTile = GetTile(i - 1, j);   // Левый сосед
        public Tile rightTile = GetTile(i + 1, j);  // Правый сосед*/

        public int BackgroundMin = 0;

        public static Random Rand { private set; get; }

        public static ItemTile ItemTile { set; get; }

        public static Perlin2D Perlin2D { private set; get; }

        // Плитки
        public Tile[,] tiles;
        Tile tile;
        //List<Tile> tilesList = new List<Tile>();

        // Предметы
        public List<Item> items = new List<Item>();

        // Конструктор класса
        public World()
        {
            Perlin2D = new Perlin2D();
            tiles = new Tile[WORLD_WIDTH, WORLD_HEIGHT];

            //ItemTile = new ItemTile(this, null);
        }

        int[] arr = new int[WORLD_WIDTH];

        private async Task GenerateTerrain(int seed = -1)
        {
            Rand = seed >= 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);

            int groundLevelMax = Rand.Next(100, 200);
            int groundLevelMin = groundLevelMax + Rand.Next(200, 300);
            BackgroundMin = groundLevelMin;

            for (int i = WORLD_WIDTH - 1; i > 0; i--)
            {
                for (int j = WORLD_HEIGHT -1; j > 0; j--)
                {
                    tiles[i, j] = new Tile(TileType.NONE, null, null, null, null);
                }
            }

            // Генерация уровня ландшафта
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                int dir = Rand.Next(0, 2) == 1 ? 2 : -2;

                if (i > 0)
                {
                    if (arr[i - 1] + dir < groundLevelMax || arr[i - 1] + dir > groundLevelMin)
                        dir = -dir;

                    arr[i] = arr[i - 1] + dir;
                }
                else
                    arr[i] = groundLevelMin;
            }

            // Сглаживание
            for (int i = 0; i < WORLD_WIDTH - 1; i++)
            {
                float sum = arr[i];
                int count = 1;
                for (int k = 1; k <= 15; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += arr[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += arr[i2];
                        count++;
                    }
                }

                arr[i] = (int)(sum / count);
            }

            // Ставим плитки на карту
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                SetTile(TileType.GRASS, i, arr[i]);
            }

            for (int i = WORLD_WIDTH - 1; i > 1; i--)
            {
                for (int j = arr[i]; j < WORLD_HEIGHT; j++)
                {
                    float v = Perlin2D.Noise((i + seed) * 0.098f, (j + seed) * 0.098f);
                    if (v > 0.9)
                    {
                        SetTile(TileType.NONE, i, j);
                    }
                    if (GetTile(i, j).type == TileType.NONE && v < 0.16)
                    {
                        SetTile(TileType.GROUND, i, j);
                    }
                    if (GetTile(i, j).type == TileType.NONE && v < 0.2)
                    {
                        SetTile(TileType.GRASS, i, j);
                    }
                    if (GetTile(i, j).type == TileType.NONE && v < 0.2)
                    {
                        SetTile(TileType.STONE, i, j);
                    }
                    if (GetTile(i, j).type == TileType.NONE && v > 0.02)
                    {
                        SetTile(TileType.IRONORE, i, j);
                    }
                }
            }
        }

        public async Task GenerateVeGetation()
        {
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < BackgroundMin; y++)
                {
                    if (GetTile(x, y) != null && GetTile(x, y).type == TileType.GRASS && GetTile(x, y - 1).type == TileType.NONE)
                    {
                        await SetTile(TileType.VEGETATION, x, y - 1);
                    }
                }
                x += Rand.Next(1, 10);
            }
        }

        public async Task GenerateTrees()
        {
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    if (GetTile(x, y) != null && GetTile(x, y).type == TileType.GRASS && y == arr[x])
                    {
                        int a = 0;
                        for (int i = 1; i < Rand.Next(8, 28); i++)
                        {
                            SetTile(TileType.TREEBRAK, x, y - i);
                            a = i;

                            //SetTile(TileType.VEGETATION, x, y);
                        }
                        SetTile(TileType.TREETOPS, x, y - a++);
                    }
                }
                x += Rand.Next(3, 20);
            }
        }

        // Генерируем новый мир
        public async void GenerateWorld()
        {
            await GenerateTerrain();

            await GenerateTrees();

            await GenerateVeGetation();
        }

        //public string[,] type_Tile;

        // Установить плитку
        public async Task SetTile(TileType type, int i, int j)
        {
            if (i < 0)
            {
                i = 0;
            }
            if (j < 0)
            {
                j = 0;
            }
            if (j >= WORLD_HEIGHT)
                j = WORLD_HEIGHT - 1;

            if(i >= WORLD_WIDTH)
                i = WORLD_WIDTH -1;

            // Находим соседей
            Tile upTile = GetTile(i, j - 1);     // верхний сосед
            Tile downTile = GetTile(i, j + 1);   // нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // левый сосед
            Tile rightTile = GetTile(i + 1, j);  // правый сосед

            tile = new Tile(type, upTile, downTile, leftTile, rightTile);

            if (type != TileType.NONE)
            {
                tile.Position = new Vector2f(i * Tile.TILE_SIZE, j * Tile.TILE_SIZE) + Position;
                tiles[i, j] = tile;
            }
        }


        public void DelTile(TileType type, int i, int j)
        {
            if (i < 0)
            {
                i = 0;
            }
            if (j < 0)
            {
                j = 0;
            }
            if (j >= WORLD_HEIGHT)
                j = WORLD_HEIGHT - 1;

            if (i >= WORLD_WIDTH)
                i = WORLD_WIDTH - 1;

            // Находим соседей
            Tile upTile = GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = GetTile(i + 1, j);  // Правый сосед

            tile = tiles[i, j];

            if (tile != null)
            {
                if (type == TileType.GROUND)
                {
                    var itemTile = new ItemTile(this, InfoItem.ItemGround);
                    itemTile.Position = tile.Position;
                    items.Add(itemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.GRASS)
                {
                    var itemTile = new ItemTile(this, InfoItem.ItemGround);
                    itemTile.Position = tile.Position;
                    items.Add(itemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.STONE)
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemStone);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.TREEBRAK)
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemBoard);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.TREETOPS)
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemBoard);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.BOARD)
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemBoard);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
                else if (type == TileType.IRONORE)
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemIronOre);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j].type = TileType.NONE;
                    tiles[i, j] = null;
                }
            }
            
            // Присваиваем соседей, а соседям эту плитку
            if (upTile != null) upTile.DownTile = null;
            if (downTile != null) downTile.UpTile = null;
            if (leftTile != null) leftTile.RightTile = null;
            if (rightTile != null) rightTile.LeftTile = null;

        }

        // Получить плитку по мировым координатам
        public Tile GetTileByWorldPos(float x, float y)
        {
            int i = (int)(x / Tile.TILE_SIZE);
            int j = (int)(y / Tile.TILE_SIZE);
            return GetTile(i, j);
        }
        public Tile GetTileByWorldPos(Vector2f pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }
        public Tile GetTileByWorldPos(Vector2i pos)
        {
            return GetTileByWorldPos(pos.X, pos.Y);
        }

        // Получить плитку
        public Tile GetTile(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < WORLD_WIDTH && j < WORLD_HEIGHT)
            {
                return tiles[i, j];
            }
            else
                return null;
        }

        public NPC.Npc GetNPC()
        {
            return null;
        }

        // Обновить мир
        public void Update()
        {
            int i = 0;
            while (i < items.Count)
            {
                if (items[i].IsDestroyed)
                    items.RemoveAt(i);
                else
                {
                    items[i].Update();
                    i++;
                }
            }

        }
        Vector2f pos;
        //int screanX = WORLD_WIDTH - (int)Program.Window.Size.X / 16;
        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states)
        {

            pos = Program.Game.Player.Position;
            var tilesPos = (pos.X / Tile.TILE_SIZE, pos.Y / Tile.TILE_SIZE);
            var tilesPerScreen = (Program.Window.Size.X / Tile.TILE_SIZE, Program.Window.Size.Y / Tile.TILE_SIZE);
            var LeftMostTilesPos = (int)(tilesPos.Item1 - tilesPerScreen.Item1 / 2);
            var TopMostTilesPos = (int)(tilesPos.Item2 - tilesPerScreen.Item2 / 2);

            //if (Program.Game.Player.Position.X <= WORLD_WIDTH * 16 - Program.Window.Size.X / 2)
            //{
                for (int i = LeftMostTilesPos; i < LeftMostTilesPos + tilesPerScreen.Item1 + 1; i++)
                {
                    for (int j = TopMostTilesPos; j < TopMostTilesPos + tilesPerScreen.Item2 + 1; j++)
                    {
                        if (i > -1 && j > -1 && i < WORLD_WIDTH && j < WORLD_HEIGHT && tiles[i, j] != null)
                            target.Draw(tiles[i, j]);
                    }
                }
            //}
            /*else if (Program.Game.Player.Position.X >= WORLD_WIDTH * 16 - Program.Window.Size.X / 2)
            {
                for (int i = (int)screanX; i < LeftMostTilesPos + tilesPerScreen.Item1 + 1; i++)
                {
                    for (int j = TopMostTilesPos; j < TopMostTilesPos + tilesPerScreen.Item2 + 10; j++)
                    {
                        if (i > -1 && j > -1 && i < WORLD_WIDTH && j < WORLD_HEIGHT && tiles[i, j] != null)
                            target.Draw(tiles[i, j]);
                    }
                }
            }*/

            // Рисуем вещи
            foreach (var item in items)
                target.Draw(item);
        }
    }
}
