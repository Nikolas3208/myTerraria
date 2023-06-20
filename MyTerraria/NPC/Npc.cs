using SFML.Graphics;
using SFML.System;
using System.Threading.Tasks;

namespace MyTerraria.NPC
{
    public abstract class Npc : Entity
    {
        public SoundController soundController;

        public Vector2f StartPosition;
        public Vector2f vector;
        public FloatRect PlayerRect;

        public float health = 100f;

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
            PlayerRect = new FloatRect(Program.Game.Player.Position - Program.Game.Player.rect.Origin, Program.Game.Player.rect.Size);

            UpdateNPC();
            base.Update();

            // Если игрок упал в пропасть, то возрождаем его
            if (Position.Y / 16 > World.WORLD_HEIGHT * 16)
                OnKill();
            else if(health <= 0)
                OnKill();

            if (Position.X < (Program.Window.Size.X / 256 / 2 + 1) * 256)
            {
                Position = new Vector2f((Program.Window.Size.X / 256 / 2 + 1) * 256, Position.Y);
            }
            else if (Position.X > World.WORLD_WIDTH * 256 - ((Program.Window.Size.X / 256 / 2 + 1) * 256))
            {
                Position = new Vector2f(World.WORLD_WIDTH * 256 - ((Program.Window.Size.X / 256 / 2 + 1) * 256), Position.Y);
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
