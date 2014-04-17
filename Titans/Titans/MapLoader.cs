using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna;
using Microsoft.Xna.Framework.GamerServices;

namespace Titans
{
    public static class MapLoader
    {
        //MAP FILE: A PRIMER
        //Map files are plaintext files encoded with data from which we can load maps
        //The format for the map file is as follows (omit the <> when writing):
        //[Start of File]
        //<x size>
        //<y size>
        //<tile set number>
        //<music number>
        //<x coordinate>,<y coordinate>,<height>,<unit number (see below)>,<player number[0 or 1]>,<impassible[0 or 1]>,<move cost>,<type>
        //[repeat for each tile]
        //[end of file]
        public static Map LoadMap(string filename)
        {
            StreamReader sr = new StreamReader(filename);
            string file = sr.ReadToEnd();
            string[] lines = file.Split(new string[]{Environment.NewLine}, StringSplitOptions.RemoveEmptyEntries);

            int sizeX = Int32.Parse(lines[0]);
            int sizeY = Int32.Parse(lines[1]);
            int tileSet = Int32.Parse(lines[2]);
            int music = Int32.Parse(lines[3]);
            int linecount = 4;
            Map finalMap = new Map(new int[]{sizeX, sizeY}, tileSet, music);
            for (int i = 4; i < lines.Length; i++)
            {
                linecount++;
                Tile newTile = new Tile();

                string[] tileData = lines[i].Split(',');

                int X = Int32.Parse(tileData[0]);
                int y = Int32.Parse(tileData[1]);
                int height = Int32.Parse(tileData[2]);
                int unit = Int32.Parse(tileData[3]);
                int PlayerUnit = Int32.Parse(tileData[4]);
                int impassible = Int32.Parse(tileData[5]);

                    int moveCost = Int32.Parse(tileData[6]);
                    newTile.MoveCost = moveCost;

                    string type = tileData[7];
                    newTile.type = type;

                newTile.X = X;
                newTile.Y = y;
                newTile.Height = height;

                if (impassible == 0)
                {
                    newTile.IsImpassible = false;
                }
                else
                {
                    newTile.IsImpassible = true;
                }

                
                
                Unit tileUnit;
                newTile.hasUnit = true;
                //the following is a list of unit codes, use -1 for no unit
                switch (unit)
                {
                    case 0: tileUnit = new Soldier(); break;
                    case 1: tileUnit = new Defender(); break;
                    case 2: tileUnit = new Cavalry(); break;
                    case 3: tileUnit = new Ranger(); break;
                    case 4: tileUnit = new Mage(); break;
                    case 5: tileUnit = new Artillery(); break;
                    case 6: tileUnit = new Scout(); break;
                    case 7: tileUnit = new Bomber(); break;
                    case 8: tileUnit = new Fighter(); break;
                    default: tileUnit = null; newTile.hasUnit = false; break;
                }

                if (newTile.hasUnit)
                {
                    newTile.IsImpassible = true;
                    if (PlayerUnit == 0)
                    {
                        tileUnit.isPlayerUnit = true;
                        
                    }
                    else
                    {
                        tileUnit.isPlayerUnit = false;
                    }
                    newTile.TileUnit = tileUnit;
                    newTile.IsImpassible = true;

                    tileUnit.Location[0] = newTile.X;
                    tileUnit.Location[1] = newTile.Y;
                }
                else
                {
                    newTile.TileUnit = null;
                }
                newTile.AssignFileName();

                finalMap.map[X][y] = newTile;

                if (newTile.type == "water")
                {
                    newTile.IsImpassible = true;
                }

            }
            sr.Close();
            return finalMap;
        }
    }
}
