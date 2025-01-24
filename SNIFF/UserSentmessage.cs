using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using SNIFF.Properties;

namespace SNIFF;

public class UserSentmessage : UserControl
{
	private IContainer components;

	private Label UsernameLbl;

	private Label MessageLbl;

	public UserSentmessage(string poster, string message)
	{
		InitializeComponent();
		ApplyBackgroundColor();
		UsernameLbl.Text = poster;
		MessageLbl.Text = message;
	}

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color = ColorTranslator.FromHtml(savedColor);
				UsernameLbl.BackColor = color;
				MessageLbl.BackColor = color;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error applying background color: " + ex.Message);
			}
		}
	}

	private void UsernameLbl_Click(object sender, EventArgs e)
	{
	}

	private void MessageLbl_Click(object sender, EventArgs e)
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
		this.UsernameLbl = new System.Windows.Forms.Label();
		this.MessageLbl = new System.Windows.Forms.Label();
		base.SuspendLayout();
		this.UsernameLbl.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.UsernameLbl.Dock = System.Windows.Forms.DockStyle.Top;
		this.UsernameLbl.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.UsernameLbl.ForeColor = System.Drawing.Color.White;
		this.UsernameLbl.Location = new System.Drawing.Point(0, 0);
		this.UsernameLbl.Name = "UsernameLbl";
		this.UsernameLbl.Size = new System.Drawing.Size(288, 27);
		this.UsernameLbl.TabIndex = 154;
		this.UsernameLbl.Text = "God";
		this.UsernameLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.UsernameLbl.Click += new System.EventHandler(UsernameLbl_Click);
		this.MessageLbl.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.MessageLbl.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.MessageLbl.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.MessageLbl.ForeColor = System.Drawing.Color.White;
		this.MessageLbl.Location = new System.Drawing.Point(0, 27);
		this.MessageLbl.Name = "MessageLbl";
		this.MessageLbl.Size = new System.Drawing.Size(288, 63);
		this.MessageLbl.TabIndex = 155;
		this.MessageLbl.Text = "Nuh uh";
		this.MessageLbl.Click += new System.EventHandler(MessageLbl_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		base.Controls.Add(this.MessageLbl);
		base.Controls.Add(this.UsernameLbl);
		base.Name = "UserSentmessage";
		base.Size = new System.Drawing.Size(288, 90);
		base.ResumeLayout(false);
	}
}
