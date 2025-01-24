using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using Newtonsoft.Json;
using SNIFF.Classes.Auth;
using SNIFF.Classes.Auth.Models;
using SNIFF.prompts;
using SNIFF.Properties;

namespace SNIFF;

public class t : Form
{
	public class GameFiltersResponse
	{
		public List<GameFilter> game_filters { get; set; }
	}

	public class GameFilter
	{
		public string Name { get; set; }

		public string Platform { get; set; }
	}

	internal List<Filter> Filters = new List<Filter>();

	private HttpClient httpclient = new HttpClient();

	private NotificationForm notification;

	private Color lastColor;

	private ColorWheel colorWheel;

	private readonly ChatClient chatClient;

	private string chatchannel = "Nigger Chat";

	private List<int> selectedRows = new List<int>();

	private DateTime _lastChangeTime = DateTime.MinValue;

	private DateTime _lastSend;

	private IContainer components;

	private ImageList imageList1;

	private PictureBox pictureBox1;

	private System.Windows.Forms.Label maxboottime;

	private System.Windows.Forms.Label label5;

	private System.Windows.Forms.Label RankLabel;

	private System.Windows.Forms.Label exDate;

	private System.Windows.Forms.Label label7;

	private System.Windows.Forms.Label label8;

	private System.Windows.Forms.Label usernameLabel;

	private Guna2TextBox guna2TextBox1;

	private Guna2TextBox host;

	private GunaDragControl DragControl_Form;

	private LogInContextMenu logInContextMenu2;

	private ToolStripMenuItem copyPacketsNumberToolStripMenuItem;

	private System.Windows.Forms.Label label9;

	private System.Windows.Forms.Label AppVersion;

	private System.Windows.Forms.Label label6;

	private System.Windows.Forms.Label totaluserslabel;

	private Timer timer1;

	private Guna2VScrollBar guna2VScrollBar2;

	private Guna2TabControl guna2TabControl1;

	private TabPage tabPage6;

	private TabPage tabPage7;

	private Panel panel2;

	private System.Windows.Forms.Label label1;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	private Guna2GroupBox guna2GroupBox1;

	private Guna2GroupBox guna2GroupBox4;

	private Guna2GroupBox guna2GroupBox3;

	private TabPage tabPage3;

	private Guna2VScrollBar guna2VScrollBar4;

	private Guna2CheckBox Remebermecheck;

	private Guna2CheckBox guna2CheckBox1;

	private Guna2CheckBox guna2CheckBox2;

	private Guna2CheckBox guna2CheckBox3;

	private System.Windows.Forms.Label label2;

	private System.Windows.Forms.Label label3;

	private TabPage tabPage1;

	private Guna2CheckBox guna2CheckBox4;

	private ToolStripMenuItem editLabelToolStripMenuItem;

	private Guna2DataGridViewStyler guna2DataGridViewStyler1;

	private Guna2DataGridView dataGridView1;

	private Guna2GroupBox guna2GroupBox2;

	private Guna2Button guna2Button2;

	private ColorDialog colorDialog1;

	private Guna2Panel guna2Panel1;

	private Guna2CheckBox otherinfoshow;

	private Guna2CheckBox xboxtabshow;

	private Guna2CheckBox playstationtabshow;

	private Guna2CheckBox filteredgamestabshow;

	private Guna2CheckBox xbltoolinfo;

	private Guna2GroupBox guna2GroupBox7;

	private LogInContextMenu logInContextMenu1;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem clearAllToolStripMenuItem;

	private Timer timer2;

	private Guna2HtmlToolTip filterDescriptionToolTip;

	private Guna2DataGridView DGVfilterList;

	private DataGridViewTextBoxColumn Column4;

	private DataGridViewTextBoxColumn Column5;

	private Guna2Elipse guna2Elipse12;

	private Guna2Elipse guna2Elipse1;

	private Guna2Elipse guna2Elipse2;

	private Guna2Elipse guna2Elipse3;

	private Guna2Elipse guna2Elipse4;

	private Guna2Elipse guna2Elipse5;

	private Guna2HtmlToolTip guna2HtmlToolTip1;

	private TabPage tabPage2;

	private Panel panel1;

	private Guna2TextBox Messageboxtb;

	private FlowLayoutPanel chatPanel;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2CheckBox pctabshow;

	public void Alert(string msg, alert.enmType type = alert.enmType.Warning)
	{
		new alert().showAlert(msg, type);
	}

	public t()
	{
		InitializeComponent();
		colorWheel = new ColorWheel
		{
			Dock = DockStyle.Fill
		};
		guna2Panel1.Controls.Add(colorWheel);
		colorWheel.ColorChanged += ColorWheel_ColorChanged;
		ApplyBackgroundColor();
		LoadSavedBackgroundImage();
		DGVfilterList.RowTemplate.Height = 35;
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
		//chatClient = Global.Authenticator.GetChatClient();
		//chatClient.OnConnected += ChatClient_OnConnected;
		//chatClient.OnDisconnected += ChatClient_OnDisconnected;
		//chatClient.OnMessageReceived += ChatClient_OnMessageReceived;
		//chatClient.OnMessagesReceived += ChatClient_OnMessagesReceived;
	}

	private void AddMessage(ChatMessage msg)
	{
		UserSentmessage msgDisplay = new UserSentmessage(msg.Poster, msg.Message);
		chatPanel.Invoke((MethodInvoker)delegate
		{
			chatPanel.Controls.Add(msgDisplay);
		});
	}

	private void ChatClient_OnMessagesReceived(IEnumerable<ChatMessage> obj)
	{
		foreach (ChatMessage msg in obj)
		{
			AddMessage(msg);
		}
	}

	private void ChatClient_OnMessageReceived(ChatMessage msg)
	{
		AddMessage(msg);
	}

	private void ChatClient_OnDisconnected()
	{
		Console.WriteLine("Disconnected");
	}

	private async void ChatClient_OnConnected()
	{
		await chatClient.JoinRoomAsync("zopzsniff");
	}

	private async void Form2_LoadAsync(object sender, EventArgs e)
	{
		dataGridView1.RowTemplate.Height = 35;
		dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		DGVfilterList.AllowUserToAddRows = false;
		dataGridView1.AllowUserToAddRows = false;
		SettingsModel settings = SettingsManager.Load();
		RankLabel.Text = "ShadowGarden";
		maxboottime.Text = "LOL";
		exDate.Text = "Til Death";
		guna2CheckBox1.Checked = settings.ShowDiscordRPC;
		guna2CheckBox3.Checked = settings.AutoLogin;
		usernameLabel.Text = settings.Username;
		Remebermecheck.Checked = settings.PFmode;
		guna2CheckBox2.Checked = settings.AppTopMost;
		filteredgamestabshow.Checked = settings.FilteredGames;
		playstationtabshow.Checked = settings.Playstation;
		otherinfoshow.Checked = settings.Otherinfo;
		xbltoolinfo.Checked = settings.XBLTool;
		xboxtabshow.Checked = settings.Xbox;
		pctabshow.Checked = settings.Pctab;
		//await chatClient.ConnectAsync();
	}

	private async void Menu_Shown(object sender, EventArgs e)
	{
		await LoadGameFilters();
		LoadSelectedGameFilter();
		//Statistics stats = await Global.Authenticator.GetStatisticsAsync();
		timer1.Start();
		AppVersion.Text = "Broken";
		totaluserslabel.Text = "0";
		label3.Text = "0";
		Loadlogs();
	}

	private void SaveSelectedGameFilter()
	{
		SettingsModel settings = SettingsManager.Load();
		settings.EnabledFilters = new List<string>();
		foreach (object selectedRow in DGVfilterList.SelectedRows)
		{
			DataGridViewRow row = selectedRow as DataGridViewRow;
			settings.EnabledFilters.Add(row.Cells[0].Value.ToString());
		}
		settings.Save();
	}

	private void ColorWheel_ColorChanged(object sender, EventArgs e)
	{
		if (colorWheel != null)
		{
			Color selectedColor = colorWheel.SelectedColor;
			if (selectedColor != lastColor)
			{
				BackColor = selectedColor;
				SNIFF.Properties.Settings.Default.BackgroundColor = ColorTranslator.ToHtml(selectedColor);
				SNIFF.Properties.Settings.Default.Save();
				lastColor = selectedColor;
			}
		}
	}

	private void LoadSelectedGameFilter()
	{
		foreach (DataGridViewRow item in DGVfilterList.Rows.Cast<DataGridViewRow>())
		{
			item.Selected = false;
		}
		List<string> SelectedGameFilters = SettingsManager.Load().EnabledFilters ?? new List<string>();
		if (!SelectedGameFilters.Any())
		{
			return;
		}
		for (int i = 0; i < DGVfilterList.RowCount; i++)
		{
			DataGridViewRow row = DGVfilterList.Rows[i];
			string filter = row.Cells[0]?.Value?.ToString();
			if (row.Cells[0].Value != null && SelectedGameFilters.Contains(filter))
			{
				row.Selected = true;
				selectedRows.Add(i);
			}
		}
	}

	public DateTime UnixTimeToDateTime(long unixtime)
	{
		DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
		try
		{
			return dtDateTime.AddSeconds(unixtime).ToLocalTime();
		}
		catch
		{
			return DateTime.MaxValue;
		}
	}

