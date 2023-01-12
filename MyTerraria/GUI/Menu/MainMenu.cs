using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.GUI.Menu
{
    public class MainMenu : Transformable
    {
        private static List<Button> StartMenu = new List<Button>();
        private static Sprite Buckground = new Sprite(Content.ssBackgroundMenu);
        private static Button buttCreateWorld;
        private static Button buttLoadWorld;
        private static Button buttExit;

        public MainMenu() 
        {
            //Buckground.Origin = new Vector2f(Program.Window.Size.X / 2, Program.Window.Size.Y / 2);
            Buckground.Scale = new Vector2f(1.5f, 1.5f);

            buttCreateWorld = new Button("Создать мир", BaseGUI.WidgetSize.Small);
            buttCreateWorld.Color = Color.White;
            buttCreateWorld.Position = new Vector2f(Program.Window.Size.X / 2, Program.Window.Size.Y / 2);
            buttCreateWorld.Subscribe();
            buttCreateWorld.Click += ButtCreateWorld_Click;

            StartMenu.Add(buttCreateWorld);

            buttLoadWorld = new Button("Загрузить мир", BaseGUI.WidgetSize.Small);
            buttLoadWorld.Color = Color.White;
            buttLoadWorld.Position = new Vector2f(Program.Window.Size.X / 2, Program.Window.Size.Y / 2 + 70);
            buttLoadWorld.Subscribe();
            buttLoadWorld.Click += ButtLoadWorld_Click;

            StartMenu.Add(buttLoadWorld);

            buttExit = new Button("Выход", BaseGUI.WidgetSize.Narrow);
            buttExit.Color = Color.White;
            buttExit.Position = new Vector2f(Program.Window.Size.X / 2 + (192 / 4), Program.Window.Size.Y / 2 + 140);
            buttExit.Subscribe();
            buttExit.Click += ButtExit_Click;

            StartMenu.Add(buttExit);
        }

        private void ButtExit_Click(object sender, EventArgs e)
        {
            if (!World.worldLoad)
            {
                //Program.Game.World.SaveWorld();

                Program.Window.Close();
            }
        }

        private void ButtLoadWorld_Click(object sender, EventArgs e)
        {
            if (!World.worldLoad)
                Program.Game.LoadWorld();
        }

        private void ButtCreateWorld_Click(object sender, EventArgs e)
        {
            if (!World.worldLoad)
                Program.Game.CreateWorld();
        }

        public static void Draw(RenderTarget target)
        {
            target.Draw(Buckground);

            foreach (var gui in StartMenu)
                target.Draw(gui);
        }
    }
}
