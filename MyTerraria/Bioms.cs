using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTerraria
{
    public class Bioms
    {
        public static Bioms plainBiom = new Bioms().SetBiom(TileType.GROUND, "Plane");

        public TileType BasicTileType { get; set; }
        public string BiomName { get; set; }

        public Bioms SetBiom(TileType basicTileType, string name)
        {
            return this;
        }
    }
}
