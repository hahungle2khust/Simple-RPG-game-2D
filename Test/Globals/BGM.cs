using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Test
{
    public static class BGM
    {
        public static WindowsMediaPlayer BGMPlayer = new WindowsMediaPlayer();

        public static void PlayByFileName(string FileName, bool loop = true)
        {
            if (BGMPlayer.playState == WMPPlayState.wmppsPlaying && BGMPlayer.currentMedia.name == FileName)
                return;
            PlayBGM(Globals.GameDir + @"\Content\BGM\" + FileName + ".mp3", loop);
        }

        public static void PlayBGM(string path, bool loop = true)
        {
            BGMPlayer.URL = path;
            BGMPlayer.controls.play();
            BGMPlayer.settings.setMode("loop", loop);
            BGMPlayer.settings.volume = 100;
        }

        public static void StopBGM()
        {
            BGMPlayer.controls.stop();
        }

        public static void PauseBGM()
        {
            BGMPlayer.controls.pause();
        }

        public static void ResumeBGM()
        {
            BGMPlayer.controls.play();
        }
    }
}
