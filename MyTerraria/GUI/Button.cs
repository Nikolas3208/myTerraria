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
        public event EventHandler<EventArgs> Click = (object s, EventArgs e) => { };

        public Button(string str, WidgetSize size) : base(size)
        {
            Text = str;
            
        }

        protected override void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {

            base.OnMouseMoved(sender, e);

            if (isEntered)
            {
                if (isClicked)
                {
                    NotActiveState();
                }
                else
                {
                    SelectedState();
                }
            }
            else
            {
                ActiveState();
            }
            

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
                if (isEntered)
                {
                    Click(this, EventArgs.Empty);
                }
                isClicked = false;
            }
        }

        /*protected override void SelectedState()
        {
            base.SelectedState();
        }*/

        public override void UpdateText()
        {
            text.Origin = new Vector2f(text.GetGlobalBounds().Width / 2, text.GetGlobalBounds().Height / 2);

            text.Position = new Vector2f(rect.GetGlobalBounds().Width / 2, rect.GetGlobalBounds().Height / 2.5f);
        }
    }
}
