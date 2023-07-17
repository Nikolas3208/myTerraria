using SFML.Graphics;
using SFML.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria.UI
{
    class UIHealthCell : UIBase
    {
        public UIHealthCell() 
        {
            rectShape = new RectangleShape((Vector2f)Content.itemTextureList[29].Size);
            rectShape.Texture = Content.itemTextureList[29];
        }
    }
}
