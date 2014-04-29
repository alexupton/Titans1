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
using System.Drawing;
using System.Windows.Forms;

namespace Titans
{
    public class ContentLoader
    {
        public Game1 game { get; set; }


        public ContentLoader(Game1 currentGame)
        {
            game = currentGame;
            game.sfx = new AudioManager(game);
            //var form = (System.Windows.Forms.Form)System.Windows.Forms.Control.FromHandle(game.Window.Handle);
            //form.Location = new System.Drawing.Point(700,1200);
        }

        public void MainMenu()
        {
            //Start main menu music and loop it
            if (!game.musicStarted)
            {
                Song title = game.Content.Load<Song>(@"Music\Title Theme");
                MediaPlayer.Play(title);
                MediaPlayer.IsRepeating = true;
                MediaPlayer.Volume = 1.0f;
                game.musicStarted = true;
            }
            
	    //Load all images for the main menu buttons
            game.spriteBatch = new SpriteBatch(game.GraphicsDevice);

            game.quick_battle = game.Content.Load<Texture2D>(@"images\Quick Battle");
            game.campaign = game.Content.Load<Texture2D>(@"images\Campaign");
            game.campaign_invert = game.Content.Load<Texture2D>(@"images\Campaign(Inverted)");
            game.custom_battle = game.Content.Load<Texture2D>(@"images\Custom Battle");
            game.custom_battle_invert = game.Content.Load<Texture2D>(@"images\Custom Battle(Inverted)");
            game.options = game.Content.Load<Texture2D>(@"images\Options");
            game.options_invert = game.Content.Load<Texture2D>(@"images\Options(Inverted)");
            game.exit = game.Content.Load<Texture2D>(@"images\Exit");
            game.quick_battle_invert = game.Content.Load<Texture2D>(@"images\Quick Battle(Inverted)");
            game.exit2 = game.Content.Load<Texture2D>(@"images\Exit(Inverted)");

            game.campaigntemp = game.campaign;
            game.custom_battletemp = game.custom_battle;
            game.optionstemp = game.options;
            game.quick_temp = game.quick_battle;
            game.exit_temp = game.exit;
        }
        
	
        public void CampaignMenu()
        {
            //Load all buttons for the campaign menu once campaign is clicked
            game.newGame = game.Content.Load<Texture2D>(@"images\New Game");
            game.newGame_invert = game.Content.Load<Texture2D>(@"images\New Game(Inverted)");
            game.loadGame = game.Content.Load<Texture2D>(@"images\Load Game");
            game.loadGame_invert = game.Content.Load<Texture2D>(@"images\Load Game(Inverted)");
            game.back = game.Content.Load<Texture2D>(@"images\Back");
            game.back_invert = game.Content.Load<Texture2D>(@"images\Back(Inverted)");
            game.backtemp = game.back;
            game.newGametemp = game.newGame;
            game.loadGametemp = game.loadGame;
        }

