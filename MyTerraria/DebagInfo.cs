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
    internal class DebagInfo : Transformable, Drawable
    {
        private Text text;

        public DebagInfo(Font font)
        {
            text = new Text();
            text.Font = font;
            text.FillColor = Color.Black;
        }

        public void ClearText()
        {
            text.DisplayedString = null;
        }

        public void SetMessage(string mess)
        {
            text.DisplayedString += mess;
        }

        public void SetMessageLine(string mess)
        {
            text.DisplayedString += "\n" + mess;
        }

        public void SetPosition(Vector2f pos)
        {
            text.Position = pos;
        }

        public void TextSettings(Vector2f scale, Color color)
        {
            text.Scale = scale;
            text.FillColor = color;
        }
        
        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            text.Draw(target, states);
        }
    }
}
