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
    public partial class MapChar : Form
    {
        public MapChar()
        {
            InitializeComponent();
        }

        private void MapChar_Load(object sender, EventArgs e)
        {
            //Có 1 listbox hiện tất cả các Character có trong Map (Game.World.MapChar)
            //--> tạo hàm RefreshList
            //Tạo các ô: tọa độ khi load map của char đó, hướng ban đầu, teams, level,...
            //Tạo nút Edit, Delete
        }
    }
}
