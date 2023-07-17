using MyTerraria.UI;
using MyTerraria.Worlds;
using MyTerraria.Items.ItemTile;
using SFML.Graphics;
using SFML.System;
using System;
using MyTerraria.Items.ItemTool;

namespace MyTerraria.Items
{
    public abstract class Item : Entity
    {
        // Максимальное кол-во предметов в стеке
        public int MaxCountInStack { get; set; } = 99;
        public TileType TileType { get; set; }
        public ItemType type { get; set; }
        public Texture Texture { get; set; }
        public int SpriteI { get; private set; }
        public int SpriteJ { get; private set; }

        private SoundController sound = new SoundController();

        public int Count = 1; //Количество подбираемых предметов
        public const float MOVE_DISTANCE_TO_PLAYER = 100f;  // Дистанция начала движения предмета в сторону игрока
        public const float TAKE_DISTANCE_TO_PLAYER = 20f;   // Дистанция подбора предмета игроком
        public const float MOVE_SPEED_COEF = 0.5f;          // Коэффицент увеличения скорости движения

        public Item(World world, Texture texture) : base(world)
        {
            this.Texture = texture;
            this.type = type;

            rect = new RectangleShape(new Vector2f(texture.Size.X, texture.Size.Y));
            rect.Texture = texture;

            sound.AddSound("grab", Content.mGrab);
        }

        public override void Update()
        {
            Vector2f playerPos = Program.Game.Player.Position;
            float dist = MathHelper.GetDistance(Position, playerPos);

            isGhost = dist < MOVE_DISTANCE_TO_PLAYER;

            if (isGhost)
            {
                if (dist < TAKE_DISTANCE_TO_PLAYER)
                {
                    /*if (Program.Game.Player.Invertory.AddItemStack(new UIItemStack(new ItemTile.ItemTile(world, Texture, TileType), Count)))
                    {
                        IsDestroyed = true;
                        sound.PlaySound("grab");
                    }*/
                }
                else
                {
                    Vector2f dir = MathHelper.Normalize(playerPos - Position);

                    float speed = 2f - dist / MOVE_DISTANCE_TO_PLAYER;
                    velocity += dir * speed * MOVE_SPEED_COEF;
                }
            }

            base.Update();

        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            if (isRectVisible)
                target.Draw(rect, states);
        }

        public abstract bool OnClickMouseButton(Tile tile);
        public abstract void SetSetingsItem();
    }
}
