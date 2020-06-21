using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.IO;

namespace Test
{
    public static class SoundManager
    {
        public static List<Sound> SoundList = new List<Sound>();

       public static bool PlayByFileName(string FileName, bool loop = false)
        {
            string path = Globals.GameDir + @"\Content\SFX\" + FileName + ".wav";

            if (!File.Exists(path))
                return false;
            else
                Play(path, loop);

            return true;
        }

        public static bool Play(string path, bool Loop = false)
        {
            if (File.Exists(path) == false) return false;

            MediaPlayer mp = new MediaPlayer();
            mp.Open(new Uri(path));
            mp.Volume = Settings.SoundVolume;
            SoundList.Add(new Sound(mp, path.Split('\\').Last().Substring(0, path.Split('\\').Last().Length - 4), Loop));
            mp.Play();
            return true;
        }

        public static void Stop(string FileNameOnly)
        {
            for (int i = 0; i < SoundList.Count; i += 1)
            {
                if (SoundList[i].FileName == FileNameOnly || FileNameOnly.ToLower() == "/all".ToLower())
                {
                    SoundList[i].Player.Stop();
                    SoundList[i].Player.Close();
                    SoundList.RemoveAt(i);
                    i -= 1;
                }
            }
        }

        public static void Update()
        {
            for (int i = 0; i < SoundList.Count; i += 1)
            {
                //Lặp trong list Sound xem những sound nào đã chạy xong
                if (SoundList[i].Player.NaturalDuration.HasTimeSpan && SoundList[i].Player.Position.TotalMilliseconds >= SoundList[i].Player.NaturalDuration.TimeSpan.TotalMilliseconds)
                {
                    
                    if (SoundList[i].Loop)
                    {
                        //Chạy xong mà loop = true thì chạy lại
                        SoundList[i].Player.Position = new TimeSpan(0, 0, 0);
                        SoundList[i].Player.Play();
                    }
                    else
                    {
                        //Chạy xong mà loop = false thì close + xóa khỏi list
                        if (SoundList[i].FileName == "MapCleared")
                            BGM.PlayByFileName(Globals.CurrentMap);
                        SoundList[i].Player.Close();
                        SoundList.RemoveAt(i);
                        i -= 1;
                    }
                }
            }
        }
    }

    public class Sound
    {
        public string FileName;
        public MediaPlayer Player;
        public bool Loop;

        public Sound(MediaPlayer Player, string FileName = "", bool Loop = false)
        {
            this.Player = Player;
            this.FileName = FileName;
            this.Loop = Loop;
        }
    }
}
