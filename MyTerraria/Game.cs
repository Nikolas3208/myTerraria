using MyTerraria.GUI;
using MyTerraria.GUI.Menu;
using MyTerraria.NPC;
using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyTerraria
{
    class Game   
    {
        public Player Player { get; set; }  // Игрок
        public World World { get; private set; } // Мир

        private MainMenu menu;

        public List<Npc> Npc = new List<Npc>(); // Слизни

        private Stopwatch worldSaveTimer = new Stopwatch(); //Авто сохранение мира

        private string StandartWorldName = "World";
        private string WorldName = "World";
        private int i = 1;
        //Клас игры
        public Game()
        {
            menu = new MainMenu();
        }
        public void Init()
        {
            // Создаём игрока
            //CreatePlayer();

            //Влючаем или отключаем отображеие прямоугольников)
            NPC.Npc.debag = false;

            // Создаём быстрого слизня
            Npc.Add(new NpcFastSlime(World));

            //Обычный слизень
            for (int i = 0; i < 10; i++)
                Npc.Add(new NpcSlime(World));

            //UI инвентарь
            UIManager.AddControl(Player.Invertory);

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = true;
        }

        public void CreateWorld()
        {
            World = new World();

            World.GenerateWorldAsync(WorldName);

            Init();
        }

        public void LoadWorldAsync()
        {
            World = new World();

            World.LoadWorld("Worlds\\" + WorldName + ".world");

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

            DebugRender.AddText(Player.health.ToString() , Player.GetGlobalPosition().X + Program.Window.Size.X - 65f, Player.GetGlobalPosition().Y, Content.font);

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
