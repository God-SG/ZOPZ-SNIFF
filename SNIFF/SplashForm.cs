using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using SNIFF.Classes.Auth.Models;

namespace SNIFF;

public class SplashForm : Form
{
	private string username;

	private string password;

	private bool willDo;

	private IContainer components;

	private Timer timer1;

	private Guna2BorderlessForm guna2BorderlessForm1;

	private System.Windows.Forms.Label label4;

	private Guna2DragControl guna2DragControl1;

	private Guna2Elipse guna2Elipse13;

	public SplashForm()
	{
		SettingsModel settings = SettingsManager.Load();
		username = settings.Username;
		password = settings.Password;
		willDo = settings.AutoLogin;
		InitializeComponent();
	}

	private void SplashForm_Load(object sender, EventArgs e)
	{
		timer1.Interval = 1000;
		timer1.Start();
	}

	private void SplashForm_Shown(object sender, EventArgs e)
	{
	}

	private async Task Loadlogs()
	{
		//Global.Labels = (await Global.Authenticator.GetLabelsAsync()).Data.OrderBy((SNIFF.Classes.Auth.Models.Label x) => x.Name).ToList();
	}

	private async void timer1_Tick(object sender, EventArgs e)
	{
		timer1.Stop();
		if (!willDo)
		{
			login obj = new login();
			Hide();
			obj.Show();
			return;
		}
		new login().Show();
		Hide();
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
            this.guna2BorderlessForm1 = new Guna.UI2.WinForms.Guna2BorderlessForm(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Elipse13 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // guna2BorderlessForm1
            // 
            this.guna2BorderlessForm1.AnimateWindow = true;
            this.guna2BorderlessForm1.BorderRadius = 2;
            this.guna2BorderlessForm1.ContainerControl = this;
            this.guna2BorderlessForm1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2BorderlessForm1.DragForm = false;
            this.guna2BorderlessForm1.ResizeForm = false;
            this.guna2BorderlessForm1.TransparentWhileDrag = true;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(941, 382);
            this.label4.TabIndex = 153;
            this.label4.Text = "Discord.gg/ShadowGarden";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TargetControl = this.label4;
            this.guna2DragControl1.TransparentWhileDrag = false;
            // 
            // guna2Elipse13
            // 
            this.guna2Elipse13.BorderRadius = 10;
            this.guna2Elipse13.TargetControl = this;
            // 
            // SplashForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(941, 382);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SplashForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZOPZ SNIFF";
            this.Load += new System.EventHandler(this.SplashForm_Load);
            this.Shown += new System.EventHandler(this.SplashForm_Shown);
            this.ResumeLayout(false);

	}

    private void label4_Click(object sender, EventArgs e)
    {

    }
}
