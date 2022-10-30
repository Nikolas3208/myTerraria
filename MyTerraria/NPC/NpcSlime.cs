﻿using SFML.Graphics;
using SFML.System;

namespace MyTerraria.NPC
{
    class NpcSlime : Npc
    {
        const float TIME_WAIT_JUMP = 1f;

        SpriteSheet spriteSheet;
        float waitTimer = 0f;

        public static float health { get; set; } = 10;



        public NpcSlime(World world) : base(world)
        {
            spriteSheet = Content.ssNpcSlime;

            rect = new RectangleShape(new Vector2f(spriteSheet.SubWidth / 1.5f, spriteSheet.SubHeight / 1.5f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);
            rect.FillColor = new Color(0, 255, 0, 200);

            rect.Texture = spriteSheet.Texture;
            rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
        }

        public override void OnKill()
        {
            health = 10;
            StartPosition = Program.Game.Player.Position;
            Spawn();
        }

        public override void OnWallCollided()
        {
            Direction *= -1;
            velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
        }

        public override void UpdateNPC()
        {
            if (Position.X < 3 * 16)
            {
                Direction *= -1;
                velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
            }
            if(Position.X > World.WORLD_WIDTH * 16)
            {
                Direction *= -1;
                velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
            }

            if(MathHelper.GetDistance(Program.Game.Player.Position,Position) > 1200)
            {
                OnKill();
            }

            if (!isFly)
            {
                if (waitTimer >= TIME_WAIT_JUMP)
                {
                    velocity = GetJumpVelocity();
                    waitTimer = 0f;
                }
                else
                {
                    waitTimer += 0.05f;
                    velocity.X = 0f;
                }

                rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
            }
            else
                rect.TextureRect = spriteSheet.GetTextureRect(0, 1);

            if (health <= 0)
                OnKill();
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
        }

        public virtual Vector2f GetJumpVelocity()
        {
            return new Vector2f(Direction * World.Rand.Next(1, 15), -World.Rand.Next(8, 15));
        }
    }
}
