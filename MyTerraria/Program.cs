using MyTerraria.NPC;
using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MyTerraria
{
    class Program
    {
        public static RenderWindow Window { private set; get; }
        public static Game Game { private set; get; }
        public static float Delta { private set; get; }

        public static float FPS { private set; get; }
        public static bool Phisics { get; internal set; } = true;

        public static void Main(string[] args)
        {
            //Создание окна
            Window = new RenderWindow(VideoMode.DesktopMode, "My Terraria!");
            //Window.SetVerticalSyncEnabled(true);
            //Window.SetFramerateLimit(75);
            //Window.SetMouseCursorVisible(false);

            Window.Closed += Win_Closed;
            Window.Resized += Win_Resized;
            Window.KeyPressed += Win_KeyPressed;

            float lastTime = 1;

            // Загрузка контента
            Content.Load();

            Game = new Game();      // Создаём новый объект класса игры
            Clock clock = new Clock();

            //Основной цикл программы
            while (Window.IsOpen)
            {
                Delta = clock.Restart().AsSeconds();
                FPS = 1 / Delta;
                lastTime = Delta;

                Window.SetTitle("Terraria FPS: " + FPS.ToString());

                Window.DispatchEvents();

                if (World.worldLoad)
                    Game.Update();
                    //Task.Run (() => { Game.Update(); }).Wait(10);

                if (World.worldLoad)
                    CenterScreen();

                Window.Clear(Color.Black);

                if (World.worldLoad)
                    Window.Draw(Content.ssBackgroundSky);

                Game.Draw();

                Window.Display();
            }
        }

        public static Vector2f pos2;
        public static float zoom = 1;

        public static bool debagCamera = false;
        public static bool debagDraw = false;
        public static (float, float) newPos;

        public static Vector2f pos = new Vector2f();


        public static void CenterScreen()
        {
            //Позиция игрока
            //var pos = Game.Player.Position;

            if (!debagCamera)
            {
                pos = Game.Player.Position;
                pos2 = pos;
            }
            else
                pos = pos2;


            //Получаем цент относительно персонажа
            newPos = (pos.X - Window.Size.X / 2, pos.Y - Window.Size.Y / 2);

            View view = new View(new FloatRect(newPos.Item1, newPos.Item2, Window.Size.X, Window.Size.Y));
            view.Zoom(zoom);

            //Установка центра
            //if (pos.X <= World.WORLD_WIDTH * 16 - Window.Size.X / 2) 
                Window.SetView(view);
        }

        public static Vector2i GetMousePosition()
        {
            return Mouse.GetPosition(Window);
        }

        public static Vector2i GetGlobalMousePosition()
        {
            return new Vector2i((int)(GetMousePosition().X * zoom + pos.X - Window.Size.X * zoom / 2), (int)(GetMousePosition().Y * zoom + pos.Y - Window.Size.Y * zoom / 2));
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            switch (e.Code)
            {
                case Keyboard.Key.Escape:
                    
                    break;
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
                    zoom += 0.2f;
                    break;
                case Keyboard.Key.Q:
                    zoom -= 0.2f;
                    break;
                case Keyboard.Key.C:
                    if (debagCamera)
                    {
                        debagCamera = false;
                    }
                    else
                        debagCamera = true;
                    break;
                case Keyboard.Key.X:
                    if (debagDraw)
                    {
                        debagDraw = false;
                    }
                    else
                        debagDraw = true;
                    break;
                case Keyboard.Key.P:
                    if(Phisics)
                        Phisics = false;
                    else
                        Phisics = true;
                    break;
                case Keyboard.Key.G:
                    break;

            }
        }

        private static void Win_Resized(object sender, SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
