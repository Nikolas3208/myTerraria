using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyTerraria.NPC
{
    class Player : Npc
    {
        public string block_Type = "GROUND";

        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public float Health = 100;

        public Color HairColor = new Color(0, 240, 10);  // Цвет волос
        public Color BodyColor = new Color(255, 229, 186);  // Цвет кожи
        public Color ShirtColor = new Color(255, 255, 0);  // Цвет куртки
        public Color LegsColor = new Color(0, 76, 135);  // Цвет штанов

        // UI
        public UIInvertory Invertory;

        Thread thread;

        // Спрайты с анимацией
        AnimSprite asHair;         // Волосы
        AnimSprite asHead;         // Голова
        AnimSprite asShirt;        // Рубашка
        AnimSprite asShirt2;        // Рубашка
        AnimSprite asUndershirt;   // Рукава
        AnimSprite asUndershirt2;   // Рукава
        AnimSprite asHands;        // Кисти
        AnimSprite asHands2;        // Кисти
        AnimSprite asLegs;         // Ноги
        AnimSprite asShoes;        // Обувь

        AnimSprite asPick;         // Кирка
        AnimSprite asAxe;         // Топор
        AnimSprite asSword;         // Мечь

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

            // Рубашка
            asShirt2 = new AnimSprite(Content.ssPlayerShirt);
            asShirt2.Position = new Vector2f(0, 19);
            asShirt2.Color = ShirtColor;
            asShirt2.AddAnimation("toolsIdle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asShirt2.AddAnimation("tools", new Animation(
                new AnimationFrame(0, 0, 0.2f),
                new AnimationFrame(0, 1, 0.2f),
                new AnimationFrame(0, 2, 0.2f),
                new AnimationFrame(0, 3, 0.2f),
                new AnimationFrame(0, 4, 0.2f)
            ));

            // Рукава
            asUndershirt2 = new AnimSprite(Content.ssPlayerUndershirt);
            asUndershirt2.Position = new Vector2f(0, 19);
            asUndershirt2.AddAnimation("toolsIdle", new Animation(
                new AnimationFrame(0, 0, 1f)
            ));
            asUndershirt2.AddAnimation("tools", new Animation(
                new AnimationFrame(0, 0, 0.2f),
                new AnimationFrame(0, 1, 0.2f),
                new AnimationFrame(0, 2, 0.2f),
                new AnimationFrame(0, 3, 0.2f),
                new AnimationFrame(0, 4, 0.2f)
            ));

            // Кисти
            asHands2 = new AnimSprite(Content.ssPlayerHands);
            asHands2.Position = new Vector2f(0, 19);
            asHands2.Color = BodyColor;
            asHands2.AddAnimation("toolsIdle", new Animation(
                new AnimationFrame(0, 0, 0.1f)
            ));
            asHands2.AddAnimation("tools", new Animation(
                new AnimationFrame(0, 0, 0.2f),
                new AnimationFrame(0, 1, 0.2f),
                new AnimationFrame(0, 2, 0.2f),
                new AnimationFrame(0, 3, 0.2f),
                new AnimationFrame(0, 4, 0.2f)
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

            asPick = new AnimSprite(Content.ssTileItemPick);
            asPick.Origin = new Vector2f(-16, 19);
            asPick.Position = new Vector2f(0, 19);
            asPick.AddAnimation("pickRun", new Animation(new AnimationFrame(0, 0, 1f)));

            asAxe = new AnimSprite(Content.ssTileItemAxe);
            asAxe.Origin = new Vector2f(-16, 19);
            asAxe.Position = new Vector2f(0, 19);
            asAxe.AddAnimation("AxeRun", new Animation(new AnimationFrame(0, 0, 1f)));

            asSword = new AnimSprite(Content.ssTileItemSword);
            asSword.Origin = new Vector2f(-16, 19);
            asSword.Position = new Vector2f(0, 19);
            asSword.AddAnimation("SwordRun", new Animation(new AnimationFrame(0, 0, 1f)));
        }

        public override void OnKill()
        {
            Spawn();
            AnimationUpdate("toolsIdle");
        }

        public Vector2i mousePos;

        public float DistanceToMouse;
        public float i = 0;
        public int i4 = 0;

        float waitTimer = 0f;

        int tools = 0;

        public override async void UpdateNPC()
        {
            thread = new Thread(Program.Game.Trees.Update);
            thread.Name = "TREES";

            int i;
            int j;

                i = (int)(mousePos.X + (Position.X - Program.Window.Size.X / 2)) / Tile.TILE_SIZE;
                j = (int)(mousePos.Y + (Position.Y - Program.Window.Size.Y / 2)) / Tile.TILE_SIZE;
            updateMovement();

            mousePos = Mouse.GetPosition(Program.Window);
            DebugRender.AddRectangle(mousePos.X + (Position.X - Program.Window.Size.X / 2), mousePos.Y + (Position.Y - Program.Window.Size.Y / 2), Tile.TILE_SIZE, Tile.TILE_SIZE, Color.Green);

            Vector2f mousePosGlobal = new Vector2f(mousePos.X + (Position.X - Program.Window.Size.X / 2), mousePos.Y + (Position.Y - Program.Window.Size.Y / 2));
            DistanceToMouse = MathHelper.GetDistance(Position, mousePosGlobal);
            if (UIManager.Over == null && UIManager.Drag == null && DistanceToMouse < 150)
            {
                

                Tile upTile = Program.Game.World.GetTile(i, j - 1);     // Верхний сосед
                Tile downTile = Program.Game.World.GetTile(i, j + 1);   // Нижний сосед
                Tile leftTile = Program.Game.World.GetTile(i - 1, j);   // Левый сосед
                Tile rightTile = Program.Game.World.GetTile(i + 1, j);  // Правый сосед

                for (int i2 = 0; i2 < 10; i2++)
                    if (UIInvertory.cells[i2].IsSelected)
                        i4 = i2;

                Tile tile1 = world.GetTileByWorldPos(mousePos.X + (Position.X - Program.Window.Size.X), mousePos.Y + (Position.Y - Program.Window.Size.Y) - 1000 * 16);

                if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    AnimationUpdate("tools");

                    if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemPick)
                    {
                        AnimationUpdate("pickRun");

                        await setNomeBlock(0, i, j);

                        tools = 1;
                    }
                    else if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemAxe)
                    {
                        AnimationUpdate("AxeRun");

                        await setNomeBlock(1, i, j);

                        tools = 2;
                    }
                    else if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemSword)
                    {
                        AnimationUpdate("SwordRun");

                        tools = 3;
                    }


                    if ((Program.Game.World.GetTile(i, j) == null || (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type == TileType.NONE)) && UIInvertory.cells != null && UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.itemCount != 0 && ((upTile != null && upTile.type != TileType.NONE) || (downTile != null && downTile.type != TileType.NONE) || (leftTile != null && leftTile.type != TileType.NONE) || (rightTile != null && rightTile.type != TileType.NONE)) && i != Position.X && j != Position.Y)
                    {
                        if (waitTimer >= 1)
                        {
                            if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemGround && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                            {
                                UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                                world.SetTile(TileType.GROUND, i, j);
                            }
                            else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemGrass && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                            {
                                UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                                world.SetTile(TileType.GRASS, i, j);
                            }
                            else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemStone && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                            {
                                UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                                world.SetTile(TileType.STONE, i, j);
                            }
                            else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemIronOre && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                            {
                                UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                                world.SetTile(TileType.IRONORE, i, j);
                            }
                            else if (UIInvertory.cells[i4].ItemStack.InfoItem == InfoItem.ItemBoard && UIInvertory.cells[i4].ItemStack.itemCount != 0)
                            {
                                UIInvertory.cells[i4].ItemStack.ItemCount -= 1;

                                world.SetTile(TileType.BOARD, i, j);
                            }

                            waitTimer = 0;
                        }
                        else
                            waitTimer += 8f * Program.Delta;
                    }
                    if (UIInvertory.cells[i4].ItemStack != null && UIInvertory.cells[i4].ItemStack.ItemCount == 0)
                    {
                        UIInvertory.cells[i4].ItemStack = null;
                        Invertory.UIItemStack.ClearUIInvertory();
                    }
                }

                if (!Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    tools = 0;
                    angle = -90;
                    AnimationUpdate("toolsIdle");
                }
            }
        }

        public async Task setNomeBlock(int type, int i, int j)
        {
            if (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type != TileType.NONE)
            {
                if (type == 0 && Program.Game.World.GetTile(i, j).type != TileType.TREEBRAK && Program.Game.World.GetTile(i, j).type != TileType.TREETOPS)
                {
                    if (Program.Game.World.GetTile(i, j).HealthTile <= 0)
                    {
                        if (Program.Game.World.tiles[i, j - 1] != null)
                        {
                            if (Program.Game.World.tiles[i, j - 1].type != TileType.TREEBRAK)
                            {
                                world.DelTile(Program.Game.World.GetTile(i, j).type, i, j);
                            }
                        }
                        else
                        {
                            world.DelTile(Program.Game.World.GetTile(i, j).type, i, j);
                        }
                    }
                    else if (Program.Game.World.GetTile(i, j).type == TileType.GRASS || Program.Game.World.GetTile(i, j).type == TileType.GROUND)
                        Program.Game.World.GetTile(i, j).HealthTile -= 3f * Program.Delta;
                    else if (Program.Game.World.GetTile(i, j).type == TileType.STONE)
                        Program.Game.World.GetTile(i, j).HealthTile -= 2f * Program.Delta;
                    else if (Program.Game.World.GetTile(i, j).type == TileType.IRONORE)
                        Program.Game.World.GetTile(i, j).HealthTile -= 1f * Program.Delta;
                    else if (Program.Game.World.GetTile(i, j).type != TileType.NONE)
                        Program.Game.World.GetTile(i, j).HealthTile -= 3f * Program.Delta;
                }
                else if (type == 1 && Program.Game.World.GetTile(i, j).type == TileType.TREEBRAK || Program.Game.World.GetTile(i, j).type == TileType.TREETOPS)
                {
                    if (Program.Game.World.GetTile(i, j).HealthTile <= 0)
                    {
                        world.DelTile(TileType.TREEBRAK, i, j);
                        thread.Start();
                    }
                    else
                        Program.Game.World.GetTile(i, j).HealthTile -= 1f * Program.Delta;
                }

                AnimationUpdate("toolseIdle");
            }
        }

        public void AnimationUpdate(string Animname)
        {
            if(Animname == "run")
            {
                asHair.Play(Animname);
                asHead.Play(Animname);
                asShirt.Play(Animname);
                asUndershirt.Play(Animname);
                asHands.Play(Animname);
                asLegs.Play(Animname);
                asShoes.Play(Animname);
            }
            else if(Animname == "idle")
            {
                asHair.Play(Animname);
                asHead.Play(Animname);
                asShirt.Play(Animname);
                asUndershirt.Play(Animname);
                asHands.Play(Animname);
                asLegs.Play(Animname);
                asShoes.Play(Animname);
            }
            else if(Animname == "tools")
            { 
                asShirt2.Play(Animname);
                asUndershirt2.Play(Animname);
                asHands2.Play(Animname);
            }
            else if (Animname == "toolsIdle")
            {
                asShirt2.Play(Animname);
                asUndershirt2.Play(Animname);
                asHands2.Play(Animname);
            }
            else if(Animname == "AxeRun")
            {
                if (angle < 120)
                {
                    asAxe.Rotation = angle;
                    angle += 16;
                }
                else
                    angle = -90;

                asAxe.Play(Animname); 
            }
            else if (Animname == "pickRun")
            {
                if (angle < 120)
                {
                    asPick.Rotation = angle;
                    angle += 16;
                }
                else
                    angle = -90;

                asPick.Play(Animname);
            }
            else if (Animname == "SwordRun")
            {
                if (angle < 120)
                {
                    asSword.Rotation = angle;
                    angle += 16;
                }
                else
                    angle = -90;

                asSword.Play(Animname);
            }
        }

        int angle = -90;

        bool isJump;
        private void updateMovement()
        {
            Invertory.ResetSelected();

            Content.ssBackgroundSky.Position = Position;
            Content.ssBackgroundSky.Origin = new Vector2f(Content.ssTextureBackgroundSky.Size.X / 2, Content.ssTextureBackgroundSky.Size.Y / 2);
            Content.ssBackgroundSky.Scale = new Vector2f(50, 1);


            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isInventory = Keyboard.IsKeyPressed(Keyboard.Key.Escape);

            if (!isFly)
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

                    if (Position.X < Program.Window.Size.X / 2 + 16)
                        movement.X = 0;
                }
                else if (isMoveRight)
                {
                    if (movement.X < 0)
                        movement.X = 0;

                    movement.X += PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = 1;

                    if (Position.X > World.WORLD_WIDTH * 16 - Program.Window.Size.X / 2)
                        movement.X = 0;
                }

                if (movement.X > PLAYER_MOVE_SPEED)
                    movement.X = PLAYER_MOVE_SPEED;
                else if (movement.X < -PLAYER_MOVE_SPEED)
                    movement.X = -PLAYER_MOVE_SPEED;

                // Анимация
                AnimationUpdate("run");
            }
            else
            {
                movement = new Vector2f();

                // Анимация
                AnimationUpdate("idle");
            }
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            //target.Draw(rectDirection, states);

            target.Draw(asHead, states);
            target.Draw(asHair, states);
            if (tools == 0)
            {
                target.Draw(asShirt, states);
                target.Draw(asUndershirt, states);
                target.Draw(asHands, states);
            }
            else
            {
                target.Draw(asShirt2, states);
                target.Draw(asUndershirt2, states);
                target.Draw(asHands2, states);
            }
            target.Draw(asLegs, states);
            target.Draw(asShoes, states);

            if (tools == 1)
            {
                target.Draw(asPick, states);
            }
            else if (tools == 2)
            {
                target.Draw(asAxe, states);
            }
            else if (tools == 3)
            {
                target.Draw(asSword, states);
            }
        }

        public override void OnWallCollided()
        {

        }
    }
}
