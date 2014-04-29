using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace Titans
{
    public static class MapLoaderUltimate
    {
        

        /// <summary>
        /// THE NEW MAP FORMAT
        /// Start with a Bitmap. Each pixel represents one tile. The color of the tile determines the type.
        /// Key:
        /// RGB(0, 0, 255) - Water
        /// RGB(0, 255, 0) - Grass Height 0
        /// RGB(255, 255, 0) - Sand Height 0
        /// RGB(255, 0, 0) - Bridge
        /// 
        /// Next is a text file. The text file is similar to the old files,
        /// only with units instead of tiles
        /// 
        /// The first two lines are:
        /// tileset
        /// musicTrack
        /// 
        /// Every subsequent line is in the format
        /// unitX,unitY,unitType,isPlayerUnit
        /// </summary>
        public static Map LoadMap(string imagePath, string textPath)
        {
            Bitmap map = new Bitmap(imagePath);
            int x = map.Width;
            int y = map.Height;

            int tileSet;
            int musicTrack;


            StreamReader sr = new StreamReader(textPath);
            string mapData = sr.ReadToEnd();

            string[] lines = mapData.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            tileSet = Int32.Parse(lines[0]);
            musicTrack = Int32.Parse(lines[1]);
            Map gameMap = new Map(new int[] { x, y }, tileSet, musicTrack);
            

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (map.GetPixel(i, j) == Color.FromArgb(0, 0, 255))
                    {
                        Tile tile = new Tile();
                        tile.type = "water";
                        tile.Height = 0;
                        tile.IsImpassible = true;
                        tile.MoveCost = 0;
                        tile.AssignFileName();
                        tile.X = i;
                        tile.Y = j;
                        gameMap.map[i][j] = tile;
                    }
                    else if (map.GetPixel(i, j) == Color.FromArgb(0, 255, 0))
                    {
                        Tile tile = new Tile();
                        tile.type = "grass";
                        tile.Height = 0;
                        tile.IsImpassible = false;
                        tile.MoveCost = 25;
                        tile.AssignFileName();
                        tile.X = i;
                        tile.Y = j;
                        gameMap.map[i][j] = tile;
                    }
                    else if (map.GetPixel(i, j) == Color.FromArgb(255, 255, 0))
                    {
                        Tile tile = new Tile();
                        tile.type = "sand";
                        tile.Height = 0;
                        tile.IsImpassible = false;
                        tile.MoveCost = 50;
                        tile.AssignFileName();
                        tile.X = i;
                        tile.Y = j;
                        gameMap.map[i][j] = tile;
                    }
                    else
                    {
                        Tile tile = new Tile();
                        tile.type = "bridge";
                        tile.Height = 0;
                        tile.IsImpassible = false;
                        tile.MoveCost = 10;
                        tile.AssignFileName();
                        tile.X = i;
                        tile.Y = j;
                        gameMap.map[i][j] = tile;
                    }

                    
                }
            }
            for (int i = 2; i < lines.Length; i++)
            {


                string[] unitData = lines[i].Split(',');

                int unitX = Int32.Parse(unitData[0]);
                int unitY = Int32.Parse(unitData[1]);

                Unit unit;

                int uType = Int32.Parse(unitData[2]);
                int playerUnit = Int32.Parse(unitData[3]);
                switch (uType)
                {
                    case 0: unit = new Soldier(); break;
                    case 1: unit = new Defender(); break;
                    case 2: unit = new Cavalry(); break;
                    case 3: unit = new Ranger(); break;
                    case 4: unit = new Mage(); break;
                    case 5: unit = new Artillery(); break;
                    case 6: unit = new Scout(); break;
                    case 7: unit = new Bomber(); break;
                    case 8: unit = new Fighter(); break;
                    default: unit = new Soldier(); break;
                }
                unit.Location[0] = unitX;
                unit.Location[1] = unitY;
                gameMap.GetTileAt(unitX, unitY).TileUnit = unit;
                gameMap.GetTileAt(unitX, unitY).hasUnit = true;
                gameMap.GetTileAt(unitX, unitY).IsImpassible = true;
                if (playerUnit == 0)
                {
                    gameMap.GetTileAt(unitX, unitY).TileUnit.isPlayerUnit = true;
                }
                else
                {
                    gameMap.GetTileAt(unitX, unitY).TileUnit.isPlayerUnit = false;
                }


            }

            sr.Close();

            return gameMap;
        }
        
    }
}
