using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.GUI
{
    public class Button : BaseGUI
    {
        static EventArgs args = new EventArgs();
        static object s;

        public event EventHandler<EventArgs> Click = (object s, EventArgs e) => { };

        public Button(string str) : base()
        {
            Text = str;
            
        }

        protected override void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            base.OnMouseMoved(sender, e);
        }

        protected override void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (isEntered)
                {
                    isClicked = true;
                }
            }
        }

        protected override void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
        {
            if (e.Button == Mouse.Button.Left)
            {
                if (isEntered && isClicked)
                {
                    Click(this, EventArgs.Empty);
                }
                isClicked = false;
            }
        }

        public override void UpdateText()
        {
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);

            text.Position = new Vector2f(rect.GetGlobalBounds().Width / 2, rect.GetGlobalBounds().Height / 2.5f);
        }
    }
}
