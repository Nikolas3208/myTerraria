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

        SortedDictionary<TileType, Tile> Tiles = new SortedDictionary<TileType, Tile>();

        // Плитки
        private Tile[] tiles;
        private Tile tile;

        private Vertex[] tileMesh;
        private int IdTile;
        public Chunk(bool isAir = false)
        {
            tiles = new Tile[CHUNK_SIZE * CHUNK_SIZE];
            tileMesh = new Vertex[CHUNK_SIZE * CHUNK_SIZE * 6];

            IdTile = 0;
        }

        public void SetTile(TileType type, int i, int j)
        {
            // Находим соседей
            Tile upTile = Program.Game.World.GetTile(i, j - 1);     // Верхний сосед
            Tile downTile = Program.Game.World.GetTile(i, j + 1);   // Нижний сосед
            Tile leftTile = Program.Game.World.GetTile(i - 1, j);   // Левый сосед
            Tile rightTile = Program.Game.World.GetTile(i + 1, j);  // Правый сосед

            int x = (int)(Math.Floor(i - (Position.X / CHUNK_SIZE)));
            int y = (int)(Math.Floor(j - (Position.Y / CHUNK_SIZE)));
            if (x >= 0 && y >= 0 && x < CHUNK_SIZE && y < CHUNK_SIZE)
            {
                int index = x + y * CHUNK_SIZE;

                if (type != TileType.None)
                {
                    tile = new Tile(type, upTile, downTile, leftTile, rightTile);
                    tile.Position = new Vector2f(x * Tile.TILE_SIZE, y * Tile.TILE_SIZE);
                    tile.IdTile = IdTile;
                    tiles[index] = tile;

                    tile.v[0].Position = new Vector2f(x * 16, y * 16);
                    tile.v[1].Position = new Vector2f(x * 16, y * 16 + 16);
                    tile.v[2].Position = new Vector2f(x * 16 + 16, y * 16);
                    tile.v[3].Position = new Vector2f(x * 16, y * 16 + 16);
                    tile.v[4].Position = new Vector2f(x * 16 + 16, y * 16);
                    tile.v[5].Position = new Vector2f(x * 16 + 16, y * 16 + 16);

                    if (IdTile <= tileMesh.Length - 6)
                    {
                        tileMesh[IdTile + 0] = tile.v[0];
                        tileMesh[IdTile + 1] = tile.v[1];
                        tileMesh[IdTile + 2] = tile.v[2];
                        tileMesh[IdTile + 3] = tile.v[3];
                        tileMesh[IdTile + 4] = tile.v[4];
                        tileMesh[IdTile + 5] = tile.v[5];

                        IdTile += 6;
                    }
                }
                else
                {
                    tile = tiles[index];
                    IdTile = tile.IdTile;
                    tiles[index] = null;
                    if (IdTile <= tileMesh.Length - 6)
                    {
                        tileMesh[IdTile + 0] = new Vertex();
                        tileMesh[IdTile + 1] = new Vertex();
                        tileMesh[IdTile + 2] = new Vertex();
                        tileMesh[IdTile + 3] = new Vertex();
                        tileMesh[IdTile + 4] = new Vertex();
                        tileMesh[IdTile + 5] = new Vertex();
                    }

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
            float x = (int)(Math.Floor(i - (Position.X / CHUNK_SIZE))) >= 0 ? (int)(Math.Floor(i - (Position.X / CHUNK_SIZE))) : i;
            float y = (int)(Math.Floor(j - (Position.Y / CHUNK_SIZE))) >= 0 ? (int)(Math.Floor(j - (Position.Y / CHUNK_SIZE))) : j;

            if (x >= 0 && y >= 0 && x < CHUNK_SIZE && y < CHUNK_SIZE)
                return tiles[(int)x + (int)y * CHUNK_SIZE];

            return null;
        }

        Vertex[] v = new Vertex[6];

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            states.Texture = Content.ssTileBoard.Texture;

            for (int y = 0; y < 16; y++)
            {
                for (int x = 0; x < 16; x++)
                {
                    tile = tiles[x + y * CHUNK_SIZE];
                    if (tile != null)
                    {
                        v[0] = tile.v[0];
                        v[1] = tile.v[1];
                        v[2] = tile.v[2];
                        v[3] = tile.v[3];
                        v[4] = tile.v[4];
                        v[5] = tile.v[5];

                        states.Texture = tile.SpriteSheet.Texture;
                        target.Draw(v, PrimitiveType.Triangles, states);
                    }
                }
            }

            //target.Draw(tileMesh, PrimitiveType.Triangles, states);
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE) * CHUNK_SIZE);
        }
    }
}
