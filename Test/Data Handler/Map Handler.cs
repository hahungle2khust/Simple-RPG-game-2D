using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Drawing;

namespace Test
{
    class MapHandler
    {
        public static void LoadMap(string name)
        {
            string path = Globals.GameDir + "\\Map\\" + name + ".map";

            if (File.Exists(path) == true)
            {
                Globals.CurrentMap = name;
                FileStream fs = new FileStream(path, FileMode.Open);
                GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress);
                BinaryReader reader = new BinaryReader(zipStream);

                Game.World = new Map(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());

                foreach (Tile t in Game.World.TileList)
                {
                    t.Location.X = reader.ReadInt32();
                    t.Location.Y = reader.ReadInt32();
                    foreach (TileLayer tl in t.Layers)
                    {
                        tl.srcImg = reader.ReadString();
                        tl.srcPos.X = reader.ReadInt32();
                        tl.srcPos.Y = reader.ReadInt32();
                    }
                    t.IsBlocked = reader.ReadBoolean();
                    t.TouchTrigger = reader.ReadBoolean();
                    t.StepTrigger = reader.ReadBoolean();
                    t.Script = reader.ReadString();
                }

                int count = reader.ReadInt32() - 1;
                for (int i = 0; i <= count; i += 1)
                {
                    Character newChar = new Character(reader.ReadInt32());
                    newChar.team = (Team)reader.ReadInt32();
                    newChar.team = Team.Enemy;
                    CharType.SetSkill(newChar);
                    newChar.Level = reader.ReadInt32();
                    newChar.LastDir = (Dir)reader.ReadInt32();
                    newChar.CurPos = new Point(reader.ReadInt32(), reader.ReadInt32());
                    Game.World.MapChar.Add(newChar);
                }

                reader.Close();
                reader.Dispose();
                zipStream.Close();
                zipStream.Dispose();
                fs.Close();
                fs.Dispose();

                if (Game.EditorMode)
                    Globals.Editor.CurrentMap = path;
                Game.worldscreen.ShowMapName(5000);
                if (name == "C1") BGM.PlayByFileName("C1");
            }
        }
    }
}
