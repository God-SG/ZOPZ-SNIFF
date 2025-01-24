using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SNIFF;

public class LoadingForm : Form
{
	public RoundProgressBar roundProgressBar;

	private IContainer components;

	public LoadingForm()
	{
		InitializeComponent();
		base.StartPosition = FormStartPosition.CenterScreen;
		base.FormBorderStyle = FormBorderStyle.None;
		base.ShowInTaskbar = false;
		InitializeRoundProgressBar();
	}

	private void InitializeRoundProgressBar()
	{
		roundProgressBar = new RoundProgressBar
		{
			Size = new Size(100, 100),
			Location = new Point((base.ClientSize.Width - 100) / 2, (base.ClientSize.Height - 100) / 2),
			Progress = 0
		};
		base.Controls.Add(roundProgressBar);
		base.StartPosition = FormStartPosition.CenterScreen;
		base.FormBorderStyle = FormBorderStyle.None;
		base.ShowInTaskbar = false;
	}

	private void LoadingForm_Load(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.LoadingForm));
		base.SuspendLayout();
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(354, 173);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "LoadingForm";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "g";
		base.TopMost = true;
		base.Load += new System.EventHandler(LoadingForm_Load);
		base.ResumeLayout(false);
	}
}
