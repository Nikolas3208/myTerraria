﻿using MyTerraria.Items;
using MyTerraria.UI;
using MyTerraria.Worlds;

using SFML.Graphics;
using SFML.System;
using SFML.Window;

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyTerraria.NPC
{
    public enum AnimType
    {
        Idle,
        Run,
        Jump,
        Tool
    }
    class Player : Npc
    {
        //Максимальная скорость
        public const float PLAYER_MOVE_SPEED = 4f;
        //Ускореие
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Color HairColor = new Color(0, 240, 10);  // Цвет волос
        public Color BodyColor = new Color(255, 229, 186);  // Цвет кожи
        public Color ShirtColor = new Color(255, 255, 0);  // Цвет куртки
        public Color LegsColor = new Color(0, 76, 135);  // Цвет штанов

        // UI
        public UIInvertory Invertory;

        //Прыжок?
        private bool isJump;

        //Позиция мыши в окне
        private Vector2i mousePos;

        //Какой слот выбран
        private int uiIsSelected = 0;

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
            health = 100f;

            //Window = new RenderWindow(new VideoMode(800, 600), "Моя Terraria!");
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE * 2f, Tile.TILE_SIZE * 3f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 3);

            isRectVisible = false;

            CreateAnimation();
        }

        private void CreateAnimation()
        {

            // Волосы
            asHair = new AnimSprite(Content.ssPlayerHair);
            asHair.Position = new Vector2f(0, 19);
            asHair.Color = HairColor;
            asHair.AddAnimation("idle", new Animation(
                new AnimationFrame(0, 0, 0.1f)));
            asHair.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 1f)));
            asHair.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 0.1f)));
            asHead.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 1f)));
            asHead.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 0.1f)));
            asShirt.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 0.5f),
                new AnimationFrame(0, 1, 0.5f),
                new AnimationFrame(0, 2, 0.5f),
                new AnimationFrame(0, 3, 0.5f),
                new AnimationFrame(0, 4, 0.5f)));
            asShirt.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 1f)));
            asUndershirt.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 0.5f),
                new AnimationFrame(0, 1, 0.5f),
                new AnimationFrame(0, 2, 0.5f),
                new AnimationFrame(0, 3, 0.5f),
                new AnimationFrame(0, 4, 0.5f)));
            asUndershirt.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 0.1f)));
            asHands.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 0.5f),
                new AnimationFrame(0, 1, 0.5f),
                new AnimationFrame(0, 2, 0.5f),
                new AnimationFrame(0, 3, 0.5f),
                new AnimationFrame(0, 4, 0.5f)));
            asHands.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 0.1f)));
            asLegs.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 0.1f)));
            asLegs.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 0.1f)));
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
                new AnimationFrame(0, 0, 1f)));
            asShoes.AddAnimation("tool", new Animation(
                new AnimationFrame(0, 0, 1f)));
            asShoes.AddAnimation("jump", new Animation(
                new AnimationFrame(0, 5, 1f)));
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
            health = 100;
            Spawn();
        }

        public Vector2f GetGlobalPosition()
        {
            return new Vector2f(Position.X - Program.Window.Size.X / 2, Position.Y - Program.Window.Size.Y / 2);
        }

        public override void UpdateNPC()
        {
            updateMovement();

            UpdateMouse();

            UpdateUIInventoriSelected();
        }

        private void UpdateUIInventoriSelected()
        {
            Invertory.ResetSelected();

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                uiIsSelected = 0;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                uiIsSelected = 1;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                uiIsSelected = 2;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num4))
                uiIsSelected = 3;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num5))
                uiIsSelected = 4;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num6))
                uiIsSelected = 5;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num7))
                uiIsSelected = 6;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num8))
                uiIsSelected = 7;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num9))
                uiIsSelected = 8;
            if (Keyboard.IsKeyPressed(Keyboard.Key.Num0))
                uiIsSelected = 9;

            UIInvertory.cells[uiIsSelected].IsSelected = true;
        }

        Sprite tool;

        private void UpdateMouse()
        {
            mousePos = Program.GetGlobalMousePosition();


            if (UIInvertory.cells != null && UIInvertory.cells[uiIsSelected].ItemStack != null && UIInvertory.cells[uiIsSelected].ItemStack.InfoItem != null)
                DebugRender.AddRectangle(mousePos.X, mousePos.Y, UIInvertory.cells[uiIsSelected].ItemStack.InfoItem.SpriteSheet.Texture.Size.X, UIInvertory.cells[uiIsSelected].ItemStack.InfoItem.SpriteSheet.Texture.Size.Y, UIInvertory.cells[uiIsSelected].ItemStack.InfoItem.SpriteSheet.Texture);

            FloatRect rect = new FloatRect(new Vector2f(mousePos.X, mousePos.Y), new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

            DebugRender.AddRectangle(rect, Color.Green);

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Tile tile = (Tile)world.GetTile(mousePos.X / 16, mousePos.Y / 16);

                if (UIInvertory.cells != null && UIInvertory.cells[uiIsSelected].ItemStack != null && UIInvertory.cells[uiIsSelected].ItemStack.itemCount != 0)
                {
                    var InfoItemType = UIInvertory.cells[uiIsSelected].ItemStack.InfoItem;
                    if (tile != null)
                    {
                        if (InfoItemType.Tooltype == ToolType.Pick && tile.type != TileType.Treebark)
                        {
                            world.SetTile(Tiles.Tile, tile.type, mousePos.X / 16, mousePos.Y / 16, tile.isGrass, true);
                            AnimationUpdate(AnimType.Tool);
                        }
                        else if(InfoItemType.Tooltype == ToolType.Axe && tile.type == TileType.Treebark)
                        {
                            
                            AnimationUpdate(AnimType.Tool);
                        }

                    }
                    else
                    {
                        Tile upTile = (Tile)Program.Game.World.GetTile(mousePos.X / 16, mousePos.Y / 16 - 1);     // Верхний сосед
                        Tile downTile = (Tile)Program.Game.World.GetTile(mousePos.X / 16, mousePos.Y / 16 + 1);   // Нижний сосед
                        Tile leftTile = (Tile)Program.Game.World.GetTile(mousePos.X / 16 - 1, mousePos.Y / 16);   // Левый сосед
                        Tile rightTile = (Tile)Program.Game.World.GetTile(mousePos.X / 16 + 1, mousePos.Y / 16);  // Правый сосед

                        if (InfoItemType.Tooltype == ToolType.None && InfoItemType.Weapontype == WeaponType.None && (upTile != null || downTile != null || leftTile != null || rightTile != null))
                        {
                            world.SetTile(InfoItemType.Tiles, InfoItemType.Tiletype, mousePos.X / 16, mousePos.Y / 16);
                            UIInvertory.cells[uiIsSelected].ItemStack.ItemCount--;

                            AnimationUpdate(AnimType.Tool);
                        }
                    }
                    ToolUpdate();

                }
            }
        }

        float angle = -90;
        float speed = 5f;

        private void ToolUpdate()
        {
            tool = new Sprite(UIInvertory.cells[uiIsSelected].ItemStack.InfoItem.SpriteSheet.Texture);
            tool.Origin = new Vector2f(0, tool.Texture.Size.Y);
            tool.Rotation = angle;

            FloatRect toolRect = new FloatRect(Position, new Vector2f(tool.Texture.Size.X, tool.Texture.Size.Y));

            DebugRender.AddRectangle(toolRect, Color.Red);

            //tool.Position = GetGlobalPosition();

            angle += 3.23f * speed;

            if (angle >= 360)
                angle = -90;
        }

        public void AnimationUpdate(AnimType type)
        {
            if(type == AnimType.Run)
            {
                tool = null;

                asHair.Play("run");
                asHead.Play("run");
                asShirt.Play("run");
                asUndershirt.Play("run");
                asHands.Play("run");
                asLegs.Play("run");
                asShoes.Play("run");
            }
            else if(type == AnimType.Idle)
            {
                tool = null;

                asHair.Play("idle");
                asHead.Play("idle");
                asShirt.Play("idle");
                asUndershirt.Play("idle");
                asHands.Play("idle");
                asLegs.Play("idle");
                asShoes.Play("idle");
            }
            else if(type == AnimType.Jump)
            {
                tool = null;

                asHair.Play("jump");
                asHead.Play("jump");
                asShirt.Play("jump");
                asUndershirt.Play("jump");
                asHands.Play("jump");
                asLegs.Play("jump");
                asShoes.Play("jump");
            }
            else if(type == AnimType.Tool)
            {
                asHair.Play("tool");
                asHead.Play("tool");
                asShirt.Play("tool");
                asUndershirt.Play("tool");
                asHands.Play("tool");
                asLegs.Play("tool");
                asShoes.Play("tool");
            }
        }

        float timerA = 0;

        private void updateMovement()
        {
            Content.ssBackgroundSky.Position = Position;
            Content.ssBackgroundSky.Origin = new Vector2f(Content.ssTextureBackgroundSky.Size.X / 2, Content.ssTextureBackgroundSky.Size.Y / 2);
            Content.ssBackgroundSky.Scale = new Vector2f(50, 1);

            /*foreach (var slime in Program.Game.Npc)
            {
                FloatRect slimeRect = new FloatRect(slime.Position, new Vector2f(spriteSheet.SubWidth / 1.5f, spriteSheet.SubHeight / 1.5f));

                if (PlayerRect.Intersects(slimeRect))
                {
                    DebugRender.AddRectangle(PlayerRect, Color.Red);

                    Direction = slime.Direction * -1;
                    velocity = new Vector2f(-velocity.X * 2.8f, -velocity.Y * 0.3f) * Direction;
                }
            }*/

            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);

            if (!isFly)
                isJump = Keyboard.IsKeyPressed(Keyboard.Key.Space);

            bool isMove = isMoveLeft || isMoveRight;            

            int jump = 0;
            float jumpSpeed = 10.01f;
            bool releaseJump = true;
            int jumpHeight = 5 * 16;
            float maxFallSpeed = 20f;

            if (isJump && timerA == 0)
            {
                AnimationUpdate(AnimType.Jump);

                if (jump > 0)
                {
                    if (velocity.Y > (-jumpSpeed + (gravity * 2f)))
                    {
                        jump = 0;
                    }
                    else
                    {
                        velocity.Y = -jumpSpeed;
                        jump--;
                    }
                }
                else if ((velocity.Y == 0f) && releaseJump)
                {
                    velocity.Y = -jumpSpeed;
                    jump = jumpHeight;
                }
                releaseJump = false;
            }
            else
            {
                jump = 0;
                releaseJump = true;
            }
            if (this.velocity.Y > maxFallSpeed)
            {
                this.velocity.Y = maxFallSpeed;
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

                // Анимация ходьбы
                if(isJump && timerA == 0)
                    AnimationUpdate(AnimType.Jump);
                else if (!isJump)
                    AnimationUpdate(AnimType.Run);
            }
            else
            {
                movement = new Vector2f();

                // Анимация спокойствия
                if (!isFly && !Mouse.IsButtonPressed(Mouse.Button.Left))
                    AnimationUpdate(AnimType.Idle);

                if(isJump)
                    AnimationUpdate(AnimType.Jump);

                if(Mouse.IsButtonPressed(Mouse.Button.Left))
                    AnimationUpdate(AnimType.Tool);
            }

            if (isJump)
                timerA += 1 * Program.Delta;
            else
                timerA = 0;
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            target.Draw(asHead, states);
            target.Draw(asHair, states);
            target.Draw(asShirt, states);
            target.Draw(asUndershirt, states);
            target.Draw(asHands, states);
            target.Draw(asLegs, states);
            target.Draw(asShoes, states);

            if (tool != null)
                target.Draw(tool, states);
        }

        public override void OnWallCollided()
        {
            //velocity.X = 0;
        }

        public void AddTools()
        {
            var itemTile = new ItemTile(world, InfoItem.ItemSword);
            itemTile.Position = Position;
            world.items.Add(itemTile);
            itemTile = new ItemTile(world, InfoItem.ItemPick);
            itemTile.Position = Position;
            world.items.Add(itemTile);
            itemTile = new ItemTile(world, InfoItem.ItemAxe);
            itemTile.Position = Position;
            world.items.Add(itemTile);

            itemTile = new ItemTile(world, InfoItem.ItemVegetation);
            itemTile.Position = Position;
            world.items.Add(itemTile);
        }
    }
}
