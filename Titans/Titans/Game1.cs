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
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D quick_battle;
        public Texture2D campaign;
        public Texture2D campaign_invert;
        public Texture2D campaigntemp;
        public Texture2D custom_battle;
        public Texture2D custom_battle_invert;
        public Texture2D custom_battletemp;
        public Texture2D options;
        public Texture2D options_invert;
        public Texture2D optionstemp;
        public Texture2D exit;
        public Texture2D exit_temp;
        public Texture2D exit2;
        public Texture2D quick_battle_invert;
        public Texture2D quick_temp;
        public Texture2D Bridge1;
        public Texture2D Grass1;
        public Texture2D Sand1_Height1;
        public Texture2D Sand1_Height2;
        public Texture2D Sand1_Height3;
        public Texture2D Sand1_Height4;
        public Texture2D Water1;
        public Texture2D UI;
        public Texture2D Highlight;
        public Texture2D RedHighlight;
        public Texture2D GreenHighlight;
        public Texture2D BlueHighlight;
        public Texture2D move;
        public Texture2D movetrue;
        public Texture2D move_invert;
        public Texture2D move_grey;
        public Texture2D pass;
        public Texture2D passtrue;
        public Texture2D pass_invert;
        public Texture2D pass_grey;
        public Texture2D attack;
        public Texture2D attacktrue;
        public Texture2D attack_invert;
        public Texture2D attack_grey;
        public Texture2D defend;
        public Texture2D defendtrue;
        public Texture2D defend_invert;
        public Texture2D defend_grey;
        public Texture2D special;
        public Texture2D specialtrue;
        public Texture2D special_invert;
        public Texture2D special_grey;
        public Texture2D item;
        public Texture2D itemtrue;
        public Texture2D item_invert;
        public Texture2D item_grey;
        public Texture2D newGame;
        public Texture2D newGame_invert;
        public Texture2D newGametemp;
        public Texture2D loadGame;
        public Texture2D loadGame_invert;
        public Texture2D loadGametemp;
        public Texture2D resolution;
        public Texture2D textSpeed;
        public Texture2D musicLevel;
        public Texture2D soundEffects;
        public Texture2D credits;
        public Texture2D res1;
        public Texture2D res1temp;
        public Texture2D res1_unselected;
        public Texture2D res2;
        public Texture2D res2temp;
        public Texture2D res2_unselected;
        public Texture2D slow;
        public Texture2D slowtemp;
        public Texture2D slow_unselected;
        public Texture2D regular;
        public Texture2D regulartemp;
        public Texture2D regular_unselected;
        public Texture2D fast;
        public Texture2D fasttemp;
        public Texture2D fast_unselected;
        public Texture2D volmute;
        public Texture2D volmute_unselected;
        public Texture2D volmutetemp;
        public Texture2D vollevel1;
        public Texture2D mutetemp;
        public Texture2D level1temp;
        public Texture2D level2temp;
        public Texture2D level3temp;
        public Texture2D maxtemp;
        public Texture2D vol1temp;
        public Texture2D vol2temp;
        public Texture2D vol3temp;
        public Texture2D volmaxtemp;
        public Texture2D vollevel1_unselected;
        public Texture2D vollevel2;
        public Texture2D vollevel2_unselected;
        public Texture2D vollevel3;
        public Texture2D vollevel3_unselected;
        public Texture2D vollevelMax;
        public Texture2D vollevelMax_unselected;
        public Texture2D mute;
        public Texture2D mute_unselected;
        public Texture2D level1;
        public Texture2D level1_unselected;
        public Texture2D level2;
        public Texture2D level2_unselected;
        public Texture2D level3;
        public Texture2D level3_unselected;
        public Texture2D levelMax;
        public Texture2D levelMax_unselected;
        public Texture2D back;
        public Texture2D back_invert;
        public Texture2D backtemp;
        public Texture2D apply;
        public Texture2D apply_invert;
        public Texture2D fullScreen;
        public Texture2D fullScreen_invert;
        public Texture2D fullScreenTemp;
        public Texture2D fullScreen_unselected;
        public Texture2D yes;
        public Texture2D yes_invert;
        public Texture2D yestemp;
        public Texture2D no;
        public Texture2D no_invert;
        public Texture2D notemp;

        public int[] optionsSettings;

        //ingame menu
        public Texture2D[] menuButtons;
        public Texture2D[] optionsButtons;
        public Texture2D menuBox;
        public Texture2D optionsBox;
        public Texture2D normal;
        public Texture2D invert;
        public Texture2D notSelected;
        public Texture2D normalTemp;

        public Texture2D[] specialButtons;
        public Texture2D specialNor;
        public Texture2D specialSel;
        public Texture2D specialUn;
        //text stuff
        public SpriteFont text;
        public SpriteFont smallText;
        public SpriteFont bigText;
        public SpriteFont specialText;

        Vector2 pos1 = Vector2.Zero;
        public bool optionsMenu;
        public bool campaignmenu;
        public bool isFullScreen = false;

        //Audio
        public AudioEngine engine;
        public SoundBank soundBank;
        public WaveBank waveBank;
        public Cue cue;
        public AudioCategory music;
        public AudioManager sfx;
        

        

        //Sprite Action Time
        public Texture2D Artillery;
        public Texture2D Defender;
        public Texture2D Mage;
        public Texture2D Ranger;
        public Texture2D Soldier;
        public Point frameSize;
        public Point currentFrame;
        public Point sheetSize;
        // Collision data
        //int collisionOffset;

        // Framerate stuff
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;
        int millisecondsPerDamageFrame = 800;
        public int timeSinceLastDamageFrame = 0;
        int millisecondsPerMoveFrame = 50;
        int timeSinceLastMoveFrame = 0;
        public int frameCount = 0;

        //for moving the map
        public int offsetY;
        public int offsetX;

        //text junk
        public string unit;
        public string hp;
        public string range;
        public string defense;
        public string speed;
        public string mp;
        public string attackText;
        public string moveText;
        public List<string> nextUnits;

        //button controls
        public bool releaseWait;
        public bool keyreleasewait;
        public bool rightReleaseWait;

        //various
        public Tile lastSelectedTile;
        public int attackedUnitTrueX;
        public int attackedUnitTrueY;
        public int unitDamage;
        public List<int> splashDamage;
        public List<Tile> splashLocations;

        //bool unitAttacked;
        public bool displayDamage;
        public bool musicStarted;
       public Draw draw;
        ContentLoader load;
        //int frameCount;


        //Global Variables
        public bool wait; //use to hold action until the end of turn
        public bool p1win;
        public bool p2win;
        public Battle battle;
        public bool ismenu=false;
        public bool isOptions = false;
        //bool startTurn;
        public bool mainMenu;
        public bool demo;
        public List<Tile> MoveTiles;
        public bool moveWait;
        public bool tickWait;
        public bool endWait;
        public bool AILock;
        public bool loseMusicStarted = false;
        public Buttons buttons;
        public bool isSpecial = false;
        public bool selfSelect = false;



        public Game1()
        {
            
            graphics = new GraphicsDeviceManager(this);
            this.graphics.PreferredBackBufferHeight = 800;
            this.graphics.PreferredBackBufferWidth = 1500;
            
            this.graphics.IsFullScreen = false;
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            mainMenu = true;
            demo = false;
            offsetY = 0;
            offsetX = 0;
            //startTurn = true;
            releaseWait = false;
            lastSelectedTile = new Tile();
            displayDamage = false;
            splashDamage = new List<int>();
            splashLocations = new List<Tile>();
            wait = false;
            p1win = false;
            p2win = false;
            musicStarted = false;
            MoveTiles = new List<Tile>();
            moveWait = false;
            tickWait = false;
            endWait = false;


            optionsSettings = new int []{2,2,2,5,5};
            specialButtons = new Texture2D[6];
            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;

            draw = new Draw(this);
            load = new ContentLoader(this);
            buttons = new Buttons(this);
            

          

        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            if (mainMenu)
            {
                load.MainMenu();
            }
            else if (campaignmenu)
            {
                load.CampaignMenu();
            }

            else if (optionsMenu)
            {
                load.OptionsMenu();
            }
            else if (demo)
            {
                load.Demo();
            }

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            Content.Unload();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            MouseState mouseState = Mouse.GetState();
            if (mainMenu)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                Rectangle quickClick = new Rectangle((Window.ClientBounds.Width / 2) - (quick_battle.Width / 2), (Window.ClientBounds.Height / 2) - (quick_battle.Height / 2), 141, 33);
                Rectangle campaignClick = new Rectangle((Window.ClientBounds.Width / 2) - (campaign.Width / 2), (Window.ClientBounds.Height / 2) - (quick_battle.Height - 55), 141, 33);
                Rectangle customClick = new Rectangle((Window.ClientBounds.Width / 2) - (custom_battle.Width / 2), (Window.ClientBounds.Height / 2) - (campaign.Height - 95), 175, 36);
                Rectangle optionsClick = new Rectangle((Window.ClientBounds.Width / 2) - (options.Width / 2), (Window.ClientBounds.Height / 2) - (custom_battle.Height - 140), 141, 33);
                Rectangle exitclickableArea = new Rectangle((Window.ClientBounds.Width / 2) - (exit.Width / 2), (Window.ClientBounds.Height - 70), 141, 33);
                

                // mousepos = new Point();
                bool mousestuffs = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(exitclickableArea);
                bool mousequick = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quickClick);
                bool mousecampaign = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(campaignClick);
                bool mousecustom = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(customClick);
                bool mouseoptions = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(optionsClick);

                if (mousestuffs && exit == exit_temp)
                {
                    exit_temp = exit;
                    exit = exit2;
                }
                else if (!mousestuffs && exit != exit_temp)
                {
                    exit = exit_temp;
                }
                //campaign menu button logic
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (campaignClick.Contains(mousePos))
                    {
                        mainMenu = false;
                        campaignmenu = true;
                        LoadContent();
                    }

                }
                //options menu button logic
                if (mouseState.LeftButton == ButtonState.Pressed)
                {
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (optionsClick.Contains(mousePos) && !releaseWait)
                    {
                        releaseWait = true;
                        mainMenu = false;
                        optionsMenu = true;
                        LoadContent();
                    }
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (exitclickableArea.Contains(mousePos))
                    {
                        this.Exit();
                    }

                }
                if (mousecampaign && campaign == campaigntemp)
                {
                    campaigntemp = campaign;
                    campaign = campaign_invert;
                }
                else if (!mousecampaign && campaign != campaigntemp)
                {
                    campaign = campaigntemp;
                }
                if (mousecustom && custom_battle == custom_battletemp)
                {
                    custom_battletemp = custom_battle;
                    custom_battle = custom_battle_invert;
                }
                else if (!mousecustom && custom_battle != custom_battletemp)
                {
                    custom_battle = custom_battletemp;
                }

                if (mouseoptions && options == optionstemp)
                {
                    optionstemp = options;
                    options = options_invert;
                }
                else if (!mouseoptions && options != optionstemp)
                {
                    options = optionstemp;
                }
                // TODO: Add your update logic here
                if (mousequick && quick_battle == quick_temp)
                {
                    quick_temp = quick_battle;
                    quick_battle = quick_battle_invert;
                }
                else if (!mousequick && quick_battle != quick_temp)
                {
                    quick_battle = quick_temp;
                }

                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    // We now know the left mouse button is down and it wasn't down last frame
                    // so we've detected a click
                    // Now find the position 
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (exitclickableArea.Contains(mousePos))
                    {
                        this.Exit();
                    }

                }
                // TODO: Add your update logic here
                if (mousequick && quick_battle == quick_temp)
                {
                    quick_temp = quick_battle;
                    quick_battle = quick_battle_invert;
                }
                else if (!mousequick && quick_battle != quick_temp)
                {
                    quick_battle = quick_temp;
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    // We now know the left mouse button is down and it wasn't down last frame
                    // so we've detected a click
                    // Now find the position 
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (quickClick.Contains(mousePos))
                    {
                        mainMenu = false;
                        demo = true;
                        MediaPlayer.Stop();
                        
                        LoadContent();
                    }

                }
            }
            else if (campaignmenu)
            {
                Rectangle backClick = new Rectangle(15, 755, 141, 33);
                Rectangle newGameClick = new Rectangle(550, 300, 141, 33);
                Rectangle loadGameClick = new Rectangle(550, 343, 141, 33);
                bool mouseback = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(backClick);
                bool mousenew = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(newGameClick);
                bool mouseload = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(loadGameClick);

                if (mouseback && back == backtemp)
                {
                    backtemp = back;
                    back = back_invert;
                }
                else if (!mouseback && back != backtemp)
                {
                    back = backtemp;
                }
                if (mousenew && newGame == newGametemp)
                {
                    newGametemp = newGame;
                    newGame = newGame_invert;
                }
                else if (!mousenew && newGame != newGametemp)
                {
                    newGame = newGametemp;
                }
                if (mouseload && loadGame == loadGametemp)
                {
                    loadGametemp = loadGame;
                    loadGame = loadGame_invert;
                }
                else if (!mouseload && loadGame != loadGametemp)
                {
                    loadGame = loadGametemp;
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (backClick.Contains(mousePos))
                    {
                        mainMenu = true;
                        campaignmenu = false;
                        LoadContent();


                    }

                }
            }
            
            
            //logic or options buttons
                else if (optionsMenu)
            {
               

                Rectangle backClick = new Rectangle(15, 755, 141, 33);
                Rectangle textClick = new Rectangle(25, 433, 141, 33);
                Rectangle fullClick = new Rectangle(25, 337, 141, 33);
                Rectangle res1Click = new Rectangle(206, 385, 141, 33);
                Rectangle res2Click = new Rectangle(360, 385, 141, 33);
                Rectangle yesClick = new Rectangle(206, 337, 141, 33);
                Rectangle noClick = new Rectangle(360, 337, 141, 33);
                Rectangle volumemuteclick = new Rectangle(206, 483, 141, 33);
                Rectangle volume1click = new Rectangle(360, 483, 141, 33);
                Rectangle volume2click = new Rectangle(514, 483, 141, 33);
                Rectangle volume3click = new Rectangle(669, 483, 141, 33);
                Rectangle volumemaxclick = new Rectangle(825, 483, 141, 33);

                Rectangle sfxmuteclick = new Rectangle(206, 533, 141, 33);
                Rectangle sfx1click = new Rectangle(360, 533, 141, 33);
                Rectangle sfx2click = new Rectangle(514, 533, 141, 33);
                Rectangle sfx3click = new Rectangle(669, 533, 141, 33);
                Rectangle sfxmaxclick = new Rectangle(825, 533, 141, 33);

                //int count = 2;
                bool mouseback = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(backClick);
                bool mousefull = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(fullClick);
                
                if(mouseState.LeftButton==ButtonState.Pressed&&!releaseWait)
                {
                    Point mousePos= new Point(mouseState.X,mouseState.Y);
                    if(res1Click.Contains(mousePos))
                    {
                         this.graphics.PreferredBackBufferHeight = 800;
                         this.graphics.PreferredBackBufferWidth = 1280;
                         graphics.ApplyChanges();
                         res1_unselected = res1;
                         res2 = res2_unselected;
                         optionsSettings[1] = 1;
                         releaseWait = true;
                    }
                    else if(res2Click.Contains(mousePos))
                    {
                         this.graphics.PreferredBackBufferHeight = 800;
                         this.graphics.PreferredBackBufferWidth = 1500;
                         graphics.ApplyChanges();
                         res1_unselected = res1temp;
                         res2 = res2temp;
                         optionsSettings[1] = 2;
                         releaseWait = true;
                    }
                }
                if (mouseState.LeftButton == ButtonState.Pressed && !releaseWait)
                {
                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (yesClick.Contains(mousePos))
                    {
                        yes_invert = yes;

                        no_invert = notemp;
                        graphics.IsFullScreen = true;
                        graphics.ApplyChanges();
                        isFullScreen = true;
                        optionsSettings[0] = 1;
                    }
                    else if (noClick.Contains(mousePos))
                    {


                        graphics.IsFullScreen = false;
                        graphics.ApplyChanges();
                        yes_invert = yestemp;

                        no_invert = no;
                        optionsSettings[0] = 2;
                        isFullScreen = false;
                    }
                }

                //volume logic
                if (mouseState.LeftButton == ButtonState.Pressed && !releaseWait)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (volumemuteclick.Contains(mousePos))
                    {

                        
                        volmute_unselected = volmute;
                        vollevel1_unselected = vol1temp;
                        vollevel2_unselected = vol2temp;
                        vollevel3_unselected= vol3temp;
                        vollevelMax_unselected= volmaxtemp;
                        optionsSettings[3] = 1;
                        MediaPlayer.Volume = 0;
                        
                    }
                    if (volume1click.Contains(mousePos))
                    {


                        volmute_unselected = volmutetemp;
                        vollevel1_unselected = vollevel1;
                        vollevel2_unselected = vol2temp;
                        vollevel3_unselected = vol3temp;
                        vollevelMax_unselected= volmaxtemp;
                        optionsSettings[3] = 2;
                        MediaPlayer.Volume = .25f;
                      
                    }
                    if (volume2click.Contains(mousePos))
                    {

                        volmute_unselected = volmutetemp;
                        vollevel1_unselected = vol1temp;
                        vollevel2_unselected = vollevel2;
                        vollevel3_unselected = vol3temp;
                        vollevelMax_unselected = volmaxtemp;
                        optionsSettings[3] = 3;
                        MediaPlayer.Volume = .5f;
                    }
                    if (volume3click.Contains(mousePos))
                    {

                        volmute_unselected = volmutetemp;
                        vollevel1_unselected = vol1temp;
                        vollevel2_unselected = vol2temp;
                        vollevel3_unselected = vollevel3;
                        vollevelMax_unselected = volmaxtemp;
                        optionsSettings[3] = 4;
                        MediaPlayer.Volume = .75f;
                    }
                    if (volumemaxclick.Contains(mousePos))
                    {

                        volmute_unselected = volmutetemp;
                        vollevel1_unselected = vol1temp;
                        vollevel2_unselected = vol2temp;
                        vollevel3_unselected = vol3temp;
                        vollevelMax_unselected = vollevelMax;
                        optionsSettings[3] = 5;
                        MediaPlayer.Volume = 1f;
                    }
                }

               //soundBank effects logic
                if (mouseState.LeftButton == ButtonState.Pressed&&!releaseWait)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (sfxmuteclick.Contains(mousePos))
                    {


                        mute_unselected = mute;
                        level1_unselected = level1temp;
                        level2_unselected = level2temp;
                        level3_unselected = level3temp;
                        levelMax_unselected = maxtemp;

                        sfx.setfxfvolume(0f);
                        optionsSettings[4] = 1;
                        releaseWait = true;

                    }
                    if (sfx1click.Contains(mousePos))
                    {


                        mute_unselected = mutetemp;
                        level1_unselected = level1;
                        level2_unselected = level2temp;
                        level3_unselected = level3temp;
                        levelMax_unselected = maxtemp;

                        sfx.setfxfvolume(0.25f*6f);
                        sfx.PlayBuzzer();
                        optionsSettings[4] = 2;
                        releaseWait = true;

                    }
                    if (sfx2click.Contains(mousePos))
                    {

                        mute_unselected = mutetemp;
                        level1_unselected = level1temp;
                        level2_unselected = level2;
                        level3_unselected = level3temp;
                        levelMax_unselected = maxtemp;
                        sfx.setfxfvolume(.5f * 6f);
                        sfx.PlayBuzzer();
                        optionsSettings[4] = 3;
                        releaseWait = true;
                    }
                    if (sfx3click.Contains(mousePos))
                    {

                        mute_unselected = mutetemp;
                        level1_unselected = level1temp;
                        level2_unselected = level2temp;
                        level3_unselected = level3;
                        levelMax_unselected = maxtemp;
                        sfx.setfxfvolume(.75f * 6f);
                        sfx.PlayBuzzer();
                        optionsSettings[4] = 4;
                        releaseWait = true;
                    }
                    if (sfxmaxclick.Contains(mousePos))
                    {

                        mute_unselected = mutetemp;
                        level1_unselected = level1temp;
                        level2_unselected = level2temp;
                        level3_unselected = level3temp;
                        levelMax_unselected = levelMax;

                        sfx.setfxfvolume(1f);
                        sfx.PlayBuzzer();
                        optionsSettings[4] = 5;
                        
                        releaseWait = true;
                    }
                }
              //full screen logic
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (fullClick.Contains(mousePos))
                    {
                        if (isFullScreen)
                        {
                           
                            graphics.ToggleFullScreen();
                            yes_invert = yestemp;
                            
                            no_invert = no;
                            isFullScreen = false;
                           
                           

                        }
                        else if (!isFullScreen)
                        {
                            
                            yes_invert = yes;
                           
                            no_invert =notemp;
                            graphics.ToggleFullScreen();
                            isFullScreen = true;
                          

                        }
                       
                    }
                }
                

                //back inverter
                if (mouseback && back == backtemp)
                {
                    backtemp = back;
                    back = back_invert;
                }
                else if (!mouseback && back != backtemp)
                {
                    back = backtemp;
                }                
                
                //exits options menu
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (backClick.Contains(mousePos))
                    {
                        mainMenu = true;
                        optionsMenu = false;
                        LoadContent();


                    }

                }

                
            }
            else if (demo && !(p1win || p2win)&&!ismenu&&!isOptions)
            {
                engine.Update();
                sfx.Update();
                if (!battle.AttackMode && !battle.AttackRangeDisplayed)
                {
                    battle.BattleMap.ClearHighlights();
                    battle.BattleMap.ClearBlueHighlights();
                }
                battle.BattleMap.ClearHighlights();

                //boxes for intersecion of buttons

                Rectangle moveclick = new Rectangle(680, 700, 116, 27);
                Rectangle passclick = new Rectangle(796, 700, 116, 27);
                Rectangle attackclick = new Rectangle(680, 727, 116, 27);
                Rectangle defendclick = new Rectangle(796, 727, 116, 27);
                Rectangle specialclick = new Rectangle(680, 754, 116, 27);
                Rectangle itemclick = new Rectangle(796, 754, 116, 27);
                mouseState = Mouse.GetState();
                bool moveenabled = true;
                bool hilightenabled = true;
                //mouse state when on buttons
                bool mousemove = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(moveclick);
                bool mousepass = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(passclick);
                bool mouseattack = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(attackclick);
                bool mousedefend = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(defendclick);
                bool mousespecial = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(specialclick);
                bool mouseitems = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(itemclick);
                Point mousePos = new Point(mouseState.X, mouseState.Y);
                
             

                //lock buttons during wait times
                if (tickWait)
                {
                    LockButtons();
                }
                //Move mode highlighting
                if (battle.MoveMode)
                {
                    //THE ALL IMPORTANT MAP POSITION ESITMATION FUNCTION
                    int X = (int)Math.Round(((double)mousePos.X - (double)offsetX - 20) / (double)55);
                    int Y = (int)Math.Round(((double)mousePos.Y - (double)offsetY - 20) / (double)55);


                    if (X >= 0 && X < battle.BattleMap.Size[0] && Y >= 0 && Y < battle.BattleMap.Size[1])
                    {
                        battle.BattleMap.AddSpecificHighlight(X, Y);
                        lastSelectedTile = battle.BattleMap.GetTileAt(X, Y);
                    }
                    else
                    {
                        battle.BattleMap.AddSpecificHighlight(lastSelectedTile.X, lastSelectedTile.Y);
                    }

                }
                    //AttackMode Highlighting
                else if(battle.AttackMode)
                {

                    int X = (int)Math.Round(((double)mousePos.X - (double)offsetX - 20) / (double)55);
                    int Y = (int)Math.Round(((double)mousePos.Y - (double)offsetY - 20) / (double)55);
                    if (X >= 0 && X < battle.BattleMap.Size[0] && Y >= 0 && Y < battle.BattleMap.Size[1])
                    {
                        battle.BattleMap.AddSpecificHighlight(X, Y);
                        lastSelectedTile = battle.BattleMap.GetTileAt(X, Y);

                        if (battle.BattleMap.GetTileAt(X, Y).IsRedHighlighted && battle.ActiveUnit is Artillery)
                        {
                            List<Tile> splashTiles = AI.GetAllAdjacentTiles(battle.BattleMap, lastSelectedTile);
                            foreach (Tile test in splashTiles)
                            {
                                if (test.hasUnit)
                                {
                                    if (test.TileUnit.isPlayerUnit != battle.ActiveUnit.isPlayerUnit)
                                    {
                                        battle.BattleMap.AddSpecificHighlight(test.X, test.Y);
                                    }
                                }
                            }
                        }
                    }
                }

                //Button clicks

                if (mouseState.LeftButton == ButtonState.Pressed && !releaseWait && !AILock && mouseState.RightButton != ButtonState.Pressed)
                {
                    
                    //select move
                    if (moveclick.Contains(mousePos) && move != move_invert && !moveWait && !tickWait && battle.SelectEnabled)
                    {

                        battle.BattleMap.ClearRedHighlights();
                        releaseWait = true;
                        move = move_invert;
                        attack = attack_grey;
                        defend = defend_grey;
                        item = item_grey;
                        pass = pass_grey;
                        special = special_grey;
                        hilightenabled = false;
                        battle.AttackMode = false;
                        battle.SelectMove();
                        battle.BattleMap.ClearRedHighlights();
                        //frameCount = 0;
                        battle.SelectMove();
                    }
                    //deselect move
                    else if (moveclick.Contains(mousePos) && move == move_invert)
                    {
                        battle.BattleMap.ClearRedHighlights();
                        releaseWait = true;
                        move = movetrue;
                        attack = attacktrue;
                        defend = defendtrue;
                        item = itemtrue;
                        pass = passtrue;
                        special = specialtrue;
                        hilightenabled = true;
                        battle.DeselectMove();
                    }
                    //select attack
                    else if (attackclick.Contains(mousePos) && attack != attack_invert && !tickWait && !moveWait && battle.SelectEnabled)
                    {
                        releaseWait = true;
                        move = move_grey;
                        attack = attack_invert;
                        defend = defend_grey;
                        item = item_grey;
                        pass = pass_grey;
                        special = special_grey;
                        hilightenabled = true;

                        //deselect other modes before selecting attack
                        battle.DeselectMove();
                        battle.SelectAttack();

                        //battle will get rid of attack mode if there are no valid targets
                        if (!battle.AttackMode)
                        {
                            battle.SelectEnabled = true;
                            move = movetrue;
                            attack = attacktrue;
                            defend = defendtrue;
                            item = itemtrue;
                            pass = passtrue;
                            special = specialtrue;
                            hilightenabled = true;
                            sfx.PlayBuzzer();
                        }
                    }
                    //deselect attack
                    else if (attackclick.Contains(mousePos) && attack == attack_invert)
                    {
                        releaseWait = true;
                        move = movetrue;
                        attack = attacktrue;
                        defend = defendtrue;
                        item = itemtrue;
                        pass = passtrue;
                        special = specialtrue;
                        hilightenabled = true;
                        battle.DeselectAttack();
                    }
                    //select pass
                    else if (passclick.Contains(mousePos) && !tickWait && !moveWait && battle.SelectEnabled)
                    {
                        sfx.PlayPassSound(battle.ActiveUnit);
                        pass = pass_invert;
                        timeSinceLastDamageFrame = 0;
                        frameCount = 0;
                        battle.ActiveUnit.AP = 0;
                        releaseWait = true;
                        move = movetrue;
                        attack = attacktrue;
                        defend = defendtrue;
                        item = itemtrue;
                        pass = passtrue;
                        special = specialtrue;
                        hilightenabled = true;

                    }
                    //select defend
                    else if (defendclick.Contains(mousePos) && !wait && !tickWait && !moveWait && battle.SelectEnabled && defend !=defend_grey)
                    {
                        timeSinceLastDamageFrame = 0;
                        wait = true;
                        battle.MoveMode = false;
                        battle.AttackMode = false;
                        releaseWait = true;
                        move = move_grey;
                        attack = attack_grey;
                        defend = defend_invert;
                        item = item_grey;
                        pass = pass_grey;
                        special = special_grey;
                        hilightenabled = true;
                        battle.SelectDefend();
                        frameCount = 0;

                    }
                    //select special 
                    else if (specialclick.Contains(mousePos) && !wait && !tickWait && !moveWait)
                    {
                        buttons.SpecialButtons();
                        
                    }
                    ////deselect if in move or attack mode and defend button clicked
                    //else if (defendclick.Contains(mousePos) && !wait && !tickWait && !moveWait && !battle.SelectEnabled)
                    //{
                    //    battle.MoveMode = false;
                    //    battle.AttackMode = false;
                    //    battle.SelectEnabled = true;
                    //    buttons.SpecialButtons();
                    //    releaseWait = true;
                    //    ResetButtons();
                    //}


                    //confirm move
                    if (mouseState.LeftButton == ButtonState.Pressed && battle.MoveMode && !releaseWait && battle.BattleMap.GetTileAt(lastSelectedTile.X, lastSelectedTile.Y).IsRedHighlighted)
                    {


                        moveWait = true;
                        move = movetrue;
                        attack = attacktrue;
                        defend = defendtrue;
                        item = itemtrue;
                        pass = passtrue;
                        special = specialtrue;
                        hilightenabled = true;
                        releaseWait = true;
                        battle.DeselectMove();
                        battle.StartMove(lastSelectedTile);
                        battle.BattleMap.ClearRedHighlights();
                        frameCount = 0;
                        if (battle.ActiveUnit.AP <= 1)
                        {
                            LockButtons();
                        }


                    }
                    else if (mouseState.LeftButton == ButtonState.Pressed && battle.MoveMode && !releaseWait && !battle.BattleMap.GetTileAt(lastSelectedTile.X, lastSelectedTile.Y).IsRedHighlighted)
                    {
                        releaseWait = true;
                        sfx.PlayBuzzer();
                    }

                    //Confirm attack
                    if (mouseState.LeftButton == ButtonState.Pressed && battle.AttackMode && !releaseWait)
                    {
                        int X = (int)Math.Round(((double)mousePos.X - (double)offsetX - 20) / (double)55);
                        int Y = (int)Math.Round(((double)mousePos.Y - (double)offsetY - 20) / (double)55);
                        if (X >= 0 && X < battle.BattleMap.Size[0] && Y >= 0 && Y < battle.BattleMap.Size[1]
                            && battle.BattleMap.GetTileAt(X, Y).IsHighlighted && battle.BattleMap.GetTileAt(X, Y).hasUnit &&
                            (battle.BattleMap.GetTileAt(X, Y).TileUnit.isPlayerUnit != battle.ActiveUnit.isPlayerUnit))
                        {
                            battle.BattleMap.ClearRedHighlights();
                            wait = true;
                            move = movetrue;
                            attack = attacktrue;
                            defend = defendtrue;
                            item = itemtrue;
                            pass = passtrue;
                            special = specialtrue;
                            hilightenabled = true;
                            releaseWait = true;
                            unitDamage = battle.Attack(battle.BattleMap.GetTileAt(X, Y).TileUnit);
                            attackedUnitTrueX = X * 55 - 13;
                            attackedUnitTrueY = Y * 55 - 20;
                            if (battle.ActiveUnit is Artillery)
                            {
                                splashDamage = battle.GetSplashDamage(lastSelectedTile.TileUnit, unitDamage);
                            }
                            displayDamage = true;
                            timeSinceLastDamageFrame = 0;
                            frameCount = 0;

                        }
                        else if (!releaseWait)
                        {
                            releaseWait = true;
                            sfx.PlayBuzzer();
                        }

                    }

                    //select another unit to view attack range
                    if (!battle.AttackMode && !battle.MoveMode && !releaseWait && !wait && !tickWait && !moveWait && battle.SelectEnabled)
                    {

                        int X = (int)Math.Round(((double)mousePos.X - (double)offsetX - 20) / (double)55);
                        int Y = (int)Math.Round(((double)mousePos.Y - (double)offsetY - 20) / (double)55);

                        if (battle.BattleMap.GetTileAt(X, Y).hasUnit)
                        {
                            battle.ShowAttackRange(battle.BattleMap.GetTileAt(X, Y).TileUnit);
                        }
                    }

                }

                if (mouseState.LeftButton == ButtonState.Released)
                {
                    releaseWait = false;
                    if (battle.AttackRangeDisplayed)
                    {
                        battle.BattleMap.ClearBlueHighlights();
                        battle.AttackRangeDisplayed = false;
                    }
                }

                //right mouse click
                if (mouseState.RightButton == ButtonState.Pressed && mouseState.LeftButton != ButtonState.Pressed)
                {
                    //view move highlights of another unit
                    if (!battle.AttackMode && !battle.MoveMode && !rightReleaseWait && !wait && !tickWait && !moveWait && battle.SelectEnabled)
                    {
                        int X = (int)Math.Round(((double)mousePos.X - (double)offsetX - 20) / (double)55);
                        int Y = (int)Math.Round(((double)mousePos.Y - (double)offsetY - 20) / (double)55);

                        if (battle.BattleMap.GetTileAt(X, Y).hasUnit)
                        {
                            battle.ShowMoveRange(battle.BattleMap.GetTileAt(X, Y).TileUnit);
                        }
                    }
                }

                //right mouse unclick
                if (mouseState.RightButton == ButtonState.Released)
                {
                    rightReleaseWait = false;
                    if (battle.MoveRangeDisplayed)
                    {
                        battle.BattleMap.ClearRedHighlights();
                        battle.MoveRangeDisplayed = false;
                    }
                }
                //move the view with WASD
                if (Keyboard.GetState().IsKeyDown(Keys.W) && offsetY < battle.BattleMap.Size[1] + 350)
                {
                    offsetY += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && offsetY > battle.BattleMap.Size[1] * -51)
                {
                    offsetY -= 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) && offsetX < battle.BattleMap.Size[0] * 10)
                {
                    offsetX += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && offsetX > battle.BattleMap.Size[0] * -51)
                {
                    offsetX -= 10;
                }


                //update the display with the active unit
                unit = battle.ActiveUnit.GetType();
                 hp = battle.ActiveUnit.HP.ToString();
                 range = battle.ActiveUnit.Range.ToString() ;
                 defense = battle.ActiveUnit.Defense.ToString();
                 speed = battle.ActiveUnit.Speed.ToString() ;
                 mp = battle.ActiveUnit.MP.ToString();
                 attackText = battle.ActiveUnit.Attack.ToString();
                 moveText = battle.ActiveUnit.AP.ToString();
                 nextUnits = new List<string>();
                 for (int i = battle.QueuePosition + 1; i < battle.QueuePosition + 6; i++)
                 {
                     if (i < battle.BattleQueue.Count)
                     {
                         string belongsTo;
                         if (battle.BattleQueue.ElementAt(i).isPlayerUnit)
                         {
                             belongsTo = "Player 1";
                         }
                         else
                         {
                             belongsTo = "Player 2";
                         }

                         string unitText = belongsTo + ": " +  battle.BattleQueue.ElementAt(i).GetType() + " " + battle.BattleQueue.ElementAt(i).HP.ToString() + " HP";
                         nextUnits.Add(unitText);
                     }

                 }

                
                 timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;


                //stuff that happens on the animation tick, about 60fps
                 if (timeSinceLastFrame > millisecondsPerFrame)
                 {
                     timeSinceLastFrame = 0;
                 }


                 timeSinceLastDamageFrame += gameTime.ElapsedGameTime.Milliseconds;

                //stuff that happens on the damage tick, including end-of-turn checking
                //this tick is quite slow
                 if (timeSinceLastDamageFrame > millisecondsPerDamageFrame)
                 {
                     
                     timeSinceLastDamageFrame = 0;
                     // Increment to next frame
                     wait = false;
                     displayDamage = false;
                     splashDamage.Clear();
                     splashLocations.Clear();
                     if (battle.ActiveUnit.AP <= 0 && frameCount == 2)
                     {
                         battle.NextPlayer();
                         
                     }
                     if (battle.gameOver)
                     {
                         endWait = false;
                     }

                     if (AILock && !moveWait)
                     {
                         battle.AIMove();
                     }
                     frameCount++;

                 }

                //stuff that happens on the move tick, a faster interval
                 timeSinceLastMoveFrame += gameTime.ElapsedGameTime.Milliseconds;
                 if (timeSinceLastMoveFrame > millisecondsPerMoveFrame)
                 {
                     timeSinceLastMoveFrame = 0;
                     //if a move is ongoing, advance unit to the next tile
                     if (moveWait)
                     {
                         battle.ContinueMove();
                         timeSinceLastDamageFrame = 0;
                     }
                 }

                //if the turn is over, lock everything for a beat
                 if (battle.ActiveUnit.AP <= 0)
                 {
                     tickWait = true;
                 }

                 
               
            }
            timeSinceLastDamageFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastDamageFrame > millisecondsPerDamageFrame && battle != null)
            {

                timeSinceLastDamageFrame = 0;
                // Increment to next frame
                wait = false;
                displayDamage = false;
                if (battle.ActiveUnit.AP <= 0)
                {
                    battle.NextPlayer();
                }
                if (battle.gameOver)
                {
                    endWait = false;
                }

            }

            if (MediaPlayer.State == MediaState.Stopped && (p1win || p2win))
            {

                if (p1win)
                {
                    cue.Stop(AudioStopOptions.Immediate);
                    Song campaign = Content.Load<Song>(@"music\CampaignMode");
                    MediaPlayer.Play(campaign);
                }
                else if (p2win && !loseMusicStarted)
                {
                    cue.Stop(AudioStopOptions.Immediate);
                    cue = soundBank.GetCue("LoseBattle");
                    cue.Play();
                    loseMusicStarted = true;
                }
            }

            if (Keyboard.GetState().GetPressedKeys().Length > 0 && (p1win || p2win) && !endWait)
            {
                battle.gameOver = false;
                p1win = false;
                p2win = false;
                musicStarted = false;
                load.MainMenu();
                demo = false;
                mainMenu = true;
                cue.Stop(AudioStopOptions.Immediate);
                
            }

            if (mouseState.LeftButton == ButtonState.Released)
            {
                releaseWait = false;
            }
            if (Keyboard.GetState().IsKeyUp(Keys.Escape))
            {
                keyreleasewait = false;
            }


            //in game menu escape(to the new wolrd) logic
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !keyreleasewait && !ismenu&&demo)
            {
                ismenu = true;
                
                keyreleasewait = true;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape) && !keyreleasewait && ismenu&&demo)
            {
                ismenu = false;
                isOptions = false;
                keyreleasewait = true;
            }
            if (ismenu&&!keyreleasewait)
            {
                buttons.InGameButtons(mouseState);
            }
            if (isOptions && !keyreleasewait)
            {
                buttons.optionButtons(mouseState);
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            

            if (mainMenu)
            {
                draw.MainMenu();
            }
            else if (campaignmenu)
            {
                draw.CampaignMenu();
            }
            else if (optionsMenu)
            {
                draw.OptionsMenu();
            }
            else if (demo&&!ismenu&&!isOptions)
            {
                draw.Demo();
            }
            else if (ismenu)
            {
                draw.Demo();
                draw.ingamemenu();
            }
            
            else if (isOptions)
            {
                draw.Demo();
                draw.optionsIngame();
            }
            

            


            base.Draw(gameTime);

        }

        public void SetOffsetValue(int x, int y)
        {
            offsetX = x;
            offsetY = y;
        }

        public void PlayWinMusic()
        {
            
            cue.Stop(AudioStopOptions.Immediate);
            Song win = Content.Load<Song>(@"Music\WinBattle");
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.Play(win);
        }

        public void PlayLoseMusic()
        {
            cue.Stop(AudioStopOptions.Immediate);
            Song lose = Content.Load<Song>(@"Music\LoseBattle");
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Volume = 0.8f;
            MediaPlayer.Play(lose);
        }

        public void ResetButtons()
        {
            move = movetrue;
            attack = attacktrue;
            defend = defendtrue;
            item = itemtrue;
            pass = passtrue;
            special = specialtrue;
        }

        public void LockButtons()
        {
            move = move_grey;
            attack = attack_grey;
            pass = pass_grey;
            defend = defend_grey;
            item = item_grey;
            special = special_grey;
        }

    }
}
