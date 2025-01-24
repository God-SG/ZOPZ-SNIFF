using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using SNIFF.Properties;

namespace SNIFF;

public class simpletextprompt : Form
{
	private Func<string, bool> _verifyFunction;

	private IContainer components;

	private Guna2TextBox textBox1;

	private Panel panel2;

	private Label label1;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	public string PlaceholderText
	{
		get
		{
			return textBox1.PlaceholderText;
		}
		set
		{
			if (string.IsNullOrEmpty(textBox1.PlaceholderText))
			{
				textBox1.PlaceholderText = value;
				textBox1.ForeColor = Color.White;
			}
		}
	}

	public simpletextprompt()
	{
		InitializeComponent();
		textBox1.Enter += textBox1_Enter;
		textBox1.Leave += textBox1_Leave;
	}

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color = ColorTranslator.FromHtml(savedColor);
				panel2.BackColor = color;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error applying background color: " + ex.Message);
			}
		}
	}

	private void simpletextprompt_Load(object sender, EventArgs e)
	{
		base.MaximizeBox = true;
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
	}

	public string ShowDialog(Func<string, bool> verifyFunction = null)
	{
		_verifyFunction = verifyFunction;
		base.ShowDialog();
		return textBox1.Text;
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (_verifyFunction == null)
		{
			Close();
		}
		else if (_verifyFunction(textBox1.Text))
		{
			Close();
		}
		else
		{
			MessageBox.Show("Value Verification Error");
		}
	}

	private void guna2Button3_Click(object sender, EventArgs e)
	{
		if (_verifyFunction == null)
		{
			Close();
		}
		else if (_verifyFunction(textBox1.Text))
		{
			Close();
		}
		else
		{
			MessageBox.Show("Value Verification Error");
		}
	}

	private void textBox1_Enter(object sender, EventArgs e)
	{
		if (textBox1.Text == PlaceholderText)
		{
			textBox1.ForeColor = Color.Black;
		}
	}

	private void textBox1_Leave(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(textBox1.Text))
		{
			textBox1.Text = PlaceholderText;
			textBox1.ForeColor = Color.Gray;
		}
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void textBox1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.SuppressKeyPress = true;
			if (_verifyFunction == null)
			{
				Close();
			}
			else if (_verifyFunction(textBox1.Text))
			{
				Close();
			}
			else
			{
				MessageBox.Show("Value Verification Error");
			}
		}
	}

	private void textBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void guna2ControlBox2_Click_1(object sender, EventArgs e)
	{
		Hide();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.simpletextprompt));
		this.textBox1 = new Guna.UI2.WinForms.Guna2TextBox();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.textBox1.Animated = true;
		this.textBox1.BackColor = System.Drawing.Color.Black;
		this.textBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.textBox1.DefaultText = "";
		this.textBox1.DisabledState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.textBox1.DisabledState.FillColor = System.Drawing.Color.FromArgb(10, 10, 10);
		this.textBox1.DisabledState.ForeColor = System.Drawing.Color.White;
		this.textBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.textBox1.FillColor = System.Drawing.Color.Black;
		this.textBox1.FocusedState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.textBox1.FocusedState.FillColor = System.Drawing.Color.Black;
		this.textBox1.FocusedState.ForeColor = System.Drawing.Color.White;
		this.textBox1.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.textBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.textBox1.ForeColor = System.Drawing.Color.White;
		this.textBox1.HoverState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.textBox1.HoverState.FillColor = System.Drawing.Color.Black;
		this.textBox1.HoverState.ForeColor = System.Drawing.Color.White;
		this.textBox1.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.textBox1.Location = new System.Drawing.Point(24, 71);
		this.textBox1.Name = "textBox1";
		this.textBox1.PasswordChar = '\0';
		this.textBox1.PlaceholderForeColor = System.Drawing.Color.White;
		this.textBox1.PlaceholderText = "";
		this.textBox1.SelectedText = "";
		this.textBox1.Size = new System.Drawing.Size(260, 36);
		this.textBox1.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
		this.textBox1.TabIndex = 143;
		this.textBox1.TextChanged += new System.EventHandler(textBox1_TextChanged);
		this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(textBox1_KeyDown);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.guna2ControlBox1);
		this.panel2.Controls.Add(this.guna2ControlBox3);
		this.panel2.Controls.Add(this.guna2ControlBox2);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(312, 28);
		this.panel2.TabIndex = 165;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
		this.label1.BackColor = System.Drawing.Color.Transparent;
		this.label1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label1.ForeColor = System.Drawing.Color.White;
		this.label1.Location = new System.Drawing.Point(3, 4);
		this.label1.Name = "label1";
		this.label1.Size = new System.Drawing.Size(101, 17);
		this.label1.TabIndex = 153;
		this.label1.Text = "ZOPZ SNIFF";
		this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		this.guna2ControlBox1.Location = new System.Drawing.Point(177, 0);
		this.guna2ControlBox1.Name = "guna2ControlBox1";
		this.guna2ControlBox1.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox1.TabIndex = 162;
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
		this.guna2ControlBox3.Location = new System.Drawing.Point(222, 0);
		this.guna2ControlBox3.Name = "guna2ControlBox3";
		this.guna2ControlBox3.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox3.TabIndex = 164;
		this.guna2ControlBox3.Click += new System.EventHandler(guna2ControlBox3_Click);
		this.guna2ControlBox2.Animated = true;
		this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox2.Location = new System.Drawing.Point(267, 0);
		this.guna2ControlBox2.Name = "guna2ControlBox2";
		this.guna2ControlBox2.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox2.TabIndex = 163;
		this.guna2ControlBox2.Click += new System.EventHandler(guna2ControlBox2_Click_1);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(312, 164);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.textBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "simpletextprompt";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ZOPZ SNIFF";
		base.Load += new System.EventHandler(simpletextprompt_Load);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
