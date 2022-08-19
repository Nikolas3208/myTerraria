using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;


namespace MyTerraria.NPC
{
    class Player : Npc
    {
        public string block_Type = "GROUND";

        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Color HairColor = new Color(0, 240, 10);  // Цвет волос
        public Color BodyColor = new Color(255, 229, 186);  // Цвет кожи
        public Color ShirtColor = new Color(255, 255, 0);  // Цвет куртки
        public Color LegsColor = new Color(0, 76, 135);  // Цвет штанов

        // UI
        public UIInvertory Invertory;

        Thread thread;
        Thread thread2;

        // Спрайты с анимацией
        AnimSprite asHair;         // Волосы
        AnimSprite asHead;         // Голова
        AnimSprite asShirt;        // Рубашка
        AnimSprite asUndershirt;   // Рукава
        AnimSprite asHands;        // Кисти
        AnimSprite asLegs;         // Ноги
        AnimSprite asShoes;        // Обувь

        public Player(World world) : base(world)
        {
            //Window = new RenderWindow(new VideoMode(800, 600), "Моя Terraria!");
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 1.5f, Tile.TILE_SIZE * 2.8f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 3);

            isRectVisible = false;

            // Волосы
            asHair = new AnimSprite(Content.ssPlayerHair);
            asHair.Position = new Vector2f(0, 19);
            asHair.Color = HairColor;
            asHair.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHair.AddAnimation("run", new Animation(
                new AnimationFrame(0, 0, 0.1f),
                new AnimationFrame(0, 1, 0.1f),
                new AnimationFrame(0, 2, 0.1f),
                new AnimationFrame(0, 3, 0.1f),
                new AnimationFrame(0, 4, 0.1f),
                new AnimationFrame(0, 5, 0.1f),
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f)
            ));

