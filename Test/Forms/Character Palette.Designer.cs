namespace Test.Forms
{
    partial class Character_Palette
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
            this.lbCharList = new System.Windows.Forms.ListBox();
            this.tbxName = new System.Windows.Forms.TextBox();
            this.nudIndex = new System.Windows.Forms.NumericUpDown();
            this.tbxSuffix = new System.Windows.Forms.TextBox();
            this.nudHeight = new System.Windows.Forms.NumericUpDown();
            this.nudWidth = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbxSrcImage = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.nudSrcX = new System.Windows.Forms.NumericUpDown();
            this.nudSrcY = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.nudBaseHP = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.nudLUHP = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.nudLUMP = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.nudBaseMP = new System.Windows.Forms.NumericUpDown();
            this.nudLUAtk = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nudBaseAtk = new System.Windows.Forms.NumericUpDown();
            this.nudLUDef = new System.Windows.Forms.NumericUpDown();
            this.label11 = new System.Windows.Forms.Label();
            this.nudBaseDef = new System.Windows.Forms.NumericUpDown();
            this.nudLUSAtk = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.nudBaseSAtk = new System.Windows.Forms.NumericUpDown();
            this.nudLUSDef = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.nudBaseSDef = new System.Windows.Forms.NumericUpDown();
            this.nudLUSpeed = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.nudBaseSpeed = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.nudRange = new System.Windows.Forms.NumericUpDown();
            this.cbxMerchant = new System.Windows.Forms.CheckBox();
            this.cbxNPC = new System.Windows.Forms.CheckBox();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnDel = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.nudWaitMin = new System.Windows.Forms.NumericUpDown();
            this.nudWaitMax = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrcX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrcY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUAtk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseAtk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSAtk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSAtk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSDef)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitMax)).BeginInit();
            this.SuspendLayout();
            // 
            // lbCharList
            // 
            this.lbCharList.FormattingEnabled = true;
            this.lbCharList.Location = new System.Drawing.Point(211, 12);
            this.lbCharList.Name = "lbCharList";
            this.lbCharList.Size = new System.Drawing.Size(247, 446);
            this.lbCharList.TabIndex = 0;
            this.lbCharList.SelectedIndexChanged += new System.EventHandler(this.lbCharList_SelectedIndexChanged);
            // 
            // tbxName
            // 
            this.tbxName.Location = new System.Drawing.Point(12, 12);
            this.tbxName.Name = "tbxName";
            this.tbxName.Size = new System.Drawing.Size(193, 20);
            this.tbxName.TabIndex = 1;
            // 
            // nudIndex
            // 
            this.nudIndex.Location = new System.Drawing.Point(139, 38);
            this.nudIndex.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudIndex.Name = "nudIndex";
            this.nudIndex.ReadOnly = true;
            this.nudIndex.Size = new System.Drawing.Size(66, 20);
            this.nudIndex.TabIndex = 2;
            // 
            // tbxSuffix
            // 
            this.tbxSuffix.Location = new System.Drawing.Point(12, 38);
            this.tbxSuffix.Name = "tbxSuffix";
            this.tbxSuffix.Size = new System.Drawing.Size(121, 20);
            this.tbxSuffix.TabIndex = 3;
            // 
            // nudHeight
            // 
            this.nudHeight.Location = new System.Drawing.Point(139, 64);
            this.nudHeight.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudHeight.Name = "nudHeight";
            this.nudHeight.Size = new System.Drawing.Size(66, 20);
            this.nudHeight.TabIndex = 4;
            // 
            // nudWidth
            // 
            this.nudWidth.Location = new System.Drawing.Point(42, 64);
            this.nudWidth.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudWidth.Name = "nudWidth";
            this.nudWidth.Size = new System.Drawing.Size(66, 20);
            this.nudWidth.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 19);
            this.label1.TabIndex = 6;
            this.label1.Text = "W";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(114, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(20, 19);
            this.label2.TabIndex = 7;
            this.label2.Text = "H";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 19);
            this.label3.TabIndex = 8;
            this.label3.Text = "Source";
            // 
            // tbxSrcImage
            // 
            this.tbxSrcImage.Location = new System.Drawing.Point(70, 90);
            this.tbxSrcImage.Name = "tbxSrcImage";
            this.tbxSrcImage.Size = new System.Drawing.Size(135, 20);
            this.tbxSrcImage.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(114, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 19);
            this.label4.TabIndex = 13;
            this.label4.Text = "Y";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 117);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 19);
            this.label5.TabIndex = 12;
            this.label5.Text = "X";
            // 
            // nudSrcX
            // 
            this.nudSrcX.Location = new System.Drawing.Point(42, 116);
            this.nudSrcX.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudSrcX.Name = "nudSrcX";
            this.nudSrcX.Size = new System.Drawing.Size(66, 20);
            this.nudSrcX.TabIndex = 11;
            // 
            // nudSrcY
            // 
            this.nudSrcY.Location = new System.Drawing.Point(139, 116);
            this.nudSrcY.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudSrcY.Name = "nudSrcY";
            this.nudSrcY.Size = new System.Drawing.Size(66, 20);
            this.nudSrcY.TabIndex = 10;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 19);
            this.label6.TabIndex = 14;
            this.label6.Text = "Base Stats";
            // 
            // nudBaseHP
            // 
            this.nudBaseHP.Location = new System.Drawing.Point(46, 161);
            this.nudBaseHP.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseHP.Name = "nudBaseHP";
            this.nudBaseHP.Size = new System.Drawing.Size(66, 20);
            this.nudBaseHP.TabIndex = 15;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(7, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 19);
            this.label7.TabIndex = 16;
            this.label7.Text = "HP";
            // 
            // nudLUHP
            // 
            this.nudLUHP.Location = new System.Drawing.Point(118, 161);
            this.nudLUHP.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUHP.Name = "nudLUHP";
            this.nudLUHP.Size = new System.Drawing.Size(87, 20);
            this.nudLUHP.TabIndex = 17;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(106, 139);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 19);
            this.label8.TabIndex = 18;
            this.label8.Text = "Level Up Stats";
            // 
            // nudLUMP
            // 
            this.nudLUMP.Location = new System.Drawing.Point(118, 187);
            this.nudLUMP.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUMP.Name = "nudLUMP";
            this.nudLUMP.Size = new System.Drawing.Size(87, 20);
            this.nudLUMP.TabIndex = 21;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(7, 188);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 19);
            this.label9.TabIndex = 20;
            this.label9.Text = "MP";
            // 
            // nudBaseMP
            // 
            this.nudBaseMP.Location = new System.Drawing.Point(46, 187);
            this.nudBaseMP.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseMP.Name = "nudBaseMP";
            this.nudBaseMP.Size = new System.Drawing.Size(66, 20);
            this.nudBaseMP.TabIndex = 19;
            // 
            // nudLUAtk
            // 
            this.nudLUAtk.Location = new System.Drawing.Point(118, 213);
            this.nudLUAtk.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUAtk.Name = "nudLUAtk";
            this.nudLUAtk.Size = new System.Drawing.Size(87, 20);
            this.nudLUAtk.TabIndex = 24;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(7, 214);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 19);
            this.label10.TabIndex = 23;
            this.label10.Text = "Atk";
            // 
            // nudBaseAtk
            // 
            this.nudBaseAtk.Location = new System.Drawing.Point(46, 213);
            this.nudBaseAtk.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseAtk.Name = "nudBaseAtk";
            this.nudBaseAtk.Size = new System.Drawing.Size(66, 20);
            this.nudBaseAtk.TabIndex = 22;
            // 
            // nudLUDef
            // 
            this.nudLUDef.Location = new System.Drawing.Point(118, 239);
            this.nudLUDef.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUDef.Name = "nudLUDef";
            this.nudLUDef.Size = new System.Drawing.Size(87, 20);
            this.nudLUDef.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(7, 240);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 19);
            this.label11.TabIndex = 26;
            this.label11.Text = "Def";
            // 
            // nudBaseDef
            // 
            this.nudBaseDef.Location = new System.Drawing.Point(46, 239);
            this.nudBaseDef.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseDef.Name = "nudBaseDef";
            this.nudBaseDef.Size = new System.Drawing.Size(66, 20);
            this.nudBaseDef.TabIndex = 25;
            // 
            // nudLUSAtk
            // 
            this.nudLUSAtk.Location = new System.Drawing.Point(118, 265);
            this.nudLUSAtk.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUSAtk.Name = "nudLUSAtk";
            this.nudLUSAtk.Size = new System.Drawing.Size(87, 20);
            this.nudLUSAtk.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(-1, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(41, 19);
            this.label12.TabIndex = 29;
            this.label12.Text = "SAtk";
            // 
            // nudBaseSAtk
            // 
            this.nudBaseSAtk.Location = new System.Drawing.Point(46, 265);
            this.nudBaseSAtk.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseSAtk.Name = "nudBaseSAtk";
            this.nudBaseSAtk.Size = new System.Drawing.Size(66, 20);
            this.nudBaseSAtk.TabIndex = 28;
            // 
            // nudLUSDef
            // 
            this.nudLUSDef.Location = new System.Drawing.Point(118, 291);
            this.nudLUSDef.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUSDef.Name = "nudLUSDef";
            this.nudLUSDef.Size = new System.Drawing.Size(87, 20);
            this.nudLUSDef.TabIndex = 33;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(0, 292);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(40, 19);
            this.label13.TabIndex = 32;
            this.label13.Text = "SDef";
            // 
            // nudBaseSDef
            // 
            this.nudBaseSDef.Location = new System.Drawing.Point(46, 291);
            this.nudBaseSDef.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseSDef.Name = "nudBaseSDef";
            this.nudBaseSDef.Size = new System.Drawing.Size(66, 20);
            this.nudBaseSDef.TabIndex = 31;
            // 
            // nudLUSpeed
            // 
            this.nudLUSpeed.Location = new System.Drawing.Point(118, 317);
            this.nudLUSpeed.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudLUSpeed.Name = "nudLUSpeed";
            this.nudLUSpeed.Size = new System.Drawing.Size(87, 20);
            this.nudLUSpeed.TabIndex = 36;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(7, 318);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(34, 19);
            this.label14.TabIndex = 35;
            this.label14.Text = "Spd";
            // 
            // nudBaseSpeed
            // 
            this.nudBaseSpeed.Location = new System.Drawing.Point(46, 317);
            this.nudBaseSpeed.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudBaseSpeed.Name = "nudBaseSpeed";
            this.nudBaseSpeed.Size = new System.Drawing.Size(66, 20);
            this.nudBaseSpeed.TabIndex = 34;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(8, 345);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(47, 19);
            this.label15.TabIndex = 38;
            this.label15.Text = "Range";
            // 
            // nudRange
            // 
            this.nudRange.Location = new System.Drawing.Point(61, 344);
            this.nudRange.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudRange.Name = "nudRange";
            this.nudRange.Size = new System.Drawing.Size(144, 20);
            this.nudRange.TabIndex = 37;
            // 
            // cbxMerchant
            // 
            this.cbxMerchant.AutoSize = true;
            this.cbxMerchant.Location = new System.Drawing.Point(12, 400);
            this.cbxMerchant.Name = "cbxMerchant";
            this.cbxMerchant.Size = new System.Drawing.Size(71, 17);
            this.cbxMerchant.TabIndex = 39;
            this.cbxMerchant.Text = "Merchant";
            this.cbxMerchant.UseVisualStyleBackColor = true;
            // 
            // cbxNPC
            // 
            this.cbxNPC.AutoSize = true;
            this.cbxNPC.Location = new System.Drawing.Point(89, 400);
            this.cbxNPC.Name = "cbxNPC";
            this.cbxNPC.Size = new System.Drawing.Size(59, 17);
            this.cbxNPC.TabIndex = 40;
            this.cbxNPC.Text = "Is NPC";
            this.cbxNPC.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            this.btnNew.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNew.Location = new System.Drawing.Point(70, 423);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(63, 36);
            this.btnNew.TabIndex = 41;
            this.btnNew.Text = "NEW";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEdit.Location = new System.Drawing.Point(4, 423);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(60, 36);
            this.btnEdit.TabIndex = 42;
            this.btnEdit.Text = "EDIT";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnDel
            // 
            this.btnDel.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDel.Location = new System.Drawing.Point(139, 423);
            this.btnDel.Name = "btnDel";
            this.btnDel.Size = new System.Drawing.Size(66, 36);
            this.btnDel.TabIndex = 43;
            this.btnDel.Text = "DEL";
            this.btnDel.UseVisualStyleBackColor = true;
            this.btnDel.Click += new System.EventHandler(this.btnDel_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(8, 371);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(37, 19);
            this.label16.TabIndex = 45;
            this.label16.Text = "Wait";
            // 
            // nudWaitMin
            // 
            this.nudWaitMin.Location = new System.Drawing.Point(51, 370);
            this.nudWaitMin.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudWaitMin.Name = "nudWaitMin";
            this.nudWaitMin.Size = new System.Drawing.Size(64, 20);
            this.nudWaitMin.TabIndex = 44;
            // 
            // nudWaitMax
            // 
            this.nudWaitMax.Location = new System.Drawing.Point(140, 370);
            this.nudWaitMax.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudWaitMax.Name = "nudWaitMax";
            this.nudWaitMax.Size = new System.Drawing.Size(65, 20);
            this.nudWaitMax.TabIndex = 46;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(116, 371);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(18, 19);
            this.label17.TabIndex = 47;
            this.label17.Text = "~";
            // 
            // Character_Palette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 466);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.nudWaitMax);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.nudWaitMin);
            this.Controls.Add(this.btnDel);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.cbxNPC);
            this.Controls.Add(this.cbxMerchant);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.nudRange);
            this.Controls.Add(this.nudLUSpeed);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.nudBaseSpeed);
            this.Controls.Add(this.nudLUSDef);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.nudBaseSDef);
            this.Controls.Add(this.nudLUSAtk);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.nudBaseSAtk);
            this.Controls.Add(this.nudLUDef);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.nudBaseDef);
            this.Controls.Add(this.nudLUAtk);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.nudBaseAtk);
            this.Controls.Add(this.nudLUMP);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.nudBaseMP);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.nudLUHP);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nudBaseHP);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.nudSrcX);
            this.Controls.Add(this.nudSrcY);
            this.Controls.Add(this.tbxSrcImage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nudWidth);
            this.Controls.Add(this.nudHeight);
            this.Controls.Add(this.tbxSuffix);
            this.Controls.Add(this.nudIndex);
            this.Controls.Add(this.tbxName);
            this.Controls.Add(this.lbCharList);
            this.Name = "Character_Palette";
            this.Text = "Character_Palette";
            this.Load += new System.EventHandler(this.Character_Palette_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudIndex)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrcX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSrcY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUAtk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseAtk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSAtk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSAtk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSDef)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLUSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBaseSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudWaitMax)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ListBox lbCharList;
        private System.Windows.Forms.TextBox tbxName;
        private System.Windows.Forms.NumericUpDown nudIndex;
        private System.Windows.Forms.TextBox tbxSuffix;
        private System.Windows.Forms.NumericUpDown nudHeight;
        private System.Windows.Forms.NumericUpDown nudWidth;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbxSrcImage;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown nudSrcX;
        private System.Windows.Forms.NumericUpDown nudSrcY;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudBaseHP;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown nudLUHP;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudLUMP;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudBaseMP;
        private System.Windows.Forms.NumericUpDown nudLUAtk;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown nudBaseAtk;
        private System.Windows.Forms.NumericUpDown nudLUDef;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown nudBaseDef;
        private System.Windows.Forms.NumericUpDown nudLUSAtk;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.NumericUpDown nudBaseSAtk;
        private System.Windows.Forms.NumericUpDown nudLUSDef;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown nudBaseSDef;
        private System.Windows.Forms.NumericUpDown nudLUSpeed;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown nudBaseSpeed;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudRange;
        private System.Windows.Forms.CheckBox cbxMerchant;
        private System.Windows.Forms.CheckBox cbxNPC;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnDel;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown nudWaitMin;
        private System.Windows.Forms.NumericUpDown nudWaitMax;
        private System.Windows.Forms.Label label17;
    }
}