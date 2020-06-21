using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading;

namespace Test
{
    class Game
    {
        public static bool IsRunning = true;
        public static bool InGame = false;
        public static bool DoResetGame = false;
        public static bool InScene = false;
        public static bool Ending = false;
        public static int Tomming = 0;

        public static Form GameWindow;
        public static Stopwatch GameTime = new Stopwatch();
        public static int ElapsedGameTime = 0; //sau khi chạy từ vòng update này đến update tiếp theo mất bao nhiêu thời gian
        public static int FrameInterval;

        public static Graphics g;
        public static Graphics bbg;
        public static Bitmap bb;

        public static Map World;
        public static WorldScreen worldscreen;
        public static Character Player;
        public static Point PlayerInScreen = new Point(15, 10);
        public static Dictionary<Key, BaseSkill> SkillButtons = new Dictionary<Key, BaseSkill>();
        public static Dictionary<Key, Item> ItemButtons = new Dictionary<Key, Item>();

        public static int ScreenX = 13;
        public static int ScreenY = 7;
        public static int ScreenOffsetX;
        public static int ScreenOffsetY;
        public static int Speed = 32;   //Speed của di chuyển screen trong Map Editor

        public static Dir MoveDir = Dir.Stand;

        public static bool EditorMode = false;

        public static PictureBox Frame = new PictureBox();

        private static Label fps = new Label();

        public static void Initialize(Form Form)
        {
            Textures.Initialize();
            GameWindow = Form;
            FrameInterval = (int)Math.Ceiling(1000.0 / Globals.MAX_FRAME_PER_SECOND);

            bb = new Bitmap(Globals.resWidth, Globals.resHeight);
            g = Graphics.FromImage(bb);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;

            Frame.Parent = GameWindow;
            Frame.Dock = DockStyle.Fill;
            Frame.SendToBack();
            Frame.SizeMode = PictureBoxSizeMode.StretchImage;
            Frame.MouseClick += Frame_MouseClick;

            BGM.BGMPlayer.settings.volume = Settings.MusicVolume;

            DataHandler.LoadCharType();

            ResetGame();

            ScreenManager.AddScreen(new TitleScreen());

            GameTime.Start();
            GameLoop();
        }

        public static void StartGame(bool StartAsEditor = false)
        {
            if (StartAsEditor)
                Player = new Character();

            worldscreen = new WorldScreen();
            ScreenManager.AddScreen(worldscreen);
            if (!StartAsEditor) ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScreneByName("StartScene")));
            MapHandler.LoadMap("StartMap");
            Player.CurPos.X = 14;
            Player.CurPos.Y = 35;
            Player.OffSet = new Point(0, 0);
            Game.SetScreenByPlayerPos();

            CharType.SetSkill(Player);

            ItemButtons.Add(Key.Z, new iRestoreHP(Player));
            ItemButtons.Add(Key.X, new iRestoreMP(Player));
            ItemButtons.First().Value.Count = 10;
            ItemButtons.Last().Value.Count = 5;

            Globals.InitializeFlags();
            Globals.Money = 50000;

