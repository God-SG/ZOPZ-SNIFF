using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SNIFF.prompts;
using SNIFF.Properties;
using Xbox_API;
using Xbox_API.Models;

namespace SNIFF;

public class xboxpartyoptions : Form
{
	public class Member
	{
		public string xuid { get; set; }

		public string gamertag { get; set; }
	}

	public class PartyInfo
	{
		public List<Member> members { get; set; }
	}

	public class SessionInfo
	{
		public string xuid { get; set; }

		public string sessionId { get; set; }
	}

	public class SessionResponse
	{
		public List<SessionInfo> sessions { get; set; }
	}

	public class PartyMember
	{
		public string Gamertag { get; set; }

		public string Xuid { get; set; }

		public string MemberStatus { get; set; }
	}

	public class FriendsList
	{
		[JsonProperty("people")]
		public List<Friend> Friends { get; set; }
	}

	public class Friend
	{
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		[JsonProperty("displayPicRaw")]
		public string Avatar { get; set; }

		[JsonProperty("xuid")]
		public string XboxUserId { get; set; }

		[JsonProperty("presenceText")]
		public string Presence { get; set; }

		[JsonProperty("xboxOneRep")]
		public string XboxOneRep { get; set; }

		[JsonProperty("realName")]
		public string RealName { get; set; }

		[JsonProperty("gamerScore")]
		public string GamerScore { get; set; }

		[JsonProperty("presenceState")]
		public string PresenceState { get; set; }

		[JsonProperty("isBroadcasting")]
		public bool IsBroadcasting { get; set; }
	}

	private NotificationForm notification;

	private const string baseUrl = "https://xblmessaging.xboxlive.com";

	private HttpClient httpclient = new HttpClient();

	private readonly HttpClient httpClients;

	private string token;

	private IContainer components;

	private PictureBox pictureBox2;

	private Panel panel3;

	private Timer timer1;

	private Guna2VScrollBar guna2VScrollBar3;

	private Guna2DataGridView dataGridView1;

	private Panel panel2;

	private Label label4;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	private Guna2GroupBox guna2GroupBox3;

	private LogInContextMenu logInContextMenu2;

	private ToolStripMenuItem copySourceIPToolStripMenuItem;

	private ToolStripMenuItem pingCellToolStripMenuItem;

	private Guna2GroupBox guna2GroupBox1;

	private Guna2DataGridView guna2DataGridView1;

	private Guna2VScrollBar guna2VScrollBar1;

	private LogInContextMenu logInContextMenu1;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem toolStripMenuItem3;

	private ToolStripMenuItem clearAllToolStripMenuItem;

	private DataGridViewTextBoxColumn Column1;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn Column4;

	private ToolStripMenuItem toolStripMenuItem4;

	private ToolStripMenuItem partyOptionsToolStripMenuItem;

	private ToolStripMenuItem openPartyToolStripMenuItem1;

	private ToolStripMenuItem closePartyToolStripMenuItem1;

	private ToolStripMenuItem partyStatusToolStripMenuItem;

	private ToolStripMenuItem friendsOptionsToolStripMenuItem;

	private ToolStripMenuItem muteUserToolStripMenuItem1;

	private ToolStripMenuItem unmuteUserToolStripMenuItem1;

	private ToolStripMenuItem addFriendToolStripMenuItem;

	private ToolStripMenuItem unfriendUserToolStripMenuItem1;

	private ToolStripMenuItem miscOptionsToolStripMenuItem;

	private ToolStripMenuItem massReportToolStripMenuItem1;

	private ToolStripMenuItem crashPartyHostToolStripMenuItem;

	private Guna2GroupBox guna2GroupBox2;

	private Guna2Button LoginBTN;

	private Guna2Button guna2Button2;

	private Guna2Button guna2Button1;

	private Timer timer2;

	private Guna2Elipse guna2Elipse1;

	private Guna2Elipse guna2Elipse2;

	private Guna2Elipse guna2Elipse3;

	private OpenFileDialog openFileDialog1;

	public string UserID { get; set; }

	public string GamerTag { get; private set; }

	public string imageUrl { get; private set; }

	public string Email { get; private set; }

