using PacketDotNet;
using System.Reflection;
using System.Text;

namespace ZOPZ_SNIFF.Menus
{
    public partial class PacketAnalyzer : Form
    {
        private readonly List<Packet> _packets = new List<Packet>();
        private int index = 0;

        public PacketAnalyzer(List<Packet> packets)
        {
            InitializeComponent();
            _packets = packets;
            RenderPacket();
        }

        private void packetTreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node?.Nodes.Count > 0)
            {
                Clipboard.SetText(e.Node.Text);
            }
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            if (index > _packets.Count)
            {
                index++;
                RenderPacket();
            }
        }

        private void packeSelectorLeft_Click(object sender, EventArgs e)
        {
            if (index != 0)
            {
                index--;
                RenderPacket();
            }
        }


        private void RenderPacket()
        {
            var packet = _packets[index];
            var ethPacket = packet.Extract<EthernetPacket>();
            var ipHdr = packet.Extract<IPv4Packet>();
            var tcpPacket = packet.Extract<TcpPacket>();
            var udpPacket = packet.Extract<UdpPacket>();

            packetTreeView.Nodes.Clear();

            TreeNode packetNode = packetTreeView.Nodes.Add("Packet Information");

            if (ethPacket != null)
            {
                TreeNode ethNode = packetNode.Nodes.Add("Ethernet Header");
                ethNode.Nodes.Add($"Source Hardware Address: {ethPacket.SourceHardwareAddress}");
                ethNode.Nodes.Add($"Destination Hardware Address: {ethPacket.DestinationHardwareAddress}");
            }

            if (ipHdr != null)
            {
                TreeNode ipNode = packetNode.Nodes.Add("IP Header");
                foreach (string ipData in GetIPV4HeaderData(ipHdr))
                {
                    ipNode.Nodes.Add(ipData);
                }

                if (tcpPacket != null)
                {
                    TreeNode tcpNode = packetNode.Nodes.Add("TCP Packet");
                    tcpNode.Nodes.Add($"Protocol: Transmission Control Protocol");
                    tcpNode.Nodes.Add($"Destination Port: {tcpPacket.DestinationPort}");
                    tcpNode.Nodes.Add($"Source Port: {tcpPacket.SourcePort}");
                    tcpNode.Nodes.Add($"Acknowledgment: {tcpPacket.Acknowledgment}");
                    tcpNode.Nodes.Add($"Checksum: {tcpPacket.Checksum}");
                    tcpNode.Nodes.Add($"Flags: {tcpPacket.Flags}");
                    tcpNode.Nodes.Add($"Valid TCP Checksum: {tcpPacket.ValidTcpChecksum}");
                    tcpNode.Nodes.Add($"Urgent: {tcpPacket.Urgent}");

                    TreeNode ipDetailsNode = tcpNode.Nodes.Add("IP Packet");
                    ipDetailsNode.Nodes.Add($"Destination Address: {ipHdr.DestinationAddress}");
                    ipDetailsNode.Nodes.Add($"Source Address: {ipHdr.SourceAddress}");
                    ipDetailsNode.Nodes.Add($"IP Version: {ipHdr.Version}");
                    ipDetailsNode.Nodes.Add($"Protocol: {ipHdr.Protocol}");
                    ipDetailsNode.Nodes.Add($"Time To Live (TTL): {ipHdr.TimeToLive}");
                }
                else if (udpPacket != null)
                {
                    TreeNode udpNode = packetNode.Nodes.Add("UDP Packet");
                    udpNode.Nodes.Add($"Protocol: User Datagram Protocol");
                    udpNode.Nodes.Add($"Destination Port: {udpPacket.DestinationPort}");
                    udpNode.Nodes.Add($"Source Port: {udpPacket.SourcePort}");
                    udpNode.Nodes.Add($"Checksum: {udpPacket.Checksum}");
                    udpNode.Nodes.Add($"Valid UDP Checksum: {udpPacket.ValidUdpChecksum}");

                    TreeNode ipDetailsNode = udpNode.Nodes.Add("IP Packet");
                    ipDetailsNode.Nodes.Add($"Destination Address: {ipHdr.DestinationAddress}");
                    ipDetailsNode.Nodes.Add($"Source Address: {ipHdr.SourceAddress}");
                    ipDetailsNode.Nodes.Add($"IP Version: {ipHdr.Version}");
                    ipDetailsNode.Nodes.Add($"Protocol: {ipHdr.Protocol}");
                    ipDetailsNode.Nodes.Add($"Time To Live (TTL): {ipHdr.TimeToLive}");
                }
            }

            TreeNode dataNode = packetNode.Nodes.Add("Data Payload");
            dataNode.Nodes.Add(GetProtocolHeaderPayloadData(ipHdr));
        }
        private string[] GetIPV4HeaderData(IPv4Packet datagram)
        {
            List<string> data = new List<string>();
            foreach (PropertyInfo property in datagram.GetType().GetProperties())
            {
                if (property.Name.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                try
                {
                    data.Add($"{property.Name}: {property.GetValue(datagram)}");
                }
                catch
                {
                    data.Add($"{property.Name}: Unable to retrieve value");
                }
            }
            return data.ToArray();
        }

        // Function to retrieve protocol header data (TCP or UDP)
        private string[] GetProtocolHeaderData(Packet packet)
        {
            UdpPacket udpHdr = packet.Extract<UdpPacket>();
            TcpPacket tcpHdr = packet.Extract<TcpPacket>();
            object value = udpHdr != null ? udpHdr : tcpHdr;
            List<string> data = new List<string>();

            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                if (property.Name.Equals("Color", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                try
                {
                    data.Add($"{property.Name}: {property.GetValue(value)}");
                }
                catch
                {
                    data.Add($"{property.Name}: Unable to retrieve value");
                }
            }
            return data.ToArray();
        }
        private void CopyTreeViewContentToClipboard()
        {
            StringBuilder treeContent = new StringBuilder();

            foreach (TreeNode rootNode in packetTreeView.Nodes)
            {
                AppendNodeText(rootNode, treeContent, 0); 
            }

            Clipboard.SetText(treeContent.ToString());
        }
        private void AppendNodeText(TreeNode node, StringBuilder treeContent, int level)
        {
            treeContent.AppendLine(new string(' ', level * 4) + node.Text);

            // Recursively add child nodes
            foreach (TreeNode childNode in node.Nodes)
            {
                AppendNodeText(childNode, treeContent, level + 1);
            }
        }

        private void CopyButton_Click(object sender, EventArgs e)
        {
            CopyTreeViewContentToClipboard();
            MessageBox.Show("TreeView content copied to clipboard!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private string GetProtocolHeaderPayloadData(Packet ipHdr)
        {
            UdpPacket udpHdr = ipHdr.Extract<UdpPacket>();
            TcpPacket tcpHdr = ipHdr.Extract<TcpPacket>();
            byte[]? payloadData = udpHdr?.PayloadData ?? tcpHdr?.PayloadData;

            if (payloadData == null || payloadData.Length == 0)
            {
                return "N/A";
            }

            return string.Join(" ", payloadData.Select(b => b.ToString("x2")));
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void guna2ControlBox3_Click(object sender, EventArgs e)
        {
            CopyTreeViewContentToClipboard();
        }

        private void PacketAnalyzer_Load(object sender, EventArgs e)
        {
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
    }
}
