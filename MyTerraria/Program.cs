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
        public static Window win;

        private static int horizontalShiftPx = Tile.TILE_SIZE;

        static void Main(string[] args)
        {
            Window = new RenderWindow(new VideoMode(800, 600), "My Terraria!");
            Window.SetVerticalSyncEnabled(true);

            Window.Closed += Win_Closed;
            Window.Resized += Win_Resized;
            Window.KeyPressed += Win_KeyPressed;

            // Загрузка контента
            Content.Load();
            
            Game = new Game();      // Создаём новый объект класса игры
            Clock clock = new Clock();

            while (Window.IsOpen)
            {
                Delta = clock.Restart().AsSeconds();

                Window.DispatchEvents();
                
                Game.Update();

                Window.Clear(Color.Cyan);

                Game.Draw();

                Window.Display();
            }
        }

        private static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            View view;

            switch (e.Code)
            {
                case Keyboard.Key.D:
                    if (Game.World.XShift < Game.World.MAX_XShift)
                    {
                        view = Window.GetView();
                        view.Move(new Vector2f(horizontalShiftPx, 0));
                        Game.World.ChangeHorizontalShift(1);
                        Window.SetView(view);
                    }
                    break;
                case Keyboard.Key.A:
                    if (Game.World.XShift > Game.World.MIN_XShift)
                    {
                        view = Window.GetView();
                        view.Move(new Vector2f(-horizontalShiftPx, 0));
                        Game.World.ChangeHorizontalShift(-1);
                        Window.SetView(view);
                    }
                    break;

                case Keyboard.Key.Up:
                    view = Window.GetView();
                    view.Zoom(0.9f);
                    Window.SetView(view);
                    break;
                case Keyboard.Key.Down:
                    view = Window.GetView();
                    view.Zoom(1.1f);
                    Window.SetView(view);
                    break;
            }
        }

        private static void Win_Resized(object sender, SFML.Window.SizeEventArgs e)
        {
            Window.SetView(new View(new FloatRect(0, 0, e.Width, e.Height)));
            Game.World.SetWindowSize(Window.Size);
        }

        private static void Win_Closed(object sender, EventArgs e)
        {
            Window.Close();
        }
    }
}
