﻿using SFML.System;
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
                Drag.Position = new Vector2i((int)Program.Game.Player.GetGlobalPosition().X, (int)Program.Game.Player.GetGlobalPosition().Y);

                if (Over != null)
                        Over.OnDrop(Drag);
                    else
                        Drag.OnCancelDrag();

                    Drag = null;
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
