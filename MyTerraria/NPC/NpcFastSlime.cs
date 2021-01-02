using SFML.Graphics;
using SFML.System;

namespace MyTerraria.NPC
{
    class NpcFastSlime : NpcSlime
    {
        SpriteSheet spriteSheet;
        Color color = new Color();
        public NpcFastSlime(World world) : base(world)
        {
            spriteSheet = Content.ssNpcSlime;
            rect = new RectangleShape(new Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);
            rect.FillColor = Color.Yellow;

            rect.Texture = spriteSheet.Texture;
            rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
        }

        public override void UpdateNPC()
        {
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
        }

        public override Vector2f GetJumpVelocity()
        {
            return new Vector2f(Direction * World.Rand.Next(15, 100), -World.Rand.Next(8, 15));
        }
    }
}
