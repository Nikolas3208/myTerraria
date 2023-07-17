using System;
using System.Collections.Generic;
using System.Threading;
using MyTerraria.Items;
using MyTerraria.Items.ItemTile;
using MyTerraria.Items.ItemTool;
using MyTerraria.Worlds;
using SFML.Audio;

namespace MyTerraria
{
    public class InfoItem
    {
        public static ItemBase ItemTile(TileType type)
        {
            switch (type)
            {
                case TileType.Ground: return new ItemTile(type, 2, 99);
                case TileType.Stone: return new ItemTile(type, 3, 99);
                case TileType.IronOre: return new ItemTile(type, 11, 99);
                case TileType.CoperOre: return new ItemTile(type, 12, 99);
                case TileType.GoldOre: return new ItemTile(type, 13, 99);
                case TileType.SilverOre: return new ItemTile(type, 14, 99);
            }

            return null;
        }

        public static ItemBase ItemTool(ItemToolType type)
        {
            switch (type)
            {
                case ItemToolType.CoperAxe: return new ItemAxe(type, 10, 1);
                case ItemToolType.CoperPick: return new ItemPick(type, 1, 1);
                case ItemToolType.IronAxe: return new ItemAxe(type, 10, 1);
                case ItemToolType.IronPick: return new ItemPick(type, 1, 1);
                case ItemToolType.SilverAxe: return new ItemAxe(type, 10, 1);
                case ItemToolType.SilverPick: return new ItemPick(type, 1, 1);
                case ItemToolType.GoldAxe: return new ItemAxe(type, 10, 1);
                case ItemToolType.GoldPick: return new ItemPick(type, 1, 1);
            }
            return null;
        }
    }
}
