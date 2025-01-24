using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNIFF.prompts;
using SNIFF.Properties;

namespace SNIFF;

public class rec : Form
{
	public class ModerationBlockDetails
	{
		public int ReportCategory { get; set; }

		public int Duration { get; set; }

		public int GameSessionId { get; set; }

		public string Message { get; set; }

		public bool IsHostKick { get; set; }

		public string PlayerIdReporter { get; set; }

		public bool IsBan { get; set; }

		public bool IsVoiceModAutoban { get; set; }

		public bool IsDeviceBan { get; set; }

		public bool IsWarning { get; set; }

		public string VoteKickReason { get; set; }

		public string TimeoutStartedAt { get; set; }

		public string AssociatedAccountUsername { get; set; }
	}

	public class AccountInfo
	{
		public int AccountId { get; set; }

		public string Username { get; set; }

		public string DisplayName { get; set; }

		public string ProfileImage { get; set; }

		public bool IsJunior { get; set; }

		public int Platforms { get; set; }

		public int PersonalPronouns { get; set; }

		public int IdentityFlags { get; set; }

		public DateTime CreatedAt { get; set; }

		public bool IsMetaPlatformBlocked { get; set; }
	}

	public class Influencer
	{
		public bool IsInfluencer { get; set; }
	}

	public class FriendsCountInfo
	{
		public string AccountId { get; set; }

		public string Username { get; set; }

		public int FriendsCount { get; set; }
	}

	public class Friend
	{
		public int PlayerID { get; set; }

		public int Favorited { get; set; }
	}

	public class AccountResponse
	{
		[JsonProperty("AccountId")]
		public string AccountId { get; set; }
	}

	private NotificationForm notification;

	private readonly HttpClient client = new HttpClient();

	public string Token;

	private static HttpClient Hostclient = new HttpClient();

	private int _successfulReports;

	private DateTime _lastChangeTime = DateTime.MinValue;

	private IContainer components;

	private Guna2GroupBox guna2GroupBox1;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2DataGridView guna2DataGridView1;

	private Label label4;

	private Panel panel2;

	private Label label1;

	private Guna2ControlBox guna2ControlBox4;

	private Guna2ControlBox guna2ControlBox5;

	private Guna2ControlBox guna2ControlBox6;

	private Guna2DragControl guna2DragControl1;

	private LogInContextMenu logInContextMenu5;

	private ToolStripMenuItem toolStripMenuItem8;

	private ToolStripMenuItem copyEntireRowToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem10;

	private ToolStripMenuItem toolStripMenuItem9;

	private ToolStripMenuItem pingCellToolStripMenuItem2;

	private ToolStripMenuItem clearAllToolStripMenuItem1;

	private ToolStripMenuItem getUserRoomsDataToolStripMenuItem;

	private ToolStripMenuItem takeBioToolStripMenuItem;

	private ToolStripMenuItem grabToolStripMenuItem;

	private ToolStripMenuItem massReportToolStripMenuItem;

	private ToolStripMenuItem unbanMeToolStripMenuItem;

	private Guna2TextBox guna2TextBox1;

	private ToolStripMenuItem checkBanStatusToolStripMenuItem;

	private ToolStripMenuItem checkIfInfluencerToolStripMenuItem;

	private ToolStripMenuItem friendsCountToolStripMenuItem;

	private ToolStripMenuItem childrenAccountToolStripMenuItem;

	private ToolStripMenuItem massReportUserToolStripMenuItem;

	private System.Windows.Forms.Timer timer2;

	private ToolStripMenuItem unFaverateAllToolStripMenuItem;

	private ToolStripMenuItem unfriendUserToolStripMenuItem;

	private ToolStripMenuItem subBotToolStripMenuItem;

	private ToolStripMenuItem massCheerToolStripMenuItem1;

	private LogInContextMenu logInContextMenu1;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem toolStripMenuItem3;

	private ToolStripMenuItem getFriendsBioToolStripMenuItem;

	private ToolStripMenuItem getMyFriendsCountToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem11;

	private ToolStripMenuItem toolStripMenuItem23;

	private ToolStripMenuItem lookupUsernameToolStripMenuItem;

	private ToolStripMenuItem lookupUseridToolStripMenuItem;

	private ToolStripMenuItem getRoomDataToolStripMenuItem;

	private ToolStripMenuItem unFavoriteAllToolStripMenuItem;

	private ToolStripMenuItem unfriendUserToolStripMenuItem1;

	private ToolStripMenuItem checkIfInfluencerToolStripMenuItem1;

	private ToolStripMenuItem checkBanStatusToolStripMenuItem1;

	private ToolStripMenuItem accountOptionsToolStripMenuItem;

	private ToolStripMenuItem getYoureAccountInfoToolStripMenuItem;

	private ToolStripMenuItem childrenAccountToolStripMenuItem1;

	private ToolStripMenuItem reportOptionsToolStripMenuItem;

	private ToolStripMenuItem massReportTrollingToolStripMenuItem;

	private ToolStripMenuItem getFriendsToolStripMenuItem;

	private ToolStripMenuItem massSubBotToolStripMenuItem;

	private ToolStripMenuItem checkFriendsBioToolStripMenuItem;

	private ToolStripMenuItem massReportToolStripMenuItem1;

	private ToolStripMenuItem massReportProfileNameToolStripMenuItem;

	private ToolStripMenuItem massReportUnder13ToolStripMenuItem;

	private ToolStripMenuItem dToolStripMenuItem;

	private ToolStripMenuItem massReportImageToolStripMenuItem;

	private ToolStripMenuItem massAddFriendToolStripMenuItem;

	private ToolStripMenuItem reportToolStripMenuItem;

	public List<ProfileEntry> Friends { get; set; }

	public rec(string token)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		Token = token;
		if (!(Assembly.GetExecutingAssembly() != Assembly.GetCallingAssembly()))
		{
			InitializeComponent();
			base.Name = "ZOPZ SNIFF";
			Text = string.Empty;
			base.ControlBox = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			guna2DataGridView1.MouseMove += guna2DataGridView1_MouseMove;
			guna2DataGridView1.MouseLeave += delegate
			{
				ResetRowColors(guna2DataGridView1);
			};
			ApplyBackgroundColor();
			guna2DataGridView1.RowTemplate.Height = 35;
			base.MaximizeBox = true;
			base.Name = "ZOPZ SNIFF";
			base.StartPosition = FormStartPosition.CenterScreen;
			((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("Authorization", "Bearer " + token);
			((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("Accept", "application/json, text/plain, */*");
		}
	}

	private void ResetRowColors(Guna2DataGridView dgv)
	{
		foreach (DataGridViewRow item in (IEnumerable)dgv.Rows)
		{
			item.DefaultCellStyle.BackColor = Color.Black;
		}
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

	public async Task FetchAndDisplayUserProfileAsync()
	{
		string apiUrl = "https://accounts.rec.net/account/me";
		HttpResponseMessage response = await client.GetAsync(apiUrl);
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception($"API request failed with status code {response.StatusCode}.");
		}
		string jsonString = await response.Content.ReadAsStringAsync();
		try
		{
			UserProfile userProfile = JsonConvert.DeserializeObject<UserProfile>(jsonString);
			string message = "Username: " + userProfile.Username + "\nDisplay Name: " + userProfile.DisplayName + "\nEmail: " + userProfile.Email + "\nPhone: " + userProfile.Phone + "\n" + $"Birthday: {userProfile.Birthday:yyyy-MM-dd}\n" + $"Account ID: {userProfile.AccountId}\n" + "Profile Image: " + userProfile.ProfileImage + "\nBanner Image: " + userProfile.BannerImage + "\n" + $"Is Junior: {userProfile.IsJunior}\n" + $"Platforms: {userProfile.Platforms}\n" + $"Personal Pronouns: {userProfile.PersonalPronouns}\n" + $"Created At: {userProfile.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {userProfile.IsMetaPlatformBlocked}";
			MessageAlert menu = new MessageAlert("User Profile", message);
			menu.StartPosition = FormStartPosition.Manual;
			menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
			menu.ShowDialog(this);
			base.Enabled = true;
		}
		catch (Exception ex)
		{
			throw new Exception("Error parsing JSON response. Details: " + ex.Message);
		}
	}

	public async Task<List<FriendEntry>> GetAccountDataAsync()
	{
		string apiUrl = "https://api.rec.net/api/relationships/v2/get";
		HttpResponseMessage response = await client.GetAsync(apiUrl);
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception($"API request failed with status code {response.StatusCode}.");
		}
		string jsonString = await response.Content.ReadAsStringAsync();
		try
		{
			return JsonConvert.DeserializeObject<List<FriendEntry>>(jsonString);
		}
		catch (Exception ex)
		{
			throw new Exception("Error parsing JSON response. Details: " + ex.Message);
		}
	}

	public async Task<List<ProfileEntry>> GetBulkAccountInfo(params long[] ids)
	{
		string apiUrl = "https://accounts.rec.net/account/bulk";
		FormUrlEncodedContent postParams = new FormUrlEncodedContent(ids.Select((long i) => new KeyValuePair<string, string>("id", i.ToString())));
		HttpResponseMessage response = await client.PostAsync(apiUrl, (HttpContent)(object)postParams);
		if (!response.IsSuccessStatusCode)
		{
			throw new Exception($"API request failed with status code {response.StatusCode}.");
		}
		string jsonString = await response.Content.ReadAsStringAsync();
		try
		{
			Friends = JsonConvert.DeserializeObject<List<ProfileEntry>>(jsonString);
			return Friends;
		}
		catch (Exception ex)
		{
			throw new Exception("Error parsing JSON response. Details: " + ex.Message);
		}
	}

	private void SetRowHoverColor(Guna2DataGridView dgv, int hoveredRowIndex)
	{
		for (int i = 0; i < dgv.Rows.Count; i++)
		{
			dgv.Rows[i].DefaultCellStyle.BackColor = Color.Black;
		}
		if (hoveredRowIndex >= 0)
		{
			dgv.Rows[hoveredRowIndex].DefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
		}
	}

	private async void rec_Load(object sender, EventArgs e)
	{
		base.MaximizeBox = true;
		base.Name = "ZOPZ SNIFF";
		guna2DataGridView1.AllowUserToAddRows = false;
		try
		{
			await GetBulkAccountInfo((from x in await GetAccountDataAsync()
				where x.RelationshipType != 0
				select x into i
				select i.OtherPlayerId).ToArray());
			guna2TextBox1_TextChanged(null, null);
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
			Console.WriteLine(ex.ToString());
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
				guna2GroupBox1.CustomBorderColor = color;
				guna2TextBox1.BackColor = color;
				guna2TextBox1.FillColor = color;
				panel2.BackColor = color;
			}
			catch (Exception ex)
			{
				ShowNotification("Error applying background color: " + ex.Message);
			}
		}
	}

	public void Alert(string msg, alert.enmType type)
	{
		new alert().showAlert(msg, type);
	}

	private void guna2ControlBox6_Click(object sender, EventArgs e)
	{
		Close();
	}

	private async void copyPacketsNumberToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string username = selectedRow.Cells[0].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://rec.net/_next/data/a98fd0cb919e734767870b3217859c0dd417ce67/user/" + username + ".json?username=" + username;
							JToken queries = JObject.Parse(await client.GetStringAsync(apiUrl))["pageProps"]["dehydratedState"]["queries"].FirstOrDefault();
							JToken stateData = queries?["state"]["data"];
							if (stateData != null)
							{
								MessageBox.Show(string.Format("Username: {0}\n", stateData["username"]) + string.Format("Display Name: {0}\n", stateData["displayName"]) + string.Format("Account ID: {0}\n", stateData["accountId"]) + string.Format("Profile Image: {0}\n", stateData["profileImage"]) + string.Format("Banner Image: {0}\n", stateData["bannerImage"]) + string.Format("Is Junior: {0}\n", stateData["isJunior"]) + string.Format("Platforms: {0}\n", stateData["platforms"]) + string.Format("Personal Pronouns: {0}\n", stateData["personalPronouns"]) + string.Format("Identity Flags: {0}\n", stateData["identityFlags"]) + "Created At: " + DateTime.Parse(stateData["createdAt"].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "\n" + string.Format("Is Meta Platform Blocked: {0}\n", stateData["isMetaPlatformBlocked"]) + string.Format("Data Update Count: {0}\n", queries["state"]["dataUpdateCount"]) + string.Format("Data Updated At: {0}\n", queries["state"]["dataUpdatedAt"]) + string.Format("Status: {0}\n", queries["state"]["status"]) + string.Format("Fetch Status: {0}", queries["state"]["fetchStatus"]));
							}
							else
							{
								ShowNotification("No account data found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void editLabelToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 3)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://accounts.rec.net/account/bulk?id=" + username;
							MessageBox.Show(JToken.Parse(await client.GetStringAsync(apiUrl)).ToString(Formatting.Indented));
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private void toolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem2_Click(object sender, EventArgs e)
	{
	}

	private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void copyToClipboardToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 1)
			{
				string obj = selectedRow.Cells[0].Value.ToString();
				string cellValue2 = selectedRow.Cells[1].Value.ToString();
				Clipboard.SetText(obj + " " + cellValue2);
			}
			else
			{
				ShowNotification("The selected row does not have enough cells.");
			}
		}
	}

	private void ClearDataGridView()
	{
		if (guna2DataGridView1.DataSource != null)
		{
			if (guna2DataGridView1.DataSource is BindingSource bindingSource)
			{
				bindingSource.Clear();
			}
			else if (guna2DataGridView1.DataSource is DataTable dataTable)
			{
				dataTable.Clear();
			}
			else if (guna2DataGridView1.DataSource is IList list)
			{
				list.Clear();
				guna2DataGridView1.DataSource = new List<object>();
			}
			else
			{
				guna2DataGridView1.DataSource = null;
			}
		}
	}

	private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ClearDataGridView();
		ShowNotification("All Rows Have Been Cleared");
	}

