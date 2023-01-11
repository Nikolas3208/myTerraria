using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.GUI
{
    public abstract class BaseGUI : Transformable, Drawable
    {
        protected Vector2f localMousePos;
        protected Vector2f gloabalMousePos;
        protected RectangleShape rect;
        protected Text text;
        protected bool isClicked;
        protected bool isEntered;

        public bool isActive { get; private set; }
        public Color ActiveColor { get; set; } = Color.White;
        public virtual Color Color
        {
            get => rect.FillColor;
            set => rect.FillColor = value;
            
        }

        public virtual string Text
        {
            get { return text.DisplayedString; }
            set { text.DisplayedString = value; UpdateText(); }
        }

        public Vector2f Size
        {
            get => rect.Size;
            private set => rect.Size = value;
        }

        public BaseGUI()
        {
            rect = new RectangleShape()
            {
                Size = new Vector2f(88, 40),
                FillColor = ActiveColor,
                OutlineColor = Color.Black,
                OutlineThickness = -2
            };

            text = new Text()
            {
                CharacterSize = 25,
                FillColor = Color.White,
                Font = Content.font
            };
        }

        public void Subscribe()
        {
            Program.Window.MouseMoved += OnMouseMoved;
            Program.Window.MouseButtonPressed += OnMouseButtonPressed;
            Program.Window.MouseButtonReleased += OnMouseButtonReleased;

        }

        protected virtual void OnMouseMoved(object sender, MouseMoveEventArgs e)
        {
            gloabalMousePos = new Vector2f(e.X, e.Y);
            isEntered = rect.GetGlobalBounds().Contains(localMousePos.X, localMousePos.Y);
        }

        protected virtual void OnMouseButtonPressed(object sender, MouseButtonEventArgs e)
		{
		}
		protected virtual void OnMouseButtonReleased(object sender, MouseButtonEventArgs e)
		{
		}

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;

            //target.Draw(rect, states);
            target.Draw(text, states);
        }

        public abstract void UpdateText();
    }
}
