using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;
using System.Drawing.Text;

namespace ZOPZ_SNIFF.Menus
{
    partial class Xboxpartyoptions
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
        private Guna2ContextMenuStrip guna2ContextMenuStrip1;
        private ToolStripMenuItem copyTooClipboardToolStripMenuItem;
        private ToolStripMenuItem copyEntireRowToolStripMenuItem;
        private ToolStripMenuItem clearAllToolStripMenuItem;
        private ToolStripMenuItem clearSelectedRowToolStripMenuItem;
        private ToolStripMenuItem pingCellToolStripMenuItem;
        private Guna2VScrollBar guna2vScrollBar1;
        private Guna2DataGridView dataGridView1;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column3;
        private ToolStripMenuItem openPartyToolStripMenuItem;
        private ToolStripMenuItem closePartyToolStripMenuItem;
        private ToolStripMenuItem partyStatusToolStripMenuItem;
        private ToolStripMenuItem crashPartyHostToolStripMenuItem;
        private ToolStripMenuItem massReportToolStripMenuItem;
        private ToolStripMenuItem grabMyPartyToolStripMenuItem;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Xboxpartyoptions));
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            guna2ContextMenuStrip1 = new Guna2ContextMenuStrip();
            copyTooClipboardToolStripMenuItem = new ToolStripMenuItem();
            copyEntireRowToolStripMenuItem = new ToolStripMenuItem();
            clearSelectedRowToolStripMenuItem = new ToolStripMenuItem();
            grabMyPartyToolStripMenuItem = new ToolStripMenuItem();
            openPartyToolStripMenuItem = new ToolStripMenuItem();
            closePartyToolStripMenuItem = new ToolStripMenuItem();
            partyStatusToolStripMenuItem = new ToolStripMenuItem();
            crashPartyHostToolStripMenuItem = new ToolStripMenuItem();
            clearAllToolStripMenuItem = new ToolStripMenuItem();
            pingCellToolStripMenuItem = new ToolStripMenuItem();
            massReportToolStripMenuItem = new ToolStripMenuItem();
            guna2vScrollBar1 = new Guna2VScrollBar();
            dataGridView1 = new Guna2DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            panel1.SuspendLayout();
            guna2ContextMenuStrip1.SuspendLayout();
            ((ISupportInitialize)dataGridView1).BeginInit();
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
            label1.Size = new Size(137, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF | Xbox Tool";
            // 
            // guna2ContextMenuStrip1
            // 
            guna2ContextMenuStrip1.AllowDrop = true;
            guna2ContextMenuStrip1.BackColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            guna2ContextMenuStrip1.Items.AddRange(new ToolStripItem[] { copyTooClipboardToolStripMenuItem, copyEntireRowToolStripMenuItem, clearSelectedRowToolStripMenuItem, clearAllToolStripMenuItem, pingCellToolStripMenuItem });
            guna2ContextMenuStrip1.Name = "guna2ContextMenuStrip1";
            guna2ContextMenuStrip1.RenderStyle.ArrowColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip1.RenderStyle.BorderColor = Color.FromArgb(25, 25, 25);
            guna2ContextMenuStrip1.RenderStyle.ColorTable = null;
            guna2ContextMenuStrip1.RenderStyle.RoundedEdges = false;
            guna2ContextMenuStrip1.RenderStyle.SelectionArrowColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SelectionBackColor = Color.FromArgb(30, 30, 30);
            guna2ContextMenuStrip1.RenderStyle.SelectionForeColor = Color.White;
            guna2ContextMenuStrip1.RenderStyle.SeparatorColor = Color.FromArgb(15, 15, 15);
            guna2ContextMenuStrip1.RenderStyle.TextRenderingHint = TextRenderingHint.SystemDefault;
            guna2ContextMenuStrip1.ShowImageMargin = false;
            guna2ContextMenuStrip1.Size = new Size(161, 114);
            // 
            // copyTooClipboardToolStripMenuItem
            // 
            copyTooClipboardToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            copyTooClipboardToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            copyTooClipboardToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            copyTooClipboardToolStripMenuItem.ForeColor = Color.White;
            copyTooClipboardToolStripMenuItem.Name = "copyTooClipboardToolStripMenuItem";
            copyTooClipboardToolStripMenuItem.Size = new Size(160, 22);
            copyTooClipboardToolStripMenuItem.Text = "Copy To Clipboard";
            copyTooClipboardToolStripMenuItem.TextImageRelation = TextImageRelation.Overlay;
            // 
            // copyEntireRowToolStripMenuItem
            // 
            copyEntireRowToolStripMenuItem.ForeColor = Color.White;
            copyEntireRowToolStripMenuItem.Name = "copyEntireRowToolStripMenuItem";
            copyEntireRowToolStripMenuItem.Size = new Size(160, 22);
            copyEntireRowToolStripMenuItem.Text = "Clear All";
            copyEntireRowToolStripMenuItem.Click += copyEntireRowToolStripMenuItem_Click;
            // 
            // clearSelectedRowToolStripMenuItem
            // 
            clearSelectedRowToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { grabMyPartyToolStripMenuItem, openPartyToolStripMenuItem, closePartyToolStripMenuItem, partyStatusToolStripMenuItem, crashPartyHostToolStripMenuItem });
            clearSelectedRowToolStripMenuItem.ForeColor = Color.White;
            clearSelectedRowToolStripMenuItem.Name = "clearSelectedRowToolStripMenuItem";
            clearSelectedRowToolStripMenuItem.Size = new Size(160, 22);
            clearSelectedRowToolStripMenuItem.Text = "Party Options";
            // 
            // grabMyPartyToolStripMenuItem
            // 
            grabMyPartyToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            grabMyPartyToolStripMenuItem.ForeColor = Color.White;
            grabMyPartyToolStripMenuItem.Name = "grabMyPartyToolStripMenuItem";
            grabMyPartyToolStripMenuItem.Size = new Size(165, 22);
            grabMyPartyToolStripMenuItem.Text = "Grab My Party";
            grabMyPartyToolStripMenuItem.Click += grabMyPartyToolStripMenuItem_Click;
            // 
            // openPartyToolStripMenuItem
            // 
            openPartyToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            openPartyToolStripMenuItem.ForeColor = Color.White;
            openPartyToolStripMenuItem.Name = "openPartyToolStripMenuItem";
            openPartyToolStripMenuItem.Size = new Size(165, 22);
            openPartyToolStripMenuItem.Text = "Open Party";
            openPartyToolStripMenuItem.Click += openPartyToolStripMenuItem_Click;
            // 
            // closePartyToolStripMenuItem
            // 
            closePartyToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            closePartyToolStripMenuItem.ForeColor = Color.White;
            closePartyToolStripMenuItem.Name = "closePartyToolStripMenuItem";
            closePartyToolStripMenuItem.Size = new Size(165, 22);
            closePartyToolStripMenuItem.Text = "Close Party";
            closePartyToolStripMenuItem.Click += closePartyToolStripMenuItem_Click;
            // 
            // partyStatusToolStripMenuItem
            // 
            partyStatusToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            partyStatusToolStripMenuItem.ForeColor = Color.White;
            partyStatusToolStripMenuItem.Name = "partyStatusToolStripMenuItem";
            partyStatusToolStripMenuItem.Size = new Size(165, 22);
            partyStatusToolStripMenuItem.Text = "Party Status";
            partyStatusToolStripMenuItem.Click += partyStatusToolStripMenuItem_Click;
            // 
            // crashPartyHostToolStripMenuItem
            // 
            crashPartyHostToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            crashPartyHostToolStripMenuItem.ForeColor = Color.White;
            crashPartyHostToolStripMenuItem.Name = "crashPartyHostToolStripMenuItem";
            crashPartyHostToolStripMenuItem.Size = new Size(165, 22);
            crashPartyHostToolStripMenuItem.Text = "Crash Party Host";
            crashPartyHostToolStripMenuItem.Click += crashPartyHostToolStripMenuItem_Click;
            // 
            // clearAllToolStripMenuItem
            // 
            clearAllToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            clearAllToolStripMenuItem.DisplayStyle = ToolStripItemDisplayStyle.Text;
            clearAllToolStripMenuItem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            clearAllToolStripMenuItem.ForeColor = Color.White;
            clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
            clearAllToolStripMenuItem.Size = new Size(160, 22);
            clearAllToolStripMenuItem.Text = "Become Unkickable";
            clearAllToolStripMenuItem.Click += clearAllToolStripMenuItem_Click;
            // 
            // pingCellToolStripMenuItem
            // 
            pingCellToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { massReportToolStripMenuItem });
            pingCellToolStripMenuItem.ForeColor = Color.White;
            pingCellToolStripMenuItem.Name = "pingCellToolStripMenuItem";
            pingCellToolStripMenuItem.Size = new Size(160, 22);
            pingCellToolStripMenuItem.Text = "Misc Options";
            // 
            // massReportToolStripMenuItem
            // 
            massReportToolStripMenuItem.BackColor = Color.FromArgb(25, 25, 25);
            massReportToolStripMenuItem.ForeColor = Color.White;
            massReportToolStripMenuItem.Name = "massReportToolStripMenuItem";
            massReportToolStripMenuItem.Size = new Size(143, 22);
            massReportToolStripMenuItem.Text = "Mass Report";
            massReportToolStripMenuItem.Click += massReportToolStripMenuItem_Click;
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
            guna2vScrollBar1.Size = new Size(18, 392);
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
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { Column1, Column2, Column3 });
            dataGridView1.ContextMenuStrip = guna2ContextMenuStrip1;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = Color.FromArgb(30, 30, 30);
            dataGridViewCellStyle3.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGridViewCellStyle3.ForeColor = Color.White;
            dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(25, 25, 25);
            dataGridViewCellStyle3.SelectionForeColor = Color.White;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
            dataGridView1.Dock = DockStyle.Fill;
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
            dataGridView1.Size = new Size(711, 392);
            dataGridView1.TabIndex = 12;
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
            // Column1
            // 
            Column1.HeaderText = "Gamertag";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.Resizable = DataGridViewTriState.False;
            // 
            // Column2
            // 
            Column2.HeaderText = "Role";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.Resizable = DataGridViewTriState.False;
            // 
            // Column3
            // 
            Column3.HeaderText = "Xuid";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.Resizable = DataGridViewTriState.False;
            // 
            // Xboxpartyoptions
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(711, 426);
            Controls.Add(guna2vScrollBar1);
            Controls.Add(dataGridView1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Xboxpartyoptions";
            Text = "ZOPZ SNIFF";
            Load += xboxpartyoptions_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            guna2ContextMenuStrip1.ResumeLayout(false);
            ((ISupportInitialize)dataGridView1).EndInit();
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
                get => Color.FromArgb(30, 30, 30);
            }
            public override Color MenuItemPressedGradientEnd
            {
                get => Color.FromArgb(30, 30, 30);
            }
        }
    }
}