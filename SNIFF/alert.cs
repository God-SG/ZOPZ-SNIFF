using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace SNIFF;

public class alert : Form
{
	public enum enmAction
	{
		wait,
		start,
		close
	}

	public enum enmType
	{
		Success,
		Warning,
		Error,
		Info
	}

	private enmAction action;

	private int x;

	private int y;

	private IContainer components;

	private Label label19;

	private Timer timer1;

	private Label lblMsg;

	private Guna2Elipse guna2Elipse13;

	public void showAlert(string msg, enmType type)
	{
		base.Opacity = 0.0;
		base.StartPosition = FormStartPosition.Manual;
		for (int i = 1; i < 10; i++)
		{
			string fname = "alert" + i;
			if ((alert)Application.OpenForms[fname] == null)
			{
				base.Name = fname;
				x = Screen.PrimaryScreen.WorkingArea.Width - base.Width + 15;
				y = Screen.PrimaryScreen.WorkingArea.Height - base.Height * i - 5 * i;
				base.Location = new Point(x, y);
				break;
			}
		}
		x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;
		switch (type)
		{
		case enmType.Success:
			BackColor = Color.Black;
			break;
		case enmType.Error:
			BackColor = Color.Black;
			break;
		case enmType.Info:
			BackColor = Color.Black;
			break;
		case enmType.Warning:
			BackColor = Color.Black;
			break;
		}
		lblMsg.Text = msg;
		Show();
		action = enmAction.start;
		timer1.Interval = 1;
		timer1.Start();
	}

	public alert()
	{
		InitializeComponent();
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
		switch (action)
		{
		case enmAction.wait:
			timer1.Interval = 5000;
			action = enmAction.close;
			break;
		case enmAction.start:
			timer1.Interval = 1;
			base.Opacity += 0.1;
			if (x < base.Location.X)
			{
				base.Left--;
			}
			else if (base.Opacity == 1.0)
			{
				action = enmAction.wait;
			}
			break;
		case enmAction.close:
			timer1.Interval = 1;
			base.Opacity -= 0.1;
			base.Left -= 3;
			if (base.Opacity == 0.0)
			{
				Close();
			}
			break;
		}
	}

	private void label19_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.alert));
		this.label19 = new System.Windows.Forms.Label();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.lblMsg = new System.Windows.Forms.Label();
		this.guna2Elipse13 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		base.SuspendLayout();
		this.label19.Dock = System.Windows.Forms.DockStyle.Top;
		this.label19.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.label19.ForeColor = System.Drawing.Color.White;
		this.label19.Location = new System.Drawing.Point(0, 0);
		this.label19.Name = "label19";
		this.label19.Size = new System.Drawing.Size(383, 20);
		this.label19.TabIndex = 10;
		this.label19.Text = "ZOPZ SNIFF";
		this.label19.Click += new System.EventHandler(label19_Click);
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.lblMsg.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.lblMsg.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.lblMsg.ForeColor = System.Drawing.Color.White;
		this.lblMsg.Location = new System.Drawing.Point(0, 20);
		this.lblMsg.Name = "lblMsg";
		this.lblMsg.Size = new System.Drawing.Size(383, 116);
		this.lblMsg.TabIndex = 11;
		this.lblMsg.Text = "lol";
		this.lblMsg.Click += new System.EventHandler(lblMsg_Click);
		this.guna2Elipse13.BorderRadius = 10;
		this.guna2Elipse13.TargetControl = this;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(383, 136);
		base.Controls.Add(this.label19);
		base.Controls.Add(this.lblMsg);
		this.ForeColor = System.Drawing.Color.White;
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "alert";
		this.Text = "ZOPZ SNIFF";
		base.ResumeLayout(false);
	}
}
