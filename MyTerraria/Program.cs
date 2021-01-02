using SFML.Graphics;
using SFML.System;
using SFML.Window;
using MyTerraria.NPC;
using MyTerraria.UI;
using System;

namespace MyTerraria
{
    class Program : Transformable
    {
        public static RenderWindow Window { private set; get; }
        public static Game Game { private set; get; }
        public static float Delta { private set; get; }

        public static int horizontalShiftPx = Tile.TILE_SIZE;

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

        public static void Win_KeyPressed(object sender, KeyEventArgs e)
        {
            View view;

            switch (e.Code)
            {
                case Keyboard.Key.D:
                    if (Game.World.XShift <= Game.World.MAX_XShift - 51)
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