            //BGM.PlayBGM(Globals.GameDir + "\\Content\\BGM\\Departure.mp3");
            InGame = true;
        }

        public static void ResetGame()
        {
            g.Clear(Globals.Main.BackColor);
            SkillButtons.Clear();
            ItemButtons.Clear();
            Globals.Flags.Clear();
            Globals.Money = 0;
            Item.EffectingItems.Clear();
            ScreenManager.Unload("World Screen");
            Player = null;
            World = null;
        }


        public static void GameLoop()
        {
            while (IsRunning == true)
            {
                //Chạy các event tác động lên cái form
                //Không có dòng này thì các thứ sẽ chạy trong vòng while này mãi, không xử lý được các event tác động lên cái form
                Application.DoEvents();

                HandleInputs();
                Update();
                Draw();

                if (DoResetGame)
                {
                    ResetGame();
                    DoResetGame = false;
                }
            }
        }

        private static void HandleInputs()
        {
            ScreenManager.HandleInputs();
        }

        private static void Update()
        {
            //Gán thời gian stopwatch chạy được từ lần chạy Update trước đến hiện tại vào biến ElapsedGameTime
            //Reset stopwatch về 0 rồi cho đếm lại
            //Biến ElapsedGameTime sẽ được sử dụng trong hàm Update() của các thứ khác, để xác định xem khi nó được chạy từ lần này đến lần tiếp theo đã bao nhiêu thời gian trôi qua rồi
            ElapsedGameTime = (int)GameTime.ElapsedMilliseconds;
            if(ElapsedGameTime < FrameInterval)
            {
                //Chế ngự ko cho chạy nhanh quá
                //Set 1 giây chạy tối đa bao nhiêu frame (bao nhiêu vòng lặp) ở Globals.MAX_FRAME_PER_SECOND
                Thread.Sleep(FrameInterval - ElapsedGameTime);
                ElapsedGameTime = FrameInterval;
            }
            fps.Text = ElapsedGameTime.ToString();
            GameTime.Restart();

            //Chạy Update của ScreenManager
            //ScreenManager sẽ lặp tất cả các Screen đang có và chạy hàm Update() của từng Screen
            ScreenManager.Update();

            //Chạy Update của SoundManager
            SoundManager.Update();
        }

        private static void Draw()
        {
            //Chạy Draw của ScreenManager
            //ScreenManager sẽ lặp tất cả các Screen đang có và chạy hàm Draw() của từng Screen
            ScreenManager.Draw(g);
            if(EditorMode) bbg.DrawImage(bb, 0, 0);
            Frame.Image = bb;
        }

        public static void Move()
        {
            if(MoveDir == Dir.Up)
            {
                ScreenOffsetY -= Speed;
                if ( ScreenOffsetY <= -Globals.TileSize)
                {
                    ScreenY -= 1;
                    ScreenOffsetY = 0;
                }
            }else if(MoveDir == Dir.Down)
            {
                ScreenOffsetY += Speed;
                if (ScreenOffsetY >= Globals.TileSize)
                {
                    ScreenY += 1;
                    ScreenOffsetY = 0;
                }
            }
            else if(MoveDir == Dir.Left)
            {
                ScreenOffsetX -= Speed;
                if (ScreenOffsetX <= -Globals.TileSize)
                {
                    ScreenX -= 1;
                    ScreenOffsetX = 0;
                }
            }else if(MoveDir == Dir.Right)
            {
                ScreenOffsetX += Speed;
                if (ScreenOffsetX >= Globals.TileSize)
                {
                    ScreenX += 1;
                    ScreenOffsetX = 0;
                }
            }
        }

        public static Rectangle GetSprite(Character character)
        {
            Rectangle sprite;

            if (Game.worldscreen.Active)
            {
                if (character.MoveDir != Dir.Stand)
                {
                    if (character.IsCasting == false)
                    {
                        character.AniTime += ElapsedGameTime;
                        if (character.AniTime >= Globals.AniInterval)
                        {
                            character.AniTime = 0;
                            character.AniFrame += 1;
                        }
                    }
                }
                else
                {
                    if (character.IsCasting == false)
                    {
                        character.AniTime = 0;
                        character.AniFrame = 0;
                    }
                }
            }
            
            if (character.LastDir == Dir.Up)
            {
                sprite = new Rectangle( (character.charType.Source.srcPos.X + character.AniFrame) * character.charType.Size.X, (character.charType.Source.srcPos.Y + 3) * character.charType.Size.Y, character.charType.Size.X, character.charType.Size.Y);
            }else if(character.LastDir == Dir.Down || character.LastDir == Dir.Stand)
            {
                sprite = new Rectangle( (character.charType.Source.srcPos.X + character.AniFrame) * character.charType.Size.X, (character.charType.Source.srcPos.Y + 0) * character.charType.Size.Y, character.charType.Size.X, character.charType.Size.Y);
            }else if(character.LastDir == Dir.Left)
            {
                sprite = new Rectangle( (character.charType.Source.srcPos.X + character.AniFrame) * character.charType.Size.X, (character.charType.Source.srcPos.Y + 1) * character.charType.Size.Y, character.charType.Size.X, character.charType.Size.Y);
            }
            else
            {
                sprite = new Rectangle( (character.charType.Source.srcPos.X + character.AniFrame) * character.charType.Size.X, (character.charType.Source.srcPos.Y + 2) * character.charType.Size.Y, character.charType.Size.X, character.charType.Size.Y);
            }
            

            return sprite;
        }

        public static void SetScreenByPlayerPos()
        {
            ScreenX = Player.CurPos.X - PlayerInScreen.X;
            ScreenY = Player.CurPos.Y - PlayerInScreen.Y;
            ScreenOffsetX = Player.OffSet.X;
            ScreenOffsetY = Player.OffSet.Y;
        }

        private static void Frame_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ScreenManager.MouseClick(e.Button);
        }
    }
}
