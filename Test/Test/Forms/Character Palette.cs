using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test.Forms
{
    public partial class Character_Palette : Form
    {
        public Character_Palette()
        {
            InitializeComponent();
        }

        private void Character_Palette_Load(object sender, EventArgs e)
        {
            //Globals.CharacterPalette = this;
            this.Location = new Point(Globals.Editor.Location.X + Globals.Editor.Size.Width - 10, Globals.Editor.Location.Y);
            RefreshList();
        }

        private void RefreshList()
        {
            lbCharList.Items.Clear();
            foreach (CharType c in Globals.CharTypeList.Values)
            {
                lbCharList.Items.Add(c.Index.ToString() + ": " + c.Name + " (" + c.Suffix + ")");
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            CharType newCharType = new CharType();
            newCharType.Name = tbxName.Text;
            newCharType.Suffix = tbxSuffix.Text;
            newCharType.Index = GetUnsedIndex();
            newCharType.Size = new Point((int)nudWidth.Value, (int)nudHeight.Value);
            newCharType.Source = new TileLayer(tbxSrcImage.Text, (int)nudSrcX.Value, (int)nudSrcY.Value);
            newCharType.baseHP = (int)nudBaseHP.Value;
            newCharType.baseMP = (int)nudBaseMP.Value;
            newCharType.baseAtk = (int)nudBaseAtk.Value;
            newCharType.baseDef = (int)nudBaseDef.Value;
            newCharType.baseSAtk = (int)nudBaseSAtk.Value;
            newCharType.baseSDef = (int)nudBaseSDef.Value;
            newCharType.baseSpeed = (int)nudBaseSpeed.Value;
            newCharType.luHP = (int)nudLUHP.Value;
            newCharType.luMP = (int)nudLUMP.Value;
            newCharType.luAtk = (int)nudLUAtk.Value;
            newCharType.luDef = (int)nudLUDef.Value;
            newCharType.luSAtk = (int)nudLUSAtk.Value;
            newCharType.luSDef = (int)nudLUSDef.Value;
            newCharType.luSpeed = (int)nudLUSpeed.Value;
            newCharType.Range = (int)nudRange.Value;
            newCharType.IsNPC = cbxNPC.Checked;
            newCharType.IsMerchant = cbxMerchant.Checked;

            Globals.CharTypeList.Add(newCharType.Index, newCharType);
            DataHandler.SaveCharType();
            RefreshList();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if(lbCharList.SelectedIndex > -1)
            {
                int selectedIndex = Convert.ToInt32(lbCharList.Items[lbCharList.SelectedIndex].ToString().Split(':')[0]);
                CharType newCharType = Globals.CharTypeList[selectedIndex];
                newCharType.Name = tbxName.Text;
                newCharType.Suffix = tbxSuffix.Text;
                newCharType.Size = new Point((int)nudWidth.Value, (int)nudHeight.Value);
                newCharType.Source = new TileLayer(tbxSrcImage.Text, (int)nudSrcX.Value, (int)nudSrcY.Value);
                newCharType.baseHP = (int)nudBaseHP.Value;
                newCharType.baseMP = (int)nudBaseMP.Value;
                newCharType.baseAtk = (int)nudBaseAtk.Value;
                newCharType.baseDef = (int)nudBaseDef.Value;
                newCharType.baseSAtk = (int)nudBaseSAtk.Value;
                newCharType.baseSDef = (int)nudBaseSDef.Value;
                newCharType.baseSpeed = (int)nudBaseSpeed.Value;
                newCharType.luHP = (int)nudLUHP.Value;
                newCharType.luMP = (int)nudLUMP.Value;
                newCharType.luAtk = (int)nudLUAtk.Value;
                newCharType.luDef = (int)nudLUDef.Value;
                newCharType.luSAtk = (int)nudLUSAtk.Value;
                newCharType.luSDef = (int)nudLUSDef.Value;
                newCharType.luSpeed = (int)nudLUSpeed.Value;
                newCharType.Range = (int)nudRange.Value;
                newCharType.IsNPC = cbxNPC.Checked;
                newCharType.IsMerchant = cbxMerchant.Checked;
                DataHandler.SaveCharType();
                RefreshList();
            }
        }


        private void btnDel_Click(object sender, EventArgs e)
        {
            if(lbCharList.SelectedIndex > -1)
            {
                int selectedIndex = Convert.ToInt32(lbCharList.Items[lbCharList.SelectedIndex].ToString().Split(':')[0]);
                Globals.CharTypeList.Remove(selectedIndex);
                DataHandler.SaveCharType();
                RefreshList();
            }
        }


        private void lbCharList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbCharList.SelectedIndex > -1)
            {
                int selectedIndex = Convert.ToInt32(lbCharList.Items[lbCharList.SelectedIndex].ToString().Split(':')[0]);
                CharType newCharType = Globals.CharTypeList[selectedIndex];
                tbxName.Text = newCharType.Name;
                tbxSuffix.Text = newCharType.Suffix;
                nudIndex.Value = newCharType.Index;
                nudWidth.Value = newCharType.Size.X;
                nudHeight.Value = newCharType.Size.Y;
                tbxSrcImage.Text = newCharType.Source.srcImg;
                nudSrcX.Value = newCharType.Source.srcPos.X;
                nudSrcY.Value = newCharType.Source.srcPos.Y;
                nudBaseHP.Value = newCharType.baseHP;
                nudBaseMP.Value = newCharType.baseMP;
                nudBaseAtk.Value = newCharType.baseAtk;
                nudBaseDef.Value = newCharType.baseDef;
                nudBaseSAtk.Value = newCharType.baseSAtk;
                nudBaseSDef.Value = newCharType.baseSDef;
                nudBaseSpeed.Value = newCharType.baseSpeed;
                nudLUHP.Value = newCharType.luHP;
                nudLUMP.Value = newCharType.luMP;
                nudLUAtk.Value = newCharType.luAtk;
                nudLUDef.Value = newCharType.luDef;
                nudLUSAtk.Value = newCharType.luSAtk;
                nudLUSDef.Value = newCharType.luSDef;
                nudLUSpeed.Value = newCharType.luSpeed;
                nudRange.Value = newCharType.Range;
                cbxNPC.Checked = newCharType.IsNPC;
                cbxMerchant.Checked = newCharType.IsMerchant;
            }
        }

        private int GetUnsedIndex()
        {
            int newIndex = -1;
            bool unused = false;

            while(unused == false)
            {
                newIndex += 1;
                unused = true;
                if (Globals.CharTypeList.ContainsKey(newIndex) == true)
                {
                    unused = false;
                }
            }

            return newIndex;
        }

    }
}
