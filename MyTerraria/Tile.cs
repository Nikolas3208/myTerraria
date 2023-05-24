using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Lifetime;
using System.Threading.Tasks;
using System.Windows;

namespace MyTerraria
{
    // Перечисление типов плитки
    public enum TileType
    {
        None,               // Пусто
        Ground,             // Почва
        GroundWall,             // Почва
        Sand,               // Почва
        Grass,              // Земляной блок с травой
        Stone,              //Камень
        StoneWall,              //Камень
        Treebark,           //Кора дерева
        Treetops,           //Верхушка дерева
        Board,              //Дска
        BoardWall,              //Дска
        Ironore,            //Железная руда
        Coperore,           //Медная руда
        Goldore,            //Золотая руда
        Silverore,          //Серебряная руда
        Vegetation,         //Растительность   
        Mushroom,           //Гриб
        Treesapling,        //Саженец дерева
        Torch               //Факел
    }

    /*public enum WallType
    {
        NONE,       // Пусто
        GROUND,     // Почва
        STONE,      //Камень
        BOARD       //Дска
    }*/

    // Класс плитки
    public class Tile : Transformable, Drawable
    {

        // Размер тайла по ширине и высоте
        public const int TILE_SIZE = 16;
        public static Color Color { get; set; }
        public SpriteSheet SpriteSheet { get; set; }    // Набор спрайтов плитки

        public TileType type;   // Тип плитки

        public Sprite rectShape;    // Прямоугольная форма плитки

        public bool activityPhithics = true;
        public bool isWall;

        // Соседи
        public Tile upTile = null;     // Верхний сосед
        public Tile downTile = null;   // Нижний сосед
        public Tile leftTile = null;   // Левый сосед
        public Tile rightTile = null;  // Правый сосед

        // Верхний сосед
        public Tile UpTile
        {
            set
            {
                upTile = value;
                UpdateView();   // Обновляем вид плитки
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
                UpdateView();   // Обновляем вид плитки
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
                UpdateView();   // Обновляем вид плитки
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
                UpdateView();   // Обновляем вид плитки
            }
            get
            {
                return rightTile;
            }
        }

