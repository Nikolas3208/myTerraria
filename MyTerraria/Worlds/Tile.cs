using SFML.Graphics;
using SFML.System;
using System;

namespace MyTerraria.Worlds
{
    public enum TileType
    {
        None,               // Пусто
        Ground,             // Почва
        GroundWall,         // Почва
        Sand,               // Почва
        Stone,              //Камень
        StoneWall,          //Камень
        Treebark,           //Кора дерева
        Board,              //Дска
        BoardWall,          //Дска
        Ironore,            //Железная руда
        Coperore,           //Медная руда
        Goldore,            //Золотая руда
        Silverore,          //Серебряная руда
        Vegetation,         //Растительность   
        Mushroom,           //Гриб
        Treesapling,        //Саженец дерева
        Torch               //Факел
    }
    public class Tile : Transformable, Drawable    
    {
        public const int TILE_SIZE = 16;
        public TileType type { get; set; }
        public bool isGrass { get; set; }
        public Color Color { get; set; } = Color.White;
        public SpriteSheet SpriteSheet { get; set; }
        public VertexArray VertexArray { get; set; }

        // Соседи
        public Tile upTile = null;     // Верхний сосед
        public Tile downTile = null;   // Нижний сосед
        public Tile leftTile = null;   // Левый сосед
        public Tile rightTile = null;  // Правый сосед

        public Tile UpTile
        {
            set
            {
                upTile = value;

                if (SpriteSheet != null) UpdateView();   // Обновляем вид плитки
            }
            get
            {
                return upTile;
            }
        }

        // Нижний сосед
        public Tile DownTile
        {
            set
            {
                downTile = value;
                if (SpriteSheet != null) UpdateView();   // Обновляем вид плитки
            }
            get
            {
                return downTile;
            }
        }

        // Левый сосед
        public Tile LeftTile
        {
            set
            {
                leftTile = value;
                if (SpriteSheet != null) UpdateView();   // Обновляем вид плитки
            }
            get
            {
                return leftTile;
            }
        }

        // Правый сосед
        public Tile RightTile
        {
            set
            {
                rightTile = value;
                if (SpriteSheet != null) UpdateView();   // Обновляем вид плитки
            }
            get
            {
                return rightTile;
            }
        }

        Texture texture;

        Vertex[] v = new Vertex[6];
        Vector2u TexturePosFraq = new Vector2u();

