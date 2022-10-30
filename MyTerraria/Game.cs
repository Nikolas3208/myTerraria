using MyTerraria.Items;
using MyTerraria.NPC;
using MyTerraria.UI;
using SFML.System;
using System.Collections.Generic;

namespace MyTerraria
{
    class Game
    {
        public Player Player { get; private set; }  // Игрок

        World world;    // Мир
        Trees trees;
        NpcFastSlime slime; // Слизень
        
        DebagInfo debag;

        // Слизни
        List<NpcSlime> slimes = new List<NpcSlime>();

        public World World { get { return world; } }
        public Trees Trees { get { return trees; } }

        public int c = 0;

        public Game()
        {
            // Создаём новый мир и выполняем его генерацию
            world = new World();
            world.GenerateWorld();

            trees = new Trees();

            // Создаём игрока
            Player = new Player(world);
            Player.Invertory = new UIInvertory();
            UIManager.AddControl(Player.Invertory);
            for (int j = World.WORLD_HEIGHT; j > 0; j--)
            {
                if (World.GetTile(World.WORLD_WIDTH / 2, j) != null && World.GetTile(World.WORLD_WIDTH / 2, j).type == TileType.GRASS)
                {
                    c = -j + 1;
                    Player.StartPosition = new Vector2f(World.WORLD_WIDTH / 2 * 16, (-1 - c) * 16);
                }
            }
            Player.Spawn();
            var itemTile = new ItemTile(World, InfoItem.ItemSword);
            itemTile.Position = Player.Position;
            World.items.Add(itemTile);
            itemTile = new ItemTile(World, InfoItem.ItemPick);
            itemTile.Position = Player.Position;
            World.items.Add(itemTile);
            itemTile = new ItemTile(World, InfoItem.ItemAxe);
            itemTile.Position = Player.Position;
            World.items.Add(itemTile);
            for (int i = 0; i < 99; i++)
            {
                itemTile = new ItemTile(World, InfoItem.ItemGround);
                itemTile.Position = Player.Position;
                World.items.Add(itemTile);
            }

            // Создаём быстрого слизня
            slime = new NpcFastSlime(world);
            slime.StartPosition = new Vector2f(500, 150);
            slime.Spawn();

            debag = new DebagInfo();

            for (int i = 0; i < 12; i++)
            {
                var s = new NpcSlime(world);
                s.StartPosition = new Vector2f(Player.Position.X, Player.Position.Y);              
                s.Direction = World.Rand.Next(0, 2) == 0 ? 1 : -1;
                s.Spawn();
                slimes.Add(s);
            }

            // Создаём UI
            //UIManager.AddControl(new UIWindow());

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = false;
        }

        // Обновление логики игры
        public void Update()
        {
            world.Update();
            //trees.Update();

            Player.Update();
            slime.Update();

            foreach (var s in slimes)
                s.Update();

            // Обновляем UI
            UIManager.UpdateOver();
            UIManager.Update();
        }

        // Прорисовка игры
        public void Draw()
        {
            Program.Window.Draw(world);
            Program.Window.Draw(Player);
            Program.Window.Draw(slime);
            Program.Window.Draw(debag);

            foreach (var s in slimes)
                Program.Window.Draw(s);

            DebugRender.Draw(Program.Window);

            // Рисуем UI
            UIManager.Draw();
        }
    }
}
