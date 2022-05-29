using MyTerraria.Items;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;

namespace MyTerraria
{
    class World : Transformable, Drawable
    {
        private Vector2u windowSize;
        //public int MAX_XShift { get { return WORLD_WIDTH;/* - (int)windowSize.X / 2 / Tile.TILE_SIZE;*/ } }
        //public int MIN_XShift { get { return 0;/* -((int)windowSize.X / 2 / Tile.TILE_SIZE);*/ } }

        // Кол-во плиток по ширине и высоте
        public const int WORLD_WIDTH = 300;
        public const int WORLD_HEIGHT = 1000;

        public static Random Rand { private set; get; }

        // Плитки
        Tile[,] tiles;

        // Предметы
        List<Item> items = new List<Item>();

        // Конструктор класса
        public World()
        {
            tiles = new Tile[600, 2000];
        }

        // Генерируем новый мир
        public void GenerateWorld(int seed = -1)
        {
            Rand = seed >= 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);

            int groundLevelMax = Rand.Next(10, 30);
            int groundLevelMin = groundLevelMax + Rand.Next(10, 50);

            // Генерация уровня ландшафта
            int[] arr = new int[WORLD_WIDTH];
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                int dir = Rand.Next(0, 2) == 1 ? 1 : -1;

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
                for (int k = 1; k <= 5; k++)
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

            int c = 0;

            // Ставим плитки на карту
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                SetTile(TileType.GRASS, i, arr[i] + 100);

                for (int j = arr[i] + 1; j < WORLD_WIDTH; j++)
                    SetTile(TileType.GROUND, i, j + 100);
            }

            //Генерация деревьев
            for (int i = 0; i < WORLD_WIDTH; i++)
            {
                i += Rand.Next(4, 10);
                int f1 = Rand.Next(5, 9);
                for (int j = 0; j < WORLD_HEIGHT; j++)
                {
                    if (type_Tile[i, j] == "GRASS")
                    {
                        c = -j + 1;
                        int f = Rand.Next(-1, 1);

                        SetTile(TileType.TREEBRAK, i, -9 - c);
                        SetTile(TileType.TREEBRAK, i, -8 - c);
                        SetTile(TileType.TREEBRAK, i, -7 - c);
                        SetTile(TileType.TREEBRAK, i, -6 - c);
                        SetTile(TileType.TREEBRAK, i, -5 - c);
                        SetTile(TileType.TREEBRAK, i, -4 - c);
                        SetTile(TileType.TREEBRAK, i, -3 - c);
                        SetTile(TileType.TREEBRAK, i, -2 - c);
                        SetTile(TileType.TREEBRAK, i, -1 - c);
                        SetTile(TileType.TREEBRAK, i, -0 - c);
                        SetTile(TileType.TREEBRAK, i + f, -0 - c);
                                                        
                        SetTile(TileType.TREETOPS, i, -9 - c);
                    }
                }
            }

            

            //Program.Game.Player.StartPosition = new Vector2f((WORLD_WIDTH / 2) * 16, c);

        }

        public string[,] type_Tile = new string[600, 2000];


        public TileType tile1;

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
            // Находим соседей
            Tile upTile = GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = GetTile(i + 1, j);  // Правый сосед

            var tile = new Tile(type, upTile, downTile, leftTile, rightTile);

            if (type != TileType.NONE)
            {
                tile.Position = new Vector2f(i * Tile.TILE_SIZE, j * Tile.TILE_SIZE) + Position;
                tiles[i, j] = tile;
                type_Tile[i, j] = type.ToString();
            }
            tile1 = type;
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

            // Находим соседей
            Tile upTile = GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = GetTile(i + 1, j);  // Правый сосед

            var tile = tiles[i, j];

            if (tile != null)
            {
                if (type == TileType.GROUND)
                {
                    var item = new ItemTile(this, InfoItem.ItemGround);
                    item.Position = tile.Position;
                    items.Add(item);
                }
                else if (type == TileType.GRASS)
                {
                    var item = new ItemTile(this, InfoItem.ItemGrass);
                    item.Position = tile.Position;
                    items.Add(item);
                }
                else if (type == TileType.STONE)
                {
                    var item = new ItemTile(this, InfoItem.ItemStone);
                    item.Position = tile.Position;
                    items.Add(item);
                }
                else if (type == TileType.TREEBRAK)
                {
                    var item = new ItemTile(this, InfoItem.ItemTreeBrak);
                    item.Position = tile.Position;
                    items.Add(item);
                }
                else if (type == TileType.DESK)
                {
                    var item = new ItemTile(this, InfoItem.ItemTreeBrak);
                    item.Position = tile.Position;
                    items.Add(item);
                }
            }
            

            tiles[i, j] = null;

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
