using SFML.Graphics;
using SFML.System;
using SFML.Window; 
using System;

namespace MyTerraria
{
    class Program
    {
        public static RenderWindow Window { private set; get; }
        public static Game Game { private set; get; }
        public static float Delta { private set; get; }

        static void Main(string[] args)
        {
            //Создание окна
            Window = new RenderWindow(new VideoMode(1920, 1080), "My Terraria!");
            Window.SetVerticalSyncEnabled(true);

            Window.Closed += Win_Closed;
            Window.Resized += Win_Resized;
            Window.KeyPressed += Win_KeyPressed;

            // Загрузка контента
            Content.Load();
            
            Game = new Game();      // Создаём новый объект класса игры
            Clock clock = new Clock();

            //Основной цикл программы
            while (Window.IsOpen)
            {
                Delta = clock.Restart().AsSeconds();

                Window.DispatchEvents();
                
                Game.Update();

                CenterScreen();

                Window.Clear(Color.Cyan);

                Game.Draw();

                Window.Display();
            }
        }
        public static Vector2f pos = new Vector2f();

        private static void CenterScreen()
        {
            //Позиция игрока
            var pos = Game.Player.Position;
            //Получаем цент относительно персонажа
            var newPos = (pos.X - Window.Size.X / 2, pos.Y - Window.Size.Y / 2);
            //Установка центра
            Window.SetView(new View(new FloatRect(newPos.Item1, newPos.Item2, Window.Size.X, Window.Size.Y)));
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            View view;

            switch (e.Code)
            {

                case Keyboard.Key.Num1:
                    Game.Player.block_Type = "GROUND";
                    break;
                case Keyboard.Key.Num2:
                    Game.Player.block_Type = "GRASS";
                    break;
                case Keyboard.Key.Num3:
                    Game.Player.block_Type = "STONE";
                    break;
                case Keyboard.Key.Num4:
                    Game.Player.block_Type = "DESK";
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
