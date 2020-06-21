using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace Test
{
    class WorldScreen : BaseScreen
    {

        public WorldScreen()
        {
            Name = "World Screen";
        }

        public override void KeyUp(Keys key)
        {
            if (Game.SkillButtons.ContainsKey(key))
            {
                Game.SkillButtons[key].Cast();
            }
        }

        public override void HandleInput(Keys key)
        {
            if(Game.Player.IsCasting == false)
            {
                if (key == Keys.Up)
                {
                    Game.MoveDir = Dir.Up;
                    Game.Player.MoveDir = Dir.Up;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                else if (key == Keys.Down)
                {
                    Game.MoveDir = Dir.Down;
                    Game.Player.MoveDir = Dir.Down;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                else if (key == Keys.Left)
                {
                    Game.MoveDir = Dir.Left;
                    Game.Player.MoveDir = Dir.Left;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                else if (key == Keys.Right)
                {
                    Game.MoveDir = Dir.Right;
                    Game.Player.MoveDir = Dir.Right;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
            }
        }

        public override void Update()
        {
            //Update tọa độ của Screen theo tọa độ của Player
            if (Game.EditorMode == false)
            {
                if (Active) Game.Player.Move();
                Game.ScreenX = Game.Player.CurPos.X - Game.PlayerInScreen.X;
                Game.ScreenY = Game.Player.CurPos.Y - Game.PlayerInScreen.Y;
                Game.ScreenOffsetX = Game.Player.OffSet.X;
                Game.ScreenOffsetY = Game.Player.OffSet.Y;
            }

            //Update Skills
            SkillManager.Update();

            if (Game.World != null)
            {
                //Update arts của skills
                for (int i = 0; i < Game.World.BeforeCharArts.Count; i += 1)
                {
                    if (Game.World.BeforeCharArts[i].remove)
                    {
                        Game.World.BeforeCharArts.RemoveAt(i);
                        i -= 1;
                    }
                    else Game.World.BeforeCharArts[i].Update();
                }
                for (int i = 0; i < Game.World.AfterCharArts.Count; i += 1)
                {
                    if (Game.World.AfterCharArts[i].remove)
                    {
                        Game.World.AfterCharArts.RemoveAt(i);
                        i -= 1;
                    }
                    else Game.World.BeforeCharArts[i].Update();
                }

                //Chạy hàm Act của quái
                foreach (Character c in Game.World.MapChar)
                {
                    c.Act();
                }
            }
        }

        public override void Draw(Graphics g)
        {
            if (Game.World is null) return;

            for (int X = -1; X <= Game.GameWindow.Size.Width / Globals.TileSize + 1; X += 1)
            {
                for (int Y = -1; Y <= Game.GameWindow.Size.Height / Globals.TileSize + 1; Y += 1)
                {
                    //Draw layer0 và layer1
                    if (Game.ScreenX + X >= 0 && Game.ScreenX + X <= Game.World.Size.X && Game.ScreenY + Y >= 0 && Game.ScreenY + Y <= Game.World.Size.Y)
                    {
                        Rectangle srcRect = new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize);
                        Rectangle desRect = new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize);
                        g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcImg], desRect, srcRect, GraphicsUnit.Pixel);
                        if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcImg != "Tiny" || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.X != 0 || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.Y != 0)
                        {
                            g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcImg], desRect, new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                        }

                        if (Game.EditorMode)
                        {
                            if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].IsBlocked == true) g.FillRectangle(Brushes.Red, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                            if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].TouchTrigger == true || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].StepTrigger == true) g.DrawImage(Properties.Resources.Trigger, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                        }
                    }
                    else
                    {
                        g.FillRectangle(Brushes.Black, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                    }

                    if (Game.EditorMode)
                    {
                        g.DrawRectangle(Pens.Black, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                    }
                }
            }

            //Vẽ art trước nhân vật
            g.DrawString(Game.Player.CurRawPos.ToString(), Globals.Font, Brushes.Red, new Point(10, 20));
            foreach (Art a in Game.World.BeforeCharArts)
            {
                g.DrawString(a.Position.ToString(), Globals.Font, Brushes.Red, new Point(10, 30));
                if (a.Angle != RotateFlipType.RotateNoneFlipNone)
                {
                    using(Bitmap tmpImage = new Bitmap(a.SrcRect.Width, a.SrcRect.Height))
                    {
                        using(Graphics G = Graphics.FromImage(tmpImage))
                        {
                            G.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(0,0,tmpImage.Width, tmpImage.Height), a.SrcRect, GraphicsUnit.Pixel);
                        }
                        tmpImage.RotateFlip(a.Angle);
                        g.DrawImage(tmpImage, new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height));
                    }
                }
                else
                {
                    g.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X , a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height), a.SrcRect, GraphicsUnit.Pixel);
                }
            }

            //Vẽ Characters
            g.FillRectangle(Brushes.Black, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 20, Game.Player.charType.Size.X, 8));
            g.FillRectangle(Brushes.Lime, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize + 1, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 19, (int)(Game.Player.HP * 1.0 / Game.Player.HPMax * (Game.Player.charType.Size.X -2)), 6));
            g.FillRectangle(Brushes.Black, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 10, Game.Player.charType.Size.X, 8));
            g.FillRectangle(Brushes.Blue, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize + 1, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 9, (int)(Game.Player.MP * 1.0 / Game.Player.MPMax * (Game.Player.charType.Size.X - 2)), 6));
            g.DrawImage(Textures.CharImg[Game.Player.charType.Source.srcImg], new Rectangle(Game.PlayerInScreen.X * Globals.TileSize, Game.PlayerInScreen.Y * Globals.TileSize, Game.Player.charType.Size.X, Game.Player.charType.Size.Y), Game.GetSprite(Game.Player), GraphicsUnit.Pixel);
            foreach (Character c in Game.World.MapChar)
            {
                g.FillRectangle(Brushes.Black, new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 10, c.charType.Size.X, 8));
                int percent = c.HP * 100 / c.HPMax;
                int w = (int)((c.charType.Size.X - 2) / 100.0 * percent);
                if (percent > 60)
                {
                    g.FillRectangle(Brushes.Lime, new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X + 1, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 9, w, 6));
                }
                else if(percent > 30)
                {
                    g.FillRectangle(Brushes.Yellow, new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X + 1, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 9, w, 6));
                }
                else
                {
                    g.FillRectangle(Brushes.Red, new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X + 1, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 9, w, 6));
                }
                //if(c == Game.World.MapChar.First())
                //{
                //    if(c.AIPath != null)
                //    {
                //        foreach(Point p in c.AIPath)
                //        {
                //            g.FillRectangle(Brushes.Brown, new Rectangle((p.X - Game.ScreenX)* Globals.TileSize,( p.Y-Game.ScreenY) * Globals.TileSize, Globals.TileSize, Globals.TileSize));
                //        }
                //    }
                //}
                g.DrawImage(Textures.CharImg[c.charType.Source.srcImg], new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y +c.OffSet.Y, c.charType.Size.X, c.charType.Size.Y), Game.GetSprite(c), GraphicsUnit.Pixel);
            }

            //Vẽ Missile

            //Vẽ art sau nhân vật
            foreach (Art a in Game.World.AfterCharArts)
            {
                if (a.Angle != RotateFlipType.RotateNoneFlipNone)
                {
                    using (Bitmap tmpImage = new Bitmap(a.Position.Width, a.Position.Height))
                    {
                        using (Graphics G = Graphics.FromImage(tmpImage))
                        {
                            G.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(0, 0, tmpImage.Width, tmpImage.Height), a.SrcRect, GraphicsUnit.Pixel);
                            tmpImage.RotateFlip(a.Angle);
                            g.DrawImage(tmpImage, new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height));
                        }
                    }
                }
                else
                {
                    g.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height), a.SrcRect, GraphicsUnit.Pixel);
                }
            }

            //Vẽ Layer2 và Layer3


            //Vẽ các cây máu trên đầu nhân vật
            //Tạo biến boolean bên setting người chơi có thể bật tắt các cây máu này

            //Vẽ các biểu cảm khi bị attack/bị status ailments


            //Vẽ các box infos


            //Dành cho Map Editor

            //Draw Debug
            //g.DrawString((Game.Player.CurPos.X + ":" + Game.Player.CurPos.Y).ToString(), Globals.Font, Brushes.Red, new Point(2, 2));
            g.DrawString(Game.World.MapChar.Count.ToString(), Globals.Font, Brushes.Red, new Point(2, 2));

            //bbg = GameWindow.CreateGraphics();

        }

    }
}
