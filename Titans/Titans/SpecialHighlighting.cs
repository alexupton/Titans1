using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Titans
{
    public class SpecialHighlighting
    {
        private Game1 game;
        public SpecialHighlighting(Game1 currentGame)
        {
            game = currentGame;
        }

        public void SpecialHighlight(Point mousePos)
        {
            int X = (int)Math.Round(((double)mousePos.X - (double)game.offsetX - 20) / (double)55);
            int Y = (int)Math.Round(((double)mousePos.Y - (double)game.offsetY - 20) / (double)55);
            Battle battle = game.battle;
            Unit active = game.battle.ActiveUnit;

            //soldier specials
            if (battle.ActiveUnit is Soldier)
            {
                if (battle.specialMode1)
                {
                    List<Tile> adjacent = AI.GetAllAdjacentTiles(battle.BattleMap, battle.BattleMap.GetTileAt(battle.ActiveUnit.Location[0], battle.ActiveUnit.Location[1]));
                    foreach (Tile adj in adjacent)
                    {
                        adj.BlueHighlight();
                    }
                    if (X >= 0 && X < battle.BattleMap.Size[0] && Y >= 0 && Y < battle.BattleMap.Size[1])
                    {
                        battle.BattleMap.AddSpecificHighlight(X, Y);
                        game.lastSelectedTile = battle.BattleMap.GetTileAt(X, Y);
                        if (game.lastSelectedTile.IsRedHighlighted)
                        {
                            battle.BattleMap.GetTileAt(game.lastSelectedTile.X, game.lastSelectedTile.Y);
                        }
                    }
                    else
                    {
                        battle.BattleMap.AddSpecificHighlight(game.lastSelectedTile.X, game.lastSelectedTile.Y);
                    }

                    List<Tile> nearbyEnemies = AI.GetAdjacentEnemyTiles(battle.ActiveUnit, battle.BattleMap);
                    foreach (Tile tile in nearbyEnemies)
                    {
                        battle.BattleMap.AddSpecificRedHighlight(tile.X, tile.Y);
                    }
                    
                    if (game.lastSelectedTile.hasUnit)
                    {
                        if (game.lastSelectedTile.TileUnit == battle.ActiveUnit)
                        {
                            battle.BattleMap.AddSpecificRedHighlight(battle.ActiveUnit.Location[0], battle.ActiveUnit.Location[1]);
                            foreach (Tile tile in nearbyEnemies)
                            {
                                battle.BattleMap.AddSpecificHighlight(tile.X, tile.Y);
                            }
                        }
                        else
                        {
                            battle.BattleMap.ClearSpecificRedHighlight(X, Y);
                        }
                    }


                }

                else if (battle.specialMode2)
                {
                    battle.SelectEnabled = false;
                    int range = active.Range;
                    int x = active.Location[0];
                    int y = active.Location[1];
                    List<int[]> rangeSquare = new List<int[]>();
                    List<int[]> actualRange = new List<int[]>();

                    for (int i = (range * -1); i <= range; i++)
                    {
                        for (int j = (range * -1); j <= range; j++)
                        {
                            if ((i + x) < battle.BattleMap.Size[0] && (j + y) < battle.BattleMap.Size[1] && (i + x) >= 0 & (j + y) >= 0)
                            {
                                rangeSquare.Add(new int[] { i + x, j + y });
                            }
                        }
                    }

                    foreach (int[] tile in rangeSquare)
                    {
                        if ((Math.Abs(tile[0] - x) + Math.Abs(tile[1] - y)) <= range)
                        {
                            actualRange.Add(tile);
                        }
                    }




                    //now we check the tiles in reach for units and highlight them if they exist
                    foreach (int[] coords in actualRange)
                    {
                        battle.BattleMap.AddSpecificBlueHighlight(coords[0], coords[1]);
                        if (battle.BattleMap.map[coords[0]][coords[1]].hasUnit)
                        {
                            if (battle.BattleMap.map[coords[0]][coords[1]].TileUnit.isPlayerUnit != active.isPlayerUnit)
                            {
                                battle.BattleMap.AddSpecificRedHighlight(coords[0], coords[1]);
                            }
                            else
                            {
                                battle.BattleMap.AddSpecificBlueHighlight(coords[0], coords[1]);
                            }
                        }
                    }
                    game.specialAttack = true;
                }

                else
                {

                    battle.GameUI.specialAttack = true;

                    if (battle.BattleMap.GetTileAt(X, Y).hasUnit)
                    {
                        if (battle.BattleMap.GetTileAt(X, Y).TileUnit == battle.ActiveUnit)
                        {
                            battle.BattleMap.AddSpecificRedHighlight(X, Y);
                        }
                        else
                        {
                            battle.BattleMap.ClearSpecificRedHighlight(active.Location[0], active.Location[1]);
                        }
                    }
                    else
                    {
                        battle.BattleMap.ClearSpecificRedHighlight(active.Location[0], active.Location[1]);
                    }
                    
                }
            }
        }
    }
}
