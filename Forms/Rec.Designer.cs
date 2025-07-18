using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;
using static Guna.UI2.Native.WinApi;
using System.Drawing.Text;

namespace ZOPZ_SNIFF.Menus
{
    partial class Rec
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Panel panel1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Label label1;
        private Guna2TextBox guna2TextBox1;
        private Guna2VScrollBar guna2vScrollBar1;
        private Guna2DataGridView dataGridView1;
        private ToolStripMenuItem copyTooClipboardToolStripMenuItem;
        private ToolStripMenuItem copyEntireRowToolStripMenuItem;
        private ToolStripMenuItem clearSelectedRowToolStripMenuItem;
        private ToolStripMenuItem clearAllToolStripMenuItem;
        private ToolStripMenuItem lookupUsernameToolStripMenuItem;
        private ToolStripMenuItem lookupUseridToolStripMenuItem;
        private ToolStripMenuItem getRoomDataToolStripMenuItem;
        private ToolStripMenuItem massSubBotToolStripMenuItem;
        private ToolStripMenuItem massAddFriendToolStripMenuItem;
        private ToolStripMenuItem pingCellToolStripMenuItem;
        private ToolStripMenuItem massReportToolStripMenuItem;
        private ToolStripMenuItem accountOptionsToolStripMenuItem;
        private ToolStripMenuItem getYoureAccountInfoToolStripMenuItem;
        private ToolStripMenuItem childrenAccountToolStripMenuItem;
        private ToolStripMenuItem reportOptionsToolStripMenuItem;
        private ToolStripMenuItem massReportTrollingToolStripMenuItem;
        private ToolStripMenuItem massReportSexualToolStripMenuItem;
        private ToolStripMenuItem massReportProfileToolStripMenuItem;
        private ToolStripMenuItem massReportUnder13ToolStripMenuItem;
        private ToolStripMenuItem massReportBanEvasionToolStripMenuItem;
        private ToolStripMenuItem massReportDisruptiveTrollingToolStripMenuItem;
        private ToolStripMenuItem massReportImageToolStripMenuItem;
        private Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private ToolStripMenuItem massCheerToolStripMenuItem1;
        private ToolStripMenuItem takeBioToolStripMenuItem;
        private ToolStripMenuItem checkPlayersBioToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;

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
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Rec));
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            guna2TextBox1 = new Guna2TextBox();
            guna2vScrollBar1 = new Guna2VScrollBar();
            dataGridView1 = new Guna2DataGridView();
            guna2ContextMenuStrip1 = new Guna2ContextMenuStrip();
            copyTooClipboardToolStripMenuItem = new ToolStripMenuItem();
            copyEntireRowToolStripMenuItem = new ToolStripMenuItem();
            clearSelectedRowToolStripMenuItem = new ToolStripMenuItem();
            clearAllToolStripMenuItem = new ToolStripMenuItem();
            lookupUsernameToolStripMenuItem = new ToolStripMenuItem();
            lookupUseridToolStripMenuItem = new ToolStripMenuItem();
            getRoomDataToolStripMenuItem = new ToolStripMenuItem();
            massSubBotToolStripMenuItem = new ToolStripMenuItem();
            massAddFriendToolStripMenuItem = new ToolStripMenuItem();
            massCheerToolStripMenuItem1 = new ToolStripMenuItem();
            takeBioToolStripMenuItem = new ToolStripMenuItem();
            checkPlayersBioToolStripMenuItem = new ToolStripMenuItem();
            pingCellToolStripMenuItem = new ToolStripMenuItem();
            massReportToolStripMenuItem = new ToolStripMenuItem();
            accountOptionsToolStripMenuItem = new ToolStripMenuItem();
            getYoureAccountInfoToolStripMenuItem = new ToolStripMenuItem();
            childrenAccountToolStripMenuItem = new ToolStripMenuItem();
            reportOptionsToolStripMenuItem = new ToolStripMenuItem();
            massReportTrollingToolStripMenuItem = new ToolStripMenuItem();
            massReportSexualToolStripMenuItem = new ToolStripMenuItem();
            massReportProfileToolStripMenuItem = new ToolStripMenuItem();
            massReportUnder13ToolStripMenuItem = new ToolStripMenuItem();
            massReportBanEvasionToolStripMenuItem = new ToolStripMenuItem();
            massReportDisruptiveTrollingToolStripMenuItem = new ToolStripMenuItem();
            massReportImageToolStripMenuItem = new ToolStripMenuItem();
            getResentPlayersToolStripMenuItem = new ToolStripMenuItem();
            timer1 = new System.Windows.Forms.Timer(components);
            panel1.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
            guna2ContextMenuStrip1.SuspendLayout();
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
            panel1.Size = new Size(711, 34);
            panel1.TabIndex = 9;
            // 
            // guna2ControlBox3
            // 
            guna2ControlBox3.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox3.ControlBoxType = ControlBoxType.MinimizeBox;
            guna2ControlBox3.CustomizableEdges = customizableEdges1;
            guna2ControlBox3.Dock = DockStyle.Right;
            guna2ControlBox3.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.IconColor = Color.White;
            guna2ControlBox3.Location = new Point(576, 0);
            guna2ControlBox3.Name = "guna2ControlBox3";
            guna2ControlBox3.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2ControlBox3.Size = new Size(45, 34);
            guna2ControlBox3.TabIndex = 5;
            // 
            // guna2ControlBox2
            // 
            guna2ControlBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox2.ControlBoxType = ControlBoxType.MaximizeBox;
            guna2ControlBox2.CustomizableEdges = customizableEdges3;
            guna2ControlBox2.Dock = DockStyle.Right;
            guna2ControlBox2.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.IconColor = Color.White;
            guna2ControlBox2.Location = new Point(621, 0);
            guna2ControlBox2.Name = "guna2ControlBox2";
            guna2ControlBox2.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2ControlBox2.Size = new Size(45, 34);
            guna2ControlBox2.TabIndex = 4;
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox1.CustomizableEdges = customizableEdges5;
            guna2ControlBox1.Dock = DockStyle.Right;
            guna2ControlBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(666, 0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            label1.Size = new Size(165, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF | Rec Room Tool";
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.CustomizableEdges = customizableEdges7;
            guna2TextBox1.DefaultText = "";
            guna2TextBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            guna2TextBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            guna2TextBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            guna2TextBox1.Dock = DockStyle.Bottom;
            guna2TextBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2TextBox1.ForeColor = Color.White;
            guna2TextBox1.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.Location = new Point(0, 390);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderForeColor = Color.White;
            guna2TextBox1.PlaceholderText = "Username";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges8;
            guna2TextBox1.Size = new Size(711, 36);
            guna2TextBox1.Style = TextBoxStyle.Material;
            guna2TextBox1.TabIndex = 15;
            guna2TextBox1.TextChanged += guna2TextBox1_TextChanged;
            // 
            // guna2vScrollBar1
            // 
            guna2vScrollBar1.BindingContainer = dataGridView1;
            guna2vScrollBar1.FillColor = Color.White;
            guna2vScrollBar1.InUpdate = false;
            guna2vScrollBar1.LargeChange = 10;
            guna2vScrollBar1.Location = new Point(693, 34);
            guna2vScrollBar1.Minimum = 1;
            guna2vScrollBar1.Name = "guna2vScrollBar1";
            guna2vScrollBar1.ScrollbarSize = 18;
            guna2vScrollBar1.Size = new Size(18, 350);
            guna2vScrollBar1.TabIndex = 14;
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
            dataGridView1.ContextMenuStrip = guna2ContextMenuStrip1;
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
            dataGridView1.Location = new Point(0, 34);
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
            dataGridView1.Size = new Size(711, 350);
            dataGridView1.TabIndex = 13;
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
            // guna2ContextMenuStrip1
            // 
            guna2ContextMenuStrip1.AllowDrop = true;
            guna2ContextMenuStrip1.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.BackgroundImageLayout = ImageLayout.None;
            guna2ContextMenuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyTooClipboardToolStripMenuItem, copyEntireRowToolStripMenuItem, clearSelectedRowToolStripMenuItem, clearAllToolStripMenuItem, pingCellToolStripMenuItem, accountOptionsToolStripMenuItem, reportOptionsToolStripMenuItem, getResentPlayersToolStripMenuItem });
            guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip1.RenderStyle.ArrowColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip1.RenderStyle.BorderColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip1.RenderStyle.RoundedEdges = false;
            guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = Color.Black;
            guna2ContextMenuStrip1.RenderStyle.SeparatorColor = Color.FromArgb(15, 15, 15);
            guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = TextRenderingHint.SystemDefault;
            guna2ContextMenuStrip1.ShowImageMargin = false;
            guna2ContextMenuStrip1.Size = new Size(163, 180);
            // 
            // copyTooClipboardToolStripMenuItem
            // 
            copyTooClipboardToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            copyTooClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            copyTooClipboardToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            copyTooClipboardToolStripMenuItem.ForeColor = Color.White;
            copyTooClipboardToolStripMenuItem.Name = "copyTooClipboardToolStripMenuItem";
            copyTooClipboardToolStripMenuItem.Size = new Size(162, 22);
            copyTooClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            copyTooClipboardToolStripMenuItem.TextImageRelation = TextImageRelation.Overlay;
            copyTooClipboardToolStripMenuItem.Click += copyTooClipboardToolStripMenuItem_Click;
            // 
            // copyEntireRowToolStripMenuItem
            // 
            copyEntireRowToolStripMenuItem.ForeColor = Color.White;
            copyEntireRowToolStripMenuItem.Name = "copyEntireRowToolStripMenuItem";
            copyEntireRowToolStripMenuItem.Size = new Size(162, 22);
            copyEntireRowToolStripMenuItem.Text = "Clear All";
            copyEntireRowToolStripMenuItem.Click += copyEntireRowToolStripMenuItem_Click;
            // 
            // clearSelectedRowToolStripMenuItem
            // 
            clearSelectedRowToolStripMenuItem.ForeColor = Color.White;
            clearSelectedRowToolStripMenuItem.Name = "clearSelectedRowToolStripMenuItem";
            clearSelectedRowToolStripMenuItem.Size = new Size(162, 22);
            clearSelectedRowToolStripMenuItem.Text = "Get Friends";
            clearSelectedRowToolStripMenuItem.Click += clearSelectedRowToolStripMenuItem_Click;
            // 
            // clearAllToolStripMenuItem
            // 
            clearAllToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            clearAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            clearAllToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { lookupUsernameToolStripMenuItem, lookupUseridToolStripMenuItem, getRoomDataToolStripMenuItem, massSubBotToolStripMenuItem, massAddFriendToolStripMenuItem, massCheerToolStripMenuItem1, takeBioToolStripMenuItem, checkPlayersBioToolStripMenuItem });
            clearAllToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clearAllToolStripMenuItem.ForeColor = Color.White;
            clearAllToolStripMenuItem.ImageScaling = ToolStripItemImageScaling.None;
            clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            clearAllToolStripMenuItem.Size = new Size(162, 22);
            clearAllToolStripMenuItem.Text = "Tool Lookup Options";
            // 
            // lookupUsernameToolStripMenuItem
            // 
            lookupUsernameToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            lookupUsernameToolStripMenuItem.ForeColor = Color.White;
            lookupUsernameToolStripMenuItem.ImageAlign = ContentAlignment.MiddleRight;
            lookupUsernameToolStripMenuItem.Name = "lookupUsernameToolStripMenuItem";
            lookupUsernameToolStripMenuItem.Size = new Size(175, 22);
            lookupUsernameToolStripMenuItem.Text = "Lookup Username";
            lookupUsernameToolStripMenuItem.Click += lookupUsernameToolStripMenuItem_Click;
            // 
            // lookupUseridToolStripMenuItem
            // 
            lookupUseridToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            lookupUseridToolStripMenuItem.ForeColor = Color.White;
            lookupUseridToolStripMenuItem.Name = "lookupUseridToolStripMenuItem";
            lookupUseridToolStripMenuItem.Size = new Size(175, 22);
            lookupUseridToolStripMenuItem.Text = "Lookup Userid";
            lookupUseridToolStripMenuItem.Click += lookupUseridToolStripMenuItem_Click;
            // 
            // getRoomDataToolStripMenuItem
            // 
            getRoomDataToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            getRoomDataToolStripMenuItem.ForeColor = Color.White;
            getRoomDataToolStripMenuItem.Name = "getRoomDataToolStripMenuItem";
            getRoomDataToolStripMenuItem.Size = new Size(175, 22);
            getRoomDataToolStripMenuItem.Text = "Get Room Data";
            getRoomDataToolStripMenuItem.Click += getRoomDataToolStripMenuItem_Click;
            // 
            // massSubBotToolStripMenuItem
            // 
            massSubBotToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massSubBotToolStripMenuItem.ForeColor = Color.White;
            massSubBotToolStripMenuItem.Name = "massSubBotToolStripMenuItem";
            massSubBotToolStripMenuItem.Size = new Size(175, 22);
            massSubBotToolStripMenuItem.Text = "Mass Sub Bot";
            massSubBotToolStripMenuItem.Click += massSubBotToolStripMenuItem_Click;
            // 
            // massAddFriendToolStripMenuItem
            // 
            massAddFriendToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massAddFriendToolStripMenuItem.ForeColor = Color.White;
            massAddFriendToolStripMenuItem.Name = "massAddFriendToolStripMenuItem";
            massAddFriendToolStripMenuItem.Overflow = ToolStripItemOverflow.Always;
            massAddFriendToolStripMenuItem.Size = new Size(175, 22);
            massAddFriendToolStripMenuItem.Text = "Mass Add Friend";
            massAddFriendToolStripMenuItem.TextImageRelation = TextImageRelation.ImageAboveText;
            massAddFriendToolStripMenuItem.Click += massAddFriendToolStripMenuItem_Click;
            // 
            // massCheerToolStripMenuItem1
            // 
            massCheerToolStripMenuItem1.BackColor = Color.FromArgb(25, 25, 25);
            massCheerToolStripMenuItem1.ForeColor = Color.White;
            massCheerToolStripMenuItem1.Name = "massCheerToolStripMenuItem1";
            massCheerToolStripMenuItem1.Size = new Size(175, 22);
            massCheerToolStripMenuItem1.Text = "Mass Cheer";
            massCheerToolStripMenuItem1.Click += massCheerToolStripMenuItem1_Click;
            // 
            // takeBioToolStripMenuItem
            // 
            takeBioToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            takeBioToolStripMenuItem.ForeColor = Color.White;
            takeBioToolStripMenuItem.Name = "takeBioToolStripMenuItem";
            takeBioToolStripMenuItem.Size = new Size(175, 22);
            takeBioToolStripMenuItem.Text = "Take Players Bio";
            takeBioToolStripMenuItem.Click += TakeBioToolStripMenuItem_Click;
            // 
            // checkPlayersBioToolStripMenuItem
            // 
            checkPlayersBioToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            checkPlayersBioToolStripMenuItem.ForeColor = Color.White;
            checkPlayersBioToolStripMenuItem.Name = "checkPlayersBioToolStripMenuItem";
            checkPlayersBioToolStripMenuItem.Size = new Size(175, 22);
            checkPlayersBioToolStripMenuItem.Text = "Check Players Bio";
            checkPlayersBioToolStripMenuItem.Click += checkPlayersBioToolStripMenuItem_Click;
            // 
            // pingCellToolStripMenuItem
            // 
            pingCellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { massReportToolStripMenuItem });
            pingCellToolStripMenuItem.ForeColor = Color.White;
            pingCellToolStripMenuItem.Name = "pingCellToolStripMenuItem";
            pingCellToolStripMenuItem.Size = new Size(162, 22);
            pingCellToolStripMenuItem.Text = "Misc Options";
            // 
            // massReportToolStripMenuItem
            // 
            massReportToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportToolStripMenuItem.ForeColor = Color.White;
            massReportToolStripMenuItem.Name = "massReportToolStripMenuItem";
            massReportToolStripMenuItem.Size = new Size(170, 22);
            massReportToolStripMenuItem.Text = "Check Ban Status";
            massReportToolStripMenuItem.Click += massReportToolStripMenuItem_Click;
            // 
            // accountOptionsToolStripMenuItem
            // 
            accountOptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { getYoureAccountInfoToolStripMenuItem, childrenAccountToolStripMenuItem });
            accountOptionsToolStripMenuItem.ForeColor = Color.White;
            accountOptionsToolStripMenuItem.Name = "accountOptionsToolStripMenuItem";
            accountOptionsToolStripMenuItem.Size = new Size(162, 22);
            accountOptionsToolStripMenuItem.Text = "Account Options";
            // 
            // getYoureAccountInfoToolStripMenuItem
            // 
            getYoureAccountInfoToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            getYoureAccountInfoToolStripMenuItem.ForeColor = Color.White;
            getYoureAccountInfoToolStripMenuItem.Name = "getYoureAccountInfoToolStripMenuItem";
            getYoureAccountInfoToolStripMenuItem.Size = new Size(208, 22);
            getYoureAccountInfoToolStripMenuItem.Text = "Get You're Account info";
            getYoureAccountInfoToolStripMenuItem.Click += getYoureAccountInfoToolStripMenuItem_Click;
            // 
            // childrenAccountToolStripMenuItem
            // 
            childrenAccountToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            childrenAccountToolStripMenuItem.ForeColor = Color.White;
            childrenAccountToolStripMenuItem.Name = "childrenAccountToolStripMenuItem";
            childrenAccountToolStripMenuItem.Size = new Size(208, 22);
            childrenAccountToolStripMenuItem.Text = "Children Account";
            childrenAccountToolStripMenuItem.Click += childrenAccountToolStripMenuItem_Click;
            // 
            // reportOptionsToolStripMenuItem
            // 
            reportOptionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { massReportTrollingToolStripMenuItem, massReportSexualToolStripMenuItem, massReportProfileToolStripMenuItem, massReportUnder13ToolStripMenuItem, massReportBanEvasionToolStripMenuItem, massReportDisruptiveTrollingToolStripMenuItem, massReportImageToolStripMenuItem });
            reportOptionsToolStripMenuItem.ForeColor = Color.White;
            reportOptionsToolStripMenuItem.Name = "reportOptionsToolStripMenuItem";
            reportOptionsToolStripMenuItem.Size = new Size(162, 22);
            reportOptionsToolStripMenuItem.Text = "Report Options";
            // 
            // massReportTrollingToolStripMenuItem
            // 
            massReportTrollingToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportTrollingToolStripMenuItem.ForeColor = Color.White;
            massReportTrollingToolStripMenuItem.Name = "massReportTrollingToolStripMenuItem";
            massReportTrollingToolStripMenuItem.Size = new Size(247, 22);
            massReportTrollingToolStripMenuItem.Text = "Mass Report Trolling";
            massReportTrollingToolStripMenuItem.Click += massReportTrollingToolStripMenuItem_Click;
            // 
            // massReportSexualToolStripMenuItem
            // 
            massReportSexualToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportSexualToolStripMenuItem.ForeColor = Color.White;
            massReportSexualToolStripMenuItem.Name = "massReportSexualToolStripMenuItem";
            massReportSexualToolStripMenuItem.Size = new Size(247, 22);
            massReportSexualToolStripMenuItem.Text = "Mass Report Sexual";
            massReportSexualToolStripMenuItem.Click += massReportSexualToolStripMenuItem_Click;
            // 
            // massReportProfileToolStripMenuItem
            // 
            massReportProfileToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportProfileToolStripMenuItem.ForeColor = Color.White;
            massReportProfileToolStripMenuItem.Name = "massReportProfileToolStripMenuItem";
            massReportProfileToolStripMenuItem.Size = new Size(247, 22);
            massReportProfileToolStripMenuItem.Text = "Mass Report Profile";
            massReportProfileToolStripMenuItem.Click += massReportProfileToolStripMenuItem_Click;
            // 
            // massReportUnder13ToolStripMenuItem
            // 
            massReportUnder13ToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportUnder13ToolStripMenuItem.ForeColor = Color.White;
            massReportUnder13ToolStripMenuItem.Name = "massReportUnder13ToolStripMenuItem";
            massReportUnder13ToolStripMenuItem.Size = new Size(247, 22);
            massReportUnder13ToolStripMenuItem.Text = "Mass Report Under 13";
            massReportUnder13ToolStripMenuItem.Click += massReportUnder13ToolStripMenuItem_Click;
            // 
            // massReportBanEvasionToolStripMenuItem
            // 
            massReportBanEvasionToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportBanEvasionToolStripMenuItem.ForeColor = Color.White;
            massReportBanEvasionToolStripMenuItem.Name = "massReportBanEvasionToolStripMenuItem";
            massReportBanEvasionToolStripMenuItem.Size = new Size(247, 22);
            massReportBanEvasionToolStripMenuItem.Text = "Mass Report Ban Evasion";
            massReportBanEvasionToolStripMenuItem.Click += massReportBanEvasionToolStripMenuItem_Click;
            // 
            // massReportDisruptiveTrollingToolStripMenuItem
            // 
            massReportDisruptiveTrollingToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportDisruptiveTrollingToolStripMenuItem.ForeColor = Color.White;
            massReportDisruptiveTrollingToolStripMenuItem.Name = "massReportDisruptiveTrollingToolStripMenuItem";
            massReportDisruptiveTrollingToolStripMenuItem.Size = new Size(247, 22);
            massReportDisruptiveTrollingToolStripMenuItem.Text = "Mass Report Disruptive trolling";
            massReportDisruptiveTrollingToolStripMenuItem.Click += massReportDisruptiveTrollingToolStripMenuItem_Click;
            // 
            // massReportImageToolStripMenuItem
            // 
            massReportImageToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportImageToolStripMenuItem.ForeColor = Color.White;
            massReportImageToolStripMenuItem.Name = "massReportImageToolStripMenuItem";
            massReportImageToolStripMenuItem.Size = new Size(247, 22);
            massReportImageToolStripMenuItem.Text = "Mass Report Image";
            massReportImageToolStripMenuItem.Click += massReportImageToolStripMenuItem_Click;
            // 
            // getResentPlayersToolStripMenuItem
            // 
            getResentPlayersToolStripMenuItem.ForeColor = Color.White;
            getResentPlayersToolStripMenuItem.Name = "getResentPlayersToolStripMenuItem";
            getResentPlayersToolStripMenuItem.Size = new Size(162, 22);
            getResentPlayersToolStripMenuItem.Text = "Get Recent Players";
            getResentPlayersToolStripMenuItem.Click += getResentPlayersToolStripMenuItem_Click;
            // 
            // timer1
            // 
            timer1.Interval = 2000;
            timer1.Tick += timer1_Tick;
            // 
            // Rec
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(711, 426);
            Controls.Add(guna2vScrollBar1);
            Controls.Add(guna2TextBox1);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Rec";
            Text = "f";
            Load += rec_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((ISupportInitialize)dataGridView1).EndInit();
            guna2ContextMenuStrip1.ResumeLayout(false);
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

        private ToolStripMenuItem getResentPlayersToolStripMenuItem;
    }
}