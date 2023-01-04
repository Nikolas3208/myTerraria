using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace MyTerraria.NPC
{
    public abstract class Npc : Entity
    {
        public Vector2f StartPosition;
        public Vector2f vector;

        public int Direction
        {
            set
            {
                int dir = value >= 0 ? 1 : -1;
                Scale = new Vector2f(dir, 1);
            }
            get
            {
                int dir = Scale.X >= 0 ? 1 : -1;
                return dir;
            }
        }

        public Npc(World world) : base(world)
        {
        }

        // Возрождение NPC
        public void Spawn()
        {
            Position = StartPosition;
            velocity = new Vector2f();
            // тут возможно будут спецэффекты
        }

        public override void Update()
        {
            UpdateNPC();
            base.Update();

            // Если игрок упал в пропасть, то возрождаем его
            if (Position.Y > World.WORLD_HEIGHT * 16)
                OnKill();

            if (Position.X < 4 * 16)
            {
                Direction *= -1;
                velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
            }
            if (Position.X > World.WORLD_WIDTH * 16)
            {
                Direction *= -1;
                velocity = new Vector2f(-velocity.X * 0.8f, velocity.Y);
            }

            if (MathHelper.GetDistance(Program.Game.Player.Position, Position) > 1200)
            {
                OnKill();
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if (isRectVisible)
                target.Draw(rect, states);

            DrawNPC(target, states);
        }
        
        public abstract void OnKill();
        public abstract void UpdateNPC();
        public abstract void DrawNPC(RenderTarget target, RenderStates states);
    }
}
