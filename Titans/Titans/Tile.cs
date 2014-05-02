using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Titans
{
    public class Tile
    {
        public string Filename { get; set; } //where the sprite file is
        public Unit TileUnit
        {
            get;
            set;
        } //what unit, if any is on this tile
        public int Height { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsImpassible { get; set; }
        public bool IsHighlighted { get; set; }
        public bool IsRedHighlighted { get; set; }
        public bool IsBlueHighlighted { get; set; }
        public bool hasUnit { get; set; }
        public int MoveCost { get; set; } //How many movement points are consumed by moving over the tile
        public bool HasTrap { get; set; }
                                            
        public string type { get; set; }

        //pathfinding junk
        public Tile parentTile { get; set; }
        public bool Searched { get; set; }
        public bool IsRoot { get; set; }
        public int FScore { get; set; }
        public int GScore { get; set; }
        public int HScore { get; set; }

        public Tile()
        {
            IsRedHighlighted = false;
            IsRoot = false;
            Searched = false;
            FScore = 0;
            GScore = 0;
            HScore = 0;

        }

        public void Highlight()
        {
            IsHighlighted = true;
        }

        //Remove the tile highlight and restore it to its clean version
        //ALSO NOT YET DONE
        public void ClearHighlight()
        {
            IsHighlighted = false;
        }

        public void RedHighlight()
        {
            IsRedHighlighted = true;
        }

        public void BlueHighlight()
        {
            IsBlueHighlighted = true;
        }

        public void ClearBlueHighlight()
        {
            IsBlueHighlighted = false;
        }

        public void ClearRedHighlight()
        {
            IsRedHighlighted = false;
        }

        public void AssignFileName()
        {
            switch (type)
            {
                case "grass": Filename = "Grass1"; break;
                case "bridge": Filename = "Bridge1"; break;
                case "sand":
                    {
                        if (Height == 0)
                            Filename = "Sand1_Height1";
                        else if (Height == 1)
                            Filename = "Sand1_Height2";
                        else if (Height == 2)
                            Filename = "Sand1_Height3";
                        else
                            Filename = "Sand1_Height4";
                        break;
                    }
                case "water": Filename = "Water1"; IsImpassible = true; break;
            }
        }
    }
}
