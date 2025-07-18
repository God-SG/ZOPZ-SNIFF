using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class UserSentmessage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;
        private Label UsernameLbl;
        private Label MessageLbl;

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
            UsernameLbl = new Label();
            MessageLbl = new Label();
            guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(components);
            SuspendLayout();
            // 
            // UsernameLbl
            // 
            UsernameLbl.Dock = DockStyle.Top;
            UsernameLbl.FlatStyle = FlatStyle.Flat;
            UsernameLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            UsernameLbl.ForeColor = Color.White;
            UsernameLbl.Location = new Point(0, 0);
            UsernameLbl.Name = "UsernameLbl";
            UsernameLbl.Size = new Size(288, 27);
            UsernameLbl.TabIndex = 3;
            UsernameLbl.Text = "N/A";
            UsernameLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // MessageLbl
            // 
            MessageLbl.Dock = DockStyle.Fill;
            MessageLbl.FlatStyle = FlatStyle.Flat;
            MessageLbl.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            MessageLbl.ForeColor = Color.White;
            MessageLbl.Location = new Point(0, 27);
            MessageLbl.Name = "MessageLbl";
            MessageLbl.Size = new Size(288, 63);
            MessageLbl.TabIndex = 4;
            MessageLbl.Text = "N/A";
            // 
            // guna2Elipse1
            // 
            guna2Elipse1.BorderRadius = 30;
            guna2Elipse1.TargetControl = this;
            // 
            // UserSentmessage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            Controls.Add(MessageLbl);
            Controls.Add(UsernameLbl);
            Name = "UserSentmessage";
            Size = new Size(288, 90);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Elipse guna2Elipse1;
    }
}
