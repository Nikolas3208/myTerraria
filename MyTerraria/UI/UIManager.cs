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
            /*for(int i = 0; i <= UIInvertory.cells.Count; i++)
            {
                if (UIInvertory.cells[i].ItemStack != null && UIInvertory.cells[i].ItemStack.InfoItem.Tooltype != ToolType.None)
                {
                    //UIInvertory.cells[i].ItemStack.ItemCountMax = 1;
                }
            }*/

            if (Drag != null)
            {
                Drag.Position = new SFML.System.Vector2i((int)Program.Game.Player.GetGlobalPosition().X, (int)Program.Game.Player.GetGlobalPosition().Y);

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