        public void OptionsMenu()
        {
            //Content.Unload();
	    //Load all buttons for the options menu when the options button is clicked
            game.resolution = game.Content.Load<Texture2D>(@"images\Resolution");
            game.textSpeed = game.Content.Load<Texture2D>(@"images\Text Speed");
            game.musicLevel = game.Content.Load<Texture2D>(@"images\Music Level");
            game.soundEffects = game.Content.Load<Texture2D>(@"images\Sound Effects");
            game.back = game.Content.Load<Texture2D>(@"images\Back");
            game.back_invert = game.Content.Load<Texture2D>(@"images\Back(Inverted)");
            game.credits = game.Content.Load<Texture2D>(@"images\Credits");
            game.res1_unselected = game.Content.Load<Texture2D>(@"images\1200-800(Selected)");
            game.res1 = game.Content.Load<Texture2D>(@"images\1200-800(Not Selected)");
            game.res2_unselected = game.Content.Load<Texture2D>(@"images\1500-800(Selected)");
            game.res2 = game.Content.Load<Texture2D>(@"images\1500-800(Not Selected)");
            game.fullScreen_unselected = game.Content.Load<Texture2D>(@"images\Full Screen(Selected)");
            game.fullScreen = game.Content.Load<Texture2D>(@"images\Full Screen(Not Selected)");
            game.fullScreen_invert = game.Content.Load<Texture2D>(@"images\Full Screen(Selected)");
            game.yes_invert = game.Content.Load<Texture2D>(@"images\Yes(Selected)");
            game.yes = game.Content.Load<Texture2D>(@"images\Yes(Not Selected)");
            game.no_invert = game.Content.Load<Texture2D>(@"images\No(Selected)");
            game.no = game.Content.Load<Texture2D>(@"images\No(Not Selected)");
            game.slow_unselected = game.Content.Load<Texture2D>(@"images\Slow(Selected)");
            game.slow = game.Content.Load<Texture2D>(@"images\Slow(Not Selected)");
            game.slowtemp = game.slow;
            game.regular_unselected = game.Content.Load<Texture2D>(@"images\Regular(Selected)");
            game.regular = game.Content.Load<Texture2D>(@"images\Regular(Not Selected)");
            game.regulartemp = game.regular_unselected;
            game.fast_unselected = game.Content.Load<Texture2D>(@"images\Fast(Selected)");
            game.fast = game.Content.Load<Texture2D>(@"images\Fast(Not Selected)");
            game.fasttemp = game.fast;
            game.volmute_unselected = game.Content.Load<Texture2D>(@"images\Mute(Selected)");
            game.volmute = game.Content.Load<Texture2D>(@"images\Mute(Not Selected)");
            game.vollevel1_unselected = game.Content.Load<Texture2D>(@"images\25%(Selected)");
            game.vollevel1 = game.Content.Load<Texture2D>(@"images\25%(Not Selected)");
            game.vollevel2_unselected = game.Content.Load<Texture2D>(@"images\50%(Selected)");
            game.vollevel2 = game.Content.Load<Texture2D>(@"images\50%(Not Selected)");
            game.vollevel3_unselected = game.Content.Load<Texture2D>(@"images\75%(Selected)");
            game.vollevel3 = game.Content.Load<Texture2D>(@"images\75%(Not Selected)");
            game.vollevelMax_unselected = game.Content.Load<Texture2D>(@"images\100%(Selected)");
            game.vollevelMax = game.Content.Load<Texture2D>(@"images\100%(Not Selected)");

            game.apply = game.Content.Load<Texture2D>(@"images\Apply");

            game.mute_unselected = game.Content.Load<Texture2D>(@"images\Mute(Selected)");
            game.mute = game.Content.Load<Texture2D>(@"images\Mute(Not Selected)");
            game.level1_unselected = game.Content.Load<Texture2D>(@"images\25%(Selected)");
            game.level1 = game.Content.Load<Texture2D>(@"images\25%(Not Selected)");
            game.level2_unselected = game.Content.Load<Texture2D>(@"images\50%(Selected)");
            game.level2 = game.Content.Load<Texture2D>(@"images\50%(Not Selected)");
            game.level3_unselected = game.Content.Load<Texture2D>(@"images\75%(Selected)");
            game.level3 = game.Content.Load<Texture2D>(@"images\75%(Not Selected)");
            game.levelMax_unselected = game.Content.Load<Texture2D>(@"images\100%(Selected)");
            game.levelMax = game.Content.Load<Texture2D>(@"images\100%(Not Selected)");
            game.apply = game.Content.Load<Texture2D>(@"images\Apply");

            game.volmutetemp = game.volmute_unselected;
            game.vol1temp = game.vollevel1_unselected;
            game.vol2temp = game.vollevel2_unselected;
            game.vol3temp = game.vollevel3_unselected;
            game.volmaxtemp = game.vollevelMax_unselected;
            game.vollevelMax_unselected = game.vollevelMax;

            game.mutetemp = game.mute_unselected;
            game.level1temp = game.level1_unselected;
            game.level2temp = game.level2_unselected;
            game.level3temp = game.level3_unselected;
            game.maxtemp = game.levelMax_unselected;
            game.levelMax_unselected = game.levelMax;

            game.fullScreenTemp = game.fullScreen;
            game.backtemp = game.back;
            game.yestemp = game.yes_invert;

            game.res1temp = game.res1_unselected;
            game.res2temp = game.res2;
           
            game.notemp = game.no_invert;
            game.no_invert = game.no;
        }

