using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;

namespace Test
{
    static class DataHandler
    {
        public static void LoadCharType()
        {
            string path = Globals.GameDir + "\\Data\\CharTypeList.dat";

            if (File.Exists(path) == false) return;

            FileStream fs = new FileStream(path, FileMode.Open);
            GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress);
            BinaryReader reader = new BinaryReader(zipStream);

            Globals.CharTypeList.Clear();
            int count = reader.ReadInt32() - 1;
            for(int i=0; i <= count; i += 1)
            {
                CharType c = new CharType();
                c.Name = reader.ReadString();
                c.Index = reader.ReadInt32();
                c.Suffix = reader.ReadString();
                c.Size = new System.Drawing.Point(reader.ReadInt32(), reader.ReadInt32());
                c.Source = new TileLayer(reader.ReadString(), reader.ReadInt32(), reader.ReadInt32());
                c.WaitAfterAttack = new System.Drawing.Point(reader.ReadInt32(), reader.ReadInt32());
                c.baseHP = reader.ReadInt32();
                c.baseMP = reader.ReadInt32();
                c.baseAtk = reader.ReadInt32();
                c.baseDef = reader.ReadInt32();
                c.baseSAtk = reader.ReadInt32();
                c.baseSDef = reader.ReadInt32();
                c.baseSpeed = reader.ReadInt32();
                c.luHP = reader.ReadInt32();
                c.luMP = reader.ReadInt32();
                c.luAtk = reader.ReadInt32();
                c.luDef = reader.ReadInt32();
                c.luSAtk = reader.ReadInt32();
                c.luSDef = reader.ReadInt32();
                c.luSpeed = reader.ReadInt32();
                c.Range = reader.ReadInt32();
                c.IsMerchant = reader.ReadBoolean();
                c.IsNPC = reader.ReadBoolean();
                c.MerchantMoney = reader.ReadInt32();
                Globals.CharTypeList.Add(c.Index, c);
            }


            reader.Close();
            reader.Dispose();
            zipStream.Close();
            zipStream.Dispose();
            fs.Close();
            fs.Dispose();
        }

        public static void SaveCharType()
        {
            string path = Globals.GameDir + "\\Data\\CharTypeList.dat";

            FileStream fs = new FileStream(path,FileMode.Create);
            GZipStream gzip = new GZipStream(fs, CompressionMode.Compress);
            BinaryWriter writer = new BinaryWriter(gzip);

            writer.Write(Globals.CharTypeList.Count);
            foreach(CharType c in Globals.CharTypeList.Values)
            {
                writer.Write(c.Name);
                writer.Write(c.Index);
                writer.Write(c.Suffix);
                writer.Write(c.Size.X);
                writer.Write(c.Size.Y);
                writer.Write(c.Source.srcImg);
                writer.Write(c.Source.srcPos.X);
                writer.Write(c.Source.srcPos.Y);
                writer.Write(c.WaitAfterAttack.X);
                writer.Write(c.WaitAfterAttack.Y);
                writer.Write(c.baseHP);
                writer.Write(c.baseMP);
                writer.Write(c.baseAtk);
                writer.Write(c.baseDef);
                writer.Write(c.baseSAtk);
                writer.Write(c.baseSDef);
                writer.Write(c.baseSpeed);
                writer.Write(c.luHP);
                writer.Write(c.luMP);
                writer.Write(c.luAtk);
                writer.Write(c.luDef);
                writer.Write(c.luSAtk);
                writer.Write(c.luSDef);
                writer.Write(c.luSpeed);
                writer.Write(c.Range);
                writer.Write(c.IsMerchant);
                writer.Write(c.IsNPC);
                writer.Write(c.MerchantMoney);
            }


            writer.Close();
            writer.Dispose();
            gzip.Close();
            gzip.Dispose();
            fs.Close();
            fs.Dispose();
        }

        public static Scene LoadScene(string path)
        {
            if (File.Exists(path) == false) return null;

            Scene scene = new Scene();

            FileStream fs = new FileStream(path, FileMode.Open);
            GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress);
            BinaryReader reader = new BinaryReader(zipStream);

            int count = reader.ReadInt32();
            for(int i=0; i<=count-1; i += 1)
            {
                SceneFrame newFrame = new SceneFrame();
                newFrame.BGFileName = reader.ReadString();
                int c = reader.ReadInt32();
                for(int j=0; j<= c-1; j += 1)
                {
                    newFrame.Characters.Add(new CharImage(reader.ReadString(), new System.Drawing.Rectangle(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32())));
                }
                newFrame.Name = reader.ReadString();
                newFrame.Text = reader.ReadString();
                newFrame.Script = reader.ReadString();
                newFrame.JumpTo = reader.ReadInt32();
                newFrame.JumpCondition = reader.ReadString();
                newFrame.IsSelection = reader.ReadBoolean();
                c = reader.ReadInt32();
                for (int j = 0; j <= c - 1; j += 1)
                {
                    newFrame.Selections.Add(reader.ReadString());
                }
                c = reader.ReadInt32();
                for (int j = 0; j <= c - 1; j += 1)
                {
                    newFrame.JumpAfterSelect.Add(reader.ReadInt32());
                }
                c = reader.ReadInt32();
                for (int j = 0; j <= c - 1; j += 1)
                {
                    newFrame.Sounds.Add(reader.ReadString());
                }

                scene.Frames.Add(newFrame);
            }


            reader.Close();
            reader.Dispose();
            zipStream.Close();
            zipStream.Dispose();
            fs.Close();
            fs.Dispose();

            return scene;
        }

        public static void SaveScene(Scene scene, string path)
        {
            if (scene == null) return;

            FileStream fs = new FileStream(path, FileMode.Create);
            GZipStream gzip = new GZipStream(fs, CompressionMode.Compress);
            BinaryWriter writer = new BinaryWriter(gzip);

            writer.Write(scene.Frames.Count);
            foreach(SceneFrame f in scene.Frames)
            {
                writer.Write(f.BGFileName);
                writer.Write(f.Characters.Count);
                foreach(CharImage c in f.Characters)
                {
                    writer.Write(c.FileName);
                    writer.Write(c.Position.X);
                    writer.Write(c.Position.Y);
                    writer.Write(c.Position.Width);
                    writer.Write(c.Position.Height);
                }
                writer.Write(f.Name);
                writer.Write(f.Text);
                writer.Write(f.Script);
                writer.Write(f.JumpTo);
                writer.Write(f.JumpCondition);
                writer.Write(f.IsSelection);
                writer.Write(f.Selections.Count);
                foreach(string s in f.Selections)
                {
                    writer.Write(s);
                }
                writer.Write(f.JumpAfterSelect.Count);
                foreach(int j in f.JumpAfterSelect)
                {
                    writer.Write(j);
                }
                writer.Write(f.Sounds.Count);
                foreach (string s in f.Sounds)
                {
                    writer.Write(s);
                }
            }

            writer.Close();
            writer.Dispose();
            gzip.Close();
            gzip.Dispose();
            fs.Close();
            fs.Dispose();
        }

        public static Scene LoadScreneByName(string FileName)
        {
            return LoadScene(Globals.GameDir + @"\Data\Scenes\" + FileName + ".dat");
        }

    }
}
