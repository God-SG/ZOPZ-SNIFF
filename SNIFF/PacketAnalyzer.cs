using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Guna.UI.WinForms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Enums;
using LoginTheme;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using SNIFF.Properties;

namespace SNIFF;

public class PacketAnalyzer : Form
{
	private readonly List<Packet> _packets = new List<Packet>();

	private int index;

	private IContainer components;

	private GunaDragControl DragControl_Form;

	private Guna2Panel guna2Panel1;

	private LogInContextMenu logInContextMenu2;

	private ToolStripSeparator toolStripSeparator1;

	private ToolStripMenuItem copyPacketsNumberToolStripMenuItem;

	private ToolStripMenuItem copySourceIPToolStripMenuItem;

	private ImageList imageList1;

	private Timer timer1;

	private Panel panel2;

	private Label label4;

	private Guna2ControlBox guna2ControlBox1;

	private Guna2ControlBox guna2ControlBox3;

	private Guna2ControlBox guna2ControlBox2;

	private Guna2VScrollBar guna2VScrollBar3;

	private Guna2Panel guna2Panel2;

	private Guna2Button packetSelectorRight;

	private Guna2Button packeSelectorLeft;

	private Guna2HtmlLabel indexLbl;

	private Guna2Panel guna2Panel3;

	private Guna2MessageDialog guna2MessageDialog1;

	private TreeView packetTreeView;

	private Guna2VScrollBar guna2VScrollBar1;

	private Guna2HScrollBar guna2HScrollBar1;

	public PacketAnalyzer(List<Packet> packet)
	{
		_packets = packet;
		InitializeComponent();
		ApplyBackgroundColor();
		indexLbl.Text = $"{index + 1}/{_packets.Count}";
		base.MaximizeBox = true;
		base.Name = "ZOPZ SNIFF";
		Text = string.Empty;
		base.ControlBox = false;
		base.StartPosition = FormStartPosition.CenterScreen;
		RenderPacket();
	}

	private void PacketAnalyzer_FormClosing(object sender, FormClosingEventArgs e)
	{
	}

	private void guna2ControlBox2_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void guna2ControlBox1_Click(object sender, EventArgs e)
	{
	}

