using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class PacketAnalyzer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
     	private IContainer components = null;
        private TreeView packetTreeView;
        private Panel panel1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Label label1;
        private Guna2VScrollBar guna2vScrollBar1;
        private Guna2HScrollBar guna2hScrollBar1;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(PacketAnalyzer));
            packetTreeView = new TreeView();
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            guna2vScrollBar1 = new Guna2VScrollBar();
            guna2hScrollBar1 = new Guna2HScrollBar();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // packetTreeView
            // 
            packetTreeView.BackColor = Color.FromArgb(30, 30, 30);
            packetTreeView.BorderStyle = BorderStyle.None;
            packetTreeView.Dock = DockStyle.Bottom;
            packetTreeView.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            packetTreeView.ForeColor = Color.White;
            packetTreeView.Location = new Point(0, 40);
            packetTreeView.Name = "packetTreeView";
            packetTreeView.Size = new Size(711, 386);
            packetTreeView.TabIndex = 0;
            packetTreeView.NodeMouseDoubleClick += packetTreeView_NodeMouseDoubleClick;
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
            panel1.TabIndex = 3;
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
            guna2ControlBox3.Click += guna2ControlBox3_Click;
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
            label1.Size = new Size(171, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF | Packet Analyzer";
            // 
            // guna2vScrollBar1
            // 
            guna2vScrollBar1.BindingContainer = packetTreeView;
            guna2vScrollBar1.FillColor = Color.White;
            guna2vScrollBar1.InUpdate = false;
            guna2vScrollBar1.LargeChange = 10;
            guna2vScrollBar1.Location = new Point(693, 40);
            guna2vScrollBar1.Name = "guna2vScrollBar1";
            guna2vScrollBar1.ScrollbarSize = 18;
            guna2vScrollBar1.Size = new Size(18, 386);
            guna2vScrollBar1.TabIndex = 8;
            guna2vScrollBar1.ThumbColor = Color.FromArgb(25, 25, 25);
            // 
            // guna2hScrollBar1
            // 
            guna2hScrollBar1.BindingContainer = packetTreeView;
            guna2hScrollBar1.FillColor = Color.White;
            guna2hScrollBar1.InUpdate = false;
            guna2hScrollBar1.LargeChange = 10;
            guna2hScrollBar1.Location = new Point(0, 408);
            guna2hScrollBar1.Name = "guna2hScrollBar1";
            guna2hScrollBar1.ScrollbarSize = 18;
            guna2hScrollBar1.Size = new Size(711, 18);
            guna2hScrollBar1.TabIndex = 6;
            guna2hScrollBar1.ThumbColor = Color.FromArgb(25, 25, 25);
            // 
            // PacketAnalyzer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(711, 426);
            Controls.Add(guna2hScrollBar1);
            Controls.Add(guna2vScrollBar1);
            Controls.Add(panel1);
            Controls.Add(packetTreeView);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "PacketAnalyzer";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Packet Analyzer";
            Load += PacketAnalyzer_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}