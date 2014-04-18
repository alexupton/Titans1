using System;
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
    class Draw
    {
        public Game1 game { get; set; }


        public Draw(Game1 currentGame)
        {
            game = currentGame;


        }

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

        public void CampaignMenu()
        {
            game.GraphicsDevice.Clear(Color.White);
            game.spriteBatch.Begin();
            game.spriteBatch.Draw(game.newGame, new Vector2(550, 300), Color.White);
            game.spriteBatch.Draw(game.loadGame, new Vector2(550, 343), Color.White);
            game.spriteBatch.Draw(game.back, new Vector2(15, 755), Color.White);
            game.spriteBatch.End();
        }

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
            game.spriteBatch.Draw(game.move, new Vector2(680, 700), Color.White);
            game.spriteBatch.Draw(game.pass, new Vector2(796, 700), Color.White);
            game.spriteBatch.Draw(game.attack, new Vector2(680, 727), Color.White);
            game.spriteBatch.Draw(game.defend, new Vector2(796, 727), Color.White);
            game.spriteBatch.Draw(game.special, new Vector2(680, 754), Color.White);
            game.spriteBatch.Draw(game.item, new Vector2(796, 754), Color.White);
            game.spriteBatch.Draw(game.UI, new Vector2(0, 0), Color.White);


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
    }
}
