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
        NpcFastSlime slime; // Слизень
        
        DebagInfo debag;

        // Слизни
        List<NpcSlime> slimes = new List<NpcSlime>();

        public World World { get { return world; } }

        public Game()
        {
            // Создаём новый мир и выполняем его генерацию
            world = new World();
            world.GenerateWorld();

            // Создаём игрока
            Player = new Player(world);
            Player.StartPosition = new Vector2f(1500, 50);
            Player.Spawn();

            // Создаём быстрого слизня
            slime = new NpcFastSlime(world);
            slime.StartPosition = new Vector2f(500, 150);
            slime.Spawn();

            debag = new DebagInfo();

            for (int i = 0; i < 5; i++)
            {
                var s = new NpcSlime(world);
                s.StartPosition = new Vector2f(World.Rand.Next(150, 600), 150);
                s.Direction = World.Rand.Next(0, 2) == 0 ? 1 : -1;
                s.Spawn();
                slimes.Add(s);
            }

            // Создаём UI
            Player.Invertory = new UIInvertory();
            UIManager.AddControl(Player.Invertory);
            //UIManager.AddControl(new UIWindow());

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = true;
        }

        // Обновление логики игры
        public void Update()
        {
            world.Update();

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
