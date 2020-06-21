using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Test
{
    public static class SFX
    {
        public static MediaPlayer SFXPlayer = new MediaPlayer();

        public static void PlaySFX(string path)
        {
            SFXPlayer.Open(new Uri(path));
            SFXPlayer.Play();
        }
    }
}
