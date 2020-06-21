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
                    newChar.Skills.Add(new sAttack());
                    newChar.Skills.Last().Owner = newChar;
                    newChar.Level = reader.ReadInt32();
                    newChar.LastDir = (Dir)reader.ReadInt32();
                    newChar.CurPos = new Point(reader.ReadInt32(), reader.ReadInt32());
                    Game.World.MapChar.Add(newChar);
                }

                //for (int i = 0; i <= Game.World.Size.X; i += 1)
                //{
                //    for (int j = 0; j <= Game.World.Size.Y; j += 1)
                //    {
                //        if (i == 0 || i == Game.World.Size.X || j == 0 || j == Game.World.Size.Y)
                //        {
                //            Game.World.TileList[i, j].IsBlocked = true;
                //            Game.World.TileList[i, j].Layers[0].srcImg = "Tiny";
                //            Game.World.TileList[i, j].Layers[0].srcPos = new Point(0, 0);
                //        }
                //    }
                //}

                reader.Close();
                reader.Dispose();
                zipStream.Close();
                zipStream.Dispose();
                fs.Close();
                fs.Dispose();
            }
        }
    }
}
