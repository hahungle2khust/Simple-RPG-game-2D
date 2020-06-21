using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;

namespace Test
{
    class Globals
    {
        public const bool DEBUGMODE = true;
        public static string GameDir = Application.StartupPath;
        public const int TileSize = 32;
        public const int AniInterval = 75;  //ms
        public const int CHARACTER_CHANGE_DIR_INTERVAL = 1000; //ms
        public const int AI_FIND_NEW_PATH_INTERVAL = 3000;
        public const int ITEM_DROP_PERCENTAGE = 15; //%
        public const int AI_MOVE_CYCLE_INTERVAL = 500;
        public const int MAX_FRAME_PER_SECOND = 60;  //Game FPS
        public static int resWidth = 960;
        public static int resHeight = 640;
        public static double resPercent = resHeight * 1.0 / resWidth;

        public static Font Font = new Font("arial", 12);

        public static Form1 Main;
        public static Forms.Character_Palette CharacterPalette;
        public static Forms.Editor Editor;
        public static Forms.Tile_Palette TilePalette;
        public static Forms.ScenePalette ScenePalette;

        public static Font DialogFont = new Font("Times New Roman", 15);
        public static Font NameBoxFont = new Font("Courier New", 20, FontStyle.Bold);
        public static Font InfoFont = new Font("Times New Roman", 15, FontStyle.Bold);

        public static string SelectedName;
        public static int SelectedX;
        public static int SelectedY;
        public static int selectedLayer;

        public static Dictionary<int, CharType> CharTypeList = new Dictionary<int, CharType>();

        public static Random gen = new Random();

        public static HardMode Mode;
        public static int Money;
        public static Dictionary<string, string> Flags = new Dictionary<string, string>() { };
        public static string CurrentMap;

        public static Dictionary<int, List<TileLayer>> Skills = new Dictionary<int, List<TileLayer>>()
        {
            { 5, new List<TileLayer>() {new TileLayer("Sword", 0, 2), new TileLayer("Pistol", 0, 2), new TileLayer("Shotgun", 0, 2), new TileLayer("Bomb", 0, 0) } },
            { 3, new List<TileLayer>() {new TileLayer("Sword", 0, 2), new TileLayer("Bow", 0, 2), new TileLayer("Charge", 0, 0), new TileLayer("Shotgun", 0, 2) } },
            { 6, new List<TileLayer>() {new TileLayer("Saber", 0, 2), new TileLayer("Bow", 0, 2), new TileLayer("Dash", 0, 0), new TileLayer("Shield", 0, 2) } },
            { 4, new List<TileLayer>() {new TileLayer("Saber", 0, 2), new TileLayer("Pistol", 0, 2), new TileLayer("Bomb", 0, 0), new TileLayer("Charge", 0, 0) } }
        };

        public static void InitializeFlags()
        {
            //Add các flag ở đây
            Flags.Add("C1", "Uncleared");
            Flags.Add("C9", "Uncleared");
            Flags.Add("D3", "Uncleared");
            Flags.Add("SubCleared", "false");
            //Flags.Add("StartMap", "Uncleared");
            Flags.Add("TQB", "Uncleared");

            //Test
            //Flags["D3"] = "Cleared";
            //Flags["C9"] = "Cleared";
            //Flags["TQB"] = "Cleared";
        }
    }
}
