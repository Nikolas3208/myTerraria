using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using System;

namespace MyTerraria.Items
{
    public abstract class Item : Entity
    {
        private SoundController sound = new SoundController();

        public int Count = 1; //Количество подбираемых предметов
        public const float MOVE_DISTANCE_TO_PLAYER = 100f;  // Дистанция начала движения предмета в сторону игрока
        public const float TAKE_DISTANCE_TO_PLAYER = 20f;   // Дистанция подбора предмета игроком
        public const float MOVE_SPEED_COEF = 0.5f;          // Коэффицент увеличения скорости движения

        InfoItem infoItem;

        public Item(World world, InfoItem infoItem) : base(world)
        {
            this.infoItem = infoItem;
            rect = new RectangleShape(new Vector2f(infoItem.SpriteSheet.SubWidth, infoItem.SpriteSheet.SubHeight));
            rect.Texture = infoItem.SpriteSheet.Texture;
            rect.TextureRect = infoItem.SpriteSheet.GetTextureRect(infoItem.SpriteI, infoItem.SpriteJ);

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
                    if (Program.Game.Player.Invertory.AddItemStack(new UIItemStack(infoItem, Count)))
                    {
                        IsDestroyed = true;
                        sound.PlaySound("grab");
                    }
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
    }
}
