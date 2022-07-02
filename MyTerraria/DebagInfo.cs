using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria
{
    internal class DebagInfo : UI.UIBase, Drawable
    {
        float x, y;
        Text text;
        public DebagInfo()
        {
            text = new Text("0", Content.font, 25);
            text.Color = Color.Black;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            text.Position = new Vector2f(Program.Game.Player.Position.X - Program.Window.Size.X / 2, 50 + Program.Game.Player.Position.Y - Program.Window.Size.Y / 2);
            //text.Position = new Vector2f(Program.Game.Player.Position.X, Program.Game.Player.Position.Y);
            text.DisplayedString = "Player: " + "X: " + (Program.Game.Player.Position.X / 16).ToString() + "Y: " + (Program.Game.Player.Position.Y / 16).ToString() + "\n" +
                                   "Mouse: " + "X: " + (Program.Game.Player.mousePos.X / 16).ToString() + "Y: " + (Program.Game.Player.mousePos.Y / 16).ToString() + "\n" +
                                   "Block Type: " + (Program.Game.Player.block_Type).ToString() + "\n" +
                                   "Path to mouse " + (Program.Game.Player.a);
            
            text.Draw(target, states);
        }
    }
}
