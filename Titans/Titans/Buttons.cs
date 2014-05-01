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
        public void InGameButtons(MouseState mouseState)
        {
            Rectangle resume = new Rectangle( 700, 227,116, 27);
            Rectangle exit = new Rectangle( 700, 400,116, 27);

            bool resumeMouse = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(resume);
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

            }

            if (mouseState.LeftButton == ButtonState.Pressed && !Game.releaseWait)
            {

                Point mousePos = new Point(mouseState.X, mouseState.Y);
                if (exit.Contains(mousePos))
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

        public void SpecialButtons()
        {

        }
    }

}
