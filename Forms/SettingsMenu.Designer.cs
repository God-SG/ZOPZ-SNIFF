using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class SettingsMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
   		private IContainer components = null;
        private Guna2TabControl guna2TabControl1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Label label1;
        private Panel panel1;
        private TabPage tabPage5;
        private TabPage tabPage6;
        private TabPage tabPage7;
        private Guna2VScrollBar guna2vScrollBar2;
        private Label label16;
        private Panel panel4;
        private Label label12;
        private Label label14;
        private Label label15;
        private Label label10;
        private Panel panel3;
        private Label label9;
        private Label label11;
        private Label label13;
        private TabPage tabPage1;
        private Guna2VScrollBar guna2vScrollBar1;
        private Guna2DataGridView dataGridView1;
        private Guna2TextBox host;
        private Guna2TextBox guna2TextBox1;
        private Label label17;
        private Panel panel5;
        private Guna2CheckBox guna2CheckBox1;
        private Guna2CheckBox guna2CheckBox2;
        private Guna2VScrollBar guna2vScrollBar3;
        private FlowLayoutPanel chatPanel;
        private Guna2TextBox Messageboxtb;
        private FlowLayoutPanel FilterDisplayPanel;
        private Label label18;
        private Label label19;

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
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(SettingsMenu));
            guna2TabControl1 = new Guna2TabControl();
            tabPage5 = new TabPage();
            label17 = new Label();
            panel5 = new Panel();
            guna2CheckBox2 = new Guna2CheckBox();
            guna2CheckBox1 = new Guna2CheckBox();
            label16 = new Label();
            panel4 = new Panel();
            label12 = new Label();
            label14 = new Label();
            label15 = new Label();
            label10 = new Label();
            panel3 = new Panel();
            label9 = new Label();
            label11 = new Label();
            label13 = new Label();
            tabPage6 = new TabPage();
            label2 = new Label();
            label19 = new Label();
            guna2vScrollBar2 = new Guna2VScrollBar();
            FilterDisplayPanel = new FlowLayoutPanel();
            label18 = new Label();
            tabPage7 = new TabPage();
            guna2vScrollBar3 = new Guna2VScrollBar();
            chatPanel = new FlowLayoutPanel();
            Messageboxtb = new Guna2TextBox();
            tabPage1 = new TabPage();
            host = new Guna2TextBox();
            guna2TextBox1 = new Guna2TextBox();
            guna2vScrollBar1 = new Guna2VScrollBar();
            dataGridView1 = new Guna2DataGridView();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            panel1 = new Panel();
            guna2ContextMenuStrip2 = new Guna2ContextMenuStrip();
            toolStripMenuItem1 = new ToolStripMenuItem();
            clearSelectedRowToolStripMenuItem = new ToolStripMenuItem();
            clearAllToolStripMenuItem = new ToolStripMenuItem();
            guna2TabControl1.SuspendLayout();
            tabPage5.SuspendLayout();
            panel5.SuspendLayout();
            panel4.SuspendLayout();
            panel3.SuspendLayout();
            tabPage6.SuspendLayout();
            tabPage7.SuspendLayout();
            tabPage1.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            panel1.SuspendLayout();
            guna2ContextMenuStrip2.SuspendLayout();
            SuspendLayout();
            // 
            // guna2TabControl1
            // 
            guna2TabControl1.Controls.Add(tabPage5);
            guna2TabControl1.Controls.Add(tabPage6);
            guna2TabControl1.Controls.Add(tabPage7);
            guna2TabControl1.Controls.Add(tabPage1);
            guna2TabControl1.Dock = DockStyle.Fill;
            guna2TabControl1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TabControl1.ImeMode = ImeMode.NoControl;
            guna2TabControl1.ItemSize = new Size(170, 40);
            guna2TabControl1.Location = new Point(0, 34);
            guna2TabControl1.Name = "guna2TabControl1";
            guna2TabControl1.SelectedIndex = 0;
            guna2TabControl1.Size = new Size(711, 392);
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
            guna2TabControl1.TabButtonImageAlign = HorizontalAlignment.Left;
            guna2TabControl1.TabButtonSelectedState.BorderColor = Color.Empty;
            guna2TabControl1.TabButtonSelectedState.FillColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonSelectedState.Font = new Font("Segoe UI Semibold", 10F);
            guna2TabControl1.TabButtonSelectedState.ForeColor = Color.White;
            guna2TabControl1.TabButtonSelectedState.InnerColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabButtonSize = new Size(170, 40);
            guna2TabControl1.TabIndex = 9;
            guna2TabControl1.TabMenuBackColor = Color.FromArgb(25, 25, 25);
            guna2TabControl1.TabMenuOrientation = TabMenuOrientation.HorizontalTop;
            guna2TabControl1.TabIndexChanged += guna2TabControl1_TabIndexChanged;
            // 
            // tabPage5
            // 
            tabPage5.BackColor = Color.FromArgb(25, 25, 25);
            tabPage5.Controls.Add(label17);
            tabPage5.Controls.Add(panel5);
            tabPage5.Controls.Add(label16);
            tabPage5.Controls.Add(panel4);
            tabPage5.Controls.Add(label10);
            tabPage5.Controls.Add(panel3);
            tabPage5.Location = new Point(4, 44);
            tabPage5.Name = "tabPage5";
            tabPage5.Size = new Size(703, 344);
            tabPage5.TabIndex = 1;
            tabPage5.Text = "Interface";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.BackColor = Color.FromArgb(30, 30, 30);
            label17.FlatStyle = FlatStyle.Flat;
            label17.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label17.ForeColor = Color.White;
            label17.Location = new Point(29, 198);
            label17.Name = "label17";
            label17.Size = new Size(53, 15);
            label17.TabIndex = 28;
            label17.Text = "Settings";
            // 
            // panel5
            // 
            panel5.BackColor = Color.FromArgb(30, 30, 30);
            panel5.Controls.Add(guna2CheckBox2);
            panel5.Controls.Add(guna2CheckBox1);
            panel5.Location = new Point(29, 227);
            panel5.Name = "panel5";
            panel5.Size = new Size(642, 51);
            panel5.TabIndex = 27;
            // 
            // guna2CheckBox2
            // 
            guna2CheckBox2.AutoSize = true;
            guna2CheckBox2.CheckedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2CheckBox2.CheckedState.BorderRadius = 0;
            guna2CheckBox2.CheckedState.BorderThickness = 0;
            guna2CheckBox2.CheckedState.FillColor = Color.FromArgb(30, 30, 30);
            guna2CheckBox2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2CheckBox2.ForeColor = Color.White;
            guna2CheckBox2.Location = new Point(116, 17);
            guna2CheckBox2.Name = "guna2CheckBox2";
            guna2CheckBox2.Size = new Size(93, 19);
            guna2CheckBox2.TabIndex = 25;
            guna2CheckBox2.Text = "Discord RPC";
            guna2CheckBox2.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            guna2CheckBox2.UncheckedState.BorderRadius = 0;
            guna2CheckBox2.UncheckedState.BorderThickness = 0;
            guna2CheckBox2.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            guna2CheckBox2.CheckedChanged += guna2CheckBox2_CheckedChanged;
            // 
            // guna2CheckBox1
            // 
            guna2CheckBox1.AutoSize = true;
            guna2CheckBox1.CheckedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2CheckBox1.CheckedState.BorderRadius = 0;
            guna2CheckBox1.CheckedState.BorderThickness = 0;
            guna2CheckBox1.CheckedState.FillColor = Color.FromArgb(30, 30, 30);
            guna2CheckBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2CheckBox1.ForeColor = Color.White;
            guna2CheckBox1.Location = new Point(14, 17);
            guna2CheckBox1.Name = "guna2CheckBox1";
            guna2CheckBox1.Size = new Size(86, 19);
            guna2CheckBox1.TabIndex = 24;
            guna2CheckBox1.Text = "Auto Login";
            guna2CheckBox1.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            guna2CheckBox1.UncheckedState.BorderRadius = 0;
            guna2CheckBox1.UncheckedState.BorderThickness = 0;
            guna2CheckBox1.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            guna2CheckBox1.CheckedChanged += guna2CheckBox1_CheckedChanged;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.BackColor = Color.FromArgb(30, 30, 30);
            label16.FlatStyle = FlatStyle.Flat;
            label16.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label16.ForeColor = Color.White;
            label16.Location = new Point(478, 24);
            label16.Name = "label16";
            label16.Size = new Size(94, 15);
            label16.TabIndex = 26;
            label16.Text = "Account Details";
            // 
            // panel4
            // 
            panel4.BackColor = Color.FromArgb(30, 30, 30);
            panel4.Controls.Add(label12);
            panel4.Controls.Add(label14);
            panel4.Controls.Add(label15);
            panel4.Location = new Point(478, 56);
            panel4.Name = "panel4";
            panel4.Size = new Size(193, 121);
            panel4.TabIndex = 25;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.FlatStyle = FlatStyle.Flat;
            label12.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label12.ForeColor = Color.White;
            label12.Location = new Point(12, 20);
            label12.Name = "label12";
            label12.Size = new Size(70, 15);
            label12.TabIndex = 7;
            label12.Text = "Username: ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.FlatStyle = FlatStyle.Flat;
            label14.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label14.ForeColor = Color.White;
            label14.Location = new Point(12, 53);
            label14.Name = "label14";
            label14.Size = new Size(94, 15);
            label14.TabIndex = 11;
            label14.Text = "Expiry: Lifetime";
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.FlatStyle = FlatStyle.Flat;
            label15.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label15.ForeColor = Color.White;
            label15.Location = new Point(12, 88);
            label15.Name = "label15";
            label15.Size = new Size(65, 15);
            label15.TabIndex = 13;
            label15.Text = "Last Login:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.FromArgb(30, 30, 30);
            label10.FlatStyle = FlatStyle.Flat;
            label10.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label10.ForeColor = Color.White;
            label10.Location = new Point(29, 24);
            label10.Name = "label10";
            label10.Size = new Size(125, 15);
            label10.TabIndex = 25;
            label10.Text = "Program Information";
            // 
            // panel3
            // 
            panel3.BackColor = Color.FromArgb(30, 30, 30);
            panel3.Controls.Add(label9);
            panel3.Controls.Add(label11);
            panel3.Controls.Add(label13);
            panel3.Location = new Point(29, 56);
            panel3.Name = "panel3";
            panel3.Size = new Size(403, 121);
            panel3.TabIndex = 24;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.FlatStyle = FlatStyle.Flat;
            label9.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label9.ForeColor = Color.White;
            label9.Location = new Point(14, 20);
            label9.Name = "label9";
            label9.Size = new Size(80, 15);
            label9.TabIndex = 7;
            label9.Text = "Total Clients: ";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.FlatStyle = FlatStyle.Flat;
            label11.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label11.ForeColor = Color.White;
            label11.Location = new Point(14, 53);
            label11.Name = "label11";
            label11.Size = new Size(93, 15);
            label11.TabIndex = 11;
            label11.Text = "Total With Plan:";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.FlatStyle = FlatStyle.Flat;
            label13.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label13.ForeColor = Color.White;
            label13.Location = new Point(14, 88);
            label13.Name = "label13";
            label13.Size = new Size(76, 15);
            label13.TabIndex = 13;
            label13.Text = "App Version:";
            // 
            // tabPage6
            // 
            tabPage6.BackColor = Color.FromArgb(25, 25, 25);
            tabPage6.Controls.Add(label2);
            tabPage6.Controls.Add(label19);
            tabPage6.Controls.Add(guna2vScrollBar2);
            tabPage6.Controls.Add(FilterDisplayPanel);
            tabPage6.Controls.Add(label18);
            tabPage6.Location = new Point(4, 44);
            tabPage6.Name = "tabPage6";
            tabPage6.Size = new Size(703, 344);
            tabPage6.TabIndex = 2;
            tabPage6.Text = "Game Filters";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(280, 8);
            label2.Name = "label2";
            label2.Size = new Size(33, 15);
            label2.TabIndex = 13;
            label2.Text = "Type";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.FlatStyle = FlatStyle.Flat;
            label19.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label19.ForeColor = Color.White;
            label19.Location = new Point(398, 8);
            label19.Name = "label19";
            label19.Size = new Size(50, 15);
            label19.TabIndex = 12;
            label19.Text = "Console";
            // 
            // guna2vScrollBar2
            // 
            guna2vScrollBar2.BindingContainer = FilterDisplayPanel;
            guna2vScrollBar2.FillColor = Color.White;
            guna2vScrollBar2.InUpdate = false;
            guna2vScrollBar2.LargeChange = 10;
            guna2vScrollBar2.Location = new Point(685, 27);
            guna2vScrollBar2.Name = "guna2vScrollBar2";
            guna2vScrollBar2.ScrollbarSize = 18;
            guna2vScrollBar2.Size = new Size(18, 317);
            guna2vScrollBar2.TabIndex = 9;
            guna2vScrollBar2.ThumbColor = Color.FromArgb(25, 25, 25);
            // 
            // FilterDisplayPanel
            // 
            FilterDisplayPanel.AutoScroll = true;
            FilterDisplayPanel.BackColor = Color.FromArgb(30, 30, 30);
            FilterDisplayPanel.Dock = DockStyle.Bottom;
            FilterDisplayPanel.FlowDirection = FlowDirection.BottomUp;
            FilterDisplayPanel.Location = new Point(0, 27);
            FilterDisplayPanel.Name = "FilterDisplayPanel";
            FilterDisplayPanel.Size = new Size(703, 317);
            FilterDisplayPanel.TabIndex = 10;
            FilterDisplayPanel.WrapContents = false;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.FlatStyle = FlatStyle.Flat;
            label18.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label18.ForeColor = Color.White;
            label18.Location = new Point(-1, 8);
            label18.Name = "label18";
            label18.Size = new Size(40, 15);
            label18.TabIndex = 11;
            label18.Text = "Name";
            label18.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // tabPage7
            // 
            tabPage7.BackColor = Color.FromArgb(25, 25, 25);
            tabPage7.Controls.Add(guna2vScrollBar3);
            tabPage7.Controls.Add(Messageboxtb);
            tabPage7.Controls.Add(chatPanel);
            tabPage7.Location = new Point(4, 44);
            tabPage7.Name = "tabPage7";
            tabPage7.Size = new Size(703, 344);
            tabPage7.TabIndex = 3;
            tabPage7.Text = "Live Chat";
            // 
            // guna2vScrollBar3
            // 
            guna2vScrollBar3.BindingContainer = chatPanel;
            guna2vScrollBar3.FillColor = Color.White;
            guna2vScrollBar3.InUpdate = false;
            guna2vScrollBar3.LargeChange = 10;
            guna2vScrollBar3.Location = new Point(685, 0);
            guna2vScrollBar3.Name = "guna2vScrollBar3";
            guna2vScrollBar3.ScrollbarSize = 18;
            guna2vScrollBar3.Size = new Size(18, 344);
            guna2vScrollBar3.TabIndex = 14;
            guna2vScrollBar3.ThumbColor = Color.FromArgb(25, 25, 25);
            // 
            // chatPanel
            // 
            chatPanel.AutoScroll = true;
            chatPanel.BackColor = Color.FromArgb(30, 30, 30);
            chatPanel.Dock = DockStyle.Fill;
            chatPanel.FlowDirection = FlowDirection.BottomUp;
            chatPanel.Location = new Point(0, 0);
            chatPanel.Name = "chatPanel";
            chatPanel.Size = new Size(703, 344);
            chatPanel.TabIndex = 166;
            chatPanel.WrapContents = false;
            // 
            // Messageboxtb
            // 
            Messageboxtb.BackColor = Color.FromArgb(30, 30, 30);
            Messageboxtb.BorderColor = Color.FromArgb(30, 30, 30);
            Messageboxtb.CustomizableEdges = customizableEdges1;
            Messageboxtb.DefaultText = "";
            Messageboxtb.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            Messageboxtb.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            Messageboxtb.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            Messageboxtb.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            Messageboxtb.Dock = DockStyle.Bottom;
            Messageboxtb.FillColor = Color.FromArgb(30, 30, 30);
            Messageboxtb.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            Messageboxtb.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            Messageboxtb.ForeColor = Color.White;
            Messageboxtb.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            Messageboxtb.Location = new Point(0, 308);
            Messageboxtb.Name = "Messageboxtb";
            Messageboxtb.PasswordChar = '\0';
            Messageboxtb.PlaceholderForeColor = Color.White;
            Messageboxtb.PlaceholderText = "Type your Message...";
            Messageboxtb.SelectedText = "";
            Messageboxtb.ShadowDecoration.CustomizableEdges = customizableEdges2;
            Messageboxtb.Size = new Size(703, 36);
            Messageboxtb.Style = TextBoxStyle.Material;
            Messageboxtb.TabIndex = 15;
            Messageboxtb.KeyDown += Messageboxtb_KeyDown;
            // 
            // tabPage1
            // 
            tabPage1.BackColor = Color.FromArgb(25, 25, 25);
            tabPage1.Controls.Add(host);
            tabPage1.Controls.Add(guna2TextBox1);
            tabPage1.Controls.Add(guna2vScrollBar1);
            tabPage1.Controls.Add(dataGridView1);
            tabPage1.Location = new Point(4, 44);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(703, 344);
            tabPage1.TabIndex = 4;
            tabPage1.Text = "Labels";
            // 
            // host
            // 
            host.BackColor = Color.FromArgb(30, 30, 30);
            host.BorderColor = Color.FromArgb(30, 30, 30);
            host.CustomizableEdges = customizableEdges3;
            host.DefaultText = "";
            host.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            host.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            host.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            host.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            host.FillColor = Color.FromArgb(30, 30, 30);
            host.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            host.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            host.ForeColor = Color.White;
            host.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            host.Location = new Point(3, 304);
            host.Name = "host";
            host.PasswordChar = '\0';
            host.PlaceholderForeColor = Color.White;
            host.PlaceholderText = "IP Address";
            host.SelectedText = "";
            host.ShadowDecoration.CustomizableEdges = customizableEdges4;
            host.Size = new Size(338, 36);
            host.Style = TextBoxStyle.Material;
            host.TabIndex = 13;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.CustomizableEdges = customizableEdges5;
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
            guna2TextBox1.Location = new Point(347, 304);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderForeColor = Color.White;
            guna2TextBox1.PlaceholderText = "Label";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2TextBox1.Size = new Size(353, 36);
            guna2TextBox1.Style = TextBoxStyle.Material;
            guna2TextBox1.TabIndex = 12;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
            guna2TextBox1.KeyDown += guna2TextBox1_KeyDown;
            // 
            // guna2vScrollBar1
            // 
            guna2vScrollBar1.BindingContainer = dataGridView1;
            guna2vScrollBar1.FillColor = Color.White;
            guna2vScrollBar1.InUpdate = false;
            guna2vScrollBar1.LargeChange = 10;
            guna2vScrollBar1.Location = new Point(682, 3);
            guna2vScrollBar1.Minimum = 1;
            guna2vScrollBar1.Name = "guna2vScrollBar1";
            guna2vScrollBar1.ScrollbarSize = 18;
            guna2vScrollBar1.Size = new Size(18, 298);
            guna2vScrollBar1.TabIndex = 11;
            guna2vScrollBar1.ThumbColor = Color.FromArgb(25, 25, 25);
            guna2vScrollBar1.Value = 1;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowDrop = true;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView1.BackgroundColor = Color.FromArgb(30, 30, 30);
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle2.ForeColor = Color.White;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridView1.ColumnHeadersHeight = 30;
            dataGridView1.ContextMenuStrip = guna2ContextMenuStrip2;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Top;
            dataGridView1.GridColor = Color.FromArgb(25, 25, 25);
            dataGridView1.Location = new Point(3, 3);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle4.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = Color.White;
            dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle4.SelectionForeColor = Color.White;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridView1.RowTemplate.Height = 35;
            dataGridView1.Size = new Size(697, 298);
            dataGridView1.TabIndex = 10;
            dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.White;
            dataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            dataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dataGridView1.ThemeStyle.BackColor = Color.FromArgb(30, 30, 30);
            dataGridView1.ThemeStyle.GridColor = Color.FromArgb(25, 25, 25);
            dataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(100, 88, 255);
            dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridView1.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridView1.ThemeStyle.HeaderStyle.Height = 30;
            dataGridView1.ThemeStyle.ReadOnly = true;
            dataGridView1.ThemeStyle.RowsStyle.BackColor = Color.White;
            dataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridView1.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.FromArgb(71, 69, 94);
            dataGridView1.ThemeStyle.RowsStyle.Height = 35;
            dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(231, 229, 255);
            dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.FromArgb(71, 69, 94);
            // 
            // guna2ControlBox3
            // 
            guna2ControlBox3.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox3.ControlBoxType = ControlBoxType.MinimizeBox;
            guna2ControlBox3.CustomizableEdges = customizableEdges7;
            guna2ControlBox3.Dock = DockStyle.Right;
            guna2ControlBox3.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.IconColor = Color.White;
            guna2ControlBox3.Location = new Point(576, 0);
            guna2ControlBox3.Name = "guna2ControlBox3";
            guna2ControlBox3.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2ControlBox3.Size = new Size(45, 34);
            guna2ControlBox3.TabIndex = 5;
            guna2ControlBox3.Click += guna2ControlBox3_Click;
            // 
            // guna2ControlBox2
            // 
            guna2ControlBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox2.ControlBoxType = ControlBoxType.MaximizeBox;
            guna2ControlBox2.CustomizableEdges = customizableEdges9;
            guna2ControlBox2.Dock = DockStyle.Right;
            guna2ControlBox2.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.IconColor = Color.White;
            guna2ControlBox2.Location = new Point(621, 0);
            guna2ControlBox2.Name = "guna2ControlBox2";
            guna2ControlBox2.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2ControlBox2.Size = new Size(45, 34);
            guna2ControlBox2.TabIndex = 4;
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox1.CustomizableEdges = customizableEdges11;
            guna2ControlBox1.Dock = DockStyle.Right;
            guna2ControlBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(666, 0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges12;
            guna2ControlBox1.Size = new Size(45, 34);
            guna2ControlBox1.TabIndex = 3;
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
            label1.Size = new Size(128, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF | Settings";
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
            panel1.Size = new Size(711, 34);
            panel1.TabIndex = 8;
            // 
            // guna2ContextMenuStrip2
            // 
            guna2ContextMenuStrip2.AllowDrop = true;
            guna2ContextMenuStrip2.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip2.BackgroundImageLayout = ImageLayout.None;
            guna2ContextMenuStrip2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2ContextMenuStrip2.Items.AddRange(new ToolStripItem[] { toolStripMenuItem1, clearSelectedRowToolStripMenuItem, clearAllToolStripMenuItem });
            guna2ContextMenuStrip2.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip2.RenderStyle.ArrowColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip2.RenderStyle.BorderColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip2.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip2.RenderStyle.RoundedEdges = false;
            guna2ContextMenuStrip2.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip2.RenderStyle.SelectionBackColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip2.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip2.RenderStyle.SeparatorColor = Color.FromArgb(15, 15, 15);
            guna2ContextMenuStrip2.RenderStyle.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            guna2ContextMenuStrip2.ShowImageMargin = false;
            guna2ContextMenuStrip2.Size = new Size(158, 92);
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
            // clearSelectedRowToolStripMenuItem
            // 
            clearSelectedRowToolStripMenuItem.ForeColor = Color.White;
            clearSelectedRowToolStripMenuItem.Name = "clearSelectedRowToolStripMenuItem";
            clearSelectedRowToolStripMenuItem.Size = new Size(157, 22);
            clearSelectedRowToolStripMenuItem.Text = "Clear Selected Row";
            clearSelectedRowToolStripMenuItem.Click += clearSelectedRowToolStripMenuItem_Click;
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
            // SettingsMenu
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(711, 426);
            Controls.Add(guna2TabControl1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "SettingsMenu";
            Text = "ZOPZ SNIFF";
            Load += SettingsMenu_Load;
            guna2TabControl1.ResumeLayout(false);
            tabPage5.ResumeLayout(false);
            tabPage5.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel4.ResumeLayout(false);
            panel4.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            tabPage6.ResumeLayout(false);
            tabPage6.PerformLayout();
            tabPage7.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            ((ISupportInitialize)dataGridView1).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            guna2ContextMenuStrip2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label label2;
        private Guna2ContextMenuStrip guna2ContextMenuStrip2;
        private ToolStripMenuItem toolStripMenuItem1;
        private ToolStripMenuItem clearAllToolStripMenuItem;
        private ToolStripMenuItem clearSelectedRowToolStripMenuItem;
    }
}