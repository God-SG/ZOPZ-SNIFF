using System.Security.Principal;

namespace SNIFF.Classes.Auth;

public class SystemGuid
{
	private static string _systemGuid = string.Empty;

	public static string GetHwid()
	{
		return WindowsIdentity.GetCurrent().User.Value;
	}
}
