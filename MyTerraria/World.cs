using MyTerraria.Items;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace MyTerraria
{
    class World : Transformable, Drawable
    {
        public string[,] block_type = new string[WORLD_WIDTH, WORLD_HEIGHT];

        // Кол-во плиток по ширине и высоте
        public const int WORLD_WIDTH = 1000;
        public const int WORLD_HEIGHT = 1000;

        public int BackgroundMin = 0;

        public static Random Rand { private set; get; }

        public static ItemTile ItemTile { private set; get; }

        public static Perlin2D Perlin2D { private set; get; }

        // Плитки
        Tile[,] tiles;
        Tile tile;
        List<Tile> tilesList = new List<Tile>();

        // Предметы
        List<Item> items = new List<Item>();

        // Конструктор класса
        public World()
        {
            Perlin2D = new Perlin2D();
            tiles = new Tile[WORLD_WIDTH, WORLD_HEIGHT];
        }

        private void GenerateTerrain(int seed = -1)
        {
            Rand = seed >= 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);

            int groundLevelMax = Rand.Next(1, 10);
            int groundLevelMin = groundLevelMax + Rand.Next(100, 500);
            BackgroundMin = groundLevelMin;

            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                for (int j = 0; j < WORLD_HEIGHT; j++)
                {
                    block_type[i, j] = "NOME";
                }
            }

            // Генерация уровня ландшафта
            int[] arr = new int[WORLD_WIDTH];
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

                for (int j = arr[i] + 1; j < groundLevelMin; j++)
                    SetTile(TileType.GROUND, i, j);
            }

            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                for (int j = groundLevelMin; j < WORLD_HEIGHT; j++)
                {
                    float v = Perlin2D.Noise((i + seed) * 0.05f, (j + seed) * 0.05f);
                    if (v < 0.003)
                    {
                        SetTile(TileType.GROUND, i, j);
                    }
                    if (GetTile(i, j) == null)
                    {
                        v = Perlin2D.Noise((i + seed) * 0.5f, (j + seed) * 0.5f);

                        if (v < 0.0003)
                            SetTile(TileType.STONE, i, j);

                        if(Rand.Next(0,6) == 1)
                            SetTile(TileType.STONE, i, j);
                    }
                    if (GetTile(i, j) == null)
                    {
                        SetTile(TileType.IRONORE, i, j);
                    }
                }
            }
        }

        public void GenerateTrees()
        {
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    if (GetTileType(x, y) == "GRASS")
                    {
                        int a = 0;
                        for (int i = 1; i < Rand.Next(8, 28); i++)
                        {
                            SetTile(TileType.TREEBRAK, x, y - i);
                            a = i;
                        }
                        SetTile(TileType.TREETOPS, x, y - a++);
                    }
                }
                x += Rand.Next(3, 20);
            }
        }

        // Генерируем новый мир
        public void GenerateWorld()
        {
            GenerateTerrain();

            GenerateTrees();
        }

        //public string[,] type_Tile;

        // Установить плитку
        public void SetTile(TileType type, int i, int j)
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
            Tile upTile = GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = GetTile(i + 1, j);  // Правый сосед

            tile = new Tile(type, upTile, downTile, leftTile, rightTile);

            if (type != TileType.NONE)
            {
                tile.Position = new Vector2f(i * Tile.TILE_SIZE, j * Tile.TILE_SIZE) + Position;
                tiles[i, j] = tile;
                block_type[i, j] = type.ToString();
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
                if (type == TileType.GROUND && GetTileType(i,j) == "GROUND")
                {
                    var itemTile = new ItemTile(this, InfoItem.ItemGround);
                    itemTile.a = 1;
                    itemTile.Position = tile.Position;
                    items.Add(itemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.GRASS && GetTileType(i, j) == "GRASS")
                {
                    var itemTile = new ItemTile(this, InfoItem.ItemGrass);
                    itemTile.Position = tile.Position;
                    items.Add(itemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.STONE && GetTileType(i, j) == "STONE")
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemStone);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.TREEBRAK && GetTileType(i, j) == "TREEBRAK")
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemTreeBrak);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.TREETOPS && GetTileType(i, j) == "TREETOPS")
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemTreeBrak);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.DESK && GetTileType(i, j) == "DESK")
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemTreeBrak);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
                }
                else if (type == TileType.IRONORE && GetTileType(i, j) == "IRONORE")
                {
                    ItemTile = new ItemTile(this, InfoItem.ItemIronOre);
                    ItemTile.Position = tile.Position;
                    items.Add(ItemTile);

                    tiles[i, j] = null;
                    block_type[i, j] = "NOME";
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

        public string GetTileType(int i, int j)
        {
            if (i >= 0 && j >= 0 && i < WORLD_WIDTH && j < WORLD_HEIGHT)
            {
                return block_type[i, j];
            }
            else
                return "NULL";
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

        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states)
        {
            var pos = Program.Game.Player.Position;
            var tilesPos = (pos.X / Tile.TILE_SIZE, pos.Y / Tile.TILE_SIZE);
            var tilesPerScreen = (Program.Window.Size.X / Tile.TILE_SIZE, Program.Window.Size.Y / Tile.TILE_SIZE);
            var LeftMostTilesPos = (int)(tilesPos.Item1 - tilesPerScreen.Item1 / 2);
            var TopMostTilesPos = (int)(tilesPos.Item2 - tilesPerScreen.Item2 / 2);

            for (int i = LeftMostTilesPos; i < LeftMostTilesPos + tilesPerScreen.Item1 + 1; i++)
            {
                for (int j = TopMostTilesPos; j < TopMostTilesPos + tilesPerScreen.Item2 + 1; j++)
                {
                    if (i > -1 && j > -1 && i < WORLD_WIDTH && j < WORLD_HEIGHT && tiles[i, j] != null)
                        target.Draw(tiles[i, j]);
                }
            }

            // Рисуем чанки
            /*for (int i = 0; i < Program.Window.Size.X / Tile.TILE_SIZE + 1; i++)
            {
                for (int j = 0; j < Program.Window.Size.Y / Tile.TILE_SIZE + 1; j++)
                {
                    int ishifted = i + XShift;
                    if (ishifted > -1 && ishifted < WORLD_WIDTH && tiles[ishifted, j] != null)
                        target.Draw(tiles[ishifted, j]);
                }
            }*/

            // Рисуем вещи
            foreach (var item in items)
                target.Draw(item);
        }
    }
}