        public Tile(TileType type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {
            isGrass = false;

            this.type = type;

            // Присваиваем соседей, а соседям эту плитку
            if (upTile != null)
            {
                this.upTile = upTile;
                this.upTile.DownTile = this;    // Для верхнего соседа эта плитка будет нижним соседом
            }
            if (downTile != null)
            {
                this.downTile = downTile;
                this.downTile.UpTile = this;    // Для нижнего соседа эта плитка будет верхним соседом
            }
            if (leftTile != null)
            {
                this.leftTile = leftTile;
                this.leftTile.RightTile = this;    // Для левого соседа эта плитка будет правым соседом
            }
            if (rightTile != null)
            {
                this.rightTile = rightTile;
                this.rightTile.LeftTile = this;    // Для правого соседа эта плитка будет левым соседом
            }

            SetSpriteSheet(type);

            if (type != TileType.None)
                texture = SpriteSheet.Texture;

            v[0] = new Vertex(new Vector2f(0, 0));
            v[1] = new Vertex(new Vector2f(0, 16));
            v[2] = new Vertex(new Vector2f(16, 0));
            v[3] = new Vertex(new Vector2f(0, 16));
            v[4] = new Vertex(new Vector2f(16, 0));
            v[5] = new Vertex(new Vector2f(16, 16));

            v[0].Color = Color;
            v[1].Color = Color;
            v[2].Color = Color;
            v[3].Color = Color;
            v[4].Color = Color;
            v[5].Color = Color;

            v[0].TexCoords = new Vector2f(0 + TexturePosFraq.X, 0 + TexturePosFraq.Y);
            v[1].TexCoords = new Vector2f(0 + TexturePosFraq.X, 16 + TexturePosFraq.Y);
            v[2].TexCoords = new Vector2f(16 + TexturePosFraq.X, 0 + TexturePosFraq.Y);
            v[3].TexCoords = new Vector2f(0 + TexturePosFraq.X, 16 + TexturePosFraq.Y);
            v[4].TexCoords = new Vector2f(16 + TexturePosFraq.X, 0 + TexturePosFraq.Y);
            v[5].TexCoords = new Vector2f(16 + TexturePosFraq.X, 16 + TexturePosFraq.Y);

            UpdateView();
        }

        private void SetSpriteSheet(TileType type)
        {
            switch(type)
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
                    // Если у плитки есть все соседи
                    if (upTile != null && downTile != null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                        TexturePosFraq = new Vector2u(1 + (uint)i, 1);
                    }
                    // Если у плитки отсутствуют все соседи
                    else if (upTile == null && downTile == null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(9 + (uint)i, 3);
                    }

                    //---------------

                    // Если у плитки отсутствует только верхний сосед
                    else if (upTile == null && downTile != null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(1 + (uint)i, 0);
                    }
                    // Если у плитки отсутствует только нижний сосед
                    else if (upTile != null && downTile == null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(1 + (uint)i, 2);
                    }
                    // Если у плитки отсутствует только левый сосед
                    else if (upTile != null && downTile != null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(0, (uint)i);
                    }
                    // Если у плитки отсутствует только правый сосед
                    else if (upTile != null && downTile != null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(4, (uint)i);
                    }

                    //---------------

                    // Если у плитки отсутствует только верхний и левый сосед
                    else if (upTile == null && downTile != null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u((uint)i * 2, 3);
                    }
                    // Если у плитки отсутствует только верхний и правый сосед
                    else if (upTile == null && downTile != null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(1 + (uint)i * 2, 3);
                    }
                    // Если у плитки отсутствует только нижний и левый сосед
                    else if (upTile != null && downTile == null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(0 + (uint)i * 2, 4);
                    }
                    // Если у плитки отсутствует только нижний и правый сосед
                    else if (upTile != null && downTile == null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(1 + (uint)i * 2, 4);
                    }
                    //Если есть только правая и леввая плитка
                    else if (upTile == null && downTile == null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(6 + (uint)i, 4);
                    }
                    //Если есть только верхняя и нижняя плитка
                    else if (upTile != null && downTile != null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(5, 0 + (uint)i);
                    }

                    //----------------------------------------------

                    //Если есть только верхний сосед
                    else if (upTile != null && downTile == null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(6, 3);
                    }
                    // Если есть только нижний сосед
                    else if (upTile == null && downTile != null && leftTile == null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(6, 0);
                    }
                    // Если есть только левый сосед
                    else if (upTile == null && downTile == null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3);
                        TexturePosFraq = new Vector2u(12, 0 + (uint)i);
                    }
                    // Если есть только правый сосед
                    else if (upTile == null && downTile == null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                        TexturePosFraq = new Vector2u(9, 0 + (uint)i);
                    }
                    // Если есть только правый сосед
                    else if (upTile == null && downTile != null && leftTile != null && leftTile.type == TileType.Treebark && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 2); // Случайное число от 0 до 2
                        TexturePosFraq = new Vector2u(1 + (uint)i, 0);
                    }
                    break;
            }
            //if (tile != Tiles.Wall)
            {
                int x = (int)(TexturePosFraq.X * 16 + TexturePosFraq.X * 2);
                int y = (int)(TexturePosFraq.Y * 16 + TexturePosFraq.Y * 2);

                v[0].TexCoords = new Vector2f(x, y);
                v[1].TexCoords = new Vector2f(x, 16 + y);
                v[2].TexCoords = new Vector2f(16 + x, y);
                v[3].TexCoords = new Vector2f(x, 16 + y);
                v[4].TexCoords = new Vector2f(16 + x, y);
                v[5].TexCoords = new Vector2f(16 + x, 16 + y);
            }
            /*else if(tile == Tiles.Wall)
            {
                int x = (int)(TexturePosFraq.X * 32 + TexturePosFraq.X * 4);
                int y = (int)(TexturePosFraq.Y * 32 + TexturePosFraq.Y * 4);

                v[0] = new Vertex(new Vector2f(0, 0));
                v[1] = new Vertex(new Vector2f(0, 32));
                v[2] = new Vertex(new Vector2f(32, 0));
                v[3] = new Vertex(new Vector2f(0, 32));
                v[4] = new Vertex(new Vector2f(32, 0));
                v[5] = new Vertex(new Vector2f(32, 32));

                v[0].TexCoords = new Vector2f(x, y);
                v[1].TexCoords = new Vector2f(x, 32 + y);
                v[2].TexCoords = new Vector2f(32 + x, y);
                v[3].TexCoords = new Vector2f(x, 32 + y);
                v[4].TexCoords = new Vector2f(32 + x, y);
                v[5].TexCoords = new Vector2f(32 + x, 32 + y);
            }*/
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            if (type != TileType.None)
                states.Texture = texture;

            target.Draw(v, PrimitiveType.Triangles, states);
        }

        public void UpdateColor(Color color)
        {
            v[0].Color = color;
            v[1].Color = color;
            v[2].Color = color;
            v[3].Color = color;
            v[4].Color = color;
            v[5].Color = color;
        }

        public void Grass(bool v)
        {
            if (v)
            {
                SpriteSheet = Content.ssTileGrass;
                texture = SpriteSheet.Texture;
            }
            else
            {
                SetSpriteSheet(type);
                texture = SpriteSheet.Texture;
            }

            isGrass = v;
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(TILE_SIZE, TILE_SIZE));
        }
    }
}
