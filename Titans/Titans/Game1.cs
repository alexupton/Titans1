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
        //I'm making a sample change. Aw yeah
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public Texture2D quick_battle;
        public Texture2D campaign;
        public Texture2D custom_battle;
        public Texture2D options;
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
        public Texture2D newGametemp;
        public Texture2D loadGame;
        public Texture2D loadGametemp;
        public Texture2D resolution;
        public Texture2D textSpeed;
        public Texture2D musicLevel;
        public Texture2D soundEffects;
        public Texture2D credits;
        public Texture2D res1;
        public Texture2D res1_unselected;
        public Texture2D res2;
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
        public Texture2D apply;
        public Texture2D apply_invert;
        public Texture2D fullScreen;
        public Texture2D fullScreenTemp;
        public Texture2D fullScreen_unselected;

        public SpriteFont text;
        public SpriteFont smallText;

        Vector2 pos1 = Vector2.Zero;
        public bool optionsMenu;
        public bool campaignmenu;
        public bool isFullScreen = true;

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
        int collisionOffset;

        // Framerate stuff
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame;
        const int defaultMillisecondsPerFrame = 100;
        int millisecondsPerDamageFrame = 800;
        int timeSinceLastDamageFrame = 0;
        int millisecondsPerMoveFrame = 50;
        int timeSinceLastMoveFrame = 0;

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
        bool releaseWait;

        //various
        public Tile lastSelectedTile;
        public int attackedUnitTrueX;
        public int attackedUnitTrueY;
        public int unitDamage;
        bool unitAttacked;
        public bool displayDamage;
        public bool musicStarted;
        Draw draw;
        ContentLoader load;


        //Global Variables
        public bool wait;
        public bool p1win;
        public bool p2win;
        public Battle battle;
        bool startTurn;
        public bool mainMenu;
        public bool demo;
        public List<Tile> MoveTiles;
        public bool moveWait;


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
            startTurn = true;
            releaseWait = false;
            lastSelectedTile = new Tile();
            unitAttacked = false;
            displayDamage = false;
            wait = false;
            p1win = false;
            p2win = false;
            musicStarted = false;
            MoveTiles = new List<Tile>();
            moveWait = false;

            timeSinceLastFrame = 0;
            millisecondsPerFrame = 100;

            draw = new Draw(this);
            load = new ContentLoader(this);



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
            
            
            if (mainMenu)
            {
                if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                    this.Exit();

                Rectangle quickClick = new Rectangle((Window.ClientBounds.Width / 2) - (quick_battle.Width / 2), (Window.ClientBounds.Height / 2) - (quick_battle.Height / 2), 141, 33);
                Rectangle campaignClick = new Rectangle((Window.ClientBounds.Width / 2) - (campaign.Width / 2), (Window.ClientBounds.Height / 2) - (quick_battle.Height - 55), 141, 33);
                Rectangle customClick = new Rectangle((Window.ClientBounds.Width / 2) - (custom_battle.Width / 2), (Window.ClientBounds.Height / 2) - (campaign.Height - 95), 175, 36);
                Rectangle optionsClick = new Rectangle((Window.ClientBounds.Width / 2) - (options.Width / 2), (Window.ClientBounds.Height / 2) - (custom_battle.Height - 140), 141, 33);
                Rectangle exitclickableArea = new Rectangle((Window.ClientBounds.Width / 2) - (exit.Width / 2), (Window.ClientBounds.Height - 70), 141, 33);
                MouseState mouseState = Mouse.GetState();

                // mousepos = new Point();
                bool mousestuffs = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(exitclickableArea);
                bool mousequick = new Rectangle(mouseState.X, mouseState.Y, 1, 1).Intersects(quickClick);

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
                    if (optionsClick.Contains(mousePos))
                    {
                        mainMenu = false;
                        optionsMenu = true;
                        LoadContent();
                    }
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
                        Map mainMap = MapLoader.LoadMap(@"Content\Demo.txt");
                        LoadContent();
                    }

                }
            }
            else if (campaignmenu)
            {
                MouseState mouseState = Mouse.GetState();
                Rectangle backClick = new Rectangle(15, 755, 141, 33);
                Rectangle newGameClick = new Rectangle(550, 300, 141, 33);
                Rectangle loadGameClick = new Rectangle(550, 343, 141, 33);
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
                MouseState mouseState = Mouse.GetState();

                Rectangle backClick = new Rectangle(15, 755, 141, 33);
                Rectangle textClick = new Rectangle(25, 433, 141, 33);
                Rectangle fullClick = new Rectangle(25, 337, 141, 33);
                int count = 2;
                if (mouseState.LeftButton == ButtonState.Pressed && !releaseWait)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (fullClick.Contains(mousePos))
                    {
                        if (isFullScreen)
                        {
                            releaseWait = true;
                            this.graphics.IsFullScreen = false;
                        }
                        else
                        {
                            releaseWait = true;
                            this.graphics.IsFullScreen = true;
                        }
                    }
                }
                if (mouseState.LeftButton == ButtonState.Pressed)
                {

                    Point mousePos = new Point(mouseState.X, mouseState.Y);
                    if (textClick.Contains(mousePos))
                    {
                        if (count == 1)
                        {
                            slowtemp = slow;
                            slow = slow_unselected;
                            regular = regulartemp;
                            regulartemp = regular;

                            count++;
                        }
                        if (count == 2)
                        {
                            regulartemp = regular;
                            regular = regular_unselected;
                            fast = fasttemp;
                            fasttemp = fast;
                            count++;
                        }
                        if (count == 3)
                        {
                            slow = slowtemp;
                            slowtemp = slow;
                            fasttemp = fast;
                            fast = fast_unselected;
                            count = 1;
                        }
                    }

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
            else if (demo && !(p1win || p2win))
            {
                engine.Update();
                sfx.Update();
                if (!battle.AttackMode)
                {
                    battle.BattleMap.ClearHighlights();
                }
                battle.BattleMap.ClearHighlights();

                //boxes for intersecion of buttons

                Rectangle moveclick = new Rectangle(680, 700, 116, 27);
                Rectangle passclick = new Rectangle(796, 700, 116, 27);
                Rectangle attackclick = new Rectangle(680, 727, 116, 27);
                Rectangle defendclick = new Rectangle(796, 727, 116, 27);
                Rectangle specialclick = new Rectangle(680, 754, 116, 27);
                Rectangle itemclick = new Rectangle(796, 754, 116, 27);
                MouseState mouseState = Mouse.GetState();
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
                    }
                }

                //Button clicks

                if (mouseState.LeftButton == ButtonState.Pressed && !releaseWait)
                {
                    
                    //select move
                    if (moveclick.Contains(mousePos) && move != move_invert)
                    {
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
                    else if (attackclick.Contains(mousePos) && attack != attack_invert)
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
                            move = movetrue;
                            attack = attacktrue;
                            defend = defendtrue;
                            item = itemtrue;
                            pass = passtrue;
                            special = specialtrue;
                            hilightenabled = true;
                            sfx.PlayBuzzer();
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
                    else if(passclick.Contains(mousePos))
                    {
                        releaseWait = true;
                        move = movetrue;
                        attack = attacktrue;
                        defend = defendtrue;
                        item = itemtrue;
                        pass = passtrue;
                        special = specialtrue;
                        hilightenabled = true;
                        battle.NextPlayer() ;
                    }

                }
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
                    
                }
                else if(mouseState.LeftButton == ButtonState.Pressed && battle.MoveMode && !releaseWait && !battle.BattleMap.GetTileAt(lastSelectedTile.X, lastSelectedTile.Y).IsRedHighlighted)
                {
                    releaseWait = true;
                    sfx.PlayBuzzer();
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
                        unitAttacked = true;
                        displayDamage = true;
                        timeSinceLastDamageFrame = 0;

                    }
                    else if(!releaseWait)
                    {
                        releaseWait = true;
                        sfx.PlayBuzzer();
                        sfx.PlayBuzzer();
                    }
                    
                }

                if (mouseState.LeftButton == ButtonState.Released)
                    releaseWait = false;
                //move the view with WASD
                if (Keyboard.GetState().IsKeyDown(Keys.W) && offsetY < 500)
                {
                    offsetY += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.S) && offsetY > -1000)
                {
                    offsetY -= 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.A) && offsetX < 750)
                {
                    offsetX += 10;
                }
                if (Keyboard.GetState().IsKeyDown(Keys.D) && offsetX > -750)
                {
                    offsetX -= 10;
                }

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

                // Update frame if time to do so based on framerate
                 timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
                 timeSinceLastDamageFrame += gameTime.ElapsedGameTime.Milliseconds;
                 if (timeSinceLastDamageFrame > millisecondsPerDamageFrame)
                 {
                     
                     timeSinceLastDamageFrame = 0;
                     // Increment to next frame
                     displayDamage = false;
                     wait = false;
                     if (battle.ActiveUnit.AP <= 0)
                     {
                         battle.NextPlayer();
                     }
                 }

                 timeSinceLastMoveFrame += gameTime.ElapsedGameTime.Milliseconds;
                 if (timeSinceLastMoveFrame > millisecondsPerMoveFrame)
                 {
                     timeSinceLastMoveFrame = 0;
                     if (moveWait)
                     {
                         battle.ContinueMove();
                     }
                 }

                 
                 
                
                    //++currentFrame.X;
                    //if (currentFrame.X >= sheetSize.X)
                    //{
                    //    currentFrame.X = 0;
                    //    ++currentFrame.Y;
                    //    if (currentFrame.Y >= sheetSize.Y)
                    //        currentFrame.Y = 0;
                    //}
                

                //Debug method for move testing
            }

            if (MediaPlayer.State == MediaState.Stopped && (p1win || p2win))
            {
                Song campaign = Content.Load<Song>(@"music\CampaignMode");
                MediaPlayer.Play(campaign);
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
            else if (demo)
            {
                draw.Demo();
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

    }
}
