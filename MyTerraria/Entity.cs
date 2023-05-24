using MyTerraria.Worlds;
using SFML.Graphics;
using SFML.System;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyTerraria
{
    enum DirectionType
    {
        Left, Right, Up, Down
    }

    public abstract class Entity : Transformable, Drawable
    {
        public bool IsDestroyed = false;        // Объект уничтожен?

        protected RectangleShape rect;

        protected Color color;
        protected Vector2f velocity;
        protected Vector2f movement;

        protected World world;

        protected bool isPlayer = true;
        protected bool isFly = true;
        protected bool isRectVisible = true;
        protected bool isGhost = false;         // Режим призрака?
        
        public static bool debag = true;

        protected float gravity = 0.4f;

        public Entity(World world)
        {
            this.world = world;
        }

        public virtual void Update()
        {
            updatePhysics();
        }

        private void updatePhysics()
        {
            velocity.X *= 0.99f;

            if (!isGhost)
            {
                this.velocity.Y += gravity;

                var offset = velocity + movement;
                float dist = MathHelper.GetDistance(offset);

                int countStep = 1;
                if (dist > (float)Tile.TILE_SIZE / 2)
                    countStep = (int)(dist / (Tile.TILE_SIZE / 2));

                Vector2f nextPos = Position + offset;
                Vector2f stepPos = Position - rect.Origin;
                FloatRect stepRect; // = new FloatRect(stepPos, rect.Size);
                Vector2f stepVec = (nextPos - Position) / countStep;

                for (int step = 0; step < countStep; step++)
                {
                    bool isBreakStep = false;

                    stepPos += stepVec;
                    stepRect = new FloatRect(stepPos, rect.Size);

                    if (debag)
                        DebugRender.AddRectangle(stepRect, Color.Blue);

                    for (int x = 0; x <= (rect.Size.X - rect.Origin.X); x += Tile.TILE_SIZE)
                    {

                        int i = (int)((stepPos.X + (Tile.TILE_SIZE / 2) + x) / Tile.TILE_SIZE);
                        int j = (int)((stepPos.Y + rect.Size.Y) / Tile.TILE_SIZE);

                        Tile tile = (Tile)world.GetITile(i, j);

                        Tile tileTop = (Tile)world.GetITile(i, j - (int)rect.Size.Y / Tile.TILE_SIZE - 1);

                        if (tile != null && tile.tile != Tiles.Wall)
                        {
                            FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                            if (debag)
                                DebugRender.AddRectangle(tileRect, Color.Red);

                            if (updateCollision(stepRect, tileRect, DirectionType.Down, ref stepPos))
                            {
                                velocity.Y = 0;
                                isFly = false;

                                isBreakStep = false;
                            }
                            else
                                isFly = true;

                        }
                        else if(world.GetITile((int)(stepPos.X + x) / Tile.TILE_SIZE, j) == null && world.GetITile((int)(stepPos.X - x) / Tile.TILE_SIZE, j) == null)
                            isFly = true;


                        if (tileTop != null)
                        {
                            FloatRect tiletop = new FloatRect(tileTop.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                            if (debag)
                                DebugRender.AddRectangle(tiletop, Color.Red);

                            updateCollision(stepRect, tiletop, DirectionType.Up, ref stepPos);

                            velocity.Y += 2f;
                        }


                        if (updateWallCollision(i, j, -1, ref stepPos, stepRect) || updateWallCollision(i, j, 1, ref stepPos, stepRect))
                        {
                            OnWallCollided();
                            isBreakStep = false;
                        }

                        if (isBreakStep)
                            break;

                    }
                }
                Position = stepPos + rect.Origin;
            }
            else
                Position += velocity + movement;
        }

        bool updateWallCollision(int i, int j, int iOffset, ref Vector2f stepPos, FloatRect stepRect)
        {
            var dirType = iOffset > 0 ? DirectionType.Right : DirectionType.Left;

            bool isWallCollided = false;

            for (int y = 1; y <= rect.Size.Y / Tile.TILE_SIZE; y++)
            {

                Tile[] walls = new Tile[] {
                    (Tile)world.GetITile(i + iOffset, j - y),
                };


                isWallCollided = false;
                foreach (Worlds.Tile t in walls)
                {
                    if (t == null) continue;

                    FloatRect tileRect = new FloatRect(t.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
                    if (debag)
                        DebugRender.AddRectangle(tileRect, Color.Yellow);

                    if (t != null && t.tile != Tiles.Wall)
                    {
                        if (updateCollision(stepRect, tileRect, dirType, ref stepPos))
                        {
                            isWallCollided = true;
                        }
                    }
                }
            }

                return isWallCollided;
        }

        bool updateCollision(FloatRect rectNPC, FloatRect rectTile, DirectionType direction, ref Vector2f pos)
        {
            if (rectNPC.Intersects(rectTile))
            {
                    if (direction == DirectionType.Up)
                        pos = new Vector2f(pos.X, rectTile.Top + rectNPC.Height + 1);
                    else if (direction == DirectionType.Down)
                        pos = new Vector2f(pos.X, rectTile.Top - rectNPC.Height + 1);
                    else if (direction == DirectionType.Left)
                        pos = new Vector2f(rectTile.Left + rectTile.Width + 1, pos.Y);
                    else if (direction == DirectionType.Right)
                        pos = new Vector2f(rectTile.Left - rectNPC.Width + 1, pos.Y);
                return true;
            }

            return false;
        }

        public abstract void OnWallCollided();
        public abstract void Draw(RenderTarget target, RenderStates states);
    }
}
