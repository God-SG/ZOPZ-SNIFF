using System.ComponentModel;

namespace ZOPZ_SNIFF.UserControls
{
    partial class FIlterDisplay
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Label MessageLbl;
        private Label UsernameLbl;

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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new Container();
            MessageLbl = new Label();
            UsernameLbl = new Label();
            label1 = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            SuspendLayout();
            // 
            // MessageLbl
            // 
            MessageLbl.BackColor = Color.Transparent;
            MessageLbl.Dock = DockStyle.Left;
            MessageLbl.FlatStyle = FlatStyle.Flat;
            MessageLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            MessageLbl.ForeColor = Color.White;
            MessageLbl.Location = new Point(0, 0);
            MessageLbl.Name = "MessageLbl";
            MessageLbl.Size = new Size(246, 49);
            MessageLbl.TabIndex = 6;
            MessageLbl.Text = "N/A";
            MessageLbl.TextAlign = ContentAlignment.MiddleLeft;
            MessageLbl.Click += UsernameLbl_Click;
            // 
            // UsernameLbl
            // 
            UsernameLbl.BackColor = Color.Transparent;
            UsernameLbl.Dock = DockStyle.Right;
            UsernameLbl.FlatStyle = FlatStyle.Flat;
            UsernameLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            UsernameLbl.ForeColor = Color.White;
            UsernameLbl.Location = new Point(398, 0);
            UsernameLbl.Name = "UsernameLbl";
            UsernameLbl.Size = new Size(282, 49);
            UsernameLbl.TabIndex = 5;
            UsernameLbl.Text = "N/A";
            UsernameLbl.TextAlign = ContentAlignment.MiddleLeft;
            UsernameLbl.Click += UsernameLbl_Click;
            // 
            // label1
            // 
            label1.BackColor = Color.FromArgb(32, 32, 32);
            label1.Dock = DockStyle.Left;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(246, 0);
            label1.Name = "label1";
            label1.Size = new Size(103, 49);
            label1.TabIndex = 7;
            label1.Text = "N/A";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 30;
            guna2Elipse1.TargetControl = label1;
            // 
            // FIlterDisplay
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 30, 30);
            Controls.Add(label1);
            Controls.Add(UsernameLbl);
            Controls.Add(MessageLbl);
            Margin = new Padding(0);
            Name = "FIlterDisplay";
            Size = new Size(680, 49);
            Click += UsernameLbl_Click;
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
