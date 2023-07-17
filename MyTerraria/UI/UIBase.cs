using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Collections.Generic;

namespace MyTerraria.UI
{
    abstract class UIBase : Transformable, Drawable
    {
        public UIBase OldParent = null;                     // Предыдущий родитель до перетаскивания элемента
        public UIBase Parent = null;                        // Родитель
        public List<UIBase> Childs = new List<UIBase>();    // Дети

        public bool visible = true;

        public new Vector2i Position
        {
            get { return (Vector2i)base.Position; }
            set { base.Position = (Vector2f)value; }
        }
        public new Vector2i Origin
        {
            get { return (Vector2i)base.Origin; }
            set { base.Origin = (Vector2f)value; }
        }
        public Vector2i GlobalPosition
        {
            get
            {
                if (Parent == null)
                    return Position;
                else
                    return Parent.GlobalPosition;
            }
        }
        public Vector2i GlobalOrigin
        {
            get
            {
                if (Parent == null)
                    return Origin;
                else
                    return Parent.GlobalOrigin;
            }
        }

        public int Width
        {
            get { return (int)rectShape.Size.X; }
            set { rectShape.Size = new Vector2f(value, rectShape.Size.Y); }
        }
        public int Height
        {
            get { return (int)rectShape.Size.Y; }
            set { rectShape.Size = new Vector2f(rectShape.Size.X, value); }
        }
        public Vector2i Size
        {
            get { return (Vector2i)rectShape.Size; }
            set { rectShape.Size = (Vector2f)value; }
        }

        public Vector2i DragOffset { get; private set; }

        protected RectangleShape rectShape;

        public virtual void UpdateOver(Vector2i mousePos)
        {
            UIManager.Drag = this;
            var localMousePos = mousePos - (GlobalPosition + GlobalOrigin);// + Program.Game.Player.Position - Program.Window.Size;
            localMousePos += new Vector2i((int)(Program.Game.Player.Position.X - Program.Window.Size.X / 2), (int)(Program.Game.Player.Position.Y - Program.Window.Size.Y / 2));
        }

        public virtual void Update()
        {
            foreach (var c in Childs)
                c.Update();
        }

        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            if (visible)
                target.Draw(rectShape, states);

            foreach (var c in Childs)
                if (c != UIManager.Drag)
                    target.Draw(c, states);
        }

        // Когда текущий элемент начинают тащить мышкой
        public virtual void OnDragBegin()
        {
            OldParent = Parent;

            if (Parent != null)
                Parent.Childs.Remove(this);

            Parent = null;
        }

        // Когда на текущий элемент сбрасывают другой
        public virtual void OnDrop(UIBase ui)
        {
        }

        // Когда невозможно сбросить текущий элемент куда то и нужно отменить все изменения
        public virtual void OnCancelDrag()
        {
            if (OldParent != null)
                OldParent.Childs.Add(this);

            Parent = OldParent;
            Position = new Vector2i();
        }
    }
}
