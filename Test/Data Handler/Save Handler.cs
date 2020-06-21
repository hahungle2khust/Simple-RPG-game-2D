using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Input;

namespace Test
{
    public class SaveHandler
    {
        public static void SaveGame(string path)
        {
            using(FileStream fs = new FileStream(path, FileMode.Create))
            {
                using(GZipStream gzip = new GZipStream(fs, CompressionMode.Compress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(gzip, Save.GetSave());
                    gzip.Close();
                }
                fs.Close();
            }
        }

        public static void LoadGame(string path)
        {
            if (File.Exists(path) == false) return;

            Save save;
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using(GZipStream gzip = new GZipStream(fs, CompressionMode.Decompress))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    save = (Save)bf.Deserialize(gzip);
                    gzip.Close();
                }
                fs.Close();
            }
            Save.Load(save);
        }
    }

    [Serializable]
    public class Save
    {
        public DateTime SavedTime;
        public Character Player;
        public int Money;
        public HardMode Mode;
        public string CurMap;
        public Map Map;
        public Dictionary<string, string> Flags;
        public Dictionary<Key, BaseSkill> SkillButtons;
        public Dictionary<Key, Item> ItemButtons;
        public string CurBGM;
        public bool BGMLoop;
        public double SoundVolume = 1.0;
        public int MusicVolume = 100;
        public bool ShowInfo = true;
        public bool ShowHPBars = true;

        public Save()
        {
            SavedTime = DateTime.Now;
        }

        public static Save GetSave()
        {
            Save save = new Save();

            save.Player = Game.Player;
            save.Money = Globals.Money;
            save.Mode = Globals.Mode;
            save.CurMap = Globals.CurrentMap;
            save.Map = Game.World;
            save.Flags = Globals.Flags;
            save.SkillButtons = Game.SkillButtons;
            save.ItemButtons = Game.ItemButtons;
            if (BGM.BGMPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
                save.CurBGM = "";
            else
                save.CurBGM = BGM.BGMPlayer.currentMedia.name;
            save.BGMLoop = BGM.BGMPlayer.settings.getMode("loop");
            save.SoundVolume = Settings.SoundVolume;
            save.MusicVolume = Settings.MusicVolume;
            save.ShowInfo = Settings.ShowInfo;
            save.ShowHPBars = Settings.ShowHPBars;

            return save;
        }

        public static void Load(Save save)
        {
            Game.Player = save.Player;
            ScreenManager.AddScreen(new WorldScreen());
            Game.World = save.Map;
            Globals.CurrentMap = save.CurMap;
            Game.worldscreen.ShowMapName(5000);
            Globals.Money = save.Money;
            Globals.Mode = save.Mode;
            Globals.Flags = save.Flags;
            Game.SkillButtons = save.SkillButtons;
            Game.ItemButtons = save.ItemButtons;
            if (save.CurBGM.Length > 0)
                BGM.PlayByFileName(save.CurBGM);
            BGM.BGMPlayer.settings.setMode("loop", save.BGMLoop);
            save.BGMLoop = BGM.BGMPlayer.settings.getMode("loop");
            Settings.SoundVolume = save.SoundVolume;
            Settings.MusicVolume = save.MusicVolume;
            Settings.ShowInfo = save.ShowInfo;
            Settings.ShowHPBars = save.ShowHPBars;

            Game.InGame = true;
        }
    }
}
