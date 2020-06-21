using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Test
{
    public static class EventManager
    {
        public static void OnCharacterDies(CharacterDies e)
        {
            if (e.Target == Game.Player)
            {
                //chạy code thua và trở về Title
                BGM.StopBGM();
                Game.InGame = false;
                Game.worldscreen.Unload();
                Globals.Main.videoPlayer.Visible = true;
                Globals.Main.videoPlayer.URL = Globals.GameDir + "\\Title\\GameOver.mp4";
                Globals.Main.videoPlayer.Ctlcontrols.play();
                return;
            }
            else if(e.Target.team == Team.Enemy)
            {
                //Drop Item
                int random = Globals.gen.Next(0, 101);
                if (random <= Globals.ITEM_DROP_PERCENTAGE)
                {
                    Item item;
                    int ran = Globals.gen.Next(0, 101);
                    if (ran <= 70)
                        item = new iRestoreHP(Game.Player);
                    else if (random <= 100)
                        item = new iRestoreMP(Game.Player);
                    else
                        item = new iMoney(Game.Player);
                    item.LocInMap = new Rectangle(e.Target.CurRawPos.X, e.Target.CurRawPos.Y, Globals.TileSize, Globals.TileSize);
                    Game.World.ItemList.Add(item);
                }
            }
            Game.World.DiedChar.Add(e.Target);
            if (e.Attacker == Game.Player)
            {
                Game.Player.curEXP += (int)((e.Target.Level / 3 + 1.0) * Globals.gen.Next(100, 201));
                while (Game.Player.curEXP >= Game.Player.nextEXP)
                {
                    Game.Player.Level += 1;
                    Game.Player.previousEXP = Game.Player.nextEXP;
                    Game.Player.nextEXP += (int)((10 + (Game.Player.Level * 3)) * 150 * (Game.Player.Level / 3 + 1.0));
                    Game.Player.HPMax += Game.Player.charType.luHP;
                    Game.Player.MPMax += Game.Player.charType.luMP;
                    Game.Player.ATK += Game.Player.charType.luAtk;
                    Game.Player.DEF += Game.Player.charType.luDef;
                    Game.Player.SATK += Game.Player.charType.luSAtk;
                    Game.Player.SDEF += Game.Player.charType.luSDef;
                    Game.Player.Speed += Game.Player.charType.luSpeed;
                    //Thêm các thứ muốn cho khi người chơi lên level
                }
            }

            //Check Flags
            if(CheckMapEnemy() && e.Target.charType.Index != 10 && e.Target.charType.Index != 7)
            {
                //Set flag cho map đã clear
                if (Globals.Flags.ContainsKey(Globals.CurrentMap) && Globals.Flags[Globals.CurrentMap] == "Uncleared")
                {
                    //Tạo boss của map
                    Character subBoss = new Character(10);
                    subBoss.team = Team.Enemy;
                    CharType.SetSkill(subBoss);
                    if (Globals.CurrentMap == "C9")
                        subBoss.CurPos = new Point(20, 11);
                    else if(Globals.CurrentMap == "D3")
                        subBoss.CurPos = new Point(17, 13);
                    else if (Globals.CurrentMap == "TQB")
                        subBoss.CurPos = new Point(21, 15);
                    else if (Globals.CurrentMap == "C1")
                        subBoss.CurPos = new Point(19, 11);
                    else Globals.Flags[Globals.CurrentMap] = "Cleared";
                    subBoss.ResetPos();
                    Game.World.NewChar.Add(subBoss);
                    BGM.PlayByFileName("SubBoss");
                }
            }

            if(e.Target.charType.Index == 10)
            {
                if (Globals.Flags.ContainsKey(Globals.CurrentMap))
                {
                    //Đánh xong boss phụ các map khác --> clear map đó
                    if (Globals.CurrentMap == "C1" && Globals.Flags[Globals.CurrentMap] != "Cleared")
                    {
                        //Đánh xong boss phụ map C1 --> Tạo boss cuối
                        Globals.Flags[Globals.CurrentMap] = "Cleared";
                        Character Boss = new Character(7);
                        Boss.team = Team.Enemy;
                        CharType.SetSkill(Boss);
                        Boss.CurPos = new Point(19, 11);
                        Game.World.NewChar.Add(Boss);
                        ScreenManager.AddScreen(new SceneScreen(DataHandler.LoadScreneByName("FinalBoss")));
                    }
                    else
                    {
                        Globals.Flags[Globals.CurrentMap] = "Cleared";
                        BGM.StopBGM();
                        SoundManager.PlayByFileName("MapCleared");
                        ScreenManager.AddScreen(new DummyScreen(8500));
                    }

                    if (Globals.Flags["SubCleared"] == "false" && Globals.Flags["C9"] == "Cleared" && Globals.Flags["D3"] == "Cleared" && Globals.Flags["TQB"] == "Cleared")
                        Globals.Flags["SubCleared"] = "true";
                }
            }
            else if(e.Target.charType.Index == 7)
            {
                BGM.StopBGM();
                SoundManager.PlayByFileName("BossDies");
                ScreenManager.AddScreen(new DummyScreen(5000));
                Game.Ending = true;
            }
            else if (e.Target.charType.Index == 8 || e.Target.charType.Index == 9)
            {
                SoundManager.PlayByFileName("GirlShout" + (e.Target.charType.Index - 7).ToString());
            }
        }

        public static bool CheckMapEnemy()
        {
            foreach(Character c in Game.World.MapChar)
            {
                if (c.team == Team.Enemy && Game.World.DiedChar.Contains(c) == false) return false;
            }

            return true;
        }
    }
}