	private async Task LoadGameFilters()
	{
		_ = 1;
		try
		{
			string jsonResponse = await GetDataFromPastebinAsync(httpclient, "https://partyhax.club/offlinefilters.json");
			if (string.IsNullOrWhiteSpace(jsonResponse))
			{
				ShowNotification("No data received from the URL.");
				return;
			}
			string url = "https://partyhax.club/onlinefilters.json";
			if (string.IsNullOrWhiteSpace(url))
			{
				ShowNotification("Received an invalid URL.");
				return;
			}
			List<GameFilter> filters = await GetJsonAsync<List<GameFilter>>(httpclient, url);
			List<GameFilter> list = (from filter in JsonConvert.DeserializeObject<GameFiltersResponse>(jsonResponse).game_filters.Concat(filters)
				orderby filter.Name
				select filter).ToList();
			DGVfilterList.Rows.Clear();
			foreach (GameFilter filter2 in list)
			{
				DGVfilterList.Rows.Add(filter2.Name, filter2.Platform);
			}
		}
		catch (Exception ex)
		{
			ShowNotification("Error fetching data: " + ex.Message);
			Clipboard.SetText(ex.Message);
		}
		finally
		{
			((HttpMessageInvoker)httpclient).Dispose();
		}
	}

	private async Task<string> GetDataFromPastebinAsync(HttpClient httpClient, string url)
	{
		_ = 1;
		try
		{
			HttpResponseMessage obj = await httpClient.GetAsync(url);
			obj.EnsureSuccessStatusCode();
			return await obj.Content.ReadAsStringAsync();
		}
		catch (HttpRequestException val)
		{
			HttpRequestException e = val;
			ShowNotification("Error fetching data: " + ((Exception)(object)e).Message);
			return null;
		}
	}

	private async Task<T> GetJsonAsync<T>(HttpClient httpClient, string url)
	{
		_ = 1;
		try
		{
			HttpResponseMessage obj = await httpClient.GetAsync(url);
			obj.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<T>(await obj.Content.ReadAsStringAsync());
		}
		catch (HttpRequestException val)
		{
			HttpRequestException e = val;
			ShowNotification("Error fetching data: " + ((Exception)(object)e).Message);
			return default(T);
		}
	}

	private void Loadlogs()
	{
		try
		{
			DataTable dataTable = CreateDataTableFromLabels(Global.Labels);
			PopulateDataGridView(dataTable);
			guna2TextBox1_TextChanged(null, null);
		}
		catch (Exception)
		{
			ShowNotification("No data received.");
		}
	}

	private void PopulateDataGridView(DataTable dataTable)
	{
		dataGridView1.DataSource = dataTable;
		dataGridView1.Columns[0].HeaderText = "ID";
		dataGridView1.Columns[1].HeaderText = "Label";
		dataGridView1.Columns[2].HeaderText = "IP Address";
		dataGridView1.Columns[0].Width = 50;
	}

