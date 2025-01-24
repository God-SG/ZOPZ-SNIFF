using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using Newtonsoft.Json;
using SNIFF.Classes.Auth.Models;
using SNIFF.prompts;
using SNIFF.Properties;

namespace SNIFF;

public class Geo : Form
{
	private NotificationForm notification;

	private IContainer components;

	private LogInContextMenu logInContextMenu1;

	private ToolStripSeparator toolStripSeparator2;

	private ToolStripMenuItem toolStripMenuItem1;

	private BindingSource bindingSource1;

	private Guna2DataGridView DGVfilterList;

	private DataGridViewTextBoxColumn Column1;

	private Guna2ControlBox guna2ControlBox2;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox1;

	private System.Windows.Forms.Label label4;

	private Panel panel2;

	private Guna2TabControl guna2TabControl1;

	private TabPage tabPage1;

	private TabPage tabPage3;

	private Guna2TextBox guna2TextBox4;

	private Timer timer2;

	private Guna2TextBox guna2TextBox1;

	private Guna2DataGridView guna2DataGridView1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2VScrollBar guna2VScrollBar2;

	private TabPage tabPage2;

	private Guna2DataGridView guna2DataGridView2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private Guna2TextBox guna2TextBox2;

	private Guna2VScrollBar guna2VScrollBar3;

