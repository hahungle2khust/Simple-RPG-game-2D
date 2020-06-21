using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace Test
{
    class Game
    {
        public static bool IsRunning = true;

        public static Form GameWindow;
        public static Stopwatch GameTime = new Stopwatch();
        public static int ElapsedGameTime = 0; //sau khi chạy từ vòng update này đến update tiếp theo mất bao nhiêu thời gian

        public static Graphics g;
        public static Graphics bbg;
        public static Bitmap bb;

        public static Map World;
        public static WorldScreen worldscreen;
        public static Character Player;
        public static Point PlayerInScreen = new Point(13, 7);
        public static Dictionary<Keys, BaseSkill> SkillButtons = new Dictionary<Keys, BaseSkill>();

        public static int ScreenX = 13;
        public static int ScreenY = 7;
        public static int ScreenOffsetX;
        public static int ScreenOffsetY;
        public static int Speed = 32;   //Speed của di chuyển screen trong Map Editor

        public static Dir MoveDir = Dir.Stand;

        public static bool EditorMode = false;

        public static void Initialize(Form Form)
        {
            Textures.Initialize();
            GameWindow = Form;

            //g = Graphics.FromImage(bb);
            //bbg = GameWindow.CreateGraphics();

            Scale(GameWindow);

            DataHandler.LoadCharType();
            
            //World = new Map("World", 20, 20);
            worldscreen = new WorldScreen();
            ScreenManager.AddScreen(worldscreen);
            MapHandler.LoadMap("1");

            SkillButtons.Add(Keys.Space, null);
            SkillButtons.Add(Keys.S, null);
            SkillButtons.Add(Keys.D, null);

            Player = new Character();

            //Test
            SkillButtons[Keys.Space] = new sAttack();
            SkillButtons[Keys.Space].Owner = Player;

            //BGM.PlayBGM(Globals.GameDir + "\\Content\\BGM\\Departure.mp3");

            GameTime.Start();
            GameLoop();
        }

        public static void GameLoop()
        {
            while (IsRunning == true)
            {
                Application.DoEvents();

                if (EditorMode)
                {
                    Globals.Editor.DoMouseIsDown();
                    Move();
                    Player.CurPos.X = ScreenX + PlayerInScreen.X;
                    Player.CurPos.Y = ScreenY + PlayerInScreen.Y;
                    Player.OffSet.X = ScreenOffsetX;
                    Player.OffSet.Y = ScreenOffsetY;
                }

                Update();

                ScreenManager.Draw(g);
                
                bbg.DrawImage(bb, new Rectangle(0, 0, GameWindow.Size.Width, GameWindow.Size.Height), new Rectangle(0, 0, GameWindow.Size.Width, GameWindow.Size.Height), GraphicsUnit.Pixel);
            }
        }

        public static void Update()
        {
            ElapsedGameTime = (int)GameTime.ElapsedMilliseconds;
            GameTime.Restart();

            if(EditorMode == false) ScreenManager.Update();
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

        public static void Draw()
        {
            if (World is null) return;

            //try
            //{
            //    bb.Dispose();
            //}
            //catch{ }

            //bb = new Bitmap(GameWindow.Size.Width, GameWindow.Size.Height);
            //g = Graphics.FromImage(bb);

            for (int X = -1; X <= GameWindow.Size.Width/Globals.TileSize+1; X += 1)
            {
                for (int Y = -1; Y<= GameWindow.Size.Height/Globals.TileSize+1; Y += 1)
                {
                    //Draw layer0 và layer1
                    if (ScreenX+X>=0 && ScreenX + X <=World.Size.X && ScreenY+Y>=0 && ScreenY+Y<=World.Size.Y)
                    {
                        Rectangle srcRect = new Rectangle(World.TileList[ScreenX + X, ScreenY + Y].Layers[0].srcPos.X * Globals.TileSize, World.TileList[ScreenX + X, ScreenY + Y].Layers[0].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize);
                        Rectangle desRect = new Rectangle(X * Globals.TileSize - ScreenOffsetX, Y * Globals.TileSize - ScreenOffsetY, Globals.TileSize, Globals.TileSize);
                        g.DrawImage(Textures.TileImg[World.TileList[ScreenX + X, ScreenY + Y].Layers[0].srcImg], desRect, srcRect , GraphicsUnit.Pixel);
                        if (World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcImg != "Tiny" || World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcPos.X != 0 || World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcPos.Y != 0)
                        {
                            g.DrawImage(Textures.TileImg[World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcImg], desRect, new Rectangle(World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcPos.X * Globals.TileSize, World.TileList[ScreenX + X, ScreenY + Y].Layers[1].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                        }

                        if (EditorMode)
                        {
                            if (World.TileList[ScreenX + X, ScreenY + Y].IsBlocked == true) g.FillRectangle(Brushes.Red, new Rectangle(X * Globals.TileSize - ScreenOffsetX, Y * Globals.TileSize - ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                            if (World.TileList[ScreenX + X, ScreenY + Y].TouchTrigger == true || World.TileList[ScreenX + X, ScreenY + Y].StepTrigger == true) g.DrawImage(Properties.Resources.Trigger, new Rectangle(X * Globals.TileSize - ScreenOffsetX, Y * Globals.TileSize - ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                        }
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Black, new Rectangle(X * Globals.TileSize - ScreenOffsetX, Y * Globals.TileSize - ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                    }

                    if (EditorMode)
                    {
                        g.DrawRectangle(Pens.Black, new Rectangle(X * Globals.TileSize - ScreenOffsetX, Y * Globals.TileSize - ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                    }
                }
            }


            //Vẽ Art của skill nằm trước nhân vật

            //Vẽ Characters
            g.DrawImage(Textures.CharImg[Player.charType.Source.srcImg], new Rectangle(PlayerInScreen.X * Globals.TileSize, PlayerInScreen.Y * Globals.TileSize, Player.charType.Size.X, Player.charType.Size.Y), GetSprite(Player), GraphicsUnit.Pixel);

            //Vẽ Missile


            //Vẽ Art của skill nằm sau nhân vật


            //Vẽ Layer2 và Layer3


            //Dành cho Map Editor

            //Draw Debug
            g.DrawString((Player.CurPos.X + ":" + Player.CurPos.Y).ToString(), Globals.Font, Brushes.Red, new Point(2, 2));


            //bbg = GameWindow.CreateGraphics();
            
        }

        public static void Scale(Form form)
        {
            try
            {
                bb.Dispose();
            }
            catch { }
            bb = new Bitmap(GameWindow.Size.Width, GameWindow.Size.Height);
            g = Graphics.FromImage(bb);
            try
            {
                bbg.Dispose();
            }
            catch { }
            bbg = form.CreateGraphics();
        }

        public static Rectangle GetSprite(Character character)
        {
            Rectangle sprite;

            if (character.MoveDir != Dir.Stand)
            {
                character.AniTime += ElapsedGameTime;
                if (character.AniTime >= Globals.AniInterval)
                {
                    character.AniTime = 0;
                    character.AniFrame += 1;
                    if (character.AniFrame > 2) character.AniFrame = 0;
                }
            }
            else
            {
                character.AniTime = 0;
                character.AniFrame = 0;
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

        public static RotateFlipType GetAngleByCharacterFacing(Character c)
        {
            if (c.LastDir == Dir.Up) return RotateFlipType.Rotate270FlipNone;
            else if (c.LastDir == Dir.Down) return RotateFlipType.Rotate90FlipNone;
            else if (c.LastDir == Dir.Left) return RotateFlipType.Rotate180FlipNone;
            else if (c.LastDir == Dir.Right) return RotateFlipType.RotateNoneFlipNone;

            return RotateFlipType.Rotate90FlipNone;
        }
    }
}