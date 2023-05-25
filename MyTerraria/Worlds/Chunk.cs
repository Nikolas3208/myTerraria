using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.Worlds
{
    public class Chunk : Transformable, Drawable
    {
        public const int CHUNK_SIZE = 16;

        private Tile[,] tiles;
        private Tile tile;

        public Chunk(bool isAir)
        {
            tiles = new Tile[CHUNK_SIZE, CHUNK_SIZE];

            if (!isAir)
            {
                for (int x = 0; x < CHUNK_SIZE; x++)
                {
                    for (int y = 0; y < CHUNK_SIZE; y++)
                    {
                        SetTile(TileType.Ground, x, y);
                    }
                }
            }
        }

        public void SetTile(TileType type, int i, int j)
        {
            // Находим соседей
            Tile upTile = GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = GetTile(i + 1, j);  // Правый сосед

            int x = (int)(Math.Floor(i - (Position.X / CHUNK_SIZE)));
            int y = (int)(Math.Floor(j - (Position.Y / CHUNK_SIZE)));
            if (x >= 0 && y >= 0 && x < CHUNK_SIZE && y < CHUNK_SIZE)
            {
                if (type != TileType.None)
                {
                    var tile = new Tile(type, upTile, downTile, leftTile, rightTile);
                    tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE);
                    tiles[x, y] = tile;
                }
                else
                {
                    tiles[x, y] = null;

                    // Присваиваем соседей, а соседям эту плитку
                    if (upTile != null) upTile.DownTile = null;
                    if (downTile != null) downTile.UpTile = null;
                    if (leftTile != null) leftTile.RightTile = null;
                    if (rightTile != null) rightTile.LeftTile = null;
                }
            }
        }

        public Tile GetTile(float i, float j)
        {
            int x = (int)(Math.Floor(i - (Position.X / CHUNK_SIZE)));
            int y = (int)(Math.Floor(j - (Position.Y / CHUNK_SIZE)));

            if (x >= 0 && y >= 0 && x < CHUNK_SIZE && y < CHUNK_SIZE)
                return tiles[x, y];

            return null;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    if (tiles[x, y] != null)
                    {
                        //FloatRect tile = new FloatRect(tiles[x, y].Position + Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
                        //DebugRender.AddRectangle(tile, Color.Magenta);

                        target.Draw(tiles[x, y], states);
                    }
                }
            }
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE) * CHUNK_SIZE);
        }
    }
}
