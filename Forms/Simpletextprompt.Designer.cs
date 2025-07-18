using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using Guna.UI2.WinForms;
using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class Simpletextprompt
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Guna2TextBox textBox1;
        private Panel panel1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Label label1;

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
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(Simpletextprompt));
            textBox1 = new Guna2TextBox();
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label1 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.FromArgb(30, 30, 30);
            textBox1.BorderColor = Color.FromArgb(30, 30, 30);
            textBox1.CustomizableEdges = customizableEdges1;
            textBox1.DefaultText = "";
            textBox1.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            textBox1.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            textBox1.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            textBox1.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            textBox1.FillColor = Color.FromArgb(30, 30, 30);
            textBox1.FocusedState.BorderColor = Color.FromArgb(30, 30, 30);
            textBox1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            textBox1.ForeColor = Color.White;
            textBox1.HoverState.BorderColor = Color.FromArgb(30, 30, 30);
            textBox1.Location = new Point(32, 72);
            textBox1.Name = "textBox1";
            textBox1.PasswordChar = '\0';
            textBox1.PlaceholderForeColor = Color.White;
            textBox1.PlaceholderText = "";
            textBox1.SelectedText = "";
            textBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            textBox1.Size = new Size(235, 36);
            textBox1.Style = TextBoxStyle.Material;
            textBox1.TabIndex = 1;
            textBox1.KeyDown += textBox1_KeyDown;
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
            panel1.Size = new Size(312, 34);
            panel1.TabIndex = 9;
            // 
            // guna2ControlBox3
            // 
            guna2ControlBox3.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox3.ControlBoxType = ControlBoxType.MinimizeBox;
            guna2ControlBox3.CustomizableEdges = customizableEdges3;
            guna2ControlBox3.Dock = DockStyle.Right;
            guna2ControlBox3.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox3.IconColor = Color.White;
            guna2ControlBox3.Location = new Point(177, 0);
            guna2ControlBox3.Name = "guna2ControlBox3";
            guna2ControlBox3.ShadowDecoration.CustomizableEdges = customizableEdges4;
            guna2ControlBox3.Size = new Size(45, 34);
            guna2ControlBox3.TabIndex = 5;
            // 
            // guna2ControlBox2
            // 
            guna2ControlBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox2.ControlBoxType = ControlBoxType.MaximizeBox;
            guna2ControlBox2.CustomizableEdges = customizableEdges5;
            guna2ControlBox2.Dock = DockStyle.Right;
            guna2ControlBox2.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox2.IconColor = Color.White;
            guna2ControlBox2.Location = new Point(222, 0);
            guna2ControlBox2.Name = "guna2ControlBox2";
            guna2ControlBox2.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2ControlBox2.Size = new Size(45, 34);
            guna2ControlBox2.TabIndex = 4;
            // 
            // guna2ControlBox1
            // 
            guna2ControlBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.ControlBoxStyle = ControlBoxStyle.Custom;
            guna2ControlBox1.CustomizableEdges = customizableEdges7;
            guna2ControlBox1.Dock = DockStyle.Right;
            guna2ControlBox1.FillColor = Color.FromArgb(30, 30, 30);
            guna2ControlBox1.IconColor = Color.White;
            guna2ControlBox1.Location = new Point(267, 0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            label1.Size = new Size(110, 15);
            label1.TabIndex = 2;
            label1.Text = "ZOPZ SNIFF | Alert";
            // 
            // Simpletextprompt
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(312, 164);
            Controls.Add(panel1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Simpletextprompt";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Alert";
            Load += Simpletextprompt_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
    }
}