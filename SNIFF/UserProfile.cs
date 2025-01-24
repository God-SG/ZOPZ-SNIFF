using System;

namespace SNIFF;

public class UserProfile
{
	public int AvailableUsernameChanges { get; set; }

	public string Email { get; set; }

	public string Phone { get; set; }

	public DateTime Birthday { get; set; }

	public object IsFakeJuniorBirthday { get; set; }

	public int AccountId { get; set; }

	public string Username { get; set; }

	public string DisplayName { get; set; }

	public string ProfileImage { get; set; }

	public string BannerImage { get; set; }

	public bool IsJunior { get; set; }

	public int Platforms { get; set; }

	public int PersonalPronouns { get; set; }

	public int IdentityFlags { get; set; }

	public DateTime CreatedAt { get; set; }

	public bool IsMetaPlatformBlocked { get; set; }
}
