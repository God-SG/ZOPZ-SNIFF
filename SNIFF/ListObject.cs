using System.Drawing;

namespace SNIFF;

internal class ListObject
{
	public Image Flag { get; set; }

	public Image ProtectionIcon { get; set; }

	public bool Protected { get; set; }

	public string Username { get; set; }

	public string Gamefilter { get; set; }

	public string IPAddress { get; set; }

	public string Port { get; set; }

	public string Country { get; set; }

	public string Region { get; set; }

	public string City { get; set; }

	public string ISP { get; set; }

	public int InPackets { get; set; }

	public int OutPackets { get; set; }

	public bool IsInbound { get; set; }
}
