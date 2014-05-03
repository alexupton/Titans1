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
    public class Buttons
    {
        public Game1 Game { get; set; }
        
       

        public Buttons(Game1 game)
        {
            Game = game;
            
        }
        public void CustomGameMenu(MouseState mouseState)
        {
            Rectangle backClick = new Rectangle(15, 755, 141, 33);
            bool mouseback = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(backClick);
            if (mouseback && Game.blankButton == Game.blankButton)
                {
                   Game.blankButton = Game.blankButtonIn;
                }
                else 
                {
                    Game.blankButton = Game.blankButton;
                }                
                
                //exits custom game menu
            if (mouseState.LeftButton == ButtonState.Pressed && !Game.releaseWait)
            {

                Point mousePos = new Point(mouseState.X, mouseState.Y);
                if (backClick.Contains(mousePos))
                {
                    Game.releaseWait = true;
                    Game.mainMenu = true;
                    Game.customMenu = false;
                    
                }
            }
        }
        public void InGameButtons(MouseState mouseState)
        {
            Rectangle resume = new Rectangle( 700, 227,116, 27);
            Rectangle saveGame = new Rectangle(725, 267,116, 27);
            Rectangle loadGame = new Rectangle(725, 304,116, 27);
            Rectangle options = new Rectangle(725, 341,116, 27);
            Rectangle exit = new Rectangle( 700, 400,116, 27);

            bool resumeMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(resume);
            bool saveMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(saveGame);
            bool loadMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(loadGame);
            bool optionsMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(options);
            bool exitMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(exit);
           

            //button click logic
            if (mouseState.LeftButton == ButtonState.Pressed&&!Game.releaseWait)
            {

                Point mousePos = new Point(mouseState.X, mouseState.Y);
                if (resume.Contains(mousePos))
                {

                    Game.ismenu = false;
                    Game.demo = true;
                    Game.releaseWait = true;

                }
                else if(options.Contains(mousePos))
                {

                    Game.isOptions = true; 
                    Game.releaseWait = true;
                }
                else if (exit.Contains(mousePos))
                {

                    Game.Exit();

                }

            }

          

            //button highlight logic
            if (resumeMouse && Game.menuButtons[0] == Game.normal)
            {
                
                Game.menuButtons[0] = Game.invert;
                Game.draw.textColor[0] = Color.White;
            }
            else if (!resumeMouse)
            {
                Game.menuButtons[0]= Game.normal;
                Game.draw.textColor[0] = Color.Black;
            }
            if (optionsMouse && Game.menuButtons[3] == Game.normal)
            {

                Game.menuButtons[3] = Game.invert;
                Game.draw.textColor[3] = Color.White;
            }
            else if (!optionsMouse)
            {
                Game.menuButtons[3] = Game.normal;
                Game.draw.textColor[3] = Color.Black;
            }
            if (exitMouse && Game.menuButtons[4] == Game.normal)
            {

                Game.menuButtons[4] = Game.invert;
                Game.draw.textColor[4] = Color.White;
            }
            else if (!exitMouse)
            {
                Game.menuButtons[4] = Game.normal;
                Game.draw.textColor[4] = Color.Black;
            }
            
            
        }
        public void SelectSpecial(MouseState mouseState)
        {
            if (Game.battle.ActiveUnit is Mage)
            {
                
                if (mouseState.LeftButton == ButtonState.Pressed && !Game.releaseWait)
                {
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    Rectangle one = new Rectangle(565, 673, 161, 27);
                    Rectangle two = new Rectangle(565, 646, 161, 27);
                    Rectangle three = new Rectangle(565, 619, 161, 27);
                    Rectangle four = new Rectangle(565, 700, 161, 27);
                    Rectangle five = new Rectangle(565, 727, 161, 27);
                    Rectangle six = new Rectangle(565, 754, 161, 27);
                    if (one.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialSel;
                        Game.specialButtons[1] = Game.specialUn;
                        Game.specialButtons[2] = Game.specialUn;
                        Game.specialButtons[3] = Game.specialUn;
                        Game.specialButtons[4] = Game.specialUn;
                        Game.specialButtons[5] = Game.specialUn;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial1(Game.battle);
                    }
                    else if (two.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialUn;
                        Game.specialButtons[1] = Game.specialSel;
                        Game.specialButtons[2] = Game.specialUn;
                        Game.specialButtons[3] = Game.specialUn;
                        Game.specialButtons[4] = Game.specialUn;
                        Game.specialButtons[5] = Game.specialUn;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial2(Game.battle);
                    }
                    else if (three.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialUn;
                        Game.specialButtons[1] = Game.specialUn;
                        Game.specialButtons[2] = Game.specialSel;
                        Game.specialButtons[3] = Game.specialUn;
                        Game.specialButtons[4] = Game.specialUn;
                        Game.specialButtons[5] = Game.specialUn;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial3(Game.battle);
                    }
                    else if (four.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialUn;
                        Game.specialButtons[1] = Game.specialUn;
                        Game.specialButtons[2] = Game.specialUn;
                        Game.specialButtons[3] = Game.specialSel;
                        Game.specialButtons[4] = Game.specialUn;
                        Game.specialButtons[5] = Game.specialUn;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial4(Game.battle);
                    }
                    else if (five.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialUn;
                        Game.specialButtons[1] = Game.specialUn;
                        Game.specialButtons[2] = Game.specialUn;
                        Game.specialButtons[3] = Game.specialUn;
                        Game.specialButtons[4] = Game.specialSel;
                        Game.specialButtons[5] = Game.specialUn;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial5(Game.battle);
                    }
                    else if (six.Contains(mousePos))
                    {
                        Game.releaseWait = true;
                        Game.specialButtons[0] = Game.specialUn;
                        Game.specialButtons[1] = Game.specialUn;
                        Game.specialButtons[2] = Game.specialUn;
                        Game.specialButtons[3] = Game.specialUn;
                        Game.specialButtons[4] = Game.specialUn;
                        Game.specialButtons[5] = Game.specialSel;

                        Game.draw.specialColor[0] = Color.White;
                        Game.draw.specialColor[1] = Color.White;
                        Game.draw.specialColor[2] = Color.White;
                        Game.draw.specialColor[3] = Color.White;
                        Game.draw.specialColor[4] = Color.White;
                        Game.draw.specialColor[5] = Color.White;
                        Game.battle.ActiveUnit.SelectSpecial6(Game.battle);
                    }
                }
            }
                else
                {
                    Rectangle one = new Rectangle(565, 700, 161, 27);
                    Rectangle two = new Rectangle(565, 727, 161, 27);
                    Rectangle three = new Rectangle(565, 754, 161, 27);

                //button clicks
                    if (mouseState.LeftButton == ButtonState.Pressed && !Game.releaseWait)
                    {
                        Point mousePos = new Point(mouseState.X, mouseState.Y);
                        if (one.Contains(mousePos))
                        {
                            Game.releaseWait = true;
                            if (ManaEvaluator.SpecialAllowed(Game.battle.ActiveUnit, 1))
                            {

                                
                                Game.specialButtons[0] = Game.specialSel;
                                Game.specialButtons[1] = Game.specialUn;
                                Game.specialButtons[2] = Game.specialUn;


                                Game.draw.specialColor[0] = Color.White;
                                Game.draw.specialColor[1] = Color.White;
                                Game.draw.specialColor[2] = Color.White;
                                Game.battle.DeselectSpecialNumber();
                                Game.battle.ActiveUnit.SelectSpecial1(Game.battle);
                                Game.battle.SelectSpecialNumber(1);
                            }
                            else
                            {
                                Game.sfx.PlayBuzzer();
                                Game.sfx.PlayBuzzer();
                            }

                        }
                        else if (two.Contains(mousePos))
                        {
                            Game.releaseWait = true;
                            if (ManaEvaluator.SpecialAllowed(Game.battle.ActiveUnit, 2))
                            {
                                Game.specialButtons[0] = Game.specialUn;
                                Game.specialButtons[1] = Game.specialSel;
                                Game.specialButtons[2] = Game.specialUn;


                                Game.draw.specialColor[0] = Color.White;
                                Game.draw.specialColor[1] = Color.White;
                                Game.draw.specialColor[2] = Color.White;
                                Game.battle.DeselectSpecialNumber();
                                Game.battle.ActiveUnit.SelectSpecial2(Game.battle);
                                Game.battle.SelectSpecialNumber(2);
                            }
                            else
                            {
                                Game.sfx.PlayBuzzer();
                                Game.sfx.PlayBuzzer();
                            }

                        }
                        else if (three.Contains(mousePos))
                        {
                            Game.releaseWait = true;
                            if (ManaEvaluator.SpecialAllowed(Game.battle.ActiveUnit, 3))
                            {
                                Game.specialButtons[0] = Game.specialUn;
                                Game.specialButtons[1] = Game.specialUn;
                                Game.specialButtons[2] = Game.specialSel;


                                Game.draw.specialColor[0] = Color.White;
                                Game.draw.specialColor[1] = Color.White;
                                Game.draw.specialColor[2] = Color.White;
                                Game.battle.DeselectSpecialNumber();
                                Game.battle.ActiveUnit.SelectSpecial3(Game.battle);
                                Game.battle.SelectSpecialNumber(3);
                            }
                            else
                            {
                                Game.sfx.PlayBuzzer();
                                Game.sfx.PlayBuzzer();
                            }

                        }
                    }
                
            }
        }
        public void SpecialButtons()
        {

            if (!Game.battle.specialMode && !Game.battle.MoveMode && !Game.battle.AttackMode)
            {
                Game.releaseWait = true;
                Game.isSpecial = true;
                Game.move = Game.move_grey;
                Game.attack = Game.attack_grey;
                Game.defend = Game.defend_grey;
                Game.item = Game.item_grey;
                Game.pass = Game.pass_grey;
                Game.special = Game.special_invert;
                Game.battle.SelectSpecial();

            }
           else if(Game.battle.specialMode && !Game.battle.MoveMode && !Game.battle.AttackMode)
            {
                Game.releaseWait = true;
                Game.isSpecial = false;
                Game.battle.DeselectSpecialNumber();
                Game.battle.DeselectSpecial();
                Game.specialAttack = false;
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


            }
           
        }


        public void optionButtons(MouseState mouseState)
        {
            Game.ismenu = false;
            

            Rectangle back = new Rectangle(500, 475, 116, 27);
            Rectangle textClick = new Rectangle(25, 433, 141, 33);
            Rectangle fullClick = new Rectangle(25, 337, 141, 33);

            Rectangle res1Click = new Rectangle(625, 264, 141, 33);
            Rectangle res2Click = new Rectangle(750, 264, 141, 33);

            Rectangle yesClick = new Rectangle(625, 227, 141, 33);
            Rectangle noClick = new Rectangle(750, 227, 141, 33);

            Rectangle volumemuteclick = new Rectangle(625, 335, 141, 33);
            Rectangle volume1click = new Rectangle(750, 335, 141, 33);
            Rectangle volume2click = new Rectangle(875, 335, 141, 33);
            Rectangle volume3click = new Rectangle(1000, 335, 141, 33);
            Rectangle volumemaxclick = new Rectangle(1125, 335, 141, 33);

            Rectangle sfxmuteclick = new Rectangle(625, 367, 141, 33);
            Rectangle sfx1click = new Rectangle(750, 367, 141, 33);
            Rectangle sfx2click = new Rectangle(875, 367, 141, 33);
            Rectangle sfx3click = new Rectangle(1000, 367, 141, 33);
            Rectangle sfxmaxclick = new Rectangle(1125, 367, 141, 33);

            bool backMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(back);

            if (mouseState.LeftButton == ButtonState.Pressed && !Game.releaseWait)
            {

                Point mousePos = new Point(mouseState.X, mouseState.Y);
                //full screen
                if (yesClick.Contains(mousePos))
                {
                    
                    Game.graphics.IsFullScreen = true;
                    Game.graphics.ApplyChanges();
                    Game.optionsButtons[6] = Game.normal;
                    Game.optionsButtons[7] = Game.notSelected;
                    Game.isFullScreen = true;
                    Game.releaseWait = true;
                }
                else if (noClick.Contains(mousePos))
                {


                    Game.graphics.IsFullScreen = false;
                    Game.graphics.ApplyChanges();
                    Game.optionsButtons[6] = Game.notSelected;
                    Game.optionsButtons[7] = Game.normal;
                    Game.isFullScreen = false;
                    Game.releaseWait = true;
                }
                //resolution
                if (res1Click.Contains(mousePos))
                {
                    Game.graphics.PreferredBackBufferHeight = 800;
                    Game.graphics.PreferredBackBufferWidth = 1280;
                    Game.graphics.ApplyChanges();
                    Game.optionsButtons[8] = Game.normal;
                    Game.optionsButtons[9] = Game.notSelected;
                    Game.releaseWait = true;
                }
                else if (res2Click.Contains(mousePos))
                {
                    Game.graphics.PreferredBackBufferHeight = 800;
                    Game.graphics.PreferredBackBufferWidth = 1500;
                    Game.graphics.ApplyChanges();
                    Game.optionsButtons[8] = Game.notSelected;
                    Game.optionsButtons[9] = Game.normal;
                    Game.releaseWait = true;
                }
                //volume
                if (volumemuteclick.Contains(mousePos))
                {


                    Game.optionsButtons[13] = Game.normal;
                    Game.optionsButtons[14] = Game.notSelected;
                    Game.optionsButtons[15] = Game.notSelected;
                    Game.optionsButtons[16] = Game.notSelected;
                    Game.optionsButtons[17] = Game.notSelected;

                    Game.engine.GetCategory("Music").SetVolume(0f);
                    Game.releaseWait = true;
                }
                else if (volume1click.Contains(mousePos))
                {


                    Game.optionsButtons[13] = Game.notSelected;
                    Game.optionsButtons[14] = Game.normal;
                    Game.optionsButtons[15] = Game.notSelected;
                    Game.optionsButtons[16] = Game.notSelected;
                    Game.optionsButtons[17] = Game.notSelected;
                    Game.engine.GetCategory("Music").SetVolume(.25f*2);
                    Game.releaseWait = true;
                }
                else if (volume2click.Contains(mousePos))
                {

                    Game.optionsButtons[13] = Game.notSelected;
                    Game.optionsButtons[14] = Game.notSelected;
                    Game.optionsButtons[15] = Game.normal;
                    Game.optionsButtons[16] = Game.notSelected;
                    Game.optionsButtons[17] = Game.notSelected;
                    Game.engine.GetCategory("Music").SetVolume(.5f * 2);
                    Game.releaseWait = true;
                }
               else if (volume3click.Contains(mousePos))
                {

                    Game.optionsButtons[13] = Game.notSelected;
                    Game.optionsButtons[14] = Game.notSelected;
                    Game.optionsButtons[15] = Game.notSelected;
                    Game.optionsButtons[16] = Game.normal;
                    Game.optionsButtons[17] = Game.notSelected;
                    Game.engine.GetCategory("Music").SetVolume(.75f * 2);
                    Game.releaseWait = true;
                }
                else if (volumemaxclick.Contains(mousePos))
                {

                    Game.optionsButtons[13] = Game.notSelected;
                    Game.optionsButtons[14] = Game.notSelected;
                    Game.optionsButtons[15] = Game.notSelected;
                    Game.optionsButtons[16] = Game.notSelected;
                    Game.optionsButtons[17] = Game.normal;
                    Game.engine.GetCategory("Music").SetVolume(1f * 2);
                    Game.releaseWait = true;
                }

                //sound effects
                if (sfxmuteclick.Contains(mousePos))
                {


                    Game.optionsButtons[18] = Game.normal;
                    Game.optionsButtons[19] = Game.notSelected;
                    Game.optionsButtons[20] = Game.notSelected;
                    Game.optionsButtons[21] = Game.notSelected;
                    Game.optionsButtons[22] = Game.notSelected;
                    
                    Game.sfx.setfxfvolume(0f);
                    
                    Game.releaseWait = true;

                }
                else if (sfx1click.Contains(mousePos))
                {


                    Game.optionsButtons[18] = Game.notSelected;
                    Game.optionsButtons[19] = Game.normal;
                    Game.optionsButtons[20] = Game.notSelected;
                    Game.optionsButtons[21] = Game.notSelected;
                    Game.optionsButtons[22] = Game.notSelected;
                    Game.sfx.setfxfvolume(.25f*2);
                    Game.sfx.PlayBuzzer();
                    Game.sfx.PlayBuzzer();
                    Game.releaseWait = true;

                }
                else if (sfx2click.Contains(mousePos))
                {

                    Game.optionsButtons[18] = Game.notSelected;
                    Game.optionsButtons[19] = Game.notSelected;
                    Game.optionsButtons[20] = Game.normal;
                    Game.optionsButtons[21] = Game.notSelected;
                    Game.optionsButtons[22] = Game.notSelected;
                    Game.sfx.setfxfvolume(.5f * 2);
                    Game.sfx.PlayBuzzer();
                    Game.sfx.PlayBuzzer();
                    Game.releaseWait = true;
                }
                else if (sfx3click.Contains(mousePos))
                {

                    Game.optionsButtons[18] = Game.notSelected;
                    Game.optionsButtons[19] = Game.notSelected;
                    Game.optionsButtons[20] = Game.notSelected;
                    Game.optionsButtons[21] = Game.normal;
                    Game.optionsButtons[22] = Game.notSelected;
                    Game.sfx.setfxfvolume(.75f * 2);
                    Game.sfx.PlayBuzzer();
                    Game.sfx.PlayBuzzer();
                    Game.releaseWait = true;
                }
                else if (sfxmaxclick.Contains(mousePos))
                {

                    Game.optionsButtons[18] = Game.notSelected;
                    Game.optionsButtons[19] = Game.notSelected;
                    Game.optionsButtons[20] = Game.notSelected;
                    Game.optionsButtons[21] = Game.notSelected;
                    Game.optionsButtons[22] = Game.normal;
                    Game.sfx.setfxfvolume(.1f * 2);
                    Game.sfx.PlayBuzzer();
                    Game.sfx.PlayBuzzer();
                    Game.releaseWait = true;
                }
                //back
                if (back.Contains(mousePos))
                {
                    Game.ismenu = true;
                    Game.isOptions = false;

                }
            }
            if (backMouse && Game.optionsButtons[5] == Game.normal)
            {

                Game.optionsButtons[5] = Game.invert;
                Game.draw.optionsColor[5] = Color.White;
            }
            else if (!backMouse)
            {
                Game.optionsButtons[5] = Game.normal;
                Game.draw.optionsColor[5] = Color.Black;
            }
        }
    }

}
