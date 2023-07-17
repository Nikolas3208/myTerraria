using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.UI
{
    class UIHealth : UIWindow
    {
        public int HealthMax { get; set; }
        public int OneCellHealthMax { get; set; }

        private List<UIHealthCell> cells;

        public UIHealth(int healthMax, int oneCellHealthMax)
        {
            cells = new List<UIHealthCell>();

            IsVisibleTitleBar = false;
            BodyColor = Color.Transparent;
            HealthMax = healthMax;
            OneCellHealthMax = oneCellHealthMax;

            int length = HealthMax / OneCellHealthMax;

            Size = new Vector2i((int)Content.itemTextureList[29].Size.X * length, (int)Content.itemTextureList[29].Size.Y);

            for (int i = 0; i < length; i++)
            {
                UIHealthCell uIHealthCell = new UIHealthCell();
                uIHealthCell.Position = new Vector2i((int)Program.Window.Size.X - cells.Count * uIHealthCell.Width - (uIHealthCell.Width * 2), uIHealthCell.Height);
                cells.Add(uIHealthCell);
                Childs.Add(uIHealthCell);
            }
        }

        public void Update()
        {
            Position = new Vector2i((int)Program.Game.Player.GetGlobalPosition().X, (int)Program.Game.Player.GetGlobalPosition().Y);
        }
    }
}
