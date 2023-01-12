using MyTerraria.GUI;
using MyTerraria.GUI.Menu;
using MyTerraria.NPC;
using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyTerraria
{
    class Game   
    {
        public Player Player { get; private set; }  // Игрок
        public World World { get; private set; } // Мир

        private MainMenu menu;

        private List<Npc> Npc = new List<Npc>(); // Слизни

        private Stopwatch worldSaveTimer = new Stopwatch(); //Авто сохранение мира

        private string WorldName = "World";

        //Клас игры
        public Game()
        {
            menu = new MainMenu();
        }
        public void Init()
        {
            // Создаём игрока
            CreatePlayer();

            //Влючаем или отключаем отображеие прямоугольников)
            NPC.Npc.debag = false;

            // Создаём быстрого слизня
            Npc.Add(new NpcFastSlime(World));
            //Обычный слизень
            Npc.Add(new NpcSlime(World));
            //Летаюший глаз
            Npc.Add(new NpcFlyingEye(World));

            //UI инвентарь
            UIManager.AddControl(Player.Invertory);

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = true;
        }

        private void CreatePlayer()
        {
            Player = new Player(World);
            Player.StartPosition = new Vector2f((World.WORLD_WIDTH / 2) * 16, 0);
            Player.Spawn();
            Player.AddTools();

            Player.Invertory = new UIInvertory();
        }

        public void CreateWorld()
        {
            World = new World();

            World.GenerateWorld(WorldName);

            LoadWorld();
        }

        public void LoadWorld()
        {
            if (World == null)
                World = new World();

            World.LoadWorld(WorldName);

            Init();
        }

        // Обновление логики игры
        public void Update()
        {
            worldSaveTimer.Start();

            Player.Update();

            World.Update();

            foreach (var s in Npc)
                s.Update();

            UIManager.UpdateOver();
            UIManager.Update();

            worldSaveTimer.Stop();

            TimerUpdate();
        }

        private void TimerUpdate()
        {
            if (worldSaveTimer.Elapsed.TotalMinutes > 0 && !World.worldSave)
            {
                World.worldSave = true;
                worldSaveTimer.Restart();
            }
        }

        // Прорисовка игры
        public void Draw()
        {
            if (!World.worldLoad)
                MainMenu.Draw(Program.Window);

            if (World.worldLoad)
            {
                Program.Window.Draw(World);
                Program.Window.Draw(Player);

                foreach (var s in Npc)
                    Program.Window.Draw(s);

                DebugRender.Draw(Program.Window);

                UIManager.Draw();
            }
        }
    }
}