	private DataTable CreateDataTableFromLabels(List<SNIFF.Classes.Auth.Models.Label> labels)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("ID", typeof(int));
		dataTable.Columns.Add("Label");
		dataTable.Columns.Add("IP Address");
		for (int i = 0; i < labels.Count; i++)
		{
			SNIFF.Classes.Auth.Models.Label label = labels[i];
			dataTable.Rows.Add(i + 1, label.Name, label.Value);
		}
		return dataTable;
	}

	private void pictureBox1_Click(object sender, EventArgs e)
	{
		ChangeBackgroundImage();
	}

	private void ChangeBackgroundImage()
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp|All Files|*.*";
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			pictureBox1.BackgroundImage = Image.FromFile(openFileDialog.FileName);
			SNIFF.Properties.Settings.Default.Background = openFileDialog.FileName;
			SNIFF.Properties.Settings.Default.Save();
		}
	}

	private void LoadSavedBackgroundImage()
	{
		string backgroundImagePath = SNIFF.Properties.Settings.Default.Background;
		if (!string.IsNullOrEmpty(backgroundImagePath) && File.Exists(backgroundImagePath))
		{
			pictureBox1.BackgroundImage = Image.FromFile(backgroundImagePath);
		}
	}

	private void exDate_Click(object sender, EventArgs e)
	{
	}

	public static bool ValidateIP(string value)
	{
		value = value.Trim();
		return new Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$").IsMatch(value);
	}

	private void logInContextMenu2_Opening(object sender, CancelEventArgs e)
	{
	}

	private void copyPacketsNumberToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (dataGridView1.SelectedRows.Count <= 0)
			{
				return;
			}
			DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 2)
			{
				object cellValue1 = selectedRow.Cells[2].Value;
				if (cellValue1 != null)
				{
					Clipboard.SetText(cellValue1.ToString());
				}
			}
		}
		catch
		{
		}
	}

	private void AppVersion_Click(object sender, EventArgs e)
	{
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
	}

	private void maxboottime_Click(object sender, EventArgs e)
	{
	}

	private void guna2ControlBox3_Click_2(object sender, EventArgs e)
	{
	}

	private void guna2GroupBox3_Click(object sender, EventArgs e)
	{
	}

	private void guna2GroupBox4_Click(object sender, EventArgs e)
	{
	}

	private void guna2ControlBox1_Click_1(object sender, EventArgs e)
	{
	}

	private void Remebermecheck_CheckedChanged(object sender, EventArgs e)
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.PFmode = Remebermecheck.Checked;
		_ = SNIFF.Properties.Settings.Default.LastMachineIP;
		DoubleBuffered = true;
		SetStyle(ControlStyles.OptimizedDoubleBuffer, value: true);
		settingsModel.Save();
	}

	private void guna2CheckBox2_CheckedChanged(object sender, EventArgs e)
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.AppTopMost = guna2CheckBox2.Checked;
		settingsModel.Save();
	}

	private void guna2CheckBox3_CheckedChanged(object sender, EventArgs e)
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.AutoLogin = guna2CheckBox3.Checked;
		settingsModel.Save();
	}

	private void guna2CheckBox1_CheckedChanged(object sender, EventArgs e)
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.ShowDiscordRPC = guna2CheckBox1.Checked;
		settingsModel.Save();
	}

	private void guna2CheckBox4_CheckedChanged(object sender, EventArgs e)
	{
		SettingsManager.Load().Save();
	}

	private void guna2DataGridView3_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		SaveSelectedGameFilter();
	}

	private void guna2DataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (selectedRows.Contains(e.RowIndex))
		{
			selectedRows.Remove(e.RowIndex);
		}
		else
		{
			selectedRows.Add(e.RowIndex);
		}
		foreach (DataGridViewRow item in DGVfilterList.Rows.Cast<DataGridViewRow>())
		{
			item.Selected = false;
		}
		foreach (int select in selectedRows)
		{
			DGVfilterList.Rows[select].Selected = true;
		}
	}

	private void guna2Button6_Click(object sender, EventArgs e)
	{
		ChangeBackgroundImage();
	}

	private void guna2Button1_Click_2(object sender, EventArgs e)
	{
		string appDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZOPZSNIFF");
		string textFilePath = Path.Combine(appDataPath, "image_directory.txt");
		if (!Directory.Exists(appDataPath))
		{
			Directory.CreateDirectory(appDataPath);
		}
		if (File.Exists(textFilePath))
		{
			File.WriteAllText(textFilePath, string.Empty);
		}
		SNIFF.Properties.Settings.Default.BackgoundPic = string.Empty;
		SNIFF.Properties.Settings.Default.Save();
		ShowNotification("Background image Cleared");
	}

	private async void guna2TextBox1_TextChanged(object sender, EventArgs e)
	{
		_lastChangeTime = DateTime.Now;
		await Task.Delay(200);
		if (!(_lastChangeTime.AddMilliseconds(150.0) > DateTime.Now))
		{
			string searchText = guna2TextBox1.Text.ToLower();
			FilterRows(searchText);
		}
	}

	private void FilterRows(string filterText)
	{
		List<SNIFF.Classes.Auth.Models.Label> filteredEntries = new List<SNIFF.Classes.Auth.Models.Label>();
		if (Global.Labels != null)
		{
			foreach (SNIFF.Classes.Auth.Models.Label label in Global.Labels)
			{
				if (label.Name.ToLower().Contains(filterText))
				{
					filteredEntries.Add(label);
				}
			}
		}
		try
		{
			Invoke((MethodInvoker)delegate
			{
				DataTable dataTable = CreateDataTableFromLabels(filteredEntries);
				PopulateDataGridView(dataTable);
			});
		}
		catch
		{
		}
	}

	private void FilterRowss(string filterText)
	{
		foreach (DataGridViewRow dataGridViewRow in (IEnumerable)DGVfilterList.Rows)
		{
			if (!dataGridViewRow.IsNewRow)
			{
				bool visible = dataGridViewRow.Cells[0].Value.ToString().ToLower().StartsWith(filterText);
				dataGridViewRow.Visible = visible;
			}
		}
	}

	private void ShowAllRowss()
	{
		foreach (DataGridViewRow dataGridViewRow in (IEnumerable)DGVfilterList.Rows)
		{
			if (!dataGridViewRow.IsNewRow)
			{
				dataGridViewRow.Visible = true;
			}
		}
	}

	private void guna2DataGridView3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
		{
			string filterText = ((char)e.KeyCode).ToString().ToLower();
			FilterRowss(filterText);
			e.SuppressKeyPress = true;
		}
		else if (e.KeyCode == Keys.Space)
		{
			ShowAllRowss();
			e.SuppressKeyPress = true;
		}
	}

	private void editLabelToolStripMenuItem_Click(object sender, EventArgs e)
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

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color = ColorTranslator.FromHtml(savedColor);
				guna2GroupBox2.CustomBorderColor = color;
				guna2TabControl1.TabMenuBackColor = color;
				guna2GroupBox4.CustomBorderColor = color;
				guna2GroupBox3.CustomBorderColor = color;
				guna2GroupBox7.CustomBorderColor = color;
				panel2.BackColor = color;
				guna2GroupBox1.CustomBorderColor = color;
				guna2Button2.BackColor = color;
				guna2Button2.FillColor = color;
				guna2Button2.PressedColor = color;
				Messageboxtb.BackColor = color;
				Messageboxtb.FillColor = color;
				host.FillColor = color;
				host.BackColor = color;
				guna2TextBox1.BackColor = color;
				guna2TextBox1.FillColor = color;
			}
			catch (Exception ex)
			{
				ShowNotification(ex.Message ?? "");
			}
		}
	}

	private void guna2Button4_Click(object sender, EventArgs e)
	{
		if (colorDialog1.ShowDialog() == DialogResult.OK)
		{
			Color selectedColor = colorDialog1.Color;
			string colorHex = ColorTranslator.ToHtml(selectedColor);
			guna2GroupBox2.CustomBorderColor = selectedColor;
			guna2TabControl1.TabMenuBackColor = selectedColor;
			guna2GroupBox4.CustomBorderColor = selectedColor;
			guna2GroupBox3.CustomBorderColor = selectedColor;
			guna2GroupBox1.CustomBorderColor = selectedColor;
			SNIFF.Properties.Settings.Default.BackgroundColor = colorHex;
			SNIFF.Properties.Settings.Default.Save();
			ShowNotification("Background color saved successfully!");
		}
	}

	private void guna2Button2_Click_1(object sender, EventArgs e)
	{
		SNIFF.Properties.Settings.Default.BackgroundColor = null;
		SNIFF.Properties.Settings.Default.Save();
		ApplyBackgroundColor();
	}

	private void guna2Panel1_Paint_1(object sender, PaintEventArgs e)
	{
	}

	private void guna2DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void host_TextChanged(object sender, EventArgs e)
	{
	}

	private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void Menu_FormClosing(object sender, FormClosingEventArgs e)
	{
		SaveSelectedGameFilter();
	}

	private async void guna2TextBox1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		e.SuppressKeyPress = true;
		string ipAddress = host.Text;
		if (ValidateIP(ipAddress))
		{
			SNIFF.Classes.Auth.Models.Label label = new SNIFF.Classes.Auth.Models.Label
			{
				Name = guna2TextBox1.Text,
				Value = ipAddress
			};
			//BaseAuthenticationResponse<object> resp = await Global.Authenticator.CreateLabelAsync(label);
			
			Global.Labels.Add(label);
			guna2TextBox1_TextChanged(null, null);
		}
		else
		{
			ShowNotification("Invalid IP address.");
		}
	}

	private void guna2CheckBox5_CheckedChanged_1(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.FilteredGames = checkbox.Checked;
		settingsModel.Save();
	}

	private void guna2CheckBox6_CheckedChanged(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Playstation = checkbox.Checked;
		settingsModel.Save();
	}

	private void xboxtabshow_CheckedChanged(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Xbox = checkbox.Checked;
		settingsModel.Save();
	}

	private void otherinfoshow_CheckedChanged(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Otherinfo = checkbox.Checked;
		settingsModel.Save();
	}

	private void xbltoolinfo_CheckedChanged(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.XBLTool = checkbox.Checked;
		settingsModel.Save();
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		notification.Hide();
		timer2.Stop();
	}

	private void LoginBTN_Click(object sender, EventArgs e)
	{
	}

	public void ApplyTheme(Theme theme)
	{
		BackColor = ColorTranslator.FromHtml(theme.BackgroundColor);
		ForeColor = ColorTranslator.FromHtml(theme.TextColor);
		foreach (Control control in base.Controls)
		{
			control.ForeColor = ColorTranslator.FromHtml(theme.TextColor);
			if (control is Button button)
			{
				button.BackColor = ColorTranslator.FromHtml(theme.ButtonColor);
			}
			control.Font = new Font(control.Font.FontFamily, float.Parse(theme.FontSize));
		}
	}

	public Theme LoadTheme(string filePath)
	{
		return JsonConvert.DeserializeObject<Theme>(File.ReadAllText(filePath));
	}

	private void DGVfilterList_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
	{
		filterDescriptionToolTip.Hide(this);
		if (e.RowIndex != -1)
		{
			string filterName = DGVfilterList.Rows[e.RowIndex].Cells[0].Value.ToString();
			Filter filter = Filters.FirstOrDefault((Filter x) => x.Name == filterName);
			if (filter != null)
			{
				Point mousePosition = PointToClient(Control.MousePosition);
				filterDescriptionToolTip.Show(filter.Description, this, mousePosition.X + 25, mousePosition.Y - 25);
			}
		}
	}

	private void DGVfilterList_CellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
	{
	}

	private void DGVfilterList_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void guna2GroupBox2_Click(object sender, EventArgs e)
	{
	}

	private void logInContextMenu1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void guna2GroupBox7_Click(object sender, EventArgs e)
	{
	}

	private async void Messageboxtb_KeyDown(object sender, KeyEventArgs e)
	{
		if (!(_lastSend.AddSeconds(5.0) > DateTime.Now) && e.KeyCode == Keys.Return)
		{
			e.Handled = true;
			e.SuppressKeyPress = true;
			if (Messageboxtb.Text.Length > 119)
			{
				ShowNotification("Your message exceeds the 119 character limit. Please shorten it");
				return;
			}
			await chatClient.SendMessageAsync(Messageboxtb.Text);
			Messageboxtb.Text = "";
		}
	}

	private void chatPanel_Paint(object sender, PaintEventArgs e)
	{
	}

	private void Messageboxtb_TextChanged(object sender, EventArgs e)
	{
	}

	private void pctabshow_CheckedChanged(object sender, EventArgs e)
	{
		Guna2CheckBox checkbox = (Guna2CheckBox)sender;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Pctab = checkbox.Checked;
		settingsModel.Save();
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.t));
		this.label9 = new System.Windows.Forms.Label();
		this.AppVersion = new System.Windows.Forms.Label();
		this.totaluserslabel = new System.Windows.Forms.Label();
		this.label6 = new System.Windows.Forms.Label();
		this.maxboottime = new System.Windows.Forms.Label();
		this.pictureBox1 = new System.Windows.Forms.PictureBox();
		this.usernameLabel = new System.Windows.Forms.Label();
		this.label7 = new System.Windows.Forms.Label();
		this.RankLabel = new System.Windows.Forms.Label();
		this.label5 = new System.Windows.Forms.Label();
		this.exDate = new System.Windows.Forms.Label();
		this.label8 = new System.Windows.Forms.Label();
		this.guna2VScrollBar2 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.dataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.logInContextMenu2 = new LoginTheme.LogInContextMenu();
		this.copyPacketsNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.editLabelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
		this.host = new Guna.UI2.WinForms.Guna2TextBox();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.DragControl_Form = new Guna.UI.WinForms.GunaDragControl(this.components);
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.guna2TabControl1 = new Guna.UI2.WinForms.Guna2TabControl();
		this.tabPage6 = new System.Windows.Forms.TabPage();
		this.guna2GroupBox4 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2CheckBox4 = new Guna.UI2.WinForms.Guna2CheckBox();
		this.guna2CheckBox3 = new Guna.UI2.WinForms.Guna2CheckBox();
		this.guna2CheckBox2 = new Guna.UI2.WinForms.Guna2CheckBox();
		this.guna2CheckBox1 = new Guna.UI2.WinForms.Guna2CheckBox();
		this.Remebermecheck = new Guna.UI2.WinForms.Guna2CheckBox();
		this.guna2GroupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.label2 = new System.Windows.Forms.Label();
		this.label3 = new System.Windows.Forms.Label();
		this.tabPage1 = new System.Windows.Forms.TabPage();
		this.guna2GroupBox7 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.pctabshow = new Guna.UI2.WinForms.Guna2CheckBox();
		this.xbltoolinfo = new Guna.UI2.WinForms.Guna2CheckBox();
		this.filteredgamestabshow = new Guna.UI2.WinForms.Guna2CheckBox();
		this.xboxtabshow = new Guna.UI2.WinForms.Guna2CheckBox();
		this.playstationtabshow = new Guna.UI2.WinForms.Guna2CheckBox();
		this.otherinfoshow = new Guna.UI2.WinForms.Guna2CheckBox();
		this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
		this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
		this.tabPage7 = new System.Windows.Forms.TabPage();
		this.tabPage3 = new System.Windows.Forms.TabPage();
		this.guna2VScrollBar4 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.DGVfilterList = new Guna.UI2.WinForms.Guna2DataGridView();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.tabPage2 = new System.Windows.Forms.TabPage();
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.chatPanel = new System.Windows.Forms.FlowLayoutPanel();
		this.panel1 = new System.Windows.Forms.Panel();
		this.Messageboxtb = new Guna.UI2.WinForms.Guna2TextBox();
		this.logInContextMenu1 = new LoginTheme.LogInContextMenu();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.colorDialog1 = new System.Windows.Forms.ColorDialog();
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.filterDescriptionToolTip = new Guna.UI2.WinForms.Guna2HtmlToolTip();
		this.guna2Elipse12 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse4 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse5 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2HtmlToolTip1 = new Guna.UI2.WinForms.Guna2HtmlToolTip();
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.logInContextMenu2.SuspendLayout();
		this.guna2TabControl1.SuspendLayout();
		this.tabPage6.SuspendLayout();
		this.guna2GroupBox4.SuspendLayout();
		this.guna2GroupBox3.SuspendLayout();
		this.guna2GroupBox1.SuspendLayout();
		this.tabPage1.SuspendLayout();
		this.guna2GroupBox7.SuspendLayout();
		this.guna2GroupBox2.SuspendLayout();
		this.tabPage7.SuspendLayout();
		this.tabPage3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGVfilterList).BeginInit();
		this.tabPage2.SuspendLayout();
		this.panel1.SuspendLayout();
		this.logInContextMenu1.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.label9.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label9.ForeColor = System.Drawing.Color.White;
		this.label9.Location = new System.Drawing.Point(591, 89);
		this.label9.Name = "label9";
		this.label9.Size = new System.Drawing.Size(122, 19);
		this.label9.TabIndex = 17;
		this.label9.Text = "App Version";
		this.label9.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.AppVersion.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.AppVersion.ForeColor = System.Drawing.Color.White;
		this.AppVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.AppVersion.Location = new System.Drawing.Point(591, 118);
		this.AppVersion.Name = "AppVersion";
		this.AppVersion.Size = new System.Drawing.Size(122, 19);
		this.AppVersion.TabIndex = 18;
		this.AppVersion.Text = "lol";
		this.AppVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.AppVersion.Click += new System.EventHandler(AppVersion_Click);
		this.totaluserslabel.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.totaluserslabel.ForeColor = System.Drawing.Color.White;
		this.totaluserslabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.totaluserslabel.Location = new System.Drawing.Point(90, 118);
		this.totaluserslabel.Name = "totaluserslabel";
		this.totaluserslabel.Size = new System.Drawing.Size(122, 19);
		this.totaluserslabel.TabIndex = 6;
		this.totaluserslabel.Text = "0";
		this.totaluserslabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label6.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label6.ForeColor = System.Drawing.Color.White;
		this.label6.Location = new System.Drawing.Point(90, 89);
		this.label6.Name = "label6";
		this.label6.Size = new System.Drawing.Size(122, 19);
		this.label6.TabIndex = 5;
		this.label6.Text = "Total Clients";
		this.label6.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.maxboottime.BackColor = System.Drawing.Color.Black;
		this.maxboottime.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.maxboottime.ForeColor = System.Drawing.Color.White;
		this.maxboottime.Location = new System.Drawing.Point(97, 203);
		this.maxboottime.Name = "maxboottime";
		this.maxboottime.RightToLeft = System.Windows.Forms.RightToLeft.No;
		this.maxboottime.Size = new System.Drawing.Size(132, 21);
		this.maxboottime.TabIndex = 13;
		this.maxboottime.Text = "0";
		this.maxboottime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.maxboottime.Click += new System.EventHandler(maxboottime_Click);
		this.pictureBox1.BackColor = System.Drawing.Color.Black;
		this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
		this.pictureBox1.ErrorImage = null;
		this.pictureBox1.InitialImage = null;
		this.pictureBox1.Location = new System.Drawing.Point(74, 52);
		this.pictureBox1.Name = "pictureBox1";
		this.pictureBox1.Size = new System.Drawing.Size(86, 43);
		this.pictureBox1.TabIndex = 131;
		this.pictureBox1.TabStop = false;
		this.pictureBox1.Click += new System.EventHandler(pictureBox1_Click);
		this.usernameLabel.BackColor = System.Drawing.Color.Black;
		this.usernameLabel.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.usernameLabel.ForeColor = System.Drawing.Color.White;
		this.usernameLabel.Location = new System.Drawing.Point(9, 104);
		this.usernameLabel.Name = "usernameLabel";
		this.usernameLabel.Size = new System.Drawing.Size(219, 21);
		this.usernameLabel.TabIndex = 3;
		this.usernameLabel.Text = "Username";
		this.usernameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label7.BackColor = System.Drawing.Color.Black;
		this.label7.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label7.ForeColor = System.Drawing.Color.White;
		this.label7.Location = new System.Drawing.Point(12, 169);
		this.label7.Name = "label7";
		this.label7.Size = new System.Drawing.Size(88, 21);
		this.label7.TabIndex = 7;
		this.label7.Text = "Plan";
		this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.RankLabel.BackColor = System.Drawing.Color.Black;
		this.RankLabel.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.RankLabel.ForeColor = System.Drawing.Color.White;
		this.RankLabel.Location = new System.Drawing.Point(66, 169);
		this.RankLabel.Name = "RankLabel";
		this.RankLabel.Size = new System.Drawing.Size(163, 21);
		this.RankLabel.TabIndex = 10;
		this.RankLabel.Text = "ShadowGarden";
		this.RankLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.label5.BackColor = System.Drawing.Color.Black;
		this.label5.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label5.ForeColor = System.Drawing.Color.White;
		this.label5.Location = new System.Drawing.Point(12, 203);
		this.label5.Name = "label5";
		this.label5.Size = new System.Drawing.Size(119, 21);
		this.label5.TabIndex = 12;
		this.label5.Text = "Last Login";
		this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.exDate.BackColor = System.Drawing.Color.Black;
		this.exDate.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.exDate.ForeColor = System.Drawing.Color.White;
		this.exDate.Location = new System.Drawing.Point(70, 137);
		this.exDate.Name = "exDate";
		this.exDate.Size = new System.Drawing.Size(158, 21);
		this.exDate.TabIndex = 9;
		this.exDate.Text = "Lifetime";
		this.exDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.exDate.Click += new System.EventHandler(exDate_Click);
		this.label8.BackColor = System.Drawing.Color.Black;
		this.label8.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label8.ForeColor = System.Drawing.Color.White;
		this.label8.Location = new System.Drawing.Point(12, 137);
		this.label8.Name = "label8";
		this.label8.Size = new System.Drawing.Size(88, 21);
		this.label8.TabIndex = 5;
		this.label8.Text = "Expiry";
		this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.guna2VScrollBar2.BindingContainer = this.dataGridView1;
		this.guna2VScrollBar2.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar2.InUpdate = false;
		this.guna2VScrollBar2.LargeChange = 10;
		this.guna2VScrollBar2.Location = new System.Drawing.Point(815, 3);
		this.guna2VScrollBar2.Minimum = 1;
		this.guna2VScrollBar2.Name = "guna2VScrollBar2";
		this.guna2VScrollBar2.ScrollbarSize = 18;
		this.guna2VScrollBar2.Size = new System.Drawing.Size(18, 378);
		this.guna2VScrollBar2.TabIndex = 104;
		this.guna2VScrollBar2.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar2.ThumbSize = 5f;
		this.guna2VScrollBar2.Value = 10;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.ColumnHeadersHeight = 20;
		this.dataGridView1.ContextMenuStrip = this.logInContextMenu2;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
		this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.dataGridView1.Location = new System.Drawing.Point(3, 3);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.dataGridView1.RowHeadersVisible = false;
		this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
		this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
		this.dataGridView1.RowTemplate.Height = 35;
		this.dataGridView1.Size = new System.Drawing.Size(830, 378);
		this.dataGridView1.TabIndex = 164;
		this.dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
		this.dataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
		this.dataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
		this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
		this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
		this.dataGridView1.ThemeStyle.BackColor = System.Drawing.Color.Black;
		this.dataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.dataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(100, 88, 255);
		this.dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		this.dataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
		this.dataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.dataGridView1.ThemeStyle.HeaderStyle.Height = 20;
		this.dataGridView1.ThemeStyle.ReadOnly = true;
		this.dataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
		this.dataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
		this.dataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		this.dataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.dataGridView1.ThemeStyle.RowsStyle.Height = 35;
		this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick_1);
		this.logInContextMenu2.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu2.FontColour = System.Drawing.Color.White;
		this.logInContextMenu2.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.copyPacketsNumberToolStripMenuItem, this.editLabelToolStripMenuItem });
		this.logInContextMenu2.Name = "logInContextMenu2";
		this.logInContextMenu2.ShowImageMargin = false;
		this.logInContextMenu2.Size = new System.Drawing.Size(148, 48);
		this.logInContextMenu2.Opening += new System.ComponentModel.CancelEventHandler(logInContextMenu2_Opening);
		this.copyPacketsNumberToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.copyPacketsNumberToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.copyPacketsNumberToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.copyPacketsNumberToolStripMenuItem.Name = "copyPacketsNumberToolStripMenuItem";
		this.copyPacketsNumberToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
		this.copyPacketsNumberToolStripMenuItem.Text = "Copy To Clipboard";
		this.copyPacketsNumberToolStripMenuItem.Click += new System.EventHandler(copyPacketsNumberToolStripMenuItem_Click);
		this.editLabelToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.editLabelToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.editLabelToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.editLabelToolStripMenuItem.Name = "editLabelToolStripMenuItem";
		this.editLabelToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
		this.editLabelToolStripMenuItem.Text = "Edit Label";
		this.editLabelToolStripMenuItem.Click += new System.EventHandler(editLabelToolStripMenuItem_Click);
		this.guna2TextBox1.Animated = true;
		this.guna2TextBox1.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.BorderThickness = 0;
		this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.guna2TextBox1.DefaultText = "";
		this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.FocusedState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2TextBox1.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Location = new System.Drawing.Point(422, 387);
		this.guna2TextBox1.Name = "guna2TextBox1";
		this.guna2TextBox1.PasswordChar = '\0';
		this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.PlaceholderText = "Label";
		this.guna2TextBox1.SelectedText = "";
		this.guna2TextBox1.Size = new System.Drawing.Size(413, 36);
		this.guna2TextBox1.TabIndex = 141;
		this.guna2TextBox1.TextChanged += new System.EventHandler(guna2TextBox1_TextChanged);
		this.guna2TextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2TextBox1_KeyDown);
		this.host.Animated = true;
		this.host.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.host.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.host.BorderThickness = 0;
		this.host.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.host.DefaultText = "";
		this.host.DisabledState.ForeColor = System.Drawing.Color.White;
		this.host.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.host.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.host.FocusedState.ForeColor = System.Drawing.Color.White;
		this.host.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.host.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.host.ForeColor = System.Drawing.Color.White;
		this.host.HoverState.ForeColor = System.Drawing.Color.White;
		this.host.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.host.Location = new System.Drawing.Point(3, 387);
		this.host.Name = "host";
		this.host.PasswordChar = '\0';
		this.host.PlaceholderForeColor = System.Drawing.Color.White;
		this.host.PlaceholderText = "IP Address";
		this.host.SelectedText = "";
		this.host.Size = new System.Drawing.Size(413, 36);
		this.host.TabIndex = 139;
		this.host.TextChanged += new System.EventHandler(host_TextChanged);
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.DragControl_Form.TargetControl = null;
		this.timer1.Enabled = true;
		this.timer1.Interval = 1;
		this.timer1.Tick += new System.EventHandler(timer1_Tick);
		this.guna2TabControl1.Controls.Add(this.tabPage6);
		this.guna2TabControl1.Controls.Add(this.tabPage1);
		this.guna2TabControl1.Controls.Add(this.tabPage7);
		this.guna2TabControl1.Controls.Add(this.tabPage3);
		this.guna2TabControl1.Controls.Add(this.tabPage2);
		this.guna2TabControl1.ImageList = this.imageList1;
		this.guna2TabControl1.ItemSize = new System.Drawing.Size(150, 40);
		this.guna2TabControl1.Location = new System.Drawing.Point(0, 34);
		this.guna2TabControl1.Name = "guna2TabControl1";
		this.guna2TabControl1.SelectedIndex = 0;
		this.guna2TabControl1.Size = new System.Drawing.Size(844, 475);
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
		this.guna2TabControl1.TabButtonSize = new System.Drawing.Size(150, 40);
		this.guna2TabControl1.TabIndex = 157;
		this.guna2TabControl1.TabMenuBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TabControl1.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
		this.tabPage6.BackColor = System.Drawing.Color.Black;
		this.tabPage6.Controls.Add(this.guna2GroupBox4);
		this.tabPage6.Controls.Add(this.guna2GroupBox3);
		this.tabPage6.Controls.Add(this.guna2GroupBox1);
		this.tabPage6.Location = new System.Drawing.Point(4, 44);
		this.tabPage6.Name = "tabPage6";
		this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage6.Size = new System.Drawing.Size(836, 427);
		this.tabPage6.TabIndex = 0;
		this.tabPage6.Text = "General";
		this.guna2GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.guna2GroupBox4.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox4.BorderThickness = 0;
		this.guna2GroupBox4.Controls.Add(this.guna2CheckBox4);
		this.guna2GroupBox4.Controls.Add(this.guna2CheckBox3);
		this.guna2GroupBox4.Controls.Add(this.guna2CheckBox2);
		this.guna2GroupBox4.Controls.Add(this.guna2CheckBox1);
		this.guna2GroupBox4.Controls.Add(this.Remebermecheck);
		this.guna2GroupBox4.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox4.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox4.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox4.Location = new System.Drawing.Point(3, 3);
		this.guna2GroupBox4.Name = "guna2GroupBox4";
		this.guna2GroupBox4.Size = new System.Drawing.Size(587, 236);
		this.guna2GroupBox4.TabIndex = 719;
		this.guna2GroupBox4.Text = "General Settings";
		this.guna2GroupBox4.Click += new System.EventHandler(guna2GroupBox4_Click);
		this.guna2CheckBox4.Animated = true;
		this.guna2CheckBox4.Checked = true;
		this.guna2CheckBox4.CheckedState.BorderColor = System.Drawing.Color.White;
		this.guna2CheckBox4.CheckedState.BorderRadius = 2;
		this.guna2CheckBox4.CheckedState.BorderThickness = 0;
		this.guna2CheckBox4.CheckedState.FillColor = System.Drawing.Color.Black;
		this.guna2CheckBox4.CheckState = System.Windows.Forms.CheckState.Checked;
		this.guna2CheckBox4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2CheckBox4.ForeColor = System.Drawing.Color.White;
		this.guna2CheckBox4.Location = new System.Drawing.Point(14, 154);
		this.guna2CheckBox4.Name = "guna2CheckBox4";
		this.guna2CheckBox4.Size = new System.Drawing.Size(198, 19);
		this.guna2CheckBox4.TabIndex = 166;
		this.guna2CheckBox4.Text = "Save Last Selected Adapter";
		this.guna2CheckBox4.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox4.UncheckedState.BorderRadius = 2;
		this.guna2CheckBox4.UncheckedState.BorderThickness = 0;
		this.guna2CheckBox4.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox4.CheckedChanged += new System.EventHandler(guna2CheckBox4_CheckedChanged);
		this.guna2CheckBox3.Animated = true;
		this.guna2CheckBox3.CheckedState.BorderColor = System.Drawing.Color.White;
		this.guna2CheckBox3.CheckedState.BorderRadius = 2;
		this.guna2CheckBox3.CheckedState.BorderThickness = 0;
		this.guna2CheckBox3.CheckedState.FillColor = System.Drawing.Color.Black;
		this.guna2CheckBox3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2CheckBox3.ForeColor = System.Drawing.Color.White;
		this.guna2CheckBox3.Location = new System.Drawing.Point(14, 189);
		this.guna2CheckBox3.Name = "guna2CheckBox3";
		this.guna2CheckBox3.Size = new System.Drawing.Size(198, 19);
		this.guna2CheckBox3.TabIndex = 165;
		this.guna2CheckBox3.Text = "Auto Login";
		this.guna2CheckBox3.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox3.UncheckedState.BorderRadius = 2;
		this.guna2CheckBox3.UncheckedState.BorderThickness = 0;
		this.guna2CheckBox3.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox3.CheckedChanged += new System.EventHandler(guna2CheckBox3_CheckedChanged);
		this.guna2CheckBox2.Animated = true;
		this.guna2CheckBox2.CheckedState.BorderColor = System.Drawing.Color.White;
		this.guna2CheckBox2.CheckedState.BorderRadius = 2;
		this.guna2CheckBox2.CheckedState.BorderThickness = 0;
		this.guna2CheckBox2.CheckedState.FillColor = System.Drawing.Color.Black;
		this.guna2CheckBox2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2CheckBox2.ForeColor = System.Drawing.Color.White;
		this.guna2CheckBox2.Location = new System.Drawing.Point(14, 120);
		this.guna2CheckBox2.Name = "guna2CheckBox2";
		this.guna2CheckBox2.Size = new System.Drawing.Size(198, 19);
		this.guna2CheckBox2.TabIndex = 164;
		this.guna2CheckBox2.Text = "Stay On Top of All Windows";
		this.guna2CheckBox2.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox2.UncheckedState.BorderRadius = 2;
		this.guna2CheckBox2.UncheckedState.BorderThickness = 0;
		this.guna2CheckBox2.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox2.CheckedChanged += new System.EventHandler(guna2CheckBox2_CheckedChanged);
		this.guna2CheckBox1.Animated = true;
		this.guna2CheckBox1.CheckedState.BorderColor = System.Drawing.Color.White;
		this.guna2CheckBox1.CheckedState.BorderRadius = 2;
		this.guna2CheckBox1.CheckedState.BorderThickness = 0;
		this.guna2CheckBox1.CheckedState.FillColor = System.Drawing.Color.Black;
		this.guna2CheckBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2CheckBox1.ForeColor = System.Drawing.Color.White;
		this.guna2CheckBox1.Location = new System.Drawing.Point(14, 86);
		this.guna2CheckBox1.Name = "guna2CheckBox1";
		this.guna2CheckBox1.Size = new System.Drawing.Size(198, 19);
		this.guna2CheckBox1.TabIndex = 163;
		this.guna2CheckBox1.Text = "Use Discord Rich Presence";
		this.guna2CheckBox1.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox1.UncheckedState.BorderRadius = 2;
		this.guna2CheckBox1.UncheckedState.BorderThickness = 0;
		this.guna2CheckBox1.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.guna2CheckBox1.CheckedChanged += new System.EventHandler(guna2CheckBox1_CheckedChanged);
		this.Remebermecheck.Animated = true;
		this.Remebermecheck.CheckedState.BorderColor = System.Drawing.Color.White;
		this.Remebermecheck.CheckedState.BorderRadius = 2;
		this.Remebermecheck.CheckedState.BorderThickness = 0;
		this.Remebermecheck.CheckedState.FillColor = System.Drawing.Color.Black;
		this.Remebermecheck.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.Remebermecheck.ForeColor = System.Drawing.Color.White;
		this.Remebermecheck.Location = new System.Drawing.Point(14, 52);
		this.Remebermecheck.Name = "Remebermecheck";
		this.Remebermecheck.Size = new System.Drawing.Size(198, 19);
		this.Remebermecheck.TabIndex = 162;
		this.Remebermecheck.Text = "Hardware Acceleraton";
		this.Remebermecheck.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.Remebermecheck.UncheckedState.BorderRadius = 2;
		this.Remebermecheck.UncheckedState.BorderThickness = 0;
		this.Remebermecheck.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.Remebermecheck.CheckedChanged += new System.EventHandler(Remebermecheck_CheckedChanged);
		this.guna2GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.guna2GroupBox3.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox3.BorderThickness = 0;
		this.guna2GroupBox3.Controls.Add(this.maxboottime);
		this.guna2GroupBox3.Controls.Add(this.label8);
		this.guna2GroupBox3.Controls.Add(this.pictureBox1);
		this.guna2GroupBox3.Controls.Add(this.exDate);
		this.guna2GroupBox3.Controls.Add(this.usernameLabel);
		this.guna2GroupBox3.Controls.Add(this.label7);
		this.guna2GroupBox3.Controls.Add(this.label5);
		this.guna2GroupBox3.Controls.Add(this.RankLabel);
		this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox3.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox3.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox3.Location = new System.Drawing.Point(596, 3);
		this.guna2GroupBox3.Name = "guna2GroupBox3";
		this.guna2GroupBox3.Size = new System.Drawing.Size(237, 236);
		this.guna2GroupBox3.TabIndex = 719;
		this.guna2GroupBox3.Text = "Account Details";
		this.guna2GroupBox3.Click += new System.EventHandler(guna2GroupBox3_Click);
		this.guna2GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
		this.guna2GroupBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox1.BorderThickness = 0;
		this.guna2GroupBox1.Controls.Add(this.label2);
		this.guna2GroupBox1.Controls.Add(this.label3);
		this.guna2GroupBox1.Controls.Add(this.label9);
		this.guna2GroupBox1.Controls.Add(this.label6);
		this.guna2GroupBox1.Controls.Add(this.totaluserslabel);
		this.guna2GroupBox1.Controls.Add(this.AppVersion);
		this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox1.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox1.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox1.Location = new System.Drawing.Point(3, 245);
		this.guna2GroupBox1.Name = "guna2GroupBox1";
		this.guna2GroupBox1.Size = new System.Drawing.Size(830, 179);
		this.guna2GroupBox1.TabIndex = 717;
		this.guna2GroupBox1.Text = "ZOPZ SNIFF Information ";
		this.label2.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label2.ForeColor = System.Drawing.Color.White;
		this.label2.Location = new System.Drawing.Point(332, 89);
		this.label2.Name = "label2";
		this.label2.Size = new System.Drawing.Size(122, 19);
		this.label2.TabIndex = 19;
		this.label2.Text = "Total With Plan";
		this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.label3.Font = new System.Drawing.Font("Segoe UI", 10f, System.Drawing.FontStyle.Bold);
		this.label3.ForeColor = System.Drawing.Color.White;
		this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.label3.Location = new System.Drawing.Point(332, 118);
		this.label3.Name = "label3";
		this.label3.Size = new System.Drawing.Size(122, 19);
		this.label3.TabIndex = 20;
		this.label3.Text = "0";
		this.label3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
		this.tabPage1.BackColor = System.Drawing.Color.Black;
		this.tabPage1.Controls.Add(this.guna2GroupBox7);
		this.tabPage1.Controls.Add(this.guna2GroupBox2);
		this.tabPage1.Location = new System.Drawing.Point(4, 44);
		this.tabPage1.Name = "tabPage1";
		this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage1.Size = new System.Drawing.Size(836, 427);
		this.tabPage1.TabIndex = 6;
		this.tabPage1.Text = "Interface";
		this.guna2GroupBox7.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox7.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox7.BorderThickness = 0;
		this.guna2GroupBox7.Controls.Add(this.pctabshow);
		this.guna2GroupBox7.Controls.Add(this.xbltoolinfo);
		this.guna2GroupBox7.Controls.Add(this.filteredgamestabshow);
		this.guna2GroupBox7.Controls.Add(this.xboxtabshow);
		this.guna2GroupBox7.Controls.Add(this.playstationtabshow);
		this.guna2GroupBox7.Controls.Add(this.otherinfoshow);
		this.guna2GroupBox7.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox7.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox7.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox7.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox7.Location = new System.Drawing.Point(3, 223);
		this.guna2GroupBox7.Name = "guna2GroupBox7";
		this.guna2GroupBox7.Size = new System.Drawing.Size(830, 201);
		this.guna2GroupBox7.TabIndex = 722;
		this.guna2GroupBox7.Text = "Show Tabs";
		this.guna2GroupBox7.Click += new System.EventHandler(guna2GroupBox7_Click);
		this.pctabshow.Animated = true;
		this.pctabshow.BackColor = System.Drawing.Color.Black;
		this.pctabshow.Checked = true;
		this.pctabshow.CheckedState.BorderColor = System.Drawing.Color.White;
		this.pctabshow.CheckedState.BorderRadius = 2;
		this.pctabshow.CheckedState.BorderThickness = 0;
		this.pctabshow.CheckedState.FillColor = System.Drawing.Color.Black;
		this.pctabshow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.pctabshow.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.pctabshow.ForeColor = System.Drawing.Color.White;
		this.pctabshow.Location = new System.Drawing.Point(5, 172);
		this.pctabshow.Name = "pctabshow";
		this.pctabshow.Size = new System.Drawing.Size(198, 19);
		this.pctabshow.TabIndex = 172;
		this.pctabshow.Text = "PC";
		this.pctabshow.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.pctabshow.UncheckedState.BorderRadius = 2;
		this.pctabshow.UncheckedState.BorderThickness = 0;
		this.pctabshow.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.pctabshow.UseVisualStyleBackColor = false;
		this.pctabshow.CheckedChanged += new System.EventHandler(pctabshow_CheckedChanged);
		this.xbltoolinfo.Animated = true;
		this.xbltoolinfo.BackColor = System.Drawing.Color.Black;
		this.xbltoolinfo.Checked = true;
		this.xbltoolinfo.CheckedState.BorderColor = System.Drawing.Color.White;
		this.xbltoolinfo.CheckedState.BorderRadius = 2;
		this.xbltoolinfo.CheckedState.BorderThickness = 0;
		this.xbltoolinfo.CheckedState.FillColor = System.Drawing.Color.Black;
		this.xbltoolinfo.CheckState = System.Windows.Forms.CheckState.Checked;
		this.xbltoolinfo.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.xbltoolinfo.ForeColor = System.Drawing.Color.White;
		this.xbltoolinfo.Location = new System.Drawing.Point(5, 148);
		this.xbltoolinfo.Name = "xbltoolinfo";
		this.xbltoolinfo.Size = new System.Drawing.Size(198, 19);
		this.xbltoolinfo.TabIndex = 171;
		this.xbltoolinfo.Text = "XBL Tool";
		this.xbltoolinfo.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.xbltoolinfo.UncheckedState.BorderRadius = 2;
		this.xbltoolinfo.UncheckedState.BorderThickness = 0;
		this.xbltoolinfo.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.xbltoolinfo.UseVisualStyleBackColor = false;
		this.xbltoolinfo.CheckedChanged += new System.EventHandler(xbltoolinfo_CheckedChanged);
		this.filteredgamestabshow.Animated = true;
		this.filteredgamestabshow.BackColor = System.Drawing.Color.Black;
		this.filteredgamestabshow.Checked = true;
		this.filteredgamestabshow.CheckedState.BorderColor = System.Drawing.Color.White;
		this.filteredgamestabshow.CheckedState.BorderRadius = 2;
		this.filteredgamestabshow.CheckedState.BorderThickness = 0;
		this.filteredgamestabshow.CheckedState.FillColor = System.Drawing.Color.Black;
		this.filteredgamestabshow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.filteredgamestabshow.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.filteredgamestabshow.ForeColor = System.Drawing.Color.White;
		this.filteredgamestabshow.Location = new System.Drawing.Point(5, 52);
		this.filteredgamestabshow.Name = "filteredgamestabshow";
		this.filteredgamestabshow.Size = new System.Drawing.Size(198, 19);
		this.filteredgamestabshow.TabIndex = 167;
		this.filteredgamestabshow.Text = "Filtered Games";
		this.filteredgamestabshow.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.filteredgamestabshow.UncheckedState.BorderRadius = 2;
		this.filteredgamestabshow.UncheckedState.BorderThickness = 0;
		this.filteredgamestabshow.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.filteredgamestabshow.UseVisualStyleBackColor = false;
		this.filteredgamestabshow.CheckedChanged += new System.EventHandler(guna2CheckBox5_CheckedChanged_1);
		this.xboxtabshow.Animated = true;
		this.xboxtabshow.BackColor = System.Drawing.Color.Black;
		this.xboxtabshow.Checked = true;
		this.xboxtabshow.CheckedState.BorderColor = System.Drawing.Color.White;
		this.xboxtabshow.CheckedState.BorderRadius = 2;
		this.xboxtabshow.CheckedState.BorderThickness = 0;
		this.xboxtabshow.CheckedState.FillColor = System.Drawing.Color.Black;
		this.xboxtabshow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.xboxtabshow.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.xboxtabshow.ForeColor = System.Drawing.Color.White;
		this.xboxtabshow.Location = new System.Drawing.Point(5, 100);
		this.xboxtabshow.Name = "xboxtabshow";
		this.xboxtabshow.Size = new System.Drawing.Size(198, 19);
		this.xboxtabshow.TabIndex = 169;
		this.xboxtabshow.Text = "Xbox";
		this.xboxtabshow.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.xboxtabshow.UncheckedState.BorderRadius = 2;
		this.xboxtabshow.UncheckedState.BorderThickness = 0;
		this.xboxtabshow.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.xboxtabshow.UseVisualStyleBackColor = false;
		this.xboxtabshow.CheckedChanged += new System.EventHandler(xboxtabshow_CheckedChanged);
		this.playstationtabshow.Animated = true;
		this.playstationtabshow.BackColor = System.Drawing.Color.Black;
		this.playstationtabshow.Checked = true;
		this.playstationtabshow.CheckedState.BorderColor = System.Drawing.Color.White;
		this.playstationtabshow.CheckedState.BorderRadius = 2;
		this.playstationtabshow.CheckedState.BorderThickness = 0;
		this.playstationtabshow.CheckedState.FillColor = System.Drawing.Color.Black;
		this.playstationtabshow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.playstationtabshow.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.playstationtabshow.ForeColor = System.Drawing.Color.White;
		this.playstationtabshow.Location = new System.Drawing.Point(5, 76);
		this.playstationtabshow.Name = "playstationtabshow";
		this.playstationtabshow.Size = new System.Drawing.Size(198, 19);
		this.playstationtabshow.TabIndex = 168;
		this.playstationtabshow.Text = "Playstation";
		this.playstationtabshow.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.playstationtabshow.UncheckedState.BorderRadius = 2;
		this.playstationtabshow.UncheckedState.BorderThickness = 0;
		this.playstationtabshow.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.playstationtabshow.UseVisualStyleBackColor = false;
		this.playstationtabshow.CheckedChanged += new System.EventHandler(guna2CheckBox6_CheckedChanged);
		this.otherinfoshow.Animated = true;
		this.otherinfoshow.BackColor = System.Drawing.Color.Black;
		this.otherinfoshow.Checked = true;
		this.otherinfoshow.CheckedState.BorderColor = System.Drawing.Color.White;
		this.otherinfoshow.CheckedState.BorderRadius = 2;
		this.otherinfoshow.CheckedState.BorderThickness = 0;
		this.otherinfoshow.CheckedState.FillColor = System.Drawing.Color.Black;
		this.otherinfoshow.CheckState = System.Windows.Forms.CheckState.Checked;
		this.otherinfoshow.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.otherinfoshow.ForeColor = System.Drawing.Color.White;
		this.otherinfoshow.Location = new System.Drawing.Point(5, 124);
		this.otherinfoshow.Name = "otherinfoshow";
		this.otherinfoshow.Size = new System.Drawing.Size(198, 19);
		this.otherinfoshow.TabIndex = 170;
		this.otherinfoshow.Text = "Other Info";
		this.otherinfoshow.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.otherinfoshow.UncheckedState.BorderRadius = 2;
		this.otherinfoshow.UncheckedState.BorderThickness = 0;
		this.otherinfoshow.UncheckedState.FillColor = System.Drawing.Color.FromArgb(125, 137, 149);
		this.otherinfoshow.UseVisualStyleBackColor = false;
		this.otherinfoshow.CheckedChanged += new System.EventHandler(otherinfoshow_CheckedChanged);
		this.guna2GroupBox2.BackColor = System.Drawing.Color.Transparent;
		this.guna2GroupBox2.BorderColor = System.Drawing.Color.FromArgb(0, 86, 179);
		this.guna2GroupBox2.BorderThickness = 0;
		this.guna2GroupBox2.Controls.Add(this.guna2Panel1);
		this.guna2GroupBox2.Controls.Add(this.guna2Button2);
		this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
		this.guna2GroupBox2.FillColor = System.Drawing.Color.Transparent;
		this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox2.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox2.Location = new System.Drawing.Point(3, 3);
		this.guna2GroupBox2.Name = "guna2GroupBox2";
		this.guna2GroupBox2.Size = new System.Drawing.Size(830, 214);
		this.guna2GroupBox2.TabIndex = 721;
		this.guna2GroupBox2.Text = "Change Color";
		this.guna2GroupBox2.UseTransparentBackground = true;
		this.guna2GroupBox2.Click += new System.EventHandler(guna2GroupBox2_Click);
		this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
		this.guna2Panel1.Location = new System.Drawing.Point(55, 15);
		this.guna2Panel1.Name = "guna2Panel1";
		this.guna2Panel1.Size = new System.Drawing.Size(772, 196);
		this.guna2Panel1.TabIndex = 165;
		this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(guna2Panel1_Paint_1);
		this.guna2Button2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.guna2Button2.Animated = true;
		this.guna2Button2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button2.BorderColor = System.Drawing.Color.Empty;
		this.guna2Button2.CheckedState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2Button2.DisabledState.FillColor = System.Drawing.Color.Black;
		this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.FillColor = System.Drawing.Color.Empty;
		this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2Button2.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.Image = SNIFF.Properties.Resources.Trash;
		this.guna2Button2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.guna2Button2.IndicateFocus = true;
		this.guna2Button2.Location = new System.Drawing.Point(4, 49);
		this.guna2Button2.Name = "guna2Button2";
		this.guna2Button2.PressedColor = System.Drawing.Color.Transparent;
		this.guna2Button2.Size = new System.Drawing.Size(40, 29);
		this.guna2Button2.TabIndex = 164;
		this.guna2Button2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
		this.guna2HtmlToolTip1.SetToolTip(this.guna2Button2, "Clear Color Status");
		this.guna2Button2.Click += new System.EventHandler(guna2Button2_Click_1);
		this.tabPage7.BackColor = System.Drawing.Color.Black;
		this.tabPage7.Controls.Add(this.guna2VScrollBar2);
		this.tabPage7.Controls.Add(this.guna2TextBox1);
		this.tabPage7.Controls.Add(this.host);
		this.tabPage7.Controls.Add(this.dataGridView1);
		this.tabPage7.Location = new System.Drawing.Point(4, 44);
		this.tabPage7.Name = "tabPage7";
		this.tabPage7.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage7.Size = new System.Drawing.Size(836, 427);
		this.tabPage7.TabIndex = 5;
		this.tabPage7.Text = "Labels";
		this.tabPage3.BackColor = System.Drawing.Color.Black;
		this.tabPage3.Controls.Add(this.guna2VScrollBar4);
		this.tabPage3.Controls.Add(this.DGVfilterList);
		this.tabPage3.Location = new System.Drawing.Point(4, 44);
		this.tabPage3.Name = "tabPage3";
		this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage3.Size = new System.Drawing.Size(836, 427);
		this.tabPage3.TabIndex = 8;
		this.tabPage3.Text = "Game Filters";
		this.guna2VScrollBar4.BindingContainer = this.DGVfilterList;
		this.guna2VScrollBar4.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar4.InUpdate = false;
		this.guna2VScrollBar4.LargeChange = 10;
		this.guna2VScrollBar4.Location = new System.Drawing.Point(815, 3);
		this.guna2VScrollBar4.Minimum = 1;
		this.guna2VScrollBar4.Name = "guna2VScrollBar4";
		this.guna2VScrollBar4.ScrollbarSize = 18;
		this.guna2VScrollBar4.Size = new System.Drawing.Size(18, 421);
		this.guna2VScrollBar4.TabIndex = 164;
		this.guna2VScrollBar4.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar4.ThumbSize = 5f;
		this.guna2VScrollBar4.Value = 1;
		this.DGVfilterList.AllowUserToResizeColumns = false;
		this.DGVfilterList.AllowUserToResizeRows = false;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
		this.DGVfilterList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
		this.DGVfilterList.BackgroundColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVfilterList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.DGVfilterList.ColumnHeadersHeight = 20;
		this.DGVfilterList.Columns.AddRange(this.Column4, this.Column5);
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.DGVfilterList.DefaultCellStyle = dataGridViewCellStyle8;
		this.DGVfilterList.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGVfilterList.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.DGVfilterList.Location = new System.Drawing.Point(3, 3);
		this.DGVfilterList.Name = "DGVfilterList";
		this.DGVfilterList.ReadOnly = true;
		this.DGVfilterList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle9.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGVfilterList.RowHeadersDefaultCellStyle = dataGridViewCellStyle9;
		this.DGVfilterList.RowHeadersVisible = false;
		this.DGVfilterList.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
		this.DGVfilterList.RowsDefaultCellStyle = dataGridViewCellStyle10;
		this.DGVfilterList.RowTemplate.Height = 35;
		this.DGVfilterList.Size = new System.Drawing.Size(830, 421);
		this.DGVfilterList.TabIndex = 163;
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
		this.DGVfilterList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(guna2DataGridView3_CellClick);
		this.DGVfilterList.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(guna2DataGridView3_CellContentClick_1);
		this.DGVfilterList.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(DGVfilterList_CellMouseEnter);
		this.DGVfilterList.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(DGVfilterList_CellMouseLeave);
		this.DGVfilterList.CellMouseMove += new System.Windows.Forms.DataGridViewCellMouseEventHandler(DGVfilterList_CellMouseMove);
		this.DGVfilterList.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2DataGridView3_KeyDown);
		this.Column4.HeaderText = "Name";
		this.Column4.Name = "Column4";
		this.Column4.ReadOnly = true;
		this.Column5.HeaderText = "Console";
		this.Column5.Name = "Column5";
		this.Column5.ReadOnly = true;
		this.tabPage2.BackColor = System.Drawing.Color.Black;
		this.tabPage2.Controls.Add(this.guna2VScrollBar1);
		this.tabPage2.Controls.Add(this.chatPanel);
		this.tabPage2.Controls.Add(this.panel1);
		this.tabPage2.Location = new System.Drawing.Point(4, 44);
		this.tabPage2.Name = "tabPage2";
		this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
		this.tabPage2.Size = new System.Drawing.Size(836, 427);
		this.tabPage2.TabIndex = 9;
		this.tabPage2.Text = "Live Chat";
		this.guna2VScrollBar1.BindingContainer = this.chatPanel;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 387;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(815, 3);
		this.guna2VScrollBar1.Maximum = 426;
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 387);
		this.guna2VScrollBar1.SmallChange = 5;
		this.guna2VScrollBar1.TabIndex = 165;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.chatPanel.AutoScroll = true;
		this.chatPanel.BackColor = System.Drawing.Color.Black;
		this.chatPanel.Dock = System.Windows.Forms.DockStyle.Fill;
		this.chatPanel.FlowDirection = System.Windows.Forms.FlowDirection.BottomUp;
		this.chatPanel.Location = new System.Drawing.Point(3, 3);
		this.chatPanel.Name = "chatPanel";
		this.chatPanel.Size = new System.Drawing.Size(830, 387);
		this.chatPanel.TabIndex = 166;
		this.chatPanel.WrapContents = false;
		this.panel1.BackColor = System.Drawing.Color.FromArgb(0, 86, 179);
		this.panel1.Controls.Add(this.Messageboxtb);
		this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.panel1.Location = new System.Drawing.Point(3, 390);
		this.panel1.Name = "panel1";
		this.panel1.Size = new System.Drawing.Size(830, 34);
		this.panel1.TabIndex = 165;
		this.Messageboxtb.Animated = true;
		this.Messageboxtb.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.Messageboxtb.BorderColor = System.Drawing.Color.Empty;
		this.Messageboxtb.BorderThickness = 0;
		this.Messageboxtb.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.Messageboxtb.DefaultText = "";
		this.Messageboxtb.DisabledState.ForeColor = System.Drawing.Color.White;
		this.Messageboxtb.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.Messageboxtb.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.Messageboxtb.FocusedState.ForeColor = System.Drawing.Color.White;
		this.Messageboxtb.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.Messageboxtb.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.Messageboxtb.ForeColor = System.Drawing.Color.White;
		this.Messageboxtb.HoverState.ForeColor = System.Drawing.Color.White;
		this.Messageboxtb.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.Messageboxtb.Location = new System.Drawing.Point(0, -1);
		this.Messageboxtb.Name = "Messageboxtb";
		this.Messageboxtb.PasswordChar = '\0';
		this.Messageboxtb.PlaceholderForeColor = System.Drawing.Color.White;
		this.Messageboxtb.PlaceholderText = "Type your Message...";
		this.Messageboxtb.SelectedText = "";
		this.Messageboxtb.Size = new System.Drawing.Size(830, 36);
		this.Messageboxtb.TabIndex = 142;
		this.Messageboxtb.TextChanged += new System.EventHandler(Messageboxtb_TextChanged);
		this.Messageboxtb.KeyDown += new System.Windows.Forms.KeyEventHandler(Messageboxtb_KeyDown);
		this.logInContextMenu1.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu1.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.clearAllToolStripMenuItem });
		this.logInContextMenu1.Name = "logInContextMenu2";
		this.logInContextMenu1.ShowImageMargin = false;
		this.logInContextMenu1.Size = new System.Drawing.Size(148, 70);
		this.logInContextMenu1.Opening += new System.ComponentModel.CancelEventHandler(logInContextMenu1_Opening);
		this.toolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(147, 22);
		this.toolStripMenuItem1.Text = "Copy To Clipboard";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click);
		this.toolStripMenuItem2.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(147, 22);
		this.toolStripMenuItem2.Text = "Edit Message";
		this.clearAllToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.clearAllToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.clearAllToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
		this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
		this.clearAllToolStripMenuItem.Text = "Delete Message";
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.guna2ControlBox1);
		this.panel2.Controls.Add(this.guna2ControlBox3);
		this.panel2.Controls.Add(this.guna2ControlBox2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(844, 28);
		this.panel2.TabIndex = 164;
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
		this.guna2ControlBox1.Location = new System.Drawing.Point(709, 0);
		this.guna2ControlBox1.Name = "guna2ControlBox1";
		this.guna2ControlBox1.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox1.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox1.TabIndex = 156;
		this.guna2ControlBox1.Click += new System.EventHandler(guna2ControlBox1_Click_1);
		this.guna2ControlBox3.Animated = true;
		this.guna2ControlBox3.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox3.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
		this.guna2ControlBox3.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox3.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox3.Location = new System.Drawing.Point(754, 0);
		this.guna2ControlBox3.Name = "guna2ControlBox3";
		this.guna2ControlBox3.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox3.TabIndex = 158;
		this.guna2ControlBox3.Click += new System.EventHandler(guna2ControlBox3_Click_2);
		this.guna2ControlBox2.Animated = true;
		this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox2.Location = new System.Drawing.Point(799, 0);
		this.guna2ControlBox2.Name = "guna2ControlBox2";
		this.guna2ControlBox2.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox2.Size = new System.Drawing.Size(45, 28);
		this.guna2ControlBox2.TabIndex = 157;
		this.guna2ControlBox2.Click += new System.EventHandler(guna2ControlBox2_Click);
		this.timer2.Interval = 2000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.filterDescriptionToolTip.AllowLinksHandling = true;
		this.filterDescriptionToolTip.BackColor = System.Drawing.Color.Black;
		this.filterDescriptionToolTip.ForeColor = System.Drawing.Color.GhostWhite;
		this.filterDescriptionToolTip.MaximumSize = new System.Drawing.Size(0, 0);
		this.guna2Elipse12.BorderRadius = 10;
		this.guna2Elipse12.TargetControl = this.guna2GroupBox3;
		this.guna2Elipse1.BorderRadius = 10;
		this.guna2Elipse1.TargetControl = this.guna2GroupBox4;
		this.guna2Elipse2.BorderRadius = 10;
		this.guna2Elipse2.TargetControl = this.guna2GroupBox1;
		this.guna2Elipse3.BorderRadius = 10;
		this.guna2Elipse3.TargetControl = this.guna2GroupBox2;
		this.guna2Elipse4.BorderRadius = 10;
		this.guna2Elipse4.TargetControl = this.guna2GroupBox7;
		this.guna2Elipse5.BorderRadius = 10;
		this.guna2Elipse5.TargetControl = this.guna2Button2;
		this.guna2HtmlToolTip1.AllowLinksHandling = true;
		this.guna2HtmlToolTip1.MaximumSize = new System.Drawing.Size(0, 0);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(844, 509);
		base.ControlBox = false;
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.guna2TabControl1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "t";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ZOPZ SNIFF";
		base.TopMost = true;
		base.TransparencyKey = System.Drawing.Color.Fuchsia;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(Menu_FormClosing);
		base.Load += new System.EventHandler(Form2_LoadAsync);
		base.Shown += new System.EventHandler(Menu_Shown);
		((System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.logInContextMenu2.ResumeLayout(false);
		this.guna2TabControl1.ResumeLayout(false);
		this.tabPage6.ResumeLayout(false);
		this.guna2GroupBox4.ResumeLayout(false);
		this.guna2GroupBox3.ResumeLayout(false);
		this.guna2GroupBox1.ResumeLayout(false);
		this.tabPage1.ResumeLayout(false);
		this.guna2GroupBox7.ResumeLayout(false);
		this.guna2GroupBox2.ResumeLayout(false);
		this.tabPage7.ResumeLayout(false);
		this.tabPage3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGVfilterList).EndInit();
		this.tabPage2.ResumeLayout(false);
		this.panel1.ResumeLayout(false);
		this.logInContextMenu1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