	public Geo()
	{
		InitializeComponent();
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
		ApplyBackgroundColor();
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
				guna2TextBox2.BackColor = color;
				guna2TextBox1.BackColor = color;
				guna2TextBox4.BackColor = color;
				guna2TextBox2.FillColor = color;
				guna2TextBox1.FillColor = color;
				guna2TextBox4.FillColor = color;
				guna2TabControl1.TabMenuBackColor = color;
			}
			catch (Exception)
			{
			}
		}
	}

	private void Geo_Load(object sender, EventArgs e)
	{
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void logInContextMenu1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void bindingSource1_CurrentChanged(object sender, EventArgs e)
	{
	}

	private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void guna2VScrollBar3_Scroll(object sender, ScrollEventArgs e)
	{
	}

	private void guna2ControlBox1_Click(object sender, EventArgs e)
	{
	}

	private void DGVfilterList_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void ShowNotification(string message)
	{
		if (notification == null || notification.IsDisposed)
		{
			notification = new NotificationForm();
			notification.SetMessage(message);
			notification.StartPosition = FormStartPosition.Manual;
			notification.Owner = this;
			notification.TopMost = true;
		}
		else
		{
			notification.SetMessage(message);
		}
		UpdateNotificationPosition();
		notification.Show();
		timer2.Start();
	}

	private void UpdateNotificationPosition()
	{
		if (notification != null && !notification.IsDisposed)
		{
			int notificationWidth = notification.Width;
			int notificationHeight = notification.Height;
			int x = base.ClientSize.Width - notificationWidth - 10;
			int y = base.ClientSize.Height - notificationHeight - 10;
			notification.Location = new Point(base.Left + x, base.Top + y);
		}
	}

	private void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.SuppressKeyPress = true;
			IEnumerable<SNIFF.Classes.Auth.Models.Label> results = Global.Labels.Where((SNIFF.Classes.Auth.Models.Label x) => x.Name == guna2TextBox1.Text);
			if (results.Any())
			{
				string cleanOutput = "IP Address: " + results.First().Value;
				ShowNotification(cleanOutput);
			}
			else
			{
				ShowNotification("No Users");
			}
		}
	}

	private async void guna2TextBox4_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			e.SuppressKeyPress = true;
			GeolocationResponse result = JsonConvert.DeserializeObject<GeolocationResponse>(await Data.Download("http://ip-api.com/json/" + guna2TextBox4.Text + "?fields=66846719"));
			if (result.Status == "fail")
			{
				ShowNotification("Failed to retrieve data.");
				return;
			}
			DGVfilterList.Rows.Clear();
			DGVfilterList.Rows.Add("IP Address: " + result.Query);
			DGVfilterList.Rows.Add("Continent: " + result.Continent);
			DGVfilterList.Rows.Add("Continent Code: " + result.ContinentCode);
			DGVfilterList.Rows.Add("Country: " + result.Country);
			DGVfilterList.Rows.Add("Country Code: " + result.CountryCode);
			DGVfilterList.Rows.Add("Region: " + result.Region);
			DGVfilterList.Rows.Add("Region Name: " + result.RegionName);
			DGVfilterList.Rows.Add("City: " + result.City);
			DGVfilterList.Rows.Add("District: " + result.District);
			DGVfilterList.Rows.Add("Zip: " + result.Zip);
			DGVfilterList.Rows.Add("Latitude: " + result.Lat);
			DGVfilterList.Rows.Add("Longitude: " + result.Lon);
			DGVfilterList.Rows.Add("Timezone: " + result.Timezone);
			DGVfilterList.Rows.Add("Offset: " + result.Offset);
			DGVfilterList.Rows.Add("Currency: " + result.Currency);
			DGVfilterList.Rows.Add("ISP: " + result.Isp);
			DGVfilterList.Rows.Add("Organization: " + result.Org);
			DGVfilterList.Rows.Add("AS: " + result.As);
			DGVfilterList.Rows.Add("AS Name: " + result.Asname);
			DGVfilterList.Rows.Add("Reverse: " + result.Reverse);
			DGVfilterList.Rows.Add("Mobile: " + result.Mobile);
			DGVfilterList.Rows.Add("Proxy: " + result.Proxy);
			DGVfilterList.Rows.Add("Hosting: " + result.Hosting);
		}
	}

	private void guna2TextBox1_TextChanged(object sender, EventArgs e)
	{
	}

	private void guna2TextBox1_KeyDown_1(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		e.SuppressKeyPress = true;
		IEnumerable<SNIFF.Classes.Auth.Models.Label> results = Global.Labels.Where((SNIFF.Classes.Auth.Models.Label x) => x.Name == guna2TextBox1.Text);
		guna2DataGridView1.Rows.Clear();
		if (results.Any())
		{
			foreach (SNIFF.Classes.Auth.Models.Label result in results)
			{
				guna2DataGridView1.Rows.Add("IP Address: " + result.Value);
			}
			return;
		}
		ShowNotification("No Users");
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		notification.Hide();
		timer2.Stop();
	}

	private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private async void guna2TextBox2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		e.SuppressKeyPress = true;
		string ipAddress = guna2TextBox2.Text;
		try
		{
			GeolocationResponse result = JsonConvert.DeserializeObject<GeolocationResponse>(await Data.Download("http://ip-api.com/json/" + ipAddress + "?fields=66846719"));
			guna2DataGridView2.Rows.Clear();
			if (result.Status == "fail")
			{
				string searchTerm = guna2TextBox2.Text;
				IOrderedEnumerable<SNIFF.Classes.Auth.Models.Label> userResults = from x in Global.Labels
					where x.Name == searchTerm
					orderby x.Name
					select x;
				if (userResults.Any())
				{
					foreach (SNIFF.Classes.Auth.Models.Label user in userResults)
					{
						guna2DataGridView2.Rows.Add("Username: " + user.Name);
					}
				}
				else
				{
					ShowNotification("No Users");
				}
				return;
			}
			IOrderedEnumerable<SNIFF.Classes.Auth.Models.Label> associatedUsers = from x in Global.Labels
				where x.Value == result.Query
				orderby x.Name
				select x;
			if (associatedUsers.Any())
			{
				foreach (SNIFF.Classes.Auth.Models.Label user2 in associatedUsers)
				{
					guna2DataGridView2.Rows.Add("Username: " + user2.Name);
				}
			}
			guna2DataGridView2.Rows.Add("IP Address: " + result.Query);
			guna2DataGridView2.Rows.Add("Continent: " + result.Continent);
			guna2DataGridView2.Rows.Add("Continent Code: " + result.ContinentCode);
			guna2DataGridView2.Rows.Add("Country: " + result.Country);
			guna2DataGridView2.Rows.Add("Country Code: " + result.CountryCode);
			guna2DataGridView2.Rows.Add("Region: " + result.Region);
			guna2DataGridView2.Rows.Add("Region Name: " + result.RegionName);
			guna2DataGridView2.Rows.Add("City: " + result.City);
			guna2DataGridView2.Rows.Add("District: " + result.District);
			guna2DataGridView2.Rows.Add("Zip: " + result.Zip);
			guna2DataGridView2.Rows.Add("Latitude: " + result.Lat);
			guna2DataGridView2.Rows.Add("Longitude: " + result.Lon);
			guna2DataGridView2.Rows.Add("Timezone: " + result.Timezone);
			guna2DataGridView2.Rows.Add("Offset: " + result.Offset);
			guna2DataGridView2.Rows.Add("Currency: " + result.Currency);
			guna2DataGridView2.Rows.Add("ISP: " + result.Isp);
			guna2DataGridView2.Rows.Add("Organization: " + result.Org);
			guna2DataGridView2.Rows.Add("AS: " + result.As);
			guna2DataGridView2.Rows.Add("AS Name: " + result.Asname);
			guna2DataGridView2.Rows.Add("Reverse: " + result.Reverse);
			guna2DataGridView2.Rows.Add("Mobile: " + result.Mobile);
			guna2DataGridView2.Rows.Add("Proxy: " + result.Proxy);
			guna2DataGridView2.Rows.Add("Hosting: " + result.Hosting);
		}
		catch (Exception ex)
		{
			ShowNotification("Error fetching data: " + ex.Message);
		}
	}

	private void guna2TextBox2_TextChanged(object sender, EventArgs e)
	{
	}

	private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.Geo));
		this.logInContextMenu1 = new LoginTheme.LogInContextMenu();
		this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.DGVfilterList = new Guna.UI2.WinForms.Guna2DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.label4 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
		this.guna2TabControl1 = new Guna.UI2.WinForms.Guna2TabControl();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.guna2VScrollBar2 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.guna2VScrollBar3 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2DataGridView2 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.logInContextMenu1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVfilterList).BeginInit();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.bindingSource1).BeginInit();
		this.guna2TabControl1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).BeginInit();
		this.tabPage2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView2).BeginInit();
		base.SuspendLayout();
		this.logInContextMenu1.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.toolStripSeparator2, this.toolStripMenuItem1 });
		this.logInContextMenu1.Name = "logInContextMenu2";
		this.logInContextMenu1.ShowImageMargin = false;
		this.logInContextMenu1.Size = new System.Drawing.Size(148, 32);
		this.logInContextMenu1.Opening += new System.ComponentModel.CancelEventHandler(logInContextMenu1_Opening);
		this.toolStripSeparator2.BackColor = System.Drawing.Color.White;
		this.toolStripSeparator2.ForeColor = System.Drawing.Color.Black;
		this.toolStripSeparator2.Name = "toolStripSeparator2";
		this.toolStripSeparator2.Size = new System.Drawing.Size(144, 6);
		this.toolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
		this.toolStripMenuItem1.Text = "Copy To Clipboard";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.DGVfilterList.AllowUserToAddRows = false;
		this.DGVfilterList.AllowUserToResizeColumns = false;
		this.DGVfilterList.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		this.DGVfilterList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.DGVfilterList.BackgroundColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVfilterList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.DGVfilterList.ColumnHeadersHeight = 20;
		this.DGVfilterList.Columns.AddRange(this.Column1);
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.DGVfilterList.DefaultCellStyle = dataGridViewCellStyle3;
		this.DGVfilterList.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.DGVfilterList.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.DGVfilterList.Location = new System.Drawing.Point(3, 41);
		this.DGVfilterList.Name = "DGVfilterList";
		this.DGVfilterList.ReadOnly = true;
		this.DGVfilterList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVfilterList.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.DGVfilterList.RowHeadersVisible = false;
		this.DGVfilterList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
		this.DGVfilterList.RowsDefaultCellStyle = dataGridViewCellStyle5;
		this.DGVfilterList.RowTemplate.Height = 35;
		this.DGVfilterList.Size = new System.Drawing.Size(816, 380);
		this.DGVfilterList.TabIndex = 721;
		this.DGVfilterList.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
		this.DGVfilterList.ThemeStyle.AlternatingRowsStyle.Font = null;
		this.DGVfilterList.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
		this.DGVfilterList.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
		this.DGVfilterList.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
		this.DGVfilterList.ThemeStyle.BackColor = System.Drawing.Color.Black;
		this.DGVfilterList.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.DGVfilterList.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
		this.DGVfilterList.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.DGVfilterList.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DGVfilterList.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
		this.DGVfilterList.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.DGVfilterList.ThemeStyle.HeaderStyle.Height = 20;
		this.DGVfilterList.ThemeStyle.ReadOnly = true;
		this.DGVfilterList.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
		this.DGVfilterList.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
		this.DGVfilterList.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.DGVfilterList.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.DGVfilterList.ThemeStyle.RowsStyle.Height = 35;
		this.DGVfilterList.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.DGVfilterList.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.DGVfilterList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(DGVfilterList_CellContentClick);
		this.Column1.HeaderText = "";
		this.Column1.Name = "Column1";
		this.Column1.ReadOnly = true;
		this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2ControlBox2.Animated = true;
		this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox2.Location = new System.Drawing.Point(785, 0);
		this.guna2ControlBox2.Name = "guna2ControlBox2";
		this.guna2ControlBox2.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox2.TabIndex = 157;
		this.guna2ControlBox2.Click += new System.EventHandler(guna2ControlBox2_Click);
		this.guna2ControlBox3.Animated = true;
		this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
		this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox3.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox3.Location = new System.Drawing.Point(740, 0);
		this.guna2ControlBox3.Name = "guna2ControlBox3";
		this.guna2ControlBox3.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox3.TabIndex = 158;
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
		this.guna2ControlBox1.Location = new System.Drawing.Point(695, 0);
		this.guna2ControlBox1.Name = "guna2ControlBox1";
		this.guna2ControlBox1.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox1.TabIndex = 156;
		this.guna2ControlBox1.Click += new System.EventHandler(guna2ControlBox1_Click);
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(3, 4);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(101, 17);
		this.label4.TabIndex = 153;
		this.label4.Text = "ZOPZ SNIFF";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.guna2ControlBox1);
		this.panel2.Controls.Add(this.guna2ControlBox3);
		this.panel2.Controls.Add(this.guna2ControlBox2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(830, 26);
		this.panel2.TabIndex = 720;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
		this.bindingSource1.CurrentChanged += new System.EventHandler(bindingSource1_CurrentChanged);
		this.guna2TabControl1.Controls.Add(this.tabPage1);
		this.guna2TabControl1.Controls.Add(this.tabPage3);
		this.guna2TabControl1.Controls.Add(this.tabPage2);
		this.guna2TabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2TabControl1.ItemSize = new System.Drawing.Size(260, 40);
		this.guna2TabControl1.Location = new System.Drawing.Point(0, 0);
		this.guna2TabControl1.Name = "guna2TabControl1";
		this.guna2TabControl1.SelectedIndex = 0;
		this.guna2TabControl1.Size = new System.Drawing.Size(830, 472);
		this.guna2TabControl1.TabButtonHoverState.BorderColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonHoverState.FillColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10f);
		this.guna2TabControl1.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TabControl1.TabButtonHoverState.InnerColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonIdleState.BorderColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonIdleState.FillColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10f);
		this.guna2TabControl1.TabButtonIdleState.ForeColor = System.Drawing.Color.White;
		this.guna2TabControl1.TabButtonIdleState.InnerColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonSelectedState.BorderColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonSelectedState.FillColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10f);
		this.guna2TabControl1.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
		this.guna2TabControl1.TabButtonSelectedState.InnerColor = System.Drawing.Color.Transparent;
		this.guna2TabControl1.TabButtonSize = new System.Drawing.Size(260, 40);
		this.guna2TabControl1.TabIndex = 722;
		this.guna2TabControl1.TabMenuBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TabControl1.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
		this.guna2TabControl1.SelectedIndexChanged += new System.EventHandler(guna2TabControl1_SelectedIndexChanged);
		this.tabPage1.BackColor = System.Drawing.Color.Black;
		this.tabPage1.Controls.Add(this.guna2VScrollBar1);
		this.tabPage1.Controls.Add(this.DGVfilterList);
		this.tabPage1.Controls.Add(this.guna2TextBox4);
		this.tabPage1.Location = new System.Drawing.Point(4, 44);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(822, 424);
		this.tabPage1.TabIndex = 6;
		this.tabPage1.Text = "IP Lookup";
		this.guna2VScrollBar1.BindingContainer = this.DGVfilterList;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 10;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(801, 41);
		this.guna2VScrollBar1.Minimum = 1;
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 380);
		this.guna2VScrollBar1.TabIndex = 722;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.guna2VScrollBar1.Value = 1;
		this.guna2TextBox4.Animated = true;
		this.guna2TextBox4.BackColor = System.Drawing.Color.Black;
		this.guna2TextBox4.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox4.BorderThickness = 0;
		this.guna2TextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.guna2TextBox4.DefaultText = "";
		this.guna2TextBox4.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.Dock = System.Windows.Forms.DockStyle.Top;
		this.guna2TextBox4.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox4.FocusedState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2TextBox4.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.Location = new System.Drawing.Point(3, 3);
		this.guna2TextBox4.Name = "guna2TextBox4";
		this.guna2TextBox4.PasswordChar = '\0';
		this.guna2TextBox4.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox4.PlaceholderText = "IP Address";
		this.guna2TextBox4.SelectedText = "";
		this.guna2TextBox4.Size = new System.Drawing.Size(816, 32);
		this.guna2TextBox4.TabIndex = 153;
		this.guna2TextBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2TextBox4_KeyDown);
		this.tabPage3.BackColor = System.Drawing.Color.Black;
		this.tabPage3.Controls.Add(this.guna2VScrollBar2);
		this.tabPage3.Controls.Add(this.guna2DataGridView1);
		this.tabPage3.Controls.Add(this.guna2TextBox1);
		this.tabPage3.Location = new System.Drawing.Point(4, 44);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(822, 424);
		this.tabPage3.TabIndex = 8;
		this.tabPage3.Text = "Username Resolver";
		this.guna2VScrollBar2.BindingContainer = this.guna2DataGridView1;
		this.guna2VScrollBar2.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar2.InUpdate = false;
		this.guna2VScrollBar2.LargeChange = 10;
		this.guna2VScrollBar2.Location = new System.Drawing.Point(801, 41);
		this.guna2VScrollBar2.Minimum = 1;
		this.guna2VScrollBar2.Name = "guna2VScrollBar2";
		this.guna2VScrollBar2.ScrollbarSize = 18;
		this.guna2VScrollBar2.Size = new System.Drawing.Size(18, 380);
		this.guna2VScrollBar2.TabIndex = 723;
		this.guna2VScrollBar2.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar2.ThumbSize = 5f;
		this.guna2VScrollBar2.Value = 1;
		this.guna2DataGridView1.AllowUserToAddRows = false;
		this.guna2DataGridView1.AllowUserToResizeColumns = false;
		this.guna2DataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
		this.guna2DataGridView1.BackgroundColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.guna2DataGridView1.ColumnHeadersHeight = 20;
		this.guna2DataGridView1.Columns.AddRange(this.dataGridViewTextBoxColumn1);
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
		this.guna2DataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView1.Location = new System.Drawing.Point(3, 41);
		this.guna2DataGridView1.Name = "guna2DataGridView1";
		this.guna2DataGridView1.ReadOnly = true;
		this.guna2DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.guna2DataGridView1.RowHeadersVisible = false;
		this.guna2DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle10;
		this.guna2DataGridView1.RowTemplate.Height = 35;
		this.guna2DataGridView1.Size = new System.Drawing.Size(816, 380);
		this.guna2DataGridView1.TabIndex = 722;
		this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
		this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
		this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
		this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
		this.guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
		this.guna2DataGridView1.ThemeStyle.BackColor = System.Drawing.Color.Black;
		this.guna2DataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 20;
		this.guna2DataGridView1.ThemeStyle.ReadOnly = true;
		this.guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
		this.guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
		this.guna2DataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.guna2DataGridView1.ThemeStyle.RowsStyle.Height = 35;
		this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.guna2DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(guna2DataGridView1_CellContentClick);
		this.dataGridViewTextBoxColumn1.HeaderText = "";
		this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
		this.dataGridViewTextBoxColumn1.ReadOnly = true;
		this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2TextBox1.Animated = true;
		this.guna2TextBox1.BackColor = System.Drawing.Color.Black;
		this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.BorderThickness = 0;
		this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.guna2TextBox1.DefaultText = "";
		this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Dock = System.Windows.Forms.DockStyle.Top;
		this.guna2TextBox1.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.FocusedState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2TextBox1.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Location = new System.Drawing.Point(3, 3);
		this.guna2TextBox1.Name = "guna2TextBox1";
		this.guna2TextBox1.PasswordChar = '\0';
		this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.PlaceholderText = "Username";
		this.guna2TextBox1.SelectedText = "";
		this.guna2TextBox1.Size = new System.Drawing.Size(816, 32);
		this.guna2TextBox1.TabIndex = 154;
		this.guna2TextBox1.TextChanged += new System.EventHandler(guna2TextBox1_TextChanged);
		this.guna2TextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2TextBox1_KeyDown_1);
		this.tabPage2.BackColor = System.Drawing.Color.Black;
		this.tabPage2.Controls.Add(this.guna2VScrollBar3);
		this.tabPage2.Controls.Add(this.guna2DataGridView2);
		this.tabPage2.Controls.Add(this.guna2TextBox2);
		this.tabPage2.Location = new System.Drawing.Point(4, 44);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(822, 424);
		this.tabPage2.TabIndex = 9;
		this.tabPage2.Text = "IP Resolver";
		this.guna2VScrollBar3.BindingContainer = this.guna2DataGridView2;
		this.guna2VScrollBar3.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar3.InUpdate = false;
		this.guna2VScrollBar3.LargeChange = 10;
		this.guna2VScrollBar3.Location = new System.Drawing.Point(801, 41);
		this.guna2VScrollBar3.Minimum = 1;
		this.guna2VScrollBar3.Name = "guna2VScrollBar3";
		this.guna2VScrollBar3.ScrollbarSize = 18;
		this.guna2VScrollBar3.Size = new System.Drawing.Size(18, 380);
		this.guna2VScrollBar3.TabIndex = 725;
		this.guna2VScrollBar3.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar3.ThumbSize = 5f;
		this.guna2VScrollBar3.Value = 1;
		this.guna2DataGridView2.AllowUserToAddRows = false;
		this.guna2DataGridView2.AllowUserToResizeColumns = false;
		this.guna2DataGridView2.AllowUserToResizeRows = false;
		dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle11;
		this.guna2DataGridView2.BackgroundColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle12;
		this.guna2DataGridView2.ColumnHeadersHeight = 20;
		this.guna2DataGridView2.Columns.AddRange(this.dataGridViewTextBoxColumn2);
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle13.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2DataGridView2.DefaultCellStyle = dataGridViewCellStyle13;
		this.guna2DataGridView2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2DataGridView2.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView2.Location = new System.Drawing.Point(3, 41);
		this.guna2DataGridView2.Name = "guna2DataGridView2";
		this.guna2DataGridView2.ReadOnly = true;
		this.guna2DataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle14.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle14;
		this.guna2DataGridView2.RowHeadersVisible = false;
		this.guna2DataGridView2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle15.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView2.RowsDefaultCellStyle = dataGridViewCellStyle15;
		this.guna2DataGridView2.RowTemplate.Height = 35;
		this.guna2DataGridView2.Size = new System.Drawing.Size(816, 380);
		this.guna2DataGridView2.TabIndex = 724;
		this.guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
		this.guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.Font = null;
		this.guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
		this.guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
		this.guna2DataGridView2.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
		this.guna2DataGridView2.ThemeStyle.BackColor = System.Drawing.Color.Black;
		this.guna2DataGridView2.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.guna2DataGridView2.ThemeStyle.HeaderStyle.Height = 20;
		this.guna2DataGridView2.ThemeStyle.ReadOnly = true;
		this.guna2DataGridView2.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
		this.guna2DataGridView2.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
		this.guna2DataGridView2.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.guna2DataGridView2.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.guna2DataGridView2.ThemeStyle.RowsStyle.Height = 35;
		this.guna2DataGridView2.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.guna2DataGridView2.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.dataGridViewTextBoxColumn2.HeaderText = "";
		this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
		this.dataGridViewTextBoxColumn2.ReadOnly = true;
		this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2TextBox2.Animated = true;
		this.guna2TextBox2.BackColor = System.Drawing.Color.Black;
		this.guna2TextBox2.BorderColor = System.Drawing.Color.Empty;
		this.guna2TextBox2.BorderThickness = 0;
		this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.guna2TextBox2.DefaultText = "";
		this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.guna2TextBox2.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox2.FocusedState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2TextBox2.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.Location = new System.Drawing.Point(3, 3);
		this.guna2TextBox2.Name = "guna2TextBox2";
		this.guna2TextBox2.PasswordChar = '\0';
		this.guna2TextBox2.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox2.PlaceholderText = "IP Address";
		this.guna2TextBox2.SelectedText = "";
		this.guna2TextBox2.Size = new System.Drawing.Size(816, 32);
		this.guna2TextBox2.TabIndex = 723;
		this.guna2TextBox2.TextChanged += new System.EventHandler(guna2TextBox2_TextChanged);
		this.guna2TextBox2.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2TextBox2_KeyDown);
		this.timer2.Interval = 2000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(830, 472);
		base.Controls.Add(this.guna2TabControl1);
		base.Controls.Add(this.panel2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "Geo";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ZOPZ SNIFF";
		base.Load += new System.EventHandler(Geo_Load);
		this.logInContextMenu1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVfilterList).EndInit();
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.bindingSource1).EndInit();
		this.guna2TabControl1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).EndInit();
		this.tabPage2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView2).EndInit();
		base.ResumeLayout(false);
	}
}
