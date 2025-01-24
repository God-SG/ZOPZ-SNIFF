using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SNIFF.Properties;

[CompilerGenerated]
[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.11.0.0")]
internal sealed class Settings : ApplicationSettingsBase
{
	private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());

	public static Settings Default => defaultInstance;

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int BackgroundImagePath
	{
		get
		{
			return (int)this["BackgroundImagePath"];
		}
		set
		{
			this["BackgroundImagePath"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int SelectedIndex
	{
		get
		{
			return (int)this["SelectedIndex"];
		}
		set
		{
			this["SelectedIndex"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("0")]
	public int Portfilter
	{
		get
		{
			return (int)this["Portfilter"];
		}
		set
		{
			this["Portfilter"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string LastMachineIP
	{
		get
		{
			return (string)this["LastMachineIP"];
		}
		set
		{
			this["LastMachineIP"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string Background
	{
		get
		{
			return (string)this["Background"];
		}
		set
		{
			this["Background"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string BackgoundPic
	{
		get
		{
			return (string)this["BackgoundPic"];
		}
		set
		{
			this["BackgoundPic"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string SelectedGameFilters
	{
		get
		{
			return (string)this["SelectedGameFilters"];
		}
		set
		{
			this["SelectedGameFilters"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string BackgroundColor
	{
		get
		{
			return (string)this["BackgroundColor"];
		}
		set
		{
			this["BackgroundColor"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string SelectedColor
	{
		get
		{
			return (string)this["SelectedColor"];
		}
		set
		{
			this["SelectedColor"] = value;
		}
	}

	[UserScopedSetting]
	[DebuggerNonUserCode]
	[DefaultSettingValue("")]
	public string IndicatorPosition
	{
		get
		{
			return (string)this["IndicatorPosition"];
		}
		set
		{
			this["IndicatorPosition"] = value;
		}
	}
}