	private void PacketAnalyzer_Load_1(object sender, EventArgs e)
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
				panel2.BackColor = color;
			}
			catch (Exception ex)
			{
				MessageBox.Show("Error applying background color: " + ex.Message);
			}
		}
	}

	private void listView1_SelectedIndexChanged_1(object sender, EventArgs e)
	{
	}

	private void panel2_Paint(object sender, PaintEventArgs e)
	{
	}

	private void textBoxPacketData_TextChanged(object sender, EventArgs e)
	{
	}

	private void packetSelectorRight_Click(object sender, EventArgs e)
	{
		if (index < _packets.Count - 1)
		{
			index++;
			indexLbl.Text = $"{index + 1}/{_packets.Count}";
			RenderPacket();
		}
	}

	private void packeSelectorLeft_Click(object sender, EventArgs e)
	{
		if (index > 0)
		{
			index--;
			indexLbl.Text = $"{index + 1}/{_packets.Count}";
			RenderPacket();
		}
	}

	private void RenderPacket()
	{
		Packet packet = _packets[index];
		IpV4Datagram ipHdr = packet.Ethernet.IpV4;
		packetTreeView.Nodes.Clear();
		TreeNode ipNode = packetTreeView.Nodes.Add("IP Header");
		TreeNode protocolNode = packetTreeView.Nodes.Add(packet.Ethernet.IpV4.Protocol.ToString().ToUpper());
		TreeNode dataNode = packetTreeView.Nodes.Add("Data Payload");
		string[] iPV4HeaderData = GetIPV4HeaderData(ipHdr);
		foreach (string item in iPV4HeaderData)
		{
			ipNode.Nodes.Add(item);
		}
		iPV4HeaderData = GetProtocolHeaderData(ipHdr);
		foreach (string item2 in iPV4HeaderData)
		{
			protocolNode.Nodes.Add(item2);
		}
		dataNode.Nodes.Add(GetProtocolHeaderPayloadData(ipHdr));
	}

	private string[] GetIPV4HeaderData(IpV4Datagram datagram)
	{
		List<string> data = new List<string>();
		PropertyInfo[] properties = datagram.GetType().GetProperties();
		foreach (PropertyInfo property in properties)
		{
			string name = property.Name;
			string propertyValue = "bad";
			try
			{
				propertyValue = property.GetValue(datagram, null).ToString();
			}
			catch
			{
				continue;
			}
			data.Add(name + ": " + propertyValue);
		}
		return data.ToArray();
	}

	private string[] GetProtocolHeaderData(IpV4Datagram ipHdr)
	{
		UdpDatagram udpHdr = ipHdr.Udp;
		TcpDatagram tcpHdr = ipHdr.Tcp;
		if (udpHdr != null)
		{
			List<string> data = new List<string>();
			PropertyInfo[] properties = udpHdr.GetType().GetProperties();
			foreach (PropertyInfo property in properties)
			{
				string name = property.Name;
				string propertyValue = "bad";
				try
				{
					propertyValue = property.GetValue(udpHdr, null).ToString();
				}
				catch
				{
					continue;
				}
				data.Add(name + ": " + propertyValue);
			}
			return data.ToArray();
		}
		if (tcpHdr != null)
		{
			List<string> data2 = new List<string>();
			PropertyInfo[] properties = tcpHdr.GetType().GetProperties();
			foreach (PropertyInfo property2 in properties)
			{
				string name2 = property2.Name;
				string propertyValue2 = "bad";
				try
				{
					propertyValue2 = property2.GetValue(tcpHdr, null).ToString();
				}
				catch
				{
					continue;
				}
				data2.Add(name2 + ": " + propertyValue2);
			}
			return data2.ToArray();
		}
		return null;
	}

	private string GetProtocolHeaderPayloadData(IpV4Datagram ipHdr)
	{
		UdpDatagram udpHdr = ipHdr.Udp;
		TcpDatagram tcpHdr = ipHdr.Tcp;
		if (udpHdr != null)
		{
			return StringifyPayload(udpHdr.Payload.ToList());
		}
		if (tcpHdr != null)
		{
			return StringifyPayload(tcpHdr.Payload.ToList());
		}
		return null;
	}

	private string StringifyPayload(List<byte> bytes)
	{
		StringBuilder sb = new StringBuilder();
		foreach (byte @byte in bytes)
		{
			sb.Append(@byte.ToString("x2"));
			sb.Append(" ");
		}
		return sb.ToString();
	}

	private void packetTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
	{
		try
		{
			if (e.Node.Nodes.Count <= 0)
			{
				Clipboard.SetText(e.Node.Text);
			}
		}
		catch
		{
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SNIFF.PacketAnalyzer));
		this.DragControl_Form = new Guna.UI.WinForms.GunaDragControl(this.components);
		this.guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
		this.guna2Panel3 = new Guna.UI2.WinForms.Guna2Panel();
		this.guna2VScrollBar1 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.packetTreeView = new System.Windows.Forms.TreeView();
		this.guna2HScrollBar1 = new Guna.UI2.WinForms.Guna2HScrollBar();
		this.guna2Panel2 = new Guna.UI2.WinForms.Guna2Panel();
		this.indexLbl = new Guna.UI2.WinForms.Guna2HtmlLabel();
		this.packetSelectorRight = new Guna.UI2.WinForms.Guna2Button();
		this.packeSelectorLeft = new Guna.UI2.WinForms.Guna2Button();
		this.guna2VScrollBar3 = new Guna.UI2.WinForms.Guna2VScrollBar();
		this.logInContextMenu2 = new LoginTheme.LogInContextMenu();
		this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.copyPacketsNumberToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.copySourceIPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.imageList1 = new System.Windows.Forms.ImageList(this.components);
		this.timer1 = new System.Windows.Forms.Timer(this.components);
		this.panel2 = new System.Windows.Forms.Panel();
		this.label4 = new System.Windows.Forms.Label();
		this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2ControlBox2 = new Guna.UI2.WinForms.Guna2ControlBox();
		this.guna2MessageDialog1 = new Guna.UI2.WinForms.Guna2MessageDialog();
		this.guna2Panel1.SuspendLayout();
		this.guna2Panel3.SuspendLayout();
		this.guna2Panel2.SuspendLayout();
		this.logInContextMenu2.SuspendLayout();
		this.panel2.SuspendLayout();
		base.SuspendLayout();
		this.DragControl_Form.TargetControl = null;
		this.guna2Panel1.BorderColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.guna2Panel1.BorderThickness = 1;
		this.guna2Panel1.Controls.Add(this.guna2Panel3);
		this.guna2Panel1.Controls.Add(this.guna2Panel2);
		this.guna2Panel1.Controls.Add(this.guna2VScrollBar3);
		this.guna2Panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2Panel1.Location = new System.Drawing.Point(0, 31);
		this.guna2Panel1.Name = "guna2Panel1";
		this.guna2Panel1.Size = new System.Drawing.Size(844, 478);
		this.guna2Panel1.TabIndex = 155;
		this.guna2Panel3.Controls.Add(this.guna2HScrollBar1);
		this.guna2Panel3.Controls.Add(this.guna2VScrollBar1);
		this.guna2Panel3.Controls.Add(this.packetTreeView);
		this.guna2Panel3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.guna2Panel3.Location = new System.Drawing.Point(0, 0);
		this.guna2Panel3.Name = "guna2Panel3";
		this.guna2Panel3.Size = new System.Drawing.Size(844, 440);
		this.guna2Panel3.TabIndex = 717;
		this.guna2VScrollBar1.BindingContainer = this.packetTreeView;
		this.guna2VScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar1.InUpdate = false;
		this.guna2VScrollBar1.LargeChange = 10;
		this.guna2VScrollBar1.Location = new System.Drawing.Point(826, 0);
		this.guna2VScrollBar1.Name = "guna2VScrollBar1";
		this.guna2VScrollBar1.ScrollbarSize = 18;
		this.guna2VScrollBar1.Size = new System.Drawing.Size(18, 440);
		this.guna2VScrollBar1.TabIndex = 716;
		this.guna2VScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar1.ThumbSize = 5f;
		this.packetTreeView.BackColor = System.Drawing.Color.Black;
		this.packetTreeView.BorderStyle = System.Windows.Forms.BorderStyle.None;
		this.packetTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
		this.packetTreeView.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.packetTreeView.ForeColor = System.Drawing.Color.GhostWhite;
		this.packetTreeView.Location = new System.Drawing.Point(0, 0);
		this.packetTreeView.Name = "packetTreeView";
		this.packetTreeView.Size = new System.Drawing.Size(844, 440);
		this.packetTreeView.TabIndex = 0;
		this.packetTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(packetTreeView_NodeMouseDoubleClick);
		this.guna2HScrollBar1.BindingContainer = this.packetTreeView;
		this.guna2HScrollBar1.FillColor = System.Drawing.Color.Black;
		this.guna2HScrollBar1.InUpdate = false;
		this.guna2HScrollBar1.LargeChange = 10;
		this.guna2HScrollBar1.Location = new System.Drawing.Point(0, 422);
		this.guna2HScrollBar1.Name = "guna2HScrollBar1";
		this.guna2HScrollBar1.ScrollbarSize = 18;
		this.guna2HScrollBar1.Size = new System.Drawing.Size(844, 18);
		this.guna2HScrollBar1.TabIndex = 717;
		this.guna2HScrollBar1.ThumbColor = System.Drawing.Color.White;
		this.guna2Panel2.Controls.Add(this.indexLbl);
		this.guna2Panel2.Controls.Add(this.packetSelectorRight);
		this.guna2Panel2.Controls.Add(this.packeSelectorLeft);
		this.guna2Panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.guna2Panel2.Location = new System.Drawing.Point(0, 440);
		this.guna2Panel2.Name = "guna2Panel2";
		this.guna2Panel2.Size = new System.Drawing.Size(844, 38);
		this.guna2Panel2.TabIndex = 716;
		this.indexLbl.AutoSize = false;
		this.indexLbl.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.indexLbl.Dock = System.Windows.Forms.DockStyle.Bottom;
		this.indexLbl.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.indexLbl.ForeColor = System.Drawing.Color.GhostWhite;
		this.indexLbl.Location = new System.Drawing.Point(180, 0);
		this.indexLbl.Name = "indexLbl";
		this.indexLbl.Size = new System.Drawing.Size(484, 38);
		this.indexLbl.TabIndex = 2;
		this.indexLbl.Text = "0/10";
		this.indexLbl.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
		this.packetSelectorRight.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packetSelectorRight.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
		this.packetSelectorRight.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
		this.packetSelectorRight.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
		this.packetSelectorRight.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
		this.packetSelectorRight.Dock = System.Windows.Forms.DockStyle.Right;
		this.packetSelectorRight.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packetSelectorRight.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.packetSelectorRight.ForeColor = System.Drawing.Color.White;
		this.packetSelectorRight.Location = new System.Drawing.Point(664, 0);
		this.packetSelectorRight.Name = "packetSelectorRight";
		this.packetSelectorRight.PressedColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packetSelectorRight.Size = new System.Drawing.Size(180, 38);
		this.packetSelectorRight.TabIndex = 1;
		this.packetSelectorRight.Text = ">";
		this.packetSelectorRight.Click += new System.EventHandler(packetSelectorRight_Click);
		this.packeSelectorLeft.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packeSelectorLeft.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
		this.packeSelectorLeft.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
		this.packeSelectorLeft.DisabledState.FillColor = System.Drawing.Color.FromArgb(169, 169, 169);
		this.packeSelectorLeft.DisabledState.ForeColor = System.Drawing.Color.FromArgb(141, 141, 141);
		this.packeSelectorLeft.Dock = System.Windows.Forms.DockStyle.Left;
		this.packeSelectorLeft.FillColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packeSelectorLeft.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.packeSelectorLeft.ForeColor = System.Drawing.Color.White;
		this.packeSelectorLeft.Location = new System.Drawing.Point(0, 0);
		this.packeSelectorLeft.Name = "packeSelectorLeft";
		this.packeSelectorLeft.PressedColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.packeSelectorLeft.Size = new System.Drawing.Size(180, 38);
		this.packeSelectorLeft.TabIndex = 0;
		this.packeSelectorLeft.Text = "<";
		this.packeSelectorLeft.Click += new System.EventHandler(packeSelectorLeft_Click);
		this.guna2VScrollBar3.FillColor = System.Drawing.Color.Black;
		this.guna2VScrollBar3.InUpdate = false;
		this.guna2VScrollBar3.LargeChange = 10;
		this.guna2VScrollBar3.Location = new System.Drawing.Point(826, 0);
		this.guna2VScrollBar3.Name = "guna2VScrollBar3";
		this.guna2VScrollBar3.ScrollbarSize = 18;
		this.guna2VScrollBar3.Size = new System.Drawing.Size(18, 478);
		this.guna2VScrollBar3.TabIndex = 714;
		this.guna2VScrollBar3.ThumbColor = System.Drawing.Color.White;
		this.guna2VScrollBar3.ThumbSize = 5f;
		this.logInContextMenu2.FontColour = System.Drawing.Color.FromArgb(55, 255, 255);
		this.logInContextMenu2.ForeColor = System.Drawing.Color.FromArgb(255, 255, 255);
		this.logInContextMenu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.toolStripSeparator1, this.copyPacketsNumberToolStripMenuItem, this.copySourceIPToolStripMenuItem });
		this.logInContextMenu2.Name = "logInContextMenu2";
		this.logInContextMenu2.ShowImageMargin = false;
		this.logInContextMenu2.Size = new System.Drawing.Size(138, 54);
		this.toolStripSeparator1.Name = "toolStripSeparator1";
		this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
		this.copyPacketsNumberToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.copyPacketsNumberToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.copyPacketsNumberToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.copyPacketsNumberToolStripMenuItem.Name = "copyPacketsNumberToolStripMenuItem";
		this.copyPacketsNumberToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
		this.copyPacketsNumberToolStripMenuItem.Text = "Copy IP Address";
		this.copySourceIPToolStripMenuItem.BackColor = System.Drawing.Color.Black;
		this.copySourceIPToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold);
		this.copySourceIPToolStripMenuItem.ForeColor = System.Drawing.Color.White;
		this.copySourceIPToolStripMenuItem.Name = "copySourceIPToolStripMenuItem";
		this.copySourceIPToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
		this.copySourceIPToolStripMenuItem.Text = "Copy Label";
		this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
		this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
		this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.timer1.Enabled = true;
		this.timer1.Interval = 1;
		this.panel2.BackColor = System.Drawing.Color.FromArgb(25, 25, 25);
		this.panel2.Controls.Add(this.label4);
		this.panel2.Controls.Add(this.guna2ControlBox1);
		this.panel2.Controls.Add(this.guna2ControlBox3);
		this.panel2.Controls.Add(this.guna2ControlBox2);
		this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
		this.panel2.Location = new System.Drawing.Point(0, 0);
		this.panel2.Name = "panel2";
		this.panel2.Size = new System.Drawing.Size(844, 26);
		this.panel2.TabIndex = 164;
		this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(panel2_Paint);
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
		this.guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
		this.guna2MessageDialog1.Caption = null;
		this.guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.None;
		this.guna2MessageDialog1.Parent = null;
		this.guna2MessageDialog1.Style = Guna.UI2.WinForms.MessageDialogStyle.Dark;
		this.guna2MessageDialog1.Text = null;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		this.BackColor = System.Drawing.Color.Black;
		base.ClientSize = new System.Drawing.Size(844, 509);
		base.Controls.Add(this.panel2);
		base.Controls.Add(this.guna2Panel1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
		base.Name = "PacketAnalyzer";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		base.FormClosing += new System.Windows.Forms.FormClosingEventHandler(PacketAnalyzer_FormClosing);
		base.Load += new System.EventHandler(PacketAnalyzer_Load_1);
		this.guna2Panel1.ResumeLayout(false);
		this.guna2Panel3.ResumeLayout(false);
		this.guna2Panel2.ResumeLayout(false);
		this.logInContextMenu2.ResumeLayout(false);
		this.panel2.ResumeLayout(false);
		base.ResumeLayout(false);
	}
}