	public xboxpartyoptions(string authorizationToken)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		token = authorizationToken;
		InitializeComponent();
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
		dataGridView1.MouseMove += dataGridView1_MouseMove;
		dataGridView1.MouseLeave += delegate
		{
			ResetRowColors(dataGridView1);
		};
		guna2DataGridView1.MouseMove += guna2DataGridView1_MouseMove;
		guna2DataGridView1.MouseLeave += delegate
		{
			ResetRowColors(guna2DataGridView1);
		};
		ApplyBackgroundColor();
	}

	public async void GetUser()
	{
		try
		{
			Profile profile = await new XboxAPI(token).GetProfileAsync();
			UserID = profile.ProfileUsers.FirstOrDefault().HostId;
			GamerTag = profile.ProfileUsers.First().Settings.First().Value;
		}
		catch (ArgumentException ex)
		{
			ShowNotification(ex.Message ?? "");
		}
		catch (HttpRequestException val)
		{
			HttpRequestException ex2 = val;
			ShowNotification(((Exception)(object)ex2).Message ?? "");
		}
		catch (Exception ex3)
		{
			ShowNotification(ex3.Message ?? "");
		}
	}

	private void xboxpartyoptions_Load(object sender, EventArgs e)
	{
		GetUser();
		dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		guna2DataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		dataGridView1.RowTemplate.Height = 35;
		guna2DataGridView1.RowTemplate.Height = 35;
		dataGridView1.AllowUserToAddRows = false;
		guna2DataGridView1.AllowUserToAddRows = false;
	}

	public void Alert(string msg, alert.enmType type)
	{
		new alert().showAlert(msg, type);
	}

	private void panel3_Paint(object sender, PaintEventArgs e)
	{
	}

	private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void copySourceIPToolStripMenuItem_Click(object sender, EventArgs e)
	{
		dataGridView1.Rows.Clear();
		ShowNotification("Cleared successfully");
	}

	private async Task<string> Unkickable(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/nokick/";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = token,
			xuid = userID
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "No message provided";
			}
			catch (HttpRequestException val)
			{
				HttpRequestException httpEx = val;
				throw new Exception("HTTP error: " + ((Exception)(object)httpEx).Message, (Exception)(object)httpEx);
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void pingCellToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await Unkickable(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification("Error: " + ex.Message);
		}
	}

	public async void GetFriendList()
	{
		guna2DataGridView1.Rows.Clear();
		guna2DataGridView1.Columns.Clear();
		DataGridViewImageColumn avatarColumn = new DataGridViewImageColumn
		{
			Name = "Avatar",
			HeaderText = "",
			ImageLayout = DataGridViewImageCellLayout.Stretch
		};
		guna2DataGridView1.Columns.Add(avatarColumn);
		guna2DataGridView1.Columns.Add("Gamertag", "Gamertag");
		guna2DataGridView1.Columns.Add("Xuid", "Xuid");
		guna2DataGridView1.Columns.Add("RealName", "Real Name");
		guna2DataGridView1.Columns.Add("PresenceState", "Presence State");
		guna2DataGridView1.Columns["Gamertag"].Width = 100;
		guna2DataGridView1.Columns["Xuid"].Width = 100;
		guna2DataGridView1.Columns["Avatar"].Width = 30;
		guna2DataGridView1.Columns["RealName"].Width = 100;
		guna2DataGridView1.Columns["PresenceState"].Width = 90;
		try
		{
			HttpClient httpClient = new HttpClient();
			try
			{
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("Authorization", token);
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("x-xbl-contract-version", "2");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				((HttpHeaders)httpClient.DefaultRequestHeaders).TryAddWithoutValidation("Content-Type", "application/json");
				httpClient.DefaultRequestHeaders.Connection.Add("Keep-Alive");
				httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
				string requestUri = "https://peoplehub.xboxlive.com/users/xuid(" + UserID + ")/people/social/decoration/broadcast,multiplayersummary,preferredcolor,socialManager";
				HttpResponseMessage response = await httpClient.GetAsync(requestUri);
				if (!response.IsSuccessStatusCode)
				{
					MessageBox.Show("Failed to retrieve friend list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					return;
				}
				FriendsList? friendsList = JsonConvert.DeserializeObject<FriendsList>(await response.Content.ReadAsStringAsync());
				List<Task> loadTasks = new List<Task>();
				foreach (Friend friend in friendsList.Friends)
				{
					loadTasks.Add(Task.Run(async delegate
					{
						Image avatarImage = await LoadImageAsync(friend.Avatar);
						Invoke((MethodInvoker)delegate
						{
							guna2DataGridView1.Rows.Add(avatarImage, friend.DisplayName, friend.XboxUserId, friend.RealName, friend.PresenceState);
						});
					}));
				}
				await Task.WhenAll(loadTasks);
			}
			finally
			{
				((IDisposable)httpClient)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async Task<Image> LoadImageAsync(string url)
	{
		return await Task.Run(delegate
		{
			try
			{
				if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
				{
					using (WebClient webClient = new WebClient())
					{
						using MemoryStream stream = new MemoryStream(webClient.DownloadData(url));
						return Image.FromStream(stream);
					}
				}
				return (Image)null;
			}
			catch
			{
				return (Image)null;
			}
		});
	}

	private void guna2ControlBox1_Click(object sender, EventArgs e)
	{
	}

	private void ResetRowColors(Guna2DataGridView dgv)
	{
		foreach (DataGridViewRow item in (IEnumerable)dgv.Rows)
		{
			item.DefaultCellStyle.BackColor = Color.Black;
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

	private void guna2DataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
	{
	}

	public async Task<bool> UnfriendUserAsync(string targetXuid)
	{
		string baseUrl = "https://social.xboxlive.com";
		string endpoint = "/users/xuid(" + UserID + ")/people/xuid(" + targetXuid + ")";
		string url = baseUrl + endpoint;
		try
		{
			HttpClient httpClient = new HttpClient();
			try
			{
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("Authorization", token);
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("x-xbl-contract-version", "2");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await httpClient.DeleteAsync(url);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Unfriend request successful.");
					return true;
				}
				Console.WriteLine($"{response.StatusCode}");
				Console.WriteLine("Response body: " + await response.Content.ReadAsStringAsync());
				return false;
			}
			finally
			{
				((IDisposable)httpClient)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
			Console.WriteLine("An error occurred: " + ex.Message);
			return false;
		}
	}

	public async Task<bool> AddFriendUserAsync(string targetXuid)
	{
		string baseUrl = "https://social.xboxlive.com";
		string endpoint = "/users/xuid(" + UserID + ")/people/xuid(" + targetXuid + ")";
		string url = baseUrl + endpoint;
		try
		{
			HttpClient httpClient = new HttpClient();
			try
			{
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("Authorization", token);
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("x-xbl-contract-version", "2");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				HttpResponseMessage response = await httpClient.PutAsync(url, (HttpContent)new StringContent(string.Empty, Encoding.UTF8, "application/json"));
				if (response.IsSuccessStatusCode)
				{
					return true;
				}
				ShowNotification((await response.Content.ReadAsStringAsync()) ?? "");
				return false;
			}
			finally
			{
				((IDisposable)httpClient)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
			return false;
		}
	}

	private async void toolStripMenuItem2_Click_1(object sender, EventArgs e)
	{
		try
		{
			if (guna2DataGridView1.SelectedRows.Count > 0)
			{
				string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
				if (await AddFriendUserAsync(targetXuid))
				{
					ShowNotification("Successful friended the user");
				}
			}
			else
			{
				ShowNotification("No user selected. Please select a user to add as a friend.");
			}
		}
		catch (Exception ex)
		{
			ShowNotification("An error occurred while trying to add the friend.\nException: " + ex.Message);
		}
	}

	private async void toolStripMenuItem3_Click_1(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			if (await UnfriendUserAsync(targetXuid))
			{
				ShowNotification("Successfully unfriend");
			}
		}
		else
		{
			ShowNotification("No user selected. Please select a user to unfriend.");
		}
	}

	private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count >= 3)
			{
				string obj = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
				string cellValue3 = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
				Clipboard.SetText(obj + " " + cellValue3);
			}
			else
			{
				ShowNotification("The selected row does not have enough cells.");
			}
		}
		else
		{
			ShowNotification("No row is selected.");
		}
	}

	private void clearAllToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ShowNotification("Successfully Cleared");
		guna2DataGridView1.Rows.Clear();
	}

	private void guna2GroupBox1_Click(object sender, EventArgs e)
	{
	}

	private void panel2_Paint_1(object sender, PaintEventArgs e)
	{
	}

	private void ApplyBackgroundColor()
	{
		string savedColor = SNIFF.Properties.Settings.Default.BackgroundColor;
		if (!string.IsNullOrEmpty(savedColor))
		{
			try
			{
				Color color2 = (BackColor = ColorTranslator.FromHtml(savedColor));
				guna2GroupBox3.CustomBorderColor = color2;
				guna2GroupBox1.CustomBorderColor = color2;
				guna2GroupBox2.CustomBorderColor = color2;
				panel2.BackColor = color2;
				LoginBTN.BackColor = color2;
				guna2Button1.BackColor = color2;
				guna2Button2.BackColor = color2;
				LoginBTN.FillColor = color2;
				guna2Button1.FillColor = color2;
				guna2Button2.FillColor = color2;
			}
			catch (Exception ex)
			{
				ShowNotification(ex.Message ?? "");
			}
		}
	}

	private void toolStripMenuItem4_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count >= 3)
			{
				string cellValue1 = selectedRow.Cells[0].Value?.ToString() ?? string.Empty;
				string cellValue2 = selectedRow.Cells[1].Value?.ToString() ?? string.Empty;
				string cellValue3 = selectedRow.Cells[2].Value?.ToString() ?? string.Empty;
				Clipboard.SetText(cellValue1 + " " + cellValue2 + " " + cellValue3);
			}
			else
			{
				ShowNotification("The selected row does not have enough cells.");
			}
		}
		else
		{
			ShowNotification("No row is selected.");
		}
	}

	private async Task<List<PartyMember>> XboxGrabParty(string token, string userId)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/get";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = token,
			xuid = userId
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			((HttpHeaders)request.Headers).TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				JToken jsonData = JObject.Parse(await obj.Content.ReadAsStringAsync())["data"];
				if (jsonData["members"] == null || !(jsonData["members"] is JObject membersObject) || !membersObject.Properties().Any())
				{
					throw new Exception("No members found in the party or 'members' data is malformed");
				}
				List<PartyMember> partyMembers = new List<PartyMember>();
				bool isFirst = true;
				foreach (JProperty item in membersObject.Properties())
				{
					if (item.Value is JObject memberValue)
					{
						string gamertag = memberValue["gamertag"]?.ToString() ?? "Unknown";
						string role = (isFirst ? "Host" : "Member");
						string xuid = memberValue["constants"]?["system"]?["xuid"]?.ToString() ?? "Not Found";
						partyMembers.Add(new PartyMember
						{
							Gamertag = gamertag,
							MemberStatus = role,
							Xuid = xuid
						});
						isFirst = false;
					}
				}
				return partyMembers;
			}
			catch (HttpRequestException)
			{
				throw new Exception("No Party Active please join one before pulling");
			}
			catch (JsonException ex)
			{
				throw new Exception("JSON parsing error: " + ex.Message, ex);
			}
			catch (Exception ex2)
			{
				throw new Exception("Error fetching party data: " + ex2.Message, ex2);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private void UpdateDataGridView(List<PartyMember> members)
	{
		dataGridView1.Rows.Clear();
		foreach (PartyMember member in members)
		{
			dataGridView1.Rows.Add(member.Gamertag, member.MemberStatus, member.Xuid);
		}
	}

	private async Task<bool> SendDM(string xuid, string message)
	{
		string endpoint = "/network/xbox/users/me/conversations/users/xuid(" + xuid + ")";
		string url = "https://xblmessaging.xboxlive.com" + endpoint;
		try
		{
			HttpClient httpClient = new HttpClient();
			try
			{
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("Authorization", token ?? "");
				((HttpHeaders)httpClient.DefaultRequestHeaders).Add("x-xbl-contract-version", "1");
				httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
				StringContent requestContent = new StringContent(JsonConvert.SerializeObject(new
				{
					parts = new[]
					{
						new
						{
							contentType = "text",
							version = 0,
							text = message
						}
					}
				}), Encoding.UTF8, "application/json");
				HttpResponseMessage response = await httpClient.PostAsync(url, (HttpContent)(object)requestContent);
				if (response.IsSuccessStatusCode)
				{
					Console.WriteLine("Message sent successfully.");
					return true;
				}
				Console.WriteLine($"Message sending failed with status code: {response.StatusCode}");
				Console.WriteLine("Response body: " + await response.Content.ReadAsStringAsync());
				return false;
			}
			finally
			{
				((IDisposable)httpClient)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred: " + ex.Message);
			return false;
		}
	}

	public async Task<bool> MuteUserAsync(string targetXuid)
	{
		try
		{
			StringContent content = new StringContent(JsonConvert.SerializeObject(new
			{
				xuid = targetXuid
			}), Encoding.UTF8, "application/json");
			HttpResponseMessage response = await httpclient.PutAsync("https://privacy.xboxlive.com/users/me/people/mute", (HttpContent)(object)content);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Successfully muted the user.");
				return true;
			}
			Console.WriteLine("Failed to mute the user: " + response.ReasonPhrase);
			return false;
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred while muting the user: " + ex.Message);
			return false;
		}
	}

	public async Task<bool> UnmuteUserAsync(string targetXuid)
	{
		try
		{
			string queryString = "?xuid=" + targetXuid;
			string requestUri = "https://privacy.xboxlive.com/users/me/people/mute" + queryString;
			HttpResponseMessage response = await httpclient.DeleteAsync(requestUri);
			if (response.IsSuccessStatusCode)
			{
				Console.WriteLine("Successfully unmuted the user.");
				return true;
			}
			Console.WriteLine("Failed to unmute the user: " + response.ReasonPhrase);
			return false;
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred while unmuting the user: " + ex.Message);
			return false;
		}
	}

	private async Task<string> SendFeedbackAsync(string url, object requestBody)
	{
		StringContent content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
		((HttpHeaders)httpclient.DefaultRequestHeaders).Clear();
		((HttpHeaders)httpclient.DefaultRequestHeaders).Add("x-xbl-contract-version", "101");
		((HttpHeaders)httpclient.DefaultRequestHeaders).Add("accept", "application/json");
		((HttpHeaders)httpclient.DefaultRequestHeaders).Add("authorization", token ?? "");
		HttpResponseMessage httpResponse = await httpclient.PostAsync(url, (HttpContent)(object)content);
		if (httpResponse.IsSuccessStatusCode)
		{
			return "Feedback sent successfully!";
		}
		return $"Failed to send feedback. Status code: {httpResponse.StatusCode}";
	}

	private async void massReportToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			string feedbackText = "he was harassing me in the voice chat";
			if (!string.IsNullOrEmpty(feedbackText))
			{
				string url = "https://reputation.xboxlive.com/users/xuid(" + targetXuid + ")/feedback";
				var requestBody = new
				{
					feedbackType = "CommsVoiceMessage",
					textReason = feedbackText,
					evidenceId = (string)null,
					feedbackContext = "User"
				};
				try
				{
					ShowNotification((await SendFeedbackAsync(url, requestBody)) ?? "");
					return;
				}
				catch (Exception ex)
				{
					ShowNotification("Error: " + ex.Message);
					return;
				}
			}
			ShowNotification("Please enter feedback before reporting.");
		}
		else
		{
			ShowNotification("No user selected. Please select a user to report.");
		}
	}

	private async Task<string> OpenPartyRequest(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/open/";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = (token ?? ""),
			xuid = (userID ?? "")
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "No message provided";
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void openPartyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await OpenPartyRequest(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async Task<string> ClosePartyRequest(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/close/";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = (token ?? ""),
			xuid = (userID ?? "")
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "No message provided";
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void closePartyToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await ClosePartyRequest(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async Task<string> CrashPartyRequest(string token, string xuid)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/crash";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = (token ?? ""),
			xuid = (xuid ?? "")
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "Unidentified Error";
			}
			catch (Exception ex)
			{
				throw new Exception("Request failed: " + ex.Message);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async Task<string> NukePartyRequest(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/nukeparty/";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = (token ?? ""),
			xuid = (userID ?? "")
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "No message provided";
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
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

	private async void nukePartyToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await NukePartyRequest(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async Task<string> PartyStatusRequest(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/get";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = token,
			xuid = userID
		});
		HttpClient client = new HttpClient();
		try
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
			{
				Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json")
			};
			try
			{
				HttpResponseMessage obj = await client.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return await obj.Content.ReadAsStringAsync();
			}
			catch (HttpRequestException val)
			{
				HttpRequestException httpRequestException = val;
				throw new Exception("HTTP Request Error: " + ((Exception)(object)httpRequestException).Message, (Exception)(object)httpRequestException);
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)client)?.Dispose();
		}
	}

	private async void partyStatusToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			string responseBody = await PartyStatusRequest(token, UserID);
			if (!string.IsNullOrEmpty(responseBody))
			{
				JToken results = JObject.Parse(responseBody)["data"]["results"]?.FirstOrDefault();
				if (results != null)
				{
					results["status"]?.ToString();
					results["visibility"]?.ToString();
					string accepted = results["accepted"]?.ToString() ?? "0";
					string readRestriction = results["readRestriction"]?.ToString() ?? "None";
					results["startTime"]?.ToString();
					ShowNotification("Total Members: " + accepted + "\nRead Restriction: " + readRestriction);
				}
				else
				{
					ShowNotification("No results found");
				}
			}
			else
			{
				ShowNotification("Empty response received.");
			}
		}
		catch (Exception ex)
		{
			ShowNotification("An unexpected error occurred: " + ex.Message);
		}
	}

	private async void muteUserToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			if (await MuteUserAsync(targetXuid))
			{
				ShowNotification("Successfully muted");
			}
			else
			{
				ShowNotification("Failed to mute user");
			}
		}
		else
		{
			ShowNotification("No user selected. Please select a user to mute.");
		}
	}

	private async void addFriendToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			if (dataGridView1.SelectedRows.Count > 0)
			{
				string targetXuid = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
				if (await AddFriendUserAsync(targetXuid))
				{
					ShowNotification("Successful friended the user");
				}
			}
			else
			{
				ShowNotification("No user selected. Please select a user to add as a friend.");
			}
		}
		catch (Exception ex)
		{
			ShowNotification("An error occurred while trying to add the friend.\nException: " + ex.Message);
		}
	}

	private async void unfriendUserToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			if (await UnfriendUserAsync(targetXuid))
			{
				ShowNotification("Successfully unfriend");
			}
		}
		else
		{
			ShowNotification("No user selected. Please select a user to unfriend.");
		}
	}

	private async void unmuteUserToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			if (await UnmuteUserAsync(targetXuid))
			{
				ShowNotification("Successfully unmuted");
			}
			else
			{
				ShowNotification("Failed to unmute user");
			}
		}
		else
		{
			ShowNotification("No user selected. Please select a user to unmute.");
		}
	}

	private async void massReportToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView1.SelectedRows.Count > 0)
		{
			string targetXuid = guna2DataGridView1.SelectedRows[0].Cells[2].Value.ToString();
			string feedbackText = "he was harassing me in the voice chat";
			if (!string.IsNullOrEmpty(feedbackText))
			{
				string url = "https://reputation.xboxlive.com/users/xuid(" + targetXuid + ")/feedback";
				var requestBody = new
				{
					feedbackType = "CommsVoiceMessage",
					textReason = feedbackText,
					evidenceId = (string)null,
					feedbackContext = "User"
				};
				try
				{
					Alert(await SendFeedbackAsync(url, requestBody), alert.enmType.Success);
					return;
				}
				catch (Exception ex)
				{
					Alert("Error: " + ex.Message, alert.enmType.Error);
					return;
				}
			}
			Alert("Please enter feedback before reporting.", alert.enmType.Warning);
		}
		else
		{
			Alert("No user selected. Please select a user to report.", alert.enmType.Warning);
		}
	}

	private async void antiPartyToolToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await CrashPartyRequest(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification("Error: " + ex.Message);
		}
	}

	private async Task<string> Crashpartyhost(string token, string userID)
	{
		string apiUrl = "https://lolzopzsniff.xyz/api/xbox/party/hostcrash/";
		string jsonString = JsonConvert.SerializeObject(new
		{
			token = (token ?? ""),
			xuid = (userID ?? "")
		});
		HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
		try
		{
			request.Content = (HttpContent)new StringContent(jsonString, Encoding.UTF8, "application/json");
			try
			{
				HttpResponseMessage obj = await httpclient.SendAsync(request);
				obj.EnsureSuccessStatusCode();
				return JObject.Parse(await obj.Content.ReadAsStringAsync())["message"]?.Value<string>() ?? "No message provided";
			}
			catch (Exception ex)
			{
				throw new Exception("Error sending request: " + ex.Message, ex);
			}
		}
		finally
		{
			((IDisposable)request)?.Dispose();
		}
	}

	private async void crashPartyHostToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await Crashpartyhost(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification(ex.Message ?? "");
		}
	}

	private async void LoginBTN_Click(object sender, EventArgs e)
	{
		try
		{
			UpdateDataGridView(await XboxGrabParty(token, UserID));
		}
		catch (Exception ex)
		{
			ShowNotification("Error: " + ex.Message);
		}
	}

	private void guna2Button2_Click_1(object sender, EventArgs e)
	{
		GetFriendList();
	}

	private async void guna2Button1_Click_1(object sender, EventArgs e)
	{
		try
		{
			ShowNotification((await CrashPartyRequest(token, UserID)) ?? "");
		}
		catch (Exception ex)
		{
			ShowNotification("Error: " + ex.Message);
		}
	}

	private void friendsOptionsToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private async void massDmToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			string message = new simpletextprompt().ShowDialog();
			if (string.IsNullOrEmpty(message))
			{
				ShowNotification("No message entered. Please enter a message to send.");
				return;
			}
			int messageCount = 5;
			foreach (DataGridViewRow row in dataGridView1.SelectedRows)
			{
				string targetXuid = row.Cells[2].Value.ToString();
				for (int i = 0; i < messageCount; i++)
				{
					if (await SendDM(targetXuid, message))
					{
						ShowNotification($"Message {i + 1} sent to XUID {targetXuid}");
					}
					else
					{
						ShowNotification($"Failed to send message {i + 1} to XUID {targetXuid}");
					}
				}
			}
		}
		else
		{
			ShowNotification("No user selected. Please select a user to send a message.");
		}
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		notification.Hide();
		timer2.Stop();
	}

	private async void partyMemberCountToolStripMenuItem_Click(object sender, EventArgs e)
	{
		try
		{
			string responseBody = await PartyStatusRequest(token, UserID);
			if (!string.IsNullOrEmpty(responseBody))
			{
				JToken results = JObject.Parse(responseBody)["results"]?.FirstOrDefault();
				if (results != null)
				{
					results["status"]?.ToString();
					results["visibility"]?.ToString();
					string accepted = results["accepted"]?.ToString() ?? "0";
					results["readRestriction"]?.ToString();
					results["startTime"]?.ToString();
					ShowNotification("Total Members: " + accepted);
				}
				else
				{
					ShowNotification("No results found");
				}
			}
			else
			{
				ShowNotification("Empty response received.");
			}
		}
		catch (Exception ex)
		{
			ShowNotification("An unexpected error occurred: " + ex.Message);
		}
	}

	private void guna2DataGridView1_MouseMove(object sender, MouseEventArgs e)
	{
		Guna2DataGridView dgv = sender as Guna2DataGridView;
		DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
		SetRowHoverColor(dgv, hit.RowIndex);
	}

	private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
	{
		Guna2DataGridView dgv = sender as Guna2DataGridView;
		DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
		SetRowHoverColor(dgv, hit.RowIndex);
	}

	private void guna2GroupBox3_Click(object sender, EventArgs e)
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.xboxpartyoptions));
		this.panel3 = new System.Windows.Forms.Panel();
		this.guna2GroupBox2 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.LoginBTN = new Guna.UI2.WinForms.Guna2Button();
		this.guna2Button2 = new Guna.UI2.WinForms.Guna2Button();
		this.guna2Button1 = new Guna.UI2.WinForms.Guna2Button();
		this.guna2GroupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2VScrollBar3 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.dataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.logInContextMenu2 = new LoginTheme.LogInContextMenu();
		this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
		this.copySourceIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.pingCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.partyOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.openPartyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.closePartyToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.partyStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.crashPartyHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.friendsOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.muteUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.unmuteUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.addFriendToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.unfriendUserToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.miscOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.massReportToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.guna2DataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
		this.logInContextMenu1 = new LoginTheme.LogInContextMenu();
		this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
		this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
		this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.panel2 = new System.Windows.Forms.Panel();
		this.label4 = new System.Windows.Forms.Label();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.timer2 = new System.Windows.Forms.Timer(this.components);
		this.guna2Elipse1 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse2 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
		this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
		this.pictureBox2 = new System.Windows.Forms.PictureBox();
		this.panel3.SuspendLayout();
		this.guna2GroupBox2.SuspendLayout();
		this.guna2GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
		this.logInContextMenu2.SuspendLayout();
		this.guna2GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).BeginInit();
		this.logInContextMenu1.SuspendLayout();
		this.panel2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).BeginInit();
		base.SuspendLayout();
		this.panel3.BackColor = System.Drawing.Color.Black;
		this.panel3.Controls.Add(this.guna2GroupBox2);
		this.panel3.Controls.Add(this.guna2GroupBox3);
		this.panel3.Controls.Add(this.guna2GroupBox1);
		this.panel3.Controls.Add(this.panel2);
		this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.panel3.Location = new System.Drawing.Point(0, 0);
		this.panel3.Name = "panel3";
		this.panel3.Size = new System.Drawing.Size(844, 509);
		this.panel3.TabIndex = 720;
		this.panel3.Paint += new System.Windows.Forms.PaintEventHandler(panel3_Paint);
		this.guna2GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.guna2GroupBox2.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox2.BorderThickness = 0;
		this.guna2GroupBox2.Controls.Add(this.LoginBTN);
		this.guna2GroupBox2.Controls.Add(this.guna2Button2);
		this.guna2GroupBox2.Controls.Add(this.guna2Button1);
		this.guna2GroupBox2.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox2.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox2.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox2.Location = new System.Drawing.Point(5, 32);
		this.guna2GroupBox2.Name = "guna2GroupBox2";
		this.guna2GroupBox2.Size = new System.Drawing.Size(191, 204);
		this.guna2GroupBox2.TabIndex = 716;
		this.guna2GroupBox2.Text = "Party Options";
		this.LoginBTN.Animated = true;
		this.LoginBTN.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.LoginBTN.BorderColor = System.Drawing.Color.Empty;
		this.LoginBTN.BorderThickness = 1;
		this.LoginBTN.CheckedState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.LoginBTN.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.LoginBTN.CheckedState.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.LoginBTN.CheckedState.ForeColor = System.Drawing.Color.White;
		this.LoginBTN.DisabledState.ForeColor = System.Drawing.Color.White;
		this.LoginBTN.FillColor = System.Drawing.Color.Transparent;
		this.LoginBTN.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.LoginBTN.ForeColor = System.Drawing.Color.White;
		this.LoginBTN.HoverState.ForeColor = System.Drawing.Color.White;
		this.LoginBTN.Location = new System.Drawing.Point(3, 66);
		this.LoginBTN.Name = "LoginBTN";
		this.LoginBTN.PressedColor = System.Drawing.Color.White;
		this.LoginBTN.Size = new System.Drawing.Size(182, 37);
		this.LoginBTN.TabIndex = 33;
		this.LoginBTN.Text = "Grab My Party";
		this.LoginBTN.Click += new System.EventHandler(LoginBTN_Click);
		this.guna2Button2.Animated = true;
		this.guna2Button2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button2.BorderColor = System.Drawing.Color.Empty;
		this.guna2Button2.BorderThickness = 1;
		this.guna2Button2.CheckedState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button2.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button2.CheckedState.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button2.CheckedState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.FillColor = System.Drawing.Color.Transparent;
		this.guna2Button2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2Button2.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2Button2.Location = new System.Drawing.Point(3, 152);
		this.guna2Button2.Name = "guna2Button2";
		this.guna2Button2.PressedColor = System.Drawing.Color.White;
		this.guna2Button2.Size = new System.Drawing.Size(182, 37);
		this.guna2Button2.TabIndex = 720;
		this.guna2Button2.Text = "Get Friends List";
		this.guna2Button2.Click += new System.EventHandler(guna2Button2_Click_1);
		this.guna2Button1.Animated = true;
		this.guna2Button1.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button1.BorderColor = System.Drawing.Color.Empty;
		this.guna2Button1.BorderThickness = 1;
		this.guna2Button1.CheckedState.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button1.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button1.CheckedState.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Button1.CheckedState.ForeColor = System.Drawing.Color.White;
		this.guna2Button1.DisabledState.ForeColor = System.Drawing.Color.White;
		this.guna2Button1.FillColor = System.Drawing.Color.Transparent;
		this.guna2Button1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2Button1.ForeColor = System.Drawing.Color.White;
		this.guna2Button1.HoverState.ForeColor = System.Drawing.Color.White;
		this.guna2Button1.Location = new System.Drawing.Point(3, 109);
		this.guna2Button1.Name = "guna2Button1";
		this.guna2Button1.PressedColor = System.Drawing.Color.White;
		this.guna2Button1.Size = new System.Drawing.Size(182, 37);
		this.guna2Button1.TabIndex = 719;
		this.guna2Button1.Text = "Crash My Party";
		this.guna2Button1.Click += new System.EventHandler(guna2Button1_Click_1);
		this.guna2GroupBox3.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox3.BorderThickness = 0;
		this.guna2GroupBox3.Controls.Add(this.guna2VScrollBar3);
		this.guna2GroupBox3.Controls.Add(this.dataGridView1);
		this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2GroupBox3.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox3.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox3.Location = new System.Drawing.Point(0, 245);
		this.guna2GroupBox3.Name = "guna2GroupBox3";
		this.guna2GroupBox3.Size = new System.Drawing.Size(844, 264);
		this.guna2GroupBox3.TabIndex = 718;
		this.guna2GroupBox3.Text = "Party Members";
		this.guna2GroupBox3.Click += new System.EventHandler(guna2GroupBox3_Click);
		this.guna2VScrollBar3.BindingContainer = this.dataGridView1;
		this.guna2VScrollBar3.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar3.InUpdate = false;
		this.guna2VScrollBar3.LargeChange = 10;
		this.guna2VScrollBar3.Location = new System.Drawing.Point(822, 45);
		this.guna2VScrollBar3.Minimum = 1;
		this.guna2VScrollBar3.Name = "guna2VScrollBar3";
		this.guna2VScrollBar3.ScrollbarSize = 18;
		this.guna2VScrollBar3.Size = new System.Drawing.Size(18, 214);
		this.guna2VScrollBar3.TabIndex = 711;
		this.guna2VScrollBar3.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar3.ThumbSize = 5f;
		this.guna2VScrollBar3.Value = 1;
		this.dataGridView1.AllowUserToResizeColumns = false;
		this.dataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
		this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
		this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
		this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.dataGridView1.ColumnHeadersHeight = 20;
		this.dataGridView1.Columns.AddRange(this.Column1, this.Column2, this.Column4);
		this.dataGridView1.ContextMenuStrip = this.logInContextMenu2;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
		this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.dataGridView1.Location = new System.Drawing.Point(5, 44);
		this.dataGridView1.Name = "dataGridView1";
		this.dataGridView1.ReadOnly = true;
		this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
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
		this.dataGridView1.RowTemplate.Height = 30;
		this.dataGridView1.Size = new System.Drawing.Size(836, 216);
		this.dataGridView1.TabIndex = 713;
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
		this.dataGridView1.ThemeStyle.RowsStyle.Height = 30;
		this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(231, 229, 255);
		this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(71, 69, 94);
		this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dataGridView1_CellContentClick);
		this.dataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(dataGridView1_MouseMove);
		this.Column1.HeaderText = "Gamertag";
		this.Column1.Name = "Column1";
		this.Column1.ReadOnly = true;
		this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column2.HeaderText = "Role";
		this.Column2.Name = "Column2";
		this.Column2.ReadOnly = true;
		this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column4.HeaderText = "Xuid";
		this.Column4.Name = "Column4";
		this.Column4.ReadOnly = true;
		this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.logInContextMenu2.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu2.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu2.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[6] { this.toolStripMenuItem4, this.copySourceIPToolStripMenuItem, this.pingCellToolStripMenuItem, this.partyOptionsToolStripMenuItem, this.friendsOptionsToolStripMenuItem, this.miscOptionsToolStripMenuItem });
		this.logInContextMenu2.Name = "logInContextMenu2";
		this.logInContextMenu2.ShowImageMargin = false;
		this.logInContextMenu2.Size = new System.Drawing.Size(161, 136);
		this.toolStripMenuItem4.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem4.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem4.Name = "toolStripMenuItem4";
		this.toolStripMenuItem4.Size = new System.Drawing.Size(160, 22);
		this.toolStripMenuItem4.Text = "Copy To Clipboard";
		this.toolStripMenuItem4.Click += new System.EventHandler(toolStripMenuItem4_Click);
		this.copySourceIPToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.copySourceIPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.copySourceIPToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.copySourceIPToolStripMenuItem.Name = "copySourceIPToolStripMenuItem";
		this.copySourceIPToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
		this.copySourceIPToolStripMenuItem.Text = "Clear All";
		this.copySourceIPToolStripMenuItem.Click += new System.EventHandler(copySourceIPToolStripMenuItem_Click);
		this.pingCellToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.pingCellToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.pingCellToolStripMenuItem.Name = "pingCellToolStripMenuItem";
		this.pingCellToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
		this.pingCellToolStripMenuItem.Text = "Become Unkickable";
		this.pingCellToolStripMenuItem.Click += new System.EventHandler(pingCellToolStripMenuItem_Click);
		this.partyOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.openPartyToolStripMenuItem1, this.closePartyToolStripMenuItem1, this.partyStatusToolStripMenuItem, this.crashPartyHostToolStripMenuItem });
		this.partyOptionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.partyOptionsToolStripMenuItem.Name = "partyOptionsToolStripMenuItem";
		this.partyOptionsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
		this.partyOptionsToolStripMenuItem.Text = "Party Options";
		this.openPartyToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.openPartyToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.openPartyToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.openPartyToolStripMenuItem1.Name = "openPartyToolStripMenuItem1";
		this.openPartyToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.openPartyToolStripMenuItem1.Text = "Open Party";
		this.openPartyToolStripMenuItem1.Click += new System.EventHandler(openPartyToolStripMenuItem1_Click);
		this.closePartyToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.closePartyToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.closePartyToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.closePartyToolStripMenuItem1.Name = "closePartyToolStripMenuItem1";
		this.closePartyToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.closePartyToolStripMenuItem1.Text = "Close Party";
		this.closePartyToolStripMenuItem1.Click += new System.EventHandler(closePartyToolStripMenuItem1_Click);
		this.partyStatusToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.partyStatusToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.partyStatusToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.partyStatusToolStripMenuItem.Name = "partyStatusToolStripMenuItem";
		this.partyStatusToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.partyStatusToolStripMenuItem.Text = "Party Status";
		this.partyStatusToolStripMenuItem.Click += new System.EventHandler(partyStatusToolStripMenuItem_Click);
		this.crashPartyHostToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.crashPartyHostToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.crashPartyHostToolStripMenuItem.Name = "crashPartyHostToolStripMenuItem";
		this.crashPartyHostToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.crashPartyHostToolStripMenuItem.Text = "Crash Party Host";
		this.crashPartyHostToolStripMenuItem.Click += new System.EventHandler(crashPartyHostToolStripMenuItem_Click);
		this.friendsOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.muteUserToolStripMenuItem1, this.unmuteUserToolStripMenuItem1, this.addFriendToolStripMenuItem, this.unfriendUserToolStripMenuItem1 });
		this.friendsOptionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.friendsOptionsToolStripMenuItem.Name = "friendsOptionsToolStripMenuItem";
		this.friendsOptionsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
		this.friendsOptionsToolStripMenuItem.Text = "Friends Options";
		this.friendsOptionsToolStripMenuItem.Click += new System.EventHandler(friendsOptionsToolStripMenuItem_Click);
		this.muteUserToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.muteUserToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.muteUserToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.muteUserToolStripMenuItem1.Name = "muteUserToolStripMenuItem1";
		this.muteUserToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.muteUserToolStripMenuItem1.Text = "Mute User";
		this.muteUserToolStripMenuItem1.Click += new System.EventHandler(muteUserToolStripMenuItem1_Click);
		this.unmuteUserToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.unmuteUserToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.unmuteUserToolStripMenuItem1.Name = "unmuteUserToolStripMenuItem1";
		this.unmuteUserToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.unmuteUserToolStripMenuItem1.Text = "Unmute user";
		this.unmuteUserToolStripMenuItem1.Click += new System.EventHandler(unmuteUserToolStripMenuItem1_Click);
		this.addFriendToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.addFriendToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.addFriendToolStripMenuItem.Name = "addFriendToolStripMenuItem";
		this.addFriendToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
		this.addFriendToolStripMenuItem.Text = "Add Friend";
		this.addFriendToolStripMenuItem.Click += new System.EventHandler(addFriendToolStripMenuItem_Click);
		this.unfriendUserToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.unfriendUserToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.unfriendUserToolStripMenuItem1.Name = "unfriendUserToolStripMenuItem1";
		this.unfriendUserToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.unfriendUserToolStripMenuItem1.Text = "Unfriend User";
		this.unfriendUserToolStripMenuItem1.Click += new System.EventHandler(unfriendUserToolStripMenuItem1_Click);
		this.miscOptionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.massReportToolStripMenuItem1 });
		this.miscOptionsToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.miscOptionsToolStripMenuItem.Name = "miscOptionsToolStripMenuItem";
		this.miscOptionsToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
		this.miscOptionsToolStripMenuItem.Text = "Misc Options";
		this.massReportToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.massReportToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.massReportToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.massReportToolStripMenuItem1.Name = "massReportToolStripMenuItem1";
		this.massReportToolStripMenuItem1.Size = new System.Drawing.Size(180, 22);
		this.massReportToolStripMenuItem1.Text = "Mass Report";
		this.massReportToolStripMenuItem1.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
		this.massReportToolStripMenuItem1.Click += new System.EventHandler(massReportToolStripMenuItem1_Click);
		this.guna2GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.guna2GroupBox1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox1.BorderThickness = 0;
		this.guna2GroupBox1.Controls.Add(this.guna2VScrollBar1);
		this.guna2GroupBox1.Controls.Add(this.guna2DataGridView1);
		this.guna2GroupBox1.CustomBorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2GroupBox1.FillColor = System.Drawing.Color.Black;
		this.guna2GroupBox1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.guna2GroupBox1.ForeColor = System.Drawing.Color.White;
		this.guna2GroupBox1.Location = new System.Drawing.Point(202, 32);
		this.guna2GroupBox1.Name = "guna2GroupBox1";
		this.guna2GroupBox1.Size = new System.Drawing.Size(638, 207);
		this.guna2GroupBox1.TabIndex = 715;
		this.guna2GroupBox1.Text = "Friends List";
		this.guna2GroupBox1.Click += new System.EventHandler(guna2GroupBox1_Click);
		this.guna2VScrollBar1.BindingContainer = this.guna2DataGridView1;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 10;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(619, 42);
		this.guna2VScrollBar1.Minimum = 1;
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 160);
		this.guna2VScrollBar1.TabIndex = 715;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.guna2VScrollBar1.Value = 1;
		this.guna2DataGridView1.AllowUserToResizeColumns = false;
		this.guna2DataGridView1.AllowUserToResizeRows = false;
		dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
		this.guna2DataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
		this.guna2DataGridView1.BackgroundColor = System.Drawing.Color.Black;
		this.guna2DataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.guna2DataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
		this.guna2DataGridView1.ColumnHeadersHeight = 20;
		this.guna2DataGridView1.ContextMenuStrip = this.logInContextMenu1;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		this.guna2DataGridView1.DefaultCellStyle = dataGridViewCellStyle8;
		this.guna2DataGridView1.GridColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2DataGridView1.Location = new System.Drawing.Point(1, 41);
		this.guna2DataGridView1.Name = "guna2DataGridView1";
		this.guna2DataGridView1.ReadOnly = true;
		this.guna2DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
		dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
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
		this.guna2DataGridView1.RowTemplate.Height = 30;
		this.guna2DataGridView1.Size = new System.Drawing.Size(637, 162);
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
		this.guna2DataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(guna2DataGridView1_CellContentClick_2);
		this.guna2DataGridView1.MouseMove += new System.Windows.Forms.MouseEventHandler(guna2DataGridView1_MouseMove);
		this.logInContextMenu1.BackColor = System.Drawing.Color.Black;
		this.logInContextMenu1.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.toolStripMenuItem1, this.toolStripMenuItem2, this.toolStripMenuItem3, this.clearAllToolStripMenuItem });
		this.logInContextMenu1.Name = "logInContextMenu2";
		this.logInContextMenu1.ShowImageMargin = false;
		this.logInContextMenu1.Size = new System.Drawing.Size(156, 114);
		this.toolStripMenuItem1.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
		this.toolStripMenuItem1.Name = "toolStripMenuItem1";
		this.toolStripMenuItem1.Size = new System.Drawing.Size(155, 22);
		this.toolStripMenuItem1.Text = "Copy To Clipboard";
		this.toolStripMenuItem1.Click += new System.EventHandler(toolStripMenuItem1_Click_1);
		this.toolStripMenuItem2.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem2.Name = "toolStripMenuItem2";
		this.toolStripMenuItem2.Size = new System.Drawing.Size(155, 22);
		this.toolStripMenuItem2.Text = "Add Friend";
		this.toolStripMenuItem2.Click += new System.EventHandler(toolStripMenuItem2_Click_1);
		this.toolStripMenuItem3.BackColor = System.Drawing.Color.Black;
		this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.toolStripMenuItem3.Name = "toolStripMenuItem3";
		this.toolStripMenuItem3.Size = new System.Drawing.Size(155, 22);
		this.toolStripMenuItem3.Text = "Remove Friend";
		this.toolStripMenuItem3.Click += new System.EventHandler(toolStripMenuItem3_Click_1);
		this.clearAllToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.clearAllToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.clearAllToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
		this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(155, 22);
		this.clearAllToolStripMenuItem.Text = "Clear All";
		this.clearAllToolStripMenuItem.Click += new System.EventHandler(clearAllToolStripMenuItem_Click);
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.guna2ControlBox1);
		this.panel2.Controls.Add(this.guna2ControlBox3);
		this.panel2.Controls.Add(this.guna2ControlBox2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(844, 26);
		this.panel2.TabIndex = 717;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint_1);
		this.label4.BackColor = System.Drawing.Color.Transparent;
		this.label4.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
		this.label4.ForeColor = System.Drawing.Color.White;
		this.label4.Location = new System.Drawing.Point(3, 4);
		this.label4.Name = "label4";
		this.label4.Size = new System.Drawing.Size(101, 17);
		this.label4.TabIndex = 153;
		this.label4.Text = "ZOPZ SNIFF";
		this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
		this.guna2ControlBox1.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox1.TabIndex = 156;
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
		this.guna2ControlBox3.Location = new System.Drawing.Point(754, 0);
		this.guna2ControlBox3.Name = "guna2ControlBox3";
		this.guna2ControlBox3.PressedColor = System.Drawing.Color.Transparent;
		this.guna2ControlBox3.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox3.TabIndex = 158;
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
		this.guna2ControlBox2.Size = new System.Drawing.Size(45, 26);
		this.guna2ControlBox2.TabIndex = 157;
		this.guna2ControlBox2.Click += new System.EventHandler(guna2ControlBox2_Click);
		this.timer1.Enabled = true;
		this.timer1.Interval = 1;
		this.timer2.Interval = 2000;
		this.timer2.Tick += new System.EventHandler(timer2_Tick);
		this.guna2Elipse1.BorderRadius = 10;
		this.guna2Elipse1.TargetControl = this.guna2GroupBox1;
		this.guna2Elipse2.BorderRadius = 10;
		this.guna2Elipse2.TargetControl = this.guna2GroupBox2;
		this.guna2Elipse3.BorderRadius = 10;
		this.guna2Elipse3.TargetControl = this.guna2GroupBox3;
		this.openFileDialog1.FileName = "openFileDialog1";
		this.pictureBox2.BackColor = System.Drawing.Color.Transparent;
		this.pictureBox2.Location = new System.Drawing.Point(343, 216);
		this.pictureBox2.Name = "pictureBox2";
		this.pictureBox2.Size = new System.Drawing.Size(159, 138);
		this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.pictureBox2.TabIndex = 718;
		this.pictureBox2.TabStop = false;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(844, 509);
		base.ControlBox = false;
		base.Controls.Add(this.panel3);
		base.Controls.Add(this.pictureBox2);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "xboxpartyoptions";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.Load += new System.EventHandler(xboxpartyoptions_Load);
		this.panel3.ResumeLayout(false);
		this.guna2GroupBox2.ResumeLayout(false);
		this.guna2GroupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
		this.logInContextMenu2.ResumeLayout(false);
		this.guna2GroupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.guna2DataGridView1).EndInit();
		this.logInContextMenu1.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.pictureBox2).EndInit();
		base.ResumeLayout(false);
	}
}
