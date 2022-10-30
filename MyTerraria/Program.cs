using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;
using System.Threading;

namespace MyTerraria
{
    class Program
    {
        public static RenderWindow Window { private set; get; }

        public static Lighting Lighting { private set; get; }
        public static Game Game { private set; get; }
        public static float Delta { private set; get; }

        public static float FPS { private set; get; }

        static void Main(string[] args)
        {
            //Создание окна
            Window = new RenderWindow(new VideoMode(1920, 1080), "My Terraria!");
            Window.SetVerticalSyncEnabled(true);

            Window.Closed += Win_Closed;
            Window.Resized += Win_Resized;
            Window.KeyPressed += Win_KeyPressed;

            float lastTime = 1;

            // Загрузка контента
            Content.Load();

            Game = new Game();      // Создаём новый объект класса игры
            Clock clock = new Clock();
            Lighting = new Lighting();

            //Основной цикл программы
            while (Window.IsOpen)
            {
                Delta = clock.Restart().AsSeconds();
                FPS = 1 / (Delta);
                lastTime = Delta;

                Window.DispatchEvents();

                Game.Update();

                CenterScreen();

                Window.Clear(Color.Cyan);

                Window.Draw(Content.ssBackgroundSky);

                Game.Draw();
                //Content.bacgroundMusic.Play();

                Window.Display();
            }
        }
        public static Vector2f pos2;
        public static float scrol = 1;

        static bool a = true;

        private static void CenterScreen()
        {
            //Позиция игрока
            //var pos = Game.Player.Position;
            Vector2f pos;
            if (pos2 == null)
                pos2 = Game.Player.Position;
            if (a)
            {
                pos = pos2;
            }
            else
            {
                pos = Game.Player.Position;
                pos2 = Game.Player.Position;
            }
            //Получаем цент относительно персонажа
            var newPos = (pos.X - Window.Size.X / 2, pos.Y - Window.Size.Y / 2);

            View view = new View(new FloatRect(newPos.Item1, newPos.Item2, Window.Size.X, Window.Size.Y));
            view.Zoom(scrol);

            //Установка центра
            //if (pos.X <= World.WORLD_WIDTH * 16 - Window.Size.X / 2) 
                Window.SetView(view);
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Up:
                    pos2.Y -= 1000 * Delta;
                    break;
                case Keyboard.Key.Down:
                    pos2.Y += 1000 * Delta;
                    break;
                case Keyboard.Key.Left:
                    pos2.X += 1000 * Delta;
                    break;
                case Keyboard.Key.Right:
                    pos2.X -= 1000 * Delta;
                    break;
                case Keyboard.Key.E:
                    scrol++;
                    break;
                case Keyboard.Key.Q:
                    scrol--;
                    break;
                case Keyboard.Key.C:
                    if (a)
                    {
                        a = false;
                    }
                    else
                        a = true;
                    break;
            }
        }

        private static void Win_Resized(object sender, SFML.Window.SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
