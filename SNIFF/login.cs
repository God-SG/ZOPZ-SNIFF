using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using Microsoft.Win32;
using SNIFF.Classes.Auth.Models;

namespace SNIFF;

public class login : Form
{
	private readonly string NpcapDownloadUrl = "https://npcap.com/dist/npcap-1.79.exe";

	private readonly string VCRedistDownloadUrl = "https://download.microsoft.com/download/1/6/5/165255E7-1014-4D0A-B094-B6A430A6BFFC/vcredist_x86.exe";

	private IContainer components;

	private Guna2Transition Ani;

	private Timer timer1;

	private GunaDragControl DragControl_Form;

	private Panel panel2;

	private System.Windows.Forms.Label label4;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	private Guna2CheckBox Remebermecheck;

	private Guna2Button LoginBTN;

	private Guna2TextBox guna2TextBox1;

	private Guna2TextBox guna2TextBox2;

	private Guna2Separator guna2Separator1;

	private Guna2Panel guna2Panel1;

	public login()
	{
		InitializeComponent();
		base.MaximizeBox = true;
		base.Name = "ZOPZ SNIFF Cracked - God <3";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void Remebermecheck_CheckedChanged(object sender, EventArgs e)
	{
	}

	private void Username_TextChanged(object sender, EventArgs e)
	{
	}

	public void Alert(string msg, alert.enmType type)
	{
		new alert().showAlert(msg, type);
	}

	private async void LoginBTN_Click(object sender, EventArgs e)
	{
        Hide();
        new Mainform().Show();
		return;
		LoginBTN.Text = "Bypass";

        MessageBox.Show("ShadowGarden on TOP!");

    }

	private bool IsNpcapInstalled()
	{
		try
		{
			using RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Npcap");
			return key != null;
		}
		catch
		{
			return false;
		}
	}

	private bool IsVCRedistributableInstalled()
	{
		try
		{
			string redistributableKey = "SOFTWARE\\WOW6432Node\\Microsoft\\VisualStudio\\10.0\\VC\\VCRedist\\x86";
			if (IsRedistributableVersionPresent(redistributableKey, "v10.0.40219"))
			{
				return true;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Error checking for VC Redistributable: " + ex.Message);
		}
		return false;
	}

	private bool IsRedistributableVersionPresent(string registryKey, string expectedVersionPrefix)
	{
		using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey))
		{
			if (key != null && key.GetValue("Version") is string versionValue)
			{
				Console.WriteLine("Found version value: " + versionValue);
				if (versionValue.StartsWith(expectedVersionPrefix, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
		}
		return false;
	}

	private void OpenBrowser(string url)
	{
		try
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = url,
				UseShellExecute = true
			});
		}
		catch (Exception ex)
		{
			MessageBox.Show("Failed to open browser. Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
		}
	}

	private void login_Load(object sender, EventArgs e)
	{
		timer1.Start();
		SettingsModel settings = SettingsManager.Load();
		if (settings.RememberMe)
		{
			guna2TextBox1.Text = settings.Username;
			guna2TextBox2.Text = settings.Password;
			Remebermecheck.Checked = true;
		}
		base.TopMost = settings.AppTopMost;
		bool npcapInstalled = IsNpcapInstalled();
		bool vcredistInstalled = IsVCRedistributableInstalled();
		if (npcapInstalled && vcredistInstalled)
		{
			return;
		}
		string message = "Required components are not installed.\n\n";
		if (!npcapInstalled)
		{
			message += "Npcap is not installed.\n";
			message += "Do you want to download it now?";
		}
		if (!vcredistInstalled)
		{
			if (!npcapInstalled)
			{
				message += "\n";
			}
			message += "Visual C++ Redistributable x86 is not installed.\n";
			message += "Do you want to download it now?";
		}
		if (MessageBox.Show(message, "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Hand) == DialogResult.Yes)
		{
			if (!npcapInstalled)
			{
				OpenBrowser(NpcapDownloadUrl);
			}
			if (!vcredistInstalled)
			{
				OpenBrowser(VCRedistDownloadUrl);
			}
		}
		Application.Exit();
	}

	private void guna2Panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Environment.Exit(0);
	}

	private void guna2ControlBox1_Click(object sender, EventArgs e)
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
            Guna.UI2.AnimatorNS.Animation animation2 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.Ani = new Guna.UI2.WinForms.Guna2Transition();
            this.LoginBTN = new Guna.UI2.WinForms.Guna2Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.Remebermecheck = new Guna.UI2.WinForms.Guna2CheckBox();
            this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Separator1 = new Guna.UI2.WinForms.Guna2Separator();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DragControl_Form = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.panel2.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // Ani
            // 
            this.Ani.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(0);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 0F;
            this.Ani.DefaultAnimation = animation2;
            // 
            // LoginBTN
            // 
            this.LoginBTN.Animated = true;
            this.LoginBTN.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.LoginBTN.BorderThickness = 1;
            this.Ani.SetDecoration(this.LoginBTN, Guna.UI2.AnimatorNS.DecorationType.None);
            this.LoginBTN.FillColor = System.Drawing.Color.Transparent;
            this.LoginBTN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.LoginBTN.ForeColor = System.Drawing.Color.White;
            this.LoginBTN.Location = new System.Drawing.Point(59, 352);
            this.LoginBTN.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.LoginBTN.Name = "LoginBTN";
            this.LoginBTN.PressedColor = System.Drawing.Color.White;
            this.LoginBTN.Size = new System.Drawing.Size(788, 46);
            this.LoginBTN.TabIndex = 32;
            this.LoginBTN.Text = "Bypass";
            this.LoginBTN.Click += new System.EventHandler(this.LoginBTN_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.guna2ControlBox1);
            this.panel2.Controls.Add(this.guna2ControlBox3);
            this.panel2.Controls.Add(this.guna2ControlBox2);
            this.Ani.SetDecoration(this.panel2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(897, 32);
            this.panel2.TabIndex = 164;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.label4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(4, 5);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(168, 27);
            this.label4.TabIndex = 153;
            this.label4.Text = "ZOPZ SNIFF CRACKED";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Animated = true;
            this.guna2ControlBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2ControlBox1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ani.SetDecoration(this.guna2ControlBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(717, 0);
            this.guna2ControlBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(60, 32);
            this.guna2ControlBox1.TabIndex = 156;
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click);
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Animated = true;
            this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
            this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ani.SetDecoration(this.guna2ControlBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(777, 0);
            this.guna2ControlBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(60, 32);
            this.guna2ControlBox3.TabIndex = 158;
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Animated = true;
            this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ani.SetDecoration(this.guna2ControlBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(837, 0);
            this.guna2ControlBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(60, 32);
            this.guna2ControlBox2.TabIndex = 157;
            this.guna2ControlBox2.Click += new System.EventHandler(this.guna2ControlBox2_Click);
            // 
            // Remebermecheck
            // 
            this.Remebermecheck.Animated = true;
            this.Remebermecheck.CheckedState.BorderColor = System.Drawing.Color.White;
            this.Remebermecheck.CheckedState.BorderRadius = 2;
            this.Remebermecheck.CheckedState.BorderThickness = 0;
            this.Remebermecheck.CheckedState.FillColor = System.Drawing.Color.Black;
            this.Ani.SetDecoration(this.Remebermecheck, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Remebermecheck.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.Remebermecheck.ForeColor = System.Drawing.Color.White;
            this.Remebermecheck.Location = new System.Drawing.Point(59, 273);
            this.Remebermecheck.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Remebermecheck.Name = "Remebermecheck";
            this.Remebermecheck.Size = new System.Drawing.Size(788, 23);
            this.Remebermecheck.TabIndex = 35;
            this.Remebermecheck.Text = "Discord.gg/ShadowGarden";
            this.Remebermecheck.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.Remebermecheck.UncheckedState.BorderRadius = 2;
            this.Remebermecheck.UncheckedState.BorderThickness = 0;
            this.Remebermecheck.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.Remebermecheck.CheckedChanged += new System.EventHandler(this.Remebermecheck_CheckedChanged);
            // 
            // guna2TextBox1
            // 
            this.guna2TextBox1.Animated = true;
            this.guna2TextBox1.BackColor = System.Drawing.Color.Black;
            this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox1.DefaultText = "";
            this.guna2TextBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox1.FocusedState.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox1.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox1.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox1.HoverState.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox1.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.Location = new System.Drawing.Point(59, 116);
            this.guna2TextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox1.Name = "guna2TextBox1";
            this.guna2TextBox1.PasswordChar = '\0';
            this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox1.PlaceholderText = "Fuck ZOPZ";
            this.guna2TextBox1.SelectedText = "";
            this.guna2TextBox1.Size = new System.Drawing.Size(788, 44);
            this.guna2TextBox1.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.guna2TextBox1.TabIndex = 90;
            this.guna2TextBox1.TextChanged += new System.EventHandler(this.Username_TextChanged);
            // 
            // guna2TextBox2
            // 
            this.guna2TextBox2.Animated = true;
            this.guna2TextBox2.BackColor = System.Drawing.Color.Black;
            this.guna2TextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox2.DefaultText = "";
            this.guna2TextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(10)))), ((int)(((byte)(10)))));
            this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.FocusedState.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox2.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox2.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.HoverState.FillColor = System.Drawing.Color.Black;
            this.guna2TextBox2.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.Location = new System.Drawing.Point(59, 192);
            this.guna2TextBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PasswordChar = '*';
            this.guna2TextBox2.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.PlaceholderText = "ShadowGarden <3";
            this.guna2TextBox2.SelectedText = "";
            this.guna2TextBox2.Size = new System.Drawing.Size(788, 44);
            this.guna2TextBox2.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.guna2TextBox2.TabIndex = 91;
            // 
            // guna2Separator1
            // 
            this.guna2Separator1.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2Separator1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Separator1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Separator1.Location = new System.Drawing.Point(-193, 39);
            this.guna2Separator1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2Separator1.Name = "guna2Separator1";
            this.guna2Separator1.Size = new System.Drawing.Size(1199, 12);
            this.guna2Separator1.TabIndex = 44;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Black;
            this.guna2Panel1.Controls.Add(this.guna2Separator1);
            this.guna2Panel1.Controls.Add(this.panel2);
            this.guna2Panel1.Controls.Add(this.guna2TextBox2);
            this.guna2Panel1.Controls.Add(this.guna2TextBox1);
            this.guna2Panel1.Controls.Add(this.Remebermecheck);
            this.guna2Panel1.Controls.Add(this.LoginBTN);
            this.Ani.SetDecoration(this.guna2Panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(897, 485);
            this.guna2Panel1.TabIndex = 45;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DragControl_Form
            // 
            this.DragControl_Form.TargetControl = this.panel2;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(897, 485);
            this.Controls.Add(this.guna2Panel1);
            this.Ani.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "gg";
            this.Load += new System.EventHandler(this.login_Load);
            this.panel2.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.ResumeLayout(false);

	}
}
