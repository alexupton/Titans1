﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Titans
{
    public class Draw
    {
        public Game1 game { get; set; }
        public Color[] textColor;
        public Color[] optionsColor;
        public Color[] specialColor;
        

        public Draw(Game1 currentGame)
        {
            game = currentGame;
            textColor = new Color[5];
            for (int i = 0; i < 5; i++)
            {
                textColor[i] = Color.Black;
            }
            optionsColor = new Color[25];
            for (int i = 0; i < 25; i++)
            {
                optionsColor[i] = Color.Black;
            }
            specialColor = new Color[6];
            for (int i = 0; i < 6; i++)
            {
                specialColor[i] = Color.Black;
            }

        }

        //Draw the initial main menu you see at startup
        public void MainMenu()
        {
            game.GraphicsDevice.Clear(Color.White);
            //// TODO: Add your drawing code here
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.quick_battle, new Vector2((game.Window.ClientBounds.Width / 2) - (game.quick_battle.Width / 2), (game.Window.ClientBounds.Height / 2) - (game.quick_battle.Height / 2)), Color.White);
            game.spriteBatch.Draw(game.campaign, new Vector2((game.Window.ClientBounds.Width / 2) - (game.campaign.Width / 2), (game.Window.ClientBounds.Height / 2) - (game.quick_battle.Height - 55)), Color.White);
            game.spriteBatch.Draw(game.custom_battle, new Vector2((game.Window.ClientBounds.Width / 2) - (game.custom_battle.Width / 2), (game.Window.ClientBounds.Height / 2) - (game.campaign.Height - 95)), Color.White);
            game.spriteBatch.Draw(game.options, new Vector2((game.Window.ClientBounds.Width / 2) - (game.options.Width / 2), (game.Window.ClientBounds.Height / 2) - (game.custom_battle.Height - 140)), Color.White);
            game.spriteBatch.Draw(game.exit, new Vector2((game.Window.ClientBounds.Width / 2) - (game.exit.Width / 2), (game.Window.ClientBounds.Height - 70)), Color.White);
            game.spriteBatch.Draw(game.exit2, new Vector2((game.Window.ClientBounds.Width / 2) - (game.exit2.Width / 2), (game.Window.ClientBounds.Height - 70)), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, -1);
            game.spriteBatch.End();
        }

        //Draw the campaign menu that you see once clicking on campaign
        public void CampaignMenu()
        {
            game.GraphicsDevice.Clear(Color.White);
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.newGame, new Vector2(550, 300), Color.White);
            game.spriteBatch.Draw(game.loadGame, new Vector2(550, 343), Color.White);
            game.spriteBatch.Draw(game.back, new Vector2(15, 755), Color.White);
            game.spriteBatch.End();
        }

        //Draw the options menu that you see once clicking on options
        public void OptionsMenu()
        {
            game.GraphicsDevice.Clear(Color.White);
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.fullScreen, new Vector2(25, 337), Color.White);
            game.spriteBatch.Draw(game.yes_invert, new Vector2(206, 337), Color.White);
            game.spriteBatch.Draw(game.no_invert, new Vector2(360, 337), Color.White);
            game.spriteBatch.Draw(game.resolution, new Vector2(25, 385), Color.White);
            game.spriteBatch.Draw(game.res1_unselected, new Vector2(206, 385), Color.White);
            game.spriteBatch.Draw(game.res2, new Vector2(360, 385), Color.White);
            game.spriteBatch.Draw(game.textSpeed, new Vector2(25, 433), Color.White);
            game.spriteBatch.Draw(game.slow_unselected, new Vector2(206, 433), Color.White);
            game.spriteBatch.Draw(game.regular, new Vector2(360, 433), Color.White);
            game.spriteBatch.Draw(game.fast_unselected, new Vector2(514, 433), Color.White);
            game.spriteBatch.Draw(game.musicLevel, new Vector2(25, 483), Color.White);
            game.spriteBatch.Draw(game.volmute_unselected, new Vector2(206, 483), Color.White);
            game.spriteBatch.Draw(game.vollevel1_unselected, new Vector2(360, 483), Color.White);
            game.spriteBatch.Draw(game.vollevel2_unselected, new Vector2(514, 483), Color.White);
            game.spriteBatch.Draw(game.vollevel3_unselected, new Vector2(669, 483), Color.White);
            game.spriteBatch.Draw(game.vollevelMax_unselected, new Vector2(825, 483), Color.White);
            game.spriteBatch.Draw(game.soundEffects, new Vector2(15, 533), Color.White);
            game.spriteBatch.Draw(game.mute_unselected, new Vector2(206, 533), Color.White);
            game.spriteBatch.Draw(game.level1_unselected, new Vector2(360, 533), Color.White);
            game.spriteBatch.Draw(game.level2_unselected, new Vector2(514, 533), Color.White);
            game.spriteBatch.Draw(game.level3_unselected, new Vector2(669, 533), Color.White);
            game.spriteBatch.Draw(game.levelMax_unselected, new Vector2(825, 533), Color.White);
            game.spriteBatch.Draw(game.credits, new Vector2(550, 755), Color.White);
            game.spriteBatch.Draw(game.back, new Vector2(15, 755), Color.White);
            game.spriteBatch.Draw(game.apply, new Vector2(1115, 755), Color.White);

            game.spriteBatch.End();
        }

        //Draw in our Demo game map
        public void Demo()
        {
            game.GraphicsDevice.Clear(Color.Black);
            game.spriteBatch.Begin();
            //draw tiles
            for (int x = 0; x < game.battle.BattleMap.Size[0]; x++)
            {
                for (int y = 0; y < game.battle.BattleMap.Size[1]; y++)
                {
                    if (game.battle.BattleMap.map[x][y].Filename == "Bridge1")
                    {
                        game.spriteBatch.Draw(game.Bridge1, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Grass1")
                    {
                        game.spriteBatch.Draw(game.Grass1, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Sand1_Height1")
                    {
                        game.spriteBatch.Draw(game.Sand1_Height1, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Sand1_Height2")
                    {
                        game.spriteBatch.Draw(game.Sand1_Height2, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Sand1_Height3")
                    {
                        game.spriteBatch.Draw(game.Sand1_Height3, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Sand1_Height4")
                    {
                        game.spriteBatch.Draw(game.Sand1_Height4, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].Filename == "Water1")
                    {
                        game.spriteBatch.Draw(game.Water1, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }

                    //Draw units
                    if (game.battle.BattleMap.map[x][y].hasUnit)
                    {
                        
                        
                        if (game.battle.BattleMap.map[x][y].TileUnit is Artillery)
                        {
                            game.spriteBatch.Draw(game.Artillery, new Vector2(x * 55 + game.offsetX + 10, y * 55 + game.offsetY + 3),
                                new Rectangle(game.currentFrame.X * game.frameSize.X, game.currentFrame.Y * game.frameSize.Y, game.frameSize.X, game.frameSize.Y), Color.White);
                        }
                        else if (game.battle.BattleMap.map[x][y].TileUnit is Defender)
                        {
                            game.spriteBatch.Draw(game.Defender, new Vector2(x * 55 + game.offsetX + 10, y * 55 + game.offsetY + 3),
                                new Rectangle(game.currentFrame.X * game.frameSize.X, game.currentFrame.Y * game.frameSize.Y, game.frameSize.X, game.frameSize.Y), Color.White);
                        }
                        else if (game.battle.BattleMap.map[x][y].TileUnit is Mage)
                        {
                            game.spriteBatch.Draw(game.Mage, new Vector2(x * 55 + game.offsetX + 10, y * 55 + game.offsetY + 3),
                                new Rectangle(game.currentFrame.X * game.frameSize.X, game.currentFrame.Y * game.frameSize.Y, game.frameSize.X, game.frameSize.Y), Color.White);
                        }
                        else if (game.battle.BattleMap.map[x][y].TileUnit is Ranger)
                        {
                            game.spriteBatch.Draw(game.Ranger, new Vector2(x * 55 + game.offsetX + 10, y * 55 + game.offsetY + 3),
                                new Rectangle(game.currentFrame.X * game.frameSize.X, game.currentFrame.Y * game.frameSize.Y, game.frameSize.X, game.frameSize.Y), Color.White);
                        }
                        else
                        {
                            game.spriteBatch.Draw(game.Soldier, new Vector2(x * 55 + game.offsetX + 10, y * 55 + game.offsetY + 3),
                                new Rectangle(game.currentFrame.X * game.frameSize.X, game.currentFrame.Y * game.frameSize.Y, game.frameSize.X, game.frameSize.Y), Color.White);
                        }
                        
                            
                            

                    }

                    //Draw highlights
                    if (game.battle.BattleMap.map[x][y].IsRedHighlighted && game.battle.BattleMap.map[x][y].IsHighlighted)
                    {
                        game.spriteBatch.Draw(game.GreenHighlight, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].IsRedHighlighted)
                    {
                        game.spriteBatch.Draw(game.RedHighlight, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].IsHighlighted)
                    {
                        game.spriteBatch.Draw(game.Highlight, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    else if (game.battle.BattleMap.map[x][y].IsBlueHighlighted)
                    {
                        game.spriteBatch.Draw(game.BlueHighlight, new Vector2(x * 55 + game.offsetX, y * 55 + game.offsetY), Color.White);
                    }
                    
                }
            }
            //draw unit HP
            for (int x = 0; x < game.battle.BattleMap.Size[0]; x++)
            {
                for (int y = 0; y < game.battle.BattleMap.Size[1]; y++)
                {
                    if (game.battle.BattleMap.map[x][y].hasUnit)
                    {
                        if (game.battle.BattleMap.map[x][y].TileUnit.isPlayerUnit)
                        {
                            game.spriteBatch.DrawString(game.smallText, game.battle.BattleMap.map[x][y].TileUnit.HP + "/" + game.battle.BattleMap.map[x][y].TileUnit.MaxHP,
                                new Vector2(x * 55 + 2 + game.offsetX, y * 55 + 50 + game.offsetY), Color.Black);
                        }
                        else
                        {
                            game.spriteBatch.DrawString(game.smallText, game.battle.BattleMap.map[x][y].TileUnit.HP + "/" + game.battle.BattleMap.map[x][y].TileUnit.MaxHP,
                                new Vector2(x * 55 + 2 + game.offsetX, y * 55 + 50 + game.offsetY), Color.Red);
                        }

                        if (game.battle.BattleMap.map[x][y].hasUnit)
                        {
                            if (game.battle.BattleMap.map[x][y].TileUnit.DefenseModifiers.Count > 0)
                            {
                                int defTotal = 0;
                                foreach (int mod in game.battle.BattleMap.map[x][y].TileUnit.DefenseModifiers)
                                {
                                    defTotal += mod;
                                }
                                game.spriteBatch.DrawString(game.text, "+" + defTotal.ToString(), new Vector2(game.battle.BattleMap.map[x][y].TileUnit.Location[0] * 55 + game.offsetX + 42,
                                    game.battle.BattleMap.map[x][y].TileUnit.Location[1] * 55 + game.offsetY + 5), Color.Magenta);
                            }
                        }
                    
                    }

                }
            }
            //draw static UI elements
            Vector2 unitLoc = new Vector2(game.battle.ActiveUnit.Location[0] * 55 + game.offsetX, game.battle.ActiveUnit.Location[1] * 55 + game.offsetY);
            game.spriteBatch.Draw(game.Highlight, unitLoc, Color.White);
            game.spriteBatch.Draw(game.move, new Vector2(680, 700), Color.White);
            game.spriteBatch.Draw(game.pass, new Vector2(796, 700), Color.White);
            game.spriteBatch.Draw(game.attack, new Vector2(680, 727), Color.White);
            game.spriteBatch.Draw(game.defend, new Vector2(796, 727), Color.White);
            game.spriteBatch.Draw(game.special, new Vector2(680, 754), Color.White);
            game.spriteBatch.Draw(game.item, new Vector2(796, 754), Color.White);
            game.spriteBatch.Draw(game.UI, new Vector2(0, 0), Color.White);

            //special buttons
            if (game.isSpecial&&!(game.battle.ActiveUnit is Artillery))
            {
                if (game.battle.ActiveUnit is Mage )
                {
                    game.spriteBatch.Draw(game.specialButtons[0], new Vector2(565, 673), Color.White);
                    float x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(0))).X;
                    float buttonx = 116 / 2;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(0), new Vector2((buttonx-(x / 2) )+565 , 676),specialColor[0]);

                    game.spriteBatch.Draw(game.specialButtons[1], new Vector2(565, 646), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(1))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(1), new Vector2(buttonx-(x / 2) + 565, 649), specialColor[1]);
                    
                    game.spriteBatch.Draw(game.specialButtons[2], new Vector2(565, 619), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(2))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(2), new Vector2(buttonx-(x / 2) + 565, 622), specialColor[2]);
                
                     game.spriteBatch.Draw(game.specialButtons[3], new Vector2(565, 700), Color.White);
                     x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(3))).X;
                     game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(3), new Vector2(buttonx-(x / 2) + 565, 703), specialColor[3]);
                    
                    game.spriteBatch.Draw(game.specialButtons[4], new Vector2(565, 727), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(4))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(4), new Vector2(buttonx-(x / 2) + 565, 730), specialColor[4]);
                    
                    game.spriteBatch.Draw(game.specialButtons[5], new Vector2(565, 754), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(5))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(5), new Vector2(buttonx-(x / 2) + 565, 757), specialColor[5]);
                
                }
                else
                {
                    game.spriteBatch.Draw(game.specialButtons[0], new Vector2(565, 700), Color.White);
                    float buttonx = 116 / 2;
                    float x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(0))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(0), new Vector2(buttonx-(x / 2) + 565, 703), specialColor[0]);
                    game.spriteBatch.Draw(game.specialButtons[1], new Vector2(565, 727), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(1))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(1), new Vector2(buttonx-(x / 2) + 565, 730), specialColor[1]);
                    game.spriteBatch.Draw(game.specialButtons[2], new Vector2(565, 754), Color.White);
                    x = (game.specialText.MeasureString(game.battle.ActiveUnit.Abilities.ElementAt(2))).X;
                    game.spriteBatch.DrawString(game.specialText, game.battle.ActiveUnit.Abilities.ElementAt(2), new Vector2(buttonx-(x / 2) + 565, 757), specialColor[2]);
                }
            }

            //draw dynamic UI elements
            Vector2 textLoc = new Vector2(1255, 704);
            game.spriteBatch.DrawString(game.text, game.unit, textLoc, Color.Black);
            textLoc.Y += 24;
            textLoc.X += 52;
            game.spriteBatch.DrawString(game.text, game.hp, textLoc, Color.Black);
            textLoc.Y += 24;
            game.spriteBatch.DrawString(game.text, game.range, textLoc, Color.Black);
            textLoc.Y += 24;
            game.spriteBatch.DrawString(game.text, game.defense, textLoc, Color.Black);
            textLoc = new Vector2(1455, 704);
            game.spriteBatch.DrawString(game.text, game.speed, textLoc, Color.Black);
            textLoc.Y += 24;
            game.spriteBatch.DrawString(game.text, game.mp, textLoc, Color.Black);
            textLoc.Y += 24;
            game.spriteBatch.DrawString(game.text, game.attackText, textLoc, Color.Black);
            textLoc.Y += 24;
            game.spriteBatch.DrawString(game.text, game.moveText, textLoc, Color.Black);
            textLoc.Y = 655;
            foreach (string next in game.nextUnits)
            {
                textLoc.X = 234 - game.text.MeasureString(next).X / 2;
                game.spriteBatch.DrawString(game.text, next, textLoc, Color.Black);
                textLoc.Y += 30;
            }

            //attack damage display
            if (game.displayDamage)
            {
                game.spriteBatch.DrawString(game.text, game.unitDamage.ToString() + " damage", new Vector2(game.attackedUnitTrueX + game.offsetX, game.attackedUnitTrueY + game.offsetY), Color.Red);
                if (game.splashDamage.Count > 0)
                {
                    for (int i = 0; i < game.splashDamage.Count; i++)
                    {
                        game.spriteBatch.DrawString(game.smallText, game.splashDamage.ElementAt(i) + " damage",
                            new Vector2(game.splashLocations.ElementAt(i).X * 55 + game.offsetX - 13, game.splashLocations.ElementAt(i).Y * 55 + game.offsetY + 20),
                            Color.Red);
                    }
                }
            }

            if (game.p1win)
            {

                game.spriteBatch.DrawString(game.text, "Player 1 wins!", new Vector2(740, 400), Color.Black);
                game.displayDamage = false;
            }

            else if (game.p2win)
            {
                game.spriteBatch.DrawString(game.text, "Player 2 wins!", new Vector2(740, 400), Color.Black);
                game.displayDamage = false;
            }
            

            game.spriteBatch.End();
        }
        
        public void ingamemenu()
        {
            
            game.spriteBatch.Begin();

            game.spriteBatch.Draw(game.menuBox, new Vector2(450, 137), Color.White);
            game.spriteBatch.DrawString(game.bigText, "Menu", new Vector2(705,160), Color.Black);
            game.spriteBatch.Draw(game.menuButtons[0], new Vector2(700, 227), Color.White);
            game.spriteBatch.DrawString(game.text, "Resume", new Vector2(725, 230), textColor[0]);
            game.spriteBatch.Draw(game.menuButtons[1], new Vector2(700, 264), Color.White);
            game.spriteBatch.DrawString(game.text, " Save", new Vector2(725, 267), textColor[1]);
            game.spriteBatch.Draw(game.menuButtons[2], new Vector2(700, 301), Color.White);
            game.spriteBatch.DrawString(game.text, " Load", new Vector2(725, 304), textColor[2]);
            game.spriteBatch.Draw(game.menuButtons[3], new Vector2(700, 338), Color.White);
            game.spriteBatch.DrawString(game.text, "Options", new Vector2(725, 341), textColor[3]);
            game.spriteBatch.Draw(game.menuButtons[4], new Vector2(700, 400), Color.White);
            game.spriteBatch.DrawString(game.text, " Exit", new Vector2(725, 403), textColor[4]);
            
            game.spriteBatch.End();
        }
        public void optionsIngame()
    {
        game.spriteBatch.Begin();
        game.spriteBatch.Draw(game.optionsBox, new Vector2(468, 137), Color.White);
        game.spriteBatch.DrawString(game.bigText, "Options", new Vector2(745, 160), Color.Black);

        game.spriteBatch.Draw(game.optionsButtons[0], new Vector2(500, 227), Color.White);
        game.spriteBatch.DrawString(game.text, "FullScreen", new Vector2(503, 230), optionsColor[0]);
        game.spriteBatch.Draw(game.optionsButtons[6], new Vector2(625, 227), Color.White);
        game.spriteBatch.DrawString(game.text, "Yes", new Vector2(668, 230), optionsColor[6]);
        game.spriteBatch.Draw(game.optionsButtons[7], new Vector2(750, 227), Color.White);
        game.spriteBatch.DrawString(game.text, "No", new Vector2(800, 230), optionsColor[7]);

        game.spriteBatch.Draw(game.optionsButtons[1], new Vector2(500, 264), Color.White);
        game.spriteBatch.DrawString(game.text, "Resolution", new Vector2(503, 267), optionsColor[1]);
        game.spriteBatch.Draw(game.optionsButtons[8], new Vector2(625, 264), Color.White);
        game.spriteBatch.DrawString(game.text, "1200X800", new Vector2(640, 267), optionsColor[8]);
        game.spriteBatch.Draw(game.optionsButtons[9], new Vector2(750, 264), Color.White);
        game.spriteBatch.DrawString(game.text, "1500X800", new Vector2(765, 267), optionsColor[9]);

        game.spriteBatch.Draw(game.optionsButtons[2], new Vector2(500, 301), Color.White);
        game.spriteBatch.DrawString(game.text, "Text Speed", new Vector2(503, 304), optionsColor[2]);
        game.spriteBatch.Draw(game.optionsButtons[10], new Vector2(625, 301), Color.White);
        game.spriteBatch.DrawString(game.text, "Slow", new Vector2(660, 304), optionsColor[10]);
        game.spriteBatch.Draw(game.optionsButtons[11], new Vector2(750, 301), Color.White);
        game.spriteBatch.DrawString(game.text, "Normal", new Vector2(780, 304), optionsColor[11]);
        game.spriteBatch.Draw(game.optionsButtons[12], new Vector2(875, 301), Color.White);
        game.spriteBatch.DrawString(game.text, "Fast", new Vector2(915, 304), optionsColor[12]);

        game.spriteBatch.Draw(game.optionsButtons[3], new Vector2(500, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "Music Vol", new Vector2(510, 338), optionsColor[3]);
        game.spriteBatch.Draw(game.optionsButtons[13], new Vector2(625, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "Mute", new Vector2(660, 338), optionsColor[13]);
        game.spriteBatch.Draw(game.optionsButtons[14], new Vector2(750, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "25%", new Vector2(793, 338), optionsColor[14]);
        game.spriteBatch.Draw(game.optionsButtons[15], new Vector2(875, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "50%", new Vector2(920, 338), optionsColor[14]);
        game.spriteBatch.Draw(game.optionsButtons[16], new Vector2(1000, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "75%", new Vector2(1045, 338), optionsColor[15]);
        game.spriteBatch.Draw(game.optionsButtons[17], new Vector2(1125, 335), Color.White);
        game.spriteBatch.DrawString(game.text, "100%", new Vector2(1165, 338), optionsColor[16]);

        game.spriteBatch.Draw(game.optionsButtons[4], new Vector2(500, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "SFX Vol", new Vector2(515, 370), optionsColor[4]);
        game.spriteBatch.Draw(game.optionsButtons[18], new Vector2(625, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "Mute", new Vector2(660, 370), optionsColor[17]);
        game.spriteBatch.Draw(game.optionsButtons[19], new Vector2(750, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "25%", new Vector2(793, 370), optionsColor[18]);
        game.spriteBatch.Draw(game.optionsButtons[20], new Vector2(875, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "50%", new Vector2(920, 370), optionsColor[19]);
        game.spriteBatch.Draw(game.optionsButtons[21], new Vector2(1000, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "75%", new Vector2(1045, 370), optionsColor[20]);
        game.spriteBatch.Draw(game.optionsButtons[22], new Vector2(1125, 367), Color.White);
        game.spriteBatch.DrawString(game.text, "100%", new Vector2(1165, 370), optionsColor[21]);

        game.spriteBatch.Draw(game.optionsButtons[5], new Vector2(500, 475), Color.White);
        game.spriteBatch.DrawString(game.text,"Back", new Vector2(535, 478), optionsColor[5]);
        
            

        
        game.spriteBatch.End();
    }
    }
}
