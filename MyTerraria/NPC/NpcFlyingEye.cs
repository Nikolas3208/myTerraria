using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.NPC
{
    public class NpcFlyingEye : Npc
    {
        SpriteSheet spriteSheet;
        float waitTimer = 0f;

        public NpcFlyingEye(World world) : base(world)
        {
            isFlyer = true;

            spriteSheet = Content.ssNpcFlyingEye;

            rect = new RectangleShape(new SFML.System.Vector2f(spriteSheet.SubWidth, spriteSheet.SubHeight));
            rect.Origin = new SFML.System.Vector2f(rect.Size.X / 2, 0);

            rect.Texture = spriteSheet.Texture;
            rect.TextureRect = spriteSheet.GetTextureRect(0, 0);
        }

        public override void OnKill()
        {
            StartPosition = Program.Game.Player.Position;
            Spawn();
        }

        public override void OnWallCollided()
        {
            velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
        }

        public override void UpdateNPC()
        {

        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            target.Draw(rect, states);
        }
    }
}
