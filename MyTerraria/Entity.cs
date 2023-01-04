﻿using SFML.Graphics;
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
        protected bool isFly = true;
        protected bool isFlyer = false;
        protected bool isRectVisible = true;
        protected bool isGhost = false;         // Режим призрака?

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

            if (!isGhost && !isFlyer)
            {
                this.velocity.Y += gravity;

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

                    for (int x = 0; x <= (rect.Size.X - rect.Origin.X); x += Tile.TILE_SIZE)
                    {

                        int i = (int)((stepPos.X + 8 + x) / Tile.TILE_SIZE);
                        int j = (int)((stepPos.Y + rect.Size.Y) / Tile.TILE_SIZE);
                        Tile tile = world.GetTile(i, j);
                        Tile tileTop = world.GetTile(i, j - (int)rect.Size.Y / Tile.TILE_SIZE - 1);

                        if (i < 0 || i >= World.WORLD_WIDTH || j < 0 || j >= World.WORLD_HEIGHT)
                            return;


                        if (tile != null && tile.type != TileType.TREEBARK && tile.type != TileType.TREETOPS && tile.type != TileType.VEGETATION && tile.type != TileType.TORCH)
                        {
                            FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

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
                        else if(world.GetTile((int)(stepPos.X + x) / Tile.TILE_SIZE, j) == null && world.GetTile((int)(stepPos.X - x) / Tile.TILE_SIZE, j) == null)
                            isFly = true;


                        if (tileTop != null && tileTop.type != TileType.TREEBARK && tileTop.type != TileType.TREETOPS && tileTop.type != TileType.NONE)
                        {
                            FloatRect tiletop = new FloatRect(tileTop.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));
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
            else if(!isGhost && isFlyer)
            {
                //rect.Rotation = World.Rand.Next(0, 360);

                //velocity.Y += 0.44f;

                Position += velocity + movement;
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
                    world.GetTile(i + iOffset, j - y),
                };


                isWallCollided = false;
                foreach (Tile t in walls)
                {
                    if (t == null) continue;

                    FloatRect tileRect = new FloatRect(t.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                    DebugRender.AddRectangle(tileRect, Color.Yellow);
                    if (t != null && t.type != TileType.TREEBARK && t.type != TileType.TREETOPS && t.type != TileType.TORCH)
                    {
                        if (t != null && t.type != TileType.VEGETATION && t.type != TileType.NONE)
                        {
                            if (updateCollision(stepRect, tileRect, dirType, ref stepPos))
                            {
                                isWallCollided = true;
                            }
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
