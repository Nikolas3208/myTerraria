using MyTerraria.Items;
using MyTerraria.NPC;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace MyTerraria
{
    public class World : Transformable, Drawable
    {
        // Кол-во плиток по ширине и высоте
        public static int WORLD_WIDTH = 100;
        public static int WORLD_HEIGHT = 100;

        public static int GroundLavelMax;

        public static bool PlayerCameraMove { get; private set; }
        public static bool worldGen = false;
        public static bool worldLoad = false;
        private bool isWall;
        public bool worldSave { get; set; }

        public static Random Rand { private set; get; }
        public static Perlin2D Perlin2D { private set; get; }

        // Плитки
        public Tile[] tiles = new Tile[WORLD_WIDTH * WORLD_HEIGHT];
        public Tile tile;

        // Предметы
        public List<Item> items = new List<Item>();

        private Vector2f CamPos;

        public  float[] PerlinCave = new float[WORLD_WIDTH * WORLD_HEIGHT];

        private float[] height = new float[WORLD_WIDTH];

        private float caveFreq = 0.08f;
        private float terrainFreq = 0.04f;

        private string name;
        private string[] cells;
        private char[] world = new char[WORLD_WIDTH * WORLD_HEIGHT];

        // Конструктор класса
        public World(int seed = -1)
        {
            Rand = seed > 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);
            Perlin2D = new Perlin2D();
            InfoItem.Colectionsgen();
        }

        private void GenerateTerrain(int seed = -1)
        {

            GroundLavelMax = Rand.Next(20, 50);
            int GroundLevelMin = GroundLavelMax + Rand.Next(10, 15);
 
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                height[x] = Perlin2D.Noise((x + seed) * terrainFreq, (seed) * terrainFreq) * 6 + GroundLavelMax;
                for (int y = (int)height[x]; y < WORLD_HEIGHT; y++)
                {
                    PerlinCave[x + y * WORLD_WIDTH] = Perlin2D.Noise((x + seed) * caveFreq, (y + seed) * caveFreq);
                    if (y < GroundLavelMax + 40)
                    {
                        //if (y > GroundLavelMax + 2)
                            isWall = true;
                        /*else
                            isWall = false;*/

                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            if (GetTile(x, y - 1) != null)
                                SetTile(TileType.Ground, x, y, false, isWall);
                            else
                            {
                                SetTile(TileType.Grass, x, y, false, isWall);
                            }


                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.001f)
                            SetTile(TileType.Stone, x, y, false, isWall);

                        if (Rand.Next(0, 10) == 1)
                            SetTree(x, y);
                    }
                    else
                    {
                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            SetTile(TileType.Ground, x, y, false, true);

                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.1f)
                            SetTile(TileType.Stone, x, y, false, false);
                    }
                }
            }

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    if (GetTile(x, y) == null)
                        SaveWorld(TileType.None, x, y);
                    else
                        SaveWorld(GetTile(x, y).type, x, y);
                }
            }
        }

        private void SetTree(int x, int y)
        {
            bool tree = true;

            int maxHeight = Rand.Next(4, 30);
            int x2 = Rand.Next(-1, 2);

            if (GetTile(x, y) != null && GetTile(x, y).type == TileType.Grass)
            {
                for (int y1 = 1; y1 < maxHeight + 6; y1++)
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
                            SetTile(TileType.Treebark, x, y - y1, false, false);

                            if (maxHeight > 6)
                                SetTile(TileType.Treetops, x, y - maxHeight, false, false);
                            else
                                SetTile(TileType.Treebark, x, y - maxHeight, false, false);
                        }
                    }

                tree = true;
            }
        }

        // Генерируем новый мир
        public void GenerateWorld(string name)
        {
            this.name = name;

            GenerateTerrain();

            tiles = null;

            File.WriteAllText("Worlds\\" + name + ".world", " ");

            StreamWriter sw = new StreamWriter("Worlds\\" + name + ".world", true, Encoding.UTF8);
            sw.Write(world);
            sw.Close();

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

                    if (tile != null && tile.type == TileType.Treebark)
                    {
                        SetTile(TileType.Board, x, y - y1, true, false);
                        if (tileLeft != null && tileLeft.type == TileType.Treebark)
                            SetTile(TileType.Board, x - 1, y - y1, true, false);
                        if (tileRight != null && tileRight.type == TileType.Treebark)
                            SetTile(TileType.Board, x + 1, y - y1, true, false);
                    }
                    else if (tile != null && tile.type == TileType.Treetops)
                    {
                        SetTile(TileType.Board, x, y - y1, true, false);

                        for (int i = 0; i < Rand.Next(1, 8); i++)
                        {
                            var itemTile = new ItemTile(this, InfoItem.ItemTreeSapling);
                            itemTile.Position = tile.Position;
                            items.Add(itemTile);
                        }
                    }
                    else
                        return;

                    Thread.Sleep(60);
                }
            });
        }

        public void SetTile(TileType type, int x, int y, bool destroy, bool isWall)
        {
            if (x < 0 || x >= WORLD_WIDTH || y < 0 || y >= WORLD_HEIGHT)
                return;

            // Находим соседей
            Tile upTile = GetTile(x, y - 1);     // верхний сосед
            Tile downTile = GetTile(x, y + 1);   // нижний сосед
            Tile leftTile = GetTile(x - 1, y);   // левый сосед
            Tile rightTile = GetTile(x + 1, y);  // правый сосед

            int index = x + y * WORLD_WIDTH;

            if (!destroy)
            {
                if (type != TileType.None)
                {
                    tile = new Tile(type, Color.White, upTile, downTile, leftTile, rightTile, isWall);
                    tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;
                    tiles[index] = tile;
                }
            }
            else
            {
                if (type != TileType.None)
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

                                if (!tile.isWall)
                                    tiles[index] = null;
                                else //if (tiles[index].type == TileType.Ground)
                                {
                                    tile = new Tile(TileType.GroundWall, Color.White, upTile, downTile, leftTile, rightTile, false);
                                    tile.Position = tiles[index].Position;

                                    tiles[index] = tile;
                                }
                            }
                        }
                    }
                }
                else
                    tiles[index] = null;

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

            if(Keyboard.IsKeyPressed(Keyboard.Key.F))
            {
                SaveWorld(false);
            }
        }

        public void LoadWorld(string name)
        {
            this.name = name;
            tiles = new Tile[WORLD_WIDTH * WORLD_HEIGHT];

            if (LoadFromFile("Worlds/" + name + ".world"))
            {
                for (int y = 0; y < cells.Length; y++)
                {
                    for (int x = 0; x < cells[y].Length; x++)
                    {
                        switch (cells[y][x])
                        {
                            case ' ': SetTile(TileType.None, x, y, true, false); break;
                            case '0': SetTile(TileType.Ground, x, y, false, false); break;
                            case ')': SetTile(TileType.Ground, x, y, false, true); break;
                            case '1': SetTile(TileType.Stone, x, y, false, false); break;
                            case '!': SetTile(TileType.StoneWall, x, y, false, false); break;
                            case '2': SetTile(TileType.Grass, x, y, false, false); break;
                            case 't': SetTile(TileType.Treebark, x, y, false, false); break;
                            case 'T': SetTile(TileType.Treetops, x, y, false, false); break;
                            case 's': SetTile(TileType.Treesapling, x, y, false, false); break;
                            case 'm': SetTile(TileType.Mushroom, x, y, false, false); break;
                            case 'b': SetTile(TileType.Board, x, y, false, false); break;
                            case 'B': SetTile(TileType.BoardWall, x, y, false, false); break;
                        }
                    }
                }
            }

            worldGen = true;
            worldLoad = true;
        }

        private bool LoadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) return false;

            cells = File.ReadAllLines(fileName);

            return true;
        }

        public void SaveWorld(bool close)
        {
            Task.Run(() =>
            {
                File.WriteAllText("Worlds\\" + name + ".world", " ");

                if(close)
                    for (int x = 0; x < WORLD_WIDTH; x++)
                    {
                        for (int y = 0; y < WORLD_HEIGHT; y++)
                        {
                            if (GetTile(x, y) == null)
                                SaveWorld(TileType.None, x, y);
                            else
                                SaveWorld(GetTile(x, y).type, x, y);
                        }
                    }

                StreamWriter sw = new StreamWriter("Worlds\\" + name + ".world", true, Encoding.UTF8);
                sw.Write(world);
                sw.Close();

                worldSave = false;

                Program.Window.Close();
            });
        }

        private void SaveWorld(TileType type, int x, int y)
        {
            int index = x + y * WORLD_WIDTH;

            if (x == WORLD_WIDTH - 1)
            {
                world[index] = '\n';
                return;
            }

            switch (type)
            {
                case TileType.None:
                    world[index] = ' ';
                    break;
                case TileType.Ground:
                    if (!tiles[index].isWall)
                        world[index] = '0';
                    else
                        world[index] = ')';
                    break;
                case TileType.GroundWall:
                    
                    break;
                case TileType.Sand:
                    break;
                case TileType.Grass:
                    world[index] = '2';
                    break;
                case TileType.Stone:
                    world[index] = '1';
                    break;
                case TileType.StoneWall:
                    world[index] = '!';
                    break;
                case TileType.Treebark:
                    world[index] = 't';
                    break;
                case TileType.Treetops:
                    world[index] = 'T';
                    break;
                case TileType.Board:
                    world[index] = 'b';
                    break;
                case TileType.BoardWall:
                    world[index] = 'B';
                    break;
                case TileType.Ironore:
                    break;
                case TileType.Coperore:
                    break;
                case TileType.Goldore:
                    break;
                case TileType.Silverore:
                    break;
                case TileType.Vegetation:
                    break;
                case TileType.Mushroom:
                    world[index] = 'm';
                    break;
                case TileType.Treesapling:
                    world[index] = 's';
                    break;
                case TileType.Torch:
                    break;
            }

        }

        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            CamPos = Program.pos;

            var tilePos = (CamPos.X / Tile.TILE_SIZE, CamPos.Y / Tile.TILE_SIZE);
            var tilesPerScreen = (Program.Window.Size.X / Tile.TILE_SIZE, Program.Window.Size.Y / Tile.TILE_SIZE);
            var LeftMostTilesPos = (int)(tilePos.Item1 - tilesPerScreen.Item1 / 2);
            var TopMostTilesPos = (int)(tilePos.Item2 - tilesPerScreen.Item2 / 2);

            if (!Program.debagDraw)
            {
                for (int x = LeftMostTilesPos; x < LeftMostTilesPos + tilesPerScreen.Item1 + 1; x++)
                {
                    for (int y = TopMostTilesPos; y < TopMostTilesPos + tilesPerScreen.Item2 + 1; y++)
                    {
                        if (x > -1 && y > -1 && x < WORLD_WIDTH && y < WORLD_HEIGHT)
                        {
                            if (GetTile(x, y) == null)
                                SaveWorld(TileType.None, x, y);
                            else
                                SaveWorld(GetTile(x, y).type, x, y);

                            if (tiles[x + y * WORLD_WIDTH] != null)
                            {
                                Tile upTile = GetTile(x, y - 1);
                                Tile downTile = GetTile(x, y + 1);
                                Tile leftTile = GetTile(x - 1, y);
                                Tile rightTile = GetTile(x + 1, y);


                                if (downTile == null && rightTile == null && leftTile == null)
                                    TreeFelling(x, y);
                                else if (GetTile(x, y) != null && GetTile(x, y).type == TileType.Grass && GetTile(x, y - 1) != null && GetTile(x, y - 1).type == TileType.Treebark)
                                    TreeFelling(x, y);

                                if (tiles[x + y * WORLD_WIDTH] != null && tiles[x + y * WORLD_WIDTH].type != TileType.None)
                                {
                                    target.Draw(tiles[x + y * WORLD_WIDTH], states);
                                }
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
                            target.Draw(tiles[x + y * WORLD_WIDTH], states);
                        }
                    }
                }


            }

            try
            {
                // Рисуем вещи
                foreach (var item in items)
                    if (item.Position.X / 16 > LeftMostTilesPos && item.Position.X / 16 < LeftMostTilesPos + tilesPerScreen.Item1 + 1)
                        if (item.Position.Y / 16 > TopMostTilesPos && item.Position.Y / 16 < TopMostTilesPos + tilesPerScreen.Item2 + 1)
                            target.Draw(item);
            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