	private void clearToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ShowNotification("All Rows Have Been Cleared");
	}

	private void FormatDataGridViewColumns()
	{
		if (guna2DataGridView1 == null)
		{
			return;
		}
		try
		{
			guna2DataGridView1.Columns["Username"].HeaderText = "Username";
			guna2DataGridView1.Columns["DisplayName"].HeaderText = "Display Name";
			guna2DataGridView1.Columns["AccountId"].HeaderText = "Account ID";
			guna2DataGridView1.Columns["IsJunior"].HeaderText = "Is Junior";
			guna2DataGridView1.Columns["Platforms"].HeaderText = "Platforms";
			guna2DataGridView1.Columns["CreatedAt"].HeaderText = "Created At";
			guna2DataGridView1.Columns["Username"].Width = 60;
			guna2DataGridView1.Columns["DisplayName"].Width = 50;
			guna2DataGridView1.Columns["AccountId"].Width = 50;
			guna2DataGridView1.Columns["IsJunior"].Width = 40;
			guna2DataGridView1.Columns["Platforms"].Width = 40;
			guna2DataGridView1.Columns["CreatedAt"].Width = 180;
			guna2DataGridView1.Columns["Username"].ReadOnly = true;
			guna2DataGridView1.Columns["DisplayName"].ReadOnly = true;
		}
		catch
		{
		}
	}

	private void grabFriendsToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void logInContextMenu1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void toolStripMenuItem8_Click(object sender, EventArgs e)
	{
		try
		{
			if (guna2DataGridView1.SelectedRows.Count <= 0)
			{
				return;
			}
			DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 0)
			{
				object cellValue1 = selectedRow.Cells[0].Value;
				if (cellValue1 != null)
				{
					string obj = cellValue1.ToString() ?? "";
					Thread.Sleep(100);
					Clipboard.SetText(obj);
				}
			}
		}
		catch (ExternalException)
		{
		}
		catch (Exception)
		{
		}
	}

	private async void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string username = selectedRow.Cells[0].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://apim.rec.net/accounts/account?username=" + username;
							JToken queries = JObject.Parse(await client.GetStringAsync(apiUrl))["pageProps"]["dehydratedState"]["queries"].FirstOrDefault();
							JToken stateData = queries?["state"]["data"];
							if (stateData != null)
							{
								MessageBox.Show(string.Format("Username: {0}\n", stateData["username"]) + string.Format("Display Name: {0}\n", stateData["displayName"]) + string.Format("Account ID: {0}\n", stateData["accountId"]) + string.Format("Profile Image: {0}\n", stateData["profileImage"]) + string.Format("Banner Image: {0}\n", stateData["bannerImage"]) + string.Format("Is Junior: {0}\n", stateData["isJunior"]) + string.Format("Platforms: {0}\n", stateData["platforms"]) + string.Format("Personal Pronouns: {0}\n", stateData["personalPronouns"]) + string.Format("Identity Flags: {0}\n", stateData["identityFlags"]) + "Created At: " + DateTime.Parse(stateData["createdAt"].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "\n" + string.Format("Is Meta Platform Blocked: {0}\n", stateData["isMetaPlatformBlocked"]) + string.Format("Data Update Count: {0}\n", queries["state"]["dataUpdateCount"]) + string.Format("Data Updated At: {0}\n", queries["state"]["dataUpdatedAt"]) + string.Format("Status: {0}\n", queries["state"]["status"]) + string.Format("Fetch Status: {0}", queries["state"]["fetchStatus"]), "Account Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								ShowNotification("No account data found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void toolStripMenuItem10_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 3)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://accounts.rec.net/account/bulk?id=" + username;
							List<AccountInfo> accountInfoList = JsonConvert.DeserializeObject<List<AccountInfo>>(await client.GetStringAsync(apiUrl));
							if (accountInfoList != null && accountInfoList.Count > 0)
							{
								AccountInfo accountInfo = accountInfoList[0];
								MessageBox.Show($"Account ID: {accountInfo.AccountId}\n" + "Username: " + accountInfo.Username + "\nDisplay Name: " + accountInfo.DisplayName + "\nProfile Image: " + accountInfo.ProfileImage + "\n" + $"Is Junior: {accountInfo.IsJunior}\n" + $"Platforms: {accountInfo.Platforms}\n" + $"Personal Pronouns: {accountInfo.PersonalPronouns}\n" + $"Identity Flags: {accountInfo.IdentityFlags}\n" + $"Created At: {accountInfo.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {accountInfo.IsMetaPlatformBlocked}", "Account Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								ShowNotification("No account data found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private void clearAllToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		ClearDataGridView();
	}

	private async void pingCellToolStripMenuItem2_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string accountId = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(accountId))
					{
						try
						{
							string apiUrl = "https://apim.rec.net/accounts/account/" + accountId + "/bio";
							string bio = JObject.Parse(await client.GetStringAsync(apiUrl))["bio"]?.ToString();
							if (!string.IsNullOrEmpty(bio))
							{
								MessageBox.Show(bio ?? "");
							}
							else
							{
								ShowNotification("Bio data is empty or not found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Account ID is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void toolStripMenuItem9_Click(object sender, EventArgs e)
	{
		_ = 1;
		try
		{
			await GetBulkAccountInfo((from x in await GetAccountDataAsync()
				where x.RelationshipType != 0
				select x into i
				select i.OtherPlayerId).ToArray());
			guna2TextBox1_TextChanged(null, null);
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
			Console.WriteLine(ex.ToString());
		}
	}

	private async void getUserRoomsDataToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string username = selectedRow.Cells[0].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://rec.net/_next/data/a98fd0cb919e734767870b3217859c0dd417ce67/user/" + username + "/rooms.json?username=" + username;
							JToken query = JObject.Parse(await client.GetStringAsync(apiUrl))["pageProps"]["dehydratedState"]["queries"].FirstOrDefault();
							JToken stateData = query?["state"]["data"];
							if (stateData != null)
							{
								MessageBox.Show(string.Format("Username: {0}\n", stateData["username"]) + string.Format("Display Name: {0}\n", stateData["displayName"]) + string.Format("Account ID: {0}\n", stateData["accountId"]) + string.Format("Profile Image: {0}\n", stateData["profileImage"]) + string.Format("Banner Image: {0}\n", stateData["bannerImage"]) + string.Format("Display Emoji: {0}\n", stateData["displayEmoji"]) + string.Format("Is Junior: {0}\n", stateData["isJunior"]) + string.Format("Platforms: {0}\n", stateData["platforms"]) + string.Format("Personal Pronouns: {0}\n", stateData["personalPronouns"]) + string.Format("Identity Flags: {0}\n", stateData["identityFlags"]) + "Created At: " + DateTime.Parse(stateData["createdAt"].ToString()).ToString("MM/dd/yyyy HH:mm:ss") + "\n" + string.Format("Is Meta Platform Blocked: {0}\n", stateData["isMetaPlatformBlocked"]) + string.Format("Data Update Count: {0}\n", query["state"]["dataUpdateCount"]) + "Data Updated At: " + DateTimeOffset.FromUnixTimeMilliseconds((long)query["state"]["dataUpdatedAt"]).DateTime.ToString("MM/dd/yyyy HH:mm:ss") + "\n" + string.Format("Status: {0}\n", query["state"]["status"]) + string.Format("Fetch Status: {0}", query["state"]["fetchStatus"]), "User Data", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								ShowNotification("No account data found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	public async Task<string> GetFriendBioAsync(string accountId)
	{
		if (string.IsNullOrEmpty(accountId))
		{
			throw new ArgumentException("Account ID cannot be null or empty.", "accountId");
		}
		try
		{
			string apiUrl = "https://apim.rec.net/accounts/account/" + accountId + "/bio";
			string obj = JObject.Parse(await client.GetStringAsync(apiUrl))["bio"]?.ToString();
			if (string.IsNullOrEmpty(obj))
			{
				throw new Exception("Bio data is empty or not found.");
			}
			return obj;
		}
		catch (Exception ex)
		{
			throw new Exception("Error fetching bio data: " + ex.Message);
		}
	}

	public async Task UpdateMyBioAsync(string newBio)
	{
		if (string.IsNullOrEmpty(newBio))
		{
			throw new ArgumentException("Bio cannot be null or empty.", "newBio");
		}
		try
		{
			string apiUrl = "https://accounts.rec.net/account/me/bio";
			FormUrlEncodedContent formUrlEncoded = new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)new Dictionary<string, string> { { "bio", newBio } });
			HttpResponseMessage response = await client.PutAsync(apiUrl, (HttpContent)(object)formUrlEncoded);
			if (!response.IsSuccessStatusCode)
			{
				string responseContent = await response.Content.ReadAsStringAsync();
				throw new Exception($"Failed to update bio with status code {response.StatusCode}. Response: {responseContent}");
			}
			ShowNotification("Bio updated successfully.");
		}
		catch (Exception ex)
		{
			ShowNotification("Error updating bio: " + ex.Message);
			Clipboard.SetText(ex.Message);
		}
	}

	public async Task DisplayAndUpdateBioAsync(string usernameInput)
	{
		if (string.IsNullOrEmpty(usernameInput))
		{
			ShowNotification("Username is required.");
			return;
		}
		try
		{
			string accountId = await GetAccountIdFromUsernameAsync(usernameInput);
			if (string.IsNullOrEmpty(accountId))
			{
				ShowNotification("Account ID not found for the provided username.");
				return;
			}
			string friendBio = await GetFriendBioAsync(accountId);
			if (string.IsNullOrEmpty(friendBio))
			{
				ShowNotification("Bio data is empty or not found.");
			}
			else if (MessageBox.Show("Do you want to update your bio to: '" + friendBio + "'?", "Confirm Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				await UpdateMyBioAsync(friendBio);
				ShowNotification("Your bio has been updated with your friend's bio.");
			}
			else
			{
				ShowNotification("Bio update canceled.");
			}
		}
		catch (Exception ex)
		{
			ShowNotification("Error: " + ex.Message);
			Clipboard.SetText(ex.Message);
		}
	}

	public async Task<string> GetAccountIdFromUsernameAsync(string username)
	{
		if (string.IsNullOrEmpty(username))
		{
			throw new ArgumentException("Username cannot be null or empty.", "username");
		}
		try
		{
			string apiUrl = "https://apim.rec.net/accounts/account?username=" + username;
			dynamic jsonResponse = JsonConvert.DeserializeObject<object>(await client.GetStringAsync(apiUrl));
			if (jsonResponse != null)
			{
				dynamic accountId = jsonResponse?.accountId?.ToString();
				if ((!string.IsNullOrEmpty(accountId)))
				{
					return accountId;
				}
				throw new Exception("Account ID not found.");
			}
			throw new Exception("Invalid response from the API.");
		}
		catch (Exception ex)
		{
			throw new Exception("Error fetching account ID: " + ex.Message);
		}
	}

	private void guna2DataGridView1_KeyDown(object sender, KeyEventArgs e)
	{
	}

	private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private async void grabToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await FetchAndDisplayUserProfileAsync();
	}

	public async Task TriggerDisplayAndUpdateBioAsync()
	{
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Username";
		string usernameInput = simplePrompt.ShowDialog((string val) => !string.IsNullOrEmpty(val));
		if (!string.IsNullOrEmpty(usernameInput))
		{
			await DisplayAndUpdateBioAsync(usernameInput);
		}
		else
		{
			ShowNotification("Username cannot be empty.");
		}
	}

	private async void takeBioToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await TriggerDisplayAndUpdateBioAsync();
	}

	private async Task MassCheer(string token, ulong userId, string cheerId)
	{
		string cheerLink = "https://api.rec.net/api/PlayerCheer/v1/SetSelectedCheer";
		StringContent cheerBody = new StringContent(JsonConvert.SerializeObject(new { userId, cheerId }), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, cheerLink);
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
		request.Content = (HttpContent)(object)cheerBody;
		HttpResponseMessage response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			await response.Content.ReadAsStringAsync();
		}
	}

	private async Task SubUser(string token, ulong userId)
	{
		string subscriptionLink = $"https://clubs.rec.net/subscription/{userId}";
		StringContent subscriptionBody = new StringContent(JsonConvert.SerializeObject(new { userId }), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, subscriptionLink);
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
		request.Content = (HttpContent)(object)subscriptionBody;
		HttpResponseMessage response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			await response.Content.ReadAsStringAsync();
		}
	}

	private async void massReportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] array = (await val2.GetStringAsync(pastebinUrl)).Split('\n');
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		ulong result;
		string id = simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out result));
		base.Enabled = true;
		string[] array2 = array;
		foreach (string token in array2)
		{
			reportTasks.Add(ReportUser(token.Replace("\n", "").Replace("\r", ""), id, "Whatever"));
		}
		await Task.WhenAll(reportTasks);
		ShowNotification("All Reports sent out");
	}

	private async Task ReportUser(string token, object imageId, string reportDetails)
	{
		string imageReportLink = $"https://api.rec.net/api/images/v2/{imageId}/report";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 1,
			ReportDetails = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, imageReportLink);
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
		request.Content = (HttpContent)(object)reportBody;
		_ = (await client.SendAsync(request)).IsSuccessStatusCode;
	}

	private async Task ReportUsername(string token, object imageId, string reportDetails)
	{
		string imageReportLink = $"https://api.rec.net/api/images/v2/{imageId}/report";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 1,
			ReportDetails = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, imageReportLink);
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
		request.Content = (HttpContent)(object)reportBody;
		_ = (await client.SendAsync(request)).IsSuccessStatusCode;
	}

	private async void guna2TextBox1_TextChanged(object sender, EventArgs e)
	{
		_lastChangeTime = DateTime.Now;
		await Task.Delay(200);
		if (!(_lastChangeTime.AddMilliseconds(100.0) > DateTime.Now))
		{
			string searchText = guna2TextBox1.Text.ToLower();
			FilterRows(searchText);
		}
	}

	private void FilterRows(string filterText)
	{
		List<ProfileEntry> filteredEntries = new List<ProfileEntry>();
		foreach (ProfileEntry label in Friends)
		{
			if (label.Username.ToLower().Contains(filterText) || label.DisplayName.ToLower().Contains(filterText))
			{
				filteredEntries.Add(label);
			}
		}
		Invoke((MethodInvoker)delegate
		{
			PopulateDataGridView(filteredEntries);
		});
	}

	private void PopulateDataGridView(List<ProfileEntry> jsonArray)
	{
		List<object> entries = new List<object>();
		foreach (ProfileEntry accountEntry in jsonArray)
		{
			entries.Add(new { accountEntry.Username, accountEntry.DisplayName, accountEntry.AccountId, accountEntry.IsJunior, accountEntry.Platforms, accountEntry.CreatedAt });
		}
		guna2DataGridView1.DataSource = entries;
		if (jsonArray.Count() != 0)
		{
			FormatDataGridViewColumns();
		}
	}

	private async void checkBanStatusToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string apiUrl = "https://api.rec.net/api/PlayerReporting/v1/moderationBlockDetails";
		HttpResponseMessage response = await client.GetAsync(apiUrl);
		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show($"API request failed with status code {response.StatusCode}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		string responseBody = await response.Content.ReadAsStringAsync();
		try
		{
			ModerationBlockDetails moderationBlockDetails = JsonConvert.DeserializeObject<ModerationBlockDetails>(responseBody);
			MessageBox.Show($"Report Category: {moderationBlockDetails.ReportCategory}\n" + $"Duration: {moderationBlockDetails.Duration}\n" + $"Game Session ID: {moderationBlockDetails.GameSessionId}\n" + "Message: " + moderationBlockDetails.Message + "\n" + $"Is Host Kick: {moderationBlockDetails.IsHostKick}\n" + "Player ID Reporter: " + moderationBlockDetails.PlayerIdReporter + "\n" + $"Is Ban: {moderationBlockDetails.IsBan}\n" + $"Is Voice Mod Autoban: {moderationBlockDetails.IsVoiceModAutoban}\n" + $"Is Device Ban: {moderationBlockDetails.IsDeviceBan}\n" + $"Is Warning: {moderationBlockDetails.IsWarning}\n" + "Vote Kick Reason: " + moderationBlockDetails.VoteKickReason + "\nTimeout Started At: " + moderationBlockDetails.TimeoutStartedAt + "\nAssociated Account Username: " + moderationBlockDetails.AssociatedAccountUsername, "Moderation Block Details", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}
		catch (Exception ex)
		{
			ShowNotification("Error parsing JSON response. Details: " + ex.Message);
		}
	}

	private async void checkIfInfluencerToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							HttpClient client = new HttpClient();
							try
							{
								string apiUrl = "https://econ.rec.net/api/influencerpartnerprogram/isinfluencer?accountId=" + username;
								ShowNotification("Influencer Partner: " + await client.GetStringAsync(apiUrl));
							}
							finally
							{
								((IDisposable)client)?.Dispose();
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void friendsCountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://api.rec.net/api/relationships/v1/current/friendscount?accountId=" + username;
							if (int.TryParse(await client.GetStringAsync(apiUrl), out var friendsCount))
							{
								MessageBox.Show($"Friends Count: {friendsCount}", "Friends Count Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								ShowNotification("Failed to parse friends count.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification("Error fetching data: " + ex.Message);
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void childrenAccountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					if (!string.IsNullOrEmpty(selectedRow.Cells[2].Value?.ToString()))
					{
						try
						{
							string apiUrl = "https://accounts.rec.net/account/me/children";
							List<AccountInfo> accountInfoList = JsonConvert.DeserializeObject<List<AccountInfo>>(await client.GetStringAsync(apiUrl));
							if (accountInfoList != null && accountInfoList.Count > 0)
							{
								string formattedResponse = "Children Accounts:\n";
								foreach (AccountInfo accountInfo in accountInfoList)
								{
									formattedResponse = formattedResponse + $"Account ID: {accountInfo.AccountId}\n" + "Username: " + accountInfo.Username + "\nDisplay Name: " + accountInfo.DisplayName + "\nProfile Image: " + accountInfo.ProfileImage + "\n" + $"Is Junior: {accountInfo.IsJunior}\n" + $"Platforms: {accountInfo.Platforms}\n" + $"Personal Pronouns: {accountInfo.PersonalPronouns}\n" + $"Identity Flags: {accountInfo.IdentityFlags}\n" + $"Created At: {accountInfo.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {accountInfo.IsMetaPlatformBlocked}\n\n";
								}
								MessageBox.Show(formattedResponse, "Children Account Information", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
							else
							{
								ShowNotification("No children account information found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async Task<List<long>> FetchSubscriptionsAsync()
	{
		try
		{
			string apiUrl = "https://clubs.rec.net/subscription/my/subscriptions";
			return JsonConvert.DeserializeObject<List<long>>(await client.GetStringAsync(apiUrl));
		}
		catch (Exception ex)
		{
			ShowNotification("Error fetching subscriptions: " + ex.Message);
			return new List<long>();
		}
	}

	private async Task<List<AccountInfo>> FetchAccountDetailsAsync(List<long> accountIds)
	{
		try
		{
			List<AccountInfo> accountInfos = new List<AccountInfo>();
			foreach (long accountId in accountIds)
			{
				string apiUrl = $"https://accounts.rec.net/account/bulk?id={accountId}";
				List<AccountInfo> accountInfoList = JsonConvert.DeserializeObject<List<AccountInfo>>(await client.GetStringAsync(apiUrl));
				if (accountInfoList != null && accountInfoList.Count > 0)
				{
					accountInfos.AddRange(accountInfoList);
				}
			}
			return accountInfos;
		}
		catch (Exception ex)
		{
			ShowNotification("Error fetching account details: " + ex.Message);
			Alert("Error fetching account details: " + ex.Message, alert.enmType.Warning);
			return new List<AccountInfo>();
		}
	}

	private void DisplayAccountDetails(List<AccountInfo> accountInfos)
	{
		guna2DataGridView1.Rows.Clear();
		guna2DataGridView1.Columns.Clear();
		guna2DataGridView1.Columns.Add("AccountId", "Account ID");
		guna2DataGridView1.Columns.Add("Username", "Username");
		guna2DataGridView1.Columns.Add("DisplayName", "Display Name");
		guna2DataGridView1.Columns.Add("ProfileImage", "Profile Image");
		guna2DataGridView1.Columns.Add("BannerImage", "Banner Image");
		guna2DataGridView1.Columns.Add("IsJunior", "Is Junior");
		guna2DataGridView1.Columns.Add("Platforms", "Platforms");
		guna2DataGridView1.Columns.Add("PersonalPronouns", "Personal Pronouns");
		guna2DataGridView1.Columns.Add("CreatedAt", "Created At");
		guna2DataGridView1.Columns.Add("IsMetaPlatformBlocked", "Is Meta Platform Blocked");
		foreach (AccountInfo accountInfo in accountInfos)
		{
			guna2DataGridView1.Rows.Add(accountInfo.Username, accountInfo.DisplayName, accountInfo.AccountId, accountInfo.IsJunior, accountInfo.Platforms, accountInfo.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
		}
	}

	private async void unbanMeToolStripMenuItem_Click(object sender, EventArgs e)
	{
		_ = 1;
		try
		{
			string url = "https://api.rec.net/api/PlayerReporting/v1/moderationBlockDetails";
			StringContent content = new StringContent(JsonConvert.SerializeObject(new
			{
				ReportCategory = 0,
				Duration = 0,
				GameSessionId = false,
				Message = false,
				IsHostKick = false,
				PlayerIdReporter = false,
				IsBan = false,
				IsVoiceModAutoban = true,
				IsDeviceBan = false,
				IsWarning = false,
				VoteKickReason = false,
				TimeoutStartedAt = false,
				AssociatedAccountUsername = false
			}), Encoding.UTF8, "application/json");
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, url)
			{
				Content = (HttpContent)(object)content
			};
			HttpResponseMessage obj = await client.SendAsync(request);
			obj.EnsureSuccessStatusCode();
			ShowNotification("Request was successful: " + await obj.Content.ReadAsStringAsync());
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async void grabLobbyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		_ = 1;
		try
		{
			string url = "https://api.rec.net/api/lobbies/2ba44d8f-67d5-4b7e-b239-f3949bccf95a/players";
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, url);
			HttpResponseMessage obj = await client.SendAsync(request);
			obj.EnsureSuccessStatusCode();
			string formattedResponse = JToken.Parse(await obj.Content.ReadAsStringAsync()).ToString(Formatting.Indented);
			ShowNotification("Players in the lobby:" + formattedResponse);
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async Task ReportUsernameAsync(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 101,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private int GetRetryAfterSeconds(HttpResponseMessage response)
	{
		IEnumerable<string> values = default(IEnumerable<string>);
		if (((HttpHeaders)response.Headers).TryGetValues("Retry-After",out values) && int.TryParse(values.FirstOrDefault(), out var retryAfter))
		{
			return retryAfter;
		}
		return 60;
	}

	private async void massReportUserToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/128.0.0.0 Safari/537.36 Edg/128.0.0.0\r\n");
		string[] array = (await val2.GetStringAsync(pastebinUrl)).Split('\n');
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		ulong result;
		string id = simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out result));
		base.Enabled = true;
		string[] array2 = array;
		foreach (string token in array2)
		{
			reportTasks.Add(ReportUsername(token.Replace("\n", "").Replace("\r", ""), id, "Whatever"));
		}
		await Task.WhenAll(reportTasks);
		ShowNotification("All Reports sent out");
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		notification.Hide();
		timer2.Stop();
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private async Task UnfavoriteFriends(string token)
	{
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		string friendsUrl = "https://api.rec.net/api/relationships/v6/current/friends?take=1";
		HttpResponseMessage friendsResponse = await client.GetAsync(friendsUrl);
		if (!friendsResponse.IsSuccessStatusCode)
		{
			return;
		}
		List<Friend> friendsList = JsonConvert.DeserializeObject<List<Friend>>(await friendsResponse.Content.ReadAsStringAsync());
		foreach (Friend friend in friendsList)
		{
			if (friend.Favorited == 3)
			{
				string unFaveUrl = $"https://api.rec.net/api/relationships/v1/unfavorite?id={friend.PlayerID}";
				await client.GetAsync(unFaveUrl);
				ShowNotification($"Unfavorited: {friend.PlayerID} ");
			}
		}
	}

	private async void unFaverateAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await UnfavoriteFriends(Token);
	}

	private async Task<bool> UnfriendUser(string playerId)
	{
		client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
		string unfriendUrl = "https://api.rec.net/api/relationships/v3/" + playerId;
		return (await client.DeleteAsync(unfriendUrl)).IsSuccessStatusCode;
	}

	private async void unfriendUserToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string accountId = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(accountId))
					{
						try
						{
							if (await UnfriendUser(accountId))
							{
								ShowNotification("Successfully unfriended the user.");
							}
							else
							{
								ShowNotification("Failed to unfriend the user.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification("Error unfriending user: " + ex.Message);
							return;
						}
					}
					ShowNotification("Account ID is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private void guna2DataGridView1_MouseHover(object sender, EventArgs e)
	{
	}

	private void guna2DataGridView1_MouseMove(object sender, MouseEventArgs e)
	{
		Guna2DataGridView dgv = sender as Guna2DataGridView;
		DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
		SetRowHoverColor(dgv, hit.RowIndex);
	}

	private void guna2ControlBox4_Click(object sender, EventArgs e)
	{
	}

	private async void subBotToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await client.GetStringAsync(pastebinUrl)).Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => true), out var userId))
		{
			base.Enabled = true;
			List<Task> reportTasks = new List<Task>();
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(SubUser(token.Trim(), userId));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("Sent all subscriptions");
		}
		else
		{
			ShowNotification("Invalid user ID. Please enter a valid number.");
		}
	}

	private async void massCheerToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await client.GetStringAsync(pastebinUrl)).Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		string s = simplePrompt.ShowDialog((string val) => true);
		string cheerInput = simplePrompt.ShowDialog((string val) => true);
		if (ulong.TryParse(s, out var userId) && !string.IsNullOrWhiteSpace(cheerInput))
		{
			base.Enabled = true;
			List<Task> cheerTasks = new List<Task>();
			string[] array = tokens;
			foreach (string token in array)
			{
				cheerTasks.Add(MassCheer(token.Trim(), userId, cheerInput));
			}
			await Task.WhenAll(cheerTasks);
			ShowNotification("Sent all cheers");
		}
		else
		{
			ShowNotification("Invalid input. Please enter a valid user ID and cheer ID.");
		}
	}

	private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
	{
		try
		{
			if (guna2DataGridView1.SelectedRows.Count <= 0)
			{
				return;
			}
			DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 0)
			{
				object cellValue1 = selectedRow.Cells[0].Value;
				if (cellValue1 != null)
				{
					string obj = cellValue1.ToString() ?? "";
					Thread.Sleep(100);
					Clipboard.SetText(obj);
				}
			}
		}
		catch (ExternalException)
		{
		}
		catch (Exception)
		{
		}
	}

	private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
	{
		ClearDataGridView();
	}

	private async void getFriendsToolStripMenuItem_Click(object sender, EventArgs e)
	{
		_ = 1;
		try
		{
			await GetBulkAccountInfo((from x in await GetAccountDataAsync()
				where x.RelationshipType != 0
				select x into i
				select i.OtherPlayerId).ToArray());
			guna2TextBox1_TextChanged(null, null);
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
			Console.WriteLine(ex.ToString());
		}
	}

	private async void getFriendsBioToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await TriggerDisplayAndUpdateBioAsync();
	}

	private async void lookupUsernameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Username";
		string usernameInput = simplePrompt.ShowDialog((string val) => !string.IsNullOrEmpty(val));
		if (!string.IsNullOrEmpty(usernameInput))
		{
			try
			{
				string apiUrl = "https://apim.rec.net/accounts/account?username=" + usernameInput;
				string value = await client.GetStringAsync(apiUrl);
				dynamic jsonResponse = JsonConvert.DeserializeObject<object>(value);
				Console.WriteLine(value);
				if (jsonResponse != null)
				{
					string formattedResponse = $"Account ID: {(object)jsonResponse.accountId}\n" + $"Username: {(object)jsonResponse.username}\n" + $"Display Name: {(object)jsonResponse.displayName}\n" + $"Profile Image: {(object)jsonResponse.profileImage}\n" + $"Is Junior: {(object)jsonResponse.isJunior}\n" + $"Platforms: {(object)jsonResponse.platforms}\n" + $"Personal Pronouns: {(object)jsonResponse.personalPronouns}\n" + $"Identity Flags: {(object)jsonResponse.identityFlags}\n" + $"Created At: {(object)jsonResponse.createdAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {(object)jsonResponse.isMetaPlatformBlocked}";
					MessageAlert menu = new MessageAlert("Account Information", formattedResponse);
					menu.StartPosition = FormStartPosition.Manual;
					menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
					menu.ShowDialog(this);
					base.Enabled = true;
				}
				else
				{
					ShowNotification("No account data found or invalid response from API.");
				}
				return;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error: " + ex.Message);
				ShowNotification("An error occurred: " + ex.Message);
				return;
			}
		}
		ShowNotification("Username input is invalid.");
	}

	private async void lookupUseridToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 3)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://accounts.rec.net/account/bulk?id=" + username;
							List<AccountInfo> accountInfoList = JsonConvert.DeserializeObject<List<AccountInfo>>(await client.GetStringAsync(apiUrl));
							if (accountInfoList != null && accountInfoList.Count > 0)
							{
								AccountInfo accountInfo = accountInfoList[0];
								string formattedResponse = $"Account ID: {accountInfo.AccountId}\n" + "Username: " + accountInfo.Username + "\nDisplay Name: " + accountInfo.DisplayName + "\nProfile Image: " + accountInfo.ProfileImage + "\n" + $"Is Junior: {accountInfo.IsJunior}\n" + $"Platforms: {accountInfo.Platforms}\n" + $"Personal Pronouns: {accountInfo.PersonalPronouns}\n" + $"Identity Flags: {accountInfo.IdentityFlags}\n" + $"Created At: {accountInfo.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {accountInfo.IsMetaPlatformBlocked}";
								MessageAlert menu = new MessageAlert("Account Information", formattedResponse);
								menu.StartPosition = FormStartPosition.Manual;
								menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
								menu.ShowDialog(this);
								base.Enabled = true;
							}
							else
							{
								ShowNotification("No account data found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void getRoomDataToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			simpletextprompt simplePrompt = new simpletextprompt();
			simplePrompt.StartPosition = FormStartPosition.Manual;
			simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
			simplePrompt.PlaceholderText = "Username";
			string usernameInput = simplePrompt.ShowDialog((string val) => !string.IsNullOrEmpty(val));
			if (!string.IsNullOrEmpty(usernameInput))
			{
				try
				{
					if (string.IsNullOrEmpty(Token))
					{
						ShowNotification("Authentication token is missing.");
						return;
					}
					string accountApiUrl = "https://apim.rec.net/accounts/account?username=" + usernameInput;
					HttpClient client = new HttpClient();
					try
					{
						client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
						JObject accountJson = JObject.Parse(await client.GetStringAsync(accountApiUrl));
						string accountId = accountJson["accountId"]?.ToString();
						if (string.IsNullOrEmpty(accountId))
						{
							ShowNotification("No account found for username: " + usernameInput);
							return;
						}
						string playerApiUrl = "https://match.rec.net/player?id=" + accountId;
						JToken playerData = JArray.Parse(await client.GetStringAsync(playerApiUrl)).FirstOrDefault();
						if (playerData != null)
						{
							string playerId = playerData["playerId"]?.ToString();
							string statusVisibility = playerData["statusVisibility"]?.ToString();
							string platform = playerData["platform"]?.ToString();
							string deviceClass = playerData["deviceClass"]?.ToString();
							string vrMovementMode = playerData["vrMovementMode"]?.ToString();
							string lastOnline = playerData["lastOnline"]?.ToString();
							string isOnline = playerData["isOnline"]?.ToString();
							string appVersion = playerData["appVersion"]?.ToString();
							JToken roomInstance = playerData["roomInstance"];
							string roomInstanceInfo = string.Empty;
							if (roomInstance != null && roomInstance.Type != JTokenType.Null)
							{
								if (roomInstance is JObject roomInstanceObj)
								{
									string roomInstanceId = roomInstanceObj["roomInstanceId"]?.ToString();
									string roomId = roomInstanceObj["roomId"]?.ToString();
									string subRoomId = roomInstanceObj["subRoomId"]?.ToString();
									string location = roomInstanceObj["location"]?.ToString();
									string photonRegion = roomInstanceObj["photonRegion"]?.ToString();
									string photonRoomId = roomInstanceObj["photonRoomId"]?.ToString();
									string roomName = roomInstanceObj["name"]?.ToString();
									string maxCapacity = roomInstanceObj["maxCapacity"]?.ToString();
									string isFull = roomInstanceObj["isFull"]?.ToString();
									string isPrivate = roomInstanceObj["isPrivate"]?.ToString();
									string isInProgress = roomInstanceObj["isInProgress"]?.ToString();
									string voiceConnectionInfo = roomInstanceObj["voiceConnectionInfo"]?.ToString();
									string voiceServerId = roomInstanceObj["voiceServerId"]?.ToString();
									string voiceAuthId = roomInstanceObj["voiceAuthId"]?.ToString();
									string matchmakingPolicy = roomInstanceObj["matchmakingPolicy"]?.ToString();
									roomInstanceInfo = "Room Instance ID: " + roomInstanceId + "\nRoom ID: " + roomId + "\nSubRoom ID: " + subRoomId + "\nLocation: " + location + "\nPhoton Region: " + photonRegion + "\nPhoton Room ID: " + photonRoomId + "\nRoom Name: " + roomName + "\nRoom Max Capacity: " + maxCapacity + "\nRoom Is Full: " + isFull + "\nRoom Is Private: " + isPrivate + "\nRoom Is In Progress: " + isInProgress + "\nMatchmaking Policy: " + matchmakingPolicy + "\nVoice Connection Info: " + voiceConnectionInfo + "\nVoice Server ID: " + voiceServerId + "\nVoice Auth ID: " + voiceAuthId + "\n";
								}
							}
							else
							{
								roomInstanceInfo = "The player is not currently in a room.";
							}
							string formattedResponse = "Player ID: " + playerId + "\nStatus Visibility: " + statusVisibility + "\nPlatform: " + platform + "\nDevice Class: " + deviceClass + "\nVR Movement Mode: " + vrMovementMode + "\nLast Online: " + lastOnline + "\nIs Online: " + isOnline + "\nApp Version: " + appVersion + "\n" + roomInstanceInfo;
							MessageAlert menu = new MessageAlert("Player Room Data", formattedResponse);
							menu.StartPosition = FormStartPosition.Manual;
							menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
							menu.ShowDialog(this);
							base.Enabled = true;
						}
						else
						{
							ShowNotification("No player data found for account ID: " + accountId);
						}
					}
					finally
					{
						((IDisposable)client)?.Dispose();
					}
				}
				catch (Exception ex)
				{
					ShowNotification("Error: " + ex.Message);
				}
			}
			else
			{
				ShowNotification("Invalid or empty username input.");
			}
		}
		else
		{
			ShowNotification("No rows in the DataGridView.");
		}
	}

	private async void checkIfInfluencerToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							HttpClient client = new HttpClient();
							try
							{
								string apiUrl = "https://econ.rec.net/api/influencerpartnerprogram/isinfluencer?accountId=" + username;
								ShowNotification("Influencer Partner: " + await client.GetStringAsync(apiUrl));
							}
							finally
							{
								((IDisposable)client)?.Dispose();
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void getYoureAccountInfoToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await FetchAndDisplayUserProfileAsync();
	}

	private async void checkBanStatusToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string apiUrl = "https://api.rec.net/api/PlayerReporting/v1/moderationBlockDetails";
		HttpResponseMessage response = await client.GetAsync(apiUrl);
		if (!response.IsSuccessStatusCode)
		{
			MessageBox.Show($"API request failed with status code {response.StatusCode}.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
			return;
		}
		string responseBody = await response.Content.ReadAsStringAsync();
		try
		{
			ModerationBlockDetails moderationBlockDetails = JsonConvert.DeserializeObject<ModerationBlockDetails>(responseBody);
			string message = $"Report Category: {moderationBlockDetails.ReportCategory}\n" + $"Duration: {moderationBlockDetails.Duration}\n" + $"Game Session ID: {moderationBlockDetails.GameSessionId}\n" + "Message: " + moderationBlockDetails.Message + "\n" + $"Is Host Kick: {moderationBlockDetails.IsHostKick}\n" + "Player ID Reporter: " + moderationBlockDetails.PlayerIdReporter + "\n" + $"Is Ban: {moderationBlockDetails.IsBan}\n" + $"Is Voice Mod Autoban: {moderationBlockDetails.IsVoiceModAutoban}\n" + $"Is Device Ban: {moderationBlockDetails.IsDeviceBan}\n" + $"Is Warning: {moderationBlockDetails.IsWarning}\n" + "Vote Kick Reason: " + moderationBlockDetails.VoteKickReason + "\nTimeout Started At: " + moderationBlockDetails.TimeoutStartedAt + "\nAssociated Account Username: " + moderationBlockDetails.AssociatedAccountUsername;
			MessageAlert menu = new MessageAlert("Moderation Block Details", message);
			menu.StartPosition = FormStartPosition.Manual;
			menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
			menu.ShowDialog(this);
			base.Enabled = true;
		}
		catch (Exception ex)
		{
			ShowNotification("Error parsing JSON response. Details: " + ex.Message);
		}
	}

	private async void unfriendUserToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string accountId = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(accountId))
					{
						try
						{
							if (await UnfriendUser(accountId))
							{
								ShowNotification("Successfully unfriended the user.");
							}
							else
							{
								ShowNotification("Failed to unfriend the user.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification("Error unfriending user: " + ex.Message);
							return;
						}
					}
					ShowNotification("Account ID is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void unFavoriteAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await UnfavoriteFriends(Token);
	}

	private async void massReportTrollingToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		ulong result;
		string idInput = simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out result));
		if (!string.IsNullOrEmpty(idInput))
		{
			MessageBox.Show("You entered: " + idInput);
		}
		if (ulong.TryParse(idInput, out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameTrolling(token, id, "Hes mass trolling on alt accounts"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
	}

	private async void childrenAccountToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					if (!string.IsNullOrEmpty(selectedRow.Cells[2].Value?.ToString()))
					{
						try
						{
							string apiUrl = "https://accounts.rec.net/account/me/children";
							List<AccountInfo> accountInfoList = JsonConvert.DeserializeObject<List<AccountInfo>>(await client.GetStringAsync(apiUrl));
							if (accountInfoList != null && accountInfoList.Count > 0)
							{
								string formattedResponse = "Children Accounts:\n";
								foreach (AccountInfo accountInfo in accountInfoList)
								{
									formattedResponse = formattedResponse + $"Account ID: {accountInfo.AccountId}\n" + "Username: " + accountInfo.Username + "\nDisplay Name: " + accountInfo.DisplayName + "\nProfile Image: " + accountInfo.ProfileImage + "\n" + $"Is Junior: {accountInfo.IsJunior}\n" + $"Platforms: {accountInfo.Platforms}\n" + $"Personal Pronouns: {accountInfo.PersonalPronouns}\n" + $"Identity Flags: {accountInfo.IdentityFlags}\n" + $"Created At: {accountInfo.CreatedAt:yyyy-MM-dd HH:mm:ss}\n" + $"Is Meta Platform Blocked: {accountInfo.IsMetaPlatformBlocked}\n\n";
								}
								MessageAlert menu = new MessageAlert("Children Account Information", formattedResponse);
								menu.StartPosition = FormStartPosition.Manual;
								menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
								menu.ShowDialog(this);
								base.Enabled = true;
							}
							else
							{
								ShowNotification("No children account information found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void massSubBotToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await client.GetStringAsync(pastebinUrl)).Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => true), out var userId))
		{
			base.Enabled = true;
			List<Task> reportTasks = new List<Task>();
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(SubUser(token.Trim(), userId));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("Sent all subscriptions");
		}
		else
		{
			ShowNotification("Invalid user ID. Please enter a valid number.");
		}
	}

	private async void getMyFriendsCountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 2)
				{
					string username = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(username))
					{
						try
						{
							string apiUrl = "https://api.rec.net/api/relationships/v1/current/friendscount?accountId=" + username;
							if (int.TryParse(await client.GetStringAsync(apiUrl), out var friendsCount))
							{
								string formattedResponse = $"Friends Count: {friendsCount}";
								MessageAlert menu = new MessageAlert("Account Information", formattedResponse);
								menu.StartPosition = FormStartPosition.Manual;
								menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
								menu.ShowDialog(this);
								base.Enabled = true;
							}
							else
							{
								ShowNotification("Failed to parse friends count.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification("Error fetching data: " + ex.Message);
							return;
						}
					}
					ShowNotification("Username is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private async void checkFriendsBioToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.Rows.Count > 0 && guna2DataGridView1.Columns.Count > 0)
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
				if (selectedRow.Cells.Count > 0)
				{
					string accountId = selectedRow.Cells[2].Value?.ToString();
					if (!string.IsNullOrEmpty(accountId))
					{
						try
						{
							string apiUrl = "https://apim.rec.net/accounts/account/" + accountId + "/bio";
							string bio = JObject.Parse(await client.GetStringAsync(apiUrl))["bio"]?.ToString();
							if (!string.IsNullOrEmpty(bio))
							{
								string formattedBio = bio ?? "";
								MessageAlert menu = new MessageAlert("Friend's Bio", formattedBio);
								menu.StartPosition = FormStartPosition.Manual;
								menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
								menu.ShowDialog(this);
								base.Enabled = true;
							}
							else
							{
								ShowNotification("Bio data is empty or not found.");
							}
							return;
						}
						catch (Exception ex)
						{
							ShowNotification(ex.Message ?? "");
							return;
						}
					}
					ShowNotification("Account ID is empty.");
				}
				else
				{
					ShowNotification("Selected row does not have the required cell.");
				}
			}
			else
			{
				ShowNotification("No row is selected.");
			}
		}
		else
		{
			ShowNotification("DataGridView is empty.");
		}
	}

	private void reportOptionsToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private async Task ReportUsernameSexual(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 102,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async Task ReportUsernameTrolling(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 101,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async Task ReportUsernameunder13(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 100,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void massReportToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out var _)), out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameSexual(token, id, "This person is seuxally harassing people over voice chat"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
	}

	private async void massReportUnder13ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out var _)), out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameunder13(token, id, "This player is under 13, this person admitted too it over voice chat"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
	}

	private async Task ReportUsernameProfile(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 104,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void massReportProfileNameToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out var _)), out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameProfile(token, id, "this person has a Inappropriate name, profile picture, or bio"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
	}

	private async Task ReportUsernameDistrolling(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = 103,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void dToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out var _)), out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameDistrolling(token, id, "this person has been Disruptively trolling"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
	}

	private void getFriendsBioToolStripMenuItem1_Click(object sender, EventArgs e)
	{
	}

	private async void massReportImageToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] array = (await val2.GetStringAsync(pastebinUrl)).Split('\n');
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Image ID";
		ulong result;
		string id = simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out result));
		base.Enabled = true;
		string[] array2 = array;
		foreach (string token in array2)
		{
			reportTasks.Add(ReportUser(token.Replace("\n", "").Replace("\r", ""), id, " "));
		}
		await Task.WhenAll(reportTasks);
		ShowNotification("All Reports sent out");
	}

	private async Task<string> GetAccountIdFromUsername(string username)
	{
		string url = "https://apim.rec.net/accounts/account?username=" + username;
		return JsonConvert.DeserializeObject<AccountResponse>(await client.GetStringAsync(url))?.AccountId ?? string.Empty;
	}

	public async Task StartMassAdd()
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		((HttpHeaders)client.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await client.GetStringAsync(pastebinUrl)).Split(new char[1] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Username";
		string userInput = simplePrompt.ShowDialog((string val) => true);
		string accountId = await GetAccountIdFromUsername(userInput);
		if (string.IsNullOrEmpty(accountId))
		{
			ShowNotification("Invalid username. Could not find account.");
			return;
		}
		base.Enabled = true;
		List<Task> reportTasks = new List<Task>();
		string[] array = tokens;
		foreach (string token in array)
		{
			reportTasks.Add(AddRelationship(token.Trim(), accountId));
		}
		await Task.WhenAll(reportTasks);
		ShowNotification("Sent all mass add requests.");
	}

	private async Task AddRelationship(string token, string accountId)
	{
		string relationshipLink = "https://api.rec.net/api/relationships/v3/" + accountId;
		StringContent relationshipBody = new StringContent(JsonConvert.SerializeObject(new
		{
			PlayerID = accountId,
			RelationshipType = 1,
			Favorited = 0,
			Muted = 0,
			Ignored = 0
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, relationshipLink);
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
		((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
		request.Content = (HttpContent)(object)relationshipBody;
		HttpResponseMessage response = await client.SendAsync(request);
		if (response.IsSuccessStatusCode)
		{
			await response.Content.ReadAsStringAsync();
		}
	}

	private async void massAddFriendToolStripMenuItem_Click(object sender, EventArgs e)
	{
		await StartMassAdd();
	}

	private async Task ReportUsernameBanevasion(string token, ulong playerId, string reportDetails)
	{
		string reportUrl = "https://apim.rec.net/apis/api/PlayerReporting/v1/internal/create";
		StringContent reportBody = new StringContent(JsonConvert.SerializeObject(new
		{
			ReportCategory = -1,
			PlayerIdReported = playerId.ToString(),
			RoomId = "0",
			Details = reportDetails
		}), Encoding.UTF8, "application/json");
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, reportUrl);
		try
		{
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Accept", "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Authorization", "Bearer " + token);
			request.Content = (HttpContent)(object)reportBody;
			try
			{
				HttpResponseMessage response = await client.SendAsync(request);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Report sent successfully.");
				}
				else if (response.StatusCode == HttpStatusCode.Forbidden)
				{
					string responseContent = await response.Content.ReadAsStringAsync();
					int retryAfterSeconds = GetRetryAfterSeconds(response);
					Console.WriteLine("Quota exceeded. Response: " + responseContent);
					Console.WriteLine($"Retrying after {retryAfterSeconds} seconds.");
					await Task.Delay(retryAfterSeconds * 1000);
					await ReportUsernameAsync(token, playerId, reportDetails);
				}
				else
				{
					string responseContent2 = await response.Content.ReadAsStringAsync();
					Console.WriteLine($"Failed to send report. Status code: {response.StatusCode}");
					Console.WriteLine("Response: " + responseContent2);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Exception occurred: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void reportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		string pastebinUrl = "https://lolzopzsniff.xyz/assets/zopzfiles/tokens.txt";
		HttpClient val2 = new HttpClient();
		((HttpHeaders)val2.DefaultRequestHeaders).TryAddWithoutValidation("User-Agent", "Keyauth");
		string[] tokens = (await val2.GetStringAsync(pastebinUrl)).Split(new char[2] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
		List<Task> reportTasks = new List<Task>();
		simpletextprompt simplePrompt = new simpletextprompt();
		simplePrompt.StartPosition = FormStartPosition.Manual;
		simplePrompt.Location = new Point(base.Location.X + (base.Width - simplePrompt.Width) / 2, base.Location.Y + (base.Height - simplePrompt.Height) / 2);
		simplePrompt.PlaceholderText = "Account ID";
		if (ulong.TryParse(simplePrompt.ShowDialog((string val) => ulong.TryParse(val, out var _)), out var id))
		{
			base.Enabled = true;
			string[] array = tokens;
			foreach (string token in array)
			{
				reportTasks.Add(ReportUsernameBanevasion(token, id, "this person is Ban evading on this account"));
			}
			await Task.WhenAll(reportTasks);
			ShowNotification("All Reports sent out");
		}
		else
		{
			ShowNotification("Invalid ID entered. Please enter a valid number.");
		}
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.rec));
		this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.logInContextMenu1 = new LoginTheme.LogInContextMenu();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.getFriendsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.getFriendsBioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.getMyFriendsCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unFavoriteAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unfriendUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.checkIfInfluencerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.checkFriendsBioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
		this.lookupUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.lookupUseridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.getRoomDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massSubBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massAddFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem23 = new System.Windows.Forms.ToolStripMenuItem();
		this.checkBanStatusToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.accountOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.getYoureAccountInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.childrenAccountToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.reportOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportTrollingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportProfileNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportUnder13ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.dToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.guna2TextBox1 = new Guna.UI2.WinForms.Guna2TextBox();
		this.logInContextMenu5 = new LoginTheme.LogInContextMenu();
		this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
		this.clearAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
		this.pingCellToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.copyEntireRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
		this.getUserRoomsDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.takeBioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.grabToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unbanMeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.checkBanStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.checkIfInfluencerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.friendsCountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.childrenAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unFaverateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unfriendUserToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.subBotToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massCheerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.label4 = new System.Windows.Forms.Label();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label1 = new System.Windows.Forms.Label();
		this.guna2ControlBox4 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox5 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox6 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.reportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.guna2GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).BeginInit();
		this.logInContextMenu1.SuspendLayout();
		this.logInContextMenu5.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.guna2GroupBox1.BackColor = System.Drawing.Color.Transparent;
		this.guna2GroupBox1.BorderColor = System.Drawing.Color.Black;
		this.guna2GroupBox1.Controls.Add(this.guna2VScrollBar1);
		this.guna2GroupBox1.Controls.Add(this.guna2TextBox1);
		this.guna2GroupBox1.Controls.Add(this.guna2DataGridView1);
		this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2GroupBox1.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox1.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox1.Location = new System.Drawing.Point(0, -7);
		this.guna2GroupBox1.Name = "guna2GroupBox1";
		this.guna2GroupBox1.Size = new System.Drawing.Size(830, 479);
		this.guna2GroupBox1.TabIndex = 721;
		this.guna2GroupBox1.Text = "Friends List";
		this.guna2VScrollBar1.BindingContainer = this.guna2DataGridView1;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 10;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(811, 41);
		this.guna2VScrollBar1.Minimum = 1;
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 357);
		this.guna2VScrollBar1.TabIndex = 715;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.guna2VScrollBar1.Value = 1;
		this.guna2DataGridView1.AllowUserToResizeColumns = false;
		this.guna2DataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.guna2DataGridView1.BackgroundColor = System.Drawing.Color.Black;
		this.guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.guna2DataGridView1.ColumnHeadersHeight = 20;
		this.guna2DataGridView1.ContextMenuStrip = this.logInContextMenu1;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.guna2DataGridView1.Dock = System.Windows.Forms.DockStyle.Top;
		this.guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView1.Location = new System.Drawing.Point(0, 40);
		this.guna2DataGridView1.Name = "guna2DataGridView1";
		this.guna2DataGridView1.ReadOnly = true;
		this.guna2DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.guna2DataGridView1.RowHeadersVisible = false;
		this.guna2DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
		this.guna2DataGridView1.RowTemplate.Height = 30;
		this.guna2DataGridView1.Size = new System.Drawing.Size(830, 385);
		this.guna2DataGridView1.TabIndex = 714;
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
		this.guna2DataGridView1.ThemeStyle.RowsStyle.Height = 30;
		this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.guna2DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(guna2DataGridView1_CellContentClick);
		this.guna2DataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(guna2DataGridView1_KeyDown);
		this.guna2DataGridView1.MouseHover += new System.EventHandler(guna2DataGridView1_MouseHover);
		this.guna2DataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(guna2DataGridView1_MouseMove);
		this.logInContextMenu1.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu1.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[8] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.getFriendsToolStripMenuItem, this.toolStripMenuItem3, this.toolStripMenuItem11, this.toolStripMenuItem23, this.accountOptionsToolStripMenuItem, this.reportOptionsToolStripMenuItem });
		this.logInContextMenu1.Name = "logInContextMenu2";
		this.logInContextMenu1.ShowImageMargin = false;
		this.logInContextMenu1.Size = new System.Drawing.Size(163, 202);
		this.toolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(162, 22);
		this.toolStripMenuItem1.Text = "Copy To Clipboard";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click_1);
		this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem2.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(162, 22);
		this.toolStripMenuItem2.Text = "Clear All";
		this.toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click_1);
		this.getFriendsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.getFriendsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.getFriendsToolStripMenuItem.Name = "getFriendsToolStripMenuItem";
		this.getFriendsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
		this.getFriendsToolStripMenuItem.Text = "Get Friends";
		this.getFriendsToolStripMenuItem.Click += new System.EventHandler(getFriendsToolStripMenuItem_Click);
		this.toolStripMenuItem3.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.getFriendsBioToolStripMenuItem, this.getMyFriendsCountToolStripMenuItem, this.unFavoriteAllToolStripMenuItem, this.unfriendUserToolStripMenuItem1, this.checkIfInfluencerToolStripMenuItem1, this.checkFriendsBioToolStripMenuItem });
		this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem3.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(162, 22);
		this.toolStripMenuItem3.Text = "Friends Options";
		this.getFriendsBioToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.getFriendsBioToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.getFriendsBioToolStripMenuItem.Name = "getFriendsBioToolStripMenuItem";
		this.getFriendsBioToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
		this.getFriendsBioToolStripMenuItem.Text = "Take Friends Bio";
		this.getFriendsBioToolStripMenuItem.Click += new System.EventHandler(getFriendsBioToolStripMenuItem_Click);
		this.getMyFriendsCountToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.getMyFriendsCountToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.getMyFriendsCountToolStripMenuItem.Name = "getMyFriendsCountToolStripMenuItem";
		this.getMyFriendsCountToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
		this.getMyFriendsCountToolStripMenuItem.Text = "Get My Friends Count";
		this.getMyFriendsCountToolStripMenuItem.Click += new System.EventHandler(getMyFriendsCountToolStripMenuItem_Click);
		this.unFavoriteAllToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.unFavoriteAllToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.unFavoriteAllToolStripMenuItem.Name = "unFavoriteAllToolStripMenuItem";
		this.unFavoriteAllToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
		this.unFavoriteAllToolStripMenuItem.Text = "UnFavorite All";
		this.unFavoriteAllToolStripMenuItem.Click += new System.EventHandler(unFavoriteAllToolStripMenuItem_Click);
		this.unfriendUserToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.unfriendUserToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.unfriendUserToolStripMenuItem1.Name = "unfriendUserToolStripMenuItem1";
		this.unfriendUserToolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
		this.unfriendUserToolStripMenuItem1.Text = "Unfriend User";
		this.unfriendUserToolStripMenuItem1.Click += new System.EventHandler(unfriendUserToolStripMenuItem1_Click);
		this.checkIfInfluencerToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.checkIfInfluencerToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.checkIfInfluencerToolStripMenuItem1.Name = "checkIfInfluencerToolStripMenuItem1";
		this.checkIfInfluencerToolStripMenuItem1.Size = new System.Drawing.Size(194, 22);
		this.checkIfInfluencerToolStripMenuItem1.Text = "Check If Influencer";
		this.checkIfInfluencerToolStripMenuItem1.Click += new System.EventHandler(checkIfInfluencerToolStripMenuItem1_Click);
		this.checkFriendsBioToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.checkFriendsBioToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.checkFriendsBioToolStripMenuItem.Name = "checkFriendsBioToolStripMenuItem";
		this.checkFriendsBioToolStripMenuItem.Size = new System.Drawing.Size(194, 22);
		this.checkFriendsBioToolStripMenuItem.Text = "Check Friends Bio";
		this.checkFriendsBioToolStripMenuItem.Click += new System.EventHandler(checkFriendsBioToolStripMenuItem_Click);
		this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[5] { this.lookupUsernameToolStripMenuItem, this.lookupUseridToolStripMenuItem, this.getRoomDataToolStripMenuItem, this.massSubBotToolStripMenuItem, this.massAddFriendToolStripMenuItem });
		this.toolStripMenuItem11.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem11.Name = "toolStripMenuItem11";
		this.toolStripMenuItem11.Size = new System.Drawing.Size(162, 22);
		this.toolStripMenuItem11.Text = "Tool Lookup Options";
		this.lookupUsernameToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.lookupUsernameToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.lookupUsernameToolStripMenuItem.Name = "lookupUsernameToolStripMenuItem";
		this.lookupUsernameToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.lookupUsernameToolStripMenuItem.Text = "Lookup Username";
		this.lookupUsernameToolStripMenuItem.Click += new System.EventHandler(lookupUsernameToolStripMenuItem_Click);
		this.lookupUseridToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.lookupUseridToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.lookupUseridToolStripMenuItem.Name = "lookupUseridToolStripMenuItem";
		this.lookupUseridToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.lookupUseridToolStripMenuItem.Text = "Lookup Userid";
		this.lookupUseridToolStripMenuItem.Click += new System.EventHandler(lookupUseridToolStripMenuItem_Click);
		this.getRoomDataToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.getRoomDataToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.getRoomDataToolStripMenuItem.Name = "getRoomDataToolStripMenuItem";
		this.getRoomDataToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.getRoomDataToolStripMenuItem.Text = "Get Room Data";
		this.getRoomDataToolStripMenuItem.Click += new System.EventHandler(getRoomDataToolStripMenuItem_Click);
		this.massSubBotToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massSubBotToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massSubBotToolStripMenuItem.Name = "massSubBotToolStripMenuItem";
		this.massSubBotToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.massSubBotToolStripMenuItem.Text = "Mass Sub Bot";
		this.massSubBotToolStripMenuItem.Click += new System.EventHandler(massSubBotToolStripMenuItem_Click);
		this.massAddFriendToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massAddFriendToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massAddFriendToolStripMenuItem.Name = "massAddFriendToolStripMenuItem";
		this.massAddFriendToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.massAddFriendToolStripMenuItem.Text = "Mass Add Friend";
		this.massAddFriendToolStripMenuItem.Click += new System.EventHandler(massAddFriendToolStripMenuItem_Click);
		this.toolStripMenuItem23.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.checkBanStatusToolStripMenuItem1 });
		this.toolStripMenuItem23.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem23.Name = "toolStripMenuItem23";
		this.toolStripMenuItem23.Size = new System.Drawing.Size(162, 22);
		this.toolStripMenuItem23.Text = "Misc Options";
		this.checkBanStatusToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.checkBanStatusToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.checkBanStatusToolStripMenuItem1.Name = "checkBanStatusToolStripMenuItem1";
		this.checkBanStatusToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.checkBanStatusToolStripMenuItem1.Text = "Check Ban Status";
		this.checkBanStatusToolStripMenuItem1.Click += new System.EventHandler(checkBanStatusToolStripMenuItem1_Click);
		this.accountOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.getYoureAccountInfoToolStripMenuItem, this.childrenAccountToolStripMenuItem1 });
		this.accountOptionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.accountOptionsToolStripMenuItem.Name = "accountOptionsToolStripMenuItem";
		this.accountOptionsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
		this.accountOptionsToolStripMenuItem.Text = "Account Options";
		this.getYoureAccountInfoToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.getYoureAccountInfoToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.getYoureAccountInfoToolStripMenuItem.Name = "getYoureAccountInfoToolStripMenuItem";
		this.getYoureAccountInfoToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
		this.getYoureAccountInfoToolStripMenuItem.Text = "Get You're Account info";
		this.getYoureAccountInfoToolStripMenuItem.Click += new System.EventHandler(getYoureAccountInfoToolStripMenuItem_Click);
		this.childrenAccountToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.childrenAccountToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.childrenAccountToolStripMenuItem1.Name = "childrenAccountToolStripMenuItem1";
		this.childrenAccountToolStripMenuItem1.Size = new System.Drawing.Size(208, 22);
		this.childrenAccountToolStripMenuItem1.Text = "Children Account";
		this.childrenAccountToolStripMenuItem1.Click += new System.EventHandler(childrenAccountToolStripMenuItem1_Click);
		this.reportOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[7] { this.massReportTrollingToolStripMenuItem, this.massReportToolStripMenuItem1, this.massReportProfileNameToolStripMenuItem, this.massReportUnder13ToolStripMenuItem, this.reportToolStripMenuItem, this.dToolStripMenuItem, this.massReportImageToolStripMenuItem });
		this.reportOptionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.reportOptionsToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.reportOptionsToolStripMenuItem.Name = "reportOptionsToolStripMenuItem";
		this.reportOptionsToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
		this.reportOptionsToolStripMenuItem.Text = "Report Options";
		this.reportOptionsToolStripMenuItem.Click += new System.EventHandler(reportOptionsToolStripMenuItem_Click);
		this.massReportTrollingToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massReportTrollingToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massReportTrollingToolStripMenuItem.Name = "massReportTrollingToolStripMenuItem";
		this.massReportTrollingToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.massReportTrollingToolStripMenuItem.Text = "Mass Report Trolling";
		this.massReportTrollingToolStripMenuItem.Click += new System.EventHandler(massReportTrollingToolStripMenuItem_Click);
		this.massReportToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.massReportToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.massReportToolStripMenuItem1.Name = "massReportToolStripMenuItem1";
		this.massReportToolStripMenuItem1.Size = new System.Drawing.Size(247, 22);
		this.massReportToolStripMenuItem1.Text = "Mass Report Sexual";
		this.massReportToolStripMenuItem1.Click += new System.EventHandler(massReportToolStripMenuItem1_Click);
		this.massReportProfileNameToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massReportProfileNameToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massReportProfileNameToolStripMenuItem.Name = "massReportProfileNameToolStripMenuItem";
		this.massReportProfileNameToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.massReportProfileNameToolStripMenuItem.Text = "Mass Report Profile";
		this.massReportProfileNameToolStripMenuItem.Click += new System.EventHandler(massReportProfileNameToolStripMenuItem_Click);
		this.massReportUnder13ToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massReportUnder13ToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massReportUnder13ToolStripMenuItem.Name = "massReportUnder13ToolStripMenuItem";
		this.massReportUnder13ToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.massReportUnder13ToolStripMenuItem.Text = "Mass Report Under 13";
		this.massReportUnder13ToolStripMenuItem.Click += new System.EventHandler(massReportUnder13ToolStripMenuItem_Click);
		this.dToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.dToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.dToolStripMenuItem.Name = "dToolStripMenuItem";
		this.dToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.dToolStripMenuItem.Text = "Mass Report Disruptive trolling";
		this.dToolStripMenuItem.Click += new System.EventHandler(dToolStripMenuItem_Click);
		this.massReportImageToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.massReportImageToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.massReportImageToolStripMenuItem.Name = "massReportImageToolStripMenuItem";
		this.massReportImageToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.massReportImageToolStripMenuItem.Text = "Mass Report Image";
		this.massReportImageToolStripMenuItem.Click += new System.EventHandler(massReportImageToolStripMenuItem_Click);
		this.guna2TextBox1.Animated = true;
		this.guna2TextBox1.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.BorderThickness = 0;
		this.guna2TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
		this.guna2TextBox1.DefaultText = "";
		this.guna2TextBox1.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2TextBox1.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2TextBox1.FocusedState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2TextBox1.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.Location = new System.Drawing.Point(0, 443);
		this.guna2TextBox1.Name = "guna2TextBox1";
		this.guna2TextBox1.PasswordChar = '\0';
		this.guna2TextBox1.PlaceholderForeColor = System.Drawing.Color.White;
		this.guna2TextBox1.PlaceholderText = "Username";
		this.guna2TextBox1.SelectedText = "";
		this.guna2TextBox1.Size = new System.Drawing.Size(830, 36);
		this.guna2TextBox1.TabIndex = 716;
		this.guna2TextBox1.TextChanged += new System.EventHandler(guna2TextBox1_TextChanged);
		this.logInContextMenu5.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu5.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu5.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu5.Items.AddRange(new System.Windows.Forms.ToolStripItem[20]
		{
			this.toolStripMenuItem8, this.clearAllToolStripMenuItem1, this.toolStripMenuItem9, this.pingCellToolStripMenuItem2, this.copyEntireRowToolStripMenuItem, this.toolStripMenuItem10, this.getUserRoomsDataToolStripMenuItem, this.takeBioToolStripMenuItem, this.grabToolStripMenuItem, this.massReportToolStripMenuItem,
			this.massReportUserToolStripMenuItem, this.unbanMeToolStripMenuItem, this.checkBanStatusToolStripMenuItem, this.checkIfInfluencerToolStripMenuItem, this.friendsCountToolStripMenuItem, this.childrenAccountToolStripMenuItem, this.unFaverateAllToolStripMenuItem, this.unfriendUserToolStripMenuItem, this.subBotToolStripMenuItem, this.massCheerToolStripMenuItem1
		});
		this.logInContextMenu5.Name = "logInContextMenu2";
		this.logInContextMenu5.ShowImageMargin = false;
		this.logInContextMenu5.Size = new System.Drawing.Size(177, 444);
		this.toolStripMenuItem8.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem8.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem8.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem8.Name = "toolStripMenuItem8";
		this.toolStripMenuItem8.Size = new System.Drawing.Size(176, 22);
		this.toolStripMenuItem8.Text = "Copy To Clipboard";
		this.toolStripMenuItem8.Click += new System.EventHandler(toolStripMenuItem8_Click);
		this.clearAllToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.clearAllToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.clearAllToolStripMenuItem1.Name = "clearAllToolStripMenuItem1";
		this.clearAllToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
		this.clearAllToolStripMenuItem1.Text = "Clear All";
		this.clearAllToolStripMenuItem1.Click += new System.EventHandler(clearAllToolStripMenuItem1_Click);
		this.toolStripMenuItem9.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem9.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem9.Name = "toolStripMenuItem9";
		this.toolStripMenuItem9.Size = new System.Drawing.Size(176, 22);
		this.toolStripMenuItem9.Text = "Grab Friends";
		this.toolStripMenuItem9.Click += new System.EventHandler(toolStripMenuItem9_Click);
		this.pingCellToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.pingCellToolStripMenuItem2.Name = "pingCellToolStripMenuItem2";
		this.pingCellToolStripMenuItem2.Size = new System.Drawing.Size(176, 22);
		this.pingCellToolStripMenuItem2.Text = "Get Friends Bio";
		this.pingCellToolStripMenuItem2.Click += new System.EventHandler(pingCellToolStripMenuItem2_Click);
		this.copyEntireRowToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.copyEntireRowToolStripMenuItem.Name = "copyEntireRowToolStripMenuItem";
		this.copyEntireRowToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.copyEntireRowToolStripMenuItem.Text = "Lookup Username";
		this.copyEntireRowToolStripMenuItem.Click += new System.EventHandler(copyEntireRowToolStripMenuItem_Click);
		this.toolStripMenuItem10.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem10.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem10.Name = "toolStripMenuItem10";
		this.toolStripMenuItem10.Size = new System.Drawing.Size(176, 22);
		this.toolStripMenuItem10.Text = "Lookup Userid";
		this.toolStripMenuItem10.Click += new System.EventHandler(toolStripMenuItem10_Click);
		this.getUserRoomsDataToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.getUserRoomsDataToolStripMenuItem.Name = "getUserRoomsDataToolStripMenuItem";
		this.getUserRoomsDataToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.getUserRoomsDataToolStripMenuItem.Text = "Get User Rooms Data";
		this.getUserRoomsDataToolStripMenuItem.Click += new System.EventHandler(getUserRoomsDataToolStripMenuItem_Click);
		this.takeBioToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.takeBioToolStripMenuItem.Name = "takeBioToolStripMenuItem";
		this.takeBioToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.takeBioToolStripMenuItem.Text = "Take Friends Bio";
		this.takeBioToolStripMenuItem.Click += new System.EventHandler(takeBioToolStripMenuItem_Click);
		this.grabToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.grabToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.grabToolStripMenuItem.Name = "grabToolStripMenuItem";
		this.grabToolStripMenuItem.ShowShortcutKeys = false;
		this.grabToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.grabToolStripMenuItem.Text = "Get You're Account info";
		this.grabToolStripMenuItem.Click += new System.EventHandler(grabToolStripMenuItem_Click);
		this.massReportToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.massReportToolStripMenuItem.Name = "massReportToolStripMenuItem";
		this.massReportToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.massReportToolStripMenuItem.Text = "Mass Report";
		this.massReportToolStripMenuItem.Click += new System.EventHandler(massReportToolStripMenuItem_Click);
		this.massReportUserToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.massReportUserToolStripMenuItem.Name = "massReportUserToolStripMenuItem";
		this.massReportUserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.massReportUserToolStripMenuItem.Text = "Mass Report User ID";
		this.massReportUserToolStripMenuItem.Click += new System.EventHandler(massReportUserToolStripMenuItem_Click);
		this.unbanMeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.unbanMeToolStripMenuItem.Name = "unbanMeToolStripMenuItem";
		this.unbanMeToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.unbanMeToolStripMenuItem.Text = "Unban Me";
		this.unbanMeToolStripMenuItem.Click += new System.EventHandler(unbanMeToolStripMenuItem_Click);
		this.checkBanStatusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.checkBanStatusToolStripMenuItem.Name = "checkBanStatusToolStripMenuItem";
		this.checkBanStatusToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.checkBanStatusToolStripMenuItem.Text = "Check Ban Status";
		this.checkBanStatusToolStripMenuItem.Click += new System.EventHandler(checkBanStatusToolStripMenuItem_Click);
		this.checkIfInfluencerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.checkIfInfluencerToolStripMenuItem.Name = "checkIfInfluencerToolStripMenuItem";
		this.checkIfInfluencerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.checkIfInfluencerToolStripMenuItem.Text = "Check If Influencer";
		this.checkIfInfluencerToolStripMenuItem.Click += new System.EventHandler(checkIfInfluencerToolStripMenuItem_Click);
		this.friendsCountToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.friendsCountToolStripMenuItem.Name = "friendsCountToolStripMenuItem";
		this.friendsCountToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.friendsCountToolStripMenuItem.Text = "Get My Friends Count";
		this.friendsCountToolStripMenuItem.Click += new System.EventHandler(friendsCountToolStripMenuItem_Click);
		this.childrenAccountToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.childrenAccountToolStripMenuItem.Name = "childrenAccountToolStripMenuItem";
		this.childrenAccountToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.childrenAccountToolStripMenuItem.Text = "Children Account";
		this.childrenAccountToolStripMenuItem.Click += new System.EventHandler(childrenAccountToolStripMenuItem_Click);
		this.unFaverateAllToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.unFaverateAllToolStripMenuItem.Name = "unFaverateAllToolStripMenuItem";
		this.unFaverateAllToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.unFaverateAllToolStripMenuItem.Text = "UnFavorite all";
		this.unFaverateAllToolStripMenuItem.Click += new System.EventHandler(unFaverateAllToolStripMenuItem_Click);
		this.unfriendUserToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.unfriendUserToolStripMenuItem.Name = "unfriendUserToolStripMenuItem";
		this.unfriendUserToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.unfriendUserToolStripMenuItem.Text = "Unfriend User";
		this.unfriendUserToolStripMenuItem.Click += new System.EventHandler(unfriendUserToolStripMenuItem_Click);
		this.subBotToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.subBotToolStripMenuItem.Name = "subBotToolStripMenuItem";
		this.subBotToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
		this.subBotToolStripMenuItem.Text = "Sub Bot";
		this.subBotToolStripMenuItem.Click += new System.EventHandler(subBotToolStripMenuItem_Click);
		this.massCheerToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.massCheerToolStripMenuItem1.Name = "massCheerToolStripMenuItem1";
		this.massCheerToolStripMenuItem1.Size = new System.Drawing.Size(176, 22);
		this.massCheerToolStripMenuItem1.Text = "Mass Cheer";
		this.massCheerToolStripMenuItem1.Click += new System.EventHandler(massCheerToolStripMenuItem1_Click);
		this.label4.BackColor = System.Drawing.Color.Black;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(2, 4);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(101, 17);
		this.label4.TabIndex = 716;
		this.label4.Text = "ZOPZ SNIFF";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.label1);
		this.panel2.Controls.Add(this.guna2ControlBox4);
		this.panel2.Controls.Add(this.guna2ControlBox5);
		this.panel2.Controls.Add(this.guna2ControlBox6);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(830, 26);
		this.panel2.TabIndex = 722;
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
		this.guna2ControlBox4.Animated = true;
		this.guna2ControlBox4.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox4.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2ControlBox4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox4.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox4.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
		this.guna2ControlBox4.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox4.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox4.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox4.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox4.Location = new System.Drawing.Point(695, 0);
		this.guna2ControlBox4.Name = "guna2ControlBox4";
		this.guna2ControlBox4.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox4.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox4.TabIndex = 156;
		this.guna2ControlBox4.Click += new System.EventHandler(guna2ControlBox4_Click);
		this.guna2ControlBox5.Animated = true;
		this.guna2ControlBox5.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox5.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox5.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox5.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MaximizeBox;
		this.guna2ControlBox5.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox5.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox5.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox5.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox5.Location = new System.Drawing.Point(740, 0);
		this.guna2ControlBox5.Name = "guna2ControlBox5";
		this.guna2ControlBox5.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox5.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox5.TabIndex = 158;
		this.guna2ControlBox6.Animated = true;
		this.guna2ControlBox6.BackColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox6.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
		this.guna2ControlBox6.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
		this.guna2ControlBox6.Cursor = System.Windows.Forms.Cursors.Default;
		this.guna2ControlBox6.Dock = System.Windows.Forms.DockStyle.Right;
		this.guna2ControlBox6.FillColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox6.IconColor = System.Drawing.Color.White;
		this.guna2ControlBox6.Location = new System.Drawing.Point(785, 0);
		this.guna2ControlBox6.Name = "guna2ControlBox6";
		this.guna2ControlBox6.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox6.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox6.TabIndex = 157;
		this.guna2ControlBox6.Click += new System.EventHandler(guna2ControlBox6_Click);
		this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6;
		this.guna2DragControl1.TransparentWhileDrag = false;
		this.timer2.Interval = 2000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.reportToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.reportToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.reportToolStripMenuItem.Name = "reportToolStripMenuItem";
		this.reportToolStripMenuItem.Size = new System.Drawing.Size(247, 22);
		this.reportToolStripMenuItem.Text = "Mass Report Ban Evasion";
		this.reportToolStripMenuItem.Click += new System.EventHandler(reportToolStripMenuItem_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(830, 472);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.guna2GroupBox1);
		base.Controls.Add(this.label4);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "rec";
		this.RightToLeft = System.Windows.Forms.RightToLeft.No;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "ZOPZ SNIFF";
		base.Load += new System.EventHandler(rec_Load);
		this.guna2GroupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).EndInit();
		this.logInContextMenu1.ResumeLayout(false);
		this.logInContextMenu5.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
