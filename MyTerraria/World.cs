using MyTerraria.Items;
using MyTerraria.NPC;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace MyTerraria
{
    public class World : Transformable, Drawable
    {
        // Кол-во плиток по ширине и высоте
        public static int WORLD_WIDTH = 1000;
        public static int WORLD_HEIGHT = 1000;

        public static int GroundLavelMax;

        public static bool worldGen = false;

        public static Random Rand { private set; get; }
        public static Perlin2D Perlin2D { private set; get; }
        public static bool PlayerCameraMove { get; private set; }

        public static float[] PerlinCave = new float[WORLD_WIDTH * WORLD_HEIGHT];

        // Плитки
        public Tile[] tiles = new Tile[WORLD_WIDTH * WORLD_HEIGHT];
        public Tile tile;

        // Предметы
        public List<Item> items = new List<Item>();

        private float[] height = new float[WORLD_WIDTH];

        private float caveFreq = 0.08f;
        private float terrainFreq = 0.04f;

        // Конструктор класса
        public World(int seed = -1)
        {
            Rand = seed > 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);
            Perlin2D = new Perlin2D();
            InfoItem.Colectionsgen();
        }

        private void GenerateTerrain(int seed = -1)
        {
            Rand = seed >= 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);

            GroundLavelMax = Rand.Next(20, 50);
            int GroundLevelMin = GroundLavelMax + Rand.Next(10, 15);

            //await Task.Run(() =>
            //{
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                height[x] = Perlin2D.Noise((x + seed) * terrainFreq, (seed) * terrainFreq) * 6 + GroundLavelMax;
                for (int y = (int)height[x]; y < WORLD_HEIGHT; y++)
                {
                    PerlinCave[x + y * WORLD_WIDTH] = Perlin2D.Noise((x + seed) * caveFreq, (y + seed) * caveFreq);
                    if (y < GroundLavelMax + 40)
                    {
                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            if (GetTile(x, y - 1) != null)
                                SetTile(TileType.GROUND, x, y, false);
                            else
                                SetTile(TileType.GRASS, x, y, false);

                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.001f)
                            SetTile(TileType.STONE, x, y, false);
                    }
                    else
                    {
                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            SetTile(TileType.GROUND, x, y, false);

                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.1f)
                            SetTile(TileType.STONE, x, y, false);
                    }
                }
            }
            //});
        }

        private void GenerateTree()
        {
            bool tree = true;

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = (int)height[x]; y < WORLD_HEIGHT; y++)
                {
                    int maxHeight = Rand.Next(4, 30);
                    int x2 = Rand.Next(-1, 2);

                    if (GetTile(x, y) != null && GetTile(x, y).type == TileType.GRASS)
                    {
                        for (int y1 = 1; y1 < maxHeight; y1++)
                        {
                            if (GetTile(x, y - y1) != null)
                                tree = false;
                            else if (GetTile(x, y - y1) == null && y1 == maxHeight - 1 && tree)
                                tree = true;
                        }

                        if (tree)
                            for (int y1 = 1; y1 < maxHeight; y1++)
                            {
                                if (GetTile(x, y - y1) == null)
                                {
                                    //if (GetTile(x + x2, y) != null && GetTile(x + x2, y).type == TileType.GRASS)
                                        //SetTile(TileType.TREEBARK, x + x2, y - 1, false);

                                    SetTile(TileType.TREEBARK, x, y - y1, false);

                                    if (maxHeight > 6)
                                        SetTile(TileType.TREETOPS, x, y - maxHeight, false);
                                    else
                                        SetTile(TileType.TREEBARK, x, y - maxHeight, false);
                                }
                            }

                        tree = true;
                    }
                }

                x += Rand.Next(2, 15);
            }
        }

        // Генерируем новый мир
        public void GenerateWorld()
        {
            GenerateTerrain();

            GenerateTree();

            worldGen = true;
        }

        public void TreeFelling(int x, int y)
        {
            Task.Run(() =>
            {
                for (int y1 = 0; ; y1++)
                {
                    Tile tile = GetTile(x, y - y1);
                    Tile tileLeft = GetTile(x - 1, y - y1);
                    Tile tileRight = GetTile(x + 1, y - y1);

                    if (tile != null && tile.type == TileType.TREEBARK)
                    {
                        SetTile(TileType.TREEBARK, x, y - y1, true);
                        if (tileLeft != null && tileLeft.type == TileType.TREEBARK)
                            SetTile(TileType.TREEBARK, x - 1, y - y1, true);
                        if (tileRight != null && tileRight.type == TileType.TREEBARK)
                            SetTile(TileType.TREEBARK, x + 1, y - y1, true);
                    }
                    else if (tile != null && tile.type == TileType.TREETOPS)
                        SetTile(TileType.TREETOPS, x, y - y1, true);
                    else
                        return;

                    Thread.Sleep(60);
                }
            });
        }

        public void SetTile(TileType type, int x, int y, bool destroy)
        {
            if (x < 0 || x > WORLD_WIDTH || y < 0 || y > WORLD_HEIGHT)
                return;

            // Находим соседей
            Tile upTile = GetTile(x, y - 1);     // верхний сосед
            Tile downTile = GetTile(x, y + 1);   // нижний сосед
            Tile leftTile = GetTile(x - 1, y);   // левый сосед
            Tile rightTile = GetTile(x + 1, y);  // правый сосед

            int index = x + y * WORLD_WIDTH;

            if (!destroy)
            {
                if (type != TileType.NONE)
                {
                    tile = new Tile(type, Color.White, upTile, downTile, leftTile, rightTile);
                    tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;
                    tiles[index] = tile;
                }
            }
            else
            {
                if (type != TileType.NONE)
                {
                    tile = tiles[index];

                    foreach (InfoItem item in InfoItem.InfoItems)
                    {
                        if (item.Tiletype == type)
                        {
                            InfoItem infoItem = item;

                            if (infoItem != null)
                            {
                                var itemTile = new ItemTile(this, infoItem);
                                itemTile.Position = tile.Position;
                                items.Add(itemTile);

                                tiles[index] = null;
                            }
                        }
                    }

                }
                // Присваиваем соседей, а соседям эту плитку
                if (upTile != null) upTile.DownTile = null;
                if (downTile != null) downTile.UpTile = null;
                if (leftTile != null) leftTile.RightTile = null;
                if (rightTile != null) rightTile.LeftTile = null;
            }
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

        public Tile GetTile(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < WORLD_WIDTH && y < WORLD_HEIGHT)
                return tiles[x + y * WORLD_WIDTH];

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
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            states.Transform *= Transform;

            Vector2f pos = Program.pos;

            var tilePos = (pos.X / Tile.TILE_SIZE, pos.Y / Tile.TILE_SIZE);
            var tilesPerScreen = (Program.Window.Size.X / Tile.TILE_SIZE, Program.Window.Size.Y / Tile.TILE_SIZE);
            var LeftMostTilesPos = (int)(tilePos.Item1 - tilesPerScreen.Item1 / 2);
            var TopMostTilesPos = (int)(tilePos.Item2 - tilesPerScreen.Item2 / 2);



            if (!Program.debagDraw)
            {
                for (int x = LeftMostTilesPos; x < LeftMostTilesPos + tilesPerScreen.Item1 + 1; x++)
                {
                    for (int y = TopMostTilesPos; y < TopMostTilesPos + tilesPerScreen.Item2 + 1; y++)
                    {
                        if (x > -1 && y > -1 && x < WORLD_WIDTH && y < WORLD_HEIGHT && tiles[x + y * WORLD_WIDTH] != null)
                        {
                            var downTile = GetTile(x, y + 1);
                            var rightTile = GetTile(x - 1, y);
                            var leftTile = GetTile(x + 1, y);
                            if (downTile == null && rightTile == null && leftTile == null)
                                TreeFelling(x, y);
                            else if (GetTile(x, y) != null && GetTile(x, y).type == TileType.GRASS && GetTile(x, y - 1) != null && GetTile(x, y - 1).type == TileType.TREEBARK)
                                TreeFelling(x, y);

                            if (tiles[x + y * WORLD_WIDTH] != null)
                            { 
                                //tiles[x + y * WORLD_WIDTH].SetVisible(true);
                                target.Draw(tiles[x + y * WORLD_WIDTH], states);
                                //tiles[x + y * WORLD_WIDTH].SetVisible(false);
                            }
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < WORLD_WIDTH; x++)
                {
                    for (int y = 0; y < WORLD_HEIGHT; y++)
                    {
                        if (x > -1 && y > -1 && x < WORLD_WIDTH && y < WORLD_HEIGHT && tiles[x + y * WORLD_WIDTH] != null)
                        {
                            //tiles[x + y * WORLD_WIDTH].SetVisible(true);
                            target.Draw(tiles[x + y * WORLD_WIDTH], states);
                            //tiles[x + y * WORLD_WIDTH].SetVisible(false);
                        }
                    }
                }


            }
            // Рисуем вещи
            foreach (var item in items)
                if (item.Position.X / 16 > LeftMostTilesPos && item.Position.X / 16 < LeftMostTilesPos + tilesPerScreen.Item1 + 1)
                    if (item.Position.Y / 16 > TopMostTilesPos && item.Position.Y / 16 < TopMostTilesPos + tilesPerScreen.Item2 + 1)
                        target.Draw(item);

            stopwatch.Stop();


            Program.Game.debag.SetMessageLine("Render time world on ms: " + stopwatch.Elapsed.Milliseconds.ToString());
        }
    }
}
