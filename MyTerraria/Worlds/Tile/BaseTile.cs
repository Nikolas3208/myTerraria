using SFML.Graphics;
using SFML.System;
using System;

namespace MyTerraria.Worlds.Tile
{
    public class BaseTile : Transformable
    {
        public const int TILE_SIZE = 16;
        public TileType type { get; set; }

        public Vertex[] MeshTile { get; set; }

        public SpriteSheet SpriteSheet { get; set; }
        public Texture Texture { get; set; }

        public Vector2f SpriteNumber;

        public int tileHealth { get; set; }
        public int Id { get; set; }

        public BaseTile()
        {
            MeshTile = new Vertex[6];
        }

        public virtual void BreakTile() { }

        public void SetSpriteShit()
        {
            switch (type)
            {
                case TileType.None:
                    break;
                case TileType.Ground:
                    SpriteSheet = Content.ssTileGround;    // Почва
                    break;
                case TileType.GroundWall:
                    SpriteSheet = Content.ssTileItemGround;
                    break;
                case TileType.Sand:
                    SpriteSheet = Content.ssTileSand;    // Песок
                    break;
                case TileType.Stone:
                    SpriteSheet = Content.ssTileStone;     //Камень
                    break;
                case TileType.StoneWall:
                    SpriteSheet = Content.ssWallStone;
                    break;
                case TileType.Treebark:
                    SpriteSheet = Content.ssTileTreeBark;
                    break;
                case TileType.Ironore:
                    SpriteSheet = Content.ssTileIronOre;    //Железная руда
                    break;
                case TileType.Coperore:
                    SpriteSheet = Content.ssTileCoperOre;    //Железная руда
                    break;
                case TileType.Goldore:
                    SpriteSheet = Content.ssTileGoldOre;    //Железная руда
                    break;
                case TileType.Silverore:
                    SpriteSheet = Content.ssTileSilverOre;    //Железная руда
                    break;
                case TileType.Vegetation:
                    SpriteSheet = Content.ssTileVegetation; //Растительность
                    break;
                case TileType.Mushroom:
                    SpriteSheet = Content.ssTileVegetation;
                    break;
                case TileType.Treesapling:
                    SpriteSheet = Content.ssTileSaplingTree;
                    break;
                case TileType.Torch:
                    SpriteSheet = Content.ssTileTorch;
                    break;
                case TileType.Board:
                    SpriteSheet = Content.ssTileBoard;      //Доска
                    break;
                case TileType.BoardWall:
                    SpriteSheet = Content.ssWallBoard;
                    break;
            }
        }

        public void UpdateView()
        {
            switch (type)
            {
                default:
                    SpriteNumber = new Vector2f(0, 0);
                    break;
            }
            {
                int x = (int)(SpriteNumber.X * 16 + SpriteNumber.X * 2);
                int y = (int)(SpriteNumber.Y * 16 + SpriteNumber.Y * 2);

                v[0].TexCoords = new Vector2f(x, y);
                v[1].TexCoords = new Vector2f(x, 16 + y);
                v[2].TexCoords = new Vector2f(16 + x, y);
                v[3].TexCoords = new Vector2f(x, 16 + y);
                v[4].TexCoords = new Vector2f(16 + x, y);
                v[5].TexCoords = new Vector2f(16 + x, 16 + y);
            }
        }
    }
}
