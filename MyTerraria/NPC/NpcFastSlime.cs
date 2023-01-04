using SFML.Graphics;
using SFML.System;

namespace MyTerraria.NPC
{
    class NpcFastSlime : NpcSlime
    {
        public NpcFastSlime(World world) : base(world)
        {
            spriteSheet = Content.ssNpcSlime;

            rect.FillColor = new Color(0, 0, 255, 200);

            rect.Texture = spriteSheet.Texture;
        }

        public override Vector2f GetJumpVelocity()
        {
            return new Vector2f(Direction * World.Rand.Next(10, 150), -World.Rand.Next(8, 15));
        }
    }
}
