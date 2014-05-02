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
    public class Buttons
    {
        public Game1 Game { get; set; }
        
       

        public Buttons(Game1 game)
        {
            Game = game;
            
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

        public void SpecialButtons(MouseState mouseState,Rectangle specialClick)
        {

            if (!Game.battle.specialMode)
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
            else
            {
                    Game.releaseWait = true;
                    Game.isSpecial = false;
                    Game.move = Game.movetrue;
                    Game.attack = Game.attacktrue;
                    Game.defend = Game.defendtrue;
                    Game.item = Game.itemtrue;
                    Game.pass = Game.passtrue;
                    Game.special = Game.specialtrue;
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
                    //Game.engine.GetCategory("SFX").SetVolume(0f);
                    Game.sfx.PlayBuzzer();
                    Game.releaseWait = true;

                }
                else if (sfx1click.Contains(mousePos))
                {


                    Game.optionsButtons[18] = Game.notSelected;
                    Game.optionsButtons[19] = Game.normal;
                    Game.optionsButtons[20] = Game.notSelected;
                    Game.optionsButtons[21] = Game.notSelected;
                    Game.optionsButtons[22] = Game.notSelected;
                    //Game.engine.GetCategory("SFX").SetVolume(.25f);
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
                    //Game.engine.GetCategory("SFX").SetVolume(.5f);
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
                    //Game.engine.GetCategory("SFX").SetVolume(.75f);
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
                    //Game.engine.GetCategory("SFX").SetVolume(1f);
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
