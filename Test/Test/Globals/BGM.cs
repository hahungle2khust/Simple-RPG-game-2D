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

        public static void PlayBGM(string path)
        {
            BGMPlayer.URL = path;
            BGMPlayer.controls.play();
            BGMPlayer.settings.setMode("loop", true);
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
