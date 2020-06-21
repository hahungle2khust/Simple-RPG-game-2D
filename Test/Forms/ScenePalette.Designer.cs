namespace Test.Forms
{
    partial class ScenePalette
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.lbFrames = new System.Windows.Forms.ListBox();
            this.tbxBGFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.tbxScript = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxText = new System.Windows.Forms.TextBox();
            this.lbCharacters = new System.Windows.Forms.ListBox();
            this.tbxCharFile = new System.Windows.Forms.TextBox();
            this.nudX = new System.Windows.Forms.NumericUpDown();
            this.nudY = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.btnAddChar = new System.Windows.Forms.Button();
            this.btnDelChar = new System.Windows.Forms.Button();
            this.btnDelFrame = new System.Windows.Forms.Button();
            this.btnAddFrame = new System.Windows.Forms.Button();
            this.btnLoadScene = new System.Windows.Forms.Button();
            this.btnSaveScene = new System.Windows.Forms.Button();
            this.ofdLoad = new System.Windows.Forms.OpenFileDialog();
            this.sfdSave = new System.Windows.Forms.SaveFileDialog();
            this.tbxSounds = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // lbFrames
            // 
            this.lbFrames.FormattingEnabled = true;
            this.lbFrames.Location = new System.Drawing.Point(604, 12);
            this.lbFrames.Name = "lbFrames";
            this.lbFrames.Size = new System.Drawing.Size(216, 407);
            this.lbFrames.TabIndex = 0;
            this.lbFrames.SelectedIndexChanged += new System.EventHandler(this.lbFrames_SelectedIndexChanged);
            // 
            // tbxBGFile
            // 
            this.tbxBGFile.Location = new System.Drawing.Point(455, 12);
            this.tbxBGFile.Name = "tbxBGFile";
            this.tbxBGFile.Size = new System.Drawing.Size(143, 20);
            this.tbxBGFile.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(408, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "BG File";
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(455, 144);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(143, 20);
            this.tbxName.TabIndex = 3;
            // 
            // tbxScript
            // 
            this.tbxScript.Location = new System.Drawing.Point(455, 38);
            this.tbxScript.Multiline = true;
            this.tbxScript.Name = "tbxScript";
            this.tbxScript.Size = new System.Drawing.Size(143, 47);
            this.tbxScript.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(408, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Script";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(408, 144);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(35, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Name";
            // 
            // tbxText
            // 
            this.tbxText.Location = new System.Drawing.Point(455, 170);
            this.tbxText.Multiline = true;
            this.tbxText.Name = "tbxText";
            this.tbxText.Size = new System.Drawing.Size(143, 209);
            this.tbxText.TabIndex = 7;
            // 
            // lbCharacters
            // 
            this.lbCharacters.FormattingEnabled = true;
            this.lbCharacters.Location = new System.Drawing.Point(144, 12);
            this.lbCharacters.Name = "lbCharacters";
            this.lbCharacters.Size = new System.Drawing.Size(216, 121);
            this.lbCharacters.TabIndex = 8;
            // 
            // tbxCharFile
            // 
            this.tbxCharFile.Location = new System.Drawing.Point(12, 12);
            this.tbxCharFile.Name = "tbxCharFile";
            this.tbxCharFile.Size = new System.Drawing.Size(126, 20);
            this.tbxCharFile.TabIndex = 9;
            // 
            // nudX
            // 
            this.nudX.Location = new System.Drawing.Point(12, 55);
            this.nudX.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudX.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudX.Name = "nudX";
            this.nudX.Size = new System.Drawing.Size(55, 20);
            this.nudX.TabIndex = 10;
            // 
            // nudY
            // 
            this.nudY.Location = new System.Drawing.Point(83, 55);
            this.nudY.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudY.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudY.Name = "nudY";
            this.nudY.Size = new System.Drawing.Size(55, 20);
            this.nudY.TabIndex = 11;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(12, 94);
            this.nudWidth.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudWidth.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(55, 20);
            this.nudWidth.TabIndex = 12;
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(83, 94);
            this.nudHeight.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudHeight.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(55, 20);
            this.nudHeight.TabIndex = 13;
            // 
            // btnAddChar
            // 
            this.btnAddChar.Location = new System.Drawing.Point(12, 124);
            this.btnAddChar.Name = "btnAddChar";
            this.btnAddChar.Size = new System.Drawing.Size(55, 35);
            this.btnAddChar.TabIndex = 14;
            this.btnAddChar.Text = "ADD";
            this.btnAddChar.UseVisualStyleBackColor = true;
            this.btnAddChar.Click += new System.EventHandler(this.btnAddChar_Click);
            // 
            // btnDelChar
            // 
            this.btnDelChar.Location = new System.Drawing.Point(83, 124);
            this.btnDelChar.Name = "btnDelChar";
            this.btnDelChar.Size = new System.Drawing.Size(55, 35);
            this.btnDelChar.TabIndex = 15;
            this.btnDelChar.Text = "DEL";
            this.btnDelChar.UseVisualStyleBackColor = true;
            this.btnDelChar.Click += new System.EventHandler(this.btnDelChar_Click);
            // 
            // btnDelFrame
            // 
            this.btnDelFrame.Location = new System.Drawing.Point(394, 344);
            this.btnDelFrame.Name = "btnDelFrame";
            this.btnDelFrame.Size = new System.Drawing.Size(55, 35);
            this.btnDelFrame.TabIndex = 17;
            this.btnDelFrame.Text = "DEL";
            this.btnDelFrame.UseVisualStyleBackColor = true;
            this.btnDelFrame.Click += new System.EventHandler(this.btnDelFrame_Click);
            // 
            // btnAddFrame
            // 
            this.btnAddFrame.Location = new System.Drawing.Point(421, 385);
            this.btnAddFrame.Name = "btnAddFrame";
            this.btnAddFrame.Size = new System.Drawing.Size(55, 35);
            this.btnAddFrame.TabIndex = 16;
            this.btnAddFrame.Text = "ADD";
            this.btnAddFrame.UseVisualStyleBackColor = true;
            this.btnAddFrame.Click += new System.EventHandler(this.btnAddFrame_Click);
            // 
            // btnLoadScene
            // 
            this.btnLoadScene.Location = new System.Drawing.Point(144, 219);
            this.btnLoadScene.Name = "btnLoadScene";
            this.btnLoadScene.Size = new System.Drawing.Size(125, 52);
            this.btnLoadScene.TabIndex = 18;
            this.btnLoadScene.Text = "LOAD SCENE";
            this.btnLoadScene.UseVisualStyleBackColor = true;
            this.btnLoadScene.Click += new System.EventHandler(this.btnLoadScene_Click);
            // 
            // btnSaveScene
            // 
            this.btnSaveScene.Location = new System.Drawing.Point(144, 277);
            this.btnSaveScene.Name = "btnSaveScene";
            this.btnSaveScene.Size = new System.Drawing.Size(125, 52);
            this.btnSaveScene.TabIndex = 19;
            this.btnSaveScene.Text = "SAVE SCENE";
            this.btnSaveScene.UseVisualStyleBackColor = true;
            this.btnSaveScene.Click += new System.EventHandler(this.btnSaveScene_Click);
            // 
            // ofdLoad
            // 
            this.ofdLoad.FileName = "openFileDialog1";
            // 
            // tbxSounds
            // 
            this.tbxSounds.Location = new System.Drawing.Point(455, 91);
            this.tbxSounds.Multiline = true;
            this.tbxSounds.Name = "tbxSounds";
            this.tbxSounds.Size = new System.Drawing.Size(143, 47);
            this.tbxSounds.TabIndex = 20;
            this.toolTip1.SetToolTip(this.tbxSounds, "Nếu muốn chạy nhiều file, dùng dấu , giữa tên các file sound.\r\n**Không dùng dấu s" +
        "pace sau dấu phẩy, trừ khi có trong tên file.\r\nVí dụ: Select1,Select2");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(408, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Text";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(408, 94);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 13);
            this.label5.TabIndex = 22;
            this.label5.Text = "Sounds";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(482, 385);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(55, 35);
            this.button1.TabIndex = 23;
            this.button1.Text = "INS";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 38);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(14, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(103, 39);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(14, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Y";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(26, 78);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Width";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(94, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(38, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Height";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(543, 385);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(55, 35);
            this.button2.TabIndex = 28;
            this.button2.Text = "EDIT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ScenePalette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(832, 438);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.tbxSounds);
            this.Controls.Add(this.btnSaveScene);
            this.Controls.Add(this.btnLoadScene);
            this.Controls.Add(this.btnDelFrame);
            this.Controls.Add(this.btnAddFrame);
            this.Controls.Add(this.btnDelChar);
            this.Controls.Add(this.btnAddChar);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.nudY);
            this.Controls.Add(this.nudX);
            this.Controls.Add(this.tbxCharFile);
            this.Controls.Add(this.lbCharacters);
            this.Controls.Add(this.tbxText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxScript);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbxBGFile);
            this.Controls.Add(this.lbFrames);
            this.Name = "ScenePalette";
            this.Text = "ScenePalette";
            this.Load += new System.EventHandler(this.ScenePalette_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbFrames;
        private System.Windows.Forms.TextBox tbxBGFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.TextBox tbxScript;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxText;
        private System.Windows.Forms.ListBox lbCharacters;
        private System.Windows.Forms.TextBox tbxCharFile;
        private System.Windows.Forms.NumericUpDown nudX;
        private System.Windows.Forms.NumericUpDown nudY;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.Button btnAddChar;
        private System.Windows.Forms.Button btnDelChar;
        private System.Windows.Forms.Button btnDelFrame;
        private System.Windows.Forms.Button btnAddFrame;
        private System.Windows.Forms.Button btnLoadScene;
        private System.Windows.Forms.Button btnSaveScene;
        private System.Windows.Forms.OpenFileDialog ofdLoad;
        private System.Windows.Forms.SaveFileDialog sfdSave;
        private System.Windows.Forms.TextBox tbxSounds;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button2;
    }
}