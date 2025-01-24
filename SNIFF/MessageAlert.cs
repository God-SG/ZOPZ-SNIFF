using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using SNIFF.Properties;

namespace SNIFF;

public class MessageAlert : Form
{
	private IContainer components;

	private RichTextBox textBox;

	private Guna2Panel TitleBanner;

	private Label TitleLabel;

	private Guna2DragControl DragControl1;

	private Guna2Elipse Rounded;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	public MessageAlert(string title, string value)
	{
		InitializeComponent();
		ApplyBackgroundColor();
		TitleLabel.Text = title;
		textBox.Text = value;
	}

	public static void Show(string title, string message)
	{
		new MessageAlert(title, message).ShowDialog();
	}

	private void CloseBTN_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void TitleBanner_Paint(object sender, PaintEventArgs e)
	{
	}

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color = ColorTranslator.FromHtml(savedColor);
				TitleLabel.BackColor = color;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error applying background color: " + ex.Message);
			}
		}
	}

	private void MessageAlert_Load(object sender, EventArgs e)
	{
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void guna2ControlBox3_Click(object sender, EventArgs e)
	{
		base.MaximizeBox.ToString();
	}

	private void guna2ControlBox1_Click(object sender, EventArgs e)
	{
		base.MinimizeBox.ToString();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.MessageAlert));
		this.textBox = new System.Windows.Forms.RichTextBox();
		this.TitleBanner = new Guna.UI2.WinForms.Guna2Panel();
		this.TitleLabel = new System.Windows.Forms.Label();
		this.DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
		this.Rounded = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.TitleBanner.SuspendLayout();
		base.SuspendLayout();
		this.textBox.BackColor = System.Drawing.Color.Black;
		this.textBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
		this.textBox.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.textBox.ForeColor = System.Drawing.Color.White;
		this.textBox.Location = new System.Drawing.Point(0, 35);
		this.textBox.Name = "textBox";
		this.textBox.ReadOnly = true;
		this.textBox.Size = new System.Drawing.Size(596, 251);
		this.textBox.TabIndex = 6;
		this.textBox.Text = "Messsage";
		this.TitleBanner.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.TitleBanner.Controls.Add(this.guna2ControlBox1);
		this.TitleBanner.Controls.Add(this.guna2ControlBox3);
		this.TitleBanner.Controls.Add(this.guna2ControlBox2);
		this.TitleBanner.Controls.Add(this.TitleLabel);
		this.TitleBanner.Dock = System.Windows.Forms.DockStyle.Top;
		this.TitleBanner.Location = new System.Drawing.Point(0, 0);
		this.TitleBanner.Name = "TitleBanner";
		this.TitleBanner.Size = new System.Drawing.Size(596, 35);
		this.TitleBanner.TabIndex = 4;
		this.TitleBanner.Paint += new System.Windows.Forms.PaintEventHandler(TitleBanner_Paint);
		this.TitleLabel.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.TitleLabel.Dock = System.Windows.Forms.DockStyle.Left;
		this.TitleLabel.Font = new System.Drawing.Font("Segoe UI", 11f, System.Drawing.FontStyle.Bold);
		this.TitleLabel.ForeColor = System.Drawing.Color.White;
		this.TitleLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.TitleLabel.Location = new System.Drawing.Point(0, 0);
		this.TitleLabel.Name = "TitleLabel";
		this.TitleLabel.Size = new System.Drawing.Size(468, 35);
		this.TitleLabel.TabIndex = 5;
		this.TitleLabel.Text = "Title";
		this.TitleLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.DragControl1.DockIndicatorTransparencyValue = 0.6;
		this.DragControl1.TargetControl = this.TitleBanner;
		this.DragControl1.UseTransparentDrag = true;
		this.guna2VScrollBar1.BindingContainer = this.textBox;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 387;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(578, 35);
		this.guna2VScrollBar1.Maximum = 426;
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 251);
		this.guna2VScrollBar1.TabIndex = 166;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.guna2ControlBox1.Animated = true;
		this.guna2ControlBox1.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2ControlBox1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox1.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox1.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
		this.guna2ControlBox1.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox1.Location = new System.Drawing.Point(461, 0);
		this.guna2ControlBox1.Name = "guna2ControlBox1";
		this.guna2ControlBox1.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.Size = new System.Drawing.Size(45, 35);
		this.guna2ControlBox1.TabIndex = 159;
		this.guna2ControlBox1.Click += new System.EventHandler(guna2ControlBox1_Click);
		this.guna2ControlBox3.Animated = true;
		this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
		this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox3.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox3.Location = new System.Drawing.Point(506, 0);
		this.guna2ControlBox3.Name = "guna2ControlBox3";
		this.guna2ControlBox3.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.Size = new System.Drawing.Size(45, 35);
		this.guna2ControlBox3.TabIndex = 161;
		this.guna2ControlBox3.Click += new System.EventHandler(guna2ControlBox3_Click);
		this.guna2ControlBox2.Animated = true;
		this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox2.Location = new System.Drawing.Point(551, 0);
		this.guna2ControlBox2.Name = "guna2ControlBox2";
		this.guna2ControlBox2.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.Size = new System.Drawing.Size(45, 35);
		this.guna2ControlBox2.TabIndex = 160;
		this.guna2ControlBox2.Click += new System.EventHandler(guna2ControlBox2_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(596, 286);
		base.Controls.Add(this.guna2VScrollBar1);
		base.Controls.Add(this.textBox);
		base.Controls.Add(this.TitleBanner);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "MessageAlert";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(MessageAlert_Load);
		this.TitleBanner.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
