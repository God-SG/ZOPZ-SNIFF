using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class LoginForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Panel panel1;
        private Guna2ControlBox guna2ControlBox3;
        private Guna2ControlBox guna2ControlBox2;
        private Guna2ControlBox guna2ControlBox1;
        private Label label2;
        private Guna2DragControl guna2DragControl1;

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
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LoginForm));
            panel1 = new Panel();
            guna2ControlBox3 = new Guna2ControlBox();
            guna2ControlBox2 = new Guna2ControlBox();
            guna2ControlBox1 = new Guna2ControlBox();
            label2 = new Label();
            guna2DragControl1 = new Guna2DragControl(components);
            label1 = new Label();
            LoginBTN = new Guna2Button();
            guna2TextBox2 = new Guna2TextBox();
            Remebermecheck = new Guna2ToggleSwitch();
            guna2TextBox1 = new Guna2TextBox();
            guna2DragControl2 = new Guna2DragControl(components);
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(30, 30, 30);
            panel1.Controls.Add(guna2ControlBox3);
            panel1.Controls.Add(guna2ControlBox2);
            panel1.Controls.Add(guna2ControlBox1);
            panel1.Controls.Add(label2);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(542, 34);
            panel1.TabIndex = 6;
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
            guna2ControlBox3.Location = new Point(407, 0);
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
            guna2ControlBox2.Location = new Point(452, 0);
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
            guna2ControlBox1.Location = new Point(497, 0);
            guna2ControlBox1.Name = "guna2ControlBox1";
            guna2ControlBox1.ShadowDecoration.CustomizableEdges = customizableEdges6;
            guna2ControlBox1.Size = new Size(45, 34);
            guna2ControlBox1.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label2.ForeColor = Color.White;
            label2.Location = new Point(4, 9);
            label2.Name = "label2";
            label2.Size = new Size(112, 15);
            label2.TabIndex = 2;
            label2.Text = "ZOPZ SNIFF | Login";
            // 
            // guna2DragControl1
            // 
            guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl1.TargetControl = panel1;
            guna2DragControl1.TransparentWhileDrag = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(133, 202);
            label1.Name = "label1";
            label1.Size = new Size(80, 15);
            label1.TabIndex = 5;
            label1.Text = "Remeber me";
            // 
            // LoginBTN
            // 
            LoginBTN.BackColor = Color.FromArgb(30, 30, 30);
            LoginBTN.BorderColor = Color.FromArgb(30, 30, 30);
            LoginBTN.CustomizableEdges = customizableEdges7;
            LoginBTN.DisabledState.BorderColor = Color.DarkGray;
            LoginBTN.DisabledState.CustomBorderColor = Color.DarkGray;
            LoginBTN.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            LoginBTN.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            LoginBTN.FillColor = Color.FromArgb(30, 30, 30);
            LoginBTN.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LoginBTN.ForeColor = Color.White;
            LoginBTN.Location = new Point(80, 234);
            LoginBTN.Name = "LoginBTN";
            LoginBTN.ShadowDecoration.CustomizableEdges = customizableEdges8;
            LoginBTN.Size = new Size(360, 36);
            LoginBTN.TabIndex = 2;
            LoginBTN.Text = "Login";
            LoginBTN.Click += guna2Button1_Click;
            // 
            // guna2TextBox2
            // 
            guna2TextBox2.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox2.CustomizableEdges = customizableEdges9;
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
            guna2TextBox2.Location = new Point(80, 150);
            guna2TextBox2.Name = "guna2TextBox2";
            guna2TextBox2.PasswordChar = '*';
            guna2TextBox2.PlaceholderForeColor = Color.White;
            guna2TextBox2.PlaceholderText = "Password";
            guna2TextBox2.SelectedText = "";
            guna2TextBox2.ShadowDecoration.CustomizableEdges = customizableEdges10;
            guna2TextBox2.Size = new Size(360, 36);
            guna2TextBox2.Style = TextBoxStyle.Material;
            guna2TextBox2.TabIndex = 3;
            // 
            // Remebermecheck
            // 
            Remebermecheck.CheckedState.BorderColor = Color.FromArgb(25, 25, 25);
            Remebermecheck.CheckedState.FillColor = Color.FromArgb(25, 25, 25);
            Remebermecheck.CheckedState.InnerBorderColor = Color.White;
            Remebermecheck.CheckedState.InnerColor = Color.White;
            Remebermecheck.CustomizableEdges = customizableEdges11;
            Remebermecheck.Location = new Point(80, 199);
            Remebermecheck.Name = "Remebermecheck";
            Remebermecheck.ShadowDecoration.CustomizableEdges = customizableEdges12;
            Remebermecheck.Size = new Size(35, 20);
            Remebermecheck.TabIndex = 4;
            Remebermecheck.UncheckedState.BorderColor = Color.FromArgb(25, 25, 25);
            Remebermecheck.UncheckedState.FillColor = Color.FromArgb(25, 25, 25);
            Remebermecheck.UncheckedState.InnerBorderColor = Color.White;
            Remebermecheck.UncheckedState.InnerColor = Color.White;
            Remebermecheck.CheckedChanged += Remebermecheck_CheckedChanged;
            // 
            // guna2TextBox1
            // 
            guna2TextBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.BorderColor = Color.FromArgb(30, 30, 30);
            guna2TextBox1.CustomizableEdges = customizableEdges13;
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
            guna2TextBox1.Location = new Point(80, 83);
            guna2TextBox1.Name = "guna2TextBox1";
            guna2TextBox1.PasswordChar = '\0';
            guna2TextBox1.PlaceholderForeColor = Color.White;
            guna2TextBox1.PlaceholderText = "Username";
            guna2TextBox1.SelectedText = "";
            guna2TextBox1.ShadowDecoration.CustomizableEdges = customizableEdges14;
            guna2TextBox1.Size = new Size(360, 36);
            guna2TextBox1.Style = TextBoxStyle.Material;
            guna2TextBox1.TabIndex = 0;
            // 
            // guna2DragControl2
            // 
            guna2DragControl2.DockIndicatorTransparencyValue = 0.6D;
            guna2DragControl2.UseTransparentDrag = true;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(542, 351);
            Controls.Add(panel1);
            Controls.Add(label1);
            Controls.Add(Remebermecheck);
            Controls.Add(guna2TextBox2);
            Controls.Add(LoginBTN);
            Controls.Add(guna2TextBox1);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "LoginForm";
            StartPosition = FormStartPosition.CenterScreen;
            Load += LoginForm_Load;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Guna2Button LoginBTN;
        private Guna2TextBox guna2TextBox2;
        private Guna2ToggleSwitch Remebermecheck;
        private Guna2TextBox guna2TextBox1;
        private Guna2DragControl guna2DragControl2;
    }
}