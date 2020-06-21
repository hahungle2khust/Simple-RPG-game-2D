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

        public static Dictionary<string, Bitmap> TileImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> CharImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> ArtImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> MissileImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> WeaponImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> DialogImg = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> ItemImg = new Dictionary<string, Bitmap>();

        public static List<Bitmap> Title = new List<Bitmap>();
        public static Image SaveLoadBG;

        public static void Initialize()
        {
            LoadSource(TileImg, TextureDir + "Tiles\\");
            LoadSource(CharImg, TextureDir + "Characters\\");
            LoadSource(ArtImg, TextureDir + "Arts\\");
            LoadSource(MissileImg, TextureDir + "Missiles\\");
            LoadSource(DialogImg, TextureDir + "Dialogs\\");
            LoadSource(ItemImg, TextureDir + "Items\\");
            LoadSource(Title, Globals.GameDir + "\\Title\\");
            SaveLoadBG = Image.FromFile(TextureDir + @"\Images\SaveLoadBG.png");
        }

        public static void LoadSource(Dictionary<string, Bitmap> LoadTo, string path)
        {
            DirectoryInfo tmpDir = new DirectoryInfo(path);
            FileInfo[] files = tmpDir.GetFiles();

            foreach (FileInfo file in files)
            {
                LoadTo.Add(file.Name.Split('.')[0], (Bitmap)Image.FromFile(file.FullName));
            }

        }

        public static void LoadSource(List<Bitmap> LoadTo, string path)
        {
            DirectoryInfo tmpDir = new DirectoryInfo(path);
            FileInfo[] files = tmpDir.GetFiles();

            foreach (FileInfo file in files)
            {
                if(file.Name.Substring(file.Name.Length-4) == ".jpg" )
                    LoadTo.Add((Bitmap)Image.FromFile(file.FullName));
            }
        }

    }
}
