using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria
{
    public class Trees
    {
        Tile downTile;
        //int i = 0;
        //int j = 0;
        public void Update()
        {
            //int i = (int)(Program.Game.Player.mousePos.X + (Program.Game.Player.Position.X - Program.Window.Size.X / 2)) / Tile.TILE_SIZE;
            //int j = (int)(Program.Game.Player.mousePos.Y + (Program.Game.Player.Position.Y - Program.Window.Size.Y / 2)) / Tile.TILE_SIZE;
            for (int a = 0; a < 17; a++)
            {
                for (int i = 0; i < World.WORLD_WIDTH; i++)
                {
                    for (int j = 0; j < Program.Game.World.BackgroundMin; j++)
                    {
                        if (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type == TileType.TREEBRAK)
                        {
                            downTile = Program.Game.World.GetTile(i, j + 1);
                            if (downTile == null)
                            {
                                Program.Game.World.DelTile(TileType.TREEBRAK, i, j);
                            }
                        }
                        if (Program.Game.World.GetTile(i, j) != null && Program.Game.World.GetTile(i, j).type == TileType.TREETOPS)
                        {
                            downTile = Program.Game.World.GetTile(i, j + 1);
                            if (downTile == null)
                            {
                                Program.Game.World.DelTile(TileType.TREETOPS, i, j);
                            }
                        }
                    }
                }
            }
        }
    }
}