	//Load the demo, currently only map we can have running when clicking quick battle
        public void Demo()
        {
            game.Content.Unload();
            //Access file where map data is located
            Map mainMap = MapLoaderUltimate.LoadMap(@"Content\TestMap.bmp", @"Content\Demo2.txt");
            //Read map data and translate that to tiles and units placed on-screen
            game.battle = new Battle(mainMap);
            game.battle.GameUI = game;
            //
            game.battle.AIControlled = true;
            //
            game.battle.RollInitiative();
            game.lastSelectedTile = game.battle.BattleMap.GetTileAt(0, 0);
            
            //Get the active units data to display in the information box
            game.unit = game.battle.ActiveUnit.GetType();
            game.hp = game.battle.ActiveUnit.HP.ToString();
            game.range = game.battle.ActiveUnit.Range.ToString();
            game.defense = game.battle.ActiveUnit.Defense.ToString();
            game.speed = game.battle.ActiveUnit.Speed.ToString();
            game.mp = game.battle.ActiveUnit.MP.ToString();
            game.attackText = game.battle.ActiveUnit.Attack.ToString();
            game.moveText = game.battle.ActiveUnit.AP.ToString();
            
            game.SetOffsetValue(game.battle.ActiveUnit.Location[0] * -55 + 750, game.battle.ActiveUnit.Location[1] * -55 + 400);
            game.nextUnits = new List<string>();
            for (int i = game.battle.QueuePosition + 1; i < game.battle.QueuePosition + 6; i++)
            {
                if (i < game.battle.BattleQueue.Count)
                {
                    string belongsTo;
                    if (game.battle.BattleQueue.ElementAt(i).isPlayerUnit)
                    {
                        belongsTo = "Player 1";
                    }
                    else
                    {
                        belongsTo = "Player 2";
                    }

                    string unitText = belongsTo + ": " + game.battle.BattleQueue.ElementAt(i).GetType() + " " + game.battle.BattleQueue.ElementAt(i).HP.ToString() + " HP";
                    game.nextUnits.Add(unitText);
                }

            }
            // Audio objects



            game.engine = new AudioEngine("Content\\Music\\Battle1.xgs");
            game.soundBank = new SoundBank(game.engine, "Content\\Music\\Sound Bank.xsb");
            game.waveBank = new WaveBank(game.engine, "Content\\Music\\Wave Bank.xwb");
            //Start battle music in-game
            game.cue = game.soundBank.GetCue("Battle");
            AudioCategory music = game.engine.GetCategory("Music");
            music.SetVolume(3.0f);
            game.cue.Play();
            game.sfx = new AudioManager(game);


            //Tiles
            game.Bridge1 = game.Content.Load<Texture2D>(@"images\Tiles\Bridge1");
            game.Grass1 = game.Content.Load<Texture2D>(@"images\Tiles\Grass1");
            game.Sand1_Height1 = game.Content.Load<Texture2D>(@"images\Tiles\Sand1_Height1");
            game.Sand1_Height2 = game.Content.Load<Texture2D>(@"images\Tiles\Sand1_Height2");
            game.Sand1_Height3 = game.Content.Load<Texture2D>(@"images\Tiles\Sand1_Height3");
            game.Sand1_Height4 = game.Content.Load<Texture2D>(@"images\Tiles\Sand1_Height4");
            game.Water1 = game.Content.Load<Texture2D>(@"images\Tiles\Water1");
            game.Highlight = game.Content.Load<Texture2D>(@"images\Tiles\BlackBorder");
            game.RedHighlight = game.Content.Load<Texture2D>(@"images\RedBorder");
            game.GreenHighlight = game.Content.Load<Texture2D>(@"images\GreenBorder");
            //Units
            game.Artillery = game.Content.Load<Texture2D>(@"images\Units\Artillery");
            game.Defender = game.Content.Load<Texture2D>(@"images\Units\Defender");
            game.Mage = game.Content.Load<Texture2D>(@"images\Units\Mage");
            game.Ranger = game.Content.Load<Texture2D>(@"images\Units\Ranger");
            game.Soldier = game.Content.Load<Texture2D>(@"images\Units\Soldier");
            game.frameSize.X = 32;
            game.frameSize.Y = 48;

            //Overlay Ui
            game.UI = game.Content.Load<Texture2D>(@"images\UI Fixed");
            game.move = game.Content.Load<Texture2D>(@"images\Move(Selectable)");
            game.move_invert = game.Content.Load<Texture2D>(@"images\Move(Selected)");
            game.move_grey = game.Content.Load<Texture2D>(@"images\Move(Un-Selectable)");
            game.movetrue = game.move;
            game.pass = game.Content.Load<Texture2D>(@"images\Pass(Selectable)");
            game.pass_invert = game.Content.Load<Texture2D>(@"images\Pass(Selected)");
            game.pass_grey = game.Content.Load<Texture2D>(@"images\Pass(Un-Selectable)");
            game.passtrue = game.pass;

            game.attack = game.Content.Load<Texture2D>(@"images\Attack(Selectable)");
            game.attack_invert = game.Content.Load<Texture2D>(@"images\Attack(Selected)");
            game.attack_grey = game.Content.Load<Texture2D>(@"images\Attack(Un-Selectable)");
            game.attacktrue = game.attack;

            game.defend = game.Content.Load<Texture2D>(@"images\Defend(Selectable)");
            game.defend_invert = game.Content.Load<Texture2D>(@"images\Defend(Selected)");
            game.defend_grey = game.Content.Load<Texture2D>(@"images\Defend(Un-Selectable)");
            game.defendtrue = game.defend;

            game.special = game.Content.Load<Texture2D>(@"images\Special Attack(Selectable)");
            game.special_invert = game.Content.Load<Texture2D>(@"images\Special Attack(Selected)");
            game.special_grey = game.Content.Load<Texture2D>(@"images\Special Attack(Un-Selectable)");
            game.specialtrue = game.special;

            game.item = game.Content.Load<Texture2D>(@"images\Item(Selectable)");
            game.item_invert = game.Content.Load<Texture2D>(@"images\Item(Selected)");
            game.item_grey = game.Content.Load<Texture2D>(@"images\Item(Un-Selectable)");
            game.itemtrue = game.item;


            game.UI = game.Content.Load<Texture2D>(@"images\UI Fixed");

            //in Game Menu items
            game.menuButtons = new Texture2D[5];
            game.menuBox = game.Content.Load<Texture2D>(@"images\Menu(InGame)\Menu Box");
            game.normal = game.Content.Load<Texture2D>(@"images\Menu(InGame)\Normal");
            for (int i = 0; i < 5; i++)
            {
                game.menuButtons[i]=game.normal;
            }
            game.invert = game.Content.Load<Texture2D>(@"images\Menu(InGame)\Invert");
            game.notSelected = game.Content.Load<Texture2D>(@"images\Menu(InGame)\Not Selected");
            game.normalTemp = game.normal;
            //sheetSize.X = 128;
            //sheetSize.Y = 192;

            game.millisecondsPerFrame = 100;

            //fonts
            game.text = game.Content.Load<SpriteFont>(@"Courier");
            game.smallText = game.Content.Load<SpriteFont>(@"CourierSmall");
            game.bigText = game.Content.Load<SpriteFont>(@"CourierBig");
        }
      
          
    }
}
