namespace Test.Forms
{
    partial class Tile_Palette
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
            this.pbSource = new System.Windows.Forms.PictureBox();
            this.cbxSource = new System.Windows.Forms.ComboBox();
            this.cbxLayer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbBlocked = new System.Windows.Forms.CheckBox();
            this.cbStepTrigger = new System.Windows.Forms.CheckBox();
            this.cbTouchTrigger = new System.Windows.Forms.CheckBox();
            this.tbxTrigAct = new System.Windows.Forms.TextBox();
            this.tbxTrigValues = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSource
            // 
            this.pbSource.Location = new System.Drawing.Point(162, 12);
            this.pbSource.Name = "pbSource";
            this.pbSource.Size = new System.Drawing.Size(418, 369);
            this.pbSource.TabIndex = 0;
            this.pbSource.TabStop = false;
            this.pbSource.Click += new System.EventHandler(this.pbSource_Click);
            this.pbSource.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pbSource_MouseClick);
            // 
            // cbxSource
            // 
            this.cbxSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxSource.FormattingEnabled = true;
            this.cbxSource.Location = new System.Drawing.Point(30, 12);
            this.cbxSource.Name = "cbxSource";
            this.cbxSource.Size = new System.Drawing.Size(126, 21);
            this.cbxSource.TabIndex = 1;
            this.cbxSource.SelectedIndexChanged += new System.EventHandler(this.cbxSource_SelectedIndexChanged);
            // 
            // cbxLayer
            // 
            this.cbxLayer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLayer.FormattingEnabled = true;
            this.cbxLayer.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3"});
            this.cbxLayer.Location = new System.Drawing.Point(61, 66);
            this.cbxLayer.Name = "cbxLayer";
            this.cbxLayer.Size = new System.Drawing.Size(95, 21);
            this.cbxLayer.TabIndex = 2;
            this.cbxLayer.SelectedIndexChanged += new System.EventHandler(this.cbxLayer_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Layer";
            // 
            // cbBlocked
            // 
            this.cbBlocked.AutoSize = true;
            this.cbBlocked.Location = new System.Drawing.Point(12, 112);
            this.cbBlocked.Name = "cbBlocked";
            this.cbBlocked.Size = new System.Drawing.Size(65, 17);
            this.cbBlocked.TabIndex = 4;
            this.cbBlocked.Text = "Blocked";
            this.cbBlocked.UseVisualStyleBackColor = true;
            // 
            // cbStepTrigger
            // 
            this.cbStepTrigger.AutoSize = true;
            this.cbStepTrigger.Location = new System.Drawing.Point(12, 135);
            this.cbStepTrigger.Name = "cbStepTrigger";
            this.cbStepTrigger.Size = new System.Drawing.Size(84, 17);
            this.cbStepTrigger.TabIndex = 5;
            this.cbStepTrigger.Text = "Step Trigger";
            this.cbStepTrigger.UseVisualStyleBackColor = true;
            // 
            // cbTouchTrigger
            // 
            this.cbTouchTrigger.AutoSize = true;
            this.cbTouchTrigger.Location = new System.Drawing.Point(12, 158);
            this.cbTouchTrigger.Name = "cbTouchTrigger";
            this.cbTouchTrigger.Size = new System.Drawing.Size(93, 17);
            this.cbTouchTrigger.TabIndex = 6;
            this.cbTouchTrigger.Text = "Touch Trigger";
            this.cbTouchTrigger.UseVisualStyleBackColor = true;
            // 
            // tbxTrigAct
            // 
            this.tbxTrigAct.Location = new System.Drawing.Point(12, 255);
            this.tbxTrigAct.Name = "tbxTrigAct";
            this.tbxTrigAct.Size = new System.Drawing.Size(144, 20);
            this.tbxTrigAct.TabIndex = 7;
            // 
            // tbxTrigValues
            // 
            this.tbxTrigValues.Location = new System.Drawing.Point(12, 281);
            this.tbxTrigValues.Multiline = true;
            this.tbxTrigValues.Name = "tbxTrigValues";
            this.tbxTrigValues.Size = new System.Drawing.Size(144, 100);
            this.tbxTrigValues.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 239);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Triggers";
            // 
            // Tile_Palette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 393);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbxTrigValues);
            this.Controls.Add(this.tbxTrigAct);
            this.Controls.Add(this.cbTouchTrigger);
            this.Controls.Add(this.cbStepTrigger);
            this.Controls.Add(this.cbBlocked);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbxLayer);
            this.Controls.Add(this.cbxSource);
            this.Controls.Add(this.pbSource);
            this.Name = "Tile_Palette";
            this.Text = "Tile_Palette";
            this.Load += new System.EventHandler(this.Tile_Palette_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSource;
        private System.Windows.Forms.ComboBox cbxSource;
        private System.Windows.Forms.ComboBox cbxLayer;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.CheckBox cbBlocked;
        public System.Windows.Forms.CheckBox cbStepTrigger;
        public System.Windows.Forms.CheckBox cbTouchTrigger;
        public System.Windows.Forms.TextBox tbxTrigAct;
        public System.Windows.Forms.TextBox tbxTrigValues;
        private System.Windows.Forms.Label label2;
    }
}