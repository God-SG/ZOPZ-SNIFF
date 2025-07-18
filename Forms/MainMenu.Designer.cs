using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;
using System.Drawing.Text;
using ZOPZ_SNIFF.Properties;

namespace ZOPZ_SNIFF.Forms
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Panel panel1;
        private Panel panel2;
        private Label label1;
        private Guna2Button guna2Button1;
        private Guna2ComboBox AdapterCB;
        private Guna2DragControl guna2DragControl1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Panel panel3;
        private Guna2ControlBox guna2ControlBox4;
        private Guna2ControlBox guna2ControlBox5;
        private Guna2ControlBox guna2ControlBox6;
        private Guna2CircleButton guna2CircleButton1;
        private Guna2DataGridView FilteredGamesDGV;
        private Guna2TabControl guna2TabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private Guna2CircleButton guna2CircleButton3;
        private Guna2CircleButton guna2CircleButton2;
        private TabPage tabPage3;
        private TabPage tabPage4;
        private Label label3;
        private Guna2DataGridView OtherInfoDGV;
        private Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private ToolStripMenuItem copyTooClipboardToolStripMenuItem;
        private ToolStripMenuItem clearAllToolStripMenuItem;
        private Guna2VScrollBar guna2vScrollBar1;
        private Guna2VScrollBar guna2vScrollBar2;
        private Guna2DataGridView guna2DataGridView4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private Guna2Button guna2Button4;
        private Guna2Button guna2Button3;
        private Guna2TextBox guna2TextBox2;
        private Guna2Button guna2Button2;
        private Guna2Button LoginBTN;
        private Guna2TextBox guna2TextBox1;
        private Guna2HtmlToolTip guna2HtmlToolTip1;
        private ToolStripMenuItem copyEntireRowToolStripMenuItem;
        private ToolStripMenuItem clearSelectedRowToolStripMenuItem;
        private ToolStripMenuItem pingCellToolStripMenuItem;
        private ToolStripMenuItem packetAnalyzerToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private Guna2ContextMenuStrip guna2ContextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem toolStripMenuItem2;
        private ToolStripMenuItem toolStripMenuItem3;
        private ToolStripMenuItem toolStripMenuItem4;
        private ToolStripMenuItem toolStripMenuItem5;
        private ToolStripMenuItem toolStripMenuItem6;

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
            components = new Container();
            CustomizableEdges customizableEdges38 = new CustomizableEdges();
            CustomizableEdges customizableEdges39 = new CustomizableEdges();
            CustomizableEdges customizableEdges40 = new CustomizableEdges();
            CustomizableEdges customizableEdges41 = new CustomizableEdges();
            CustomizableEdges customizableEdges42 = new CustomizableEdges();
            CustomizableEdges customizableEdges43 = new CustomizableEdges();
            CustomizableEdges customizableEdges44 = new CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainMenu));
            CustomizableEdges customizableEdges45 = new CustomizableEdges();
            CustomizableEdges customizableEdges46 = new CustomizableEdges();
            CustomizableEdges customizableEdges47 = new CustomizableEdges();
            CustomizableEdges customizableEdges48 = new CustomizableEdges();
            CustomizableEdges customizableEdges49 = new CustomizableEdges();
            CustomizableEdges customizableEdges50 = new CustomizableEdges();
            CustomizableEdges customizableEdges51 = new CustomizableEdges();
            CustomizableEdges customizableEdges52 = new CustomizableEdges();
            CustomizableEdges customizableEdges53 = new CustomizableEdges();
            CustomizableEdges customizableEdges54 = new CustomizableEdges();
            CustomizableEdges customizableEdges55 = new CustomizableEdges();
            CustomizableEdges customizableEdges56 = new CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle13 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle14 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle15 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle16 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            CustomizableEdges customizableEdges57 = new CustomizableEdges();
            CustomizableEdges customizableEdges58 = new CustomizableEdges();
            CustomizableEdges customizableEdges59 = new CustomizableEdges();
            CustomizableEdges customizableEdges60 = new CustomizableEdges();
            CustomizableEdges customizableEdges61 = new CustomizableEdges();
            CustomizableEdges customizableEdges62 = new CustomizableEdges();
            CustomizableEdges customizableEdges63 = new CustomizableEdges();
            CustomizableEdges customizableEdges64 = new CustomizableEdges();
            CustomizableEdges customizableEdges65 = new CustomizableEdges();
            CustomizableEdges customizableEdges66 = new CustomizableEdges();
            CustomizableEdges customizableEdges67 = new CustomizableEdges();
            CustomizableEdges customizableEdges68 = new CustomizableEdges();
            CustomizableEdges customizableEdges69 = new CustomizableEdges();
            CustomizableEdges customizableEdges70 = new CustomizableEdges();
            CustomizableEdges customizableEdges71 = new CustomizableEdges();
            CustomizableEdges customizableEdges72 = new CustomizableEdges();
            CustomizableEdges customizableEdges73 = new CustomizableEdges();
            CustomizableEdges customizableEdges74 = new CustomizableEdges();
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            guna2Button1 = new Guna2Button();
            AdapterCB = new Guna2ComboBox();
            panel2 = new Panel();
            label3 = new Label();
            guna2DragControl1 = new Guna2DragControl(components);
            panel3 = new Panel();
            guna2CircleButton3 = new Guna2CircleButton();
            guna2CircleButton2 = new Guna2CircleButton();
            guna2CircleButton1 = new Guna2CircleButton();
            guna2ControlBox4 = new Guna2ControlBox();
            guna2ControlBox5 = new Guna2ControlBox();
            guna2ControlBox6 = new Guna2ControlBox();
            FilteredGamesDGV = new Guna2DataGridView();
            guna2ContextMenuStrip1 = new Guna2ContextMenuStrip();
            copyTooClipboardToolStripMenuItem = new ToolStripMenuItem();
            copyEntireRowToolStripMenuItem = new ToolStripMenuItem();
            clearAllToolStripMenuItem = new ToolStripMenuItem();
            clearSelectedRowToolStripMenuItem = new ToolStripMenuItem();
            pingCellToolStripMenuItem = new ToolStripMenuItem();
            packetAnalyzerToolStripMenuItem = new ToolStripMenuItem();
            guna2TabControl1 = new Guna2TabControl();
            tabPage1 = new TabPage();
            guna2vScrollBar1 = new Guna2VScrollBar();
            tabPage2 = new TabPage();
            guna2vScrollBar2 = new Guna2VScrollBar();
            OtherInfoDGV = new Guna2DataGridView();
            guna2ContextMenuStrip2 = new Guna2ContextMenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            toolStripMenuItem2 = new ToolStripMenuItem();
            toolStripMenuItem3 = new ToolStripMenuItem();
            toolStripMenuItem4 = new ToolStripMenuItem();
            toolStripMenuItem5 = new ToolStripMenuItem();
            toolStripMenuItem6 = new ToolStripMenuItem();
            tabPage3 = new TabPage();
            guna2DataGridView4 = new Guna2DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
            tabPage4 = new TabPage();
            guna2Button3 = new Guna2Button();
            guna2TextBox2 = new Guna2TextBox();
            guna2Button6 = new Guna2Button();
            guna2TextBox3 = new Guna2TextBox();
            guna2Button5 = new Guna2Button();
            guna2Button2 = new Guna2Button();
            LoginBTN = new Guna2Button();
            guna2TextBox1 = new Guna2TextBox();
            guna2Button4 = new Guna2Button();
            guna2HtmlToolTip1 = new Guna2HtmlToolTip();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((ISupportInitialize)FilteredGamesDGV).BeginInit();
            guna2ContextMenuStrip1.SuspendLayout();
            guna2TabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            ((ISupportInitialize)OtherInfoDGV).BeginInit();
            guna2ContextMenuStrip2.SuspendLayout();
            tabPage3.SuspendLayout();
            ((ISupportInitialize)guna2DataGridView4).BeginInit();
            tabPage4.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(guna2ControlBox3);
            panel1.Controls.Add(guna2ControlBox2);
            panel1.Controls.Add(guna2ControlBox1);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1126, 34);
            panel1.TabIndex = 1;
            // 
            // guna2ControlBox3
            // 
            guna2ControlBox3.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox3.ControlBoxType = ControlBoxType.MinimizeBox;
            guna2ControlBox3.CustomizableEdges = customizableEdges38;
            guna2ControlBox3.Dock = DockStyle.Right;
            guna2ControlBox3.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.IconColor = Color.White;
            guna2ControlBox3.Location = new Point(991, 0);
            guna2ControlBox3.Name = "guna2ControlBox3";
            guna2ControlBox3.ShadowDecoration.CustomizableEdges = customizableEdges39;
            guna2ControlBox3.Size = new Size(45, 34);
            guna2ControlBox3.TabIndex = 5;
            guna2HtmlToolTip1.SetToolTip(guna2ControlBox3, "Minimize");
            // 
            // guna2ControlBox2
            // 
            guna2ControlBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox2.ControlBoxType = ControlBoxType.MaximizeBox;
            guna2ControlBox2.CustomizableEdges = customizableEdges40;
            guna2ControlBox2.Dock = DockStyle.Right;
            guna2ControlBox2.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.IconColor = Color.White;
            guna2ControlBox2.Location = new Point(1036, 0);
            guna2ControlBox2.Name = "guna2ControlBox2";
            guna2ControlBox2.ShadowDecoration.CustomizableEdges = customizableEdges41;
            guna2ControlBox2.Size = new Size(45, 34);
            guna2ControlBox2.TabIndex = 4;
            guna2HtmlToolTip1.SetToolTip(guna2ControlBox2, "Maximize");
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox1.CustomizableEdges = customizableEdges42;
            guna2ControlBox1.Dock = DockStyle.Right;
            guna2ControlBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(1081, 0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges43;
            guna2ControlBox1.Size = new Size(45, 34);
            guna2ControlBox1.TabIndex = 3;
            guna2HtmlToolTip1.SetToolTip(guna2ControlBox1, "Exit");
            guna2ControlBox1.Click += guna2ControlBox1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(4, 9);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF";
            // 
            // guna2Button1
            // 
            guna2Button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button1.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button1.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button1.CustomizableEdges = customizableEdges44;
            guna2Button1.DisabledState.BorderColor = Color.DarkGray;
            guna2Button1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button1.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button1.ForeColor = Color.White;
            guna2Button1.Image = (Image)resources.GetObject("guna2Button1.Image");
            guna2Button1.Location = new Point(840, 8);
            guna2Button1.Name = "guna2Button1";
            guna2Button1.ShadowDecoration.CustomizableEdges = customizableEdges45;
            guna2Button1.Size = new Size(139, 36);
            guna2Button1.TabIndex = 1;
            guna2Button1.Text = "Start Sniffing";
            guna2HtmlToolTip1.SetToolTip(guna2Button1, "Capture Traffic");
            guna2Button1.Click += guna2Button1_Click;
            // 
            // AdapterCB
            // 
            AdapterCB.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            AdapterCB.BackColor = Color.Transparent;
            AdapterCB.BorderColor = Color.FromArgb(25, 25, 25);
            AdapterCB.BorderThickness = 0;
            AdapterCB.CustomizableEdges = customizableEdges46;
            AdapterCB.DrawMode = DrawMode.OwnerDrawFixed;
            AdapterCB.DropDownStyle = ComboBoxStyle.DropDownList;
            AdapterCB.FillColor = Color.FromArgb(30, 30, 30);
            AdapterCB.FocusedColor = Color.FromArgb(25, 25, 25);
            AdapterCB.FocusedState.BorderColor = Color.FromArgb(25, 25, 25);
            AdapterCB.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            AdapterCB.ForeColor = Color.White;
            AdapterCB.ItemHeight = 30;
            AdapterCB.ItemsAppearance.BackColor = Color.FromArgb(30, 30, 30);
            AdapterCB.ItemsAppearance.ForeColor = Color.White;
            AdapterCB.ItemsAppearance.SelectedBackColor = Color.FromArgb(25, 25, 25);
            AdapterCB.ItemsAppearance.SelectedForeColor = Color.White;
            AdapterCB.Location = new Point(4, 8);
            AdapterCB.Name = "AdapterCB";
            AdapterCB.ShadowDecoration.CustomizableEdges = customizableEdges47;
            AdapterCB.Size = new Size(830, 36);
            AdapterCB.Style = TextBoxStyle.Material;
            AdapterCB.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.FromArgb(30, 30, 30);
            panel2.Controls.Add(label3);
            panel2.Dock = DockStyle.Bottom;
            panel2.Location = new Point(0, 557);
            panel2.Name = "panel2";
            panel2.Size = new Size(1126, 42);
            panel2.TabIndex = 3;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(4, 14);
            label3.Name = "label3";
            label3.Size = new Size(69, 15);
            label3.TabIndex = 4;
            label3.Text = "Status: Idle";
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = panel1;
            guna2DragControl1.TransparentWhileDrag = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(25, 25, 25);
            panel3.Controls.Add(guna2CircleButton3);
            panel3.Controls.Add(guna2CircleButton2);
            panel3.Controls.Add(guna2CircleButton1);
            panel3.Controls.Add(guna2ControlBox4);
            panel3.Controls.Add(guna2ControlBox5);
            panel3.Controls.Add(guna2ControlBox6);
            panel3.Controls.Add(guna2Button1);
            panel3.Controls.Add(AdapterCB);
            panel3.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 34);
            panel3.Name = "panel3";
            panel3.Size = new Size(1126, 54);
            panel3.TabIndex = 4;
            // 
            // guna2CircleButton3
            // 
            guna2CircleButton3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2CircleButton3.DisabledState.BorderColor = Color.DarkGray;
            guna2CircleButton3.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2CircleButton3.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2CircleButton3.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2CircleButton3.FillColor = Color.FromArgb(30, 30, 30);
            guna2CircleButton3.Font = new Font("Segoe UI", 9F);
            guna2CircleButton3.ForeColor = Color.White;
            guna2CircleButton3.Image = (Image)resources.GetObject("guna2CircleButton3.Image");
            guna2CircleButton3.Location = new Point(985, 8);
            guna2CircleButton3.Name = "guna2CircleButton3";
            guna2CircleButton3.ShadowDecoration.CustomizableEdges = customizableEdges48;
            guna2CircleButton3.ShadowDecoration.Mode = ShadowMode.Circle;
            guna2CircleButton3.Size = new Size(39, 36);
            guna2CircleButton3.TabIndex = 8;
            guna2HtmlToolTip1.SetToolTip(guna2CircleButton3, "Tool Box");
            guna2CircleButton3.Click += guna2CircleButton3_Click;
            // 
            // guna2CircleButton2
            // 
            guna2CircleButton2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2CircleButton2.DisabledState.BorderColor = Color.DarkGray;
            guna2CircleButton2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2CircleButton2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2CircleButton2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2CircleButton2.FillColor = Color.FromArgb(30, 30, 30);
            guna2CircleButton2.Font = new Font("Segoe UI", 9F);
            guna2CircleButton2.ForeColor = Color.White;
            guna2CircleButton2.Image = (Image)resources.GetObject("guna2CircleButton2.Image");
            guna2CircleButton2.Location = new Point(1030, 8);
            guna2CircleButton2.Name = "guna2CircleButton2";
            guna2CircleButton2.ShadowDecoration.CustomizableEdges = customizableEdges49;
            guna2CircleButton2.ShadowDecoration.Mode = ShadowMode.Circle;
            guna2CircleButton2.Size = new Size(39, 36);
            guna2CircleButton2.TabIndex = 7;
            guna2HtmlToolTip1.SetToolTip(guna2CircleButton2, "Refresh Adapters");
            guna2CircleButton2.Click += guna2CircleButton2_Click;
            // 
            // guna2CircleButton1
            // 
            guna2CircleButton1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2CircleButton1.DisabledState.BorderColor = Color.DarkGray;
            guna2CircleButton1.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2CircleButton1.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2CircleButton1.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2CircleButton1.FillColor = Color.FromArgb(30, 30, 30);
            guna2CircleButton1.Font = new Font("Segoe UI", 9F);
            guna2CircleButton1.ForeColor = Color.White;
            guna2CircleButton1.Image = (Image)resources.GetObject("guna2CircleButton1.Image");
            guna2CircleButton1.Location = new Point(1075, 8);
            guna2CircleButton1.Name = "guna2CircleButton1";
            guna2CircleButton1.ShadowDecoration.CustomizableEdges = customizableEdges50;
            guna2CircleButton1.ShadowDecoration.Mode = ShadowMode.Circle;
            guna2CircleButton1.Size = new Size(39, 36);
            guna2CircleButton1.TabIndex = 6;
            guna2HtmlToolTip1.SetToolTip(guna2CircleButton1, "Settings");
            guna2CircleButton1.Click += guna2CircleButton1_Click;
            // 
            // guna2ControlBox4
            // 
            guna2ControlBox4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ControlBox4.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox4.ControlBoxType = ControlBoxType.MinimizeBox;
            guna2ControlBox4.CustomizableEdges = customizableEdges51;
            guna2ControlBox4.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox4.IconColor = Color.White;
            guna2ControlBox4.Location = new Point(1914, 3);
            guna2ControlBox4.Name = "guna2ControlBox4";
            guna2ControlBox4.ShadowDecoration.CustomizableEdges = customizableEdges52;
            guna2ControlBox4.Size = new Size(45, 29);
            guna2ControlBox4.TabIndex = 5;
            // 
            // guna2ControlBox5
            // 
            guna2ControlBox5.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ControlBox5.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox5.ControlBoxType = ControlBoxType.MaximizeBox;
            guna2ControlBox5.CustomizableEdges = customizableEdges53;
            guna2ControlBox5.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox5.IconColor = Color.White;
            guna2ControlBox5.Location = new Point(1959, 3);
            guna2ControlBox5.Name = "guna2ControlBox5";
            guna2ControlBox5.ShadowDecoration.CustomizableEdges = customizableEdges54;
            guna2ControlBox5.Size = new Size(45, 29);
            guna2ControlBox5.TabIndex = 4;
            // 
            // guna2ControlBox6
            // 
            guna2ControlBox6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2ControlBox6.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox6.CustomizableEdges = customizableEdges55;
            guna2ControlBox6.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox6.IconColor = Color.White;
            guna2ControlBox6.Location = new Point(2004, 3);
            guna2ControlBox6.Name = "guna2ControlBox6";
            guna2ControlBox6.ShadowDecoration.CustomizableEdges = customizableEdges56;
            guna2ControlBox6.Size = new Size(45, 29);
            guna2ControlBox6.TabIndex = 3;
            // 
            // FilteredGamesDGV
            // 
            FilteredGamesDGV.AllowUserToAddRows = false;
            FilteredGamesDGV.AllowUserToDeleteRows = false;
            FilteredGamesDGV.AllowUserToResizeColumns = false;
            FilteredGamesDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle13.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = Color.White;
            dataGridViewCellStyle13.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle13.SelectionForeColor = Color.White;
            FilteredGamesDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            FilteredGamesDGV.BackgroundColor = Color.FromArgb(30, 30, 30);
            FilteredGamesDGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = Color.White;
            dataGridViewCellStyle14.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle14.SelectionForeColor = Color.White;
            dataGridViewCellStyle14.WrapMode = DataGridViewTriState.True;
            FilteredGamesDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            FilteredGamesDGV.ColumnHeadersHeight = 30;
            FilteredGamesDGV.ContextMenuStrip = guna2ContextMenuStrip1;
            dataGridViewCellStyle15.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle15.ForeColor = Color.White;
            dataGridViewCellStyle15.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle15.SelectionForeColor = Color.White;
            dataGridViewCellStyle15.WrapMode = DataGridViewTriState.False;
            FilteredGamesDGV.DefaultCellStyle = dataGridViewCellStyle15;
            FilteredGamesDGV.Dock = DockStyle.Fill;
            FilteredGamesDGV.GridColor = Color.FromArgb(25, 25, 25);
            FilteredGamesDGV.Location = new Point(3, 3);
            FilteredGamesDGV.Name = "FilteredGamesDGV";
            FilteredGamesDGV.ReadOnly = true;
            FilteredGamesDGV.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle16.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle16.ForeColor = Color.White;
            dataGridViewCellStyle16.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle16.SelectionForeColor = Color.White;
            dataGridViewCellStyle16.WrapMode = DataGridViewTriState.True;
            FilteredGamesDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
            FilteredGamesDGV.RowHeadersVisible = false;
            FilteredGamesDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            FilteredGamesDGV.RowTemplate.Height = 35;
            FilteredGamesDGV.Size = new Size(1112, 415);
            FilteredGamesDGV.TabIndex = 164;
            FilteredGamesDGV.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            FilteredGamesDGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            FilteredGamesDGV.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            FilteredGamesDGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            FilteredGamesDGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            FilteredGamesDGV.ThemeStyle.BackColor = Color.FromArgb(30, 30, 30);
            FilteredGamesDGV.ThemeStyle.GridColor = Color.FromArgb(25, 25, 25);
            FilteredGamesDGV.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            FilteredGamesDGV.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            FilteredGamesDGV.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            FilteredGamesDGV.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            FilteredGamesDGV.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            FilteredGamesDGV.ThemeStyle.HeaderStyle.Height = 30;
            FilteredGamesDGV.ThemeStyle.ReadOnly = true;
            FilteredGamesDGV.ThemeStyle.RowsStyle.BackColor = Color.White;
            FilteredGamesDGV.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            FilteredGamesDGV.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            FilteredGamesDGV.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            FilteredGamesDGV.ThemeStyle.RowsStyle.Height = 35;
            FilteredGamesDGV.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            FilteredGamesDGV.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // guna2ContextMenuStrip1
            // 
            guna2ContextMenuStrip1.AllowDrop = true;
            guna2ContextMenuStrip1.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.BackgroundImageLayout = ImageLayout.None;
            guna2ContextMenuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyTooClipboardToolStripMenuItem, copyEntireRowToolStripMenuItem, clearAllToolStripMenuItem, clearSelectedRowToolStripMenuItem, pingCellToolStripMenuItem, packetAnalyzerToolStripMenuItem });
            guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip1.RenderStyle.ArrowColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip1.RenderStyle.BorderColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip1.RenderStyle.RoundedEdges = false;
            guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SeparatorColor = Color.FromArgb(15, 15, 15);
            guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            guna2ContextMenuStrip1.ShowImageMargin = false;
            guna2ContextMenuStrip1.Size = new Size(158, 136);
            // 
            // copyTooClipboardToolStripMenuItem
            // 
            copyTooClipboardToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            copyTooClipboardToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            copyTooClipboardToolStripMenuItem.ForeColor = Color.White;
            copyTooClipboardToolStripMenuItem.Name = "copyTooClipboardToolStripMenuItem";
            copyTooClipboardToolStripMenuItem.Size = new Size(157, 22);
            copyTooClipboardToolStripMenuItem.Text = "Copy too Clipboard";
            copyTooClipboardToolStripMenuItem.Click += copyTooClipboardToolStripMenuItem_Click;
            // 
            // copyEntireRowToolStripMenuItem
            // 
            copyEntireRowToolStripMenuItem.ForeColor = Color.White;
            copyEntireRowToolStripMenuItem.Name = "copyEntireRowToolStripMenuItem";
            copyEntireRowToolStripMenuItem.Size = new Size(157, 22);
            copyEntireRowToolStripMenuItem.Text = "Copy Entire Row";
            copyEntireRowToolStripMenuItem.Click += copyEntireRowToolStripMenuItem_Click;
            // 
            // clearAllToolStripMenuItem
            // 
            clearAllToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            clearAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            clearAllToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clearAllToolStripMenuItem.ForeColor = Color.White;
            clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            clearAllToolStripMenuItem.Size = new Size(157, 22);
            clearAllToolStripMenuItem.Text = "Clear All";
            clearAllToolStripMenuItem.Click += clearAllToolStripMenuItem_Click;
            // 
            // clearSelectedRowToolStripMenuItem
            // 
            clearSelectedRowToolStripMenuItem.ForeColor = Color.White;
            clearSelectedRowToolStripMenuItem.Name = "clearSelectedRowToolStripMenuItem";
            clearSelectedRowToolStripMenuItem.Size = new Size(157, 22);
            clearSelectedRowToolStripMenuItem.Text = "Clear Selected Row";
            clearSelectedRowToolStripMenuItem.Click += clearSelectedRowToolStripMenuItem_Click;
            // 
            // pingCellToolStripMenuItem
            // 
            pingCellToolStripMenuItem.ForeColor = Color.White;
            pingCellToolStripMenuItem.Name = "pingCellToolStripMenuItem";
            pingCellToolStripMenuItem.Size = new Size(157, 22);
            pingCellToolStripMenuItem.Text = "Ping Cell";
            pingCellToolStripMenuItem.Click += pingCellToolStripMenuItem_Click;
            // 
            // packetAnalyzerToolStripMenuItem
            // 
            packetAnalyzerToolStripMenuItem.ForeColor = Color.White;
            packetAnalyzerToolStripMenuItem.Name = "packetAnalyzerToolStripMenuItem";
            packetAnalyzerToolStripMenuItem.Size = new Size(157, 22);
            packetAnalyzerToolStripMenuItem.Text = "Packet Analyzer";
            packetAnalyzerToolStripMenuItem.Click += packetAnalyzerToolStripMenuItem_Click;
            // 
            // guna2TabControl1
            // 
            guna2TabControl1.Controls.Add(tabPage1);
            guna2TabControl1.Controls.Add(tabPage2);
            guna2TabControl1.Controls.Add(tabPage3);
            guna2TabControl1.Controls.Add(tabPage4);
            guna2TabControl1.Dock = DockStyle.Fill;
            guna2TabControl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TabControl1.ItemSize = new Size(180, 40);
            guna2TabControl1.Location = new Point(0, 88);
            guna2TabControl1.Name = "guna2TabControl1";
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Size = new Size(1126, 469);
            guna2TabControl1.TabButtonHoverState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonHoverState.FillColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonHoverState.Font = new Font("Segoe UI Semibold", 10F);
            guna2TabControl1.TabButtonHoverState.ForeColor = Color.White;
            guna2TabControl1.TabButtonHoverState.InnerColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonIdleState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonIdleState.FillColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonIdleState.Font = new Font("Segoe UI Semibold", 10F);
            guna2TabControl1.TabButtonIdleState.ForeColor = Color.White;
            guna2TabControl1.TabButtonIdleState.InnerColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonSelectedState.FillColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White;
            guna2TabControl1.TabButtonSelectedState.InnerColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonSize = new Size(180, 40);
            guna2TabControl1.TabIndex = 6;
            guna2TabControl1.TabMenuBackColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabMenuOrientation = TabMenuOrientation.HorizontalTop;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(25, 25, 25);
            tabPage1.Controls.Add(guna2vScrollBar1);
            tabPage1.Controls.Add(FilteredGamesDGV);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1118, 421);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Filtered Games";
            // 
            // guna2vScrollBar1
            // 
            guna2vScrollBar1.BindingContainer = FilteredGamesDGV;
            guna2vScrollBar1.FillColor = Color.White;
            guna2vScrollBar1.InUpdate = false;
            guna2vScrollBar1.LargeChange = 10;
            guna2vScrollBar1.Location = new Point(1097, 3);
            guna2vScrollBar1.Minimum = 1;
            guna2vScrollBar1.Name = "guna2vScrollBar1";
            guna2vScrollBar1.ScrollbarSize = 18;
            guna2vScrollBar1.Size = new Size(18, 415);
            guna2vScrollBar1.TabIndex = 5;
            guna2vScrollBar1.ThumbColor = Color.FromArgb(25, 25, 25);
            guna2vScrollBar1.Value = 1;
            // 
            // tabPage2
            // 
            tabPage2.BackColor = Color.FromArgb(25, 25, 25);
            tabPage2.Controls.Add(guna2vScrollBar2);
            tabPage2.Controls.Add(OtherInfoDGV);
            tabPage2.Location = new Point(4, 44);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1118, 421);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Other Info";
            // 
            // guna2vScrollBar2
            // 
            guna2vScrollBar2.BindingContainer = OtherInfoDGV;
            guna2vScrollBar2.FillColor = Color.White;
            guna2vScrollBar2.InUpdate = false;
            guna2vScrollBar2.LargeChange = 10;
            guna2vScrollBar2.Location = new Point(1097, 3);
            guna2vScrollBar2.Minimum = 1;
            guna2vScrollBar2.Name = "guna2vScrollBar2";
            guna2vScrollBar2.ScrollbarSize = 18;
            guna2vScrollBar2.Size = new Size(18, 415);
            guna2vScrollBar2.TabIndex = 7;
            guna2vScrollBar2.ThumbColor = Color.FromArgb(25, 25, 25);
            guna2vScrollBar2.Value = 1;
            // 
            // OtherInfoDGV
            // 
            OtherInfoDGV.AllowDrop = true;
            OtherInfoDGV.AllowUserToAddRows = false;
            OtherInfoDGV.AllowUserToDeleteRows = false;
            OtherInfoDGV.AllowUserToResizeColumns = false;
            OtherInfoDGV.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            OtherInfoDGV.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            OtherInfoDGV.BackgroundColor = Color.FromArgb(30, 30, 30);
            OtherInfoDGV.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            OtherInfoDGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            OtherInfoDGV.ColumnHeadersHeight = 30;
            OtherInfoDGV.ContextMenuStrip = guna2ContextMenuStrip2;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            OtherInfoDGV.DefaultCellStyle = dataGridViewCellStyle3;
            OtherInfoDGV.Dock = DockStyle.Fill;
            OtherInfoDGV.GridColor = Color.FromArgb(25, 25, 25);
            OtherInfoDGV.Location = new Point(3, 3);
            OtherInfoDGV.Name = "OtherInfoDGV";
            OtherInfoDGV.ReadOnly = true;
            OtherInfoDGV.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            OtherInfoDGV.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            OtherInfoDGV.RowHeadersVisible = false;
            OtherInfoDGV.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            OtherInfoDGV.RowTemplate.Height = 35;
            OtherInfoDGV.Size = new Size(1112, 415);
            OtherInfoDGV.TabIndex = 6;
            OtherInfoDGV.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            OtherInfoDGV.ThemeStyle.AlternatingRowsStyle.Font = null;
            OtherInfoDGV.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            OtherInfoDGV.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            OtherInfoDGV.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            OtherInfoDGV.ThemeStyle.BackColor = Color.FromArgb(30, 30, 30);
            OtherInfoDGV.ThemeStyle.GridColor = Color.FromArgb(25, 25, 25);
            OtherInfoDGV.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            OtherInfoDGV.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            OtherInfoDGV.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            OtherInfoDGV.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            OtherInfoDGV.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            OtherInfoDGV.ThemeStyle.HeaderStyle.Height = 30;
            OtherInfoDGV.ThemeStyle.ReadOnly = true;
            OtherInfoDGV.ThemeStyle.RowsStyle.BackColor = Color.White;
            OtherInfoDGV.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            OtherInfoDGV.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            OtherInfoDGV.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            OtherInfoDGV.ThemeStyle.RowsStyle.Height = 35;
            OtherInfoDGV.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            OtherInfoDGV.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // guna2ContextMenuStrip2
            // 
            guna2ContextMenuStrip2.AllowDrop = true;
            guna2ContextMenuStrip2.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip2.BackgroundImageLayout = ImageLayout.None;
            guna2ContextMenuStrip2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2ContextMenuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, toolStripMenuItem2, toolStripMenuItem3, toolStripMenuItem4, toolStripMenuItem5, toolStripMenuItem6 });
            guna2ContextMenuStrip2.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip2.RenderStyle.ArrowColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip2.RenderStyle.BorderColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip2.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip2.RenderStyle.RoundedEdges = false;
            guna2ContextMenuStrip2.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip2.RenderStyle.SelectionBackColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip2.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip2.RenderStyle.SeparatorColor = Color.FromArgb(15, 15, 15);
            guna2ContextMenuStrip2.RenderStyle.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            guna2ContextMenuStrip2.ShowImageMargin = false;
            guna2ContextMenuStrip2.Size = new Size(158, 136);
            // 
            // toolStripMenuItem1
            // 
            toolStripMenuItem1.BackColor = Color.FromArgb(25, 25, 25);
            toolStripMenuItem1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripMenuItem1.ForeColor = Color.White;
            toolStripMenuItem1.Name = "toolStripMenuItem1";
            toolStripMenuItem1.Size = new Size(157, 22);
            toolStripMenuItem1.Text = "Copy too Clipboard";
            toolStripMenuItem1.Click += toolStripMenuItem1_Click;
            // 
            // toolStripMenuItem2
            // 
            toolStripMenuItem2.ForeColor = Color.White;
            toolStripMenuItem2.Name = "toolStripMenuItem2";
            toolStripMenuItem2.Size = new Size(157, 22);
            toolStripMenuItem2.Text = "Copy Entire Row";
            toolStripMenuItem2.Click += toolStripMenuItem2_Click;
            // 
            // toolStripMenuItem3
            // 
            toolStripMenuItem3.BackColor = Color.FromArgb(25, 25, 25);
            toolStripMenuItem3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripMenuItem3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            toolStripMenuItem3.ForeColor = Color.White;
            toolStripMenuItem3.Name = "toolStripMenuItem3";
            toolStripMenuItem3.Size = new Size(157, 22);
            toolStripMenuItem3.Text = "Clear All";
            toolStripMenuItem3.Click += toolStripMenuItem3_Click;
            // 
            // toolStripMenuItem4
            // 
            toolStripMenuItem4.ForeColor = Color.White;
            toolStripMenuItem4.Name = "toolStripMenuItem4";
            toolStripMenuItem4.Size = new Size(157, 22);
            toolStripMenuItem4.Text = "Clear Selected Row";
            toolStripMenuItem4.Click += toolStripMenuItem4_Click;
            // 
            // toolStripMenuItem5
            // 
            toolStripMenuItem5.ForeColor = Color.White;
            toolStripMenuItem5.Name = "toolStripMenuItem5";
            toolStripMenuItem5.Size = new Size(157, 22);
            toolStripMenuItem5.Text = "Ping Cell";
            toolStripMenuItem5.Click += toolStripMenuItem5_Click;
            // 
            // toolStripMenuItem6
            // 
            toolStripMenuItem6.ForeColor = Color.White;
            toolStripMenuItem6.Name = "toolStripMenuItem6";
            toolStripMenuItem6.Size = new Size(157, 22);
            toolStripMenuItem6.Text = "Packet Analyzer";
            toolStripMenuItem6.Click += toolStripMenuItem6_Click;
            // 
            // tabPage3
            // 
            tabPage3.BackColor = Color.FromArgb(25, 25, 25);
            tabPage3.Controls.Add(guna2DataGridView4);
            tabPage3.Location = new Point(4, 44);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1118, 421);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Xbox";
            // 
            // guna2DataGridView4
            // 
            guna2DataGridView4.AllowDrop = true;
            guna2DataGridView4.AllowUserToAddRows = false;
            guna2DataGridView4.AllowUserToDeleteRows = false;
            guna2DataGridView4.AllowUserToResizeColumns = false;
            guna2DataGridView4.AllowUserToResizeRows = false;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle5.SelectionForeColor = Color.White;
            guna2DataGridView4.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle5;
            guna2DataGridView4.BackgroundColor = Color.FromArgb(30, 30, 30);
            guna2DataGridView4.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.True;
            guna2DataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle6;
            guna2DataGridView4.ColumnHeadersHeight = 30;
            guna2DataGridView4.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView4.Columns.AddRange(new DataGridViewColumn[] { dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2, dataGridViewTextBoxColumn10, dataGridViewTextBoxColumn11, dataGridViewTextBoxColumn12, dataGridViewTextBoxColumn13 });
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle7.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = Color.White;
            dataGridViewCellStyle7.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle7.SelectionForeColor = Color.White;
            dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
            guna2DataGridView4.DefaultCellStyle = dataGridViewCellStyle7;
            guna2DataGridView4.Dock = DockStyle.Fill;
            guna2DataGridView4.GridColor = Color.FromArgb(25, 25, 25);
            guna2DataGridView4.Location = new Point(3, 3);
            guna2DataGridView4.Name = "guna2DataGridView4";
            guna2DataGridView4.ReadOnly = true;
            guna2DataGridView4.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle8.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle8.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = Color.White;
            dataGridViewCellStyle8.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle8.SelectionForeColor = Color.White;
            dataGridViewCellStyle8.WrapMode = DataGridViewTriState.True;
            guna2DataGridView4.RowHeadersDefaultCellStyle = dataGridViewCellStyle8;
            guna2DataGridView4.RowHeadersVisible = false;
            guna2DataGridView4.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            guna2DataGridView4.RowTemplate.Height = 35;
            guna2DataGridView4.Size = new Size(1112, 415);
            guna2DataGridView4.TabIndex = 8;
            guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.Font = null;
            guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            guna2DataGridView4.ThemeStyle.BackColor = Color.FromArgb(30, 30, 30);
            guna2DataGridView4.ThemeStyle.GridColor = Color.FromArgb(25, 25, 25);
            guna2DataGridView4.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            guna2DataGridView4.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            guna2DataGridView4.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView4.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            guna2DataGridView4.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            guna2DataGridView4.ThemeStyle.HeaderStyle.Height = 30;
            guna2DataGridView4.ThemeStyle.ReadOnly = true;
            guna2DataGridView4.ThemeStyle.RowsStyle.BackColor = Color.White;
            guna2DataGridView4.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            guna2DataGridView4.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            guna2DataGridView4.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            guna2DataGridView4.ThemeStyle.RowsStyle.Height = 35;
            guna2DataGridView4.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            guna2DataGridView4.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // dataGridViewTextBoxColumn1
            // 
            dataGridViewTextBoxColumn1.HeaderText = "IP Address";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            dataGridViewTextBoxColumn2.HeaderText = "Port";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn10
            // 
            dataGridViewTextBoxColumn10.HeaderText = "Country";
            dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            dataGridViewTextBoxColumn10.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn11
            // 
            dataGridViewTextBoxColumn11.HeaderText = "State";
            dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            dataGridViewTextBoxColumn11.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn12
            // 
            dataGridViewTextBoxColumn12.HeaderText = "City";
            dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            dataGridViewTextBoxColumn12.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn13
            // 
            dataGridViewTextBoxColumn13.HeaderText = "ISP";
            dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            dataGridViewTextBoxColumn13.ReadOnly = true;
            // 
            // tabPage4
            // 
            tabPage4.BackColor = Color.FromArgb(25, 25, 25);
            tabPage4.Controls.Add(guna2Button3);
            tabPage4.Controls.Add(guna2TextBox2);
            tabPage4.Controls.Add(guna2Button6);
            tabPage4.Controls.Add(guna2TextBox3);
            tabPage4.Controls.Add(guna2Button5);
            tabPage4.Controls.Add(guna2Button2);
            tabPage4.Controls.Add(LoginBTN);
            tabPage4.Controls.Add(guna2TextBox1);
            tabPage4.Controls.Add(guna2Button4);
            tabPage4.Location = new Point(4, 44);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(3);
            tabPage4.Size = new Size(1118, 421);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "XBL Tool";
            // 
            // guna2Button3
            // 
            guna2Button3.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button3.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button3.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button3.CustomizableEdges = customizableEdges57;
            guna2Button3.DisabledState.BorderColor = Color.DarkGray;
            guna2Button3.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button3.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button3.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button3.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button3.ForeColor = Color.White;
            guna2Button3.Location = new Point(955, 7);
            guna2Button3.Name = "guna2Button3";
            guna2Button3.ShadowDecoration.CustomizableEdges = customizableEdges58;
            guna2Button3.Size = new Size(155, 36);
            guna2Button3.TabIndex = 7;
            guna2Button3.Text = "Authorize";
            guna2Button3.Click += guna2Button3_Click;
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2TextBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.CustomizableEdges = customizableEdges59;
            guna2TextBox2.DefaultText = "";
            guna2TextBox2.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox2.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox2.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox2.FillColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TextBox2.ForeColor = Color.White;
            guna2TextBox2.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.Location = new Point(6, 7);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PasswordChar = '\0';
            guna2TextBox2.PlaceholderForeColor = Color.White;
            guna2TextBox2.PlaceholderText = "Rec room Account Token";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges60;
            guna2TextBox2.Size = new Size(943, 36);
            guna2TextBox2.Style = TextBoxStyle.Material;
            guna2TextBox2.TabIndex = 6;
            // 
            // guna2Button6
            // 
            guna2Button6.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button6.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button6.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button6.CustomizableEdges = customizableEdges61;
            guna2Button6.DisabledState.BorderColor = Color.DarkGray;
            guna2Button6.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button6.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button6.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button6.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button6.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button6.ForeColor = Color.White;
            guna2Button6.Location = new Point(955, 7);
            guna2Button6.Name = "guna2Button6";
            guna2Button6.ShadowDecoration.CustomizableEdges = customizableEdges62;
            guna2Button6.Size = new Size(155, 36);
            guna2Button6.TabIndex = 11;
            guna2Button6.Text = "Authorize";
            guna2Button6.Click += guna2Button6_Click;
            // 
            // guna2TextBox3
            // 
            guna2TextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2TextBox3.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox3.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox3.CustomizableEdges = customizableEdges63;
            guna2TextBox3.DefaultText = "";
            guna2TextBox3.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox3.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox3.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox3.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox3.FillColor = Color.FromArgb(30, 30, 30);
            guna2TextBox3.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TextBox3.ForeColor = Color.White;
            guna2TextBox3.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox3.Location = new Point(6, 7);
            guna2TextBox3.Name = "guna2TextBox3";
            guna2TextBox3.PasswordChar = '\0';
            guna2TextBox3.PlaceholderForeColor = Color.White;
            guna2TextBox3.PlaceholderText = "PlayStation Account Token (NPSSO)";
            guna2TextBox3.SelectedText = "";
            guna2TextBox3.ShadowDecoration.CustomizableEdges = customizableEdges64;
            guna2TextBox3.Size = new Size(943, 36);
            guna2TextBox3.Style = TextBoxStyle.Material;
            guna2TextBox3.TabIndex = 10;
            // 
            // guna2Button5
            // 
            guna2Button5.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button5.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button5.CustomizableEdges = customizableEdges65;
            guna2Button5.DisabledState.BorderColor = Color.DarkGray;
            guna2Button5.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button5.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button5.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button5.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button5.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button5.ForeColor = Color.White;
            guna2Button5.Location = new Point(328, 49);
            guna2Button5.Name = "guna2Button5";
            guna2Button5.ShadowDecoration.CustomizableEdges = customizableEdges66;
            guna2Button5.Size = new Size(155, 36);
            guna2Button5.TabIndex = 9;
            guna2Button5.Text = "PlayStation Tool";
            guna2Button5.Click += guna2Button5_Click;
            // 
            // guna2Button2
            // 
            guna2Button2.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button2.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button2.CustomizableEdges = customizableEdges67;
            guna2Button2.DisabledState.BorderColor = Color.DarkGray;
            guna2Button2.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button2.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button2.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button2.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button2.ForeColor = Color.White;
            guna2Button2.Location = new Point(167, 49);
            guna2Button2.Name = "guna2Button2";
            guna2Button2.ShadowDecoration.CustomizableEdges = customizableEdges68;
            guna2Button2.Size = new Size(155, 36);
            guna2Button2.TabIndex = 5;
            guna2Button2.Text = "Rec Room Tool";
            guna2Button2.Click += guna2Button2_Click;
            // 
            // LoginBTN
            // 
            LoginBTN.BackColor = Color.FromArgb(30, 30, 30);
            LoginBTN.BorderColor = Color.FromArgb(30, 30, 30);
            LoginBTN.CustomizableEdges = customizableEdges69;
            LoginBTN.DisabledState.BorderColor = Color.DarkGray;
            LoginBTN.DisabledState.CustomBorderColor = Color.DarkGray;
            LoginBTN.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            LoginBTN.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            LoginBTN.FillColor = Color.FromArgb(30, 30, 30);
            LoginBTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LoginBTN.ForeColor = Color.White;
            LoginBTN.Location = new Point(6, 49);
            LoginBTN.Name = "LoginBTN";
            LoginBTN.ShadowDecoration.CustomizableEdges = customizableEdges70;
            LoginBTN.Size = new Size(155, 36);
            LoginBTN.TabIndex = 4;
            LoginBTN.Text = "Xbox Tool";
            LoginBTN.Click += LoginBTN_Click;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            guna2TextBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.CustomizableEdges = customizableEdges71;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TextBox1.ForeColor = Color.White;
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.Location = new Point(6, 7);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderForeColor = Color.White;
            guna2TextBox1.PlaceholderText = "XBL Token";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges72;
            guna2TextBox1.Size = new Size(943, 36);
            guna2TextBox1.Style = TextBoxStyle.Material;
            guna2TextBox1.TabIndex = 3;
            // 
            // guna2Button4
            // 
            guna2Button4.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            guna2Button4.BackColor = Color.FromArgb(30, 30, 30);
            guna2Button4.BorderColor = Color.FromArgb(30, 30, 30);
            guna2Button4.CustomizableEdges = customizableEdges73;
            guna2Button4.DisabledState.BorderColor = Color.DarkGray;
            guna2Button4.DisabledState.CustomBorderColor = Color.DarkGray;
            guna2Button4.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            guna2Button4.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            guna2Button4.FillColor = Color.FromArgb(30, 30, 30);
            guna2Button4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2Button4.ForeColor = Color.White;
            guna2Button4.Location = new Point(955, 7);
            guna2Button4.Name = "guna2Button4";
            guna2Button4.ShadowDecoration.CustomizableEdges = customizableEdges74;
            guna2Button4.Size = new Size(155, 36);
            guna2Button4.TabIndex = 8;
            guna2Button4.Text = "Authorize";
            guna2Button4.Click += guna2Button4_Click;
            // 
            // guna2HtmlToolTip1
            // 
            guna2HtmlToolTip1.AllowLinksHandling = true;
            guna2HtmlToolTip1.MaximumSize = new Size(0, 0);
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // MainMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(1126, 599);
            Controls.Add(guna2TabControl1);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ZOPZ SNIFF";
            Load += Form1_Load;
            LocationChanged += Form1_LocationChanged;
            SizeChanged += Form1_SizeChanged;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            ((ISupportInitialize)FilteredGamesDGV).EndInit();
            guna2ContextMenuStrip1.ResumeLayout(false);
            guna2TabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ((ISupportInitialize)OtherInfoDGV).EndInit();
            guna2ContextMenuStrip2.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            ((ISupportInitialize)guna2DataGridView4).EndInit();
            tabPage4.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        public class CustomColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected
            {
                get => Color.FromArgb(30, 30, 30);
            }

            public override Color MenuItemBorder
            {
                get => Color.FromArgb(30, 30, 30);
            }

            public override Color MenuItemSelectedGradientBegin
            {
                get => Color.FromArgb(30, 30, 30);
            }

            public override Color MenuItemSelectedGradientEnd
            {
                get => Color.FromArgb(30, 30, 30);
            }


            public override Color MenuItemPressedGradientBegin
            {
                get => Color.FromArgb(25, 25, 25);
            }

            public override Color MenuItemPressedGradientEnd
            {
                get => Color.FromArgb(25, 25, 25);
            }
        }

        private Guna2Button guna2Button5;
        private Guna2TextBox guna2TextBox3;
        private Guna2Button guna2Button6;
    }
}