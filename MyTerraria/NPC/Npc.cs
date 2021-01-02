using SFML.Graphics;
using SFML.System;

namespace MyTerraria.NPC
{
    abstract class Npc : Entity
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
            if (Position.Y > Program.Window.Size.Y)
                OnKill();

            var vec = vector.X += 5;

            View viev = new View();
            viev.Move(new Vector2f(vec,6));
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
