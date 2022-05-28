using SFML.Window;
using System;
using System.Collections.Generic;

namespace MyTerraria.UI
{
    class UIManager
    {
        public static UIBase Over, Drag;

        static List<UIBase> controls = new List<UIBase>();

        public static void AddControl(UIBase c)
        {
            controls.Add(c);
        }

        public static void UpdateOver()
        {
            Over = null;

            var mousePos = Mouse.GetPosition(Program.Window);

            foreach (var c in controls)
                c.UpdateOver(mousePos);
        }

        public static void Update()
        { 
            if (Drag != null)
            {
                Drag.Position = new SFML.System.Vector2i((int)(Program.Game.Player.Position.X - Program.Window.Size.X / 2), (int)(Program.Game.Player.Position.Y / Program.Window.Size.Y / 2));
                //Drag.Position = Mouse.GetPosition(Program.Window);
                /*if (Mouse.IsButtonPressed(Mouse.Button.Left))
                {
                    var mousePosLocal = Mouse.GetPosition(Program.Window);
                    Drag.Position = mousePosLocal - Drag.DragOffset;
                }
                else
                {*/
                if (Over != null)
                        Over.OnDrop(Drag);
                    else
                        Drag.OnCancelDrag();

                    Drag = null;
               // }
            }

            foreach (var c in controls)
                c.Update();
        }

        public static void Draw()
        {
            var win = Program.Window;

            foreach (var c in controls)
                if (c != Drag)
                    win.Draw(c);

            if (Drag != null)
                win.Draw(Drag);
        }
    }
}
