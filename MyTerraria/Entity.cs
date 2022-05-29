using SFML.Graphics;
using SFML.System;

namespace MyTerraria
{
    enum DirectionType
    {
        Left, Right, Up, Down
    }

    abstract class Entity : Transformable, Drawable
    {
        public bool IsDestroyed = false;        // Объект уничтожен?

        int i1, j1;

        protected RectangleShape rect;
        protected Vector2f velocity;
        protected Vector2f movement;
        protected World world;
        protected bool isFly = true;
        protected bool isRectVisible = true;
        protected bool isGhost = false;         // Режим призрака?
        protected bool isGhost1 = true;         // Режим призрака?

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
                velocity.Y += 0.55f;

                var offset = velocity + movement;
                float dist = MathHelper.GetDistance(offset);

                int countStep = 1;
                if (dist > (float)Tile.TILE_SIZE / 2)
                    countStep = (int)(dist / (Tile.TILE_SIZE / 2));

                Vector2f nextPos = Position + offset;
                Vector2f stepPos = Position - rect.Origin;
                FloatRect stepRect = new FloatRect(stepPos, rect.Size);
                Vector2f stepVec = (nextPos - Position) / countStep;

                for (int step = 0; step < countStep; step++)
                {
                    bool isBreakStep = false;

                    stepPos += stepVec;
                    stepRect = new FloatRect(stepPos, rect.Size);

                    DebugRender.AddRectangle(stepRect, Color.Blue);

                    int i = (int)((stepPos.X + rect.Size.X / 2) / Tile.TILE_SIZE);
                    int j = (int)((stepPos.Y + rect.Size.Y) / Tile.TILE_SIZE);
                    Tile tile = world.GetTile(i, j);
                    Tile tile1 = world.GetTile(i, j - 4);
                    if (i < 0)
                        i = 0;
                    if (j < 0)
                        j = 0;
                    if (i > 599)
                        i = 599;
                    if (j > 1999)
                        j = 1999;

                    if (Program.Game.World.type_Tile[i, j] != "TREEBRAK" && Program.Game.World.type_Tile[i, j] != "TREETOPS")
                    {
                        if (tile != null)
                        {
                            FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                            DebugRender.AddRectangle(tileRect, Color.Red);

                            if (updateCollision(stepRect, tileRect, DirectionType.Down, ref stepPos))
                            {
                                velocity.Y = 0;
                                isFly = false;

                                isBreakStep = true;
                            }
                            else
                                isFly = true;
                        }
                        else
                            isFly = true;
                    }

                    if (tile1 != null)
                    {
                        FloatRect tileRect = new FloatRect(tile1.Position - new Vector2f(0, 5), new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                        DebugRender.AddRectangle(tileRect, Color.Red);

                        if (updateCollision(stepRect, tileRect, DirectionType.Up, ref stepPos) && isGhost1)
                        {
                            velocity.Y = 11;
                            isFly = true;

                            isBreakStep = true;
                        }
                        else if (isGhost1)
                            isFly = true;
                    }

                    if (updateWallCollision(i, j, -1, ref stepPos, stepRect) || updateWallCollision(i, j, 1, ref stepPos, stepRect))
                    {
                        OnWallCollided();
                        isBreakStep = false;
                    }

                    if (isBreakStep)
                        break;
                }

                Position = stepPos + rect.Origin;
            }
            else
                Position += velocity + movement;
        }

        bool updateWallCollision(int i, int j, int iOffset, ref Vector2f stepPos, FloatRect stepRect)
        {
            var dirType = iOffset > 0 ? DirectionType.Right : DirectionType.Left;

            Tile[] walls = new Tile[] {
                world.GetTile(i + iOffset, j - 1),
                world.GetTile(i + iOffset, j - 2),
                world.GetTile(i + iOffset, j - 3),
            };

            i1 = i;// + iOffset;
            j1 = j;

            bool isWallCollided = false;
            foreach (Tile t in walls)
            {
                if (t == null) continue;

                FloatRect tileRect = new FloatRect(t.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                if (Program.Game.World.type_Tile[i + iOffset, j] == "TREEBRAK")
                {
                    isGhost1 = false;
                }
                else
                    isGhost1 = true;

                if (world.GetTile(i + iOffset, j - 1) != null && world.GetTile(i + iOffset, j - 2) == null && velocity.Y >= 0 && velocity.X < 1 && Program.Game.World.type_Tile[i + iOffset, j - 1] != "TREEBRAK" && Program.Game.World.type_Tile[i + iOffset, j - 1] != "TREETOPS")
                {
                    //velocity.Y += -10f;
                }

                DebugRender.AddRectangle(tileRect, Color.Yellow);
                if (Program.Game.World.type_Tile[i + iOffset,j - 1] != "TREEBRAK" && Program.Game.World.type_Tile[i + iOffset, j - 1] != "TREETOPS")
                {
                    if (updateCollision(stepRect, tileRect, dirType, ref stepPos))
                    {
                        isWallCollided = true;
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
                        pos = new Vector2f(pos.X, rectTile.Top + rectNPC.Height - 1);
                    else if (direction == DirectionType.Down)
                        pos = new Vector2f(pos.X, rectTile.Top - rectNPC.Height + 1);
                    else if (direction == DirectionType.Left)
                        pos = new Vector2f(rectTile.Left + rectTile.Width - 1, pos.Y);
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
