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
        public enum WidgetSize
        {
            Narrow = 0,
            Small,
            Wide
        };

        protected Vector2f localMousePos;
        protected Vector2f gloabalMousePos;
        protected RectangleShape rect;
        protected Text text;
        protected bool isClicked;
        protected bool isEntered;

        protected readonly float[] dimensions = new float[]
        {
            88f,
            192f,
            400f
        };
        protected bool isDrawRect = false;

        public bool isActive { get; set; }
        public IntRect NotActiveRect { get; set; } = new IntRect(0, 0, 200, 20);
        public IntRect ActiveRect { get; set; } = new IntRect(0, 20, 200, 20);
        public IntRect SelectedRect { get; set; } = new IntRect(0, 40, 200, 20);
        public Color NotActiveColor { get; set; } = new Color(170, 170, 170);
        public Color ActiveColor { get; set; } = Color.White;
        public Color SelectedColor { get; set; } = new Color(45, 107, 236);
        public Color NotActiveTextColor { get; set; } = Color.White;
        public Color ActiveTextColor { get; set; } = Color.White;
        public Color SelectedTextColor { get; set; } = new Color(255, 255, 130);
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

        public BaseGUI(WidgetSize size)
        {
            localMousePos = new Vector2f(-1, 0);

            rect = new RectangleShape()
            {
                Size = new Vector2f(dimensions[(int)size], 40),
                FillColor = ActiveColor,
                OutlineColor = Color.Black,
                OutlineThickness = -2
            };

            text = new Text()
            {
                CharacterSize = 35,
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

        protected virtual void NotActiveState()
        {
            text.FillColor = NotActiveTextColor;
            if (rect.Texture != null)
            {
                rect.TextureRect = NotActiveRect;
            }
            else
            {
                rect.FillColor = NotActiveColor;
            }
        }
        protected virtual void ActiveState()
        {
            text.FillColor = ActiveTextColor;
            text.CharacterSize = 35;
            UpdateText();
            if (rect.Texture != null)
            {
                rect.TextureRect = ActiveRect;
            }
            else
            {
                rect.FillColor = ActiveColor;
            }
        }
        protected virtual void SelectedState()
        {
            text.FillColor = SelectedTextColor;
            text.CharacterSize = 55;
            UpdateText();
            if (rect.Texture != null)
            {
                rect.TextureRect = SelectedRect;
            }
            else
            {
                rect.FillColor = SelectedColor;
            }
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            localMousePos = states.Transform.GetInverse().TransformPoint(gloabalMousePos);

            if(isDrawRect)
            target.Draw(rect, states);
            target.Draw(text, states);
        }

        public abstract void UpdateText();
    }
}
