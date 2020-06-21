using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace Test
{
    class Textures
    {
        public static string TextureDir = Globals.GameDir + "\\Content\\Textures\\";

        public static Dictionary<string, Image> TileImg = new Dictionary<string, Image>();
        public static Dictionary<string, Image> CharImg = new Dictionary<string, Image>();
        public static Dictionary<string, Image> ArtImg = new Dictionary<string, Image>();

        public static void Initialize()
        {
            LoadSource(TileImg, TextureDir + "Tiles\\");
            LoadSource(CharImg, TextureDir + "Characters\\");
            LoadSource(ArtImg, TextureDir + "Arts\\");

            //TileImg.Add("Tiny", Image.FromFile(TextureDir + "Tiles\\Tiny.png"));
            //CharImg.Add("Tiny", Image.FromFile(TextureDir + "Characters\\Tiny.png"));
        }

        public static void LoadSource(Dictionary<string, Image> LoadTo, string path)
        {
            DirectoryInfo tmpDir = new DirectoryInfo(path);
            FileInfo[] files = tmpDir.GetFiles();

            //for(int i=0; i<= files.Length; i += 1)
            //{
            //    files[i]
            //}

            foreach (FileInfo file in files)
            {
                LoadTo.Add(file.Name.Split('.')[0], Image.FromFile(file.FullName));
            }

        }

    }
}
