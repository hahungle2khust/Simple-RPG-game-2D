using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using Test.Forms;
using System.Windows.Input;

namespace Test
{
    class WorldScreen : BaseScreen
    {
        public int MapNameMoveCounter = 0;
        private int MapNameMoveInterval = 50;
        private int ShowMapNameCounter = 0;
        private int ShowMapNameTime;
        private int UPorDOWN = 1;
        public Label MapName = new Label();

        public WorldScreen()
        {
            Name = "World Screen";
            Game.worldscreen = this;

            MapName.Parent = Game.Frame;
            MapName.AutoSize = true;
            MapName.BackColor = Color.FromArgb(180, 0, 0, 0);
            MapName.ForeColor = Color.DarkTurquoise;
            MapName.Font = Globals.InfoFont;
            //Globals.Main.label1.Visible = true;
        }

        public void ShowMapName(int Time)
        {
            if (Game.EditorMode) return;
            ShowMapNameCounter = 0;
            ShowMapNameTime = Time;
            MapName.Text = Game.World.Name;
            MapName.Location = new Point((Game.Frame.Width-MapName.Width)/2, -MapName.Height);
        }

        public override void HandleInputs()
        {
            if (Game.Player != null && Game.Player.IsCasting == false)
            {
                if (Keyboard.IsKeyDown(Key.Up))
                {
                    Game.MoveDir = Dir.Up;
                    Game.Player.MoveDir = Dir.Up;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                if (Keyboard.IsKeyDown(Key.Down))
                {
                    Game.MoveDir = Dir.Down;
                    Game.Player.MoveDir = Dir.Down;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                if (Keyboard.IsKeyDown(Key.Left))
                {
                    Game.MoveDir = Dir.Left;
                    Game.Player.MoveDir = Dir.Left;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
                if (Keyboard.IsKeyDown(Key.Right))
                {
                    Game.MoveDir = Dir.Right;
                    Game.Player.MoveDir = Dir.Right;
                    Game.Player.LastDir = Game.Player.MoveDir;
                }
            }
            foreach(Key k in Game.SkillButtons.Keys)
            {
                if(Keyboard.IsKeyDown(k))
                    Game.SkillButtons[k].Cast();
            }
            if (Keyboard.IsKeyDown(Key.Space))
            {
                for(int i=0; i<Game.World.ItemList.Count; i += 1)
                {
                    if (Game.World.ItemList[i].LocInMap.IntersectsWith(Game.Player.HitBox))
                    {
                        bool use = true;
                        foreach(var ib in Game.ItemButtons.Values)
                        {
                            if (ib.GetType().Equals(Game.World.ItemList[i].GetType()))
                            {
                                ib.Count += 1;
                                use = false;
                            }
                        }
                        if (use) Game.World.ItemList[i].Use();
                        Game.World.ItemList.RemoveAt(i);
                        i -= 1;
                    }
                }
            }
        }

        public override void KeyUp(Keys key)
        {
            //if (Game.SkillButtons.ContainsKey(key))
            //    Game.SkillButtons[key].Cast();
            if (Game.Player != null && (key == Keys.Down || key == Keys.Up || key == Keys.Left || key == Keys.Right))
            {
                Game.Player.MoveDir = Dir.Stand;
                Game.Player.AniFrame = 0;
            }
            if(key == Keys.Escape)
            {
                Unload();
                ScreenManager.AddScreen(new TitleScreen());
            }
            if (key == Keys.Tab)
                Settings.ShowInfo = !Settings.ShowInfo;
            if (key == Keys.Oem3)
                Settings.ShowHPBars = !Settings.ShowHPBars;
            foreach (Key k in Game.ItemButtons.Keys)
            {
                if (key.ToString() == k.ToString())
                    Game.ItemButtons[k].Use();
            }
            if(key == Keys.F4 || key == Keys.ShiftKey)
            {
                ScreenManager.AddScreen(new SaveLoadScreen("save"));
            }

            //Test
            if(Globals.DEBUGMODE && key == Keys.J)
            {
                //ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScreneByName("TestScene")));
                //ScreenManager.AddScreen(new TrollScreen());
            }

            if (Globals.DEBUGMODE && key == Keys.P)
            {
                for (int i = 0; i < Game.World.MapChar.Count; i += 1)
                {

                    if (i == Game.World.MapChar.Count - 1)
                    {
                        Game.World.MapChar[i].CurPos = new Point(Game.Player.CurPos.X, Game.Player.CurPos.Y - 2);
                    }
                    else Game.World.DiedChar.Add(Game.World.MapChar[i]);
                }
            }
        }

        public override void Update()
        {
            if (!Game.InGame) return;

            //Update label Map's Name
            if (!MapName.Visible) MapName.Visible = true;
            if (ShowMapNameCounter < ShowMapNameTime)
            {
                ShowMapNameCounter += Game.ElapsedGameTime;
                if(ShowMapNameCounter >= ShowMapNameTime)
                {
                    ShowMapNameCounter = 0;
                    ShowMapNameTime = 0;
                    UPorDOWN = 1;
                    MapName.Location = new Point(MapName.Location.X, -MapName.Height - 2);
                }
                MapNameMoveCounter += Game.ElapsedGameTime;
                if(MapNameMoveCounter >= MapNameMoveInterval)
                {
                    MapNameMoveCounter = 0;
                    MapName.Location = new Point(MapName.Location.X, MapName.Location.Y + UPorDOWN * (MapName.Height + 20) * MapNameMoveInterval / 1000);
                    if (MapName.Location.Y >= 20)
                    {
                        if (ShowMapNameCounter >= ShowMapNameTime - 2000) UPorDOWN = -1;
                        else UPorDOWN = 0;
                    }
                }
            }

            //Update tọa độ của Screen theo tọa độ của Player
            if (Game.EditorMode)
            {
                Globals.Editor.DoMouseIsDown();
                Game.Move();
                Game.Player.CurPos.X = Game.ScreenX + Game.PlayerInScreen.X;
                Game.Player.CurPos.Y = Game.ScreenY + Game.PlayerInScreen.Y;
                Game.Player.OffSet.X = Game.ScreenOffsetX;
                Game.Player.OffSet.Y = Game.ScreenOffsetY;
            }
            else
            {
                if (Active) Game.Player.Move();
                Game.SetScreenByPlayerPos();
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
                    else Game.World.AfterCharArts[i].Update();
                }

                //Update Missile trong map
                for(int i=0; i<=Game.World.MissileList.Count-1; i += 1)
                {
                    if (Game.World.MissileList[i].remove)
                    {
                        Game.World.MissileList.RemoveAt(i);
                        i -= 1;
                    }
                    else
                        Game.World.MissileList[i].Update();
                }

                //Update Item
                Item.UpdateList();

                //Chạy hàm Act của quái, trừ khi là đang trong Map Editor
                if (Game.EditorMode == false)
                {
                    foreach (Character c in Game.World.MapChar)
                    {
                        c.Act();
                    }
                }

                //Remove những char trong diedChar ra khỏi MapChar
                foreach(Character c in Game.World.DiedChar)
                {
                    Game.World.MapChar.Remove(c);
                }
                Game.World.DiedChar.Clear();

                //Add char mới vào MapChar
                foreach(Character c in Game.World.NewChar)
                {
                    Game.World.MapChar.Add(c);
                }
                Game.World.NewChar.Clear();
            }
        }

        public override void Draw(Graphics g)
        {
            if (Game.World is null) return;

            //Draw layer0 và layer1
            for (int X = -1; X <= Game.GameWindow.Size.Width / Globals.TileSize + 1; X += 1)
            {
                for (int Y = -1; Y <= Game.GameWindow.Size.Height / Globals.TileSize + 1; Y += 1)
                {
                    if (Game.ScreenX + X >= 0 && Game.ScreenX + X <= Game.World.Size.X && Game.ScreenY + Y >= 0 && Game.ScreenY + Y <= Game.World.Size.Y)
                    {
                        Rectangle srcRect = new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize);
                        Rectangle desRect = new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize);
                        g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[0].srcImg], desRect, srcRect, GraphicsUnit.Pixel);
                        if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcImg != "Tiny" || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.X != 0 || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.Y != 0)
                        {
                            g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcImg], desRect, new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[1].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                        }
                    }
                }
            }

            //Vẽ art trước nhân vật
            foreach (Art a in Game.World.BeforeCharArts)
            {
                if (a.remove || OutOfSceen(a.Position)) continue;
                g.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height), a.getSprite(), GraphicsUnit.Pixel);
            }

            //Vẽ items
            foreach(Item item in Game.World.ItemList){
                if(!OutOfSceen(new Rectangle(item.LocInMap.X, item.LocInMap.Y, Globals.TileSize, Globals.TileSize)))
                    g.DrawImage(Textures.ItemImg[item.Source.srcImg], new Rectangle(item.LocInMap.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, item.LocInMap.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, Globals.TileSize, Globals.TileSize), new Rectangle(item.Source.srcPos.X * Globals.TileSize, item.Source.srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
            }

            //Vẽ Player
            //if (!Game.EditorMode && Settings.ShowHPBars)
            //{
            //    g.FillRectangle(Brushes.Black, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 20, Game.Player.charType.Size.X, 8));
            //    g.FillRectangle(Brushes.Lime, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize + 1, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 19, (int)(Game.Player.HP * 1.0 / Game.Player.HPMax * (Game.Player.charType.Size.X - 2)), 6));
            //    g.FillRectangle(Brushes.Black, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 10, Game.Player.charType.Size.X, 8));
            //    g.FillRectangle(Brushes.Blue, new Rectangle((Game.Player.CurPos.X - Game.ScreenX) * Globals.TileSize + 1, (Game.Player.CurPos.Y - Game.ScreenY) * Globals.TileSize - 9, (int)(Game.Player.MP * 1.0 / Game.Player.MPMax * (Game.Player.charType.Size.X - 2)), 6));
            //}
            g.DrawImage(Textures.CharImg[Game.Player.charType.Source.srcImg], new Rectangle(Game.PlayerInScreen.X * Globals.TileSize, Game.PlayerInScreen.Y * Globals.TileSize, Game.Player.charType.Size.X, Game.Player.charType.Size.Y), Game.GetSprite(Game.Player), GraphicsUnit.Pixel);

            //Vẽ các character khác trong list MapChar của Map
            foreach (Character c in Game.World.MapChar)
            {
                if (OutOfSceen(c.HitBox)) continue;

                if (!Game.EditorMode && Settings.ShowHPBars)
                {
                    int w = (int)((c.charType.Size.X - 2) / 100.0 * c.HP * 100 / c.HPMax);
                    g.FillRectangle(Brushes.Black, new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 10, c.charType.Size.X, 8));
                    g.FillRectangle(GamePlay.GetHPColor(c), new Rectangle((c.CurPos.X - Game.ScreenX) * Globals.TileSize - Game.Player.OffSet.X + c.OffSet.X + 1, (c.CurPos.Y - Game.ScreenY) * Globals.TileSize - Game.Player.OffSet.Y + c.OffSet.Y - 9, w, 6));
                }

                ////Comment in đoạn dưới nếu muốn vẽ đường đi của quái
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
            foreach(Missile m in Game.World.MissileList)
            {
                if (OutOfSceen(m.HitBox)) continue;
                g.DrawImage(Textures.MissileImg[m.Source.srcImg], new Rectangle(m.curPos.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, m.curPos.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, m.Size.X, m.Size.Y), m.getSprite, GraphicsUnit.Pixel);
            }

            //Vẽ art sau nhân vật
            foreach (Art a in Game.World.AfterCharArts)
            {
                if (a.remove || OutOfSceen(a.Position)) continue;
                g.DrawImage(Textures.ArtImg[a.SrcImage], new Rectangle(a.Position.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X, a.Position.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y, a.Position.Width, a.Position.Height), a.getSprite(), GraphicsUnit.Pixel);
            }

            //Vẽ Layer2 và Layer3
            for (int X = -1; X <= Game.GameWindow.Size.Width / Globals.TileSize + 1; X += 1)
            {
                for (int Y = -1; Y <= Game.GameWindow.Size.Height / Globals.TileSize + 1; Y += 1)
                {
                    if (Game.ScreenX + X >= 0 && Game.ScreenX + X <= Game.World.Size.X && Game.ScreenY + Y >= 0 && Game.ScreenY + Y <= Game.World.Size.Y)
                    {
                        Rectangle srcRect = new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize);
                        Rectangle desRect = new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize);
                        if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcImg != "Tiny" || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcPos.X != 0 || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcPos.Y != 0)
                            g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[2].srcImg], desRect, srcRect, GraphicsUnit.Pixel);
                        if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcImg != "Tiny" || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcPos.X != 0 || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcPos.Y != 0)
                            g.DrawImage(Textures.TileImg[Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcImg], desRect, new Rectangle(Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcPos.X * Globals.TileSize, Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].Layers[3].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);

                        if (Game.EditorMode && Globals.Editor.cbxPalette.SelectedIndex == 0)
                        {
                            if (Globals.TilePalette.cbxMode.SelectedIndex == 1)
                            {
                                if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].IsBlocked == true) g.FillRectangle(Brushes.Red, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                            }
                            else if (Globals.TilePalette.cbxMode.SelectedIndex == 2)
                            {
                                if (Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].TouchTrigger == true || Game.World.TileList[Game.ScreenX + X, Game.ScreenY + Y].StepTrigger == true) g.DrawImage(Properties.Resources.Trigger, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                            }
                        }
                    }
                    else g.FillRectangle(Brushes.Black, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));

                    if (Game.EditorMode)
                        g.DrawRectangle(Pens.Black, new Rectangle(X * Globals.TileSize - Game.ScreenOffsetX, Y * Globals.TileSize - Game.ScreenOffsetY, Globals.TileSize, Globals.TileSize));
                }
            }

            //Vẽ các biểu cảm khi bị attack/bị status ailments

            //Vẽ các box infos
            if (!Game.EditorMode && Settings.ShowInfo && Game.InGame && !Game.InScene)
            {
                //Draw cây máu, mana, exp cho player
                g.DrawImage(Textures.DialogImg["SkillBox"], new Point(0, 0));
                g.DrawString(Game.Player.charType.Name + " (Level " + Game.Player.Level + ")", Globals.InfoFont, Brushes.Gold, new Point(13, 23));
                g.DrawString("HP", Globals.InfoFont, Brushes.White, new Point(13, 23 + 25 * 1));
                g.DrawString("MP", Globals.InfoFont, Brushes.White, new Point(13, 23 + 25 * 2));
                g.DrawString("EXP", Globals.InfoFont, Brushes.White, new Point(13, 23 + 25 * 3));
                int percentHP = Game.Player.HP * 100 / Game.Player.HPMax;
                int percentMP = 0;
                if (Game.Player.MPMax > 0) percentMP = Game.Player.MP * 100 / Game.Player.MPMax;
                g.FillRectangle(Brushes.Black, new Rectangle(70, 24 + 25 * 1, 140, 22));
                g.FillRectangle(GamePlay.GetHPColor(Game.Player), new Rectangle(71, 25 + 25 * 1, 138 * percentHP / 100, 20));
                g.FillRectangle(Brushes.Black, new Rectangle(70, 24 + 25 * 2, 140, 22));
                g.FillRectangle(Brushes.DodgerBlue, new Rectangle(71, 25 + 25 * 2, 138 * percentMP / 100, 20));
                g.FillRectangle(Brushes.Black, new Rectangle(70, 24 + 25 * 3, 140, 22));
                g.FillRectangle(Brushes.Orange, new Rectangle(71, 25 + 25 * 3, 138 * (int)((Game.Player.curEXP - Game.Player.previousEXP) * 100 / (Game.Player.nextEXP - Game.Player.previousEXP)) / 100, 20));
                g.DrawString("Tiền: " + Globals.Money, Globals.InfoFont, Brushes.Gold, new Point(13, 23 + 25 * 4));
                var item = Game.ItemButtons.First();
                g.DrawString("[Z]", Globals.InfoFont, Brushes.Gold, new Point(13, 30 + 25 * 3 + 40));
                g.DrawImage(Textures.ItemImg[item.Value.Source.srcImg], new Rectangle(45, 40 + 25 * 3 + 30, Globals.TileSize, Globals.TileSize), new Rectangle(item.Value.Source.srcPos.X * Globals.TileSize, item.Value.Source.srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                g.DrawString("x" + item.Value.Count.ToString(), Globals.Font, Brushes.White, new Point(45 + Globals.TileSize, 35 + 25 * 3 + 40));
                item = Game.ItemButtons.Last();
                g.DrawString("[X]", Globals.InfoFont, Brushes.Gold, new Point(115, 30 + 25 * 3 + 40));
                g.DrawImage(Textures.ItemImg[item.Value.Source.srcImg], new Rectangle(147, 37 + 25 * 3 + 30, Globals.TileSize, Globals.TileSize), new Rectangle(item.Value.Source.srcPos.X * Globals.TileSize, item.Value.Source.srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                g.DrawString("x" + item.Value.Count.ToString(), Globals.Font, Brushes.White, new Point(147 + Globals.TileSize, 35 + 25 * 3 + 40));

                g.DrawImage(Textures.DialogImg["SkillBox"], new Point(Game.bb.Width - 243, 0));
                g.DrawString("SKILLS", Globals.InfoFont, Brushes.White, new Point(Game.bb.Width - 243 + 13, 20));
                int i = 1;
                string c;
                foreach (var s in Game.SkillButtons)
                {
                    if (i == 1) c = "_";
                    else c = s.Key.ToString();

                    int BoxX = Game.bb.Width - 243 + 40;
                    int BoxW = 100;
                    g.DrawString("[" + c + "]", Globals.InfoFont, GamePlay.GetSkillStringColor(s.Value), new Point(Game.bb.Width - 243 + 13, 20 + 25 * i));
                    g.DrawImage(Textures.ArtImg[Globals.Skills[Game.Player.charType.Index][i - 1].srcImg], new Rectangle(BoxX + 15, 20 + 25 * i-3, Globals.TileSize, Globals.TileSize), new Rectangle(Globals.Skills[Game.Player.charType.Index][i - 1].srcPos.X, Globals.Skills[Game.Player.charType.Index][i - 1].srcPos.Y * Globals.TileSize, Globals.TileSize, Globals.TileSize), GraphicsUnit.Pixel);
                    g.FillRectangle(Brushes.Black, new Rectangle(BoxX + 60, 21 + 25 * i, BoxW, 22));
                    if(s.Value.CooldownTime == 0)
                        g.FillRectangle(Brushes.LightGoldenrodYellow, new Rectangle(BoxX + 61, 22 + 25 * i, BoxW-2, 20));
                    else if(s.Value.Cooldown == 0)
                        g.FillRectangle(Brushes.Orange, new Rectangle(BoxX + 61, 22 + 25 * i, (BoxW - 2) * ((s.Value.CooldownTime - s.Value.Cooldown) * 100 / s.Value.CooldownTime) / 100, 20));
                    else
                        g.FillRectangle(Brushes.Brown, new Rectangle(BoxX + 61, 22 + 25 * i, (BoxW - 2) * ((s.Value.CooldownTime - s.Value.Cooldown) * 100 / s.Value.CooldownTime) / 100, 20));


                    i += 1;
                    if (i > 5) break;
                }
            }

            //Dành cho Map Editor

            //Draw Debug
            //g.DrawString(Game.World.MapChar.Count.ToString(), Globals.Font, Brushes.Red, new Point(2, 2));
        }

        private bool OutOfSceen(Rectangle area)
        {
            int inScreenX = area.X - Game.ScreenX * Globals.TileSize - Game.Player.OffSet.X;
            int inScreenY = area.Y - Game.ScreenY * Globals.TileSize - Game.Player.OffSet.Y;

            if (inScreenX + area.Width < 0 || inScreenX > Game.Frame.Width || inScreenY + area.Height < 0 || inScreenY > Game.Frame.Height)
                return true;
            else return false;
        }

        public override void Unload()
        {
            if (MapName != null) MapName.Dispose();
            MapName = null;
            Globals.Main.label1.Visible = false;
            Game.DoResetGame = true;
            base.Unload();
        }

        public override void MouseClick(MouseButtons Button)
        {
            
        }
    }
}
