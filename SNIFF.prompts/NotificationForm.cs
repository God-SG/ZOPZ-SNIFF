using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SNIFF.Properties;

namespace SNIFF.prompts;

public class NotificationForm : Form
{
	private IContainer components;

	private Timer timer1;

	private Timer timer2;

	private Label lblMsg;

	private PictureBox pictureBox1;

	public NotificationForm()
	{
		InitializeComponent();
		ApplyBackgroundColor();
	}

	public void SetMessage(string message)
	{
		lblMsg.Text = message;
	}

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color = ColorTranslator.FromHtml(savedColor);
				pictureBox1.BackColor = color;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error applying background color: " + ex.Message);
			}
		}
	}

	private void NotificationForm_Load(object sender, EventArgs e)
	{
	}

	private void lblMsg_Click(object sender, EventArgs e)
	{
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		this.components = new System.ComponentModel.Container();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.lblMsg = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		base.SuspendLayout();
		this.lblMsg.Dock = System.Windows.Forms.DockStyle.Right;
		this.lblMsg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
		this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.lblMsg.ForeColor = System.Drawing.Color.White;
		this.lblMsg.Location = new System.Drawing.Point(46, 0);
		this.lblMsg.Name = "lblMsg";
		this.lblMsg.Size = new System.Drawing.Size(315, 55);
		this.lblMsg.TabIndex = 13;
		this.lblMsg.Text = "0";
		this.lblMsg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.lblMsg.Click += new System.EventHandler(lblMsg_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
		this.pictureBox1.ErrorImage = SNIFF.Properties.Resources.Info;
		this.pictureBox1.Image = SNIFF.Properties.Resources.Info4;
		this.pictureBox1.Location = new System.Drawing.Point(0, 0);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(45, 55);
		this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pictureBox1.TabIndex = 14;
		this.pictureBox1.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(361, 55);
		base.Controls.Add(this.pictureBox1);
		base.Controls.Add(this.lblMsg);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Name = "NotificationForm";
		this.Text = "NotificationForm";
		base.Load += new System.EventHandler(NotificationForm_Load);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		base.ResumeLayout(false);
	}
}
