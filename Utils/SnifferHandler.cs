using PacketDotNet;
using SharpPcap;
using SharpPcap.LibPcap;
using System.Net;
using System.Net.Sockets;
using ZOPZ_SNIFF.Json.Sniffer;

namespace ZOPZ_SNIFF.Utils
{
    public class SnifferHandler
    {
        private bool Active { get; set; }
        private LibPcapLiveDevice? liveDevice;
        public event Action<Packet>? Captured;

        public SnifferHandler()
        {
            Active = false;
        }

        public void Initialize(LibPcapLiveDevice newDevice)
        {
            liveDevice = newDevice;
            liveDevice.OnPacketArrival += OnPacketArrival;
            liveDevice.Open(DeviceModes.Promiscuous | DeviceModes.NoCaptureLocal, 1000);
            liveDevice.StartCapture();
        }

        public void SetActive(bool active) => Active = active;

        public bool GetState() => Active;

        private void OnPacketArrival(object sender, PacketCapture e)
        {
            if (Active && Captured != null)
            {
                RawCapture rawPacket = e.GetPacket();
                Packet packet = Packet.ParsePacket(rawPacket.LinkLayerType, rawPacket.Data);
                if (!PrefilterPacket(packet))
                    Captured.Invoke(packet);
            }
        } 

        private bool PrefilterPacket(Packet packet)
        {
            IPv4Packet ipPacket = packet.Extract<IPv4Packet>();
            return ipPacket == null || IsPrivateIp(ipPacket.DestinationAddress.ToString());
        }

        public (ushort SourcePort, ushort DestinationPort) ExtractPort(Packet packet)
        {
            UdpPacket udpPacket = packet.Extract<UdpPacket>();
            if (udpPacket != null)
                return (udpPacket.SourcePort, udpPacket.DestinationPort);
            TcpPacket tcpPacket = packet.Extract<TcpPacket>();
            if (tcpPacket != null)
                return (tcpPacket.SourcePort, tcpPacket.DestinationPort);
            return (0, 0);
        }

        private bool IsPrivateIp(string ipAddress)
        {
            if (!IPAddress.TryParse(ipAddress, out IPAddress? ip))
                throw new ArgumentException("Invalid IP address format.");
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                byte[] ipBytes = ip.GetAddressBytes();
                return IsInRange(ipBytes, new byte[] { 10, 0, 0, 0 }, new byte[] { 10, 255, 255, 255 }) ||
                       IsInRange(ipBytes, new byte[] { 172, 16, 0, 0 }, new byte[] { 172, 31, 255, 255 }) ||
                       IsInRange(ipBytes, new byte[] { 192, 168, 0, 0 }, new byte[] { 192, 168, 255, 255 }) ||
                       IsInRange(ipBytes, new byte[] { 127, 0, 0, 0 }, new byte[] { 127, 255, 255, 255 });
            }
            if (ip.AddressFamily == AddressFamily.InterNetworkV6)
                return (ip.GetAddressBytes()[0] & 0xFE) == 0xFC; 
            return false;
        }

        private bool IsInRange(byte[] address, byte[] start, byte[] end)
        {
            for (int i = 0; i < address.Length; i++)
            {
                if (address[i] < start[i] || address[i] > end[i])
                    return false;
            }
            return true;
        }

        public byte[] GetPacketDataPayload(Packet packet)
        {
            return packet.Extract<UdpPacket>()?.PayloadData ??
                   packet.Extract<TcpPacket>()?.PayloadData ??
                   Array.Empty<byte>();
        }

        public bool IsPortInRange(IEnumerable<string> ports, int port)
        {
            return ports.Any(portRange => TryParsePortRange(portRange, out int lower, out int upper) && port >= lower && port <= upper);
        }

        private bool TryParsePortRange(string portRange, out int lower, out int upper)
        {
            lower = upper = 0;
            if (!portRange.Contains("-")) return false;
            string[] ranges = portRange.Split('-');
            if (ranges.Length == 2 && int.TryParse(ranges[0], out lower) && int.TryParse(ranges[1], out upper))
            {
                return lower <= upper;
            }
            return false;
        }

        public bool IsLengthValid(FilterOption option, int payloadLength)
        {
            return (option.MinimumLength == 0 && option.MaximumLength == 0) || (payloadLength >= option.MinimumLength && payloadLength <= option.MaximumLength);
        }

        public bool IsPayloadMatching(IEnumerable<string>? payloads, byte[] payload)
        {
            if (payloads == null || !payloads.Any()) return false;
            foreach (string payloadMatch in payloads)
            {
                byte[] matchPayload = payloadMatch.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(hex => Convert.ToByte(hex, 16)).ToArray();
                if (payload.AsSpan().IndexOf(matchPayload) >= 0)
                    return true;
            }
            return false;
        }
    }
}
