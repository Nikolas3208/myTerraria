using MyTerraria.Items;
using MyTerraria.NPC;
using MyTerraria.UI;
using MyTerraria.Worlds;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyTerraria
{
    public class World : Transformable, Drawable
    {
        // Кол-во плиток по ширине и высоте
        public static int WORLD_WIDTH = 63;
        public static int WORLD_HEIGHT = 63;
        public static (int, int) WORLD_LOAD = (200, 100);

        public static int GroundLavelMax;

        public static bool PlayerCameraMove { get; private set; }
        public static bool worldGen = false;
        public static bool worldLoad = false;
        public bool worldSave { get; set; }
        public static Random Rand { private set; get; }
        public static Perlin2D Perlin2D { private set; get; }

        // Плитки
        /*public Tile[] Tiles = new Tile[WORLD_WIDTH * WORLD_HEIGHT];
        public Tile tile;*/

        private Chunk[] chunks;
        private Chunk chunk;

        // Предметы
        public List<Item> items = new List<Item>();

        private Vector2f CamPos;

        public float[] PerlinCave = new float[WORLD_WIDTH * WORLD_HEIGHT];
        public float[] Cave = new float[WORLD_WIDTH * WORLD_HEIGHT];

        private float[] terrain = new float[WORLD_WIDTH];
        private float[] mouns = new float[WORLD_WIDTH];
        private float[] temp = new float[WORLD_WIDTH];
        private float[] v = new float[WORLD_WIDTH];

        private float caveFreq = 0.09f;
        private float caveFactor = 18f;
        private float terrainFreq = 0.04f;
        private float terrainFactor = 7f;
        private float terrainMounsFreq = 0.2f;
        private float terrainMounsFactor = 24f;

        private string name;

        // Конструктор класса
        public World(int seed = -1)
        {
            Rand = seed > 0 ? new Random(seed) : new Random((int)DateTime.Now.Ticks);
            Perlin2D = new Perlin2D();
            chunks = new Chunk[WORLD_WIDTH * WORLD_HEIGHT];
            InfoItem.Colectionsgen();
        }

        private void GenerateTerrain(int seed = -1)
        {
            GroundLavelMax = Rand.Next(WORLD_HEIGHT / 5, WORLD_HEIGHT / 2);
            int GroundLevelMin = GroundLavelMax + Rand.Next(100, 250);

            /*for (int i = 0; i < WORLD_WIDTH; i++)
            {
                terrain[i] = Perlin2D.Noise(seed * terrainFreq, (i + seed) * terrainFreq) * terrainFactor;
                mouns[i] = Perlin2D.Noise(seed * terrainMounsFreq, (i + seed) * terrainMounsFreq) * terrainMounsFactor;

                v[i] = (terrain[i] * mouns[i]);
            }

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    PerlinCave[x + y * WORLD_WIDTH] = Perlin2D.Noise((x + seed) * caveFreq, (y + seed) * caveFreq) * caveFactor;

                    Cave[x + y * WORLD_WIDTH] = Perlin2D.Noise(x + seed, y + seed) * caveFreq * caveFactor;
                }
            }

            for (int i = 1; i < WORLD_WIDTH * WORLD_HEIGHT - 1; i++)
            {
                float sum = PerlinCave[i];
                int count = 1;
                for (int k = 1; k <= 3; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += PerlinCave[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += PerlinCave[i2];
                        count++;
                    }
                }

                PerlinCave[i] = (int)(sum / count);
            }

            for (int i = 1; i < WORLD_WIDTH - 1; i++)
            {
                float sum = v[i];
                int count = 1;
                for (int k = 1; k <= 3; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += v[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += v[i2];
                        count++;
                    }
                }

                v[i] = (int)(sum / count);
            }

            for (int i = 1; i < WORLD_WIDTH - 1; i++)
            {
                float sum = mouns[i];
                int count = 1;
                for (int k = 1; k <= 3; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += mouns[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += mouns[i2];
                        count++;
                    }
                }

                mouns[i] = (int)(sum / count);
            }

            //Генерация верхнего уровня и вкраплений камня
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = (int)v[x] + GroundLavelMax; y < WORLD_HEIGHT + (int)mouns[x] + (int)terrain[x] + (int)(GroundLavelMax * 1.5f); y++)
                {
                    SetTile(TileType.Ground, x, y);

                    int index = x + y * WORLD_WIDTH;

                    if (index < WORLD_WIDTH * WORLD_HEIGHT)
                    {
                        if (PerlinCave[index] >= 0.001f)
                            SetTile(TileType.Stone, x, y);
                    }
                }
            }

            //Вырезание пешер
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = (int)mouns[x] + (int)terrain[x] + (int)(GroundLavelMax + 10); y < WORLD_HEIGHT + (v[x] + GroundLavelMax); y++)
                {
                    int index = x + y * WORLD_WIDTH;

                    if (index < WORLD_WIDTH * WORLD_HEIGHT)
                    {
                        //if (Cave[index] <= 0.03f)
                            //SetTile(Worlds.Tiles.Tile, TileType.None, x, y);
                    }
                }
            }

            //Засаживание растительноестью
            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                int y = (int)v[x] + GroundLavelMax;
                Tile tile = (Tile)GetTile(x, y);

                if (GetTile(x, y) != null && GetTile(x, y).type == TileType.Ground)
                    tile.Grass(true);
            }

            /*for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    float v1 = Perlin2D.Noise((((x + seed))) * 0.1f, ((y + seed)) * 0.1f);
                    float v2 = Perlin2D.Noise((((x + seed) * 0.6f)) * 0.01f, ((y + seed) * 0.6f) * 0.01f);
                    float v3 = Perlin2D.Noise((((x + seed)) / 1.5645f) * 0.1f, ((y + seed) / 2.43f) * 0.1f);


                    PerlinCave[x + y * WORLD_WIDTH] = Perlin2D.Noise(x * caveFreq, y * caveFreq) * (v1 + v2 + v3) / 3;

                    v[x + y * WORLD_WIDTH] = (v1 + v2 + v3) / 3;

                    height[x] = Perlin2D.Noise((x + seed) * terrainFreq, (y + seed - GroundLevelMin) * terrainFreq) * 28 + GroundLavelMax;
                }
            }

            // Сглаживание
            for (int i = 1; i < WORLD_WIDTH - 1; i++)
            {
                float sum = height[i];
                int count = 1;
                for (int k = 1; k <= 5; k++)
                {
                    int i1 = i - k;
                    int i2 = i + k;

                    if (i1 > 0)
                    {
                        sum += height[i1];
                        count++;
                    }

                    if (i2 < WORLD_WIDTH)
                    {
                        sum += height[i2];
                        count++;
                    }
                }

                height[i] = (int)(sum / count);
            }

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = (int)height[x] + 16; y < WORLD_HEIGHT; y++)
                {
                    if (y >= 0)
                    {
                        if (v[x + y * WORLD_WIDTH] <= 0.1f)
                        {
                            SetTile(TileType.GroundWall, x, y, false);
                            if (GetTile(x, y - 1) == null)
                                SetTile(TileType.Grass, x, y, false);
                        }
                        if (v[x + y * WORLD_WIDTH] <= 0.01f)
                            SetTile(TileType.StoneWall, x, y, false);

                        if (PerlinCave[x + y * WORLD_WIDTH] <= 0.01f)
                            if (v[x + y * WORLD_WIDTH] <= 0.1f)
                            {
                                SetTile(TileType.GroundWall, x, y, false);
                                if (GetTile(x, y - 1) == null)
                                    SetTile(TileType.Grass, x, y, false);
                            }


                        if (y >= height[x] + 50)
                        {
                            if (v[x + y * WORLD_WIDTH] <= 0.1f)
                            {
                                SetTile(TileType.GroundWall, x, y, false);
                                if (GetTile(x, y - 1) == null)
                                    SetTile(TileType.Grass, x, y, false);
                            }
                            if (v[x + y * WORLD_WIDTH] <= 0.01f)
                                SetTile(TileType.StoneWall, x, y, false);

                            if (v[x + y * WORLD_WIDTH] <= 0.089f && PerlinCave[x + y * WORLD_WIDTH] <= 0.01f)
                                SetTile(TileType.StoneWall, x, y, false);
                        }

                        if (v[x + y * WORLD_WIDTH] <= 0.007f)
                            if (GetTile(x, y) == null && y <= height[x])
                                SetTile(TileType.GroundWall, x, y, false);

                        if (GetTile(x, y) == null && y >= height[x] + 50)
                            SetTile(TileType.StoneWall, x, y, false);


                    }


                    PerlinCave[x + y * WORLD_WIDTH] = Perlin2D.Noise((x + seed) * caveFreq, (y + seed) * caveFreq);

                    if (y < GroundLavelMax + 40)
                    {
                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            if (GetTile(x, y - 1) != null)
                                SetTile(TileType.Ground, x, y, false);
                            else
                            {
                                SetTile(TileType.Grass, x, y, false);
                            }


                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.001f)
                            SetTile(TileType.Stone, x, y, false);

                        if (Rand.Next(0, 10) == 1)
                            SetTree(x, y);
                    }
                    else
                    {
                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.3f)
                            SetTile(TileType.GroundWall, x, y, false);

                        if (PerlinCave[x + y * WORLD_WIDTH] < 0.1f)
                            SetTile(TileType.Stone, x, y, false);
                    }
                }
            }*/

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 1; y < WORLD_HEIGHT; y++)
                {
                    SetChunk(x, y);
                }
            }
        }

        // Генерируем новый мир
        public void GenerateWorldAsync(string name)
        {
            this.name = name;

            GenerateTerrain();

            /*SaveWordlFromFile("Worlds\\" + name + ".world", Tiles);
            {
                Tiles = null;
                worldGen = true;

            }*/
            CreatePlayer( 0, 0 );

            worldGen = true;
            worldLoad = true;
        }

        public void SetChunk(int x, int y, bool isAir = false)
        {
            chunk = new Chunk(isAir);
            chunk.Position = new Vector2f(x * Tile.TILE_SIZE * Chunk.CHUNK_SIZE, y * Tile.TILE_SIZE * Chunk.CHUNK_SIZE) + Position;
            chunks[x + y * WORLD_WIDTH] = chunk;
        }

        public void SetTile(TileType type, float x, float y, bool isgrass = false, bool destriy = false)
        {
            if (x < 0 || x >= WORLD_WIDTH || y < 0 || y >= WORLD_HEIGHT)
                return;

            chunk = GetChunk(x, y);

            if (chunk != null && type != TileType.None && !destriy)
            {
                int Xt = (int)(Math.Floor(x - (Position.X / Chunk.CHUNK_SIZE)));
                int Yt = (int)(Math.Floor(y - (Position.Y / Chunk.CHUNK_SIZE)));
                chunk.SetTile(type, Xt, Yt);
            }
            else if (chunk != null && type != TileType.None && destriy)
            {
                InfoItem item = InfoItem.GetItem(type);

                if (item != null)
                {
                    var itemTile = new ItemTile(this, item);
                    itemTile.Position = new Vector2f(x, y) * Tile.TILE_SIZE;
                    items.Add(itemTile);

                    chunk.SetTile(TileType.None, (int)x, (int)y);
                }

            }
            else
            {

                int Xc = (int)Math.Floor(x / Chunk.CHUNK_SIZE);
                int Yc = (int)Math.Floor(y / Chunk.CHUNK_SIZE);

                SetChunk(Xc, Yc, true);
                chunk = GetChunk(x, y);

                int Xt = (int)(Math.Floor(x - (Position.X / Chunk.CHUNK_SIZE)));
                int Yt = (int)(Math.Floor(y - (Position.Y / Chunk.CHUNK_SIZE)));
                chunk.SetTile(type, Xt, Yt);
            }

            //// Находим соседей
            //Tile upTile = (Tile)GetTile(x, y - 1);     // верхний сосед
            //Tile downTile = (Tile)GetTile(x, y + 1);   // нижний сосед
            //Tile leftTile = (Tile)GetTile(x - 1, y);   // левый сосед
            //Tile rightTile = (Tile)GetTile(x + 1, y);  // правый сосед

            //int index = x + y * WORLD_WIDTH;

            //if (type != TileType.None && !destriy)
            //{
            //    Tile tileBlock = new Tile(type, upTile, downTile, leftTile, rightTile);
            //    tileBlock.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;
            //    tileBlock.Grass(isgrass);
            //    Tiles[x + y * WORLD_WIDTH] = tileBlock;
            //}
            //else if(type != TileType.None && destriy)
            //{
            //    Tile tileType = (Tile)Tiles[index];

            //    foreach (InfoItem item in InfoItem.InfoItems)
            //    {
            //        if (item.Tiletype == type)
            //        {
            //            InfoItem infoItem = item;

            //            if (infoItem != null)
            //            {
            //                var itemTile = new ItemTile(this, infoItem);
            //                itemTile.Position = tileType.Position;
            //                items.Add(itemTile);
            //            }
            //        }
            //    }

            //    Tiles[x + y * WORLD_WIDTH] = null;

            //    // Присваиваем соседей, а соседям эту плитку
            //    if (upTile != null) upTile.DownTile = null;
            //    if (downTile != null) downTile.UpTile = null;
            //    if (leftTile != null) leftTile.RightTile = null;
            //    if (rightTile != null) rightTile.LeftTile = null;
            //}
            //else
            //{
            //    Tiles[x + y * WORLD_WIDTH] = null;

            //    // Присваиваем соседей, а соседям эту плитку
            //    if (upTile != null) upTile.DownTile = null;
            //    if (downTile != null) downTile.UpTile = null;
            //    if (leftTile != null) leftTile.RightTile = null;
            //    if (rightTile != null) rightTile.LeftTile = null;
            //}

        }

        /*public void SetTile(TileType type, int x, int y, bool destroy)
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
                    if (type == TileType.GroundWall || type == TileType.StoneWall || type == TileType.BoardWall)
                        tile = new Tile(type, Color.White, upTile, downTile, leftTile, rightTile, true, true);
                    else
                        tile = new Tile(type, Color.White, upTile, downTile, leftTile, rightTile, false, false);
                    tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE) + Position;
                    tiles[index] = tile;
                }
            }
            else
            {
                if (type != TileType.None && !tile.SetWall && !tile.isWall)
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
                                    tile = new Tile(TileType.GroundWall, Color.White, upTile, downTile, leftTile, rightTile, false, false);
                                    tile.Position = tiles[index].Position;

                                    tiles[index] = tile;
                                }
                            }
                        }
                    }
                }
                else if (tile.SetWall)
                {
                    tiles[index] = null;
                    SetTile(type, x, y, false);
                }
                else
                    tiles[index] = null;

                // Присваиваем соседей, а соседям эту плитку
                if (upTile != null) upTile.DownTile = null;
                if (downTile != null) downTile.UpTile = null;
                if (leftTile != null) leftTile.RightTile = null;
                if (rightTile != null) rightTile.LeftTile = null;
            }
        }*/

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
            if (x / Chunk.CHUNK_SIZE >= 0 && y / Chunk.CHUNK_SIZE >= 0 && x / Chunk.CHUNK_SIZE < WORLD_WIDTH && y / Chunk.CHUNK_SIZE < WORLD_HEIGHT)
            {
                chunk = GetChunk(x, y);
                if (chunk != null)
                    return chunk.GetTile(x, y);
            }
            return null;
        }

        public Chunk GetChunk(float x, float y)
        {
            x = (float)Math.Floor(x / Chunk.CHUNK_SIZE);
            y = (float)Math.Floor(y / Chunk.CHUNK_SIZE);

            if (x >= 0 && y >= 0 && x < WORLD_WIDTH && y < WORLD_HEIGHT)
                return chunks[(int)(x + y * WORLD_WIDTH)];

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
        private Task SaveWordlFromFile(string path, Tile[] tiles)
        {
            if (File.Exists(path)) File.Delete(path);

            File.Create(path).Close();

            StreamWriter sw = new StreamWriter("Worlds\\" + name + ".world", true, Encoding.UTF8);

            for (int x = 0; x < WORLD_WIDTH; x++)
            {
                for (int y = 0; y < WORLD_HEIGHT; y++)
                {
                    if (GetTile(x, y) != null)
                    {
                        Tile tile = (Tile)GetTile(x, y);
                        sw.Write("p " + tile.Position.X / 16 + " " + tile.Position.Y / 16 + "\nt " + tile.type + " " + tile.isGrass + "\n;" + "\n");
                    }
                }
            }
            sw.Write("s " + WORLD_WIDTH / 2 + " " + (int)(v[WORLD_WIDTH / 2] + GroundLavelMax));
            sw.Close();
            return Task.CompletedTask;
        }
        public void LoadWorld(string path)
        {
            /*Tiles = new Tile[WORLD_WIDTH * WORLD_HEIGHT];

            Vector2f pos = new Vector2f();
            TileType tileType = TileType.None;
            bool isgrass = false;

            if (File.Exists(path))
                using (StreamReader streamReader = new StreamReader(path))
                {
                    while (!streamReader.EndOfStream)
                    {
                        List<string> words = new List<string>(streamReader.ReadLine().Split(' '));
                        words.RemoveAll(s => s == string.Empty);

                        if (words.Count == 0)
                            continue;

                        string type = words[0];
                        words.RemoveAt(0);

                        switch (type)
                        {
                            case "p":
                                pos = new Vector2f(int.Parse(words[0]), int.Parse(words[1]));
                                break;

                            case "t":
                                tileType = (TileType)Enum.Parse(typeof(TileType), words[0]);
                                isgrass = bool.Parse(words[1]);
                                break;
                            case ";":
                                SetTile(tileType, (int)(pos.X), (int)(pos.Y), isgrass);
                                break;
                            case "s":
                                CreatePlayer(int.Parse(words[0]), int.Parse(words[1]));
                                break;
                        }
                    }
                }

            worldLoad = true;*/
        }
        private void CreatePlayer(int x, int y)
        {
            Program.Game.Player = new Player(this);
            Program.Game.Player.StartPosition = new Vector2f(x * 16, y * 16);
            Program.Game.Player.Spawn();
            Program.Game.Player.AddTools();

            Program.Game.Player.Invertory = new UIInvertory();
        }
        // Нарисовать мир
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            CamPos = Program.pos;

            var tilePos = (CamPos.X / Tile.TILE_SIZE / Chunk.CHUNK_SIZE, CamPos.Y / Tile.TILE_SIZE / Chunk.CHUNK_SIZE);
            var tilesPerScreen = (Program.Window.Size.X * Program.zoom / Tile.TILE_SIZE / Chunk.CHUNK_SIZE, Program.Window.Size.Y * Program.zoom / Tile.TILE_SIZE / Chunk.CHUNK_SIZE);
            var LeftMostTilesPos = (int)(tilePos.Item1 - tilesPerScreen.Item1 / 2);
            var TopMostTilesPos = (int)(tilePos.Item2 - tilesPerScreen.Item2 / 2);

            for (int x = LeftMostTilesPos; x < LeftMostTilesPos + tilesPerScreen.Item1 + 1; x++)
            {
                for (int y = TopMostTilesPos; y < TopMostTilesPos + tilesPerScreen.Item2 + 1; y++)
                {
                    if (x > -1 && y > -1 && x < WORLD_WIDTH && y < WORLD_HEIGHT)
                    {
                        int index = x + y * WORLD_WIDTH;
                        if (chunks[index] != null)
                        {
                            FloatRect chunkRect = chunks[index].GetFloatRect();
                            DebugRender.AddRectangle(chunkRect, Color.Red);

                            target.Draw(chunks[index]);
                        }

                        /*if (chunks[index] != null)
                        {
                            if (GetTile(x, y).type != TileType.Treebark)
                            {
                                for (int i = 0; i <= 15; i++)
                                {
                                    byte color = (byte)(255 - i * 17);

                                    if (GetTile(x, y - i) == null)
                                    {
                                        color = (byte)(255 - i * 10);
                                    }
                                    if (GetTile(x, y + i) == null)
                                        color = (byte)(255 - i * 10);

                                    if (GetTile(x, y + i) != null)
                                    {
                                        if (GetTile(x, y - i) != null)
                                        {
                                            if (GetITile(x, y).Color != new Color(color, color, color))
                                                Tiles[x + y * WORLD_WIDTH].UpdateColor(new Color(color, color, color));
                                        }
                                    }

                                }
                            }

                            FloatRect chunk = GetChunk(x, y).GetFloatRect();
                            DebugRender.AddRectangle(chunk, Color.Magenta);

                            target.Draw(chunks[x + y * WORLD_WIDTH], states);
                        }*/
                    }
                }
            }

            try
            {
                // Рисуем вещи
                foreach (var item in items)
                    /*if (item.Position.X / 16 > LeftMostTilesPos && item.Position.X / 16 < LeftMostTilesPos + tilesPerScreen.Item1 + 1)
                        if (item.Position.Y / 16 > TopMostTilesPos && item.Position.Y / 16 < TopMostTilesPos + tilesPerScreen.Item2 + 1)*/
                            target.Draw(item);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }
    }
}
