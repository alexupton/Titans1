﻿using System;
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
    public class SpecialClicks
    {
        private Game1 Game;
        public SpecialClicks(Game1 game)
        {
            Game = game;
        }

        public void EvaluateSpecialClick(Point mousePos)
        {
            Game.releaseWait = true;
            Unit active = Game.battle.ActiveUnit;
            Battle battle = Game.battle;
            int X = (int)Math.Round(((double)mousePos.X - (double)Game.offsetX - 20) / (double)55);
            int Y = (int)Math.Round(((double)mousePos.Y - (double)Game.offsetY - 20) / (double)55);

            //soldier confirm special
            if (active is Soldier)
            {
                if (battle.specialMode1)
                {
                    battle.SelectedTile = battle.BattleMap.GetTileAt(X, Y);
                    if (battle.BattleMap.GetTileAt(X, Y).hasUnit)
                    {
                        if (battle.BattleMap.GetTileAt(X, Y).TileUnit == active)
                        {
                            active.Special1(battle);
                            battle.DeselectSpecial();
                            battle.DeselectSpecialNumber();

                            Game.releaseWait = true;
                            Game.isSpecial = false;
                            Game.battle.DeselectSpecialNumber();
                            Game.battle.DeselectSpecial();
                            Game.move = Game.movetrue;
                            Game.attack = Game.attacktrue;
                                Game.defend = Game.defendtrue;
                            Game.item = Game.itemtrue;
                            Game.pass = Game.passtrue;
                            Game.special = Game.specialtrue;

                            Game.specialButtons[0] = Game.specialNor;
                            Game.specialButtons[1] = Game.specialNor;
                            Game.specialButtons[2] = Game.specialNor;
                            Game.specialButtons[3] = Game.specialNor;
                            Game.specialButtons[4] = Game.specialNor;
                            Game.specialButtons[5] = Game.specialNor;

                            Game.draw.specialColor[0] = Color.Black;
                            Game.draw.specialColor[1] = Color.Black;
                            Game.draw.specialColor[2] = Color.Black;
                            Game.draw.specialColor[3] = Color.Black;
                            Game.draw.specialColor[4] = Color.Black;
                            Game.draw.specialColor[5] = Color.Black;
                            Game.battle.DeselectSpecial();


                            Game.battle.BattleMap.ClearAllHighlights();
                        }
                        else
                        {
                            Game.sfx.PlayBuzzer();
                            Game.sfx.PlayBuzzer();
                        }
                    }
                    else
                    {
                        Game.sfx.PlayBuzzer();
                        Game.sfx.PlayBuzzer();
                    }
                }
                else if (battle.specialMode2)
                {
                    battle.SelectedTile = battle.BattleMap.GetTileAt(X, Y);
                    if (battle.SelectedTile.hasUnit)
                    {
                        if (active.isPlayerUnit != battle.SelectedTile.TileUnit.isPlayerUnit)
                        {
                            battle.CurrentTarget = battle.SelectedTile.TileUnit;
                            active.Special2(battle);

                            EndSpecial();

                        }
                    }
                    else
                    {
                        Game.sfx.PlayBuzzer();
                        Game.sfx.PlayBuzzer();
                    }
                }
                else
                {
                    battle.SelectedTile = battle.BattleMap.GetTileAt(X, Y);
                    if (battle.SelectedTile.hasUnit)
                    {
                        if (active == battle.SelectedTile.TileUnit)
                        {
                            battle.CurrentTarget = battle.SelectedTile.TileUnit;
                            active.Special3(battle);

                            EndSpecial();
                        }
                        else
                        {
                            Game.sfx.PlayBuzzer();
                            Game.sfx.PlayBuzzer();
                        }
                    }
                    else
                    {
                        Game.sfx.PlayBuzzer();
                        Game.sfx.PlayBuzzer();
                    }
                }
            }
        }

        private void EndSpecial()
        {
            Game.releaseWait = true;
            Game.isSpecial = false;
            Game.battle.DeselectSpecialNumber();
            Game.battle.DeselectSpecial();
            Game.move = Game.movetrue;
            Game.attack = Game.attacktrue;
            if (Game.battle.ActiveUnit is Ranger)
            {
                Game.defend = Game.defend_grey;
            }
            else
            {
                Game.defend = Game.defendtrue;
            }
            Game.item = Game.itemtrue;
            Game.pass = Game.passtrue;
            Game.special = Game.specialtrue;

            Game.specialButtons[0] = Game.specialNor;
            Game.specialButtons[1] = Game.specialNor;
            Game.specialButtons[2] = Game.specialNor;
            Game.specialButtons[3] = Game.specialNor;
            Game.specialButtons[4] = Game.specialNor;
            Game.specialButtons[5] = Game.specialNor;

            Game.draw.specialColor[0] = Color.Black;
            Game.draw.specialColor[1] = Color.Black;
            Game.draw.specialColor[2] = Color.Black;
            Game.draw.specialColor[3] = Color.Black;
            Game.draw.specialColor[4] = Color.Black;
            Game.draw.specialColor[5] = Color.Black;
            Game.battle.DeselectSpecial();


            Game.battle.BattleMap.ClearAllHighlights();
            Game.specialAttack = false;
        }
    }
}