using SFML.Graphics;
using SFML.System;

namespace MyTerraria
{
    // Перечисление типов плитки
    enum TileType
    {
        NONE,       // Пусто
        GROUND,     // Почва
        GRASS,       // Земляной блок с травой
        STONE,
        TREEBRAK,
        TREETOPS,
        DESK,
        IRONORE
    }

    // Класс плитки
    class Tile : Transformable, Drawable
    {
        // Размер тайла по ширине и высоте
        public const int TILE_SIZE = 16;

        public SpriteSheet SpriteSheet { get; set; }    // Набор спрайтов плитки

        TileType type = TileType.GROUND;    // Тип плитки
        RectangleShape rectShape;           // Прямоугольная форма плитки

        // Соседи
        Tile upTile = null;     // Верхний сосед
        Tile downTile = null;   // Нижний сосед
        Tile leftTile = null;   // Левый сосед
        Tile rightTile = null;  // Правый сосед

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
        public Tile(TileType type, Tile upTile, Tile downTile, Tile leftTile, Tile rightTile)
        {
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

            rectShape = new RectangleShape(new Vector2f(TILE_SIZE, TILE_SIZE));

            switch (type)
            {
                case TileType.GROUND:
                    SpriteSheet = Content.ssTileGround;    // Почва
                    break;
                case TileType.GRASS:
                    SpriteSheet = Content.ssTileGrass;    // Земляной блок с травой
                    break;
                case TileType.STONE:
                    SpriteSheet = Content.ssTileSone;
                    break;
                case TileType.TREEBRAK:
                    SpriteSheet = Content.ssTileTreeBark;
                    break;
                case TileType.TREETOPS:
                    SpriteSheet = Content.ssTileTreeTops;
                    break;
                case TileType.DESK:
                    SpriteSheet = Content.ssTileDesk1;
                    break;
                case TileType.IRONORE:
                    SpriteSheet = Content.ssTileIronOre;
                    break;
            }

            rectShape.Texture = SpriteSheet.Texture;

            // Обновляем внешний вид плитки в зависимости от соседей
            UpdateView();
        }

        // Обновляем внешний вид плитки в зависимости от соседей
        public void UpdateView()
        {
            switch (type)
            {
                case TileType.NONE:
                    break;
                case TileType.GROUND:
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
                    else if (upTile == null && downTile != null && leftTile != null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 2); // Случайное число от 0 до 2
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(1 + i, 0);
                    }
                    break;
                case TileType.GRASS:
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
                    break;
                case TileType.STONE:
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
                    break;
                case TileType.TREEBRAK:
                    rectShape.TextureRect = SpriteSheet.GetTextureRect(0, 0);
                    break;
                case TileType.DESK:
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
                    // Если есть только левый сосед
                    else if (upTile == null && downTile != null && leftTile != null && rightTile == null)
                    {
                        int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(1, 6 + i);
                    }
                    // Если есть только правый сосед
                    else if (upTile == null && downTile != null && leftTile == null && rightTile != null)
                    {
                        int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                        rectShape.TextureRect = SpriteSheet.GetTextureRect(2, 6 + i);
                    }
                    break;
                case TileType.IRONORE:
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
                    break;
                case TileType.TREETOPS:
                    //int i = World.Rand.Next(0, 3); // Случайное число от 0 до 2
                    rectShape.TextureRect = SpriteSheet.GetTextureRect(0, 0);
                    rectShape.Scale = new Vector2f(4.5f,5);
                    rectShape.Origin = new Vector2f(5.8f, 12.5f);
                    break;
            }
        }

        // Рисуем плитку
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            target.Draw(rectShape, states);
        }

        public FloatRect GetFloatRect()
        {
            return new FloatRect(Position, new Vector2f(TILE_SIZE, TILE_SIZE));
        }
    }
}
