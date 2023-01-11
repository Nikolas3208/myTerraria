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
        private static List<BaseGUI> GUI = new List<BaseGUI>();

        public MainMenu() 
        {
        }

        public static void AddButton(Color color, Vector2f pos, string str)
        {
            Button button = new Button(str);
            button.Color = color;
            button.Position = pos;
            button.Text = str;
            button.Subscribe();
            button.Click += Button_Click;

            GUI.Add(button);
        }

        private static void Button_Click(object sender, EventArgs e)
        {
            if (!World.worldGen)
                Program.Game.Init();
        }

        public static void Draw(RenderTarget target)
        {
            foreach (var item in GUI)
                target.Draw(item);

            GUI.Clear();
        }
    }
}