        // Конструктор класса
        public Tile(TileType type, Color color, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile, bool isWall)
        {
            this.type = type;
            this.isWall = isWall;

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

            Color = color;

            try
            {
                switch (type)
                {
                    case TileType.None:
                        activityPhithics = false;
                        break;
                    case TileType.Ground:
                        SpriteSheet = Content.ssTileGround;    // Почва
                        if (isWall)
                       {
                            SpriteSheet = Content.ssWallGround;
                            activityPhithics = false;
                        } 
                        break;
                    case TileType.GroundWall:
                        SpriteSheet = Content.ssWallGround;    // Почва
                        activityPhithics = false;
                        break;
                    case TileType.Sand:
                        SpriteSheet = Content.ssTileSand;    // Песок
                        break;
                    case TileType.Grass:
                        SpriteSheet = Content.ssTileGrass;    // Земляной блок с травой
                        break;
                    case TileType.Stone:
                        SpriteSheet = Content.ssTileStone;     //Камень
                        break;
                    case TileType.StoneWall:
                        SpriteSheet = Content.ssWallStone;     //Камень
                        activityPhithics = false;
                        break;
                    case TileType.Treebark:
                        SpriteSheet = Content.ssTileTreeBark;   //Кора дерева
                        activityPhithics = false;
                        break;
                    case TileType.Treetops:
                        SpriteSheet = Content.ssTileTreeTops;   //Вершина дерева
                        activityPhithics = false;
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
                        activityPhithics = false;
                        break;
                    case TileType.Mushroom:
                        SpriteSheet = Content.ssTileVegetation;
                        activityPhithics = false;
                        break;
                    case TileType.Treesapling:
                        SpriteSheet = Content.ssTileSaplingTree;
                        activityPhithics = false;
                        break;
                    case TileType.Torch:
                        SpriteSheet = Content.ssTileTorch;      //Факел
                        activityPhithics = false;
                        break;
                    case TileType.Board:
                        SpriteSheet = Content.ssTileBoard;      //Доска
                        break;
                    case TileType.BoardWall:
                        SpriteSheet = Content.ssWallBoard;      //Доска
                        activityPhithics = false;
                        break;
                }

                // Обновляем внешний вид плитки в зависимости от соседей
                if (SpriteSheet != null)
                    rectShape = new Sprite(SpriteSheet.Texture);

                if (rectShape != null)
                    UpdateView();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // Обновляем внешний вид плитки в зависимости от соседей
        public void UpdateView()
        {
            if (rectShape != null)
            {
                switch (type)
                {
                     
                    default:
                        {
                            // Если у плитки есть все соседи
                            if (upTile != null && downTile != null && leftTile != null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i, 1);
                            }
                            // Если у плитки отсутствуют все соседи
                            else if (upTile == null && downTile == null && leftTile == null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(9 + i, 3);
                            }

                            //---------------

                            // Если у плитки отсутствует только верхний сосед
                            else if (upTile == null && downTile != null && leftTile != null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i, 0);
                            }
                            // Если у плитки отсутствует только нижний сосед
                            else if (upTile != null && downTile == null && leftTile != null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i, 2);
                            }
                            // Если у плитки отсутствует только левый сосед
                            else if (upTile != null && downTile != null && leftTile == null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(0, i);
                            }
                            // Если у плитки отсутствует только правый сосед
                            else if (upTile != null && downTile != null && leftTile != null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(4, i);
                            }

                            //---------------

                            // Если у плитки отсутствует только верхний и левый сосед
                            else if (upTile == null && downTile != null && leftTile == null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(0 + i * 2, 3);
                            }
                            // Если у плитки отсутствует только верхний и правый сосед
                            else if (upTile == null && downTile != null && leftTile != null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i * 2, 3);
                            }
                            // Если у плитки отсутствует только нижний и левый сосед
                            else if (upTile != null && downTile == null && leftTile == null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(0 + i * 2, 4);
                            }
                            // Если у плитки отсутствует только нижний и правый сосед
                            else if (upTile != null && downTile == null && leftTile != null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i * 2, 4);
                            }
                            //Если есть только правая и леввая плитка
                            else if (upTile == null && downTile == null && leftTile != null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(6 + i, 4);
                            }
                            //Если есть только верхняя и нижняя плитка
                            else if (upTile != null && downTile != null && leftTile == null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(5, 0 + i);
                            }

                            //----------------------------------------------

                            //Если есть только верхний сосед
                            else if (upTile != null && downTile == null && leftTile == null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(6, 3);
                            }
                            // Если есть только нижний сосед
                            else if (upTile == null && downTile != null && leftTile == null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(6, 0);
                            }
                            // Если есть только левый сосед
                            else if (upTile == null && downTile == null && leftTile != null && rightTile == null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(12, 0 + i);
                            }
                            // Если есть только правый сосед
                            else if (upTile == null && downTile == null && leftTile == null && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(9, 0 + i);
                            }
                            // Если есть только правый сосед
                            else if (upTile == null && downTile != null && leftTile != null && leftTile.type == TileType.Treebark && rightTile != null)
                            {
                                int i = World.Rand.Next(0, 2); // Случайное число от 0 до 2
                                rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i, 0);
                            }
                        }
                    break;
                    case TileType.GroundWall:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(7, 0);
                        break;
                    case TileType.StoneWall:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(7, 0);
                        break;
                    case TileType.BoardWall:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(7, 0);
                        break;
                    case TileType.Treesapling:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(0, 0);
                        rectShape.Origin = new Vector2f(0, rectShape.Texture.Size.Y / 2);
                        break;
                    case TileType.Treebark:
                        // Если есть верхний и нижний сосед
                        if (upTile != null && downTile != null && (leftTile == null || leftTile != null) && (rightTile == null || rightTile != null))
                        {
                            int i = World.Rand.Next(0, 4); // Случайное число от 0 до 3
                            rectShape.TextureRect = SpriteSheet.GetTextureRect(0, i);
                        }
                        // Если есть верхний и нижний сосед
                        if (upTile == null  && downTile != null && (leftTile == null || leftTile != null) && (rightTile == null || rightTile != null))
                        {
                            int i = World.Rand.Next(0, 3); // Случайное число от 0 до 3
                            rectShape.TextureRect = SpriteSheet.GetTextureRect(0, 9 + i);
                        }
                        if (upTile != null && (upTile.type != TileType.Treebark && upTile.type != TileType.Treetops) && downTile != null && (leftTile == null || leftTile != null) && (rightTile == null || rightTile != null))
                        {
                            int i = World.Rand.Next(0, 3); // Случайное число от 0 до 3
                            rectShape.TextureRect = SpriteSheet.GetTextureRect(0, 9 + i);
                        }
                        // Если есть верхний и нижний сосед
                        if (upTile == null && downTile != null && leftTile != null && (rightTile == null || rightTile != null))
                        {
                            int i = World.Rand.Next(0, 3); // Случайное число от 0 до 3
                            rectShape.TextureRect = SpriteSheet.GetTextureRect(1, 5 + i);
                        }
                        // Если есть верхний и нижний сосед
                        if (upTile == null && downTile != null && (leftTile == null || leftTile != null) && rightTile != null)
                        {
                            int i = World.Rand.Next(0, 3); // Случайное число от 0 до 3
                            rectShape.TextureRect = SpriteSheet.GetTextureRect(2, 5 + i);
                        }
                        break;
                    case TileType.Treetops:
                        //int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(World.Rand.Next(0, 3), 0);
                        //rectShape.Scale = new Vector2f(4.5f,5);
                        rectShape.Origin = new Vector2f(Content.ssTileTreeTops.SubWidth - 50, Content.ssTileTreeTops.SubHeight - 16);
                        break;
                    case TileType.Mushroom:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(8, 0);
                        break;
                    case TileType.Vegetation:
                        int i2 = World.Rand.Next(1, 9); // Случайное число от 0 до 2
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(i2, 0);
                        break;
                    case TileType.Board:
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(1, 0);
                        break;
                }
            }
        }

        // Рисуем плитку
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;


            if (rectShape != null)
            {
                rectShape.Color = Color;
                target.Draw(rectShape, states);
            }
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(TILE_SIZE, TILE_SIZE));
        }
    }
}
