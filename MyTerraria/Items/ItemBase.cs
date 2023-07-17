using MyTerraria.UI;
using MyTerraria.Worlds;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace MyTerraria.Items
{
    public enum ItemType
    {
        Tile,
        Axe,
        Pick
    };

    public class ItemBase : Transformable
    {
        public const float MOVE_DISTANCE_TO_PLAYER = 100f;  // Дистанция начала движения предмета в сторону игрока
        public const float TAKE_DISTANCE_TO_PLAYER = 20f;   // Дистанция подбора предмета игроком
        public const float MOVE_SPEED_COEF = 0.5f;          // Коэффицент увеличения скорости движения

        public int MaxStackSize { get; protected set; } = 99;

        public int MaxDamage { get; protected set; } = 0;

        public int Id { get; protected set; }

        public int TextureNumber { get; protected set; }

        public ItemType IType { get; protected set; }

        public Texture Texture { get; protected set; }

        public TileType tileType { get; protected set; }

        public bool IsDestroyed { get; private set; } = false;

        private bool isGhost;
        private float gravity = 0.4f;
        private Vector2f velocity;

        public ItemBase(int textureNumber, int maxStackSize)
        {
            MaxStackSize = maxStackSize;
            TextureNumber = textureNumber;
        }
        public void Update()
        {
            Vector2f playerPos = Program.Game.Player.Position;
            float dist = MathHelper.GetDistance(Position, playerPos);

            isGhost = dist < MOVE_DISTANCE_TO_PLAYER;

            
            if (isGhost)
            {
                if (dist < TAKE_DISTANCE_TO_PLAYER)
                {
                    if (IType == ItemType.Tile && Program.Game.Player.Invertory.AddItemStack(new UIItemStack(this, 1)))
                    {
                        IsDestroyed = true;
                        //sound.PlaySound("grab");
                    }
                }
                else
                {
                    Vector2f dir = MathHelper.Normalize(playerPos - Position);

                    float speed = 2f - dist / MOVE_DISTANCE_TO_PLAYER;
                    velocity += dir * speed * MOVE_SPEED_COEF;
                }
            }

            if (!isGhost)
            {
                velocity.Y += gravity;

                FloatRect stepRect = new FloatRect(Position, new Vector2f(Texture.Size.X, Texture.Size.Y));
                Vector2f stepPos = Position - (new Vector2f(Texture.Size.X, Texture.Size.Y) / 2);

                int x = (int)((stepPos.X + (Tile.TILE_SIZE / 2)) / Tile.TILE_SIZE);
                int y = (int)((stepPos.Y + Texture.Size.Y) / Tile.TILE_SIZE);

                Chunk chunk = Program.Game.World.GetChunk(x, y);

                Tile tile = Program.Game.World.GetTile(x, y);

                if (tile != null)
                {
                    FloatRect tileRect = new FloatRect(tile.Position + chunk.Position, new Vector2f(Tile.TILE_SIZE, Tile.TILE_SIZE));

                    if (updateCollision(stepRect, tileRect, ref stepPos))
                    {
                        velocity.Y = 0;
                    }
                }
            }
            if (dist > Program.Window.Size.X / 2)
                velocity = new Vector2f();

            Position += velocity;
        }

        private bool updateCollision(FloatRect rectNPC, FloatRect rectTile, ref Vector2f pos)
        {
            if (rectNPC.Intersects(rectTile))
            {
                pos = new Vector2f(pos.X, rectTile.Top - rectNPC.Height + 1);
                return true;
            }

            return false;
        }
    }
}
