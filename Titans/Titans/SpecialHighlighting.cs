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

            if (battle.ActiveUnit is Defender)
            {
                if (battle.specialMode1)
                {
                    battle.ShowSpecificAttackRange(battle.ActiveUnit, 1);
                    if (X >= 0 && X < battle.BattleMap.Size[0] && Y >= 0 && Y < battle.BattleMap.Size[1])
                    {
                        battle.BattleMap.AddSpecificHighlight(X, Y);
                        game.lastSelectedTile = battle.BattleMap.GetTileAt(X, Y);
                    }
                    else
                    {
                        battle.BattleMap.AddSpecificHighlight(game.lastSelectedTile.X, game.lastSelectedTile.Y);
                    }

                    List<Tile> nearbyEnemies = AI.GetAdjacentEnemyTiles(battle.ActiveUnit, battle.BattleMap);
                    if (game.lastSelectedTile.hasUnit)
                    {
                        if (game.lastSelectedTile.TileUnit == battle.ActiveUnit)
                        {
                            
                            foreach (Tile tile in nearbyEnemies)
                            {
                                battle.BattleMap.AddSpecificHighlight(tile.X, tile.Y);
                            }
                        }
                    }

                    foreach (Tile tile in nearbyEnemies)
                    {
                        battle.BattleMap.AddSpecificRedHighlight(tile.X, tile.Y);
                    }
                }
            }
        }
    }
}
