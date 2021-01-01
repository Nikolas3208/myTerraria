﻿using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace MyTerraria.NPC
{
    class Player : Npc
    {
        public static RenderWindow Window { private set; get; }

        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Color HairColor = new Color(255, 0, 0);  // Цвет волос
        public Color BodyColor = new Color(255, 229, 186);  // Цвет кожи
        public Color ShirtColor = new Color(255, 255, 0);  // Цвет куртки
        public Color LegsColor = new Color(0, 76, 135);  // Цвет штанов

        // UI
        public UIInvertory Invertory;

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
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);
            isRectVisible = true;

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

        public override void UpdateNPC()
        {

            updateMovement();

            if (UIManager.Over == null && UIManager.Drag == null)
            {
                Vector2i mousePos = Mouse.GetPosition(Program.Window);
                //Mouse.SetPosition(new Vector2i(100, 200));
                Tile tile = world.GetTileByWorldPos(mousePos);
                if (tile == null || tile != null)
                {
                    //FloatRect tileRect = tile.GetFloatRect();
                    DebugRender.AddRectangle(mousePos.X + world.xShift, mousePos.Y, 16, 16, Color.Green);

                    if (Mouse.IsButtonPressed(Mouse.Button.Left))
                    {
                        int i = (int)(mousePos.X / Tile.TILE_SIZE) + world.xShift;
                        int j = (int)(mousePos.Y / Tile.TILE_SIZE);
                        world.SetTile(TileType.NONE, i, j);
                    }
                }
                if (tile == null)
                {
                    if (Mouse.IsButtonPressed(Mouse.Button.Right))
                    {
                        int i = (int)(mousePos.X / Tile.TILE_SIZE) + world.xShift;
                        int j = (int)(mousePos.Y / Tile.TILE_SIZE);
                        world.SetTile(TileType.GROUND, i, j);
                    }
                }
            }
        }

        private void updateMovement()
        {
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isJump = Keyboard.IsKeyPressed(Keyboard.Key.Space);
            bool isMove = isMoveLeft || isMoveRight;

            // Прыжок
            if (isJump && !isFly)
            {
                velocity.Y = -10f;
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
