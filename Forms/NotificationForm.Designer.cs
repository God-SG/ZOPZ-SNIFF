using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using System.ComponentModel;

namespace ZOPZ_SNIFF.Menus
{
    partial class NotificationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
    	private IContainer components = null;
        private Label lblMsg;
        private Guna2PictureBox guna2PictureBox1;

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
            ComponentResourceManager resources = new ComponentResourceManager(typeof(NotificationForm));
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            lblMsg = new Label();
            guna2PictureBox1 = new Guna2PictureBox();
            ((ISupportInitialize)guna2PictureBox1).BeginInit();
            SuspendLayout();
            // 
            // lblMsg
            // 
            lblMsg.Dock = DockStyle.Right;
            lblMsg.FlatStyle = FlatStyle.Flat;
            lblMsg.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblMsg.ForeColor = Color.White;
            lblMsg.Location = new Point(46, 0);
            lblMsg.Name = "lblMsg";
            lblMsg.Size = new Size(315, 55);
            lblMsg.TabIndex = 3;
            lblMsg.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // guna2PictureBox1
            // 
            guna2PictureBox1.BackColor = Color.FromArgb(30, 30, 30);
            guna2PictureBox1.BackgroundImage = (Image)resources.GetObject("guna2PictureBox1.BackgroundImage");
            guna2PictureBox1.BackgroundImageLayout = ImageLayout.Center;
            guna2PictureBox1.CustomizableEdges = customizableEdges1;
            guna2PictureBox1.Dock = DockStyle.Fill;
            guna2PictureBox1.ErrorImage = (Image)resources.GetObject("guna2PictureBox1.ErrorImage");
            guna2PictureBox1.FillColor = Color.Transparent;
            guna2PictureBox1.ImageRotate = 0F;
            guna2PictureBox1.Location = new Point(0, 0);
            guna2PictureBox1.Name = "guna2PictureBox1";
            guna2PictureBox1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2PictureBox1.Size = new Size(46, 55);
            guna2PictureBox1.SizeMode = PictureBoxSizeMode.CenterImage;
            guna2PictureBox1.TabIndex = 4;
            guna2PictureBox1.TabStop = false;
            // 
            // NotificationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(25, 25, 25);
            ClientSize = new Size(361, 55);
            Controls.Add(guna2PictureBox1);
            Controls.Add(lblMsg);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "NotificationForm";
            Text = "Alert";
            Load += NotificationForm_Load;
            ((ISupportInitialize)guna2PictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
    }
}