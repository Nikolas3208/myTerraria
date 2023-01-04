using MyTerraria.Items;
using MyTerraria.NPC;
using MyTerraria.UI;
using SFML.Graphics;
using SFML.System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MyTerraria
{
    class Game   
    {
        public Player Player { get; private set; }  // Игрок
        public World World { get; private set; } // Мир

        List<Npc> Npc = new List<Npc>(); // Слизни

        public DebagInfo debag; // Дебаг информация

        Stopwatch stopwatch1 = new Stopwatch(); // Счетчик затрат времени

        public Game()
        {
            // Создаём новый мир и выполняем его генерацию
            World = new World();
            Task.Run(() => World.GenerateWorld());

            // Создаём игрока
            Player = new Player(World);
            Player.Spawn();
            Player.AddTools();

            //Персонаж
            Npc.Add(Player);

            // Создаём быстрого слизня
            Npc.Add(new NpcFastSlime(World));
            //Обычный слизень
            Npc.Add(new NpcSlime(World));
            //Летаюший глаз
            Npc.Add(new NpcFlyingEye(World));

            Player.Invertory = new UI.UIInvertory();
            UIManager.AddControl(Player.Invertory);

            //DebagInfo
            debag = new DebagInfo(Content.font);

            // Включаем прорисовку объектов для визуальной отладки
            DebugRender.Enabled = false;
        }


        // Обновление логики игры
        public void Update()
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Player.Update();


                World.Update();

                foreach (var s in Npc)
                    s.Update();

            UIManager.UpdateOver();
            UIManager.Update();

            stopwatch.Stop();

            debag.SetPosition(Player.GetGlobalPosition());

            //DebageInfo
            debag.ClearText();
            debag.SetMessageLine(Program.FPS.ToString());
            //debag.SetMessageLine((Program.GetGlobalMousePosition() / 16).ToString());
            //debag.SetMessageLine((Player.GetGlobalPosition() / 16).ToString());
            /*if (World.GetTileByWorldPos(Program.GetGlobalMousePosition()) != null)
                debag.SetMessageLine(World.GetTileByWorldPos(Program.GetGlobalMousePosition()).type.ToString());*/

            debag.SetMessageLine("Phisics time: " + stopwatch.Elapsed.ToString());
            //debag.SetMessageLine("Render time on ms: " + stopwatch1.Elapsed.Milliseconds.ToString());

        }

        // Прорисовка игры
        public void Draw()
        {
            stopwatch1.Restart();
            stopwatch1.Start();

            Program.Window.Draw(World);
            //Program.Window.Draw(Player);

            foreach (var s in Npc)
                Program.Window.Draw(s);

            DebugRender.Draw(Program.Window);

            Program.Window.Draw(debag);

            UIManager.Draw();

            stopwatch1.Stop();

        }
    }
}
