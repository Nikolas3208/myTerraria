using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria
{
    internal class DebagInfo : Drawable
    {
        float x, y;
        Text text;
        public DebagInfo(float x1, float y1)
        {
            text = new Text("0", Content.font, 25);
            text.Color = Color.Black;
            text.Position = new Vector2f(x,y);
            text.DisplayedString = x.ToString();
            x = x1;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {

            text.Position = new Vector2f(Program.Game.Player.Position.X, Program.Game.Player.Position.Y);
            text.DisplayedString = "Player: " + "X: " + (Program.Game.Player.Position.X / 16).ToString() +  "Y: " + (Program.Game.Player.Position.X / 16).ToString() + "\n" + 
                                   "Mouse: " + "X: " + (Program.Game.Player.mousePos.X / 16).ToString() + "Y: " + (Program.Game.Player.mousePos.Y / 16).ToString();
            text.Draw(target, states);
        }
    }
}
