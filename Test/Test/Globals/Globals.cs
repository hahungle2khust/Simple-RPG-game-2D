using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Test
{
    class Globals
    {
        public const bool DEBUGMODE = true;
        public static string GameDir = Application.StartupPath;
        public const int TileSize = 32;
        public const int AniInterval = 75;  //ms
        public const int CHARACTER_CHANGE_DIR_INTERVAL = 1000; //ms
        public const int AI_FIND_NEW_PATH_INTERVAL = 500;

        public static System.Drawing.Font Font = new System.Drawing.Font("arial", 12);

        public static Form1 Main;
        public static Forms.Character_Palette CharacterPalette;
        public static Forms.Editor Editor;
        public static Forms.Tile_Palette TilePalette;
        public static Forms.ScenePalette ScenePalette;
        public static Forms.MapChar MapChar;

        public static System.Drawing.Font DialogFont = new System.Drawing.Font("Arial", 12.0F);

        public static string SelectedName;
        public static int SelectedX;
        public static int SelectedY;
        public static int selectedLayer;

        public static Dictionary<int, CharType> CharTypeList = new Dictionary<int, CharType>();

        public static Random gen = new Random();

        public static HardMode Mode = HardMode.Normal;
    }
}