            // Голова
            asHead = new AnimSprite(Content.ssPlayerHead);
            asHead.Position = new Vector2f(0, 19);
            asHead.Color = BodyColor;
            asHead.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHead.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Рубашка
            asShirt = new AnimSprite(Content.ssPlayerShirt);
            asShirt.Position = new Vector2f(0, 19);
            asShirt.Color = ShirtColor;
            asShirt.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asShirt.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Рукава
            asUndershirt = new AnimSprite(Content.ssPlayerUndershirt);
            asUndershirt.Position = new Vector2f(0, 19);
            asUndershirt.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 1f)
            ));
            asUndershirt.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Кисти
            asHands = new AnimSprite(Content.ssPlayerHands);
            asHands.Position = new Vector2f(0, 19);
            asHands.Color = BodyColor;
            asHands.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHands.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Ноги
            asLegs = new AnimSprite(Content.ssPlayerLegs);
            asLegs.Color = LegsColor;
            asLegs.Position = new Vector2f(0, 19);
            asLegs.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asLegs.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));

            // Обувь
            asShoes = new AnimSprite(Content.ssPlayerShoes);
            asShoes.Position = new Vector2f(0, 19);
            asShoes.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 1f)
            ));
            asShoes.AddAnimation("run", new Animation(
                new AnimationFrame(0, 6, 0.1f),
                new AnimationFrame(0, 7, 0.1f),
                new AnimationFrame(0, 8, 0.1f),
                new AnimationFrame(0, 9, 0.1f),
                new AnimationFrame(0, 10, 0.1f),
                new AnimationFrame(0, 11, 0.1f),
                new AnimationFrame(0, 12, 0.1f),
                new AnimationFrame(0, 13, 0.1f),
                new AnimationFrame(0, 14, 0.1f),
                new AnimationFrame(0, 15, 0.1f),
                new AnimationFrame(0, 16, 0.1f),
                new AnimationFrame(0, 17, 0.1f),
                new AnimationFrame(0, 18, 0.1f),
                new AnimationFrame(0, 19, 0.1f)
            ));
        }

        public override void OnKill()
        {
            Spawn();
        }


        TileType type = TileType.GROUND;
        public Vector2i mousePos;

        public float a;
        public float i = 0;
        public int i4 = 0;

        public override void UpdateNPC()
        {
            thread = new Thread(Program.Game.Trees.Update);
            thread.Name = "TREES";

            updateMovement();


            mousePos = Mouse.GetPosition(Program.Window);
            DebugRender.AddRectangle(mousePos.X + (Position.X - Program.Window.Size.X / 2), mousePos.Y + (Position.Y - Program.Window.Size.Y / 2), Tile.TILE_SIZE, Tile.TILE_SIZE, Color.Green);

            Vector2f mousePosGlobal = new Vector2f(mousePos.X + (Position.X - Program.Window.Size.X / 2), mousePos.Y + (Position.Y - Program.Window.Size.Y / 2));
            a = MathHelper.GetDistance(Position, mousePosGlobal);

            if (UIManager.Over == null && UIManager.Drag == null && a < 150)
            {
                Tile tile1 = world.GetTileByWorldPos(mousePos.X + (Position.X - Program.Window.Size.X), mousePos.Y + (Position.Y - Program.Window.Size.Y) - 1000 * 16);
                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    setBlock();
                }
                if (tile1 == null)
                {
                    int i = (int)(mousePos.X + (Position.X - Program.Window.Size.X / 2)) / Tile.TILE_SIZE;
                    int j = (int)(mousePos.Y + (Position.Y - Program.Window.Size.Y / 2)) / Tile.TILE_SIZE;

                    Tile upTile = Program.Game.World.GetTile(i, j - 1);     // Верхний сосед
                    Tile downTile = Program.Game.World.GetTile(i, j + 1);   // Нижний сосед
                    Tile leftTile = Program.Game.World.GetTile(i - 1, j);   // Левый сосед
                    Tile rightTile = Program.Game.World.GetTile(i + 1, j);  // Правый сосед

                    for (int i2 = 0; i2 < 10; i2++)
                    {
                        if (UIInvertory.cells[i2].IsSelected)
                            i4 = i2;

                    }
                    //if (&& (upTile != null || downTile != null || leftTile != null || rightTile != null) && Program.Game.World.GetTileType(i + 1, j) != "TREETOPS" && Program.Game.World.GetTileType(i, j) != "TREEBRAK" && Program.Game.World.GetTileType(i - 1, j) != "TREETOPS" && Program.Game.World.GetTileType(i, j + 1) != "TREETOPS" && Program.Game.World.GetTileType(i, j - 1) != "TREETOPS" && Program.Game.World.GetTileType(i, j + 1) != "TREEBRAK" && Program.Game.World.GetTileType(i, j - 1) != "TREEBRAK" && Program.Game.World.GetTileType(i + 1, j) != "TREEBRAK" && Program.Game.World.GetTileType(i - 1, j) != "TREEBRAK" && Program.Game.World.GetTileType(i, j) == "NOME")
                    if ((Program.Game.World.GetTile(i, j) == null || (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type == TileType.NONE)) && UIInvertory.cells != null && UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.itemCount != 0 && Mouse.IsButtonPressed(Mouse.Button.Right) && ((upTile != null && upTile.type != TileType.NONE) || (downTile != null && downTile.type != TileType.NONE) || (leftTile != null && leftTile.type != TileType.NONE) || (rightTile != null && rightTile.type != TileType.NONE)) && i != Position.X && j != Position.Y)
                    {
                        if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemGround && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                        {
                            //if (UIInvertory.cells[i4].ItemStack != null && !UIInvertory.cells[i4].ItemStack.IsFull)
                                //if (UIInvertory.cells[i4].ItemStack.ItemCount != 0)
                                    UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                            world.SetTile(TileType.GROUND, i, j);
                            //World.ItemTile.a = -1;
                        }
                        else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemGrass && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                        {
                            /*if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == Invertory.UIItemStack.InfoItem && !UIInvertory.cells[i4].ItemStack.IsFull)
                                if (UIInvertory.cells[i4].ItemStack.ItemCount != 0)*/
                                    UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                            world.SetTile(TileType.GRASS, i, j);
                            //World.ItemTile.a = -1;
                        }
                        else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemStone && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                        {
                            /*if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == Invertory.UIItemStack.InfoItem && !UIInvertory.cells[i4].ItemStack.IsFull)
                                if (UIInvertory.cells[i4].ItemStack.ItemCount != 0)*/
                                    UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                            world.SetTile(TileType.STONE, i, j);
                            //World.ItemTile.a = -1;
                        }
                        else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemTreeBrak && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                        {
                            /*if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == Invertory.UIItemStack.InfoItem && !UIInvertory.cells[i4].ItemStack.IsFull)
                                if (UIInvertory.cells[i4].ItemStack.ItemCount != 0)*/
                                    UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                            world.SetTile(TileType.DESK, i, j);
                            //World.ItemTile.a = -1;
                        }
                        else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemIronOre && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                        {
                            /*if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == Invertory.UIItemStack.InfoItem && !UIInvertory.cells[i4].ItemStack.IsFull)
                                if (UIInvertory.cells[i4].ItemStack.ItemCount != 0)*/
                                    UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                            world.SetTile(TileType.IRONORE, i, j);
                            //World.ItemTile.a = -1;
                        }
                    }
                    if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.ItemCount == 0)
                    {
                        UIInvertory.cells[i4].ItemStack = null;
                        Invertory.UIItemStack.ClearUIInvertory();
                    }
                }
            }
        }

        public void setBlock()
        {
            int i = (int)(mousePos.X + (Position.X - Program.Window.Size.X / 2)) / Tile.TILE_SIZE;
            int j = (int)(mousePos.Y + (Position.Y - Program.Window.Size.Y / 2)) / Tile.TILE_SIZE;
            if (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type != TileType.NONE)
            {
                if (Program.Game.World.GetTile(i, j).type == TileType.GROUND)
                {
                    world.DelTile(TileType.GROUND, i, j);
                    //if (Program.Game.World.GetTile(i, j).type == TileType.TREEBRAK)
                    //    thread.Start();
                }
                else if (Program.Game.World.GetTile(i, j).type == TileType.GRASS)
                {
                    if (Program.Game.World.tiles[i, j - 1] != null)
                    {
                        if (Program.Game.World.tiles[i, j - 1].type != TileType.TREEBRAK)
                            world.DelTile(TileType.GRASS, i, j);
                    }
                    else
                        world.DelTile(TileType.GRASS, i, j);
                    //if (Program.Game.World.tiles[i, j - 1].type == TileType.TREEBRAK)
                    //    thread.Start();
                }
                else if (Program.Game.World.GetTile(i, j).type == TileType.STONE)
                {
                    world.DelTile(TileType.STONE, i, j);
                }
                else if (Program.Game.World.GetTile(i, j).type == TileType.TREEBRAK)
                {
                    world.DelTile(TileType.TREEBRAK, i, j);
                    thread.Start();

                }
                else if (Program.Game.World.GetTile(i, j).type == TileType.DESK)
                {
                    world.DelTile(TileType.DESK, i, j);
                }
                else if (Program.Game.World.GetTile(i, j).type == TileType.IRONORE)
                {
                    world.DelTile(TileType.IRONORE, i, j);
                }
            }
        }

        bool isJump;
        private void updateMovement()
        {
            Invertory.A();

            Content.ssBackgroundSky.Position = Position;
            Content.ssBackgroundSky.Origin = new Vector2f(Content.ssTextureBackgroundSky.Size.X / 2, Content.ssTextureBackgroundSky.Size.Y / 2);
            Content.ssBackgroundSky.Scale = new Vector2f(120, 1);

            Content.ssBackgroundMountains.Position = new Vector2f(Position.X, Position.Y - Program.Window.Size.Y / 2);
            Content.ssBackgroundMountains.Origin = new Vector2f(Content.ssTextureBackgroundMountains.Size.X / 2, -Content.ssTextureBackgroundMountains.Size.Y + 10 * 16);
            Content.ssBackgroundMountains.Scale = new Vector2f(2.5f, 1.6f);


            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isInventory = Keyboard.IsKeyPressed(Keyboard.Key.Escape);

            if(!isFly)
                isJump = Keyboard.IsKeyPressed(Keyboard.Key.Space);
            
            bool isMove = isMoveLeft || isMoveRight;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                i = 0;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                i = 1;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                i = 2;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                i = 3;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                i = 4;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num6))
                i = 5;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                i = 6;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num8))
                i = 7;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num9))
                i = 8;

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num0))
                i = 9;

            UIInvertory.cells[(int)i].IsSelected = true;
            // Прыжок
            if (isJump && !isFly && velocity.Y >= 0)
            {
                velocity.Y += -10f;
            }

            if (isMove)
            {
                if (isMoveLeft)
                {
                    if (movement.X > 0)
                        movement.X = 0;
                    
                    movement.X -= PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = -1;
                }
                else if (isMoveRight)
                {
                    if (movement.X < 0)
                        movement.X = 0;

                    movement.X += PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = 1;
                }

                if (movement.X > PLAYER_MOVE_SPEED)
                    movement.X = PLAYER_MOVE_SPEED;
                else if (movement.X < -PLAYER_MOVE_SPEED)
                    movement.X = -PLAYER_MOVE_SPEED;

                // Анимация
                asHair.Play("run");
                asHead.Play("run");
                asShirt.Play("run");
                asUndershirt.Play("run");
                asHands.Play("run");
                asLegs.Play("run");
                asShoes.Play("run");
            }
            else
            {
                movement = new Vector2f();

                // Анимация
                asHair.Play("idle");
                asHead.Play("idle");
                asShirt.Play("idle");
                asUndershirt.Play("idle");
                asHands.Play("idle");
                asLegs.Play("idle");
                asShoes.Play("idle");
            }
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            //target.Draw(rectDirection, states);

            target.Draw(asHead, states);
            target.Draw(asHair, states);
            target.Draw(asShirt, states);
            target.Draw(asUndershirt, states);
            target.Draw(asHands, states);
            target.Draw(asLegs, states);
            target.Draw(asShoes, states);
        }

        public override void OnWallCollided()
        {
            
        }
    }
}
